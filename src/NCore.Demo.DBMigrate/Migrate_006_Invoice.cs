using FluentMigrator;
using NCore.DBMigrate.Extensions;
using NCore.Demo.DBMigrate.Extensions;

namespace NCore.Demo.DBMigrate
{
    [Migration(6)]
    public class Migrate_006_Invoice : Migration
    {
        private readonly string _tableName = "Invoice";
        private readonly string _tableNameLines = "InvoiceLine";

        public override void Up()
        {
            this.DefaultTable(_tableName)
                .WithInt64Column("CustomerId")
                .WithAddress()
                .WithColumn("Total").AsDecimal().NotNullable()
                .WithColumn("DateDays").AsInt32().NotNullable();
            this.DefaultForeignKey(_tableName, "Customer");
            this.DefaultTable(_tableNameLines)
                .WithInt64Column("InvoiceId", true)
                .WithColumn("[Index]").AsInt32().NotNullable()
                .WithStringColumn("Description")
                .WithColumn("Total").AsDecimal().NotNullable();
            this.DefaultForeignKey(_tableNameLines, "Invoice");
        }

        public override void Down()
        {
            Delete.Table(_tableName);
            Delete.Table(_tableNameLines);
        }
    }
}