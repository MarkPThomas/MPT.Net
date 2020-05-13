using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteAreaSections
    {
        internal static void DefineAreaSections(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.AREA_SECTION_PROPERTIES, SetAREA_SECTION_PROPERTIES);
            writer.WriteSingleTable(SAP2000Tables.AREA_SECTION_PROPERTY_DESIGN_PARAMETERS, SetAREA_SECTION_PROPERTY_DESIGN_PARAMETERS);
            writer.WriteSingleTable(SAP2000Tables.AREA_SECTION_PROPERTY_LAYERS, SetAREA_SECTION_PROPERTY_LAYERS);
            //writer.WriteSingleTable(SAP2000Tables.AREA_SECTION_PROPERTY_TIME_DEPENDENT, WriteAreaSections.SetAREA_SECTION_PROPERTY_TIME_DEPENDENT);
        }

        /// <summary>
        /// Sets the area section properties.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAREA_SECTION_PROPERTIES(Model model, List<Dictionary<string, string>> table)
        {
            // TODO: Incomplete
            foreach (AreaSection areaSection in model.Components.AreaSections)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Section", Adaptor.ToStringEntryLimited(areaSection.Name) },
                    { "AreaType", Adaptor.fromEnum(areaSection.AreaType) },
                    { "Material", Adaptor.ToStringEntryLimited(areaSection.MaterialName) },
                    { "Color", Adaptor.ToStringEntryLimited(areaSection.ColorName) }
                };

                if (!string.IsNullOrEmpty(areaSection.Notes))
                {
                    tableRow["Notes"] = Adaptor.ToStringEntryLimited(areaSection.Notes);
                }

                if (areaSection.Modifiers != null)
                {
                    tableRow["F11Mod"] = Adaptor.fromDouble(areaSection.Modifiers.MembraneF11);
                    tableRow["F22Mod"] = Adaptor.fromDouble(areaSection.Modifiers.MembraneF22);
                    tableRow["F12Mod"] = Adaptor.fromDouble(areaSection.Modifiers.MembraneF12);
                    tableRow["M11Mod"] = Adaptor.fromDouble(areaSection.Modifiers.BendingM11);
                    tableRow["M22Mod"] = Adaptor.fromDouble(areaSection.Modifiers.BendingM22);
                    tableRow["M12Mod"] = Adaptor.fromDouble(areaSection.Modifiers.BendingM12);
                    tableRow["V13Mod"] = Adaptor.fromDouble(areaSection.Modifiers.ShearV13);
                    tableRow["V23Mod"] = Adaptor.fromDouble(areaSection.Modifiers.ShearV23);
                    tableRow["MMod"] = Adaptor.fromDouble(areaSection.Modifiers.MassModifier);
                    tableRow["WMod"] = Adaptor.fromDouble(areaSection.Modifiers.WeightModifier);
                }

                switch (areaSection)
                {
                    case Shell areaShell:
                        tableRow["Type"] = convertFromShellType(areaShell.SectionProperties.ShellType);
                        tableRow["MatAngle"] = Adaptor.fromDouble(areaShell.SectionProperties.MaterialAngle);
                        tableRow["Thickness"] = Adaptor.fromDouble(areaShell.SectionProperties.MembraneThickness);
                        tableRow["BendThick"] = Adaptor.fromDouble(areaShell.SectionProperties.BendingThickness);
                        if (areaShell.SectionProperties.IncludeDrillingDOF)
                        {
                            tableRow["DrillDOF"] = Adaptor.toYesNo(areaShell.SectionProperties.IncludeDrillingDOF);
                        }
                        break;
                    case Plane areaPlane:
                        tableRow["Type"] = convertFromPlaneType(areaPlane.SectionProperties.PlaneType);
                        tableRow["MatAngle"] = Adaptor.fromDouble(areaPlane.SectionProperties.MaterialAngle);
                        tableRow["Thickness"] = Adaptor.fromDouble(areaPlane.SectionProperties.Thickness);
                        tableRow["InComp"] = Adaptor.toYesNo(areaPlane.SectionProperties.IncompatibleModes);
                        break;
                    case ASolid areaASolid:
                        tableRow["MatAngle"] = Adaptor.fromDouble(areaASolid.SectionProperties.MaterialAngle);
                        tableRow["Arc"] = Adaptor.fromDouble(areaASolid.SectionProperties.ArcAngle);
                        tableRow["InComp"] = Adaptor.toYesNo(areaASolid.SectionProperties.IncompatibleBendingModes);
                        tableRow["CoordSys"] = areaASolid.SectionProperties.CoordinateSystem;
                        break;
                }

                table.Add(tableRow);
            }
        }

        /// <summary>
        /// Converts the type of to shell.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eShellType.</returns>
        private static string convertFromShellType(eShellType value)
        {
            switch (value)
            {
                case eShellType.ShellThin:
                    return "Shell-Thin";
                case eShellType.ShellThick:
                    return "Shell-Thick";
                case eShellType.ShellLayered:
                    return "Shell-Layered";
                case eShellType.PlateThin:
                    return "Plate-Thin";
                case eShellType.PlateThick:
                    return "Plate-Thick";
                case eShellType.Membrane:
                    return "Membrane";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Converts the type of to plane.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ePlaneType.</returns>
        private static string convertFromPlaneType(ePlaneType value)
        {
            switch (value)
            {
                case ePlaneType.PlaneStress:
                    return "Plane-Stress";
                case ePlaneType.PlaneStrain:
                    return "Plane-Strain";
                default:
                    return string.Empty;
            }
        }


        /// <summary>
        /// Sets the area section property design parameters.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAREA_SECTION_PROPERTY_DESIGN_PARAMETERS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (AreaSection areaSection in model.Components.AreaSections)
            {
                if (!(areaSection is ShellBase areaShell)) continue;
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    {"Section", Adaptor.ToStringEntryLimited(areaSection.Name)},
                    {"RebarMat", Adaptor.ToStringEntryLimited(areaShell.DesignProperties.Properties.MaterialName)},
                    {"RebarOpt", Adaptor.fromEnum(areaShell.DesignProperties.Properties.RebarLayout)}
                };

                if (areaShell.DesignProperties.Properties.CoverTopDirection1 > 0 ||
                    areaShell.DesignProperties.Properties.CoverBottomDirection1 > 0)
                {
                    tableRow["CoverTop1"] = Adaptor.fromDouble(areaShell.DesignProperties.Properties.CoverTopDirection1);
                    tableRow["CoverBot1"] = Adaptor.fromDouble(areaShell.DesignProperties.Properties.CoverBottomDirection1);
                }
                if (areaShell.DesignProperties.Properties.CoverTopDirection2 > 0 ||
                    areaShell.DesignProperties.Properties.CoverBottomDirection2 > 0)
                {
                    tableRow["CoverTop2"] = Adaptor.fromDouble(areaShell.DesignProperties.Properties.CoverTopDirection2);
                    tableRow["CoverBot2"] = Adaptor.fromDouble(areaShell.DesignProperties.Properties.CoverBottomDirection2);
                }

                table.Add(tableRow);
            }
        }


        /// <summary>
        /// Sets the area section property layers.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetAREA_SECTION_PROPERTY_LAYERS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (AreaSection areaSection in model.Components.AreaSections)
            {
                if (!(areaSection is Shell areaShellLayered)) continue;
                foreach (ShellLayerProperties layer in areaShellLayered.Layers.Properties.Layers)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "Section", Adaptor.ToStringEntryLimited(areaSection.Name) },
                            { "LayerName", Adaptor.ToStringEntryLimited(layer.LayerName) },
                            { "Distance", Adaptor.fromDouble(layer.DistanceOffset) },
                            { "Thickness", Adaptor.fromDouble(layer.Thickness) },
                            { "Type", Adaptor.fromEnum(layer.LayerType) },
                            { "NumIntPts", Adaptor.fromInteger(layer.NumberOfIntegrationPoints) },
                            { "Material", Adaptor.ToStringEntryLimited(layer.MaterialName) },
                            { "MatAngle", Adaptor.fromDouble(layer.MaterialAngle) },
                            { "S11Opt", Adaptor.fromEnum(layer.S11Type) },
                            { "S22Opt", Adaptor.fromEnum(layer.S22Type) },
                            { "S12Opt", Adaptor.fromEnum(layer.S12Type) },
                        });
                }
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
