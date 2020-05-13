using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadSectionCuts
    {
        internal static void DefineSectionCuts(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.SECTION_CUTS_1_GENERAL, setSECTION_CUTS_1_GENERAL);
            //reader.ReadSingleTable(SAP2000Tables.SECTION_CUTS_2_ADVANCED_LOCAL_AXES, setSECTION_CUTS_2_ADVANCED_LOCAL_AXES);
            reader.ReadSingleTable(SAP2000Tables.SECTION_CUTS_3_QUADRILATERAL_DEFINITIONS, setSECTION_CUTS_3_QUADRILATERAL_DEFINITIONS);
        }

        // Note: These only seem to be exported when explicitly exporting a *.s2k. *$.2k does not include these! In v20.0.0. Appears resolved in v21.0.0.
        /// <summary>
        /// Sets the section cuts 1 general.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setSECTION_CUTS_1_GENERAL(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                SectionCut sectionCut = model.Analysis.SectionCuts.FillItem(tableRow["CutName"]);
                sectionCut.CutDefinition = Enums.EnumLibrary.ConvertStringToEnumByDescription<eSectionCut>(tableRow["DefinedBy"]);
                sectionCut.Group = model.Groupings.Groups[tableRow["Group"]];

                string designType = tableRow.ContainsKey("DesignType") ? tableRow["DesignType"] : string.Empty;
                sectionCut.SectionCutType = convertToSectionResultType(tableRow["ResultType"], designType);
                sectionCut.IsReportedAtDefaultLocation = Adaptor.fromYesNo(tableRow["DefaultLoc"]);
                sectionCut.UserDefinedLocation = new Coordinate3DCartesian
                {
                    X = Adaptor.toDouble(tableRow["GlobalX"]),
                    Y = Adaptor.toDouble(tableRow["GlobalY"]),
                    Z = Adaptor.toDouble(tableRow["GlobalZ"])
                };
                if (tableRow.ContainsKey("AngleA"))
                {
                    AngleLocalAxes angle = new AngleLocalAxes
                    {
                        AngleA = Adaptor.toDouble(tableRow["AngleA"]),
                        AngleB = Adaptor.toDouble(tableRow["AngleB"]),
                        AngleC = Adaptor.toDouble(tableRow["AngleC"])
                    };
                    sectionCut.Angle = angle;
                    sectionCut.UsesAdvancedAxes = Adaptor.fromYesNo(tableRow["AdvanceAxes"]);
                }

                if (tableRow.ContainsKey("DesignAngle")) sectionCut.DesignAngle = Adaptor.toDouble(tableRow["DesignAngle"]);
                if (tableRow.ContainsKey("ElemSide")) sectionCut.IsPositiveElementSide = (tableRow["ElemSide"].ToLower() == "positive");
            }
        }

        /// <summary>
        /// Converts the type of to section result.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <param name="designType">Type of the design.</param>
        /// <returns>eSectionResultType.</returns>
        private static eSectionResultType convertToSectionResultType(string resultType, string designType)
        {
            if (resultType.ToLower() == "analysis")
            {
                return eSectionResultType.Analysis;
            }

            switch (designType.ToLower())
            {
                case "wall":
                    return eSectionResultType.DesignWall;
                case "spandrel":
                    return eSectionResultType.DesignSpandrel;
                case "slab":
                    return eSectionResultType.DesignSlab;
                default:
                    return 0;
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
            foreach (Dictionary<string, string> tableRow in table)
            {
                SectionCut sectionCut = model.Analysis.SectionCuts[tableRow["SectionCut"]];
                Quadrilateral quadrilateral = new Quadrilateral();
                Coordinate3DCartesian point = new Coordinate3DCartesian
                {
                    X = Adaptor.toDouble(tableRow["X"]),
                    Y = Adaptor.toDouble(tableRow["Y"]),
                    Z = Adaptor.toDouble(tableRow["Z"])
                };
                int quadrilateralNumber = Adaptor.toInteger(tableRow["QuadNum"]);

                switch (tableRow["PointNum"])
                {
                    case "1":
                        quadrilateral.Point1 = point;
                        sectionCut.Quadrilaterals.Add(quadrilateral);
                        break;
                    case "2":
                        sectionCut.Quadrilaterals[quadrilateralNumber - 1].Point2 = point;
                        break;
                    case "3":
                        sectionCut.Quadrilaterals[quadrilateralNumber - 1].Point3 = point;
                        break;
                    case "4":
                        sectionCut.Quadrilaterals[quadrilateralNumber - 1].Point4 = point;
                        break;
                }
            }
        }
    }
}
