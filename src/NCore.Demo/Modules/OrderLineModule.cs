using NCore.Demo.Contracts;
using NCore.Demo.Creators;
using NCore.Demo.Domain;
using NCore.Demo.Updaters;
using NCore.Nancy;
using NCore.Nancy.Creators;
using NCore.Nancy.Updaters;

namespace NCore.Demo.Modules
{
    public class OrderLineModule : CRUDModule<OrderLine, object>
    {
        protected override ICreator<OrderLine> GetCreator(object dto)
        {
            return new OrderLineCreator(dto);
        }

        protected override IUpdater<OrderLine> GetUpdater(long id, object dto)
        {
            return new OrderLineUpdater(id, dto);
        }
    }
}