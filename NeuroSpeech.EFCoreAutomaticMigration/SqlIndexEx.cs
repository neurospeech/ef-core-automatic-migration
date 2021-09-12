using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Linq;

namespace NeuroSpeech.EFCoreAutomaticMigration
{
    public class SqlIndexEx
    {
        public readonly string Name;
        public readonly IEntityType DeclaringEntityType;
        public readonly IReadOnlyList<IProperty> Properties;
        public readonly IReadOnlyList<string> IncludedProperties;
        public readonly string Filter;
        public readonly bool Unique;
        public readonly DbTableInfo Table;
        public readonly IIndex Index;

        public SqlIndexEx(DbTableInfo table, IIndex index, ModelMigrationBase modelMigration)
        {
            this.Table = table;
            this.Index = index;
#if NETSTANDARD2_1
            this.Name = index.GetDatabaseName();
#else
            this.Name = index.GetName();
#endif
            this.DeclaringEntityType = index.DeclaringEntityType;
            this.Properties = index.Properties;
            this.IncludedProperties = index.GetIncludeProperties();
            this.Filter = index.GetFilter();
            this.Unique = index.IsUnique;

            if(this.Filter == null)
            {
                // create filter based on nullable foreign key...

                var nullables = this.Properties.Where(x => x.IsColumnNullable() && x.IsForeignKey());
                if (index.DeclaringEntityType.BaseType != null)
                {
                    nullables = this.Properties;
                } 
                if(nullables.Any())
                {
                    var columns = string.Join(" AND ",
                        nullables.Select(x => $"{modelMigration.Escape(x.ColumnName())} IS NOT NULL")
                        );
                    this.Filter = $"({columns})";
                }
                
            }
        }

        public string GetName() => Name;
        public string GetFilter() => Filter;
    }
}