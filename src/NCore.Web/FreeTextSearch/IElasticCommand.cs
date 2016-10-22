using System.Collections.Generic;
using Nest;

namespace NCore.Web.FreeTextSearch
{
    public interface IElasticCommand
    {
        bool TryExecute(ElasticClient client, out IEnumerable<string> errors);
    }
}