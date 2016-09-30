using FluentMigrator;
using NCore.DBMigrate.Extensions;
using NCore.Demo.DBMigrate.Extensions;

namespace NCore.Demo.DBMigrate
{
    [Migration(2)]
    public class Migrate_002_Customer : Migration
    {
        private readonly string _tableName = "Customer";

        public override void Up()
        {
            this.DefaultTable(_tableName)
                .WithStringColumn("CompanyName")
                .WithAddress();
        }

        public override void Down()
        {
            Delete.Table(_tableName);
        }
    }
}