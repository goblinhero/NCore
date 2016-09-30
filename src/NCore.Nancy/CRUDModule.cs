using System.Collections.Generic;
using Nancy;
using Nancy.ModelBinding;
using NCore.Nancy.Creators;
using NCore.Nancy.Queries;
using NCore.Nancy.Updaters;

namespace NCore.Nancy
{
    public abstract class CRUDModule<T, TDto> : NancyModule
        where T : IEntity
    {
        private readonly SessionHelper _sessionHelper = new SessionHelper();

        protected CRUDModule()
        {
            var staticRoutes = new StaticRoutes(typeof(T).Name);
            Get[staticRoutes.Get] = p => GetOne(p.id);
            Post[staticRoutes.Post] = p => PostOne(this.Bind<TDto>());
            Put[staticRoutes.Put] = p => PutOne(p.id, this.Bind<TDto>());
        }

        protected abstract ICreator<T> GetCreator(TDto dto);
        protected abstract IUpdater<T> GetUpdater(long id, TDto dto);

        private object PutOne(long id, TDto dto)
        {
            IEnumerable<string> errors;
            var updater = GetUpdater(id,dto);
            return _sessionHelper.TryUpdate(updater, out errors)
                ? new
                {
                    Success = true,
                    updater.Id,
                }
                : (object)new
                {
                    Success = false,
                    Errors = errors
                };
        }

        private object PostOne(TDto dto)
        {
            IEnumerable<string> errors;
            var creator = GetCreator(dto);
            return _sessionHelper.TryCreate(creator, out errors)
                ? new
                {
                    Success = true,
                    creator.AssignedId,
                }
                : (object)new
                {
                    Success = false,
                    Errors = errors
                };
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
                : (object)new
                {
                    Success = false,
                    Errors = errors
                };
        }
    }

    public class StaticRoutes
    {
        public StaticRoutes(string baseRoute)
        {
            Post = baseRoute;
        }

        public string Get => Post + "/{id}";
        public string Post { get; }

        public string Put => Post + "/{id}";
        public string Delete => Post + "/{id}";
    }
}