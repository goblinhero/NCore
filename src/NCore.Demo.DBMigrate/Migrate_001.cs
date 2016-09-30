using FluentMigrator;
using NCore.DBMigrate.Defaults;

namespace NCore.Demo.DBMigrate
{
    [Migration(1)]
    public class Migrate_001 : Migration_CreateNHibernateRelated
    {
    }
}