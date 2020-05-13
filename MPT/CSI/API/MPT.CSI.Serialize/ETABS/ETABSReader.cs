using System.Collections.Generic;
using MPT.CSI.Serialize.Components;
using MPT.CSI.Serialize.Models;

namespace MPT.CSI.Serialize.ETABS
{
    public class ETABSReader : ReaderBase
    {
        internal const string FILE_EXTENSION = ".$et";

        public override string FileExtension => FILE_EXTENSION;

        public ETABSReader()
        {
            _tableNames = new ETABSTables();
        }

        public override Model FillModelFromTables(
            Model model,
            Dictionary<string, List<Dictionary<string, string>>> tables)
        {
            _model = model;
            _tables = tables;

            throw new System.NotImplementedException();
        }

        protected override bool lineHasTableName(string line)
        {
            return line[0] == '$';
        }

        protected override bool lineIsEndOfTable(string line, string tableName)
        {
            return (!string.IsNullOrWhiteSpace(tableName) && string.IsNullOrWhiteSpace(line));
        }

        protected override string parseTableName(string text)
        {
            if (text.Length <= 2) return string.Empty;

            string[] textValues = text.Split('$');
            return textValues.Length == 2 ? textValues[1].Trim() : string.Empty;
        }

    }
}
