using FluentMigrator;
using NCore.DBMigrate.Extensions;
using NCore.Demo.DBMigrate.Extensions;

namespace NCore.Demo.DBMigrate
{
    [Migration(7)]
    public class Migrate_007_Invoice : Migration
    {
        private readonly string _tableName = "Invoice";
        private readonly string _tableNameLines = "InvoiceLine";

        public override void Up()
        {
            this.DefaultTable(_tableName)
                .WithInt64Column("CustomerId")
                .WithAddress()
                .WithColumn("Total").AsDecimal().NotNullable()
                .WithColumn("DateDays").AsInt32().NotNullable()
                .WithCompany();
            this.DefaultForeignKey(_tableName, "Customer");
            this.CompanyForeignKey(_tableName);
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