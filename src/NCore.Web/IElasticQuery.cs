using System.Collections.Generic;
using Nest;

namespace NCore.Web
{
    public interface IElasticQuery
    {
        bool TryExecute(ElasticClient client, out IEnumerable<string> errors);
    }
}