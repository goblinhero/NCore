using System;
using System.Linq.Expressions;
using NCore.Extensions;

namespace NCore.Nancy.Updaters
{
    public class EntitySetter<TEntity>
        where TEntity : IEntity
    {
        protected TEntity _entity;

        protected void UpdateValueTypeProperty<T>(Expression<Func<TEntity, T>> property, object dto, Action<T, T> postUpdate = null)
        {
            var propName = property.GetMemberInfo().Name;
            T newValue;
            if (!TryGetValueType(propName, dto, out newValue))
                return;
            var oldValue = property.Compile().Invoke(_entity);
            if (Equals(oldValue, newValue))
                return;
            var propertyInfo = typeof(TEntity).GetProperty(propName);
            propertyInfo.SetValue(_entity, newValue, null);
            postUpdate?.Invoke(oldValue, newValue);
        }
        protected void UpdateSimpleProperty<T>(Expression<Func<TEntity, T>> property, object dto, Action<T, T> postUpdate = null)
            where T:class
        {
            var propName = property.GetMemberInfo().Name;
            T newValue;
            if (!TryGetValue(propName, dto, out newValue))
                return;
            var oldValue = property.Compile().Invoke(_entity);
            if (Equals(oldValue, newValue))
                return;
            var propertyInfo = typeof(TEntity).GetProperty(propName);
            propertyInfo.SetValue(_entity, newValue, null);
            postUpdate?.Invoke(oldValue, newValue);
        }

        protected bool TryGetValueType<T>(string property, object dto, out T value)
        {
            var dtoProp = dto.GetType().GetProperty(property);
            if (dtoProp == null || dtoProp.PropertyType != typeof(T))
            {
                value = default(T);
                return false;
            }
            value = (T)dtoProp.GetValue(dto);
            return true;
        }
        protected bool TryGetValue<T>(string property, object dto, out T value)
            where T:class
        {
            var dtoProp = dto.GetType().GetProperty(property);
            if (dtoProp == null || dtoProp.PropertyType != typeof(T))
            {
                value = null;
                return false;
            }
            value = dtoProp.GetValue(dto) as T;
            return true;
        }
    }
}