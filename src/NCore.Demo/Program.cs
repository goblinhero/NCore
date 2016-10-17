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
            StaticConfiguration.DisableErrorTraces = false;
            IEnumerable<string> errors;
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200")) { AutoRegisterTemplate = true, })
                .CreateLogger();
            
            SessionHelper.TryInitialize(ConfigurationManager.ConnectionStrings["NCoreConnection"], out errors, null, typeof(CustomerMapping));
            TriggerConfig.InitializeDefault();
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