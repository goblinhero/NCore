using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Extensions;
using NCore.Nancy.Queries;
using NHibernate;

namespace NCore.Demo.Queries
{
    public class FindInvoiceQuery : IQuery<IList<InvoiceDto>>
    {
        private long? _customerId;

        public FindInvoiceQuery(long? customerId)
        {
            _customerId = customerId;
        }

        public bool TryExecute(ISession session, out IList<InvoiceDto> result, out IEnumerable<string> errors)
        {
            var query = session.QueryOver<InvoiceDto>();
            if (_customerId.HasValue)
                query = query.Where(o => o.CustomerId == _customerId);
            result = query.List();
            return this.Success(out errors);
        }
    }
}