using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.ColdFormed;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal class ReadDesignColdFormedOverwrites : ReadDesignOverwrites
    {
        protected ReadDesignColdFormedOverwrites() { }

        internal static void DefineOverwrites(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_COLD_FORMED_DESIGN_AISI_ASD96, setOVERWRITES_COLD_FORMED_DESIGN_AISI_ASD96);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_COLD_FORMED_DESIGN_AISI_LRFD96, setOVERWRITES_COLD_FORMED_DESIGN_AISI_LRFD96);
        }

        // // Cold Formed
        // // DesignSect="Program Determined"/FSEC1/...   
        // // Fy=0/32   
        // // (Reduced Live Load Factor)RLLF=0/11   
        // // (Unbraced length ratios) XLMajor=0/12   XLMinor=0/13   XLLTB=0/14
        // // (Effective length)XKMajor=4   XKMinor=5	
        // // CmMajor=0   CmMinor=0    Cb=0
        private static SteelColdFormedDesignOverwrite setOverwritesSteelColdFormedBasic(Model model, Dictionary<string, string> tableRow)
        {
            return new SteelColdFormedDesignOverwrite
            {
                DesignSection = getNullableFrameSection(model, tableRow["DesignSect"]),
                Fy = Adaptor.toDouble(tableRow["Fy"]),
                RLLF = Adaptor.toDouble(tableRow["RLLF"]),
                XLMajor = Adaptor.toDouble(tableRow["XLMajor"]),
                XLMinor = Adaptor.toDouble(tableRow["XLMinor"]),
                XLLTB = Adaptor.toDouble(tableRow["XLLTB"]),
                XKMajor = Adaptor.toDouble(tableRow["XKMajor"]),
                XKMinor = Adaptor.toDouble(tableRow["XKMinor"]),
                CmMajor = Adaptor.toDouble(tableRow["CmMajor"]),
                CmMinor = Adaptor.toDouble(tableRow["CmMinor"]),
                Cb = Adaptor.toDouble(tableRow["Cb"])
            };
        }

        // TABLE:  "OVERWRITES - COLD FORMED DESIGN - AISI-LRFD96"
        // TABLE:  "OVERWRITES - COLD FORMED DESIGN - AISI-ASD96"
        // // CtfMajor=0   CtfMinor=0   
        // // AlphaMajor=0   AlphaMinor=0   
        // // Pnc=0    Pnt=0   Mn33Yield=0   Mn22Yield=0   Mn33LTB=0   Mn22LTB=0   Vn2=0   Vn3=0   
        // // FastToDeck=No   FastEcc=0   HoleDiaTop=0   HoleDiaBot=0   HoleDiaWeb=0
        private static void setOVERWRITES_COLD_FORMED_DESIGN_AISI_ASD96(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelColdFormedDesignOverwrite.GenericOverwrites = setOverwritesSteelColdFormedBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelColdFormedDesignOverwrite.UpdateItem(
                    new AISI_ASD_1996_Overwrites
                    {
                        FrameType = getNullableEnum<AISI_ASD_1996_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        CtfMajor = Adaptor.toDouble(tableRow["CtfMajor"]),
                        CtfMinor = Adaptor.toDouble(tableRow["CtfMinor"]),
                        AlphaMajor = Adaptor.toDouble(tableRow["AlphaMajor"]),
                        AlphaMinor = Adaptor.toDouble(tableRow["AlphaMinor"]),
                        Pnc = Adaptor.toDouble(tableRow["Pnc"]),
                        Pnt = Adaptor.toDouble(tableRow["Pnt"]),
                        Mn33Yield = Adaptor.toDouble(tableRow["Mn33Yield"]),
                        Mn22Yield = Adaptor.toDouble(tableRow["Mn22Yield"]),
                        Mn33LTB = Adaptor.toDouble(tableRow["Mn33LTB"]),
                        Mn22LTB = Adaptor.toDouble(tableRow["Mn22LTB"]),
                        Vn2 = Adaptor.toDouble(tableRow["Vn2"]),
                        Vn3 = Adaptor.toDouble(tableRow["Vn3"]),
                        IsThroughFastenedToDeck = getNullableYesNo(tableRow["FastToDeck"]),
                        FastenerEccentricity = Adaptor.toDouble(tableRow["FastEcc"]),
                        HoleDiameterAtTopFlange = Adaptor.toDouble(tableRow["HoleDiaTop"]),
                        HoleDiameterAtBottomFlange = Adaptor.toDouble(tableRow["HoleDiaBot"]),
                        HoleDiameterOnWeb = Adaptor.toDouble(tableRow["HoleDiaWeb"])
                    }
                );
            }
        }

        private static void setOVERWRITES_COLD_FORMED_DESIGN_AISI_LRFD96(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelColdFormedDesignOverwrite.GenericOverwrites = setOverwritesSteelColdFormedBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelColdFormedDesignOverwrite.UpdateItem(
                    new AISI_LRFD_1996_Overwrites
                    {
                        FrameType = getNullableEnum<AISI_LRFD_1996_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        CtfMajor = Adaptor.toDouble(tableRow["CtfMajor"]),
                        CtfMinor = Adaptor.toDouble(tableRow["CtfMinor"]),
                        AlphaMajor = Adaptor.toDouble(tableRow["AlphaMajor"]),
                        AlphaMinor = Adaptor.toDouble(tableRow["AlphaMinor"]),
                        Pnc = Adaptor.toDouble(tableRow["Pnc"]),
                        Pnt = Adaptor.toDouble(tableRow["Pnt"]),
                        Mn33Yield = Adaptor.toDouble(tableRow["Mn33Yield"]),
                        Mn22Yield = Adaptor.toDouble(tableRow["Mn22Yield"]),
                        Mn33LTB = Adaptor.toDouble(tableRow["Mn33LTB"]),
                        Mn22LTB = Adaptor.toDouble(tableRow["Mn22LTB"]),
                        Vn2 = Adaptor.toDouble(tableRow["Vn2"]),
                        Vn3 = Adaptor.toDouble(tableRow["Vn3"]),
                        IsThroughFastenedToDeck = getNullableYesNo(tableRow["FastToDeck"]),
                        FastenerEccentricity = Adaptor.toDouble(tableRow["FastEcc"]),
                        HoleDiameterAtTopFlange = Adaptor.toDouble(tableRow["HoleDiaTop"]),
                        HoleDiameterAtBottomFlange = Adaptor.toDouble(tableRow["HoleDiaBot"]),
                        HoleDiameterOnWeb = Adaptor.toDouble(tableRow["HoleDiaWeb"])
                    }
                );
            }
        }
    }
}
