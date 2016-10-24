using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using NCore.Demo.Install.FreeTextSearch;
using NCore.Demo.Install.Helpers;
using NCore.Demo.Mappings;
using NCore.Web;
using NCore.Web.FreeTextSearch;
using Nest;

namespace NCore.Demo.Install
{
    internal class Program
    {
        private static string _defaultIndexPrefix = "ncore";
        private static void Main(string[] args)
        {
            new Runner().Run();
            var process = Process.Start("RunMigrations.bat");
            process.WaitForExit();
            IEnumerable<string> errors;
            Action<FluentDictionary<Type, string>> mapping = fd =>
            {
                
            };
            if(!SessionHelper.TryInitialize(ConfigurationManager.ConnectionStrings["NCoreConnection"], out errors, null, typeof(CustomerMapping)) ||
               !ElasticHelper.TryInitialize(mapping,out errors))
            {
                return;
            }
            new RefreshCustomerIndexCommand(_defaultIndexPrefix).TryExecute(out errors);
            Process.Start("LastDBMigrate.log.txt");
        }
    }
}