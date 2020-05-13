using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal class ReadDesignConcreteOverwrites : ReadDesignOverwrites
    {
        protected ReadDesignConcreteOverwrites() { }

        internal static void DefineOverwrites(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_ACI_318_08_IBC_2009, setOVERWRITES_CONCRETE_DESIGN_ACI_318_08_IBC_2009);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_ACI_318_11, setOVERWRITES_CONCRETE_DESIGN_ACI_318_11);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_ACI_318_14, setOVERWRITES_CONCRETE_DESIGN_ACI_318_14);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_AS_3600_09, setOVERWRITES_CONCRETE_DESIGN_AS_3600_09);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_AS_3600_18, setOVERWRITES_CONCRETE_DESIGN_AS_3600_18);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_BS8110_97, setOVERWRITES_CONCRETE_DESIGN_BS8110_97);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_CSA_A233_04, setOVERWRITES_CONCRETE_DESIGN_CSA_A233_04);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_CSA_A233_14, setOVERWRITES_CONCRETE_DESIGN_CSA_A233_14);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_EUROCODE_2_2004, setOVERWRITES_CONCRETE_DESIGN_EUROCODE_2_2004);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_HONG_KONG_CP_2013, setOVERWRITES_CONCRETE_DESIGN_HONG_KONG_CP_2013);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_INDIAN_IS_456_2000, setOVERWRITES_CONCRETE_DESIGN_INDIAN_IS_456_2000);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_ITALIAN_NTC_2008, setOVERWRITES_CONCRETE_DESIGN_ITALIAN_NTC_2008);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_KBC_2009, setOVERWRITES_CONCRETE_DESIGN_KBC_2009);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_KBC_2016, setOVERWRITES_CONCRETE_DESIGN_KBC_2016);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2004, setOVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2004);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2017, setOVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2017);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_NZS_3101_06, setOVERWRITES_CONCRETE_DESIGN_NZS_3101_06);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_SINGAPORE_CP_65_99, setOVERWRITES_CONCRETE_DESIGN_SINGAPORE_CP_65_99);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_CONCRETE_DESIGN_TS_500_2000, setOVERWRITES_CONCRETE_DESIGN_TS_500_2000);
        }

        // //  Concrete
        // // ConcreteDesignSect="Program Determined"/FSEC1/...   
        // // (Reduced Live Load Factor)RLLF=0/11   
        // // (Unbraced length ratios) XLMajor=0/12   XLMinor=0/13
        // // (Effective length)XKMajor=0/15   XKMinor=0/16 
        private static ConcreteDesignOverwrite setOverwritesConcreteBasic(Model model, Dictionary<string, string> tableRow)
        {
            return new ConcreteDesignOverwrite
            {
                DesignSection = getNullableFrameSection(model, tableRow["DesignSect"]),
                RLLF = Adaptor.toDouble(tableRow["RLLF"]),
                XLMajor = Adaptor.toDouble(tableRow["XLMajor"]),
                XLMinor = Adaptor.toDouble(tableRow["XLMinor"]),
                XKMajor = getNullableDouble(tableRow, "XKMajor"),
                XKMinor = getNullableDouble(tableRow, "XKMinor")
            };
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - ACI 318-08/IBC2009"
        // // FrameType="Program Determined"
        // // (Nonsway/sway Moment Factors) DnsMajor=0   DnsMinor=0   DsMajor=4   DsMinor=0
        // // (Moment Coefficient) CmMajor=0/22   CmMinor=0/23
        private static void setOVERWRITES_CONCRETE_DESIGN_ACI_318_08_IBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new ACI_318_08_Overwrites
                    {
                        FrameType = getNullableEnum<ACI_318_08_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DnsMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DnsMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor")
                    }
                );
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
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new ACI_318_11_Overwrites
                    {
                        FrameType = getNullableEnum<ACI_318_11_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DnsMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DnsMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor")
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - ACI 318-14"
        // // FrameType="Program Determined" 
        // // (Nonsway/sway Moment Factors) DnsMajor=0   DnsMinor=0   DsMajor=4   DsMinor=0
        // // (Moment Coefficient) CmMajor=0/22   CmMinor=0/23
        private static void setOVERWRITES_CONCRETE_DESIGN_ACI_318_14(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new ACI_318_14_Overwrites
                    {
                        FrameType = getNullableEnum<ACI_318_14_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DnsMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DnsMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor")
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - AS 3600-09"
        // // FrameType="Program Determined"   
        // // DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0
        // // kmMajor=0   kmMinor=0   
        private static void setOVERWRITES_CONCRETE_DESIGN_AS_3600_09(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new AS_3600_2009_Overwrites
                    {
                        FrameType = getNullableEnum<AS_3600_2009_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DbMajor = getNullableDouble(tableRow, "DbMajor"),
                        DbMinor = getNullableDouble(tableRow, "DbMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        kmMajor = getNullableDouble(tableRow, "kmMajor"),
                        kmMinor = getNullableDouble(tableRow, "kmMinor")
                    }
                );
            }
        }

        private static void setOVERWRITES_CONCRETE_DESIGN_AS_3600_18(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new AS_3600_2018_Overwrites
                    {
                        FrameType = getNullableEnum<AS_3600_2018_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DbMajor = getNullableDouble(tableRow, "DbMajor"),
                        DbMinor = getNullableDouble(tableRow, "DbMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        kmMajor = getNullableDouble(tableRow, "kmMajor"),
                        kmMinor = getNullableDouble(tableRow, "kmMinor")
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - BS8110 97"
        // // FrameType="Program Determined"   
        // // BetaMajor=0   BetaMinor=0   
        // // DnsMajor=0   DnsMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0 
        private static void setOVERWRITES_CONCRETE_DESIGN_BS8110_97(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new BS_8110_1997_Overwrites
                    {
                        FrameType = getNullableEnum<BS_8110_1997_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DnsMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DnsMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor")
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - CSA-A233-04"
        // // FrameType="Program Determined"   
        // // DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0 
        // // CmMajor=0   CmMinor=0     
        // // Rd=0   Ro=0   MaxAggSize=0
        private static void setOVERWRITES_CONCRETE_DESIGN_CSA_A233_04(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new CSA_A23_3_04_Overwrites
                    {
                        FrameType = getNullableEnum<CSA_A23_3_04_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DbMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DbMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor"),
                        Rd = Adaptor.toDouble(tableRow["Rd"]),
                        Ro = Adaptor.toDouble(tableRow["Ro"]),
                        MaxAggregateSize = Adaptor.toDouble(tableRow["MaxAggSize"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - CSA-A233-14"
        // // FrameType="Program Determined" 
        // // DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0    
        // // CmMajor=0   CmMinor=0     
        // // Rd=0   Ro=0   MaxAggSize=0
        private static void setOVERWRITES_CONCRETE_DESIGN_CSA_A233_14(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new CSA_A23_3_14_Overwrites
                    {
                        FrameType = getNullableEnum<CSA_A23_3_14_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DbMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DbMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor"),
                        Rd = Adaptor.toDouble(tableRow["Rd"]),
                        Ro = Adaptor.toDouble(tableRow["Ro"]),
                        MaxAggregateSize = Adaptor.toDouble(tableRow["MaxAggSize"])
                    }
                );
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
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites.XKMajor = getNullableDouble(tableRow, "BetaMajor");
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites.XKMinor = getNullableDouble(tableRow, "BetaMinor");
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new EN_2_2004_Overwrites
                    {
                        FrameType = getNullableEnum<EN_2_2004_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        TangentTheta = Adaptor.toDouble(tableRow["TanTheta"]),
                        Kr = Adaptor.toDouble(tableRow["Kr"]),
                        KPhi = Adaptor.toDouble(tableRow["KPhi"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - HONG KONG CP 2013"
        // // FrameType="Program Determined"   
        // // BetaMajor=0   BetaMinor=0   
        // // DnsMajor=0   DnsMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0  
        private static void setOVERWRITES_CONCRETE_DESIGN_HONG_KONG_CP_2013(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites.XKMajor = getNullableDouble(tableRow, "BetaMajor");
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites.XKMinor = getNullableDouble(tableRow, "BetaMinor");
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new HK_CP_2013_Overwrites
                    {
                        FrameType = getNullableEnum<HK_CP_2013_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DnsMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DnsMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor")
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - INDIAN IS 456-2000"
        // // FrameType="Program Determined"
        private static void setOVERWRITES_CONCRETE_DESIGN_INDIAN_IS_456_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new IS_456_2000_Overwrites
                    {
                        FrameType = getNullableEnum<IS_456_2000_Overwrites.FrameTypes>(tableRow["FrameType"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - ITALIAN NTC 2008"
        // // FrameType="Program Determined"   
        // // BetaMajor=0   BetaMinor=0   
        // // TanTheta=0   Kr=0   KPhi=0
        private static void setOVERWRITES_CONCRETE_DESIGN_ITALIAN_NTC_2008(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites.XKMajor = getNullableDouble(tableRow, "BetaMajor");
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites.XKMinor = getNullableDouble(tableRow, "BetaMinor");
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new NTC_2008_Overwrites
                    {
                        FrameType = getNullableEnum<NTC_2008_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        TangentTheta = Adaptor.toDouble(tableRow["TanTheta"]),
                        Kr = Adaptor.toDouble(tableRow["Kr"]),
                        KPhi = Adaptor.toDouble(tableRow["KPhi"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - KBC 2009"
        // // FrameType="Program Determined"   
        // // DnsMajor=0   DnsMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0  
        private static void setOVERWRITES_CONCRETE_DESIGN_KBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new KBC_2009_Overwrites
                    {
                        FrameType = getNullableEnum<KBC_2009_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DnsMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DnsMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor")
                    }
                );
            }
        }

        private static void setOVERWRITES_CONCRETE_DESIGN_KBC_2016(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new KBC_2016_Overwrites
                    {
                        FrameType = getNullableEnum<KBC_2016_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DnsMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DnsMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor")
                    }
                );
            }
        }

        //TABLE:  "OVERWRITES - CONCRETE DESIGN - MEXICAN RCDF 2004"
        //Frame=1   DesignSect="Program Determined"     FrameType=NonSway                RLLF=0   XLMajor=0   XLMinor=0
        // TABLE:  "OVERWRITES - CONCRETE DESIGN - MEXICAN RCDF 2004"
        // // FrameType=NonSway 
        private static void setOVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2004(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new RCDF_2004_Overwrites
                    {
                        FrameType = getNullableEnum<RCDF_2004_Overwrites.FrameTypes>(tableRow["FrameType"])
                    }
                );
            }
        }

        private static void setOVERWRITES_CONCRETE_DESIGN_MEXICAN_RCDF_2017(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new RCDF_2017_Overwrites
                    {
                        FrameType = getNullableEnum<RCDF_2017_Overwrites.FrameTypes>(tableRow["FrameType"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - NZS 3101-06"
        // // FrameType="Program Determined"   
        // // DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0  
        private static void setOVERWRITES_CONCRETE_DESIGN_NZS_3101_06(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new NZS_3101_2006_Overwrites
                    {
                        FrameType = getNullableEnum<NZS_3101_2006_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DbMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DbMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor"),
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - SINGAPORE CP 65-99"
        // // FrameType="Program Determined"   
        // // BetaMajor=0   BetaMinor=0 
        // // DnsMajor=0   DnsMinor=0   DsMajor=0   DsMinor=0
        // // CmMajor=0   CmMinor=0 
        private static void setOVERWRITES_CONCRETE_DESIGN_SINGAPORE_CP_65_99(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites.XKMajor = getNullableDouble(tableRow, "BetaMajor");
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites.XKMinor = getNullableDouble(tableRow, "BetaMinor");
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new SS_CP_65_1999_Overwrites
                    {
                        FrameType = getNullableEnum<SS_CP_65_1999_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        DnsMajor = getNullableDouble(tableRow, "DnsMajor"),
                        DnsMinor = getNullableDouble(tableRow, "DnsMinor"),
                        DsMajor = getNullableDouble(tableRow, "DsMajor"),
                        DsMinor = getNullableDouble(tableRow, "DsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor")
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - CONCRETE DESIGN - TS 500-2000"
        // // FrameType="Program Determined"   
        // // BnsMajor=0   BnsMinor=0   BsMajor=0   BsMinor=0
        // // CmMajor=0   CmMinor=0 
        private static void setOVERWRITES_CONCRETE_DESIGN_TS_500_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.GenericOverwrites = setOverwritesConcreteBasic(model, tableRow);
                frame.FrameDesignOverwrites.ConcreteDesignOverwrite.UpdateItem(
                    new TS_500_2000_Overwrites
                    {
                        FrameType = getNullableEnum<TS_500_2000_Overwrites.FrameTypes>(tableRow["FrameType"]),
                        BnsMajor = getNullableDouble(tableRow, "BnsMajor"),
                        BnsMinor = getNullableDouble(tableRow, "BnsMinor"),
                        BsMajor = getNullableDouble(tableRow, "BsMajor"),
                        BsMinor = getNullableDouble(tableRow, "BsMinor"),
                        CmMajor = getNullableDouble(tableRow, "CmMajor"),
                        CmMinor = getNullableDouble(tableRow, "CmMinor"),
                    }
                );
            }
        }
    }
}
