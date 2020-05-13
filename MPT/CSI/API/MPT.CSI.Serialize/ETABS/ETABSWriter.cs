using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.Serialize.Components;

namespace MPT.CSI.Serialize.ETABS
{
    public class ETABSWriter : WriterBase
    {
        internal const string FILE_EXTENSION = ".$et";

        public override string FileExtension => FILE_EXTENSION;


        protected override string[] writeHeader(string[] output, string filePath)
        {
            output[0] = "File " + filePath + " saved " + "m/d/yy h:mm:ss AM/PM"; // TODO: Automate date/time
            output[1] = string.Empty;
            return output;
        }

        protected override string[] writeLines(
            string[] output,
            ReadOnlyDictionary<string, List<Dictionary<string, string>>> tables,
            out int currentLine)
        {
            currentLine = 2;
            foreach (string tableName in tables.Keys)
            {
                output[currentLine] = "$ " + tableName;
                currentLine++;
                foreach (Dictionary<string, string> tableEntries in tables[tableName])
                {
                    string tableLine = string.Empty;
                    foreach (string key in tableEntries.Keys)
                    {
                        tableLine += "  " + key + " " + tableEntries[key];
                    }
                    // TODO: Handle special cases, such as Controls, Grids, Log

                    output[currentLine] = tableLine;
                    currentLine++;
                }

                output[currentLine] = " ";
            }

            return output;
        }

        protected override string[] writeFooter(string[] output, int currentLine)
        {
            output[currentLine] = "$ END OF MODEL FILE";
            output[1] = string.Empty;
            return output;
        }

        protected override int numberOfHeaderLines()
        {
            return 2;
        }

        protected override int numberOfTableLines(ReadOnlyDictionary<string, List<Dictionary<string, string>>> tables)
        {
            // For each table
            int numberOfLines = 2 * tables.Count;

            // For each line entry in each table
            foreach (List<Dictionary<string, string>> table in tables.Values)
            {
                numberOfLines += table.Count;
                // TODO: Handle special cases, such as Controls, Grids, Log
            }

            return numberOfLines;
        }

        protected override int numberOfFooterLines()
        {
            return 2;
        }
    }
}
