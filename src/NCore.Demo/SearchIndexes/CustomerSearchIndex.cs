using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Web.FreeTextSearch;

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
                Dto = customer
            };
        }
    }

}