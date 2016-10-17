using NCore.Demo.SearchIndexes;
using Nest;

namespace NCore.Demo.Install.Commands
{
    public class CreateNewCustomerIndexCommand : BaseCreateIndexCommand<CustomerSearchIndex>
    {
        public CreateNewCustomerIndexCommand(string indexAlias)
            : base(indexAlias)
        {
        }

        public override void AddProperties(PropertiesDescriptor<CustomerSearchIndex> p)
        {
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
    }
}