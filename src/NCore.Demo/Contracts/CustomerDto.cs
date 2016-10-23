using NCore.Demo.Domain;
using NCore.Web.Api.Contracts;

namespace NCore.Demo.Contracts
{
    public class CustomerDto : EntityDto, IHasCompanyDto
    {
        public string CompanyName { get; set; }
        public AddressDto Address { get; set; }
        public long CompanyId { get; set; }

        public static CustomerDto FromCustomer(Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                CreationDate = customer.CreationDate,
                Version = customer.Version,
                CompanyName = customer.CompanyName,
                Address = AddressDto.FromAddress(customer.Address),
                CompanyId = customer.Company.Id.Value
            };
        }
    }
}