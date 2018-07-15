using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using PackScan.PackageLoader;

namespace PackScan.Test.PackageLoader
{
    public class NuGetLoaderStrategyTest
    {
        public class Parse
        {
            [Theory]
            [InlineData("<?xml version=\"1.0\" encoding=\"utf-8\"?><packages><package id=\"Autofac\" version=\"3.5.2\" targetFramework=\"net45\" /></packages>")]
            public void With_Valid_Stream(string data)
            {
                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    var strategy = new NuGetLoaderStrategy();
                    var result = strategy.Parse(stream);
                    Assert.True(result.Any());
                }
            }

            [Theory]
            [InlineData("<?xml version=\"1.0\" encoding=\"utf-8\"?><packages><package id=")]
            public void With_InValid_Stream(string data)
            {
                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    var strategy = new NuGetLoaderStrategy();
                    Assert.Throws<ParserException>(() => strategy.Parse(stream));
                }
            }
        }
    }
}