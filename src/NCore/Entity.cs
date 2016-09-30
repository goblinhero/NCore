using System;
using System.Collections.Generic;
using System.Linq;

namespace NCore
{
    public abstract class Entity<T> : IEntity
        where T : class, IEntity
    {
        public virtual bool IsValid(out IEnumerable<string> errors)
        {
            return new RuleSet<T>(GetEntityRules()).UpholdsRules(this as T, out errors);
        }

        public virtual DateTime? CreationDate { get; set; }
        public virtual long? Id { get; protected set; }
        public virtual int Version { get; protected set; }

        private IRule<T>[] GetEntityRules()
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