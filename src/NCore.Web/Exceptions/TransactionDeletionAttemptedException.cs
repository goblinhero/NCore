using System;

namespace NCore.Web.Exceptions
{
    public class TransactionDeletionAttemptedException : Exception
    {
        public TransactionDeletionAttemptedException(ITransaction transaction)
        {
            Transaction = transaction;
        }

        public ITransaction Transaction { get; }
    }
}