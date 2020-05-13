using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadAreas
    {
        internal static void DefineAreas(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.CONNECTIVITY_AREA, setCONNECTIVITY_AREA);
        }

        internal static void AssignAreas(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.AREA_SECTION_ASSIGNMENTS, setAREA_SECTION_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.AREA_STIFFNESS_MODIFIERS, SetAREA_STIFFNESS_MODIFIERS);
            //TODO: Add more, such as auto meshing, edge constraints
        }

        internal static void LoadAreas(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.AREA_LOADS_UNIFORM, setAREA_LOADS_UNIFORM);
            reader.ReadSingleTable(SAP2000Tables.AREA_LOADS_WIND_PRESSURE_COEFFICIENTS, setAREA_LOADS_WIND_PRESSURE_COEFFICIENTS);
            // TODO: Add more area loads
        }

        /// <summary>
        /// Sets the connectivity area.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_AREA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                string name = tableRow["Area"];
                Area area = model.Structure.Areas.FillItem(name);
                if (tableRow.ContainsKey("GUID")) area.GUID = tableRow["GUID"];
                area.PointNames.Add(tableRow["Joint1"]);
                area.PointNames.Add(tableRow["Joint2"]);
                if (tableRow.ContainsKey("Joint3")) area.PointNames.Add(tableRow["Joint3"]);
                if (tableRow.ContainsKey("Joint4")) area.PointNames.Add(tableRow["Joint4"]);
            }
        }


        /// <summary>
        /// Sets the area section assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setAREA_SECTION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Area area = model.Structure.Areas[tableRow["Area"]];
                area.SectionName = tableRow["Section"];
                area.AddMaterialOverwrite(model.Components.Materials.FillItem(tableRow["MatProp"]));
            }
        }

        /// <summary>
        /// Sets the area stiffness modifiers.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAREA_STIFFNESS_MODIFIERS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Area area = model.Structure.Areas[tableRow["Area"]];
                area.AreaModifier.MembraneF11 = Adaptor.toDouble(tableRow["f11"]);
                area.AreaModifier.MembraneF22 = Adaptor.toDouble(tableRow["f22"]);
                area.AreaModifier.MembraneF12 = Adaptor.toDouble(tableRow["f12"]);
                area.AreaModifier.BendingM11 = Adaptor.toDouble(tableRow["m11"]);
                area.AreaModifier.BendingM22 = Adaptor.toDouble(tableRow["m22"]);
                area.AreaModifier.BendingM12 = Adaptor.toDouble(tableRow["m12"]);
                area.AreaModifier.ShearV13 = Adaptor.toDouble(tableRow["v13"]);
                area.AreaModifier.ShearV23 = Adaptor.toDouble(tableRow["v23"]);
                area.AreaModifier.MassModifier = Adaptor.toDouble(tableRow["MassMod"]);
                area.AreaModifier.WeightModifier = Adaptor.toDouble(tableRow["WeightMod"]);
            }
        }


        /// <summary>
        /// Sets the area loads uniform.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setAREA_LOADS_UNIFORM(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Area area = model.Structure.Areas[tableRow["Area"]];
                AreaLoadUniform load = new AreaLoadUniform
                {
                    LoadPattern = tableRow["LoadPat"],
                    CoordinateSystem = tableRow["CoordSys"],
                    LoadDirection = Enums.EnumLibrary.ConvertStringToEnumByDescription<eLoadDirection>(tableRow["Dir"]),
                    Value = Adaptor.toDouble(tableRow["UnifLoad"])
                };
                area.AddLoadUniform(load);
            }
        }


        /// <summary>
        /// Sets the area loads wind pressure coefficients.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setAREA_LOADS_WIND_PRESSURE_COEFFICIENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Area area = model.Structure.Areas[tableRow["Area"]];
                AreaLoadWindPressure windPressure = new AreaLoadWindPressure
                {
                    LoadPattern = tableRow["LoadPat"],
                    PressureCoefficient = Adaptor.toDouble(tableRow["Cp"]),
                    WindPressureType = Adaptor.fromYesNo(tableRow["Windward"])
                        ? eWindPressureApplication.Windward
                        : eWindPressureApplication.Other,
                };
                area.SetLoadWindPressure(windPressure);
            }
        }
    }
}
