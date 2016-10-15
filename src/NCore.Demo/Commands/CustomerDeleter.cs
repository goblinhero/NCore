using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.Nancy.Commands;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class CustomerDeleter : BaseDeleter<Customer>
    {
        public CustomerDeleter(long id)
            : base(id)
        {
        }

        public override bool TryDelete(Customer entity, ISession session, out IEnumerable<string> errors)
        {
            if (session.QueryOver<Order>().Where(o => o.Customer == entity).RowCount() > 0)
                return this.Error(out errors, "Cannot delete customer with orders");
            return base.TryDelete(entity, session, out errors);
        }
    }
}