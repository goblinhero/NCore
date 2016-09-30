using FluentMigrator;
using NCore.DBMigrate.Extensions;

namespace NCore.Demo.DBMigrate
{
    [Migration(4)]
    public class Migrate_004_OrderLine : Migration
    {
        private readonly string _tableName = "OrderLine";

        public override void Up()
        {
            this.DefaultTable(_tableName)
                .WithInt64Column("OrderId", true)
                .WithColumn("[Index]").AsInt32().NotNullable()
                .WithStringColumn("Description")
                .WithColumn("Total").AsDecimal().NotNullable();
            this.DefaultForeignKey(_tableName, "Order");
        }

        public override void Down()
        {
            Delete.Table(_tableName);
        }
    }
}