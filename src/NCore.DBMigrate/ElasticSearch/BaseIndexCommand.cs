using System;
using System.Collections.Generic;
using System.Linq;
using NCore.Web.Api.Contracts;
using NCore.Web.Commands;
using NCore.Web.FreeTextSearch;
using Nest;
using NHibernate;
using NHibernate.Util;

namespace NCore.DBMigrate.ElasticSearch
{
    public class BaseIndexCommand<TDto, TSearchDto> : ICommand
        where TDto : class, IHasIdDto
        where TSearchDto : class
    {
        private readonly string _index;
        private readonly Func<TDto, TSearchDto[]> _convert;

        public BaseIndexCommand(string index, Func<TDto, TSearchDto[]> convert)
        {
            _index = index;
            _convert = convert;
        }

        public bool TryExecute(ISession session, out IEnumerable<string> errors)
        {
            var entities = session.QueryOver<TDto>()
                .List();
            if (!entities.Any())
            {
                errors = new string[0];
                return true;
            }
            return new ElasticHelper().TryWrap(c =>
            {
                var count = 0;
                var pageSize = 1000;
                var descriptor = new BulkDescriptor();
                var descriptorEmpty = true;
                var totalCount = 0;
                entities.ForEach(e =>
                {
                    count++;
                    var search = _convert(e);
                    foreach (var s in search)
                    {
                        descriptor.Index<TSearchDto>(op => op.Document(s).Id(e.Id.Value).Index(_index));
                    }
                    descriptorEmpty = false;
                    if (count % pageSize == 0)
                    {
                        var result = c.Bulk(d => descriptor);
                        totalCount += result.Items.Count();
                        Console.WriteLine("Imported {0} {3} - errors: {1}, took {2} MS", totalCount, string.Join(", ", result.Errors), result.Took, typeof(TDto).Name);
                        descriptor = new BulkDescriptor();
                        descriptorEmpty = true;
                    }
                });
                if (!descriptorEmpty)
                {
                    var result = c.Bulk(d => descriptor);
                    totalCount += result.Items.Count();
                    Console.WriteLine("Imported {0} {3} - errors: {1}, took {2} MS", totalCount, string.Join(", ", result.Errors), result.Took, typeof(TDto).Name);
                }
                return true;
            }, out errors);
        }
    }
}