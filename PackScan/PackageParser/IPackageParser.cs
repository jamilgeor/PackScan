using System.Collections.Generic;
using System.IO;

namespace PackScan.PackageParser
{
    public interface IPackageParser
    {
        IEnumerable<string> Parse(Stream stream);
    }
}