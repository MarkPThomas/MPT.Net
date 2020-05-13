using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteTendons
    {
        internal static void DefineTendonProperties(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.TENDON_SECTION_DEFINITIONS, setTENDON_SECTION_DEFINITIONS);
        }

        internal static void DefineTendons(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.CONNECTIVITY_TENDON, setCONNECTIVITY_TENDON);
        }

        internal static void AssignTendons(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.TENDON_SECTION_ASSIGNMENTS, setTENDON_SECTION_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.TENDON_LAYOUT_DATA_01_GENERAL, setTENDON_LAYOUT_DATA_01_GENERAL);
            writer.WriteSingleTable(SAP2000Tables.TENDON_LAYOUT_DATA_02_SEGMENTS, setTENDON_LAYOUT_DATA_02_SEGMENTS);
        }

        internal static void LoadTendons(SAP2000Writer writer)
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
            foreach (TendonProperty tendonProperty in model.Components.TendonProperties)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"TendonSect",  Adaptor.ToStringEntryLimited(tendonProperty.Name)},
                    {"Color",  Adaptor.ToStringEntryLimited(tendonProperty.ColorName)}
                };

                if (!string.IsNullOrEmpty(tendonProperty.Notes))
                {
                    tableRow["Notes"] = Adaptor.ToStringEntryLimited(tendonProperty.Notes);
                }

                tableRow["Material"] = Adaptor.ToStringEntryLimited(tendonProperty.MaterialName);
                tableRow["Specify"] = Adaptor.fromEnum(tendonProperty.TendonSpecification);
                tableRow["Diameter"] = Adaptor.fromDouble(tendonProperty.Diameter);
                tableRow["Area"] = Adaptor.fromDouble(tendonProperty.Area);
                tableRow["ModelOpt"] = Adaptor.fromEnum(tendonProperty.ModelingOption);

                tableRow["I2Mod"] = Adaptor.fromDouble(tendonProperty.Modifiers.BendingM2);
                tableRow["I3Mod"] = Adaptor.fromDouble(tendonProperty.Modifiers.BendingM3);
                tableRow["JMod"] = Adaptor.fromDouble(tendonProperty.Modifiers.Torsion);
                tableRow["A2Mod"] = Adaptor.fromDouble(tendonProperty.Modifiers.ShearV2);
                tableRow["A3Mod"] = Adaptor.fromDouble(tendonProperty.Modifiers.ShearV3);
                tableRow["AMod"] = Adaptor.fromDouble(tendonProperty.Modifiers.CrossSectionalArea);
                tableRow["MMod"] = Adaptor.fromDouble(tendonProperty.Modifiers.MassModifier);
                tableRow["WMod"] = Adaptor.fromDouble(tendonProperty.Modifiers.WeightModifier);

                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Sets the connectivity tendon.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_TENDON(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Tendon tendon in model.Structure.Tendons)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"Tendon",  Adaptor.ToStringEntryLimited(tendon.Name)},
                    {"JointI",  Adaptor.ToStringEntryLimited(tendon.PointNames[0])},
                    {"JointJ",  Adaptor.ToStringEntryLimited(tendon.PointNames[1])}
                };
                if (!string.IsNullOrEmpty(tendon.GUID))
                {
                    tableRow["GUID"] = Adaptor.ToStringEntryLimited(tendon.GUID);
                }
                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Sets the tendon section assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setTENDON_SECTION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Tendon tendon in model.Structure.Tendons)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Tendon",  Adaptor.ToStringEntryLimited(tendon.Name)},
                        {"TendonSect",  Adaptor.ToStringEntryLimited(tendon.SectionName)},
                    });
            }
        }

        /// <summary>
        /// Sets the tendon layout data 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setTENDON_LAYOUT_DATA_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Tendon tendon in model.Structure.Tendons)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"Tendon",  Adaptor.ToStringEntryLimited(tendon.Name)},
                        {"MaxDiscLen",  Adaptor.fromDouble(tendon.MaxDiscretizationLength)},
                        {"LoadGroup",  Adaptor.ToStringEntryLimited(tendon.LoadGroupName)}
                    });
            }
        }

        /// <summary>
        /// Sets the tendon layout data 02 segments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setTENDON_LAYOUT_DATA_02_SEGMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Tendon tendon in model.Structure.Tendons)
            {
                foreach (Tuple<eTendonGeometryDefinition, Coordinate3DCartesian> tendonSegment in tendon.Segments)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            {"Tendon",  Adaptor.ToStringEntryLimited(tendon.Name)},
                            {"SegType",  Adaptor.fromEnum(tendonSegment.Item1)},
                            {"XGlobal",  Adaptor.fromDouble(tendonSegment.Item2.X)},
                            {"YGlobal",  Adaptor.fromDouble(tendonSegment.Item2.Y)},
                            {"ZGlobal",  Adaptor.fromDouble(tendonSegment.Item2.Z)},
                        });
                }
            }
        }
    }
}
