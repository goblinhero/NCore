using NCore.Demo.Contracts;
using NCore.Demo.Creators;
using NCore.Demo.Domain;
using NCore.Demo.Updaters;
using NCore.Nancy;
using NCore.Nancy.Creators;
using NCore.Nancy.Updaters;

namespace NCore.Demo.Modules
{
    public class OrderModule : CRUDModule<Order, OrderDto>
    {
        protected override ICreator<Order> GetCreator(object dto)
        {
            return new OrderCreator(dto);
        }

        protected override IUpdater<Order> GetUpdater(long id, object dto)
        {
            return new OrderUpdater(id, dto);
        }
    }
}