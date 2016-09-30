using System.Collections.Generic;
using NCore.Nancy.Exceptions;
using NCore.Strategies;
using NHibernate.Event;

namespace NCore.Nancy.Aspects.Strategies
{
    public class ValidStrategy : IStrategy<PreInsertEvent>, IStrategy<PreUpdateEvent>
    {
        public bool IsApplicable(PreInsertEvent criteria)
        {
            return criteria.Entity is IIsValidatable;
        }

        public bool IsApplicable(PreUpdateEvent criteria)
        {
            return criteria.Entity is IIsValidatable;
        }

        public void Execute(PreInsertEvent criteria)
        {
            CheckValidity((IIsValidatable)criteria.Entity);
        }

        public void Execute(PreUpdateEvent criteria)
        {
            CheckValidity((IIsValidatable)criteria.Entity);
        }

        private static void CheckValidity(IIsValidatable entity)
        {
            IEnumerable<string> errors;
            if (!entity.IsValid(out errors))
                throw new ValidationException(entity, errors);
        }
    }
}