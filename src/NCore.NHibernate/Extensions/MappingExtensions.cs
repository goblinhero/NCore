using System;
using System.Linq.Expressions;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.NHibernate.Extensions
{
    public static class MappingExtensions
    {
        public static void MapId<TMapping, T>(this TMapping mapping)
            where TMapping : ClassMapping<T>
            where T : class, IHasId
        {
            mapping.Id(m => m.Id, m => m.Generator(Generators.HighLow, g => g.Params(new {max_lo = 100})));
        }

        public static void MapEntity<TMapping, T>(this TMapping mapping)
            where TMapping : ClassMapping<T>
            where T : class, IEntity
        {
            mapping.MapId<TMapping, T>();
            mapping.Version(m => m.Version, m => m.Column("[Version]"));
            mapping.Property(m => m.CreationDate);
        }

        public static void MapTransaction<TMapping, T>(this TMapping mapping)
            where TMapping : ClassMapping<T>
            where T : class, ITransaction
        {
            mapping.MapId<TMapping, T>();
            mapping.Property(m => m.CreationDate);
        }

        public static void MapDate<TMapping, T>(this TMapping mapping, Expression<Func<T, Date>> prop)
            where TMapping : ClassMapping<T>
            where T : class
        {
            mapping.Component(prop, c => c.Property(a => a.DateDays));
        }
    }
}