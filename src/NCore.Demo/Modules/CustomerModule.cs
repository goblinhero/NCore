using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Nancy;

namespace NCore.Demo.Modules
{
    public class CustomerModule : CRUDModule<Customer, CustomerDto>
    {
    }
}