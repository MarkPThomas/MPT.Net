using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteFrameSections
    {
        internal static void DefineFrameSections(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_01_GENERAL, setFRAME_SECTION_PROPERTIES_01_GENERAL);
            writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_02_CONCRETE_COLUMN, setFRAME_SECTION_PROPERTIES_02_CONCRETE_COLUMN);
            writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_03_CONCRETE_BEAM, setFRAME_SECTION_PROPERTIES_03_CONCRETE_BEAM);
            //writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_04_AUTO_SELECT, setFRAME_SECTION_PROPERTIES_04_AUTO_SELECT);
            //writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_05_NONPRISMATIC, setFRAME_SECTION_PROPERTIES_05_NONPRISMATIC);
            writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_07_BUILT_UP_I, setFRAME_SECTION_PROPERTIES_07_BUILT_UP_I);
            //writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_10_STEEL_JOIST_GENERAL, setFRAME_SECTION_PROPERTIES_10_STEEL_JOIST_GENERAL);
            //writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_11_STEEL_JOIST_DATA, setFRAME_SECTION_PROPERTIES_11_STEEL_JOIST_DATA);
            //writer.WriteSingleTable(SAP2000Tables.FRAME_SECTION_PROPERTIES_13_TIME_DEPENDENT, setFRAME_SECTION_PROPERTIES_13_TIME_DEPENDENT);
            //writer.WriteSingleTable(SAP2000Tables.SECTION_DESIGNER_PROPERTIES_01_GENERAL, setSECTION_DESIGNER_PROPERTIES_01_GENERAL);
            //writer.WriteSingleTable(SAP2000Tables.SECTION_DESIGNER_PROPERTIES_12_SHAPE_SOLID_RECTANGLE, setSECTION_DESIGNER_PROPERTIES_12_SHAPE_SOLID_RECTANGLE);
            //writer.WriteSingleTable(SAP2000Tables.SECTION_DESIGNER_PROPERTIES_30_FIBER_GENERAL, setSECTION_DESIGNER_PROPERTIES_30_FIBER_GENERAL);
        }

        /// <summary>
        /// Sets the frame section properties 01 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_PROPERTIES_01_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (FrameSection frameSection in model.Components.FrameSections)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "SectionName", Adaptor.ToStringEntryLimited(frameSection.Name) },
                    { "Shape", Adaptor.ToStringEntryLimited(convertFromFrameSectionType(frameSection.SectionType)) },
                    { "Color", Adaptor.ToStringEntryLimited(frameSection.ColorName) }
                };
                if (!string.IsNullOrEmpty(frameSection.Notes)) tableRow["Notes"] = Adaptor.ToStringEntryLimited(frameSection.Notes);
                if (!string.IsNullOrEmpty(frameSection.GUID)) tableRow["GUID"] = Adaptor.ToStringEntryLimited(frameSection.GUID);
                if (!string.IsNullOrEmpty(frameSection.MaterialName)) tableRow["Material"] = Adaptor.ToStringEntryLimited(frameSection.MaterialName);
                if (!string.IsNullOrEmpty(frameSection.FileName)) tableRow["FileName"] = Adaptor.ToStringEntryLimited(frameSection.FileName);
                if (!string.IsNullOrEmpty(frameSection.NameInFile)) tableRow["SectInFile"] = Adaptor.ToStringEntryLimited(frameSection.NameInFile);

                if (frameSection.Ag > 0)
                {
                    tableRow["Area"] = Adaptor.fromDouble(frameSection.Ag);
                    tableRow["AS2"] = Adaptor.fromDouble(frameSection.As2);
                    tableRow["AS3"] = Adaptor.fromDouble(frameSection.As3);
                    tableRow["I23"] = Adaptor.fromDouble(frameSection.I23);
                    tableRow["I22"] = Adaptor.fromDouble(frameSection.I22);
                    tableRow["I33"] = Adaptor.fromDouble(frameSection.I33);
                    tableRow["TorsConst"] = Adaptor.fromDouble(frameSection.J);
                    tableRow["S22"] = Adaptor.fromDouble(frameSection.S22);
                    tableRow["S33"] = Adaptor.fromDouble(frameSection.S33);
                    tableRow["Z22"] = Adaptor.fromDouble(frameSection.Z22);
                    tableRow["Z33"] = Adaptor.fromDouble(frameSection.Z33);
                    tableRow["R22"] = Adaptor.fromDouble(frameSection.r22);
                    tableRow["R33"] = Adaptor.fromDouble(frameSection.r33);
                }

                if (frameSection.Modifiers != null)
                {
                    tableRow["AMod"] = Adaptor.fromDouble(frameSection.Modifiers.CrossSectionalArea);
                    tableRow["A2Mod"] = Adaptor.fromDouble(frameSection.Modifiers.ShearV2);
                    tableRow["A3Mod"] = Adaptor.fromDouble(frameSection.Modifiers.ShearV3);
                    tableRow["JMod"] = Adaptor.fromDouble(frameSection.Modifiers.Torsion);
                    tableRow["I2Mod"] = Adaptor.fromDouble(frameSection.Modifiers.BendingM2);
                    tableRow["I3Mod"] = Adaptor.fromDouble(frameSection.Modifiers.BendingM3);
                    tableRow["MMod"] = Adaptor.fromDouble(frameSection.Modifiers.MassModifier);
                    tableRow["WMod"] = Adaptor.fromDouble(frameSection.Modifiers.WeightModifier);
                }

                switch (frameSection)
                {
                    case ISection iSection:
                        tableRow["t3"] = Adaptor.fromDouble(iSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(iSection.SectionProperties.t2);
                        tableRow["tf"] = Adaptor.fromDouble(iSection.SectionProperties.tf);
                        tableRow["tw"] = Adaptor.fromDouble(iSection.SectionProperties.tw);
                        tableRow["t2b"] = Adaptor.fromDouble(iSection.SectionProperties.t2b);
                        tableRow["tfb"] = Adaptor.fromDouble(iSection.SectionProperties.tfb);
                        break;
                    case CoverPlatedISection coverPlatedSection:
                        tableRow["t3"] = Adaptor.fromDouble(coverPlatedSection.SectionProperties.hTotal);
                        tableRow["t2"] = Adaptor.fromDouble(coverPlatedSection.SectionProperties.bMax);
                        break;
                    case HybridISection hybridISection:
                        tableRow["t3"] = Adaptor.fromDouble(hybridISection.SectionProperties.hTotal);
                        tableRow["tw"] = Adaptor.fromDouble(hybridISection.SectionProperties.tw);
                        tableRow["t2"] = Adaptor.fromDouble(hybridISection.SectionProperties.bcTop);
                        tableRow["tf"] = Adaptor.fromDouble(hybridISection.SectionProperties.tcTop);
                        tableRow["t2b"] = Adaptor.fromDouble(hybridISection.SectionProperties.bcBottom);
                        tableRow["tfb"] = Adaptor.fromDouble(hybridISection.SectionProperties.tcBottom);
                        break;
                    case AutoSelectSection autoSelectSection:
                        tableRow["AutoType"] = Adaptor.ToStringEntryLimited(autoSelectSection.SectionProperties.AutoType);
                        break;
                    case TeeSection teeSection:
                        tableRow["t3"] = Adaptor.fromDouble(teeSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(teeSection.SectionProperties.t2);
                        tableRow["tf"] = Adaptor.fromDouble(teeSection.SectionProperties.tf);
                        tableRow["tw"] = Adaptor.fromDouble(teeSection.SectionProperties.tw);
                        break;
                    case AngleSection angleSection:
                        tableRow["t3"] = Adaptor.fromDouble(angleSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(angleSection.SectionProperties.t2);
                        tableRow["tf"] = Adaptor.fromDouble(angleSection.SectionProperties.tf);
                        tableRow["tw"] = Adaptor.fromDouble(angleSection.SectionProperties.tw);
                        break;
                    case DoubleAngleSection doubleAngleSection:
                        tableRow["t3"] = Adaptor.fromDouble(doubleAngleSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(doubleAngleSection.SectionProperties.t2);
                        tableRow["tf"] = Adaptor.fromDouble(doubleAngleSection.SectionProperties.tf);
                        tableRow["tw"] = Adaptor.fromDouble(doubleAngleSection.SectionProperties.tw);
                        tableRow["dis"] = Adaptor.fromDouble(doubleAngleSection.SectionProperties.Separation);
                        break;
                    case ChannelSection channelSection:
                        tableRow["t3"] = Adaptor.fromDouble(channelSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(channelSection.SectionProperties.t2);
                        tableRow["tf"] = Adaptor.fromDouble(channelSection.SectionProperties.tf);
                        tableRow["tw"] = Adaptor.fromDouble(channelSection.SectionProperties.tw);
                        tableRow["EccV2"] = Adaptor.fromDouble(channelSection.SectionProperties.ShearCenterOffsetV2);
                        break;
                    case DoubleChannelSection doubleChannelSection:
                        tableRow["t3"] = Adaptor.fromDouble(doubleChannelSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(doubleChannelSection.SectionProperties.t2);
                        tableRow["tf"] = Adaptor.fromDouble(doubleChannelSection.SectionProperties.tf);
                        tableRow["tw"] = Adaptor.fromDouble(doubleChannelSection.SectionProperties.tw);
                        tableRow["dis"] = Adaptor.fromDouble(doubleChannelSection.SectionProperties.Separation);
                        break;
                    case CircleSection circleSection:
                        tableRow["t3"] = Adaptor.fromDouble(circleSection.SectionProperties.D);
                        break;
                    case RectangleSection rectangleSection:
                        tableRow["t3"] = Adaptor.fromDouble(rectangleSection.SectionProperties.h);
                        tableRow["t2"] = Adaptor.fromDouble(rectangleSection.SectionProperties.b);
                        break;
                    case PipeSection pipeSection:
                        tableRow["t3"] = Adaptor.fromDouble(pipeSection.SectionProperties.D);
                        tableRow["tw"] = Adaptor.fromDouble(pipeSection.SectionProperties.tw);
                        break;
                    case TubeSection tubeSection:
                        tableRow["t3"] = Adaptor.fromDouble(tubeSection.SectionProperties.h);
                        tableRow["t2"] = Adaptor.fromDouble(tubeSection.SectionProperties.b);
                        tableRow["tf"] = Adaptor.fromDouble(tubeSection.SectionProperties.tf);
                        tableRow["tw"] = Adaptor.fromDouble(tubeSection.SectionProperties.tw);
                        break;
                    case ColdCSection coldCSection:
                        tableRow["t3"] = Adaptor.fromDouble(coldCSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(coldCSection.SectionProperties.t2);
                        tableRow["tw"] = Adaptor.fromDouble(coldCSection.SectionProperties.tw);
                        tableRow["Radius"] = Adaptor.fromDouble(coldCSection.SectionProperties.Radius);
                        tableRow["LipDepth"] = Adaptor.fromDouble(coldCSection.SectionProperties.LipDepth);
                        break;
                    case ColdZSection coldZSection:
                        tableRow["t3"] = Adaptor.fromDouble(coldZSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(coldZSection.SectionProperties.t2);
                        tableRow["tw"] = Adaptor.fromDouble(coldZSection.SectionProperties.tw);
                        tableRow["Radius"] = Adaptor.fromDouble(coldZSection.SectionProperties.Radius);
                        tableRow["LipDepth"] = Adaptor.fromDouble(coldZSection.SectionProperties.LipDepth);
                        tableRow["LipAngle"] = Adaptor.fromDouble(coldZSection.SectionProperties.LipAngle);
                        break;
                    case ColdHatSection coldHatSection:
                        tableRow["t3"] = Adaptor.fromDouble(coldHatSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(coldHatSection.SectionProperties.t2);
                        tableRow["tw"] = Adaptor.fromDouble(coldHatSection.SectionProperties.tw);
                        tableRow["Radius"] = Adaptor.fromDouble(coldHatSection.SectionProperties.Radius);
                        tableRow["LipDepth"] = Adaptor.fromDouble(coldHatSection.SectionProperties.LipDepth);
                        break;
                    case TrapezoidalSection trapezoidalSection:
                        tableRow["t3"] = Adaptor.fromDouble(trapezoidalSection.SectionProperties.t3);
                        tableRow["t2"] = Adaptor.fromDouble(trapezoidalSection.SectionProperties.t2);
                        tableRow["t2b"] = Adaptor.fromDouble(trapezoidalSection.SectionProperties.t2b);
                        break;
                }

                table.Add(tableRow);
            }
            
        }

        /// <summary>
        /// Converts the type of to frame section.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>eFrameSectionType.</returns>
        private static string convertFromFrameSectionType(eFrameSectionType value)
        {
            switch (value)
            {
                case eFrameSectionType.ISection:
                    return "I/Wide Flange";
                case eFrameSectionType.BuiltUpICoverPlate:
                    return "Built Up I";
                case eFrameSectionType.SectionDesigner:
                    return "SD Section";
                case eFrameSectionType.Rectangular:
                    return "Rectangular";
                case eFrameSectionType.TSection:
                    return "Tee";
                case eFrameSectionType.Angle:
                    return "Angle";
                case eFrameSectionType.DoubleAngle:
                    return "Double Angle";
                case eFrameSectionType.DoubleChannel:
                    return "Double Channel";
                case eFrameSectionType.Circle:
                    return "Circle";
                case eFrameSectionType.Trapezoidal:
                    return "Trapezoidal";
                case eFrameSectionType.ColdC:
                    return "Cold Formed C";
                case eFrameSectionType.ColdZ:
                    return "Cold Formed Z";
                case eFrameSectionType.ColdHat:
                    return "Cold Formed Hat";
                case eFrameSectionType.Channel:
                    return "Channel";
                case eFrameSectionType.Pipe:
                    return "Pipe";
                case eFrameSectionType.Box:
                    return "Box/Tube";
                case eFrameSectionType.Joist:
                    return "Joist";
                case eFrameSectionType.Variable:
                    return "Nonprismatic";
                case eFrameSectionType.Auto:
                    return "Auto Select";
                default:
                    return "All";
            }
        }


        /// <summary>
        /// Sets the frame section properties 02 concrete column.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_PROPERTIES_02_CONCRETE_COLUMN(Model model, List<Dictionary<string, string>> table)
        {
            foreach (FrameSection frameSection in model.Components.FrameSections)
            {
                if (!(frameSection is IColumnRebar concreteSection)) continue;

                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "SectionName", Adaptor.ToStringEntryLimited(frameSection.Name) },
                    { "RebarMatL", Adaptor.ToStringEntryLimited(concreteSection.ColumnRebar.Detailing.MaterialNameLongitudinal) },
                    { "ReinfConfig", Adaptor.fromEnum(concreteSection.ColumnRebar.Detailing.RebarConfiguration) }
                };
                if (concreteSection.ColumnRebar.Detailing.RebarConfiguration == eRebarConfiguration.Circular)
                {
                    tableRow["NumBarsCirc"] = Adaptor.fromInteger(concreteSection.ColumnRebar.Detailing.NumberOfCircularBars);
                    tableRow["SpacingC"] = Adaptor.fromDouble(concreteSection.ColumnRebar.Detailing.TieSpacingLongitudinal);
                }
                else if (concreteSection.ColumnRebar.Detailing.RebarConfiguration == eRebarConfiguration.Rectangular)
                {
                    tableRow["NumBars2Dir"] = Adaptor.fromInteger(concreteSection.ColumnRebar.Detailing.NumberOfRectangularBars2Axis);
                    tableRow["NumBars3Dir"] = Adaptor.fromInteger(concreteSection.ColumnRebar.Detailing.NumberOfRectangularBars2Axis);
                    tableRow["NumCBars2"] = Adaptor.fromInteger(concreteSection.ColumnRebar.Detailing.NumberOfConfinementBars2Axis);
                    tableRow["NumCBars3"] = Adaptor.fromInteger(concreteSection.ColumnRebar.Detailing.NumberOfConfinementBars3Axis);
                }
                tableRow["BarSizeL"] = Adaptor.ToStringEntryLimited(concreteSection.ColumnRebar.Detailing.RebarSize);

                tableRow["LatReinf"] = Adaptor.fromEnum(concreteSection.ColumnRebar.Detailing.ConfinementType);
                tableRow["RebarMatC"] = Adaptor.ToStringEntryLimited(concreteSection.ColumnRebar.Detailing.MaterialNameConfinement);

                tableRow["BarSizeC"] = Adaptor.ToStringEntryLimited(concreteSection.ColumnRebar.Detailing.TieSize);

                tableRow["Cover"] = Adaptor.fromDouble(concreteSection.ColumnRebar.Detailing.Cover);
                tableRow["ReinfType"] = concreteSection.ColumnRebar.Detailing.ToBeDesigned
                    ? "Check"
                    : "Design";

                table.Add(tableRow);
            }
        }


        /// <summary>
        /// Sets the frame section properties 03 concrete beam.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setFRAME_SECTION_PROPERTIES_03_CONCRETE_BEAM(Model model, List<Dictionary<string, string>> table)
        {
            foreach (FrameSection frameSection in model.Components.FrameSections)
            {
                if (!(frameSection is IBeamRebar concreteSection)) continue;

                table.Add(
                    new Dictionary<string, string>
                    {
                        { "SectionName", Adaptor.ToStringEntryLimited(frameSection.Name) },
                        { "RebarMatL", Adaptor.ToStringEntryLimited(concreteSection.BeamRebar.Detailing.MaterialNameLongitudinal) },
                        { "TopCover", Adaptor.fromDouble(concreteSection.BeamRebar.Detailing.CoverTop) },
                        { "BotCover", Adaptor.fromDouble(concreteSection.BeamRebar.Detailing.CoverBottom) },
                        { "TopLeftArea", Adaptor.fromDouble(concreteSection.BeamRebar.Detailing.TopLeftArea) },
                        { "TopRghtArea", Adaptor.fromDouble(concreteSection.BeamRebar.Detailing.TopRightArea) },
                        { "BotLeftArea", Adaptor.fromDouble(concreteSection.BeamRebar.Detailing.BottomLeftArea) },
                        { "BotRghtArea", Adaptor.fromDouble(concreteSection.BeamRebar.Detailing.BottomRightArea) }
                    });
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
            List<CoverPlatedISection> sections = getSectionsByType<CoverPlatedISection>(model.Components.FrameSections);
            foreach (CoverPlatedISection frameSection in sections)
            {

            }
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
            List<CoverPlatedISection> sections = getSectionsByType<CoverPlatedISection>(model.Components.FrameSections);
            foreach (CoverPlatedISection frameSection in sections)
            {

            }
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
            List<CoverPlatedISection> sections = getSectionsByType<CoverPlatedISection>(model.Components.FrameSections);
            foreach (CoverPlatedISection frameSection in sections)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "SectionName",  Adaptor.ToStringEntryLimited(frameSection.Name) },
                        { "ItemType",  Adaptor.ToStringEntryLimited("I Section") },
                        { "ISection",  Adaptor.ToStringEntryLimited(frameSection.SectionName) },
                        { "FyTF",  Adaptor.fromDouble(frameSection.SectionProperties.fyTopFlange) },
                        { "FyW",  Adaptor.fromDouble(frameSection.SectionProperties.fyWeb) },
                        { "FyBF",  Adaptor.fromDouble(frameSection.SectionProperties.fyBottomFlange) }
                    });

                table.Add(
                    new Dictionary<string, string>
                    {
                        { "SectionName",  Adaptor.ToStringEntryLimited(frameSection.Name) },
                        { "ItemType",  Adaptor.ToStringEntryLimited("Top Cover Plate") },
                        { "Material",  Adaptor.ToStringEntryLimited(frameSection.MaterialNameTop) },
                        { "Width",  Adaptor.fromDouble(frameSection.SectionProperties.bcTop) },
                        { "Thick",  Adaptor.fromDouble(frameSection.SectionProperties.tcTop) }
                    });

                table.Add(
                    new Dictionary<string, string>
                    {
                        { "SectionName",  Adaptor.ToStringEntryLimited(frameSection.Name) },
                        { "ItemType",  Adaptor.ToStringEntryLimited("Bottom Cover Plate") },
                        { "Material",  Adaptor.ToStringEntryLimited(frameSection.MaterialNameBottom) },
                        { "Width",  Adaptor.fromDouble(frameSection.SectionProperties.bcBottom) },
                        { "Thick",  Adaptor.fromDouble(frameSection.SectionProperties.tcBottom) }
                    });
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
            List<CoverPlatedISection> sections = getSectionsByType<CoverPlatedISection>(model.Components.FrameSections);
            foreach (CoverPlatedISection frameSection in sections)
            {

            }
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
            foreach (Dictionary<string, string> tableRow in table)
            {
                // All but Builtup, coldformed, none in ETABS
                //FrameSection frameSection = model.Components.FrameSections[tableRow["SectionName"]];
                //if (frameSection is INotionalSize timeDependentSection)
                //{
                //    timeDependentSection.AutoScaleFactorSize =
                //        Enums.EnumLibrary.GetEnumDescription<eNotionalSizeType>(tableRow["TypeSize"]);
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

        private static List<T> getSectionsByType<T>(FrameSections frameSections) where T : FrameSection
        {
            List<T> sections = new List<T>();
            foreach (FrameSection frameSection in frameSections)
            {
                if (!(frameSection is T validSection)) continue;
                sections.Add(validSection);
            }

            return sections;
        }
    }
}
