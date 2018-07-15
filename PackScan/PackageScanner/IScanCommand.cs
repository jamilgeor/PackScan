using System.Threading.Tasks;

namespace PackScan.PackageScanner
{
    public interface IScanCommand
    {
        Task Execute();
    }
}