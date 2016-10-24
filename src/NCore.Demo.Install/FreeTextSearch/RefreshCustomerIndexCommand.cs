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

        protected override void AddProperties(PropertiesDescriptor<CustomerSearchIndex> p)
        {
            //Used to control how the data is stored for full text searching - if left blanc, Elastic Search 
            //will typically analyze string types and not numbers
            p.Number(n => n.Name(name => name.Id).Index());
            p.String(n => n.Name(name => name.CompanyName).Index(FieldIndexOption.Analyzed));
            p.String(n => n.Name(name => name.CompanyNameRaw).Index(FieldIndexOption.NotAnalyzed));
            p.String(n => n.Name(name => name.Street).Index(FieldIndexOption.Analyzed));
            p.String(n => n.Name(name => name.StreetRaw).Index(FieldIndexOption.NotAnalyzed));
            p.String(n => n.Name(name => name.City).Index(FieldIndexOption.Analyzed));
            p.String(n => n.Name(name => name.CityRaw).Index(FieldIndexOption.NotAnalyzed));
            p.String(n => n.Name(name => name.Country).Index(FieldIndexOption.Analyzed));
            p.String(n => n.Name(name => name.CountryRaw).Index(FieldIndexOption.NotAnalyzed));
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