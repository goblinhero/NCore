using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Elasticsearch.Net;
using NCore.Extensions;
using Nest;
using Serilog;

namespace NCore.Web
{
    public class ElasticHelper
    {
        private static ElasticClient _client;

        public static bool TryInitialize(Action<FluentDictionary<Type, string>> mapping, out IEnumerable<string> errors)
        {
            try
            {
                var urls = new[]
                    {
                        ConfigurationManager.AppSettings["ElasticNode1"],
                        ConfigurationManager.AppSettings["ElasticNode2"],
                        ConfigurationManager.AppSettings["ElasticNode3"]
                    }.Where(u => !string.IsNullOrEmpty(u))
                    .Select(u => new Uri(u))
                    .ToArray();
                if (!urls.Any())
                    return urls.Error(out errors, "Failed to find any ElasticNodes in appSettings in app.config");
                IConnectionPool connectionPool = new SniffingConnectionPool(urls);
                var config = new ConnectionSettings(connectionPool).MapDefaultTypeIndices(mapping);
                var client = new ElasticClient(config);
                var ping = client.Ping();
                if (ping.IsValid)
                {
                    _client = client;
                    return _client.Success(out errors);
                }
                return ping.Error(out errors, $"Ping resulted in: {ping.IsValid} (HTTP)");
            }
            catch (Exception ex)
            {
                return ex.Error(out errors);
            }
        }

        public bool TryWrap(Predicate<ElasticClient> action, out IEnumerable<string> errors)
        {
            if (_client == null)
            {
                errors = new[]
                    {"Client was not initialized - maybe you forgot to call ElasticHelper.TryInitialize in the startup"};
                Log.Warning(string.Join(Environment.NewLine, errors));
                return false;
            }
            try
            {
                var success = action(_client);
                errors = new string[0];
                return success;
            }
            catch (Exception ex)
            {
                Log.Error("ElasticSearch helper encountered an exception.", ex);
                errors = new[] {$"Elasticsearch failed - {ex.Message}"};
                return false;
            }
        }

        public bool TryWrapCommand(IElasticCommand command, out IEnumerable<string> errors)
        {
            if (_client == null)
            {
                errors = new[]
                    {"Client was not initialized - maybe you forgot to call ElasticHelper.TryInitialize in the startup"};
                return false;
            }
            try
            {
                var success = command.TryExecute(_client, out errors);
                if (errors.Any())
                    Log.Warning(string.Join(Environment.NewLine, errors));
                return success;
            }
            catch (Exception ex)
            {
                Log.Error("ElasticSearch helper encountered an exception.", ex);
                errors = new[] {$"Elasticsearch failed - {ex.Message}"};
                return false;
            }
        }

        public bool TryWrapQuery(IElasticQuery query, out IEnumerable<string> errors)
        {
            if (_client == null)
            {
                errors = new[]
                    {"Client was not initialized - maybe you forgot to call ElasticHelper.TryInitialize in the startup"};
                return false;
            }
            try
            {
                var success = query.TryExecute(_client, out errors);
                if (errors.Any())
                    Log.Warning(string.Join(Environment.NewLine, errors));
                return success;
            }
            catch (Exception ex)
            {
                Log.Error("ElasticSearch helper encountered an exception.", ex);
                errors = new[] {$"Elasticsearch failed - {ex.Message}"};
                return false;
            }
        }
    }

    public interface IElasticQuery
    {
        bool TryExecute(ElasticClient client, out IEnumerable<string> errors);
    }

    public interface IElasticQuery<T> : IElasticQuery
        where T : class
    {
        ISearchResponse<T> Results { get; }
    }

    public interface IElasticCommand
    {
        bool TryExecute(ElasticClient client, out IEnumerable<string> errors);
    }

    public abstract class ElasticCommand : IElasticCommand
    {
        public abstract bool TryExecute(ElasticClient client, out IEnumerable<string> errors);

        protected bool SuccessResult(out IEnumerable<string> errors)
        {
            errors = new string[0];
            return true;
        }

        protected bool ErrorResult(out IEnumerable<string> errors, params string[] error)
        {
            errors = error;
            return false;
        }
    }
}