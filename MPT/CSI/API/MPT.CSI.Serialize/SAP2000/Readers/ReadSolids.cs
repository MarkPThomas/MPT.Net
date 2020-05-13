using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadSolids
    {
        internal static void DefineSolidProperties(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.SOLID_PROPERTY_DEFINITIONS, setSOLID_PROPERTY_DEFINITIONS);
        }

        internal static void DefineSolids(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.CONNECTIVITY_SOLID, setCONNECTIVITY_SOLID);
        }

        internal static void AssignSolids(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.SOLID_PROPERTY_ASSIGNMENTS, setSOLID_PROPERTY_ASSIGNMENTS);
            //reader.ReadSingleTable(SAP2000Tables.SOLID_EDGE_CONSTRAINT_ASSIGNMENTS, setSOLID_EDGE_CONSTRAINT_ASSIGNMENTS);
        }

        internal static void LoadSolids(SAP2000Reader reader)
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
            foreach (Dictionary<string, string> tableRow in table)
            {
                Solid solid = model.Structure.Solids.FillItem(tableRow["SolidProp"]);
                if (tableRow.ContainsKey("Notes")) solid.Section.Notes = tableRow["Notes"];
                solid.Section.ColorName = tableRow["Color"];
                solid.Section.MaterialName = tableRow["Material"];
                solid.Section.IncompatibleModes = Adaptor.fromYesNo(tableRow["InComp"]);
                solid.Section.MaterialAngleA = Adaptor.toDouble(tableRow["MatAngleA"]);
                solid.Section.MaterialAngleB = Adaptor.toDouble(tableRow["MatAngleB"]);
                solid.Section.MaterialAngleC = Adaptor.toDouble(tableRow["MatAngleC"]);
            }
        }

        /// <summary>
        /// Sets the connectivity solid.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_SOLID(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Solid solid = model.Structure.Solids[tableRow["Solid"]];
                if (tableRow.ContainsKey("GUID")) solid.GUID = tableRow["GUID"];
                solid.PointNames.Add(tableRow["Joint1"]);
                solid.PointNames.Add(tableRow["Joint2"]);
                solid.PointNames.Add(tableRow["Joint3"]);
                solid.PointNames.Add(tableRow["Joint4"]);
                solid.PointNames.Add(tableRow["Joint5"]);
                solid.PointNames.Add(tableRow["Joint6"]);
                solid.PointNames.Add(tableRow["Joint7"]);
                solid.PointNames.Add(tableRow["Joint8"]);
            }
        }


        /// <summary>
        /// Sets the solid property assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setSOLID_PROPERTY_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Solid solid = model.Structure.Solids[tableRow["Solid"]];
                solid.SectionName = tableRow["SolidProp"];
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
