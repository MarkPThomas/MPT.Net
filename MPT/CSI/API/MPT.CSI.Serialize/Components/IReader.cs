using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;

namespace MPT.CSI.Serialize.Components
{
    public interface IReader
    {
        string FileExtension { get; }

        Tables ReadFile(Tables tables, IEnumerable<string> lines);

        Model FillModelFromTables(Dictionary<string, List<Dictionary<string, string>>> tables);

        bool TableIsSupported(string tableName);

        void ReadSingleTable(
            string tableName,
            Action<Model, List<Dictionary<string, string>>> readFunction);
    }
}
