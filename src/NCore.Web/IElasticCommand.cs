using System.Collections.Generic;
using Nest;

namespace NCore.Web
{
    public interface IElasticCommand
    {
        bool TryExecute(ElasticClient client, out IEnumerable<string> errors);
    }
}