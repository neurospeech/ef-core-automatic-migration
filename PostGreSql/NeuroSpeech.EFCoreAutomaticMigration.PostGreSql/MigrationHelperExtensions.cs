using Microsoft.EntityFrameworkCore;
using NeuroSpeech.EFCoreAutomaticMigration.PostGreSql;

namespace NeuroSpeech.EFCoreAutomaticMigration
{
    public static class MigrationHelperExtensions
    {
        public static ModelMigrationBase MigrationForPostGreSql(this DbContext context)
        {
            return new PostGreSqlMigration(context);
        }
    }
}
