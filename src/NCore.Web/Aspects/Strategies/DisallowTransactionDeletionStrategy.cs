using NCore.Web.Exceptions;
using NCore.Strategies;
using NHibernate.Event;

namespace NCore.Web.Aspects.Strategies
{
    public class DisallowTransactionDeletionStrategy : IStrategy<PreDeleteEvent>
    {
        public bool IsApplicable(PreDeleteEvent criteria)
        {
            return criteria.Entity is ITransaction;
        }

        public void Execute(PreDeleteEvent criteria)
        {
            throw new TransactionDeletionAttemptedException((ITransaction) criteria.Entity);
        }
    }
}