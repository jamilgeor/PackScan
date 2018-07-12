using System.Collections.Generic;
using System.Xml;

namespace PackScan.PackageLoader
{
    public class NuGetLoaderStrategy : IPackageLoaderStrategy
    {
        public IEnumerable<string> GetPackages(string file)
        {
            var project = new XmlDocument();
            project.Load(file);

            var packageElements = project.DocumentElement.SelectNodes("//packages/package");
            var packages = new List<string>();

            foreach(var package in packageElements)
            {
                var element = (XmlElement)package;
                packages.Add($"{element.Attributes["id"].Value}@{element.Attributes["version"].Value}");
            }

            return packages;
        }
    }
}