using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.Web.Utilities;

namespace NCore.Demo.Utilities
{
    public class DemoEntitySetter<TEntity> : EntitySetter<TEntity>
        where TEntity : IEntity
    {
        public DemoEntitySetter(IPropertyHelper propertyHelper, TEntity entity) : base(propertyHelper, entity)
        {
        }

        public void PatchAddress(Expression<Func<TEntity, Address>> property)
        {
            IDictionary<string, object> dto;
            var addressProperty = property.GetMemberInfo().Name;
            if (!_propertyHelper.TryGetValue(addressProperty, out dto))
            {
                return;
            }
            var oldAddress = property.Compile().Invoke(_entity);
            var newAddress = dto != null ? ExtractPatchedAddress(dto, oldAddress) : Address.Blank;
            if (Equals(oldAddress, newAddress))
                return;
            var propertyInfo = typeof(TEntity).GetProperty(addressProperty);
            propertyInfo.SetValue(_entity, newAddress, null);
        }

        private Address ExtractPatchedAddress(IDictionary<string, object> dto, Address oldAddress)
        {
            var propertyHelper = new DictionaryHelper(dto);
            var isBlankAddress = true;
            var properties = typeof(Address).GetProperties()
                .ToDictionary(pi => pi.Name, pi =>
                {
                    string value;
                    if (!propertyHelper.TryGetValue(pi.Name, out value))
                        return oldAddress == null ? null : pi.GetValue(oldAddress) as string;
                    isBlankAddress = false;
                    return value;
                });
            if (isBlankAddress)
            {
                return Address.Blank;
            }
            var street = properties["Street"];
            var city = properties["City"];
            var country = properties["Country"];
            return new Address(street, city, country);
        }
    }
}
