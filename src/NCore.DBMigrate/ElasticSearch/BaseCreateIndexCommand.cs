using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NCore.Web.FreeTextSearch;
using Nest;

namespace NCore.DBMigrate.ElasticSearch
{
    public class BaseCreateIndexCommand<TIndex> : ElasticCommand
        where TIndex : class
    {
        private readonly string _indexAlias;
        private readonly Func<TypeMappingDescriptor<TIndex>, ITypeMapping> _addProperties;
        public BaseCreateIndexCommand(string indexAlias, Func<TypeMappingDescriptor<TIndex>, ITypeMapping> addProperties)
        {
            _addProperties = addProperties;
            _indexAlias = indexAlias.ToLower();
        }

        public override bool TryExecute(ElasticClient client, out IEnumerable<string> errors)
        {
            var indices = client.GetIndicesPointingToAlias(_indexAlias);
            NewIndexName = indices.Any() ? CreateNewIndexFromOld(indices[0]) : $"{_indexAlias}_001";

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

            var response = client.CreateIndex(NewIndexName, cid => cid.Mappings(m => m.Map<TIndex>(_addProperties)));
            if (response.ServerError != null)
            {
                return ErrorResult(out errors, "Create index failed with the following errors:",
                    $"Servererror - Error: {response.ServerError.Error}",
                    $"Servererror - Status: {response.ServerError.Status}");
            }
            return SuccessResult(out errors);

        }

        public string NewIndexName { get; private set; }

        private string CreateNewIndexFromOld(string oldIndex)
        {
            var digits = Regex.Replace(oldIndex, @"\D", "");
            var newDigit = int.Parse(digits) + 1;
            return $"{_indexAlias}_{newDigit:D3}";
        }
    }
}