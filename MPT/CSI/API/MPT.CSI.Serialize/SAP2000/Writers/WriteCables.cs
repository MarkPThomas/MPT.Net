using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteCables
    {
        internal static void DefineCableProperties(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.CABLE_SECTION_DEFINITIONS, setCABLE_SECTION_DEFINITIONS);
        }

        internal static void DefineCables(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.CONNECTIVITY_CABLE, setCONNECTIVITY_CABLE);
        }

        internal static void AssignCables(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.CABLE_SECTION_ASSIGNMENTS, setCABLE_SECTION_ASSIGNMENTS);
            writer.WriteSingleTable(SAP2000Tables.CABLE_SHAPE_DATA, setCABLE_SHAPE_DATA);
            writer.WriteSingleTable(SAP2000Tables.CABLE_OUTPUT_STATION_ASSIGNMENTS, setCABLE_OUTPUT_STATION_ASSIGNMENTS);
        }

        internal static void LoadCables(SAP2000Writer writer)
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
            foreach (CableProperty cableProperty in model.Components.CableProperties)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "CableSect", Adaptor.ToStringEntryLimited(cableProperty.Name) },
                    { "Color", Adaptor.ToStringEntryLimited(cableProperty.ColorName) },
                    { "Material", Adaptor.ToStringEntryLimited(cableProperty.MaterialName) },
                    { "Specify", Adaptor.fromEnum(cableProperty.CableSpecification) },
                    { "Diameter", Adaptor.fromDouble(cableProperty.Diameter) },
                    { "Area", Adaptor.fromDouble(cableProperty.Area) }
                };

                if (!string.IsNullOrEmpty(cableProperty.Notes))
                {
                    tableRow["Notes"] = Adaptor.ToStringEntryLimited(cableProperty.Notes);
                }

                if (cableProperty.Modifiers != null)
                {
                    tableRow["I2Mod"] = Adaptor.fromDouble(cableProperty.Modifiers.BendingM2);
                    tableRow["I3Mod"] = Adaptor.fromDouble(cableProperty.Modifiers.BendingM3);
                    tableRow["JMod"] = Adaptor.fromDouble(cableProperty.Modifiers.Torsion);
                    tableRow["A2Mod"] = Adaptor.fromDouble(cableProperty.Modifiers.ShearV2);
                    tableRow["A3Mod"] = Adaptor.fromDouble(cableProperty.Modifiers.ShearV3);
                    tableRow["AMod"] = Adaptor.fromDouble(cableProperty.Modifiers.CrossSectionalArea);
                    tableRow["MMod"] = Adaptor.fromDouble(cableProperty.Modifiers.MassModifier);
                    tableRow["WMod"] = Adaptor.fromDouble(cableProperty.Modifiers.WeightModifier);
                }

                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Sets the connectivity cable.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_CABLE(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Cable cable in model.Structure.Cables)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Cable", Adaptor.ToStringEntryLimited(cable.Name) },
                    { "JointI", Adaptor.ToStringEntryLimited(cable.PointNames[0]) },
                    { "JointJ", Adaptor.ToStringEntryLimited(cable.PointNames[1]) },
                };

                if (!string.IsNullOrEmpty(cable.GUID))
                {
                    tableRow["GUID"] = Adaptor.ToStringEntryLimited(cable.GUID);
                }
                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Sets the cable section assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCABLE_SECTION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Cable cable in model.Structure.Cables)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Cable", Adaptor.ToStringEntryLimited(cable.Name) },
                        { "Section", Adaptor.ToStringEntryLimited(cable.SectionName) },
                        { "MatProp", Adaptor.ToStringEntryLimited(cable.MaterialOverwrite.Name) },
                    });
            }
        }


        /// <summary>
        /// Sets the cable shape data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCABLE_SHAPE_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Cable cable in model.Structure.Cables)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Cable", Adaptor.ToStringEntryLimited(cable.Name) },
                        { "CableType", Adaptor.fromEnum(cable.CableGeometryDefinition) },
                        { "NumSegs", Adaptor.fromInteger(cable.NumberOfSegments) },
                        { "TensionI", Adaptor.fromDouble(cable.TensionI) },
                        { "TensionJ", Adaptor.fromDouble(cable.TensionJ) },
                        { "TensionHorz", Adaptor.fromDouble(cable.HorizontalTension) },
                        { "SagMax", Adaptor.fromDouble(cable.MaximumVerticalSag) },
                        { "SagLow", Adaptor.fromDouble(cable.LowPointVerticalSag) },
                        { "UnDefLength", Adaptor.fromDouble(cable.UndeformedLength) },
                        { "UnDefRelLen", Adaptor.fromDouble(cable.UndeformedRelativeLength) },
                        { "AddedWt", Adaptor.fromDouble(cable.AddedWeight) },
                        { "ProjLoad", Adaptor.fromDouble(cable.ProjectedUniformGravityLoad) },
                        { "UseDefGeom", Adaptor.toYesNo(cable.UseDeformedGeometry) },
                        { "UseFrames", Adaptor.toYesNo(cable.ModelCableUsingStraightFrameObjects) }
                    });
            }
        }

        /// <summary>
        /// Sets the cable output station assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCABLE_OUTPUT_STATION_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Cable cable in model.Structure.Cables)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Cable", Adaptor.ToStringEntryLimited(cable.Name) },
                    { "StationType", Adaptor.ToStringEntryLimited(convertfromOutputStationType(cable.OutputStations.OutputStationType)) },
                    { "AddAtPtLoad", Adaptor.toYesNo(cable.OutputStations.NoOutputAndDesignAtPointLoads) },
                    { "AddAtElmInt", Adaptor.toYesNo(cable.OutputStations.NoOutputAndDesignAtElementIntersections) }
                };

                if (cable.OutputStations.MinStationNumber > 0)
                {
                    tableRow["MinNumSta"] = Adaptor.fromInteger(cable.OutputStations.MinStationNumber);
                }

                if (cable.OutputStations.MaxStationSpacing > 0)
                {
                    tableRow["MaxStaSpcg"] = Adaptor.fromDouble(cable.OutputStations.MaxStationSpacing);
                }

                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Converts the type of to output station.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eOutputStationType.</returns>
        private static string convertfromOutputStationType(eOutputStationType value)
        {
            switch (value)
            {
                case eOutputStationType.MinStations:
                    return "MinNumSta";
                case eOutputStationType.MaxSpacing:
                    return "MaxStaSpcg";
                default:
                    return "MinNumSta";
            }
        }
    }
}
