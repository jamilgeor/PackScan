using System.Collections.Generic;

namespace PackScan.Model
{
    public class Package
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public List<Vulnerability> Vulnerabilities { get; private set; } = new List<Vulnerability>();
    }
}