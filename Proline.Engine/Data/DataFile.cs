using System;
using System.Collections.Generic;

namespace Proline.Engine.Client
{
    internal class DataFile
    {
        public DataFileEntry[] Entries { get; set; }
        public int Handle { get; set; }
        public string FileName { get; internal set; }

        internal void AddEntryValue<T>(string entryName, T value)
        {
            var entries = new List<DataFileEntry>(Entries);
            var entry = new DataFileEntry();
            entry.Key = entryName;
            entry.Value = value.ToString();
            entries.Add(entry);
            entries.ToArray();
        }

        internal T GetEntryValue<T>(string entryName)
        {
            for (int i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].ToString().ToLower().Equals(entryName))
                {
                    return (T)Convert.ChangeType(Entries[i].Value, typeof(T));
                }
            }
            throw new Exception();
        }
    }
}
