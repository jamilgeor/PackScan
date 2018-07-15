using System.Collections.Generic;
using System.IO;

namespace PackScan.PackageLoader
{
    public interface IPackageLoaderStrategy
    {
        IEnumerable<string> Parse(Stream stream);
    }
}