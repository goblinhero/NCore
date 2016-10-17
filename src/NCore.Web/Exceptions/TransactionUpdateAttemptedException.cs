using System;

namespace NCore.Web.Exceptions
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