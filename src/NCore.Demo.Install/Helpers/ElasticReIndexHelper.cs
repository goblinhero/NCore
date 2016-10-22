using System;
using NCore.Web;
using NCore.Web.FreeTextSearch;
using Nest;

namespace NCore.Demo.Install.Helpers
{
    public class ElasticReIndexHelper:ElasticHelper
    {
        public static void SetMap<TIndex>(string newIndexName, Action<PropertiesDescriptor<TIndex>> properties)
            where TIndex : class
        {
            _client.Map<TIndex>(d => d.Properties(p =>
            {
                properties(p);
                return p;
            }).Index(newIndexName));
        }


        public static void Dispose()
        {
            _client?.Flush(new FlushRequest {Force = true});
            _client = null;
        }
    }
}