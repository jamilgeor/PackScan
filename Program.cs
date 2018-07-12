using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Mono.Options;
using PackScan.Model;
using PackScan.PackageLoader;
using PackScan.PackageScanner;
using PackScan.PackageRenderer;

namespace PackScan
{
    class Program
    {
        static string file;
        static bool verbose;

        static async Task Main(string[] args)
        {
            var options = new OptionSet {
                { "f|file=", "either a nuget package.config or .csproj file containing nuget references", f => file = f},
                { "v|verbose", "output full report details for insecure packages", v => verbose = true }
            };
            
            var packages = BuildPackageList(options, args);
            await Process(packages, new Renderer());
        }

        static IEnumerable<Package> BuildPackageList(OptionSet options, string[] args)
        {
            var packages = new List<Package>();

            var extras = options.Parse(args);
            if(extras.Any()) packages.AddRange(extras.Select(x => new Package { Title = x.Split("@").First(), Version = x.Split("@").Last() }));

            if(!string.IsNullOrEmpty(file))
            {
                var packageLoader = GetPackageLoader(file);
                if(packageLoader == null) return packages;
                
                packages.AddRange(packageLoader.GetPackages(file).Select(x => new Package { Title = x.Split("@").First(), Version = x.Split("@").Last() }));
            }

            return packages;
        }

        static async Task Process(IEnumerable<Package> packages, Renderer renderer)
        {
            var scannner = new Scanner(packages, verbose);
            await scannner.Execute();

            packages.Where(x => !x.Vulnerabilities.Any())
                    .ToList()
                    .ForEach(x => renderer.OutputSuccess(x));

            packages.Where(x => x.Vulnerabilities.Any())
                    .ToList()
                    .ForEach(x => renderer.OutputFailure(x, verbose));
        }

        static IPackageLoaderStrategy GetPackageLoader(string fileName)
        {
            var file = new FileInfo(fileName);

            switch(file.Extension.ToLower())
            {
                case ".csproj":
                    return new CSProjLoaderStrategy();
                case ".config":
                    return new NuGetLoaderStrategy();
            }

            Console.Error.WriteLine("File format not supported.");
            return null;
        }
    }
}
