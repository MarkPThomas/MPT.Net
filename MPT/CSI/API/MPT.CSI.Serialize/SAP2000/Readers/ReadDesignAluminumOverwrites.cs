using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Aluminum;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal class ReadDesignAluminumOverwrites : ReadDesignOverwrites
    {
        protected ReadDesignAluminumOverwrites() { }

        internal static void DefineOverwrites(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_ALUMINUM_DESIGN_AA_ASD_2000, setOVERWRITES_ALUMINUM_DESIGN_AA_ASD_2000);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_ALUMINUM_DESIGN_AA_LRFD_2000, setOVERWRITES_ALUMINUM_DESIGN_AA_LRFD_2000);
        }
        
        // // Aluminum   
        // // DesignSect="Program Determined"/FSEC1/...   
        // // Fy=0/32   
        // // (Reduced Live Load Factor)RLLF=0/11   
        // // (Unbraced length ratios) XLMajor=0/12   XLMinor=0/13   XLLTB=0/14
        // // (Effective length)XKMajor=4   XKMinor=5
        // // CmMajor=0   CmMinor=0    Cb=0 
        private static AluminumDesignOverwrite setOverwritesAluminumBasic(Model model, Dictionary<string, string> tableRow)
        {
            return new AluminumDesignOverwrite
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

        // TABLE:  "OVERWRITES - ALUMINUM DESIGN - AA-LRFD 2000"
        // TABLE:  "OVERWRITES - ALUMINUM DESIGN - AA-ASD 2000"
        // // FrameType="Program Determined"   
        // // K1Comp=0   K2Comp=0   
        // // K1Bend=0   K2Bend=0   KT=0   
        // // C1=0   C2=0 
        // // Fa=0   Ft=0   Fb3=0   Fb2=0   Fs2=0   Fs3=0
        private static void setOVERWRITES_ALUMINUM_DESIGN_AA_ASD_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.AluminumDesignOverwrite.GenericOverwrites = setOverwritesAluminumBasic(model, tableRow);
                frame.FrameDesignOverwrites.AluminumDesignOverwrite.UpdateItem(
                    new AA_ASD_2000_Overwrites
                    {
                        FrameType = getNullableEnum<AA_ASD_2000_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        K1Comp = Adaptor.toDouble(tableRow["K1Comp"]),
                        K2Comp = Adaptor.toDouble(tableRow["K2Comp"]),
                        K1Bend = Adaptor.toDouble(tableRow["K1Bend"]),
                        K2Bend = Adaptor.toDouble(tableRow["K2Bend"]),
                        KT = Adaptor.toDouble(tableRow["KT"]),
                        C1 = Adaptor.toDouble(tableRow["C1"]),
                        C2 = Adaptor.toDouble(tableRow["C2"]),
                        Fa = Adaptor.toDouble(tableRow["Fa"]),
                        Ft = Adaptor.toDouble(tableRow["Ft"]),
                        Fb3 = Adaptor.toDouble(tableRow["Fb3"]),
                        Fb2 = Adaptor.toDouble(tableRow["Fb2"]),
                        Fs2 = Adaptor.toDouble(tableRow["Fs2"]),
                        Fs3 = Adaptor.toDouble(tableRow["Fs3"])
                    }
                );
            }
        }

        private static void setOVERWRITES_ALUMINUM_DESIGN_AA_LRFD_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.AluminumDesignOverwrite.GenericOverwrites = setOverwritesAluminumBasic(model, tableRow);
                frame.FrameDesignOverwrites.AluminumDesignOverwrite.UpdateItem(
                    new AA_LRFD_2000_Overwrites
                    {
                        FrameType = getNullableEnum<AA_LRFD_2000_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        K1Comp = Adaptor.toDouble(tableRow["K1Comp"]),
                        K2Comp = Adaptor.toDouble(tableRow["K2Comp"]),
                        K1Bend = Adaptor.toDouble(tableRow["K1Bend"]),
                        K2Bend = Adaptor.toDouble(tableRow["K2Bend"]),
                        KT = Adaptor.toDouble(tableRow["KT"]),
                        C1 = Adaptor.toDouble(tableRow["C1"]),
                        C2 = Adaptor.toDouble(tableRow["C2"]),
                        Fa = Adaptor.toDouble(tableRow["Fa"]),
                        Ft = Adaptor.toDouble(tableRow["Ft"]),
                        Fb3 = Adaptor.toDouble(tableRow["Fb3"]),
                        Fb2 = Adaptor.toDouble(tableRow["Fb2"]),
                        Fs2 = Adaptor.toDouble(tableRow["Fs2"]),
                        Fs3 = Adaptor.toDouble(tableRow["Fs3"])
                    }
                );
            }
        }
    }
}
