using System;
using System.Linq.Expressions;
using NCore.Extensions;
using NHibernate;

namespace NCore.Nancy.Utilities
{
    public class EntitySetter<TEntity>
        where TEntity : IEntity
    {
        protected readonly TEntity _entity;
        protected readonly IPropertyHelper _propertyHelper;

  
        public EntitySetter(IPropertyHelper propertyHelper, TEntity entity)
        {
            _entity = entity;
            _propertyHelper = propertyHelper;
        }

        public void UpdateSimpleProperty<T>(Expression<Func<TEntity, T>> property, Action<T, T> postUpdate = null)
        {
            var propName = property.GetMemberInfo().Name;
            T newValue;
            if (!_propertyHelper.TryGetValue(propName, out newValue))
                return;
            UpdateEntity(property, newValue, postUpdate);
        }

        private void UpdateEntity<T>(Expression<Func<TEntity, T>> property, T newValue, Action<T, T> postUpdate)
        {
            var propName = property.GetMemberInfo().Name;
            var oldValue = property.Compile().Invoke(_entity);
            if (Equals(oldValue, newValue))
                return;
            var propertyInfo = typeof(TEntity).GetProperty(propName);
            propertyInfo.SetValue(_entity, newValue, null);
            postUpdate?.Invoke(oldValue, newValue);
        }

        public void UpdateComplexProperty<T>(Expression<Func<TEntity, T>> property, ISession session, Action<T, T> postUpdate = null)
            where T : class, IHasId
        {
            var propName = $"{property.GetMemberInfo().Name}Id";
            long? newId;
            if (!_propertyHelper.TryGetValue(propName, out newId))
                return;
            var newValue = newId.HasValue ? session.Get<T>(newId.Value) : null;
            UpdateEntity(property, newValue, postUpdate);
        }
    }
}