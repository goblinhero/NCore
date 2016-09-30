using FluentMigrator;
using NCore.DBMigrate.Extensions;
using NCore.Demo.DBMigrate.Extensions;

namespace NCore.Demo.DBMigrate
{
    [Migration(3)]
    public class Migrate_003_Order : Migration
    {
        private readonly string _tableName = "Order";

        public override void Up()
        {
            this.DefaultTable(_tableName)
                .WithInt64Column("CustomerId", true)
                .WithAddress();
            this.DefaultForeignKey(_tableName, "Customer");
        }

        public override void Down()
        {
            Delete.Table(_tableName);
        }
    }
}