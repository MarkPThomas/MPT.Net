using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Loads.Assignments;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteAreas
    {
        internal static void DefineAreas(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.CONNECTIVITY_AREA, setCONNECTIVITY_AREA);
        }

        internal static void AssignAreas(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.AREA_SECTION_ASSIGNMENTS, setAREA_SECTION_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.AREA_STIFFNESS_MODIFIERS, SetAREA_STIFFNESS_MODIFIERS);
            //TODO: Add more, such as auto meshing, edge constraints
        }

        internal static void LoadAreas(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.AREA_LOADS_UNIFORM, setAREA_LOADS_UNIFORM);
            writer.WriteSingleTable(SAP2000Tables.AREA_LOADS_WIND_PRESSURE_COEFFICIENTS, setAREA_LOADS_WIND_PRESSURE_COEFFICIENTS);
            // TODO: Add more area loads
        }

        /// <summary>
        /// Sets the connectivity area.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_AREA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Area area in model.Structure.Areas)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Area", Adaptor.ToStringEntryLimited(area.Name) },
                    { "Joint1", Adaptor.ToStringEntryLimited(area.PointNames[0]) },
                    { "Joint2", Adaptor.ToStringEntryLimited(area.PointNames[1]) }
                };
                if (!string.IsNullOrEmpty(area.GUID))
                {
                    tableRow["GUID"] = Adaptor.ToStringEntryLimited(area.GUID);
                }

                if (area.PointNames.Count > 2)
                {
                    tableRow["Joint3"] = Adaptor.ToStringEntryLimited(area.PointNames[2]);
                }
                if (area.PointNames.Count > 3)
                {
                    tableRow["Joint4"] = Adaptor.ToStringEntryLimited(area.PointNames[3]);
                }

                table.Add(tableRow);
            }
        }


        /// <summary>
        /// Sets the area section assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setAREA_SECTION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Area area in model.Structure.Areas)
            {
                if (area.MaterialOverwrite == null) continue;
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Area", Adaptor.ToStringEntryLimited(area.Name) },
                        { "Section", Adaptor.ToStringEntryLimited(area.SectionName) },
                        { "MatProp", Adaptor.ToStringEntryLimited(area.MaterialOverwriteName) },
                    });
            }
        }


        /// <summary>
        /// Sets the area loads uniform.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setAREA_LOADS_UNIFORM(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Area area in model.Structure.Areas)
            {
                foreach (AreaLoadUniform uniformLoad in area.UniformLoads)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Area", Adaptor.ToStringEntryLimited(area.Name) },
                            { "LoadPat", Adaptor.ToStringEntryLimited(uniformLoad.LoadPattern) },
                            { "CoordSys", Adaptor.ToStringEntryLimited(uniformLoad.CoordinateSystem) },
                            { "Dir", Adaptor.fromEnum(uniformLoad.LoadDirection) },
                            { "UnifLoad", Adaptor.fromDouble(uniformLoad.Value) }
                        });
                }
            }
        }


        /// <summary>
        /// Sets the area loads wind pressure coefficients.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setAREA_LOADS_WIND_PRESSURE_COEFFICIENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Area area in model.Structure.Areas)
            {
                foreach (AreaLoadWindPressure windPressureLoad in area.WindPressureLoads)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Area", Adaptor.ToStringEntryLimited(area.Name) },
                            { "LoadPat", Adaptor.ToStringEntryLimited(windPressureLoad.LoadPattern) },
                            { "Cp", Adaptor.fromDouble(windPressureLoad.PressureCoefficient) },
                            { "Windward", Adaptor.toYesNo(windPressureLoad.WindPressureType == eWindPressureApplication.Windward) }
                        });
                }
            }
        }

        /// <summary>
        /// Sets the area stiffness modifiers.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAREA_STIFFNESS_MODIFIERS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Area area in model.Structure.Areas)
            {
                if (area.AreaModifier == null) continue;
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Area", Adaptor.ToStringEntryLimited(area.Name) },
                        { "f11", Adaptor.fromDouble(area.AreaModifier.MembraneF11) },
                        { "f22", Adaptor.fromDouble(area.AreaModifier.MembraneF22) },
                        { "f12", Adaptor.fromDouble(area.AreaModifier.MembraneF12) },
                        { "m11", Adaptor.fromDouble(area.AreaModifier.BendingM11) },
                        { "m22", Adaptor.fromDouble(area.AreaModifier.BendingM22) },
                        { "m12", Adaptor.fromDouble(area.AreaModifier.BendingM12) },
                        { "v13", Adaptor.fromDouble(area.AreaModifier.ShearV13) },
                        { "v23", Adaptor.fromDouble(area.AreaModifier.ShearV23) },
                        { "MassMod", Adaptor.fromDouble(area.AreaModifier.MassModifier) },
                        { "WeightMod", Adaptor.fromDouble(area.AreaModifier.WeightModifier) }
                    });
            }
        }
    }
}
