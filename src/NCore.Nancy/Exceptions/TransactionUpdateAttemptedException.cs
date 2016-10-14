using System;

namespace NCore.Nancy.Exceptions
{
    public class TransactionUpdateAttemptedException : Exception
    {
        public TransactionUpdateAttemptedException(ITransaction transaction)
        {
            Transaction = transaction;
        }

        public ITransaction Transaction { get; }
    }
}