using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace NCore.DBMigrate.Extensions
{
    public static class MappingExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax WithStringColumn(this ICreateTableColumnOptionOrWithColumnSyntax syntax, string name, bool nullable = false, int length = 255)
        {
            return syntax
                .WithColumn($"[{name}]").AsString(length).Null(nullable);
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax Null(this ICreateTableColumnOptionOrWithColumnSyntax syntax, bool nullable)
        {
            return nullable ? syntax.Nullable() : syntax.NotNullable();
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax WithInt64Column(this ICreateTableColumnOptionOrWithColumnSyntax syntax, string name, bool nullable = false)
        {
            return syntax
                .WithColumn($"[{name}]").AsInt64().Null(nullable);
        }

        public static void DefaultForeignKey(this Migration step, string fromTable, string toTable)
        {
            step.Create.ForeignKey($"FK_{fromTable}_{toTable}")
                .FromTable($"[{fromTable}]")
                .ForeignColumn($"{toTable}Id")
                .ToTable($"[{toTable}]")
                .PrimaryColumn("Id");
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax DefaultTable(this Migration step, string tableName)
        {
            return step.Create.Table($"[{tableName}]")
                .WithColumn("Id").AsInt64().PrimaryKey()
                .WithColumn("[Version]").AsInt32().Nullable()
                .WithColumn("CreationDate").AsDateTime().NotNullable();
        }
    }
}