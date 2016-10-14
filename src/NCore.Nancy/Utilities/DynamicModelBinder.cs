using System;
using System.Collections.Generic;
using System.IO;
using Nancy;
using Nancy.Json;
using Nancy.ModelBinding;

namespace NCore.Nancy.Utilities
{
    public class DynamicModelBinder : IModelBinder
    {
        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
        {
            using (var reader = new StreamReader(context.Request.Body))
            {
                var input = reader.ReadToEnd();
                return new JavaScriptSerializer().Deserialize<dynamic>(input);
            }
        }

        public bool CanBind(Type modelType)
        {
            return modelType == typeof(IDictionary<string, object>);
        }
    }
}