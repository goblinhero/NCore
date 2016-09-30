using System.Collections.Generic;
using System.Linq;

namespace NCore.Demo.Domain
{
    public class Invoice : Transaction<Invoice>
    {
        protected Invoice()
        {
        }

        public Invoice(Customer customer, Order order, IEnumerable<OrderLine> lines)
        {
            Customer = customer;
            Order = order;
            Address = order.Address;
            Lines = lines.OrderBy(l => l.Index).Select(ol => new InvoiceLine());
        }

        public virtual Customer Customer { get; protected set; }
        public virtual Order Order { get; protected set; }
        public virtual Date Date { get; protected set; }
        public virtual Address Address { get; protected set; }
        public virtual decimal Total { get; protected set; }

        public virtual IEnumerable<InvoiceLine> Lines { get; protected set; }
    }
}