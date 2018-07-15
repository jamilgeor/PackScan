using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace PackScan.PackageLoader
{
    public class NuGetLoaderStrategy : IPackageLoaderStrategy
    {
        public IEnumerable<string> Parse(Stream stream)
        {
            try {
                var project = new XmlDocument();
                project.Load(stream);

                var packageElements = project.DocumentElement.SelectNodes("//packages/package");
                var packages = new List<string>();

                foreach(var package in packageElements)
                {
                    var element = (XmlElement)package;
                    packages.Add($"{element.Attributes["id"].Value}@{element.Attributes["version"].Value}");
                }

                return packages;
            } catch {
                throw new ParserException();
            }
        }
    }
}