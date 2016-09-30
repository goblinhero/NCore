using System;
using System.Collections.Generic;
using System.Linq;
using NCore.Rules;

namespace NCore
{
    public abstract class Transaction : ITransaction
    {
        public virtual DateTime? CreationDate { get; set; }
        public virtual long? Id { get; protected set; }
        public abstract bool IsValid(out IEnumerable<string> errors);
    }
    public abstract class Transaction<T> : Transaction
        where T : Transaction<T>, ITransaction
    {
        public override bool IsValid(out IEnumerable<string> errors)
        {
            return new RuleSet<T>(GetTransactionRules()).UpholdsRules(this as T, out errors);
        }


        private IRule<T>[] GetTransactionRules()
        {
            return new IRule<T>[]
            {
                new RelayRule<T>(ut => !ut.CreationDate.HasValue, "Creation date is not set")
            }.Concat(GetBusinessRules()).ToArray();
        }

        protected virtual IEnumerable<IRule<T>> GetBusinessRules()
        {
            return new IRule<T>[0];
        }
    }
}