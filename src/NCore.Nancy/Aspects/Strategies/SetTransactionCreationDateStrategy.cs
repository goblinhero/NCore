using System;
using NCore.Strategies;
using NHibernate.Event;

namespace NCore.Nancy.Aspects.Strategies
{
    public class SetTransactionCreationDateStrategy : IStrategy<PreInsertEvent>
    {
        public bool IsApplicable(PreInsertEvent criteria)
        {
            return criteria.Entity is ITransaction;
        }

        public void Execute(PreInsertEvent criteria)
        {
            var transaction = (Transaction) criteria.Entity;
            transaction.CreationDate = DateTime.UtcNow;
        }
    }
}