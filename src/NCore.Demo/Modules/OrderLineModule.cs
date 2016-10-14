using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Creators;
using NCore.Demo.Domain;
using NCore.Demo.Updaters;
using NCore.Nancy.Api;
using NCore.Nancy.Creators;
using NCore.Nancy.Updaters;

namespace NCore.Demo.Modules
{
    public class OrderLineModule : CRUDModule<OrderLine, OrderLineDto>
    {
        protected override ICreator<OrderLine> GetCreator(IDictionary<string, object> dto)
        {
            return new OrderLineCreator(dto);
        }

        protected override IUpdater<OrderLine> GetUpdater(long id, IDictionary<string, object> dto)
        {
            return new OrderLineUpdater(id, dto);
        }
    }
}