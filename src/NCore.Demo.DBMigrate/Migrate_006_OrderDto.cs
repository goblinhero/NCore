using FluentMigrator;

namespace NCore.Demo.DBMigrate
{
    [Migration(6)]
    public class Migrate_006_OrderDto : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"CREATE VIEW OrderDto
                          AS (
                            SELECT o.*
                                 , c.CompanyName AS CustomerCompanyName
                            FROM [Order] o
                            LEFT JOIN Customer c ON c.Id = o.CustomerId)");
        }

        public override void Down()
        {
            Execute.Sql("DROP VIEW OrderDto");
        }
    }
}