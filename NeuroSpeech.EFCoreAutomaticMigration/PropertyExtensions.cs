using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeuroSpeech.EFCoreAutomaticMigration
{
    public static class PropertyExtensions
    {

        public static string GetSchemaOrDefault(this IEntityType type)
        {
            return type.GetSchema() ?? type.GetDefaultSchema() ?? "dbo";
        }

    }
}
