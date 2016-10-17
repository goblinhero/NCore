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
        private int? _transientHash;
        public override bool IsValid(out IEnumerable<string> errors)
        {
            return new RuleSet<T>(GetBusinessRules()).UpholdsRules(this as T, out errors);
        }

        protected virtual IRule<T>[] GetBusinessRules()
        {
            return new IRule<T>[0];
        }
        protected bool Equals(ITransaction other)
        {
            return Id.HasValue && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ITransaction)obj);
        }

        public override int GetHashCode()
        {
            if (_transientHash.HasValue)
                return _transientHash.Value;
            if (Id.HasValue)
                return Id.GetHashCode();
            _transientHash = Guid.NewGuid().GetHashCode();
            return _transientHash.Value;
        }
    }
}