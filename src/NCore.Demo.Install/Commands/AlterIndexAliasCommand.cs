using System.Collections.Generic;
using NCore.Web;
using NCore.Web.FreeTextSearch;
using Nest;
using NHibernate.Util;

namespace NCore.Demo.Install.Commands
{
    public class AlterIndexAliasCommand : ElasticCommand
    {
        private readonly string _alias;
        private readonly string _newIndexName;

        public AlterIndexAliasCommand(string alias, string newIndexName)
        {
            _alias = alias;
            _newIndexName = newIndexName;
        }

        public override bool TryExecute(ElasticClient client, out IEnumerable<string> errors)
        {
            var indices = client.GetIndicesPointingToAlias(_alias);
            var response = client.Alias(a =>
            {
                indices.ForEach(i =>
                {
                    a.Remove(remove => remove.Alias(_alias).Index((string)i));
                });
                a.Add(add => add.Index(_newIndexName).Alias(_alias));
                return a;
            });
            if (response.ServerError != null)
            {
                return ErrorResult(out errors, $"Alter alias ({_alias} - {_newIndexName}) failed with the following errors:",
                    $"Servererror - Error: {response.ServerError.Error}",
                    $"Servererror - Status: {response.ServerError.Status}");
            }
            return SuccessResult(out errors);
        }
    }
}