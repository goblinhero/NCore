using System;
using System.Collections.Generic;
using System.IO;
using Nancy;
using Nancy.Json;
using Nancy.ModelBinding;
using Serilog;

namespace NCore.Web.Utilities
{
    public class DynamicModelBinder : IModelBinder
    {
        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
        {
            using (var reader = new StreamReader(context.Request.Body))
            {
                var input = reader.ReadToEnd();
                try
                {
                    return new JavaScriptSerializer().Deserialize<IDictionary<string, object>>(input);
                } catch(Exception ex)
                {
                    Log.Error(ex, "Failed to serialize incomming Json string: {@Json}", input);
                    context.Response = new Response() { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = $"Failed to parse {input} as valid Json" };
                    return null;
                }
            }
        }

        public bool CanBind(Type modelType)
        {
            return modelType == typeof(IDictionary<string, object>);
        }
    }
}