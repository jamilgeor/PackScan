using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace PackScan.PackageParser
{
    public class CSProjParser : IPackageParser
    {
        public IEnumerable<string> Parse(Stream stream)
        {
            try
            {
                var project = new XmlDocument();
                project.Load(stream);
                var packageElements = project.DocumentElement.SelectNodes("//ItemGroup/PackageReference");
                var packages = new List<string>();

                foreach(var package in packageElements)
                {
                    var element = (XmlElement)package;
                    packages.Add($"{element.Attributes["Include"].Value}@{element.Attributes["Version"].Value}");
                }

                return packages;
            } catch {
                throw new ParserException();
            }
        }
    }
}