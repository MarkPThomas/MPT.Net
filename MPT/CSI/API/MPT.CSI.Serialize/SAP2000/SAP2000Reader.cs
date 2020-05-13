using System.Collections.Generic;
using MPT.CSI.Serialize.Components;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.SAP2000.Readers;

namespace MPT.CSI.Serialize.SAP2000
{
    public class SAP2000Reader : ReaderBase
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

        public SAP2000Reader()
        {
            _tableNames = new SAP2000Tables();
        }

        /// <summary>
        /// Lines the name of the has table.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected override bool lineHasTableName(string line)
        {
            return line.Contains("TABLE:");
        }

        /// <summary>
        /// Lines the is end of table.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected override bool lineIsEndOfTable(string line, string tableName)
        {
            return (!string.IsNullOrWhiteSpace(tableName) && string.IsNullOrWhiteSpace(line));
        }

        /// <summary>
        /// Parses the name of the table.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>System.String.</returns>
        protected override string parseTableName(string text)
        {
            string[] textValues = text.Split('"');
            return textValues.Length == 3 ? textValues[1] : string.Empty;
        }

        // TODO: Detect wrap-around lines

        

        /// <summary>
        /// Fills the model from tables.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="tables">The tables.</param>
        /// <returns>Model.</returns>
        public override Model FillModelFromTables(Dictionary<string, List<Dictionary<string, string>>> tables)
        {
            _model = new Model();
            _tables = tables;

            // General settings
            ReadGeneralSettings.DefineGeneralSettings(this);
            ReadSingleTable(SAP2000Tables.GROUPS_1_DEFINITIONS, ReadGroups.SetGROUPS_1_DEFINITIONS);

            // Load Definitions
            ReadLoadingFunctions.DefineLoadingFunctions(this);
            ReadLoads.DefineLoads(this, _model, _tables);
            ReadSingleTable(SAP2000Tables.MASS_SOURCE, ReadGeneralSettings.SetMASS_SOURCE);

            // Component Definitions
            ReadMaterials.DefineMaterials(this);
            ReadFrameSections.DefineFrameSections(this);
            ReadAreaSections.DefineAreaSections(this);
            ReadLinkProperties.DefineLinkProperties(this);
            ReadTendons.DefineTendonProperties(this);
            ReadCables.DefineCableProperties(this);
            ReadSolids.DefineSolidProperties(this);

            // Joint
            ReadJoints.DefineJoints(this);
            ReadJoints.AssignJoints(this);
            ReadJoints.LoadJoints(this);

            // Frames
            ReadFrames.DefineFrames(this);
            ReadFrames.AssignFrames(this);
            ReadFrames.LoadFrames(this);

            // Areas
            ReadAreas.DefineAreas(this);
            ReadAreas.AssignAreas(this);
            ReadAreas.LoadAreas(this);

            // Links
            ReadLinks.DefineLinks(this);
            ReadLinks.AssignLinks(this);
            ReadLinks.LoadLinks(this);

            // Tendons
            ReadTendons.DefineTendons(this);
            ReadTendons.AssignTendons(this);
            ReadTendons.LoadTendons(this);

            // Cables
            ReadCables.DefineCables(this);
            ReadCables.AssignCables(this);
            ReadCables.LoadCables(this);

            // Solids
            ReadSolids.DefineSolids(this);
            ReadSolids.AssignSolids(this);
            ReadSolids.LoadSolids(this);

            // Misc Assigns & Defines
            ReadSingleTable(SAP2000Tables.GROUPS_2_ASSIGNMENTS, ReadGroups.SetGroups_2_ASSIGNMENTS);
            ReadSectionCuts.DefineSectionCuts(this);
            ReadConstraints.DefineConstraints(this);
            ReadConstraints.AssignConstraints(this);
            ReadHinges.DefineHinges(this);
            ReadHinges.AssignHinges(this);

            // Tables
            ReadTableSetsExports.DefineTableSets(this);
            ReadTableSetsExports.SetTableExports(this);

            // Design Preferences
            ReadDesignAluminumPreferences.DefinePreferences(this);
            ReadDesignColdFormedPreferences.DefinePreferences(this);
            ReadDesignConcretePreferences.DefinePreferences(this);
            ReadDesignSteelPreferences.DefinePreferences(this);

            // Design Overwrites
            ReadDesignAluminumOverwrites.DefineOverwrites(this);
            ReadDesignColdFormedOverwrites.DefineOverwrites(this);
            ReadDesignConcreteOverwrites.DefineOverwrites(this);
            ReadDesignSteelOverwrites.DefineOverwrites(this);

            return _model;
        }
    }
}
