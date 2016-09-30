using NCore.Demo.Contracts;
using NCore.Demo.Domain;

namespace NCore.Demo.Extensions
{
    public static class ContractExtensions
    {
        public static Address ConvertToValueType(this AddressDto dto)
        {
            return new Address(dto?.Street, dto?.City, dto?.Country);
        }
    }
}