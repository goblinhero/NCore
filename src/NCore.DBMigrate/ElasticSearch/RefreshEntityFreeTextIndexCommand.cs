using System;
using System.Collections.Generic;
using NCore.Web;
using NCore.Web.Api.Contracts;
using NCore.Web.FreeTextSearch;
using Nest;

namespace NCore.DBMigrate.ElasticSearch
{
    public abstract class RefreshEntityFreeTextIndexCommand<TEntity,TDto,TSearch>
        where TEntity:class
        where TSearch:class
        where TDto:class,IHasIdDto
    {
        private readonly string _defaultIndexPrefix;

        protected RefreshEntityFreeTextIndexCommand(string defaultIndexPrefix)
        {
            _defaultIndexPrefix = defaultIndexPrefix;
        }

        public bool TryExecute(out IEnumerable<string> errors)
        {
            Console.WriteLine($"Now starting indexing entities of type: {typeof(TEntity).Name}");
            var elasticHelper = new ElasticHelper();
            var alias = $"{_defaultIndexPrefix}_{typeof(TEntity).Name.ToLower()}";
            var createIndexCommand = new BaseCreateIndexCommand<TSearch>(alias, tmd => tmd.Properties(AddProperties));
            if (!elasticHelper.TryWrapCommand(createIndexCommand, out errors))
                return false;
            ElasticHelper.SetMap<TSearch>(createIndexCommand.NewIndexName,pd => AddProperties(pd));
            var helper = new SessionHelper();
            helper.TryExecute(new BaseIndexCommand<TDto,TSearch>(createIndexCommand.NewIndexName,Convert), out errors);
            elasticHelper.TryWrapCommand(new AlterIndexAliasCommand(alias, createIndexCommand.NewIndexName), out errors);
            errors = new string[0];
            return true;
        }

        protected abstract IPromise<IProperties> AddProperties(PropertiesDescriptor<TSearch> arg);
        protected abstract TSearch[] Convert(TDto dto);
    }

}
