using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace NeuroSpeech.EFCoreAutomaticMigration
{
    public static class SqlServerMigrationExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="migration"></param>
        /// <param name="name">Set to null to not check for old cache</param>
        /// <returns></returns>
        public static T WithMigrationTable<T>(this T migration, string name = null)
            where T : ModelMigrationBase
        {
            migration.MigrationsTable = name;
            return migration;
        }


        public static T OnColumnChange<T>(this T migration, ColumnChangedDelegate d)
            where T: ModelMigrationBase
        {
            migration.onColumnChange = d;
            return migration;
        }

        public static ModelMigrationBase MigrationForSqlServer(this DbContext context)
        {
            return new ModelMigration(context);
        }
    }

}
