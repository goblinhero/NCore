﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NCore
{
    public abstract class Transaction<T> : ITransaction
        where T : class, ITransaction
    {
        public virtual bool IsValid(out IEnumerable<string> errors)
        {
            return new RuleSet<T>(GetTransactionRules()).UpholdsRules(this as T, out errors);
        }

        public virtual DateTime? CreationDate { get; set; }
        public virtual long? Id { get; protected set; }

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