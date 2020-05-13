using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.ColdFormed;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal class WriteDesignColdFormedOverwrites : WriteDesignOverwrites
    {
        protected WriteDesignColdFormedOverwrites() { }

        internal static void DefineOverwrites(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_COLD_FORMED_DESIGN_AISI_ASD96, setOVERWRITES_COLD_FORMED_DESIGN_AISI_ASD96);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_COLD_FORMED_DESIGN_AISI_LRFD96, setOVERWRITES_COLD_FORMED_DESIGN_AISI_LRFD96);
        }

        // // Cold Formed
        // // DesignSect="Program Determined"/FSEC1/...   
        // // Fy=0/32   
        // // (Reduced Live Load Factor)RLLF=0/11   
        // // (Unbraced length ratios) XLMajor=0/12   XLMinor=0/13   XLLTB=0/14
        // // (Effective length)XKMajor=4   XKMinor=5	
        // // CmMajor=0   CmMinor=0    Cb=0
        private static Dictionary<string, string> setOverwritesSteelColdFormedBasic(Frame frame)
        {
            FrameDesignOverwrites<SteelColdFormedDesignOverwrite, SteelColdFormedDesignOverwriteProperties> overwrite = frame.FrameDesignOverwrites.SteelColdFormedDesignOverwrite;

            Dictionary<string, string> tableRow = new Dictionary<string, string>
            {
                ["Frame"] = Adaptor.ToStringEntryLimited(frame.Name),
                ["DesignSect"] =
                    Adaptor.ToStringEntryLimited(getNullableFrameSection(overwrite.GenericOverwrites.DesignSection)),
                ["Fy"] = Adaptor.fromDouble(overwrite.GenericOverwrites.Fy),
                ["RLLF"] = Adaptor.fromDouble(overwrite.GenericOverwrites.RLLF),
                ["XLMajor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XLMajor),
                ["XLMinor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XLMinor),
                ["XLLTB"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XLLTB),
                ["XKMajor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XKMajor),
                ["XKMinor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XKMinor),
                ["CmMajor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.CmMajor),
                ["CmMinor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.CmMinor),
                ["Cb"] = Adaptor.fromDouble(overwrite.GenericOverwrites.Cb)
            };

            return tableRow;
        }

        // TABLE:  "OVERWRITES - COLD FORMED DESIGN - AISI-LRFD96"
        // TABLE:  "OVERWRITES - COLD FORMED DESIGN - AISI-ASD96"
        // // CtfMajor=0   CtfMinor=0   
        // // AlphaMajor=0   AlphaMinor=0   
        // // Pnc=0    Pnt=0   Mn33Yield=0   Mn22Yield=0   Mn33LTB=0   Mn22LTB=0   Vn2=0   Vn3=0   
        // // FastToDeck=No   FastEcc=0   HoleDiaTop=0   HoleDiaBot=0   HoleDiaWeb=0
        private static void setOVERWRITES_COLD_FORMED_DESIGN_AISI_ASD96(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                AISI_ASD_1996_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelColdFormedDesignOverwrite.GetItem<AISI_ASD_1996_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelColdFormedBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["CtfMajor"] = Adaptor.fromDouble(overwrite.CtfMajor);
                tableRow["CtfMinor"] = Adaptor.fromDouble(overwrite.CtfMinor);
                tableRow["AlphaMajor"] = Adaptor.fromDouble(overwrite.AlphaMajor);
                tableRow["AlphaMinor"] = Adaptor.fromDouble(overwrite.AlphaMinor);
                tableRow["Pnc"] = Adaptor.fromDouble(overwrite.Pnc);
                tableRow["Pnt"] = Adaptor.fromDouble(overwrite.Pnt);
                tableRow["Mn33Yield"] = Adaptor.fromDouble(overwrite.Mn33Yield);
                tableRow["Mn22Yield"] = Adaptor.fromDouble(overwrite.Mn22Yield);
                tableRow["Mn33LTB"] = Adaptor.fromDouble(overwrite.Mn33LTB);
                tableRow["Mn22LTB"] = Adaptor.fromDouble(overwrite.Mn22LTB);
                tableRow["Vn2"] = Adaptor.fromDouble(overwrite.Vn2);
                tableRow["Vn3"] = Adaptor.fromDouble(overwrite.Vn3);
                tableRow["FastToDeck"] = getNullableYesNo(overwrite.IsThroughFastenedToDeck);
                tableRow["FastEcc"] = Adaptor.fromDouble(overwrite.FastenerEccentricity);
                tableRow["HoleDiaTop"] = Adaptor.fromDouble(overwrite.HoleDiameterAtTopFlange);
                tableRow["HoleDiaBot"] = Adaptor.fromDouble(overwrite.HoleDiameterAtBottomFlange);
                tableRow["HoleDiaWeb"] = Adaptor.fromDouble(overwrite.HoleDiameterOnWeb);

                table.Add(tableRow);
            }
        }

        private static void setOVERWRITES_COLD_FORMED_DESIGN_AISI_LRFD96(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                AISI_LRFD_1996_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelColdFormedDesignOverwrite.GetItem<AISI_LRFD_1996_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelColdFormedBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["CtfMajor"] = Adaptor.fromDouble(overwrite.CtfMajor);
                tableRow["CtfMinor"] = Adaptor.fromDouble(overwrite.CtfMinor);
                tableRow["AlphaMajor"] = Adaptor.fromDouble(overwrite.AlphaMajor);
                tableRow["AlphaMinor"] = Adaptor.fromDouble(overwrite.AlphaMinor);
                tableRow["Pnc"] = Adaptor.fromDouble(overwrite.Pnc);
                tableRow["Pnt"] = Adaptor.fromDouble(overwrite.Pnt);
                tableRow["Mn33Yield"] = Adaptor.fromDouble(overwrite.Mn33Yield);
                tableRow["Mn22Yield"] = Adaptor.fromDouble(overwrite.Mn22Yield);
                tableRow["Mn33LTB"] = Adaptor.fromDouble(overwrite.Mn33LTB);
                tableRow["Mn22LTB"] = Adaptor.fromDouble(overwrite.Mn22LTB);
                tableRow["Vn2"] = Adaptor.fromDouble(overwrite.Vn2);
                tableRow["Vn3"] = Adaptor.fromDouble(overwrite.Vn3);
                tableRow["FastToDeck"] = getNullableYesNo(overwrite.IsThroughFastenedToDeck);
                tableRow["FastEcc"] = Adaptor.fromDouble(overwrite.FastenerEccentricity);
                tableRow["HoleDiaTop"] = Adaptor.fromDouble(overwrite.HoleDiameterAtTopFlange);
                tableRow["HoleDiaBot"] = Adaptor.fromDouble(overwrite.HoleDiameterAtBottomFlange);
                tableRow["HoleDiaWeb"] = Adaptor.fromDouble(overwrite.HoleDiameterOnWeb);

                table.Add(tableRow);
            }
        }
    }
}
