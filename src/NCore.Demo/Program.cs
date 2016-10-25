using System;
using System.Collections.Generic;
using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Nancy;
using Nancy.Hosting.Self;
using NCore.Demo.Mappings;
using NCore.Demo.SearchIndexes;
using NCore.Demo.Utilities;
using NCore.Extensions;
using NCore.Web;
using NCore.Web.Aspects;
using NCore.Web.FreeTextSearch;
using Nest;
using Serilog;
using Serilog.Sinks.Elasticsearch;
namespace NCore.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //This is to allow easy debugging of NancyFx - should probably not be used in production
            StaticConfiguration.DisableErrorTraces = false;
            IEnumerable<string> errors;

            //TryInitialize the ElasticSearch helper - if this fails, ElasticSearch is probably not
            //running - and logging will default back to console logging
            Action<FluentDictionary<Type, string>> mapping = fd =>
            {
                fd.Add(typeof(CustomerSearchIndex), "ncore_customer");
            };
            if (ElasticHelper.TryInitialize(mapping,out errors))
            {
                //Initializing the Serilog logging framework used throughout NCore
                Log.Logger = new LoggerConfiguration()
                    .WriteTo
                    .Elasticsearch(new ElasticsearchSinkOptions(new Uri(ConfigurationManager.AppSettings["ElasticNode1"]))
                        {
                            AutoRegisterTemplate = true,
                        })
                    .CreateLogger();
                Log.Debug("ElaticSearch cluster is up and running - logging will be sent here.");
            }
            else
            {
                //Initializing the Serilog logging framework used throughout NCore
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.LiterateConsole()
                    .CreateLogger();
                Log.Error("Failed to initialize Elasticsearch");
                errors.ForEach(Log.Error);
            }

            //Creating the NHibernate SessionFactory and check that all the mappings are set correct
            SessionHelper.TryInitialize(ConfigurationManager.ConnectionStrings["NCoreConnection"], out errors, null, typeof(CustomerMapping));

            //Setting up aspect oriented things like validation before sending entities and transactions to the DB, 
            //Setting creation date on all entities and transactions etc.
            var container = new WindsorContainer();
            container.Register(Component.For<ICompanyContext>().ImplementedBy<CompanyContext>().LifestylePerWebRequest());
            TriggerConfig.InitializeDefault(() => container.Resolve<ICompanyContext>());

            //Starting a self-hosted Nancy webserver. It will create URL-reservations automatically (requires a UAC-prompt)
            var config = new HostConfiguration
            {
                UrlReservations = new UrlReservations { CreateAutomatically = true }
            };
            using (var host = new NancyHost(config, new Uri("http://localhost:1234")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:1234");
                Console.ReadLine();
            }
        }
    }
}