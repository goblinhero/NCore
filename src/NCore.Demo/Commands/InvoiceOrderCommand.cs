using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.Nancy.Commands;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class InvoiceOrderCommand : ICommand
    {
        private readonly long _orderId;

        public InvoiceOrderCommand(long orderId)
        {
            _orderId = orderId;
        }

        public long? InvoiceId { get; set; }

        public bool TryExecute(ISession session, out IEnumerable<string> errors)
        {
            var order = session.Get<Order>(_orderId);
            if (order == null)
            {
                return order.NotFound(_orderId, out errors);
            }
            var lines = session.QueryOver<OrderLine>()
                .Where(ol => ol.Order == order)
                .List();
            Invoice invoice;
            if (!order.TryInvoice(lines, out invoice, out errors) ||
                !invoice.IsValid(out errors))
                return false;
            session.Save(invoice);
            InvoiceId = invoice.Id;
            return this.Success(out errors);
        }
    }
}