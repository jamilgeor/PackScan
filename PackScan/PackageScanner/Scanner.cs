using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using PackScan.Model;

namespace PackScan.PackageScanner
{
    public class Scanner
    {
        readonly List<IScanCommand> _commands = new List<IScanCommand>();

        public Scanner(IEnumerable<Package> packages, bool verbose)
        {
            _commands.Add(new ScanOssIndexCommand(packages, verbose));
        }

        public async Task Execute()
        {
            foreach(var command in _commands) await command.Execute();
        }
    }
}