using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Extensions;
using NCore.Nancy.Queries;
using NHibernate;

namespace NCore.Demo.Queries
{
    public class FindOrderQuery : IQuery<IList<OrderDto>>
    {
        private long? _customerId;

        public FindOrderQuery(long? customerId)
        {
            _customerId = customerId;
        }

        public bool TryExecute(ISession session, out IList<OrderDto> result, out IEnumerable<string> errors)
        {
            var query = session.QueryOver<OrderDto>();
            if (_customerId.HasValue)
                query = query.Where(o => o.CustomerId == _customerId);
            result = query.List();
            return this.Success(out errors);
        }
    }
}