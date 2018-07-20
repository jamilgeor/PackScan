using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Mono.Options;
using PackScan.Model;
using PackScan.PackageParser;
using PackScan.PackageScanner;
using PackScan.PackageRenderer;

namespace PackScan
{
    class Program
    {
        static readonly IPackageProcessor _processor = new PackageProcessor(new Renderer());

        static async Task Main(string[] args)
        {
            var showHelp = false;
            var filePath = string.Empty;
            var verbose = false;

            var options = new OptionSet {
                { "f|file=", "either a nuget package.config or .csproj file containing nuget references", f => filePath = f},
                { "v|verbose", "output full report details for insecure packages", v => verbose = true },
                { "h|help", "show help", h => showHelp = true}
            };

            if(showHelp || !args.Any())
            {
                ShowHelp(options);
                return;
            }
            
            try 
            {
                var packages = PackageListBuilder
                                .Build()
                                .WithArgs(options.Parse(args))
                                .WithFilePath(filePath)
                                .Create();

                await _processor.Process(packages, verbose);

                Exit(packages.Any(x => x.Vulnerabilities.Any()) ? ExitCode.VulnerabilitiesFound : ExitCode.Success);
            } catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
                Exit(ExitCode.ExceptionOccurred);
            }
        }

        static void Exit(ExitCode code)
        {
            Environment.Exit((int)code);
        }

        static void ShowHelp(OptionSet options)
        {
            Console.WriteLine("Usage: packscan [OPTIONS]+ nuget|file");
            Console.WriteLine("Show vulnerability status of a NuGet package or packages.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            options.WriteOptionDescriptions(Console.Out);
        }
    }

    public enum ExitCode
    {
        Success = 0,
        VulnerabilitiesFound = 1,
        ExceptionOccurred = 2
    }
}
