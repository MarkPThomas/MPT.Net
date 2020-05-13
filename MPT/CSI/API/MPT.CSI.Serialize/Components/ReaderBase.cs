using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;

namespace MPT.CSI.Serialize.Components
{
    public abstract class ReaderBase : IReader
    {
        protected Model _model;
        protected Dictionary<string, List<Dictionary<string, string>>> _tables;
        internal ProgramTables _tableNames;

        public abstract string FileExtension { get; }

        public Tables ReadFile(Tables tables, IEnumerable<string> lines)
        {
            string tableName = string.Empty;
            List<Dictionary<string, string>> tableKeyValues = new List<Dictionary<string, string>>();
            foreach (string line in lines)
            {
                if (lineHasTableName(line))
                {
                    tableName = parseTableName(line);
                }
                else if (lineIsEndOfTable(line, tableName))
                {
                    tables.AddTable(tableName, tableKeyValues);
                    tableName = string.Empty;
                    tableKeyValues = new List<Dictionary<string, string>>();
                }
                else if (!string.IsNullOrWhiteSpace(tableName))
                { // TODO: May be program specific
                    string[] lineKeyValues = System.Text.RegularExpressions.Regex.Split(line, @"\s{3,3}");
                    Dictionary<string, string> keyValue = new Dictionary<string, string>();
                    foreach (var lineKeyValue in lineKeyValues)
                    {
                        string[] keyValues = lineKeyValue.Split('=');
                        if (keyValues.Length != 2) continue;

                        keyValue[keyValues[0]] = keyValues[1];
                    }
                    tableKeyValues.Add(keyValue);
                }
            }

            return tables;
        }

        public abstract Model FillModelFromTables(Dictionary<string, List<Dictionary<string, string>>> tables);

        public bool TableIsSupported(string tableName)
        {
            return _tableNames.TableNames.Contains(tableName);
        }

        public void ReadSingleTable(
            string tableName,
            Action<Model, List<Dictionary<string, string>>> readFunction)
        {
            if (containsTable(tableName))
            {
                readFunction(_model, _tables[tableName]);
            }
        }

        protected bool containsTable(string tableName)
        {
            return _tables.ContainsKey(tableName);
        }

        protected abstract bool lineHasTableName(string line);

        protected abstract bool lineIsEndOfTable(string line, string tableName);

        protected abstract string parseTableName(string text);


    }
}
