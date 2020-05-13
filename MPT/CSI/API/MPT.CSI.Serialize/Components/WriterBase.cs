using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.Serialize.Models;

namespace MPT.CSI.Serialize.Components
{
    public abstract class WriterBase : IWriter
    {
        protected Model _model;
        protected Dictionary<string, List<Dictionary<string, string>>> _tables;

        public abstract string FileExtension { get; }

        public string[] ToArray(string filePath, Tables tables)
        {
            string[] output = new string[numberOfLinesFromTable(tables.TableSet)];
            output = writeHeader(output, filePath);
            output = writeLines(output, tables.TableSet, out var currentLine);
            output = writeFooter(output, currentLine);

            return output;
        }

        public abstract Dictionary<string, List<Dictionary<string, string>>> FillTablesFromModel(Model model);

        public void WriteSingleTable(
            string tableName,
            Action<Model, List<Dictionary<string, string>>> readFunction)
        {
            readFunction(_model, _tables[tableName]);
        }

        protected abstract string[] writeHeader(string[] output, string filePath);

        protected abstract string[] writeLines(
            string[] output,
            ReadOnlyDictionary<string, List<Dictionary<string, string>>> tables,
            out int currentLine);

        protected abstract string[] writeFooter(string[] output, int currentLine);


        /// <summary>
        /// Returns the number of lines to be generated from the tables.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected int numberOfLinesFromTable(ReadOnlyDictionary<string, List<Dictionary<string, string>>> tables)
        {
            int numberOfLines = numberOfHeaderLines();
            numberOfLines += numberOfTableLines(tables);
            numberOfLines += numberOfFooterLines();
            return numberOfLines;
        }

        protected abstract int numberOfHeaderLines();

        protected abstract int numberOfTableLines(ReadOnlyDictionary<string, List<Dictionary<string, string>>> tables);

        protected abstract int numberOfFooterLines();
    }
}
