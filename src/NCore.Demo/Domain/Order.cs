using System.Collections.Generic;
using NCore.Extensions;
using NCore.Rules;

namespace NCore.Demo.Domain
{
    public class Order : Entity<Order>
    {
        public Order()
        {
            Address = new Address(string.Empty,string.Empty,string.Empty);
        }
        public virtual Customer Customer { get; set; }
        public virtual Address Address { get; set; }

        public virtual bool CanInvoice(IEnumerable<OrderLine> lines, out IEnumerable<string> errors)
        {
            return new RuleSet<Order>(GetInvoiceRules(lines)).UpholdsRules(this, out errors);
        }

        private IRule<Order>[] GetInvoiceRules(IEnumerable<OrderLine> lines)
        {
            return new IRule<Order>[0];
        }

        public virtual bool TryInvoice(IList<OrderLine> lines, out Invoice invoice, out IEnumerable<string> errors)
        {
            if (!CanInvoice(lines, out errors))
            {
                invoice = null;
                return false;
            }
            invoice = new Invoice(Customer, this, lines);
            return this.Success(out errors);
        }
    }
}