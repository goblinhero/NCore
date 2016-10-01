using System;

namespace NCore.Nancy.Exceptions
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