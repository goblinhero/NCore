using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using NCore.Demo.Install.Commands;
using NCore.Demo.Install.Helpers;
using NCore.Demo.Mappings;
using NCore.Demo.SearchIndexes;
using NCore.Web;
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
            ReIndexCustomers();
            Process.Start("LastDBMigrate.log.txt");
        }
        private static void ReIndexCustomers()
        {
            Console.WriteLine("Now starting indexing customers");
            var elasticHelper = new ElasticReIndexHelper();
            IEnumerable<string> errors;
            var alias = $"{_defaultIndexPrefix}_customer";
            var createIndexCommand = new CreateNewCustomerIndexCommand(alias);
            if (!elasticHelper.TryWrapCommand(createIndexCommand, out errors))
                return;
            ElasticReIndexHelper.SetMap<CustomerSearchIndex>(createIndexCommand.NewIndexName, pd => createIndexCommand.AddProperties(pd));
            var helper = new SessionHelper();
            helper.TryExecute(new IndexCustomersCommand(createIndexCommand.NewIndexName), out errors);
            elasticHelper.TryWrapCommand(new AlterIndexAliasCommand(alias, createIndexCommand.NewIndexName), out errors);
        }
    }
}