using System;
using System.IO;

namespace PackScan.PackageParser
{
    public class ParserFactory
    {
        public IPackageParser CreateParser(string fileName)
        {
            var file = new FileInfo(fileName);

            switch(file.Extension.ToLower())
            {
                case ".csproj":
                    return new CSProjParser();
                case ".config":
                    return new NuGetParser();
            }

            throw new Exception("File format not supported.");
        }
    }
}