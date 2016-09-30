using System.Collections.Generic;
using Nancy;
using NCore.Nancy.Queries;

namespace NCore.Nancy
{
    public abstract class CRUDModule<T, TDto> : NancyModule
    {
        private readonly SessionHelper _sessionHelper = new SessionHelper();

        protected CRUDModule()
        {
            var staticRoutes = new StaticRoutes(typeof(T).Name);
            Get[staticRoutes.Get] = p => GetOne(p.id);
        }

        private object GetOne(long id)
        {
            TDto result;
            IEnumerable<string> errors;
            return _sessionHelper.TryQuery(new GetQuery<TDto>(id), out result, out errors)
                ? new
                {
                    Success = true,
                    Result = result
                }
                : (object) new
                {
                    Success = false,
                    Errors = errors
                };
        }

    }
    public class StaticRoutes
    {
        private readonly string _base;

        public StaticRoutes(string baseRoute)
        {
            _base = baseRoute;
        }

        public string Get => _base + "/{id}";
        public string Post => _base;
        public string Put => _base + "/{id}";
        public string Delete => _base + "/{id}";
    }
}