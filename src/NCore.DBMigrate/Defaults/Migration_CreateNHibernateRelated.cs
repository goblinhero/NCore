using FluentMigrator;

namespace NCore.DBMigrate.Defaults
{
    public class Migration_CreateNHibernateRelated : Migration
    {
        private readonly string _hibernateTable = "hibernate_unique_key";

        public override void Up()
        {
            Create.Table(_hibernateTable)
                .WithColumn("next_hi").AsInt64().Nullable();
            Execute.Sql($"INSERT INTO {_hibernateTable} VALUES (1)");
        }

        public override void Down()
        {
            Delete.Table(_hibernateTable);
        }
    }
}