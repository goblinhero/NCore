using NCore.Demo.Contracts;
using NCore.Demo.Creators;
using NCore.Demo.Domain;
using NCore.Nancy;
using NCore.Nancy.Creators;
using NCore.Nancy.Updaters;

namespace NCore.Demo.Modules
{
    public class CustomerModule : CRUDModule<Customer, CustomerDto>
    {
        protected override ICreator<Customer> GetCreator(CustomerDto dto)
        {
            return new CustomerCreator(dto);
        }

        protected override IUpdater<Customer> GetUpdater(long id, CustomerDto dto)
        {
            return new CustomerUpdater(id, dto);
        }
    }
}