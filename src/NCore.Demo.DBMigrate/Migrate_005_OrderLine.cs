using FluentMigrator;
using NCore.DBMigrate.Extensions;
using NCore.Demo.DBMigrate.Extensions;

namespace NCore.Demo.DBMigrate
{
    [Migration(5)]
    public class Migrate_005_OrderLine : Migration
    {
        private readonly string _tableName = "OrderLine";

        public override void Up()
        {
            this.DefaultTable(_tableName)
                .WithInt64Column("OrderId", true)
                .WithColumn("[Index]").AsInt32().NotNullable()
                .WithStringColumn("Description")
                .WithColumn("Total").AsDecimal().NotNullable()
                .WithCompany();
            this.DefaultForeignKey(_tableName, "Order");
            this.CompanyForeignKey(_tableName);
        }

        public override void Down()
        {
            Delete.Table(_tableName);
        }
    }
}