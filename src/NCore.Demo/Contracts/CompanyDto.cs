using NCore.Demo.Domain;
using NCore.Web.Api.Contracts;

namespace NCore.Demo.Contracts
{
    public class CompanyDto : EntityDto
    {
        public string CompanyName { get; set; }
        public AddressDto Address { get; set; }

        public static CompanyDto FromCustomer(Company company)
        {
            return new CompanyDto
            {
                Id = company.Id,
                CreationDate = company.CreationDate,
                Version = company.Version,
                CompanyName = company.CompanyName,
                Address = AddressDto.FromAddress(company.Address)
            };
        }
    }
}