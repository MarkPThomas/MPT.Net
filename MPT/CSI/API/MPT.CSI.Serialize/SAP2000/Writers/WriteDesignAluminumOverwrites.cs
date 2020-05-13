using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Aluminum;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal class WriteDesignAluminumOverwrites : WriteDesignOverwrites
    {
        protected WriteDesignAluminumOverwrites() { }

        internal static void DefineOverwrites(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_ALUMINUM_DESIGN_AA_ASD_2000, setOVERWRITES_ALUMINUM_DESIGN_AA_ASD_2000);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_ALUMINUM_DESIGN_AA_LRFD_2000, setOVERWRITES_ALUMINUM_DESIGN_AA_LRFD_2000);
        }
        
        // // Aluminum   
        // // DesignSect="Program Determined"/FSEC1/...   
        // // Fy=0/32   
        // // (Reduced Live Load Factor)RLLF=0/11   
        // // (Unbraced length ratios) XLMajor=0/12   XLMinor=0/13   XLLTB=0/14
        // // (Effective length)XKMajor=4   XKMinor=5
        // // CmMajor=0   CmMinor=0    Cb=0 
        private static Dictionary<string, string> setOverwritesAluminumBasic(Frame frame)
        {
            FrameDesignOverwrites<AluminumDesignOverwrite, AluminumDesignOverwriteProperties> overwrite = frame.FrameDesignOverwrites.AluminumDesignOverwrite;
            
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

        // TABLE:  "OVERWRITES - ALUMINUM DESIGN - AA-LRFD 2000"
        // TABLE:  "OVERWRITES - ALUMINUM DESIGN - AA-ASD 2000"
        // // FrameType="Program Determined"   
        // // K1Comp=0   K2Comp=0   
        // // K1Bend=0   K2Bend=0   KT=0   
        // // C1=0   C2=0 
        // // Fa=0   Ft=0   Fb3=0   Fb2=0   Fs2=0   Fs3=0
        private static void setOVERWRITES_ALUMINUM_DESIGN_AA_ASD_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                AA_ASD_2000_Overwrites overwrite = 
                    frame.FrameDesignOverwrites.AluminumDesignOverwrite.GetItem<AA_ASD_2000_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesAluminumBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["K1Comp"] = Adaptor.fromDouble(overwrite.K1Comp);
                tableRow["K2Comp"] = Adaptor.fromDouble(overwrite.K2Comp);
                tableRow["K1Bend"] = Adaptor.fromDouble(overwrite.K1Bend);
                tableRow["K2Bend"] = Adaptor.fromDouble(overwrite.K2Bend);
                tableRow["KT"] = Adaptor.fromDouble(overwrite.KT);
                tableRow["C1"] = Adaptor.fromDouble(overwrite.C1);
                tableRow["C2"] = Adaptor.fromDouble(overwrite.C2);
                tableRow["Fa"] = Adaptor.fromDouble(overwrite.Fa);
                tableRow["Ft"] = Adaptor.fromDouble(overwrite.Ft);
                tableRow["Fb3"] = Adaptor.fromDouble(overwrite.Fb3);
                tableRow["Fb2"] = Adaptor.fromDouble(overwrite.Fb2);
                tableRow["Fs2"] = Adaptor.fromDouble(overwrite.Fs2);
                tableRow["Fs3"] = Adaptor.fromDouble(overwrite.Fs3);

                table.Add(tableRow);
            }
        }

        private static void setOVERWRITES_ALUMINUM_DESIGN_AA_LRFD_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                AA_LRFD_2000_Overwrites overwrite =
                    frame.FrameDesignOverwrites.AluminumDesignOverwrite.GetItem<AA_LRFD_2000_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesAluminumBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["K1Comp"] = Adaptor.fromDouble(overwrite.K1Comp);
                tableRow["K2Comp"] = Adaptor.fromDouble(overwrite.K2Comp);
                tableRow["K1Bend"] = Adaptor.fromDouble(overwrite.K1Bend);
                tableRow["K2Bend"] = Adaptor.fromDouble(overwrite.K2Bend);
                tableRow["KT"] = Adaptor.fromDouble(overwrite.KT);
                tableRow["C1"] = Adaptor.fromDouble(overwrite.C1);
                tableRow["C2"] = Adaptor.fromDouble(overwrite.C2);
                tableRow["Fa"] = Adaptor.fromDouble(overwrite.Fa);
                tableRow["Ft"] = Adaptor.fromDouble(overwrite.Ft);
                tableRow["Fb3"] = Adaptor.fromDouble(overwrite.Fb3);
                tableRow["Fb2"] = Adaptor.fromDouble(overwrite.Fb2);
                tableRow["Fs2"] = Adaptor.fromDouble(overwrite.Fs2);
                tableRow["Fs3"] = Adaptor.fromDouble(overwrite.Fs3);

                table.Add(tableRow);
            }
        }
    }
}
