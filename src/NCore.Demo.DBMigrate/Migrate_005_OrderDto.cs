using FluentMigrator;

namespace NCore.Demo.DBMigrate
{
    [Migration(5)]
    public class Migrate_005_OrderDto : Migration
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