using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using PackScan.PackageParser;

namespace PackScan.Test.PackageParser
{
    public class CSProjParserTest
    {
        public class Parse
        {
            [Theory]
            [InlineData("<Project Sdk=\"Microsoft.NET.Sdk\"><ItemGroup><PackageReference Include=\"Mono.Options\" Version=\"5.3.0.1\" /></ItemGroup></Project>")]
            public void With_Valid_Stream(string data)
            {
                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    var strategy = new CSProjParser();
                    var result = strategy.Parse(stream);
                    Assert.True(result.Any());
                }
            }

            [Theory]
            [InlineData("<Project Sdk=\"Microsoft.NET.Sdk\"><ItemGroup><PackageReference Include=\"Mono.Options\"")]
            [InlineData("")]
            public void With_InValid_Stream(string data)
            {
                using(var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
                {
                    var strategy = new CSProjParser();
                    Assert.Throws<ParserException>(() => strategy.Parse(stream));
                }
            }
        }
    }
}