using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal class WriteDesignConcreteOverwrites : WriteDesignOverwrites
    {
        protected WriteDesignConcreteOverwrites() { }

        internal static void DefineOverwrites(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_ACI_318_08_IBC_2009, setOVERWRITES_CONCRETE_DESIGN_ACI_318_08_IBC_2009);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_ACI_318_11, setOVERWRITES_CONCRETE_DESIGN_ACI_318_11);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_ACI_318_14, setOVERWRITES_CONCRETE_DESIGN_ACI_318_14);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_AS_3600_09, setOVERWRITES_CONCRETE_DESIGN_AS_3600_09);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_AS_3600_18, setOVERWRITES_CONCRETE_DESIGN_AS_3600_18);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_BS8110_97, setOVERWRITES_CONCRETE_DESIGN_BS8110_97);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_CSA_A233_04, setOVERWRITES_CONCRETE_DESIGN_CSA_A233_04);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_CSA_A233_14, setOVERWRITES_CONCRETE_DESIGN_CSA_A233_14);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_EUROCODE_2_2004, setOVERWRITES_CONCRETE_DESIGN_EUROCODE_2_2004);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_HONG_KONG_CP_2013, setOVERWRITES_CONCRETE_DESIGN_HONG_KONG_CP_2013);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_INDIAN_IS_456_2000, setOVERWRITES_CONCRETE_DESIGN_INDIAN_IS_456_2000);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_ITALIAN_NTC_2008, setOVERWRITES_CONCRETE_DESIGN_ITALIAN_NTC_2008);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_KBC_2009, setOVERWRITES_CONCRETE_DESIGN_KBC_2009);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_KBC_2016, setOVERWRITES_CONCRETE_DESIGN_KBC_2016);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2004, setOVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2004);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2017, setOVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2017);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_NZS_3101_06, setOVERWRITES_CONCRETE_DESIGN_NZS_3101_06);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_SINGAPORE_CP_65_99, setOVERWRITES_CONCRETE_DESIGN_SINGAPORE_CP_65_99);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_TS_500_2000, setOVERWRITES_CONCRETE_DESIGN_TS_500_2000);
        }

        // //  Concrete
        // // ConcreteDesignSect="Program Determined"/FSEC1/...   
        // // (Reduced Live Load Factor)RLLF=0/11   
        // // (Unbraced length ratios) XLMajor=0/12   XLMinor=0/13
        // // (Effective length)XKMajor=0/15   XKMinor=0/16 
        private static Dictionary<string, string> setOverwritesConcreteBasic(Frame frame)
        {
            FrameDesignOverwrites<ConcreteDesignOverwrite, ConcreteDesignOverwriteProperties> overwrite = frame.FrameDesignOverwrites.ConcreteDesignOverwrite;

            Dictionary<string, string> tableRow = new Dictionary<string, string>
            {
                ["Frame"] = Adaptor.ToStringEntryLimited(frame.Name),
                ["DesignSect"] =
                    Adaptor.ToStringEntryLimited(getNullableFrameSection(overwrite.GenericOverwrites.DesignSection)),
                ["RLLF"] = Adaptor.fromDouble(overwrite.GenericOverwrites.RLLF),
                ["XLMajor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XLMajor),
                ["XLMinor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XLMinor)
            };

            if (overwrite.GenericOverwrites.XKMajor.HasValue) tableRow["XKMajor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XKMajor.Value);
            if (overwrite.GenericOverwrites.XKMinor.HasValue) tableRow["XKMinor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XKMinor.Value);

            return tableRow;
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - ACI 318-08/IBC2009"
        // // FrameType="Program Determined"
        // // (Nonsway/sway Moment Factors) DnsMajor=0   DnsMinor=0   DsMajor=4   DsMinor=0
        // // (Moment Coefficient) CmMajor=0/22   CmMinor=0/23
        private static void setOVERWRITES_CONCRETE_DESIGN_ACI_318_08_IBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                ACI_318_08_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<ACI_318_08_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DnsMajor.HasValue) tableRow["DnsMajor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DnsMinor.HasValue) tableRow["DnsMinor"] = Adaptor.fromDouble(overwrite.DnsMinor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - ACI 318-11" (20 is a column, others are beams)
        // Frame=20   DesignSect="Program Determined"   FrameType="Program Determined"   RLLF=0   XLMajor=1   XLMinor=0   XKMajor=2     XKMinor=0       CmMajor=3   CmMinor=0   DnsMajor=0   DnsMinor=0   DsMajor=4   DsMinor=0
        // Frame=30   DesignSect="Program Determined"   FrameType="Program Determined"   RLLF=0   XLMajor=0   XLMinor=0
        // Frame=56   DesignSect="Program Determined"   FrameType="Sway Special"         RLLF=0   XLMajor=5   XLMinor=0

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - ACI 318-11" (20 is a column, others are beams)
        // // FrameType="Program Determined"   
        // // (Nonsway/sway Moment Factors) DnsMajor=0   DnsMinor=0   DsMajor=4   DsMinor=0
        // // (Moment Coefficient) CmMajor=0/22   CmMinor=0/23
        private static void setOVERWRITES_CONCRETE_DESIGN_ACI_318_11(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                ACI_318_11_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<ACI_318_11_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DnsMajor.HasValue) tableRow["DnsMajor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DnsMinor.HasValue) tableRow["DnsMinor"] = Adaptor.fromDouble(overwrite.DnsMinor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - ACI 318-14"
        // // FrameType="Program Determined" 
        // // (Nonsway/sway Moment Factors) DnsMajor=0   DnsMinor=0   DsMajor=4   DsMinor=0
        // // (Moment Coefficient) CmMajor=0/22   CmMinor=0/23
        private static void setOVERWRITES_CONCRETE_DESIGN_ACI_318_14(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                ACI_318_14_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<ACI_318_14_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DnsMajor.HasValue) tableRow["DnsMajor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DnsMinor.HasValue) tableRow["DnsMinor"] = Adaptor.fromDouble(overwrite.DnsMinor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - AS 3600-09"
        // // FrameType="Program Determined"   
        // // DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0
        // // kmMajor=0   kmMinor=0   
        private static void setOVERWRITES_CONCRETE_DESIGN_AS_3600_09(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                AS_3600_2009_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<AS_3600_2009_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DbMajor.HasValue) tableRow["DbMajor"] = Adaptor.fromDouble(overwrite.DbMajor.Value);
                if (overwrite.DbMinor.HasValue) tableRow["DbMinor"] = Adaptor.fromDouble(overwrite.DbMinor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.kmMajor.HasValue) tableRow["kmMajor"] = Adaptor.fromDouble(overwrite.kmMajor.Value);
                if (overwrite.kmMinor.HasValue) tableRow["kmMinor"] = Adaptor.fromDouble(overwrite.kmMinor.Value);

                table.Add(tableRow);
            }
        }

        private static void setOVERWRITES_CONCRETE_DESIGN_AS_3600_18(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                AS_3600_2018_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<AS_3600_2018_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DbMajor.HasValue) tableRow["DbMajor"] = Adaptor.fromDouble(overwrite.DbMajor.Value);
                if (overwrite.DbMinor.HasValue) tableRow["DbMinor"] = Adaptor.fromDouble(overwrite.DbMinor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.kmMajor.HasValue) tableRow["kmMajor"] = Adaptor.fromDouble(overwrite.kmMajor.Value);
                if (overwrite.kmMinor.HasValue) tableRow["kmMinor"] = Adaptor.fromDouble(overwrite.kmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - BS8110 97"
        // // FrameType="Program Determined"   
        // // BetaMajor=0   BetaMinor=0   
        // // DnsMajor=0   DnsMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0 
        private static void setOVERWRITES_CONCRETE_DESIGN_BS8110_97(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                BS_8110_1997_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<BS_8110_1997_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DnsMajor.HasValue) tableRow["DnsMajor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DnsMinor.HasValue) tableRow["DnsMinor"] = Adaptor.fromDouble(overwrite.DnsMinor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - CSA-A233-04"
        // // FrameType="Program Determined"   
        // // DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0 
        // // CmMajor=0   CmMinor=0     
        // // Rd=0   Ro=0   MaxAggSize=0
        private static void setOVERWRITES_CONCRETE_DESIGN_CSA_A233_04(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                CSA_A23_3_04_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<CSA_A23_3_04_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DbMajor.HasValue) tableRow["DbMajor"] = Adaptor.fromDouble(overwrite.DbMajor.Value);
                if (overwrite.DbMinor.HasValue) tableRow["DbMinor"] = Adaptor.fromDouble(overwrite.DbMinor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - CSA-A233-14"
        // // FrameType="Program Determined" 
        // // DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0    
        // // CmMajor=0   CmMinor=0     
        // // Rd=0   Ro=0   MaxAggSize=0
        private static void setOVERWRITES_CONCRETE_DESIGN_CSA_A233_14(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                CSA_A23_3_14_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<CSA_A23_3_14_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DbMajor.HasValue) tableRow["DbMajor"] = Adaptor.fromDouble(overwrite.DbMajor.Value);
                if (overwrite.DbMinor.HasValue) tableRow["DbMinor"] = Adaptor.fromDouble(overwrite.DbMinor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - EUROCODE 2-2004"
        // Frame=13   DesignSect="Program Determined"   FrameType="Program Determined"   RLLF=0   XLMajor=0   XLMinor=0   BetaMajor=0   BetaMinor=0     TanTheta=0   Kr=0   KPhi=0
        // Frame=20   DesignSect="Program Determined"   FrameType="Program Determined"   RLLF=0   XLMajor=1   XLMinor=0   BetaMajor=2   BetaMinor=0     TanTheta=0   Kr=0   KPhi=0
        // Frame=27   DesignSect="Program Determined"   FrameType="Program Determined"   RLLF=0   XLMajor=0   XLMinor=0                                 TanTheta=0   Kr=0   KPhi=0
        // Frame=30   DesignSect="Program Determined"   FrameType="Program Determined"   RLLF=0   XLMajor=0   XLMinor=0                                 TanTheta=0   Kr=0   KPhi=0
        // Frame=56   DesignSect="Program Determined"   FrameType="Program Determined"   RLLF=0   XLMajor=5   XLMinor=0                                 TanTheta=0   Kr=0   KPhi=0

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - EUROCODE 2-2004"
        // // FrameType="Program Determined"   
        // // (Effective length)BetaMajor=0   BetaMinor=0
        // // () TanTheta=0   
        // // (Correction Factor) Kr=0   
        // // (Creep Factor) KPhi=0
        private static void setOVERWRITES_CONCRETE_DESIGN_EUROCODE_2_2004(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                EN_2_2004_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<EN_2_2004_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                tableRow.Remove("XKMajor");
                tableRow.Remove("XKMinor");
                FrameDesignOverwrites<ConcreteDesignOverwrite, ConcreteDesignOverwriteProperties> overwriteGeneric = frame.FrameDesignOverwrites.ConcreteDesignOverwrite;
                if (overwriteGeneric.GenericOverwrites.XKMajor.HasValue) tableRow["BetaMajor"] = Adaptor.fromDouble(overwriteGeneric.GenericOverwrites.XKMajor.Value);
                if (overwriteGeneric.GenericOverwrites.XKMinor.HasValue) tableRow["BetaMinor"] = Adaptor.fromDouble(overwriteGeneric.GenericOverwrites.XKMinor.Value);

                tableRow["TanTheta"] = Adaptor.fromDouble(overwrite.TangentTheta);
                tableRow["Kr"] = Adaptor.fromDouble(overwrite.Kr);
                tableRow["KPhi"] = Adaptor.fromDouble(overwrite.KPhi);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - HONG KONG CP 2013"
        // // FrameType="Program Determined"   
        // // BetaMajor=0   BetaMinor=0   
        // // DnsMajor=0   DnsMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0  
        private static void setOVERWRITES_CONCRETE_DESIGN_HONG_KONG_CP_2013(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                HK_CP_2013_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<HK_CP_2013_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DnsMajor.HasValue) tableRow["DnsMajor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DnsMajor.HasValue) tableRow["DnsMinor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - INDIAN IS 456-2000"
        // // FrameType="Program Determined"
        private static void setOVERWRITES_CONCRETE_DESIGN_INDIAN_IS_456_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                IS_456_2000_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<IS_456_2000_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - ITALIAN NTC 2008"
        // // FrameType="Program Determined"   
        // // BetaMajor=0   BetaMinor=0   
        // // TanTheta=0   Kr=0   KPhi=0
        private static void setOVERWRITES_CONCRETE_DESIGN_ITALIAN_NTC_2008(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                NTC_2008_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<NTC_2008_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                tableRow.Remove("XKMajor");
                tableRow.Remove("XKMinor");
                FrameDesignOverwrites<ConcreteDesignOverwrite, ConcreteDesignOverwriteProperties> overwriteGeneric = frame.FrameDesignOverwrites.ConcreteDesignOverwrite;
                if (overwriteGeneric.GenericOverwrites.XKMajor.HasValue) tableRow["BetaMajor"] = Adaptor.fromDouble(overwriteGeneric.GenericOverwrites.XKMajor.Value);
                if (overwriteGeneric.GenericOverwrites.XKMinor.HasValue) tableRow["BetaMinor"] = Adaptor.fromDouble(overwriteGeneric.GenericOverwrites.XKMinor.Value);

                tableRow["TanTheta"] = Adaptor.fromDouble(overwrite.TangentTheta);
                tableRow["Kr"] = Adaptor.fromDouble(overwrite.Kr);
                tableRow["KPhi"] = Adaptor.fromDouble(overwrite.KPhi);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - KBC 2009"
        // // FrameType="Program Determined"   
        // // DnsMajor=0   DnsMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0  
        private static void setOVERWRITES_CONCRETE_DESIGN_KBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                KBC_2009_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<KBC_2009_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DnsMajor.HasValue) tableRow["DnsMajor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DnsMajor.HasValue) tableRow["DnsMinor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        private static void setOVERWRITES_CONCRETE_DESIGN_KBC_2016(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                KBC_2016_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<KBC_2016_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DnsMajor.HasValue) tableRow["DnsMajor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DnsMajor.HasValue) tableRow["DnsMinor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        //TABLE:  "OVERWRITES - CONCRETE DESIGN - MEXICAN RCDF 2004"
        //Frame=1   DesignSect="Program Determined"     FrameType=NonSway                RLLF=0   XLMajor=0   XLMinor=0
        // TABLE:  "OVERWRITES - CONCRETE DESIGN - MEXICAN RCDF 2004"
        // // FrameType=NonSway 
        private static void setOVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2004(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                RCDF_2004_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<RCDF_2004_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                table.Add(tableRow);
            }
        }

        private static void setOVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2017(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                RCDF_2017_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<RCDF_2017_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - NZS 3101-06"
        // // FrameType="Program Determined"   
        // // DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0  
        private static void setOVERWRITES_CONCRETE_DESIGN_NZS_3101_06(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                NZS_3101_2006_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<NZS_3101_2006_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DbMajor.HasValue) tableRow["DnsMajor"] = Adaptor.fromDouble(overwrite.DbMajor.Value);
                if (overwrite.DbMinor.HasValue) tableRow["DnsMinor"] = Adaptor.fromDouble(overwrite.DbMinor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - SINGAPORE CP 65-99"
        // // FrameType="Program Determined"   
        // // BetaMajor=0   BetaMinor=0 
        // // DnsMajor=0   DnsMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0 
        private static void setOVERWRITES_CONCRETE_DESIGN_SINGAPORE_CP_65_99(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                SS_CP_65_1999_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<SS_CP_65_1999_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.DnsMajor.HasValue) tableRow["DnsMajor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DnsMajor.HasValue) tableRow["DnsMinor"] = Adaptor.fromDouble(overwrite.DnsMajor.Value);
                if (overwrite.DsMajor.HasValue) tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor.Value);
                if (overwrite.DsMinor.HasValue) tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - TS 500-2000"
        // // FrameType="Program Determined"   
        // // BnsMajor=0   BnsMinor=0   BsMajor=0   BsMinor=0
        // // CmMajor=0   CmMinor=0 
        private static void setOVERWRITES_CONCRETE_DESIGN_TS_500_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                TS_500_2000_Overwrites overwrite =
                    frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GetItem<TS_500_2000_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesConcreteBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);

                if (overwrite.BnsMajor.HasValue) tableRow["BnsMajor"] = Adaptor.fromDouble(overwrite.BnsMajor.Value);
                if (overwrite.BnsMinor.HasValue) tableRow["BnsMinor"] = Adaptor.fromDouble(overwrite.BnsMinor.Value);
                if (overwrite.BsMajor.HasValue) tableRow["BsMajor"] = Adaptor.fromDouble(overwrite.BsMajor.Value);
                if (overwrite.BsMinor.HasValue) tableRow["BsMinor"] = Adaptor.fromDouble(overwrite.BsMinor.Value);
                if (overwrite.CmMajor.HasValue) tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor.Value);
                if (overwrite.CmMinor.HasValue) tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor.Value);

                table.Add(tableRow);
            }
        }
    }
}
