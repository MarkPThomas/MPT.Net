using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Materials;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadAreaSections
    {
        internal static void DefineAreaSections(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.AREA_SECTION_PROPERTIES, SetAREA_SECTION_PROPERTIES);
            reader.ReadSingleTable(SAP2000Tables.AREA_SECTION_PROPERTY_DESIGN_PARAMETERS, SetAREA_SECTION_PROPERTY_DESIGN_PARAMETERS);
            reader.ReadSingleTable(SAP2000Tables.AREA_SECTION_PROPERTY_LAYERS, SetAREA_SECTION_PROPERTY_LAYERS);
            //reader.ReadSingleTable(SAP2000Tables.AREA_SECTION_PROPERTY_TIME_DEPENDENT, ReadAreaSections.SetAREA_SECTION_PROPERTY_TIME_DEPENDENT);
        }

        /// <summary>
        /// Sets the area section properties.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAREA_SECTION_PROPERTIES(Model model, List<Dictionary<string, string>> table)
        {
            // TODO: Incomplete
            foreach (Dictionary<string, string> tableRow in table)
            {
                AreaSections.AreaSectionType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eAreaSectionType>(tableRow["AreaType"]);
                AreaSection areaSection = model.Components.AreaSections.FillItem(tableRow["Section"]);
                areaSection.MaterialName = tableRow["Material"];
                areaSection.ColorName = tableRow["Color"];
                if (tableRow.ContainsKey("Notes")) areaSection.Notes = tableRow["Notes"];
                if (tableRow.ContainsKey("GUID")) areaSection.GUID = tableRow["GUID"];
                areaSection.Modifiers = new AreaModifier
                {
                    MembraneF11 = Adaptor.toDouble(tableRow["F11Mod"]),
                    MembraneF22 = Adaptor.toDouble(tableRow["F22Mod"]),
                    MembraneF12 = Adaptor.toDouble(tableRow["F12Mod"]),
                    BendingM11 = Adaptor.toDouble(tableRow["M11Mod"]),
                    BendingM22 = Adaptor.toDouble(tableRow["M22Mod"]),
                    BendingM12 = Adaptor.toDouble(tableRow["M12Mod"]),
                    ShearV13 = Adaptor.toDouble(tableRow["V13Mod"]),
                    ShearV23 = Adaptor.toDouble(tableRow["V23Mod"]),
                    MassModifier = Adaptor.toDouble(tableRow["MMod"]),
                    WeightModifier = Adaptor.toDouble(tableRow["WMod"])
                };
                areaSection.AreaType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eAreaSectionType>(tableRow["AreaType"]);

                switch (areaSection)
                {
                    case Shell areaShell:
                        areaShell.SectionProperties.ShellType = convertToShellType(tableRow["Type"]);
                        areaShell.SectionProperties.MaterialAngle = Adaptor.toDouble(tableRow["MatAngle"]);
                        areaShell.SectionProperties.MembraneThickness = Adaptor.toDouble(tableRow["Thickness"]);
                        areaShell.SectionProperties.BendingThickness = Adaptor.toDouble(tableRow["BendThick"]);
                        if (tableRow.ContainsKey("DrillDOF")) areaShell.SectionProperties.IncludeDrillingDOF = Adaptor.fromYesNo(tableRow["DrillDOF"]);
                        break;
                    case Plane areaPlane:
                        areaPlane.SectionProperties.PlaneType = convertToPlaneType(tableRow["Type"]);
                        areaPlane.SectionProperties.MaterialAngle = Adaptor.toDouble(tableRow["MatAngle"]);
                        areaPlane.SectionProperties.Thickness = Adaptor.toDouble(tableRow["Thickness"]);
                        areaPlane.SectionProperties.IncompatibleModes = Adaptor.fromYesNo(tableRow["InComp"]);
                        break;
                    case ASolid areaASolid:
                        areaASolid.SectionProperties.MaterialAngle = Adaptor.toDouble(tableRow["MatAngle"]);
                        areaASolid.SectionProperties.ArcAngle = Adaptor.toDouble(tableRow["Arc"]);
                        areaASolid.SectionProperties.IncompatibleBendingModes = Adaptor.fromYesNo(tableRow["InComp"]);
                        areaASolid.SectionProperties.CoordinateSystem = tableRow["CoordSys"];
                        break;
                }
            }
        }

        /// <summary>
        /// Converts the type of to shell.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eShellType.</returns>
        private static eShellType convertToShellType(string value)
        {
            switch (value)
            {
                case "Shell-Thin":
                    return eShellType.ShellThin;
                case "Shell-Thick":
                    return eShellType.ShellThick;
                case "Shell-Layered":
                    return eShellType.ShellLayered;
                case "Plate-Thin":
                    return eShellType.PlateThin;
                case "Plate-Thick":
                    return eShellType.PlateThick;
                case "Membrane":
                    return eShellType.Membrane;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Converts the type of to plane.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ePlaneType.</returns>
        private static ePlaneType convertToPlaneType(string value)
        {
            switch (value)
            {
                case "Plane-Stress":
                    return ePlaneType.PlaneStress;
                case "Plane-Strain":
                    return ePlaneType.PlaneStrain;
                default:
                    return 0;
            }
        }


        /// <summary>
        /// Sets the area section property design parameters.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAREA_SECTION_PROPERTY_DESIGN_PARAMETERS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AreaSection area = model.Components.AreaSections[tableRow["Section"]];
                if (!(area is ShellBase areaShell)) continue;

                areaShell.DesignProperties.Properties.MaterialName = tableRow["RebarMat"];
                areaShell.DesignProperties.Properties.RebarLayout = Enums.EnumLibrary.ConvertStringToEnumByDescription<eShellSteelLayoutOption>(tableRow["RebarOpt"]);
                if (tableRow.ContainsKey("CoverTop1"))
                {
                    areaShell.DesignProperties.Properties.CoverTopDirection1 = Adaptor.toDouble(tableRow["CoverTop1"]);
                    areaShell.DesignProperties.Properties.CoverBottomDirection1 = Adaptor.toDouble(tableRow["CoverBot1"]);
                }

                if (!(tableRow.ContainsKey("CoverTop2"))) continue;
                areaShell.DesignProperties.Properties.CoverTopDirection2 = Adaptor.toDouble(tableRow["CoverTop2"]);
                areaShell.DesignProperties.Properties.CoverBottomDirection2 = Adaptor.toDouble(tableRow["CoverBot2"]);
            }
        }


        /// <summary>
        /// Sets the area section property layers.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAREA_SECTION_PROPERTY_LAYERS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AreaSection area = model.Components.AreaSections[tableRow["Section"]];
                if (!(area is Shell areaShellLayered)) continue;

                areaShellLayered.Layers.Properties.Layers.Add(
                    new ShellLayerProperties
                    {
                        LayerName = tableRow["LayerName"],
                        DistanceOffset = Adaptor.toDouble(tableRow["Distance"]),
                        Thickness = Adaptor.toDouble(tableRow["Thickness"]),
                        LayerType = Enums.EnumLibrary.ConvertStringToEnumByDescription<eShellLayerType>(tableRow["Type"]),
                        NumberOfIntegrationPoints = Adaptor.toInteger(tableRow["NumIntPts"]),
                        MaterialName = tableRow["Material"],
                        MaterialAngle = Adaptor.toDouble(tableRow["MatAngle"]),
                        S11Type = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMaterialComponentBehaviorType>(tableRow["S11Opt"]),
                        S22Type = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMaterialComponentBehaviorType>(tableRow["S22Opt"]),
                        S12Type = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMaterialComponentBehaviorType>(tableRow["S12Opt"])
                    }
                );
            }
        }

        //TABLE:  "AREA SECTION PROPERTY - TIME DEPENDENT"
        //Section=ASEC1 TypeSize = Auto   AutoSFSize=1
        //Section=ASEC2 TypeSize = None   AutoSFSize=1
        //Section=ASEC3 TypeSize = User   AutoSFSize=1   UserValSize=1
        /// <summary>
        /// Sets the area section property time dependent.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        internal static void SetAREA_SECTION_PROPERTY_TIME_DEPENDENT(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AreaSection area = model.Components.AreaSections[tableRow["Section"]];
                throw new NotImplementedException();
            }
        }
    }
}
