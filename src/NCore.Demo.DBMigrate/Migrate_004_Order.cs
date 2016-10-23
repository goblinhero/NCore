using FluentMigrator;
using NCore.DBMigrate.Extensions;
using NCore.Demo.DBMigrate.Extensions;

namespace NCore.Demo.DBMigrate
{
    [Migration(4)]
    public class Migrate_004_Order : Migration
    {
        private readonly string _tableName = "Order";

        public override void Up()
        {
            this.DefaultTable(_tableName)
                .WithInt64Column("CustomerId", true)
                .WithAddress()
                .WithCompany();
            this.DefaultForeignKey(_tableName, "Customer");
            this.CompanyForeignKey(_tableName);
        }

        public override void Down()
        {
            Delete.Table(_tableName);
        }
    }
}