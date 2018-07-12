using System.Collections.Generic;

namespace PackScan.PackageLoader
{
    public interface IPackageLoaderStrategy
    {
        IEnumerable<string> GetPackages(string file);
    }
}