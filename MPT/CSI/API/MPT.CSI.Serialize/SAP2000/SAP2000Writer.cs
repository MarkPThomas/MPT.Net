using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.Serialize.Components;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.SAP2000.Writers;

namespace MPT.CSI.Serialize.SAP2000
{
    public class SAP2000Writer : WriterBase
    {
        /// <summary>
        /// The file extension
        /// </summary>
        internal const string FILE_EXTENSION = ".$2k";

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <value>The file extension.</value>
        public override string FileExtension => FILE_EXTENSION;

        public override Dictionary<string, List<Dictionary<string, string>>> FillTablesFromModel(Model model)
        {
            _model = model;
            _tables = new Dictionary<string, List<Dictionary<string, string>>>();

            //// General Settings
            WriteGeneralSettings.DefineGeneralSettings(this);
            WriteSingleTable(SAP2000Tables.GROUPS_1_DEFINITIONS, WriteGroups.SetGROUPS_1_DEFINITIONS);

            //// Load Definitions
            WriteLoadingFunctions.DefineLoadingFunctions(this);
            WriteLoads.DefineLoads(this, _model, _tables);
            WriteSingleTable(SAP2000Tables.MASS_SOURCE, WriteGeneralSettings.SetMASS_SOURCE);

            //// Component Definitions
            WriteMaterials.DefineMaterials(this);
            WriteFrameSections.DefineFrameSections(this);
            WriteAreaSections.DefineAreaSections(this);
            WriteLinkProperties.DefineLinkProperties(this);
            WriteTendons.DefineTendonProperties(this);
            WriteCables.DefineCableProperties(this);
            WriteSolids.DefineSolidProperties(this);

            //// Joint
            WriteJoints.DefineJoints(this);
            WriteJoints.AssignJoints(this);
            WriteJoints.LoadJoints(this);

            //// Frames
            WriteFrames.DefineFrames(this);
            WriteFrames.AssignFrames(this);
            WriteFrames.LoadFrames(this);

            //// Areas
            WriteAreas.DefineAreas(this);
            WriteAreas.AssignAreas(this);
            WriteAreas.LoadAreas(this);

            //// Links
            WriteLinks.DefineLinks(this);
            WriteLinks.AssignLinks(this);
            WriteLinks.LoadLinks(this);

            //// Tendons
            WriteTendons.DefineTendons(this);
            WriteTendons.AssignTendons(this);
            WriteTendons.LoadTendons(this);

            //// Cables
            WriteCables.DefineCables(this);
            WriteCables.AssignCables(this);
            WriteCables.LoadCables(this);

            //// Solids
            WriteSolids.DefineSolids(this);
            WriteSolids.AssignSolids(this);
            WriteSolids.LoadSolids(this);

            //// Misc Assigns & Defines
            WriteSingleTable(SAP2000Tables.GROUPS_2_ASSIGNMENTS, WriteGroups.SetGroups_2_ASSIGNMENTS);
            WriteSectionCuts.DefineSectionCuts(this);
            WriteConstraints.DefineConstraints(this);
            WriteConstraints.AssignConstraints(this);
            WriteHinges.DefineHinges(this);
            WriteHinges.AssignHinges(this);

            //// Tables
            WriteTableSetsExports.DefineTableSets(this);
            WriteTableSetsExports.SetTableExports(this);

            //// Design Preferences
            WriteDesignAluminumPreferences.DefinePreferences(this);
            WriteDesignColdFormedPreferences.DefinePreferences(this);
            WriteDesignConcretePreferences.DefinePreferences(this);
            WriteDesignSteelPreferences.DefinePreferences(this);

            //// Design Overwrites
            WriteDesignAluminumOverwrites.DefineOverwrites(this);
            WriteDesignColdFormedOverwrites.DefineOverwrites(this);
            WriteDesignConcreteOverwrites.DefineOverwrites(this);
            WriteDesignSteelOverwrites.DefineOverwrites(this);

            return _tables;
        }

        /// <summary>
        /// Writes the header.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="filePath">The file path.</param>
        /// <returns>System.String[].</returns>
        protected override string[] writeHeader(string[] output, string filePath)
        {
            output[0] = "File " + filePath + " was saved on m/d/yy at h:mm:ss";
            output[1] = string.Empty;
            return output;
        }

        /// <summary>
        /// Writes the lines.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="tables">The tables.</param>
        /// <param name="currentLine">The current line.</param>
        /// <returns>System.String[].</returns>
        protected override string[] writeLines(
            string[] output,
            ReadOnlyDictionary<string, List<Dictionary<string, string>>> tables, out int currentLine)
        {
            currentLine = 2;
            foreach (string tableName in tables.Keys)
            {
                output[currentLine] = "TABLE:  \"" + tableName + "\"";
                currentLine++;
                foreach (Dictionary<string, string> tableEntries in tables[tableName])
                {
                    string tableLine = string.Empty;
                    foreach (string key in tableEntries.Keys)
                    {
                        tableLine += "   " + key + "=" + tableEntries[key];
                    }

                    output[currentLine] = tableLine;
                    currentLine++;
                }

                output[currentLine] = " ";
            }

            return output;
        }

        /// <summary>
        /// Writes the footer.
        /// </summary>
        /// <param name="output">The output.</param>
        /// <param name="currentLine">The current line.</param>
        /// <returns>System.String[].</returns>
        protected override string[] writeFooter(string[] output, int currentLine)
        {
            output[currentLine] = "END TABLE DATA";
            output[1] = string.Empty;
            return output;
        }


        /// <summary>
        /// Numbers the of header lines.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected override int numberOfHeaderLines()
        {
            return 2;
        }

        /// <summary>
        /// Numbers the of table lines.
        /// </summary>
        /// <param name="tables">The tables.</param>
        /// <returns>System.Int32.</returns>
        protected override int numberOfTableLines(ReadOnlyDictionary<string, List<Dictionary<string, string>>> tables)
        {
            // For each table
            int numberOfLines = 2 * tables.Count;

            // For each line entry in each table
            foreach (List<Dictionary<string, string>> table in tables.Values)
            {
                numberOfLines += table.Count;
            }

            return numberOfLines;
        }

        /// <summary>
        /// Numbers the of footer lines.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected override int numberOfFooterLines()
        {
            return 2;
        }
    }
}
