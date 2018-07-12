using System.Collections.Generic;
using System.Xml;

namespace PackScan.PackageLoader
{
    public class CSProjLoaderStrategy : IPackageLoaderStrategy
    {
        public IEnumerable<string> GetPackages(string file)
        {
            var project = new XmlDocument();
            project.Load(file);

            var packageElements = project.DocumentElement.SelectNodes("//ItemGroup/PackageReference");
            var packages = new List<string>();

            foreach(var package in packageElements)
            {
                var element = (XmlElement)package;
                packages.Add($"{element.Attributes["Include"].Value}@{element.Attributes["Version"].Value}");
            }

            return packages;
        }
    }
}