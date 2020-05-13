using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using MPT.CSI.Serialize.Components;
using MPT.CSI.Serialize.ETABS;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.SAP2000;

namespace MPT.CSI.Serialize
{
    public class Tables
    {
        private Dictionary<string, List<Dictionary<string, string>>> _tables = 
            new Dictionary<string, List<Dictionary<string, string>>>();
        
        public ReadOnlyDictionary<string, List<Dictionary<string, string>>> TableSet => 
            new ReadOnlyDictionary<string, List<Dictionary<string, string>>>(_tables);

        public string ModelName { get; }

        public string ProgramName
        {
            get
            {
                // TODO: Improve this with polymorphism
                switch (ModelExtension)
                {
                    case SAP2000Reader.FILE_EXTENSION:
                        return "SAP2000";
                    case ETABSReader.FILE_EXTENSION:
                        return "ETABS";
                    default:
                        return string.Empty;
                }
            }
        }
        public string ModelExtension { get; private set; }
        public bool HasTables { get; private set; }

        private Model _model;
        public Model Model => _model ?? (_model = new Model());

        public Tables() { }

        public Tables(string modelFileName)
        {
            ModelExtension = Path.GetExtension(modelFileName);
            ModelName = Path.GetFileNameWithoutExtension(modelFileName);
        }

        public bool AddTable(string tableName, List<Dictionary<string, string>> values)
        {
            if (string.IsNullOrWhiteSpace(tableName) ||
                values == null ||
                values.Count == 0) return false;
            if (_tables.ContainsKey(tableName)) return false;

            _tables.Add(tableName, values);
            HasTables = true;
            return true;
        }

        public bool RemoveTable(string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName)) return false;
            if (!_tables.Remove(tableName)) return false;

            HasTables = _tables.Count > 0;
            return true;

        }

        public bool GenerateModelFromTables(IReader reader)
        {
            if (!HasTables) return false;
            ModelExtension = reader.FileExtension;
            _model = reader.FillModelFromTables(_tables);

            //Model model = Model;
            //foreach (var table in _tables)
            //{
            //    if (!reader.TableIsSupported(table.Key))
            //    {
            //        // TODO: Complete implementation
            //        //throw new ArgumentException($"Table {table.Key} not currently supported.");
            //        continue;
            //    }

            //    model = reader.FillModelFromTable(model, table);
            //}
            return true;
        }

        public bool GenerateTablesFromModel(IWriter writer) 
        {
            ModelExtension = writer.FileExtension;
            _tables = writer.FillTablesFromModel(Model);
            return true;
        }
    }
}
