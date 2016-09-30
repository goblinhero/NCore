using System;

namespace NCore.Nancy.Exceptions
{
    public class TransactionUpdateAttemptedException : Exception
    {
        public ITransaction Transaction { get;  }
        public TransactionUpdateAttemptedException(ITransaction transaction)
        {
            Transaction = transaction;
        }
    }
}