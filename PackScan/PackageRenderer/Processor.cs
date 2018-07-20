using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using PackScan.Model;
using PackScan.PackageScanner;

namespace PackScan.PackageRenderer
{
    public interface IPackageProcessor
    {
        Task Process(IEnumerable<Package> packages, bool verbose);
    }

    public class PackageProcessor : IPackageProcessor
    {
        readonly Renderer _renderer;

        public PackageProcessor(Renderer renderer)
        {
            _renderer = renderer;
        }

        public async Task Process(IEnumerable<Package> packages, bool verbose)
        {
            var scannner = new Scanner(packages, verbose);
            await scannner.Execute();

            packages.Where(x => !x.Vulnerabilities.Any())
                    .ToList()
                    .ForEach(x => _renderer.OutputSuccess(x));

            packages.Where(x => x.Vulnerabilities.Any())
                    .ToList()
                    .ForEach(x => _renderer.OutputFailure(x, verbose));
        }
    }
}