using System;
using System.Collections.Generic;
using System.Configuration;
using Nancy;
using Nancy.Hosting.Self;
using NCore.Demo.Mappings;
using NCore.Web;
using NCore.Web.Aspects;
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

            //Initializing the Serilog logging framework used throughout NCore
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")) { AutoRegisterTemplate = true, })
                .CreateLogger();
            
            //Creating the NHibernate SessionFactory and check that all the mappings are set correct
            SessionHelper.TryInitialize(ConfigurationManager.ConnectionStrings["NCoreConnection"], out errors, null, typeof(CustomerMapping));

            //Setting up aspect oriented things like validation before sending entities and transactions to the DB, 
            //Setting creation date on all entities and transactions etc.
            TriggerConfig.InitializeDefault();

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