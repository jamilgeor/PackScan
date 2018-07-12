using System;
using PackScan.Model;

namespace PackScan.PackageRenderer
{
    public class Renderer
    {
        public void OutputSuccess (Package package)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[✓] ");
            Console.ResetColor();
            Console.WriteLine($"{package.Title}@{package.Version}");
        }

        public void OutputFailure (Package package, bool verbose)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("[x] ");
            Console.ResetColor();
            Console.WriteLine($"{package.Title}@{package.Version}");
            if (verbose)
            {
                foreach(var vulnerability in package.Vulnerabilities)
                {
                    Console.WriteLine(vulnerability.Title);
                    Console.WriteLine(vulnerability.Description);
                    Console.WriteLine(vulnerability.Reference);
                    Console.WriteLine();
                }
            }
        }
    }
}