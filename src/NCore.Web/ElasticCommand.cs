using System.Collections.Generic;
using Nest;

namespace NCore.Web
{
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