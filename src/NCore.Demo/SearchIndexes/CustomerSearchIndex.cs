using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Web.FreeTextSearch;
using Nest;

namespace NCore.Demo.SearchIndexes
{
    public class CustomerSearchIndex:SearchDto
    {
        public string CompanyName { get; set; }
        public string CompanyNameRaw { get; set; }
        public string Street { get; set; }
        public string StreetRaw { get; set; }
        public string City { get; set; }
        public string CityRaw { get; set; }
        public string Country { get; set; }
        public string CountryRaw { get; set; }
        public CustomerDto Dto { get; set; }
        public long CompanyId { get; set; }
        public static CustomerSearchIndex FromCustomer(CustomerDto customer)
        {
            return new CustomerSearchIndex
            {
                Id = customer.Id.Value,
                City = customer.Address.City,
                CityRaw = customer.Address.City,
                CompanyName = customer.CompanyName,
                CompanyNameRaw = customer.CompanyName,
                Country = customer.Address.Country,
                CountryRaw = customer.Address.Country,
                Street = customer.Address.Street,
                StreetRaw = customer.Address.Street,
                CompanyId = customer.CompanyId,
                Dto = customer,
            };
        }

        public static void AddProperties(PropertiesDescriptor<CustomerSearchIndex> descriptor)
        {
            descriptor.Number(n => n.Name(name => name.Id).Index());
            descriptor.String(n => n.Name(name => name.CompanyName).Index(FieldIndexOption.Analyzed));
            descriptor.String(n => n.Name(name => name.CompanyNameRaw).Index(FieldIndexOption.NotAnalyzed));
            descriptor.String(n => n.Name(name => name.Street).Index(FieldIndexOption.Analyzed));
            descriptor.String(n => n.Name(name => name.StreetRaw).Index(FieldIndexOption.NotAnalyzed));
            descriptor.String(n => n.Name(name => name.City).Index(FieldIndexOption.Analyzed));
            descriptor.String(n => n.Name(name => name.CityRaw).Index(FieldIndexOption.NotAnalyzed));
            descriptor.String(n => n.Name(name => name.Country).Index(FieldIndexOption.Analyzed));
            descriptor.String(n => n.Name(name => name.CountryRaw).Index(FieldIndexOption.NotAnalyzed));
            descriptor.Number(n => n.Name(name => name.CompanyId).Index());
        }
    }

}