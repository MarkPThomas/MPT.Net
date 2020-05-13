using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadFrameSections
    {
        internal static void DefineFrameSections(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_01_GENERAL, setFRAME_SECTION_PROPERTIES_01_GENERAL);
            reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_02_CONCRETE_COLUMN, setFRAME_SECTION_PROPERTIES_02_CONCRETE_COLUMN);
            reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_03_CONCRETE_BEAM, setFRAME_SECTION_PROPERTIES_03_CONCRETE_BEAM);
            //reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_04_AUTO_SELECT, setFRAME_SECTION_PROPERTIES_04_AUTO_SELECT);
            //reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_05_NONPRISMATIC, setFRAME_SECTION_PROPERTIES_05_NONPRISMATIC);
            reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_07_BUILT_UP_I, setFRAME_SECTION_PROPERTIES_07_BUILT_UP_I);
            //reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_10_STEEL_JOIST_GENERAL, setFRAME_SECTION_PROPERTIES_10_STEEL_JOIST_GENERAL);
            //reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_11_STEEL_JOIST_DATA, setFRAME_SECTION_PROPERTIES_11_STEEL_JOIST_DATA);
            //reader.ReadSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_13_TIME_DEPENDENT, setFRAME_SECTION_PROPERTIES_13_TIME_DEPENDENT);
            //reader.ReadSingleTable(SAP2000Tables.SECTION_DESIGNER_PROPERTIES_01_GENERAL, setSECTION_DESIGNER_PROPERTIES_01_GENERAL);
            //reader.ReadSingleTable(SAP2000Tables.SECTION_DESIGNER_PROPERTIES_12_SHAPE_SOLID_RECTANGLE, setSECTION_DESIGNER_PROPERTIES_12_SHAPE_SOLID_RECTANGLE);
            //reader.ReadSingleTable(SAP2000Tables.SECTION_DESIGNER_PROPERTIES_30_FIBER_GENERAL, setSECTION_DESIGNER_PROPERTIES_30_FIBER_GENERAL);
        }

        /// <summary>
        /// Sets the frame section properties 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_PROPERTIES_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections.FillItem(tableRow["SectionName"]);
                frameSection.SetSectionType(convertToFrameSectionType(tableRow["Shape"]));
                frameSection.ColorName = tableRow["Color"];
                if (tableRow.ContainsKey("Notes")) frameSection.Notes = tableRow["Notes"];
                if (tableRow.ContainsKey("GUID")) frameSection.GUID = tableRow["GUID"];
                if (tableRow.ContainsKey("Material")) frameSection.MaterialName = tableRow["Material"];
                if (tableRow.ContainsKey("FileName")) frameSection.FileName = tableRow["FileName"];
                if (tableRow.ContainsKey("SectInFile")) frameSection.NameInFile = tableRow["SectInFile"];

                if (tableRow.ContainsKey("Area"))
                {
                    SectionResultantProperties resultantProperties =
                        new SectionResultantProperties
                        {
                            Ag = Adaptor.toDouble(tableRow["Area"]),
                            As2 = Adaptor.toDouble(tableRow["AS2"]),
                            As3 = Adaptor.toDouble(tableRow["AS3"]),
                            I23 = Adaptor.toDouble(tableRow["I23"]),
                            I22 = Adaptor.toDouble(tableRow["I22"]),
                            I33 = Adaptor.toDouble(tableRow["I33"]),
                            J = Adaptor.toDouble(tableRow["TorsConst"]),
                            S22 = Adaptor.toDouble(tableRow["S22"]),
                            S33 = Adaptor.toDouble(tableRow["S33"]),
                            Z22 = Adaptor.toDouble(tableRow["Z22"]),
                            Z33 = Adaptor.toDouble(tableRow["Z33"]),
                            r22 = Adaptor.toDouble(tableRow["R22"]),
                            r33 = Adaptor.toDouble(tableRow["R33"])
                        };
                    frameSection.SetSectionProperties(resultantProperties);
                }

                if (tableRow.ContainsKey("AMod"))
                {
                    frameSection.Modifiers.CrossSectionalArea = Adaptor.toDouble(tableRow["AMod"]);
                    frameSection.Modifiers.ShearV2 = Adaptor.toDouble(tableRow["A2Mod"]);
                    frameSection.Modifiers.ShearV3 = Adaptor.toDouble(tableRow["A3Mod"]);
                    frameSection.Modifiers.Torsion = Adaptor.toDouble(tableRow["JMod"]);
                    frameSection.Modifiers.BendingM2 = Adaptor.toDouble(tableRow["I2Mod"]);
                    frameSection.Modifiers.BendingM3 = Adaptor.toDouble(tableRow["I3Mod"]);
                    frameSection.Modifiers.MassModifier = Adaptor.toDouble(tableRow["MMod"]);
                    frameSection.Modifiers.WeightModifier = Adaptor.toDouble(tableRow["WMod"]);
                }

                switch (frameSection)
                {
                    case ISection iSection:
                        iSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        iSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        iSection.SectionProperties.tf = Adaptor.toDouble(tableRow["tf"]);
                        iSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        iSection.SectionProperties.t2b = Adaptor.toDouble(tableRow["t2b"]);
                        iSection.SectionProperties.tfb = Adaptor.toDouble(tableRow["tfb"]);
                        break;
                    case CoverPlatedISection coverPlatedSection:
                        coverPlatedSection.SectionProperties.hTotal = Adaptor.toDouble(tableRow["t3"]);
                        coverPlatedSection.SectionProperties.bMax = Adaptor.toDouble(tableRow["t2"]);
                        break;
                    case HybridISection hybridISection:
                        hybridISection.SectionProperties.hTotal = Adaptor.toDouble(tableRow["t3"]);
                        hybridISection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        hybridISection.SectionProperties.bcTop = Adaptor.toDouble(tableRow["t2"]);
                        hybridISection.SectionProperties.tcTop = Adaptor.toDouble(tableRow["tf"]);
                        hybridISection.SectionProperties.bcBottom = Adaptor.toDouble(tableRow["t2b"]);
                        hybridISection.SectionProperties.tcBottom = Adaptor.toDouble(tableRow["tfb"]);
                        break;
                    case AutoSelectSection autoSelectSection:
                        autoSelectSection.SectionProperties.AutoType = tableRow["AutoType"];
                        break;
                    case TeeSection teeSection:
                        teeSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        teeSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        teeSection.SectionProperties.tf = Adaptor.toDouble(tableRow["tf"]);
                        teeSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        break;
                    case AngleSection angleSection:
                        angleSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        angleSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        angleSection.SectionProperties.tf = Adaptor.toDouble(tableRow["tf"]);
                        angleSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        break;
                    case DoubleAngleSection doubleAngleSection:
                        doubleAngleSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        doubleAngleSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        doubleAngleSection.SectionProperties.tf = Adaptor.toDouble(tableRow["tf"]);
                        doubleAngleSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        doubleAngleSection.SectionProperties.Separation = Adaptor.toDouble(tableRow["dis"]);
                        break;
                    case ChannelSection channelSection:
                        channelSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        channelSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        channelSection.SectionProperties.tf = Adaptor.toDouble(tableRow["tf"]);
                        channelSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        channelSection.SectionProperties.ShearCenterOffsetV2 = Adaptor.toDouble(tableRow["EccV2"]);
                        break;
                    case DoubleChannelSection doubleChannelSection:
                        doubleChannelSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        doubleChannelSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        doubleChannelSection.SectionProperties.tf = Adaptor.toDouble(tableRow["tf"]);
                        doubleChannelSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        doubleChannelSection.SectionProperties.Separation = Adaptor.toDouble(tableRow["dis"]);
                        break;
                    case CircleSection circleSection:
                        circleSection.SectionProperties.D = Adaptor.toDouble(tableRow["t3"]);
                        break;
                    case RectangleSection rectangleSection:
                        rectangleSection.SectionProperties.h = Adaptor.toDouble(tableRow["t3"]);
                        rectangleSection.SectionProperties.b = Adaptor.toDouble(tableRow["t2"]);
                        break;
                    case PipeSection pipeSection:
                        pipeSection.SectionProperties.D = Adaptor.toDouble(tableRow["t3"]);
                        pipeSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        break;
                    case TubeSection tubeSection:
                        tubeSection.SectionProperties.h = Adaptor.toDouble(tableRow["t3"]);
                        tubeSection.SectionProperties.b = Adaptor.toDouble(tableRow["t2"]);
                        tubeSection.SectionProperties.tf = Adaptor.toDouble(tableRow["tf"]);
                        tubeSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        break;
                    case ColdCSection coldCSection:
                        coldCSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        coldCSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        coldCSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        coldCSection.SectionProperties.Radius = Adaptor.toDouble(tableRow["Radius"]);
                        coldCSection.SectionProperties.LipDepth = Adaptor.toDouble(tableRow["LipDepth"]);
                        break;
                    case ColdZSection coldZSection:
                        coldZSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        coldZSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        coldZSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        coldZSection.SectionProperties.Radius = Adaptor.toDouble(tableRow["Radius"]);
                        coldZSection.SectionProperties.LipDepth = Adaptor.toDouble(tableRow["LipDepth"]);
                        coldZSection.SectionProperties.LipAngle = Adaptor.toDouble(tableRow["LipAngle"]);
                        break;
                    case ColdHatSection coldHatSection:
                        coldHatSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        coldHatSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        coldHatSection.SectionProperties.tw = Adaptor.toDouble(tableRow["tw"]);
                        coldHatSection.SectionProperties.Radius = Adaptor.toDouble(tableRow["Radius"]);
                        coldHatSection.SectionProperties.LipDepth = Adaptor.toDouble(tableRow["LipDepth"]);
                        break;
                    case TrapezoidalSection trapezoidalSection:
                        trapezoidalSection.SectionProperties.t3 = Adaptor.toDouble(tableRow["t3"]);
                        trapezoidalSection.SectionProperties.t2 = Adaptor.toDouble(tableRow["t2"]);
                        trapezoidalSection.SectionProperties.t2b = Adaptor.toDouble(tableRow["t2b"]);
                        break;
                }
            }
        }

        /// <summary>
        /// Converts the type of to frame section.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eFrameSectionType.</returns>
        private static eFrameSectionType convertToFrameSectionType(string value)
        {
            switch (value)
            {
                case "I/Wide Flange":
                    return eFrameSectionType.ISection;
                case "Built Up I":
                    return eFrameSectionType.BuiltUpICoverPlate;
                case "SD Section":
                    return eFrameSectionType.SectionDesigner;
                case "Rectangular":
                    return eFrameSectionType.Rectangular;
                case "Tee":
                    return eFrameSectionType.TSection;
                case "Angle":
                    return eFrameSectionType.Angle;
                case "Double Angle":
                    return eFrameSectionType.DoubleAngle;
                case "Double Channel":
                    return eFrameSectionType.DoubleChannel;
                case "Circle":
                    return eFrameSectionType.Circle;
                case "Trapezoidal":
                    return eFrameSectionType.Trapezoidal;
                case "Cold Formed C":
                    return eFrameSectionType.ColdC;
                case "Cold Formed Z":
                    return eFrameSectionType.ColdZ;
                case "Cold Formed Hat":
                    return eFrameSectionType.ColdHat;
                case "Channel":
                    return eFrameSectionType.Channel;
                case "Pipe":
                    return eFrameSectionType.Pipe;
                case "Box/Tube":
                    return eFrameSectionType.Box;
                case "Joist":
                    return eFrameSectionType.Joist;
                case "Nonprismatic":
                    return eFrameSectionType.Variable;
                case "Auto Select":
                    return eFrameSectionType.Auto;
                default:
                    return eFrameSectionType.All;
            }
        }


        /// <summary>
        /// Sets the frame section properties 02 concrete column.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_PROPERTIES_02_CONCRETE_COLUMN(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                if (!(frameSection is IColumnRebar concreteSection)) continue;

                concreteSection.ColumnRebar.Detailing.RebarConfiguration =
                    Enums.EnumLibrary.ConvertStringToEnumByDescription<eRebarConfiguration>(tableRow["ReinfConfig"]);
                concreteSection.ColumnRebar.Detailing.MaterialNameLongitudinal = tableRow["RebarMatL"];
                if (tableRow.ContainsKey("NumBarsCirc"))
                {
                    concreteSection.ColumnRebar.Detailing.NumberOfCircularBars = Adaptor.toInteger(tableRow["NumBarsCirc"]);
                }
                if (tableRow.ContainsKey("NumBars2Dir"))
                {
                    concreteSection.ColumnRebar.Detailing.NumberOfRectangularBars2Axis = Adaptor.toInteger(tableRow["NumBars2Dir"]);
                    concreteSection.ColumnRebar.Detailing.NumberOfRectangularBars2Axis = Adaptor.toInteger(tableRow["NumBars3Dir"]);
                }
                concreteSection.ColumnRebar.Detailing.RebarSize = tableRow["BarSizeL"];

                concreteSection.ColumnRebar.Detailing.ConfinementType =
                    Enums.EnumLibrary.ConvertStringToEnumByDescription<eConfinementType>(tableRow["LatReinf"]);
                concreteSection.ColumnRebar.Detailing.MaterialNameConfinement = tableRow["RebarMatC"];
                if (tableRow.ContainsKey("SpacingC"))
                {
                    concreteSection.ColumnRebar.Detailing.TieSpacingLongitudinal = Adaptor.toDouble(tableRow["SpacingC"]);
                }
                if (tableRow.ContainsKey("NumCBars2"))
                {
                    concreteSection.ColumnRebar.Detailing.NumberOfConfinementBars2Axis = Adaptor.toInteger(tableRow["NumCBars2"]);
                    concreteSection.ColumnRebar.Detailing.NumberOfConfinementBars3Axis = Adaptor.toInteger(tableRow["NumCBars3"]);
                }
                concreteSection.ColumnRebar.Detailing.TieSize = tableRow["BarSizeC"];

                concreteSection.ColumnRebar.Detailing.Cover = Adaptor.toDouble(tableRow["Cover"]);
                concreteSection.ColumnRebar.Detailing.ToBeDesigned = (tableRow["ReinfType"] == "Check");
            }
        }


        /// <summary>
        /// Sets the frame section properties 03 concrete beam.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_PROPERTIES_03_CONCRETE_BEAM(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                if (!(frameSection is IBeamRebar concreteSection)) continue;

                concreteSection.BeamRebar.Detailing.MaterialNameLongitudinal = tableRow["RebarMatL"];
                concreteSection.BeamRebar.Detailing.CoverTop = Adaptor.toDouble(tableRow["TopCover"]);
                concreteSection.BeamRebar.Detailing.CoverBottom = Adaptor.toDouble(tableRow["BotCover"]);
                concreteSection.BeamRebar.Detailing.TopLeftArea = Adaptor.toDouble(tableRow["TopLeftArea"]);
                concreteSection.BeamRebar.Detailing.TopRightArea = Adaptor.toDouble(tableRow["TopRghtArea"]);
                concreteSection.BeamRebar.Detailing.BottomLeftArea = Adaptor.toDouble(tableRow["BotLeftArea"]);
                concreteSection.BeamRebar.Detailing.BottomRightArea = Adaptor.toDouble(tableRow["BotRghtArea"]);
            }
        }

        // TABLE:  "FRAME SECTION PROPERTIES 04 - AUTO SELECT"
        // ListName=AUTO1   SectionName=FSEC1
        // ListName=AUTO1   SectionName=FSEC4
        // ListName=AUTO1   SectionName=FSEC5
        // ListName=AUTO1   SectionName=FSEC6
        // ListName=AUTO1   SectionName=FSEC7
        // ListName=AUTO1   SectionName=FSEC8
        // ListName=AUTO1   SectionName=FSEC9
        /// <summary>
        /// Sets the frame section properties 04 automatic select.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setFRAME_SECTION_PROPERTIES_04_AUTO_SELECT(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["ListName"]];
                throw new NotImplementedException();
            }
        }

        // TABLE:  "FRAME SECTION PROPERTIES 05 - NONPRISMATIC"
        // SectionName=VAR1   StartSect=FSEC1   EndSect=FSEC10   LengthType=Variable   VarLength=0.5   EI33Var=Parabolic   EI22Var=Linear
        // SectionName=VAR1   StartSect=FSEC10   EndSect=FSEC2   LengthType=Absolute   AbsLength=0.5   EI33Var=Cubic   EI22Var=Linear
        /// <summary>
        /// Sets the frame section properties 05 nonprismatic.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setFRAME_SECTION_PROPERTIES_05_NONPRISMATIC(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Sets the frame section properties 07 built up i.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_PROPERTIES_07_BUILT_UP_I(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                if (!(frameSection is CoverPlatedISection builtUpSection)) continue;
                switch (tableRow["ItemType"])
                {
                    case "I Section":
                        builtUpSection.SectionName = tableRow["ISection"];
                        builtUpSection.SectionProperties.fyTopFlange = Adaptor.toDouble(tableRow["FyTF"]);
                        builtUpSection.SectionProperties.fyWeb = Adaptor.toDouble(tableRow["FyW"]);
                        builtUpSection.SectionProperties.fyBottomFlange = Adaptor.toDouble(tableRow["FyBF"]);
                        break;
                    case "Top Cover Plate":
                        builtUpSection.MaterialNameTop = tableRow["Material"];
                        builtUpSection.SectionProperties.bcTop = Adaptor.toDouble(tableRow["Width"]);
                        builtUpSection.SectionProperties.tcTop = Adaptor.toDouble(tableRow["Thick"]);
                        break;
                    case "Bottom Cover Plate":
                        builtUpSection.MaterialNameBottom = tableRow["Material"];
                        builtUpSection.SectionProperties.bcBottom = Adaptor.toDouble(tableRow["Width"]);
                        builtUpSection.SectionProperties.tcBottom = Adaptor.toDouble(tableRow["Thick"]);
                        break;
                }
            }
        }

        // TABLE:  "FRAME SECTION PROPERTIES 10 - STEEL JOIST GENERAL"
        // SectionName=JST1   JoistType=Standard   Depth=0.254   UnitWt=0.0725692114770882   AnalysisI33=1.2E-05
        // SectionName=JST2   JoistType=Envelope   Depth=0.254   UnitWt=0.0725692114770882   AnalysisI33=1.2E-05   MomCap=19.4   ShearCap=8.8   MinSpan=2.438   MaxSpan=4.876
        /// <summary>
        /// Sets the frame section properties 10 steel joist general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setFRAME_SECTION_PROPERTIES_10_STEEL_JOIST_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                throw new NotImplementedException();

            }
        }

        // TABLE:  "FRAME SECTION PROPERTIES 11 - STEEL JOIST DATA"
        // SectionName=JST1   SpanLength=2.438   TLCap=8.02   LLDeflCap=8.02   I33=8.2E-06
        /// <summary>
        /// Sets the frame section properties 11 steel joist data.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setFRAME_SECTION_PROPERTIES_11_STEEL_JOIST_DATA(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                throw new NotImplementedException();

            }
        }

        // TABLE:  "FRAME SECTION PROPERTIES 13 - TIME DEPENDENT" // Notional Size
        // SectionName=FSEC2   TypeSize=Auto   AutoSFSize=1.5
        // SectionName=FSEC3   TypeSize=User   AutoSFSize=1   UserValSize=0.0234
        // SectionName=FSEC4   TypeSize=None   AutoSFSize=1
        /// <summary>
        /// Sets the frame section properties 13 time dependent.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_PROPERTIES_13_TIME_DEPENDENT(Model model, List<Dictionary<string, string>> table)
        {
            // TODO: No ETABS equivalent...?
            foreach (Dictionary<string, string> tableRow in table)
            {
                // All but Builtup, coldformed, none in ETABS
                //FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                //if (frameSection is INotionalSize timeDependentSection)
                //{
                //    timeDependentSection.AutoScaleFactorSize =
                //        Enums.EnumLibrary.ConvertStringToEnumByDescription<eNotionalSizeType>(tableRow["TypeSize"]);
                //}
            }
        }

        // TABLE:  "SECTION DESIGNER PROPERTIES 01 - GENERAL"
        // SectionName=FSEC11   DesignType="No Check/Design"   DsgnOrChck=Check   IncludeVStr=No
        /// <summary>
        /// Sets the section designer properties 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setSECTION_DESIGNER_PROPERTIES_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                throw new NotImplementedException();

            }
        }

        // TABLE:  "SECTION DESIGNER PROPERTIES 12 - SHAPE SOLID RECTANGLE"
        // SectionName=FSEC11   ShapeName=Rectangle1   ShapeMat=4000Psi   ZOrder=1   FillColor=Green   XCenter=0   YCenter=0   Height=0.5   Width=0.5   Rotation=0   Reinforcing=No
        /// <summary>
        /// Sets the section designer properties 12 shape solid rectangle.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setSECTION_DESIGNER_PROPERTIES_12_SHAPE_SOLID_RECTANGLE(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                throw new NotImplementedException();

            }
        }

        // TABLE:  "SECTION DESIGNER PROPERTIES 30 - FIBER GENERAL"
        // SectionName=FSEC11   NumFibersD2=3   NumFibersD3=3   CoordSys=Cartesian   GridAngle=0   LumpRebar=No   FiberPMM=No   FiberMC=No
        /// <summary>
        /// Sets the section designer properties 30 fiber general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setSECTION_DESIGNER_PROPERTIES_30_FIBER_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                throw new NotImplementedException();

            }
        }
    }
}
