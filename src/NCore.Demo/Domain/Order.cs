using System.Collections.Generic;
using System.Linq;
using NCore.Extensions;
using NCore.Rules;

namespace NCore.Demo.Domain
{
    public class Order : Entity<Order>
    {
        public Order()
        {
            Address = Address.Blank;
        }

        public virtual Customer Customer { get; set; }
        public virtual Address Address { get; set; }

        public virtual bool CanInvoice(IEnumerable<OrderLine> lines, out IEnumerable<string> errors)
        {
            return new RuleSet<Order>(GetInvoiceRules(lines)).UpholdsRules(this, out errors);
        }

        private IRule<Order>[] GetInvoiceRules(IEnumerable<OrderLine> lines)
        {
            return new IRule<Order>[]
            {
                new RelayRule<Order>(o => !lines.Any(), "Cannot invoice an order without lines."),
                new RelayRule<Order>(o => o.Customer == null, "Cannot invoice an order without customer.")
            };
        }

        public virtual bool TryInvoice(IList<OrderLine> lines, out Invoice invoice, out IEnumerable<string> errors)
        {
            if (!CanInvoice(lines, out errors))
            {
                invoice = null;
                return false;
            }
            invoice = new Invoice(Customer, Address, lines);
            return invoice.IsValid(out errors);
        }

        protected override IRule<Order>[] GetBusinessRules()
        {
            return new IRule<Order>[]
            {
                new RelayRule<Order>(c => c.Address == null, "Orders must have an address (can be blank).")
            };
        }
    }
}