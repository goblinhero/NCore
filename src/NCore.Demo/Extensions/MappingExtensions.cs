using System;
using System.Linq.Expressions;
using NCore.Demo.Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Extensions
{
    public static class MappingExtensions
    {
        public static void MapAddress<TMapping, T>(this TMapping mapping, Expression<Func<T, Address>> prop)
            where TMapping : ClassMapping<T>
            where T : class
        {
            mapping.Component(prop, c =>
            {
                c.Property(a => a.City);
                c.Property(a => a.Street);
                c.Property(a => a.Country);
            });
        }
    }
}