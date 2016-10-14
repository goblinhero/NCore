using System;
using NCore.Strategies;
using NHibernate.Event;

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
            var entity = (Entity) criteria.Entity;
            entity.CreationDate = DateTime.UtcNow;
        }
    }
}