using System;
using System.Collections.Generic;
using System.Linq;
using NCore.Demo.Contracts;
using NCore.Demo.SearchIndexes;
using NCore.Extensions;
using NCore.Rules;
using NCore.Web;
using NCore.Web.Extensions;
using NCore.Web.FreeTextSearch;
using Nest;

namespace NCore.Demo.Queries
{
    public class CompositeCustomerFreeTextQuery
    {
        private readonly ICompanyContext _companyContext;
        private readonly IEnumerable<string> _query;
        private readonly ElasticHelper _elasticHelper = new ElasticHelper();

        public CompositeCustomerFreeTextQuery(ICompanyContext companyContext, string query)
        {
            _companyContext = companyContext;
            _query = query.SplitForFreeText();
        }

        public bool TryExecute(out IList<CustomerDto> result, out IEnumerable<string> errors)
        {
            if(!new RuleSet<IEnumerable<string>>(GetSearchRules()).UpholdsRules(_query, out errors))
            { 
                result = null;
                return false;
            }
            var query = new CustomerPrefixDescriptionQuery(_companyContext,_query);
            if (!_elasticHelper.TryWrapQuery(query, out errors))
            {
                result = null;
                return false;
            }
            result = new List<CustomerDto>(query.Results.Distinct().Select(r => r.Dto));
            return this.Success(out errors);
        }

        private abstract class CustomerQuery : IElasticQuery<IEnumerable<CustomerSearchIndex>>
        {
            public abstract bool TryExecute(ElasticClient client, out IEnumerable<string> errors);
            public IEnumerable<CustomerSearchIndex> Results { get; protected set; }
        }
        private class CustomerPrefixDescriptionQuery : CustomerQuery
        {
            private readonly ICompanyContext _companyContext;
            private readonly IEnumerable<string> _query;

            public CustomerPrefixDescriptionQuery(ICompanyContext companyContext, IEnumerable<string> query)
            {
                _companyContext = companyContext;
                _query = query;
            }

            public override bool TryExecute(ElasticClient client, out IEnumerable<string> errors)
            {
                Results = client.Search<CustomerSearchIndex>(sd => sd.Query(q =>
                    q.Bool(bq =>
                    {
                        bq.Must(_query.Select<string, Func<QueryContainerDescriptor<CustomerSearchIndex>, QueryContainer>>(s => qcd => qcd.Prefix(csi => csi.CompanyName, s)));
                        bq.Filter(f => f.Term(csi => csi.CompanyId, _companyContext.CurrentCompany));
                        return bq;
                    }))).Hits.Select(h => h.Source).ToList();
                return this.Success(out errors);
            }
        }
        private IRule<IEnumerable<string>>[] GetSearchRules()
        {
            return new IRule<IEnumerable<string>>[]
            {
                new RelayRule<IEnumerable<string>>(s => !_elasticHelper.IsInitialized, "Elastic search not initialized."),
                new RelayRule<IEnumerable<string>>(s => !s.Any(), "Nothing to search for.")
            };
        }
    }
}