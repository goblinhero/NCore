using System.Collections.Generic;
using NCore.Demo.Commands;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Web.Api;
using NCore.Web.Commands;

namespace NCore.Demo.Modules
{
    public class OrderLineModule : CRUDModule<OrderLine, OrderLineDto>
    {
        protected override ICreator GetCreator(IDictionary<string, object> dto)
        {
            return new OrderLineCreator(dto);
        }

        protected override ICommand GetUpdater(long id, IDictionary<string, object> dto)
        {
            return new OrderLineUpdater(id, dto);
        }
    }
}