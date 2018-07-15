using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PackScan.Model;

namespace PackScan.PackageScanner
{
    public class ScanOssIndexCommand : IScanCommand
    {
        readonly IEnumerable<Package> _packages;
        readonly bool _verbose;

        public ScanOssIndexCommand(IEnumerable<Package> packages, bool verbose)
        {
            _packages = packages;
            _verbose = verbose;
        }

        public async Task Execute()
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { coordinates = _packages.Select(x => $"nuget:{x.Title}@{x.Version}") }));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.ossindex.component-report-request.v1+json");
                client.BaseAddress = new Uri("https://ossindex.net/api/v3/");
                
                var result = await client.PostAsync("component-report", content);
                var reports = JsonConvert.DeserializeObject<List<OSSIndexComponentReport>>(await result.Content.ReadAsStringAsync());
                
                foreach(var report in reports)
                {
                    if (!report.vulnerabilities.Any()) continue;

                    var package = _packages.FirstOrDefault(x => report.coordinates == $"nuget:{x.Title}@{x.Version}");
                    if(package == null) continue;
                    
                    package.Vulnerabilities.AddRange(report.vulnerabilities.Select(x => new Vulnerability{
                        Title = x.title,
                        Description = x.description,
                        Reference = x.reference
                    }));
                }
            }
        }

        class OSSIndexComponentReport
        {
            public string coordinates { get; set; }
            public string description { get; set; }
            public string reference { get; set; }
            public string title { get { return reference.Replace("https://ossindex.net/component/nuget:", string.Empty); } }
            public IEnumerable<OSSIndexVulnerability> vulnerabilities { get; set; }
        }

        class OSSIndexVulnerability
        {
            public string id { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public double cvssScore { get; set; }
            public string cvssVector { get; set; }
            public string cwe { get; set; }
            public string reference { get; set; }
        }
    }
}