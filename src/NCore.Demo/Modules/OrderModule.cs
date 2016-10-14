using System.Collections.Generic;
using NCore.Demo.Commands;
using NCore.Demo.Contracts;
using NCore.Demo.Creators;
using NCore.Demo.Domain;
using NCore.Demo.Queries;
using NCore.Demo.Updaters;
using NCore.Nancy.Api;
using NCore.Nancy.Creators;
using NCore.Nancy.Updaters;

namespace NCore.Demo.Modules
{
    public class OrderModule : CRUDModule<Order, OrderDto>
    {
        public OrderModule()
        {
            Get[_staticRoutes.Base] = p => GetList(p.customerId);
            Post[_staticRoutes.Put + "/Invoice"] = p => PostInvoice(p.Id);
        }

        private object GetList(long? customerId)
        {
            IEnumerable<string> errors;
            IList<OrderDto> result;
            return _sessionHelper.TryQuery(new FindOrderQuery(customerId), out result, out errors)
                ? new
                {
                    Success = true,
                    Result = result
                }
                : (object) new
                {
                    Success = false,
                    Errors = errors
                };
        }

        private object PostInvoice(long id)
        {
            IEnumerable<string> errors;
            var command = new InvoiceOrderCommand(id);
            return _sessionHelper.TryExecute(command, out errors)
                ? new
                {
                    Success = true,
                    AssignedId = command.InvoiceId
                }
                : (object) new
                {
                    Success = false,
                    Errors = errors
                };
        }

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