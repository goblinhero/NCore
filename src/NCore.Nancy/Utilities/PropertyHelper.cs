using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NCore.Extensions;
using NCore.Strategies;

namespace NCore.Nancy.Utilities
{
    public class DictionaryHelper : IPropertyHelper
    {
        private readonly Dictionary<string, object> _properties;

        public DictionaryHelper(IDictionary<string, object> properties)
        {
            _properties = properties.ToDictionary(e => e.Key.ToLowerInvariant(), e => e.Value);
        }

        public bool TryGetValue<T>(string propertyName, out T value)
        {
            object rawValue;
            if (!TryGetRawValue(propertyName, out rawValue))
            {
                value = default(T);
                return false;
            }
            var strategies = new[]
            {
                new RelayStrategy<object, object>(v => default(T), v => v == null),
                new RelayStrategy<object, object>(v => v, v => typeof(T) == typeof(string)),
                new RelayStrategy<object, object>(v => Convert.ToDecimal(v), v => typeof(T) == typeof(decimal)),
                new RelayStrategy<object, object>(v => Convert.ToDecimal(v), v => typeof(T) == typeof(decimal?)),
                new RelayStrategy<object, object>(v => Convert.ToInt32(v), v => typeof(T) == typeof(int)),
                new RelayStrategy<object, object>(v => Convert.ToInt32(v), v => typeof(T) == typeof(int?)),
                new RelayStrategy<object, object>(v => Convert.ToInt64(v), v => typeof(T) == typeof(long)),
                new RelayStrategy<object, object>(v => Convert.ToInt64(v), v => typeof(T) == typeof(long?)),
                new RelayStrategy<object, object>(v => v)
            };
            try
            {
                value = (T) strategies.SafeExecute(rawValue);
                return true;
            }
            catch (Exception ex)
            {
                value = default(T);
                return false;
            }
        }

        private bool TryGetRawValue(string propertyName, out object value)
        {
            return _properties.TryGetValue(propertyName.ToLowerInvariant(), out value);
        }
    }

    public class PropertyHelper : IPropertyHelper
    {
        private readonly object _dto;
        private readonly IDictionary<string, PropertyInfo> _dtoProperties;

        public PropertyHelper(object dto)
        {
            _dto = dto;
            _dtoProperties = dto.GetType()
                .GetProperties()
                .ToDictionary(pi => pi.Name.ToLowerInvariant());
        }

        public bool TryGetValue<T>(string propertyName, out T value)
        {
            PropertyInfo property;
            if (!TryGetProperty<T>(propertyName, out property))
            {
                value = default(T);
                return false;
            }
            try
            {
                value = (T) property.GetValue(_dto);
                return true;
            }
            catch (Exception ex)
            {
                value = default(T);
                return false;
            }
        }

        private bool TryGetProperty<T>(string propertyName, out PropertyInfo property)
        {
            return _dtoProperties.TryGetValue(propertyName.ToLowerInvariant(), out property);
        }
    }

    public interface IPropertyHelper
    {
        bool TryGetValue<T>(string propertyName, out T value);
    }
}