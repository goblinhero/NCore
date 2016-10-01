using FluentMigrator.Builders.Create.Table;
using NCore.DBMigrate.Extensions;

namespace NCore.Demo.DBMigrate.Extensions
{
    public static class MappingExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithAddress(
            this ICreateTableColumnOptionOrWithColumnSyntax syntax)
        {
            return syntax
                .WithStringColumn("Street")
                .WithStringColumn("City")
                .WithStringColumn("Country");
        }
    }
}