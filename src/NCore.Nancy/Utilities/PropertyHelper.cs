using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NCore.Nancy.Utilities
{
    public class DictionaryHelper : IPropertyHelper
    {
        private Dictionary<string, object> _properties;

        public DictionaryHelper(IDictionary<string,object> properties)
        {
            _properties = properties.ToDictionary(e => e.Key.ToLowerInvariant(), e => e.Value);
        }
        private bool TryGetRawValue<T>(string propertyName, out object value)
        {
            return _properties.TryGetValue(propertyName.ToLowerInvariant(), out value);
        }

        public bool TryGetValue<T>(string propertyName, out T value)
        {
            object rawValue;
            if (!TryGetRawValue<T>(propertyName, out rawValue))
            {
                value = default(T);
                return false;
            }
            try
            {
                value = (T)rawValue;
                return true;
            }
            catch (Exception ex)
            {
                value = default(T);
                return false;
            }
        }
    }
    public class PropertyHelper:IPropertyHelper
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

        private bool TryGetProperty<T>(string propertyName, out PropertyInfo property)
        {
            return _dtoProperties.TryGetValue(propertyName.ToLowerInvariant(), out property);
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
    }

    public interface IPropertyHelper
    {
        bool TryGetValue<T>(string propertyName, out T value);
    }
}