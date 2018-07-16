using System;

namespace PackScan.PackageParser
{
    public class ParserException : Exception
    {
        public override string Message 
        {
            get {
                return "There was a problem parsing package file.";
            }
        }
    }
}