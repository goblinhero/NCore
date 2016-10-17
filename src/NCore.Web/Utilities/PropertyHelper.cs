using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NCore.Web.Utilities
{
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
}