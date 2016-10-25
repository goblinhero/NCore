using NCore.DBMigrate.ElasticSearch;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.SearchIndexes;
using Nest;

namespace NCore.Demo.Install.FreeTextSearch
{
    public class RefreshCustomerIndexCommand: RefreshEntityFreeTextIndexCommand<Customer,CustomerDto,CustomerSearchIndex>
    {
        public RefreshCustomerIndexCommand(string defaultIndexPrefix)
            : base(defaultIndexPrefix)
        {
        }


        protected override IPromise<IProperties> AddProperties(PropertiesDescriptor<CustomerSearchIndex> descriptor)
        {
            CustomerSearchIndex.AddProperties(descriptor);
            return descriptor;
        }

        protected override CustomerSearchIndex[] Convert(CustomerDto dto)
        {
            //Insert logic here to exclude rows based on some criteria - or create more than one per row
            return new[]
            {
                CustomerSearchIndex.FromCustomer(dto)
            };
        }
    }
}