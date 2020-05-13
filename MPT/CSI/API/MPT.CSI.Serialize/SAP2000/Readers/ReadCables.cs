using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadCables
    {
        internal static void DefineCableProperties(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.CABLE_SECTION_DEFINITIONS, setCABLE_SECTION_DEFINITIONS);
        }

        internal static void DefineCables(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.CONNECTIVITY_CABLE, setCONNECTIVITY_CABLE);
        }

        internal static void AssignCables(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.CABLE_SECTION_ASSIGNMENTS, setCABLE_SECTION_ASSIGNMENTS);
            reader.ReadSingleTable(SAP2000Tables.CABLE_SHAPE_DATA, setCABLE_SHAPE_DATA);
            reader.ReadSingleTable(SAP2000Tables.CABLE_OUTPUT_STATION_ASSIGNMENTS, setCABLE_OUTPUT_STATION_ASSIGNMENTS);
        }

        internal static void LoadCables(SAP2000Reader reader)
        {
            // TODO: Add cable loads
        }

        /// <summary>
        /// Sets the cable section definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCABLE_SECTION_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                CableProperty cableProperty = model.Components.CableProperties.FillItem(tableRow["CableSect"]);
                cableProperty.ColorName = tableRow["Color"];
                if (tableRow.ContainsKey("Notes")) cableProperty.Notes = tableRow["Notes"];
                cableProperty.MaterialName = tableRow["Material"];
                cableProperty.CableSpecification = Enums.EnumLibrary.ConvertStringToEnumByDescription<eCableSpecification>(tableRow["Specify"]);
                cableProperty.Diameter = Adaptor.toDouble(tableRow["Diameter"]);
                cableProperty.Area = Adaptor.toDouble(tableRow["Area"]);

                cableProperty.Modifiers.BendingM2 = Adaptor.toDouble(tableRow["I2Mod"]);
                cableProperty.Modifiers.BendingM3 = Adaptor.toDouble(tableRow["I3Mod"]);
                cableProperty.Modifiers.Torsion = Adaptor.toDouble(tableRow["JMod"]);
                cableProperty.Modifiers.ShearV2 = Adaptor.toDouble(tableRow["A2Mod"]);
                cableProperty.Modifiers.ShearV3 = Adaptor.toDouble(tableRow["A3Mod"]);
                cableProperty.Modifiers.CrossSectionalArea = Adaptor.toDouble(tableRow["AMod"]);
                cableProperty.Modifiers.MassModifier = Adaptor.toDouble(tableRow["MMod"]);
                cableProperty.Modifiers.WeightModifier = Adaptor.toDouble(tableRow["WMod"]);
            }
        }

        /// <summary>
        /// Sets the connectivity cable.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_CABLE(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Cable cable = model.Structure.Cables[tableRow["Cable"]];
                if (tableRow.ContainsKey("GUID")) cable.GUID = tableRow["GUID"];
                cable.PointNames.Add(tableRow["JointI"]);
                cable.PointNames.Add(tableRow["JointJ"]);
            }
        }

        /// <summary>
        /// Sets the cable section assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCABLE_SECTION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Cable cable = model.Structure.Cables[tableRow["Cable"]];
                cable.SectionName = tableRow["Section"];
                cable.AddMaterialOverwrite(model.Components.Materials.FillItem(tableRow["MatProp"]));
            }
        }


        /// <summary>
        /// Sets the cable shape data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCABLE_SHAPE_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Cable cable = model.Structure.Cables[tableRow["Cable"]];
                cable.CableGeometryDefinition = Enums.EnumLibrary.ConvertStringToEnumByDescription<eCableGeometryDefinition>(tableRow["CableType"]);
                cable.NumberOfSegments = Adaptor.toInteger(tableRow["NumSegs"]);
                cable.TensionI = Adaptor.toDouble(tableRow["TensionI"]);
                cable.TensionJ = Adaptor.toDouble(tableRow["TensionJ"]);
                cable.HorizontalTension = Adaptor.toDouble(tableRow["TensionHorz"]);
                cable.MaximumVerticalSag = Adaptor.toDouble(tableRow["SagMax"]);
                cable.LowPointVerticalSag = Adaptor.toDouble(tableRow["SagLow"]);
                cable.UndeformedLength = Adaptor.toDouble(tableRow["UnDefLength"]);
                cable.UndeformedRelativeLength = Adaptor.toDouble(tableRow["UnDefRelLen"]);
                cable.AddedWeight = Adaptor.toDouble(tableRow["AddedWt"]);
                cable.ProjectedUniformGravityLoad = Adaptor.toDouble(tableRow["ProjLoad"]);
                cable.UseDeformedGeometry = Adaptor.fromYesNo(tableRow["UseDefGeom"]);
                cable.ModelCableUsingStraightFrameObjects = Adaptor.fromYesNo(tableRow["UseFrames"]);
            }
        }

        /// <summary>
        /// Sets the cable output station assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCABLE_OUTPUT_STATION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Cable cable = model.Structure.Cables[tableRow["Cable"]];
                cable.OutputStations.OutputStationType = convertToOutputStationType(tableRow["StationType"]);
                if (tableRow.ContainsKey("MinNumSta")) cable.OutputStations.MinStationNumber = Adaptor.toInteger(tableRow["MinNumSta"]);
                if (tableRow.ContainsKey("MaxStaSpcg")) cable.OutputStations.MaxStationSpacing = Adaptor.toDouble(tableRow["MaxStaSpcg"]);
                cable.OutputStations.NoOutputAndDesignAtPointLoads = Adaptor.fromYesNo(tableRow["AddAtPtLoad"]);
                cable.OutputStations.NoOutputAndDesignAtElementIntersections = Adaptor.fromYesNo(tableRow["AddAtElmInt"]);
            }
        }

        /// <summary>
        /// Converts the type of to output station.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eOutputStationType.</returns>
        private static eOutputStationType convertToOutputStationType(string value)
        {
            switch (value)
            {
                case "MinNumSta":
                    return eOutputStationType.MinStations;
                case "MaxStaSpcg":
                    return eOutputStationType.MaxSpacing;
                default:
                    return eOutputStationType.MinStations;
            }
        }
    }
}
