using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.Web.Commands;
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
            return session.QueryOver<Order>().Where(o => o.Customer == entity).RowCount() > 0 
                ? this.Error(out errors, "Cannot delete customer with orders") 
                : base.TryDelete(entity, session, out errors);
        }
    }
}