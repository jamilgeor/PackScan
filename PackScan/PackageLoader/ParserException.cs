using System;

namespace PackScan.PackageLoader
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