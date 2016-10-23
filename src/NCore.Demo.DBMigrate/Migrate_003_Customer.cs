using FluentMigrator;
using NCore.DBMigrate.Extensions;
using NCore.Demo.DBMigrate.Extensions;

namespace NCore.Demo.DBMigrate
{
    [Migration(3)]
    public class Migrate_003_Customer : Migration
    {
        private readonly string _tableName = "Customer";

        public override void Up()
        {
            this.DefaultTable(_tableName)
                .WithStringColumn("CompanyName")
                .WithAddress()
                .WithCompany();
            this.CompanyForeignKey(_tableName);
        }

        public override void Down()
        {
            Delete.Table(_tableName);
        }
    }
}