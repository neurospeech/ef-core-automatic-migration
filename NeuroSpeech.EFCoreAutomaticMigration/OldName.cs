using System;

namespace NeuroSpeech.EFCoreAutomaticMigration
{
    public class OldNameAttribute : Attribute
    {
        public string Name { get; set; }

        public OldNameAttribute(string name)
        {
            Name = name;
        }
    }
}