﻿using System;
using System.Linq.Expressions;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Web.Extensions;
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

        public static void MapAddressDto<TMapping, T>(this TMapping mapping, Expression<Func<T, AddressDto>> prop)
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
        public static void MapDemoCompany<TMapping, T>(this TMapping mapping)
            where TMapping : ClassMapping<T>
            where T : class,IHasCompany
        {
            mapping.MapHasCompany<TMapping, T>(typeof(Company));
        }
    }
}