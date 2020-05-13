using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Helpers.Definitions;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteSectionCuts
    {
        internal static void DefineSectionCuts(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.SECTION_CUTS_1_GENERAL, setSECTION_CUTS_1_GENERAL);
            //writer.WriteSingleTable(SAP2000Tables.SECTION_CUTS_2_ADVANCED_LOCAL_AXES, setSECTION_CUTS_2_ADVANCED_LOCAL_AXES);
            writer.WriteSingleTable(SAP2000Tables.SECTION_CUTS_3_QUADRILATERAL_DEFINITIONS, setSECTION_CUTS_3_QUADRILATERAL_DEFINITIONS);
        }

        // Note: These only seem to be exported when explicitly exporting a *.s2k. *$.2k does not include these! In v20.0.0. Appears resolved in v21.0.0.
        /// <summary>
        /// Sets the section cuts 1 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setSECTION_CUTS_1_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (SectionCut sectionCut in model.Analysis.SectionCuts)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "CutName", Adaptor.ToStringEntryLimited(sectionCut.Name) },
                    { "DefinedBy", Adaptor.fromEnum(sectionCut.CutDefinition) },
                    { "Group", Adaptor.ToStringEntryLimited(sectionCut.Group.Name) },
                    { "ResultType", Adaptor.ToStringEntryLimited(convertFromSectionResultType(sectionCut.SectionCutType)) },
                    { "DefaultLoc", Adaptor.toYesNo(sectionCut.IsReportedAtDefaultLocation) },
                    { "GlobalX", Adaptor.fromDouble(sectionCut.UserDefinedLocation.X) },
                    { "GlobalY", Adaptor.fromDouble(sectionCut.UserDefinedLocation.Y) },
                    { "GlobalZ", Adaptor.fromDouble(sectionCut.UserDefinedLocation.Z) }
                };

                if (sectionCut.SectionCutType != eSectionResultType.Analysis)
                {
                    tableRow["DesignType"] = Adaptor.ToStringEntryLimited(convertFromSectionDesignType(sectionCut.SectionCutType));
                }

                if (Math.Abs(sectionCut.Angle.AngleA) > Constants.Tolerance &&
                    Math.Abs(sectionCut.Angle.AngleB) > Constants.Tolerance &&
                    Math.Abs(sectionCut.Angle.AngleC) > Constants.Tolerance)
                {
                    tableRow["AngleA"] = Adaptor.fromDouble(sectionCut.Angle.AngleA);
                    tableRow["AngleB"] = Adaptor.fromDouble(sectionCut.Angle.AngleB);
                    tableRow["AngleC"] = Adaptor.fromDouble(sectionCut.Angle.AngleC);
                    tableRow["AdvanceAxes"] = Adaptor.toYesNo(sectionCut.UsesAdvancedAxes);
                }

                if (Math.Abs(sectionCut.DesignAngle) > Constants.Tolerance)
                {
                    tableRow["DesignAngle"] = Adaptor.fromDouble(sectionCut.DesignAngle);
                }

                if (!string.IsNullOrEmpty(sectionCut.Group.Name))
                {
                    tableRow["ElemSide"] = convertPositiveNegativeSide(sectionCut.IsPositiveElementSide);
                }

                table.Add(tableRow);
            }
        }

        private static string convertPositiveNegativeSide(bool isPositiveElementSide)
        {
            return isPositiveElementSide ? "Positive" : "Negative";
        }

        /// <summary>
        /// Converts the type of to section result.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="designType">Type of the design.</param>
        /// <returns>eSectionResultType.</returns>
        private static string convertFromSectionResultType(eSectionResultType resultType)
        {
            return (resultType  == eSectionResultType.Analysis) ? "Analysis" : "Design";
        }


        /// <summary>
        /// Converts the type of to section result.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="designType">Type of the design.</param>
        /// <returns>eSectionResultType.</returns>
        private static string convertFromSectionDesignType(eSectionResultType designType)
        {
            switch (designType)
            {
                case eSectionResultType.DesignWall:
                    return "Wall";
                case eSectionResultType.DesignSpandrel:
                    return "Spandrel";
                case eSectionResultType.DesignSlab:
                    return "Slab";
                default:
                    return string.Empty;
            }
        }

        //TABLE:  "SECTION CUTS 2 - ADVANCED LOCAL AXES"
        //SectionCut=SCUT3
        //LocalPlane = 31   AxOption1="Coord Dir"   AxCoordSys=GLOBAL AxCoordDir = Z
        //AxVecJt1=None AxVecJt2 = None
        //PlOption1="Coord Dir"   PlCoordSys=GLOBAL
        //CoordDir1 = X   CoordDir2=Y PlVecJt1 = None   PlVecJt2=None _
        //AxVecX=0   AxVecY=0   AxVecZ=1
        //PlVecX=1   PlVecY=0   PlVecZ=0
        /// <summary>
        /// Sets the section cuts 2 advanced local axes.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        /// <exception cref="NotImplementedException"></exception>
        private static void setSECTION_CUTS_2_ADVANCED_LOCAL_AXES(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                SectionCut sectionCut = model.Analysis.SectionCuts[tableRow["CutName"]];
                throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Sets the section cuts 3 quadrilateral definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setSECTION_CUTS_3_QUADRILATERAL_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (SectionCut sectionCut in model.Analysis.SectionCuts)
            {
                foreach (Quadrilateral quadrilateral in sectionCut.Quadrilaterals)
                {
                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "SectionCut", Adaptor.ToStringEntryLimited(sectionCut.Name) },
                            { "X", Adaptor.fromDouble(quadrilateral.Point1.X) },
                            { "Y", Adaptor.fromDouble(quadrilateral.Point1.Y) },
                            { "Z", Adaptor.fromDouble(quadrilateral.Point1.Z) },
                        });

                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "SectionCut", Adaptor.ToStringEntryLimited(sectionCut.Name) },
                            { "X", Adaptor.fromDouble(quadrilateral.Point2.X) },
                            { "Y", Adaptor.fromDouble(quadrilateral.Point2.Y) },
                            { "Z", Adaptor.fromDouble(quadrilateral.Point2.Z) },
                        });

                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "SectionCut", Adaptor.ToStringEntryLimited(sectionCut.Name) },
                            { "X", Adaptor.fromDouble(quadrilateral.Point3.X) },
                            { "Y", Adaptor.fromDouble(quadrilateral.Point3.Y) },
                            { "Z", Adaptor.fromDouble(quadrilateral.Point3.Z) },
                        });

                    table.Add(
                        new Dictionary<string, string>
                        {
                            { "SectionCut", Adaptor.ToStringEntryLimited(sectionCut.Name) },
                            { "X", Adaptor.fromDouble(quadrilateral.Point4.X) },
                            { "Y", Adaptor.fromDouble(quadrilateral.Point4.Y) },
                            { "Z", Adaptor.fromDouble(quadrilateral.Point4.Z) },
                        });
                }
            }
        }
    }
}
