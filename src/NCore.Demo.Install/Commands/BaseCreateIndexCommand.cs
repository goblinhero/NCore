using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NCore.Web;
using NCore.Web.FreeTextSearch;
using Nest;

namespace NCore.Demo.Install.Commands
{
    public abstract class BaseCreateIndexCommand<TIndex> : ElasticCommand
        where TIndex : class
    {
        private readonly string _indexAlias;

        protected BaseCreateIndexCommand(string indexAlias)
        {
            _indexAlias = indexAlias.ToLower();
        }

        public override bool TryExecute(ElasticClient client, out IEnumerable<string> errors)
        {
            var indices = client.GetIndicesPointingToAlias(_indexAlias);
            NewIndexName = indices.Any() ? CreateNewIndexFromOld(indices[0]) : string.Format("{0}_001", _indexAlias);

            //If the previous run failed, the new index might already exists
            var existsResponse = client.IndexExists(NewIndexName);
            if (existsResponse.ServerError != null)
            {
                return ErrorResult(out errors, "Create index failed with the following errors:",
                    $"Servererror - Error: {existsResponse.ServerError.Error}",
                    $"Servererror - Status: {existsResponse.ServerError.Status}");
            }
            if (existsResponse.Exists)
            {
                client.DeleteIndex(NewIndexName);
            }

            var response = client.CreateIndex(NewIndexName,cid => cid.Mappings(md => md.Map<TIndex>(m => m.Properties(p =>
            {
                AddProperties(p);
                return p;
            }))));
            if (response.ServerError != null)
            {
                return ErrorResult(out errors, "Create index failed with the following errors:",
                    $"Servererror - Error: {response.ServerError.Error}",
                    $"Servererror - Status: {response.ServerError.Status}");
            }
            return SuccessResult(out errors);

        }

        public abstract void AddProperties(PropertiesDescriptor<TIndex> p);
        public string NewIndexName { get; private set; }

        private string CreateNewIndexFromOld(string oldIndex)
        {
            var digits = Regex.Replace(oldIndex, @"\D", "");
            var newDigit = int.Parse(digits) + 1;
            return $"{_indexAlias}_{newDigit:D3}";
        }
    }
}