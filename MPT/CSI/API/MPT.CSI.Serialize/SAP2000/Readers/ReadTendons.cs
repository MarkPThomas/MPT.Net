using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.TendonSections;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadTendons
    {
        internal static void DefineTendonProperties(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.TENDON_SECTION_DEFINITIONS, setTENDON_SECTION_DEFINITIONS);
        }

        internal static void DefineTendons(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.CONNECTIVITY_TENDON, setCONNECTIVITY_TENDON);
        }

        internal static void AssignTendons(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.TENDON_SECTION_ASSIGNMENTS, setTENDON_SECTION_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.TENDON_LAYOUT_DATA_01_GENERAL, setTENDON_LAYOUT_DATA_01_GENERAL);
            reader.ReadSingleTable(SAP2000Tables.TENDON_LAYOUT_DATA_02_SEGMENTS, setTENDON_LAYOUT_DATA_02_SEGMENTS);
        }

        internal static void LoadTendons(SAP2000Reader reader)
        {
            // TODO: Add tendon loads
        }

        /// <summary>
        /// Sets the tendon section definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setTENDON_SECTION_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                TendonProperty tendonProperty = model.Components.TendonProperties.FillItem(tableRow["TendonSect"]);
                tendonProperty.ColorName = tableRow["Color"];
                if (tableRow.ContainsKey("Notes")) tendonProperty.Notes = tableRow["Notes"];
                tendonProperty.MaterialName = tableRow["Material"];
                tendonProperty.TendonSpecification = Enums.EnumLibrary.ConvertStringToEnumByDescription<eCableSpecification>(tableRow["Specify"]);
                tendonProperty.Diameter = Adaptor.toDouble(tableRow["Diameter"]);
                tendonProperty.Area = Adaptor.toDouble(tableRow["Area"]);
                tendonProperty.ModelingOption = Enums.EnumLibrary.ConvertStringToEnumByDescription<eTendonModelingOption>(tableRow["ModelOpt"]);

                tendonProperty.Modifiers.BendingM2 = Adaptor.toDouble(tableRow["I2Mod"]);
                tendonProperty.Modifiers.BendingM3 = Adaptor.toDouble(tableRow["I3Mod"]);
                tendonProperty.Modifiers.Torsion = Adaptor.toDouble(tableRow["JMod"]);
                tendonProperty.Modifiers.ShearV2 = Adaptor.toDouble(tableRow["A2Mod"]);
                tendonProperty.Modifiers.ShearV3 = Adaptor.toDouble(tableRow["A3Mod"]);
                tendonProperty.Modifiers.CrossSectionalArea = Adaptor.toDouble(tableRow["AMod"]);
                tendonProperty.Modifiers.MassModifier = Adaptor.toDouble(tableRow["MMod"]);
                tendonProperty.Modifiers.WeightModifier = Adaptor.toDouble(tableRow["WMod"]);
            }
        }

        /// <summary>
        /// Sets the connectivity tendon.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_TENDON(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Tendon tendon = model.Structure.Tendons[tableRow["Tendon"]];
                if (tableRow.ContainsKey("GUID")) tendon.GUID = tableRow["GUID"];
                tendon.PointNames.Add(tableRow["JointI"]);
                tendon.PointNames.Add(tableRow["JointJ"]);
            }
        }

        /// <summary>
        /// Sets the tendon section assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setTENDON_SECTION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Tendon tendon = model.Structure.Tendons[tableRow["Tendon"]];
                tendon.SectionName = tableRow["TendonSect"];
            }
        }

        /// <summary>
        /// Sets the tendon layout data 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setTENDON_LAYOUT_DATA_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Tendon tendon = model.Structure.Tendons[tableRow["Tendon"]];
                tendon.MaxDiscretizationLength = Adaptor.toDouble(tableRow["MaxDiscLen"]);

                string loadGroupName = tableRow["LoadGroup"];
                tendon.LoadGroupName = loadGroupName;
                tendon.LoadGroup = model.Groupings.Groups[loadGroupName];
            }
        }

        /// <summary>
        /// Sets the tendon layout data 02 segments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setTENDON_LAYOUT_DATA_02_SEGMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Tendon tendon = model.Structure.Tendons[tableRow["Tendon"]];

                eTendonGeometryDefinition segmentType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eTendonGeometryDefinition>(tableRow["SegType"]);
                Coordinate3DCartesian coordinate = new Coordinate3DCartesian
                {
                    X = Adaptor.toDouble(tableRow["XGlobal"]),
                    Y = Adaptor.toDouble(tableRow["YGlobal"]),
                    Z = Adaptor.toDouble(tableRow["ZGlobal"])
                };
                tendon.Segments.Add(new Tuple<eTendonGeometryDefinition, Coordinate3DCartesian>(segmentType, coordinate));
            }
        }
    }
}
