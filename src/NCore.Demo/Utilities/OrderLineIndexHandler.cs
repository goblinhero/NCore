using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.Nancy.Utilities;
using NCore.Strategies;

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
            IDictionary<string,object> dto;
            var addressProperty = property.GetMemberInfo().Name;
            if (!_propertyHelper.TryGetValue(addressProperty, out dto))
            {
                return;
            }
            var oldAddress = property.Compile().Invoke(_entity);
            var newAddress = dto != null ? ExtractPatchedAddress(dto, oldAddress) : null;
            if (Equals(oldAddress, newAddress))
                return;
            var propertyInfo = typeof(TEntity).GetProperty(addressProperty);
            propertyInfo.SetValue(_entity, newAddress, null);
        }

        private Address ExtractPatchedAddress(IDictionary<string, object> dto, Address oldAddress)
        {
            var propertyHelper = new DictionaryHelper(dto);
            bool isBlankAddress = true;
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
                return new Address(string.Empty, string.Empty, string.Empty);
            }
            var street = properties["Street"];
            var city = properties["City"];
            var country = properties["Country"];
            return new Address(street, city, country);
        }
    }
    public class OrderLineIndexHandler
    {
        public void AdjustIndexes(OrderLine changingLine, int? newIndex, IList<OrderLine> otherLines)
        {
            new IStrategy<IList<OrderLine>, int>[]
            {
                new RelayStrategy<IList<OrderLine>, int>(ol =>
                {
                    var i = Math.Min(newIndex.Value, ol.Max(l => l.Index) + 1);
                    ol.Where(l => l.Index >= i).ForEach(l => l.Index++);
                    return i;
                }, ol => ol.Any() && newIndex.HasValue),
                new RelayStrategy<IList<OrderLine>, int>(ol => ol.Max(l => l.Index) + 1, ol => ol.Any()),
                new RelayStrategy<IList<OrderLine>, int>(ol => 0)
            }.ExecuteFirstOrDefault(otherLines);
        }
    }
}