using System;
using NCore.Strategies;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace NCore.Nancy.Aspects.Strategies
{
    public class SetEntityCreationDateStrategy : IStrategy<PreInsertEvent>
    {
        public bool IsApplicable(PreInsertEvent criteria)
        {
            return criteria.Entity is Entity;
        }

        public void Execute(PreInsertEvent criteria)
        {
            var createdAt = DateTime.UtcNow;
            var entity = (Entity)criteria.Entity;
            entity.CreationDate = createdAt;
            Set(criteria.Persister, criteria.State, "CreationDate", createdAt);
        }
        private void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
                return;
            state[index] = value;
        }
    }
}