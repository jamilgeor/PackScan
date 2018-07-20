using System.Linq;
using System.IO;
using System.Collections.Generic;
using PackScan.Model;
using PackScan.PackageParser;

namespace PackScan
{
    public class PackageListBuilder
    {
        static readonly ParserFactory _parserFactory = new ParserFactory();
        readonly List<Package> _packages;
        private PackageListBuilder(List<Package> packages)
        {
            _packages = packages;
        }
        public static PackageListBuilder Build()
        {
            return new PackageListBuilder(new List<Package>());
        }

        public PackageListBuilder WithArgs(IEnumerable<string> args)
        {
            if(!args.Any()) return this;
            
            var packages = args.Select(x => 
                                        new Package { 
                                            Title = x.Split("@").First(), 
                                            Version = x.Split("@").Last() 
                                        });
                                        
            _packages.AddRange(packages);

            return this;
        }

        public PackageListBuilder WithFilePath(string filePath)
        {
            using(var stream = File.OpenRead(filePath))
            {
                var parser = _parserFactory.CreateParser(filePath);
                if(parser == null) return this;
                
                var packages = parser
                                .Parse(stream)
                                .Select(x => 
                                    new Package { 
                                        Title = x.Split("@").First(), 
                                        Version = x.Split("@").Last() 
                                    }
                                );

                _packages.AddRange(packages);
            
                return this;
            }
        }

        public List<Package> Create()
        {
            return _packages;
        }
    }
}