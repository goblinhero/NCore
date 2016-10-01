using System.Collections.Generic;
using System.Linq;

namespace NCore.Demo.Domain
{
    public class Invoice : Transaction<Invoice>
    {
        private readonly IList<InvoiceLine> _lines;

        protected Invoice()
        {
        }

        public Invoice(Customer customer, Order order, IEnumerable<OrderLine> lines)
        {
            Customer = customer;
            Address = order.Address;
            var index = 0;
            _lines = lines
                .Where(ol => ol.Total != decimal.Zero || !string.IsNullOrWhiteSpace(ol.Description))
                .OrderBy(l => l.Index)
                .Select(ol => new InvoiceLine(ol.Description, ol.Total, index++))
                .ToList();
        }

        public virtual Customer Customer { get; protected set; }
        public virtual Date Date { get; protected set; }
        public virtual Address Address { get; protected set; }
        public virtual decimal Total { get; protected set; }
        public virtual IEnumerable<InvoiceLine> Lines => _lines;
    }
}