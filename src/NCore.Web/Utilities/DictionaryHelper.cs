using System;
using System.Collections.Generic;
using System.Linq;
using NCore.Extensions;
using NCore.Strategies;

namespace NCore.Web.Utilities
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
}