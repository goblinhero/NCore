using System.Collections.Generic;
using System.Linq;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.Web.Commands;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class CompanyDeleter : BaseDeleter<Company>
    {
        public CompanyDeleter(long id)
            : base(id)
        {
        }

        public override bool TryDelete(Company entity, ISession session, out IEnumerable<string> errors)
        {
            var dependantCount = new[]
            {
                GetConstraintQuery<Order>(session, o => o.Company == entity),
                GetConstraintQuery<Customer>(session, o => o.Company == entity),
                GetConstraintQuery<Invoice>(session, o => o.Company == entity),
                GetConstraintQuery<OrderLine>(session, o => o.Company == entity),
            }.Sum(q => q.Value);

            return dependantCount > 0 
                ? this.Error(out errors, "Cannot delete company that has customers, orders, orderlines or invoices") 
                : base.TryDelete(entity, session, out errors);
        }
    }
}