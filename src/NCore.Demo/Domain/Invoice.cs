using System.Collections.Generic;
using System.Linq;
using NCore.Rules;

namespace NCore.Demo.Domain
{
    public class Invoice : Transaction<Invoice>, IHasCompany
    {
        private readonly IList<InvoiceLine> _lines;

        protected Invoice()
        {
        }

        public Invoice(Customer customer, Address address, IEnumerable<OrderLine> lines)
        {
            Customer = customer;
            Address = address;
            Company = customer?.Company;
            var index = 0;
            _lines = lines
                .Where(ol => ol.Total != decimal.Zero || !string.IsNullOrWhiteSpace(ol.Description))
                .OrderBy(l => l.Index)
                .Select(ol => new InvoiceLine(ol.Description, ol.Total, index++))
                .ToList();
            Date = Date.Today;
        }

        public virtual Customer Customer { get; protected set; }
        public virtual Date Date { get; protected set; }
        public virtual Address Address { get; protected set; }
        public virtual decimal Total { get; protected set; }
        public virtual ICompany Company { get; protected set; }
        public virtual IEnumerable<InvoiceLine> Lines => _lines;

        protected override IRule<Invoice>[] GetBusinessRules()
        {
            return new IRule<Invoice>[]
            {
                new RelayRule<Invoice>(i => i.Customer == null,"Invoices must have a Customer."),
                new RelayRule<Invoice>(i => i.Company == null,"Invoices must have a Company."),
                new RelayRule<Invoice>(i => i.Address == null,"Invoices must have an address (can be blank)."),
                new RelayRule<Invoice>(i => i.Lines.Any(),"Invoices must have at least one line."),
            };
        }
    }
}