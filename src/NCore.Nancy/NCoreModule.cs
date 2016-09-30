using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using NCore.Nancy.Queries;

namespace NCore.Nancy
{
    public abstract class NCoreModule<T,TDto>:NancyModule
    {
        private readonly SessionHelper _sessionHelper = new SessionHelper();

        protected NCoreModule()
        {
            Get[StaticRoutes.Get] = p => GetOne(p.id);
        }

        private object GetOne(long id)
        {
            TDto result;
            IEnumerable<string> errors;
            return _sessionHelper.TryQuery(new GetQuery<TDto>(id), out result, out errors) ? new
            {
                Success = true,
                Result = result,
            } : (object)new
            {
                Success = false,
                Errors = errors
            };
        }

        public static class StaticRoutes
        {
            public const string Base = "CustomData/{xenaAppId}/{fiscalSetupId}/{customType}";
            public const string Get = Base + "/{id}";
            public const string Post = Base;
            public const string Put = Base + "/{id}";
            public const string Delete = Base + "/{id}";
        }
    }
}
