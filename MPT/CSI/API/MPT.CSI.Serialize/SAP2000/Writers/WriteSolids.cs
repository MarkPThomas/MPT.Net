using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteSolids
    {
        internal static void DefineSolidProperties(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.SOLID_PROPERTY_DEFINITIONS, setSOLID_PROPERTY_DEFINITIONS);
        }

        internal static void DefineSolids(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.CONNECTIVITY_SOLID, setCONNECTIVITY_SOLID);
        }

        internal static void AssignSolids(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.SOLID_PROPERTY_ASSIGNMENTS, setSOLID_PROPERTY_ASSIGNMENTS);
            //writer.WriteSingleTable(SAP2000Tables.SOLID_EDGE_CONSTRAINT_ASSIGNMENTS, setSOLID_EDGE_CONSTRAINT_ASSIGNMENTS);
        }

        internal static void LoadSolids(SAP2000Writer writer)
        {
            // TODO: Add solid loads
        }

        /// <summary>
        /// Sets the solid property definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setSOLID_PROPERTY_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Solid solid in model.Structure.Solids)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"SolidProp", Adaptor.ToStringEntryLimited(solid.Section.Name)},
                    {"Color", Adaptor.ToStringEntryLimited(solid.Section.ColorName)},
                    {"Material", Adaptor.ToStringEntryLimited(solid.Section.MaterialName)},
                    {"InComp", Adaptor.toYesNo(solid.Section.IncompatibleModes)},
                    {"MatAngleA", Adaptor.fromDouble(solid.Section.MaterialAngleA)},
                    {"MatAngleB", Adaptor.fromDouble(solid.Section.MaterialAngleB)},
                    {"MatAngleC", Adaptor.fromDouble(solid.Section.MaterialAngleC)}
                };
                if (!string.IsNullOrEmpty(solid.Section.Notes))
                {
                    tableRow["Notes"] = Adaptor.ToStringEntryLimited(solid.Section.Notes);
                }

                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Sets the connectivity solid.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_SOLID(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Solid solid in model.Structure.Solids)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"Solid", Adaptor.ToStringEntryLimited(solid.Name)},
                    {"Joint1", Adaptor.ToStringEntryLimited(solid.PointNames[0])},
                    {"Joint2", Adaptor.ToStringEntryLimited(solid.PointNames[1])},
                    {"Joint3", Adaptor.ToStringEntryLimited(solid.PointNames[2])},
                    {"Joint4", Adaptor.ToStringEntryLimited(solid.PointNames[3])},
                    {"Joint5", Adaptor.ToStringEntryLimited(solid.PointNames[4])},
                    {"Joint6", Adaptor.ToStringEntryLimited(solid.PointNames[5])},
                    {"Joint7", Adaptor.ToStringEntryLimited(solid.PointNames[6])},
                    {"Joint8", Adaptor.ToStringEntryLimited(solid.PointNames[7])},
                };
                if (!string.IsNullOrEmpty(solid.GUID))
                {
                    tableRow["GUID"] = Adaptor.ToStringEntryLimited(solid.GUID);
                }

                table.Add(tableRow);
            }
        }


        /// <summary>
        /// Sets the solid property assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setSOLID_PROPERTY_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Solid solid in model.Structure.Solids)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Solid", Adaptor.ToStringEntryLimited(solid.Name) },
                        { "SolidProp", Adaptor.ToStringEntryLimited(solid.SectionName) }
                    });
            }
        }

        // TABLE:  "SOLID EDGE CONSTRAINT ASSIGNMENTS"
        /// <summary>
        /// Sets the solid edge constraint assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setSOLID_EDGE_CONSTRAINT_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Solid solid = model.Structure.Solids[tableRow["Solid"]];
                throw new NotImplementedException();
            }
        }
    }
}
