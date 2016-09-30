using System;

namespace NCore.Nancy.Exceptions
{
    public class TransactionDeletionAttemptedException : Exception
    {
        public ITransaction Transaction { get;  }
        public TransactionDeletionAttemptedException(ITransaction transaction)
        {
            Transaction = transaction;
        }
    }
}