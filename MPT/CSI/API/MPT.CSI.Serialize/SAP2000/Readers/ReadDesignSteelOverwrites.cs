using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal class ReadDesignSteelOverwrites : ReadDesignOverwrites
    {
        protected ReadDesignSteelOverwrites() { }

        internal static void DefineOverwrites(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_AISC360_05_IBC2006, setOVERWRITES_STEEL_DESIGN_AISC360_05_IBC2006);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_AISC_360_10, setOVERWRITES_STEEL_DESIGN_AISC_360_10);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_AISC_360_16, setOVERWRITES_STEEL_DESIGN_AISC_360_16);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_AS_4100_1998, setOVERWRITES_STEEL_DESIGN_AS_4100_1998);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_BS5950_2000, setOVERWRITES_STEEL_DESIGN_BS5950_2000);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_CSA_S16_09, setOVERWRITES_STEEL_DESIGN_CSA_S16_09);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_CSA_S16_14, setOVERWRITES_STEEL_DESIGN_CSA_S16_14);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_EUROCODE_3_2005, setOVERWRITES_STEEL_DESIGN_EUROCODE_3_2005);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_INDIAN_IS_800_2007, setOVERWRITES_STEEL_DESIGN_INDIAN_IS_800_2007);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2008, setOVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2008);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2018, setOVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2018);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_KBC_2009, setOVERWRITES_STEEL_DESIGN_KBC_2009);
            reader.ReadSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_NZS_3404_1997, setOVERWRITES_STEEL_DESIGN_NZS_3404_1997);
        }

        // // Steel
        // // DesignSect="Program Determined"/FSEC1/...   
        // // Fy=0/32   
        // // (Reduced Live Load Factor)RLLF=0/11   
        // // (Net Area to Total Area ratio)AreaRatio=0/10 
        // // (Unbraced length ratios) XLMajor=0/12   XLMinor=0/13   XLLTB=0/14
        // // (Effective length)K1Major=0/15   K1Minor=0/16   K2Major=0/17   	   K2Minor=0/18   	 KLTB=0/41
        // // CheckDefl="Program Determined"/Yes/No 
        // // DeflType="Program Determined"/Both/Ratio/Absolute   
        // // (Ratio, L/)DLRat=0/1   SDLAndLLRat=0/2   LLRat=0/400   TotalRat=0/500   NetRat=0/3   
        // // (Absolute) DLAbs=0/4   SDLAndLLAbs=0/5   LLAbs=0/6     TotalAbs=0/7     NetAbs=0/8   
        // // (Specified Camber)SpecCamber=0/9   
        // // DCLimit=0/40

        private static SteelDesignOverwrite setOverwritesSteelBasic(Model model, Dictionary<string, string> tableRow)
        {
            SteelDesignOverwrite overwrite = new SteelDesignOverwrite
            {
                DesignSection = getNullableFrameSection(model, tableRow["DesignSect"]),
                Fy = Adaptor.toDouble(tableRow["Fy"]),
                RLLF = Adaptor.toDouble(tableRow["RLLF"]),
                AreaRatio = Adaptor.toDouble(tableRow["AreaRatio"]),
                XLMajor = Adaptor.toDouble(tableRow["XLMajor"]),
                XLMinor = Adaptor.toDouble(tableRow["XLMinor"]),
                XLLTB = Adaptor.toDouble(tableRow["XLLTB"]),
                K1Major = getNullableDouble(tableRow, "K1Major"),
                K1Minor = getNullableDouble(tableRow, "K1Minor"),
                K2Major = getNullableDouble(tableRow, "K2Major"),
                K2Minor = getNullableDouble(tableRow, "K2Minor"),
                KLTB = Adaptor.toDouble(tableRow["KLTB"]),
                IsDeflectionConsidered = getNullableYesNo(tableRow["CheckDefl"]),
                DeflectionCheckType = getNullableEnum<SteelDesignOverwrite.DeflectionCheckTypes>(tableRow["DeflType"]),
                RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                AbsoluteDL = Adaptor.toDouble(tableRow["DLAbs"]),
                AbsoluteSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLAbs"]),
                AbsoluteLL = Adaptor.toDouble(tableRow["LLAbs"]),
                AbsoluteTotal = Adaptor.toDouble(tableRow["TotalAbs"]),
                AbsoluteNet = Adaptor.toDouble(tableRow["NetAbs"]),
                SpecifiedCamber = Adaptor.toDouble(tableRow["SpecCamber"]),
                DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["DCLimit"])
            };
            return overwrite;
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - AISC 360-10"
        // // FrameType="Program Determined"/SMF
        // // HSSReduceT="Program Determined"   HSSWelding="Program Determined"
        // // Omega0=0   
        // // (Expected to specified Fy ratio)Ry=0/49  
        // // (Nonsway/sway Moment Factors)B1Major=0   B1Minor=0   B2Major=0   B2Minor=0  
        // // (Moment Coefficient) CmMajor=0/22   CmMinor=0/23
        // // (Bending coefficient)Cb=0      
        // // (Capacities)Pnc=0   Pnt=0   Mn3=0   Mn2=0   Vn2=0   Vn3=0
        private static void setOVERWRITES_STEEL_DESIGN_AISC360_05_IBC2006(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new AISC_360_05_Overwrites
                    {
                        FrameType = getNullableEnum<AISC_360_05_Preferences.FrameTypes>(tableRow["FrameType"]),
                        ReduceHSSThickness = getNullableYesNo(tableRow["HSSReduceT"]),
                        HSSWeldingType = getNullableEnum<AISC_360_05_Preferences.HSSWeldingTypes>(tableRow["HSSWelding"]),
                        Omega0 = Adaptor.toDouble(tableRow["Omega0"]),
                        Ry = Adaptor.toDouble(tableRow["Ry"]),
                        B1Major = Adaptor.toDouble(tableRow["B1Major"]),
                        B1Minor = Adaptor.toDouble(tableRow["B1Minor"]),
                        B2Major = Adaptor.toDouble(tableRow["B2Major"]),
                        B2Minor = Adaptor.toDouble(tableRow["B2Minor"]),
                        CmMajor = Adaptor.toDouble(tableRow["CmMajor"]),
                        CmMinor = Adaptor.toDouble(tableRow["CmMinor"]),
                        Cb = Adaptor.toDouble(tableRow["Cb"]),
                        Pnc = Adaptor.toDouble(tableRow["Pnc"]),
                        Pnt = Adaptor.toDouble(tableRow["Pnt"]),
                        Mn3 = Adaptor.toDouble(tableRow["Mn3"]),
                        Mn2 = Adaptor.toDouble(tableRow["Mn2"]),
                        Vn2 = Adaptor.toDouble(tableRow["Vn2"]),
                        Vn3 = Adaptor.toDouble(tableRow["Vn3"])
                    }
                );
            }
        }


        //TABLE:  "OVERWRITES - STEEL DESIGN - AISC 360-10"
        // Frame=39   DesignSect="Program Determined"   FrameType="Program Determined"   Fy=0   RLLF=0   AreaRatio=0
        // XLMajor=0   XLMinor=0   XLLTB=0
        // K1Major=0   K1Minor=0   K2Major=0   K2Minor=0   KLTB=0
        // CmMajor=0   CmMinor=0
        // Cb=0 
        // B1Major=0   B1Minor=0   B2Major=0   B2Minor=0   HSSReduceT="Program Determined"   HSSWelding="Program Determined"   Omega0=0   Ry=0   Pnc=0   Pnt=0   Mn3=0   Mn2=0   Vn2=0   Vn3=0
        // CheckDefl="Program Determined" 
        // DeflType="Program Determined"   DLRat=0   SDLAndLLRat=0   LLRat=0   TotalRat=0   NetRat=0   DLAbs=0   SDLAndLLAbs=0   LLAbs=0   TotalAbs=0   NetAbs=0   SpecCamber=0   DCLimit=0

        // Frame=40   DesignSect="Program Determined"   FrameType="Program Determined"   Fy=0   RLLF=0   AreaRatio=0   XLMajor=0   XLMinor=0   XLLTB=0   K1Major=0   K1Minor=0   K2Major=0   K2Minor=0   KLTB=0   CmMajor=0   CmMinor=0   Cb=0 _
        // B1Major = 0   B1Minor=0   B2Major=0   B2Minor=0   HSSReduceT="Program Determined"   HSSWelding=ERW Omega0 = 7   Ry=0   Pnc=0   Pnt=0   Mn3=900   Mn2=0   Vn2=0   Vn3=0   CheckDefl=Yes DeflType = "Program Determined"   DLRat=0 _
        //   SDLAndLLRat = 0   LLRat=400   TotalRat=500   NetRat=0   DLAbs=0   SDLAndLLAbs=0   LLAbs=0   TotalAbs=0   NetAbs=0   SpecCamber=0   DCLimit=0

        // TABLE:  "OVERWRITES - STEEL DESIGN - AISC 360-10"
        // // FrameType="Program Determined"/SMF
        // // HSSReduceT="Program Determined"   HSSWelding="Program Determined"
        // // Omega0=0   
        // // (Expected to specified Fy ratio)Ry=0/49  
        // // (Nonsway/sway Moment Factors)B1Major=0   B1Minor=0   B2Major=0   B2Minor=0  
        // // (Moment Coefficient) CmMajor=0/22   CmMinor=0/23
        // // (Bending coefficient)Cb=0      
        // // (Capacities)Pnc=0   Pnt=0   Mn3=0   Mn2=0   Vn2=0   Vn3=0
        private static void setOVERWRITES_STEEL_DESIGN_AISC_360_10(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new AISC_360_10_Overwrites
                    {
                        FrameType = getNullableEnum<AISC_360_10_Preferences.FrameTypes>(tableRow["FrameType"]),
                        ReduceHSSThickness = getNullableYesNo(tableRow["HSSReduceT"]),
                        HSSWeldingType = getNullableEnum<AISC_360_10_Preferences.HSSWeldingTypes>(tableRow["HSSWelding"]),
                        Omega0 = Adaptor.toDouble(tableRow["Omega0"]),
                        Ry = Adaptor.toDouble(tableRow["Ry"]),
                        B1Major = Adaptor.toDouble(tableRow["B1Major"]),
                        B1Minor = Adaptor.toDouble(tableRow["B1Minor"]),
                        B2Major = Adaptor.toDouble(tableRow["B2Major"]),
                        B2Minor = Adaptor.toDouble(tableRow["B2Minor"]),
                        CmMajor = Adaptor.toDouble(tableRow["CmMajor"]),
                        CmMinor = Adaptor.toDouble(tableRow["CmMinor"]),
                        Cb = Adaptor.toDouble(tableRow["Cb"]),
                        Pnc = Adaptor.toDouble(tableRow["Pnc"]),
                        Pnt = Adaptor.toDouble(tableRow["Pnt"]),
                        Mn3 = Adaptor.toDouble(tableRow["Mn3"]),
                        Mn2 = Adaptor.toDouble(tableRow["Mn2"]),
                        Vn2 = Adaptor.toDouble(tableRow["Vn2"]),
                        Vn3 = Adaptor.toDouble(tableRow["Vn3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - AISC 360-10"
        // // FrameType="Program Determined"/SMF
        // // HSSReduceT="Program Determined"   HSSWelding="Program Determined"
        // // Omega0=0   
        // // (Expected to specified Fy ratio)Ry=0/49  
        // // (Nonsway/sway Moment Factors)B1Major=0   B1Minor=0   B2Major=0   B2Minor=0  
        // // (Moment Coefficient) CmMajor=0/22   CmMinor=0/23
        // // (Bending coefficient)Cb=0      
        // // (Capacities)Pnc=0   Pnt=0   Mn3=0   Mn2=0   Vn2=0   Vn3=0
        private static void setOVERWRITES_STEEL_DESIGN_AISC_360_16(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new AISC_360_16_Overwrites
                    {
                        FrameType = getNullableEnum<AISC_360_16_Preferences.FrameTypes>(tableRow["FrameType"]),
                        ReduceHSSThickness = getNullableYesNo(tableRow["HSSReduceT"]),
                        HSSWeldingType = getNullableEnum<AISC_360_16_Preferences.HSSWeldingTypes>(tableRow["HSSWelding"]),
                        Omega0 = Adaptor.toDouble(tableRow["Omega0"]),
                        Ry = Adaptor.toDouble(tableRow["Ry"]),
                        B1Major = Adaptor.toDouble(tableRow["B1Major"]),
                        B1Minor = Adaptor.toDouble(tableRow["B1Minor"]),
                        B2Major = Adaptor.toDouble(tableRow["B2Major"]),
                        B2Minor = Adaptor.toDouble(tableRow["B2Minor"]),
                        CmMajor = Adaptor.toDouble(tableRow["CmMajor"]),
                        CmMinor = Adaptor.toDouble(tableRow["CmMinor"]),
                        Cb = Adaptor.toDouble(tableRow["Cb"]),
                        Pnc = Adaptor.toDouble(tableRow["Pnc"]),
                        Pnt = Adaptor.toDouble(tableRow["Pnt"]),
                        Mn3 = Adaptor.toDouble(tableRow["Mn3"]),
                        Mn2 = Adaptor.toDouble(tableRow["Mn2"]),
                        Vn2 = Adaptor.toDouble(tableRow["Vn2"]),
                        Vn3 = Adaptor.toDouble(tableRow["Vn3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - AS 4100-1998"
        // // FrameType="Program Determined"   
        // // SteelType="Program Determined"   
        // // keMajor=0   keMinor=0   keSwayMajor=0   keSwayMinor=0   
        // // ktLTB=0 krLTB=0   klLTB=0   
        // // CmMajor=0   CmMinor=0   
        // // Alpham=0   Alphas=0   
        // // Kf=0   KtAxial=0   
        // // DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0   
        // // Ns=0   Nt=0   Ms3=0   Ms2=0   Mb=0   Vv2=0   Vv3=0
        private static void setOVERWRITES_STEEL_DESIGN_AS_4100_1998(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites.K1Major = Adaptor.toDouble(tableRow["keMajor"]);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites.K1Minor = Adaptor.toDouble(tableRow["keMinor"]);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites.K2Major = Adaptor.toDouble(tableRow["keSwayMajor"]);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites.K2Minor = Adaptor.toDouble(tableRow["keSwayMinor"]);

                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new AS_4100_1998_Overwrites
                    {
                        FrameType = getNullableEnum<AS_4100_1998_Preferences.FrameTypes>(tableRow["FrameType"]),
                        SteelType = getNullableEnum<AS_4100_1998_Preferences.SteelTypes>(tableRow["SteelType"]),
                        CmMajor = Adaptor.toDouble(tableRow["CmMajor"]),
                        CmMinor = Adaptor.toDouble(tableRow["CmMinor"]),
                        ktLTB = Adaptor.toDouble(tableRow["ktLTB"]),
                        krLTB = Adaptor.toDouble(tableRow["krLTB"]),
                        klLTB = Adaptor.toDouble(tableRow["klLTB"]),
                        Alpha_m = Adaptor.toDouble(tableRow["Alpham"]),
                        Alpha_s = Adaptor.toDouble(tableRow["Alphas"]),
                        Kf = Adaptor.toDouble(tableRow["Kf"]),
                        KtAxial = Adaptor.toDouble(tableRow["KtAxial"]),
                        DbMajor = Adaptor.toDouble(tableRow["DbMajor"]),
                        DbMinor = Adaptor.toDouble(tableRow["DbMinor"]),
                        DsMajor = Adaptor.toDouble(tableRow["DsMajor"]),
                        DsMinor = Adaptor.toDouble(tableRow["DsMinor"]),
                        Ns = Adaptor.toDouble(tableRow["Ns"]),
                        Nt = Adaptor.toDouble(tableRow["Nt"]),
                        Ms3 = Adaptor.toDouble(tableRow["Ms3"]),
                        Ms2 = Adaptor.toDouble(tableRow["Ms2"]),
                        Mb = Adaptor.toDouble(tableRow["Mb"]),
                        Vv2 = Adaptor.toDouble(tableRow["Vv2"]),
                        Vv3 = Adaptor.toDouble(tableRow["Vv3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - BS5950 2000"
        // // FrameType="Program Determined"   
        // // MMajor=0   MMinor=0   MLT=0   
        // // Pc=0   Pt=0   Mc3=0   Mc2=0   Mb=0   Pv2=0   Pv3=0 
        private static void setOVERWRITES_STEEL_DESIGN_BS5950_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new BS_5950_2000_Overwrites
                    {
                        FrameType = getNullableEnum<BS_5950_2000_Preferences.FrameTypes>(tableRow["FrameType"]),
                        MMajor = Adaptor.toDouble(tableRow["MMajor"]),
                        MMinor = Adaptor.toDouble(tableRow["MMinor"]),
                        MLT = Adaptor.toDouble(tableRow["MLT"]),
                        Pc = Adaptor.toDouble(tableRow["Pc"]),
                        Pt = Adaptor.toDouble(tableRow["Pt"]),
                        Mc3 = Adaptor.toDouble(tableRow["Mc3"]),
                        Mc2 = Adaptor.toDouble(tableRow["Mc2"]),
                        Mb = Adaptor.toDouble(tableRow["Mb"]),
                        Pv2 = Adaptor.toDouble(tableRow["Pv2"]),
                        Pv3 = Adaptor.toDouble(tableRow["Pv3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - CSA-S16-09"
        // // FrameType="Program Determined"   
        // // Omega1Major=0   Omega1Minor=0   Omega2=0   
        // // U1Major=0  U1Minor=0   U2Major=0   U2Minor=0   
        // // NPower=0   Ry=0   
        // // Cr=0   Tr=0   Mr3=0   Mr2=0   Vr2=0   Vr3=0
        private static void setOVERWRITES_STEEL_DESIGN_CSA_S16_09(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new CSA_S16_09_Overwrites
                    {
                        FrameType = getNullableEnum<CSA_S16_09_Preferences.FrameTypes>(tableRow["FrameType"]),
                        NPower = Adaptor.toDouble(tableRow["NPower"]),
                        Ry = Adaptor.toDouble(tableRow["Ry"]),
                        U1Major = Adaptor.toDouble(tableRow["U1Major"]),
                        U1Minor = Adaptor.toDouble(tableRow["U1Minor"]),
                        U2Major = Adaptor.toDouble(tableRow["U2Major"]),
                        U2Minor = Adaptor.toDouble(tableRow["U2Minor"]),
                        Omega1Major = Adaptor.toDouble(tableRow["Omega1Major"]),
                        Omega1Minor = Adaptor.toDouble(tableRow["Omega1Minor"]),
                        Omega2 = Adaptor.toDouble(tableRow["Omega2"]),
                        Cr = Adaptor.toDouble(tableRow["Cr"]),
                        Tr = Adaptor.toDouble(tableRow["Tr"]),
                        Mr3 = Adaptor.toDouble(tableRow["Mr3"]),
                        Mr2 = Adaptor.toDouble(tableRow["Mr2"]),
                        Vr2 = Adaptor.toDouble(tableRow["Vr2"]),
                        Vr3 = Adaptor.toDouble(tableRow["Vr3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - CSA S16-14"
        // // FrameType="Program Determined"   
        // // Omega1Major=0   Omega1Minor=0   Omega2=0   
        // // U1Major=0    U1Minor=0   U2Major=0   U2Minor=0   
        // // NPower=0   Ry=0   
        // // Cr=0   Tr=0   Mr3=0   Mr2=0   Vr2=0   Vr3=0 
        private static void setOVERWRITES_STEEL_DESIGN_CSA_S16_14(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new CSA_S16_14_Overwrites
                    {
                        FrameType = getNullableEnum<CSA_S16_14_Preferences.FrameTypes>(tableRow["FrameType"]),
                        NPower = Adaptor.toDouble(tableRow["NPower"]),
                        Ry = Adaptor.toDouble(tableRow["Ry"]),
                        U1Major = Adaptor.toDouble(tableRow["U1Major"]),
                        U1Minor = Adaptor.toDouble(tableRow["U1Minor"]),
                        U2Major = Adaptor.toDouble(tableRow["U2Major"]),
                        U2Minor = Adaptor.toDouble(tableRow["U2Minor"]),
                        Omega1Major = Adaptor.toDouble(tableRow["Omega1Major"]),
                        Omega1Minor = Adaptor.toDouble(tableRow["Omega1Minor"]),
                        Omega2 = Adaptor.toDouble(tableRow["Omega2"]),
                        Cr = Adaptor.toDouble(tableRow["Cr"]),
                        Tr = Adaptor.toDouble(tableRow["Tr"]),
                        Mr3 = Adaptor.toDouble(tableRow["Mr3"]),
                        Mr2 = Adaptor.toDouble(tableRow["Mr2"]),
                        Vr2 = Adaptor.toDouble(tableRow["Vr2"]),
                        Vr3 = Adaptor.toDouble(tableRow["Vr3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - EUROCODE 3-2005"
        // //FrameType="Program Determined"    
        // // SectClass="Program Determined"   
        // // Rolled=Yes   
        // // kyyMajor=0   kzzMinor=0   C1=0   kzy=0   kyz=0  
        // // CurveYY="Program Determined"   
        // // CurveZZ="Program Determined"   
        // // CurveLTB="Program Determined"   
        // // Omega=0   GammaOV=0   Iw=0   
        // // NcrT=0   NcrTF=0   Nc=0   Nt=0   Mc3=0   Mc2=0   Mb=0   V2=0   V3=0   
        private static void setOVERWRITES_STEEL_DESIGN_EUROCODE_3_2005(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new EN_3_2005_Overwrites
                    {
                        FrameType = getNullableEnum<EN_3_2005_Preferences.FrameTypes>(tableRow["FrameType"]),
                        IsRolled = getNullableYesNo(tableRow["Rolled"]),
                        SectionClass = getNullableEnum<EN_3_2005_Overwrites.SectionClasses>(tableRow["SectClass"]),
                        BucklingCurveYY = getNullableEnum<EN_3_2005_Overwrites.BucklingCurves>(tableRow["CurveYY"]),
                        BucklingCurveZZ = getNullableEnum<EN_3_2005_Overwrites.BucklingCurves>(tableRow["CurveZZ"]),
                        BucklingCurveLTB = getNullableEnum<EN_3_2005_Overwrites.BucklingCurves>(tableRow["CurveLTB"]),
                        kyyMajor = Adaptor.toDouble(tableRow["kyyMajor"]),
                        kzzMinor = Adaptor.toDouble(tableRow["kzzMinor"]),
                        C1 = Adaptor.toDouble(tableRow["C1"]),
                        kzy = Adaptor.toDouble(tableRow["kzy"]),
                        kyz = Adaptor.toDouble(tableRow["kyz"]),
                        Omega = Adaptor.toDouble(tableRow["Omega"]),
                        GammaOV = Adaptor.toDouble(tableRow["GammaOV"]),
                        Iw = Adaptor.toDouble(tableRow["Iw"]),
                        NcrT = Adaptor.toDouble(tableRow["NcrT"]),
                        NcrTF = Adaptor.toDouble(tableRow["NcrTF"]),
                        Nc = Adaptor.toDouble(tableRow["Nc"]),
                        Nt = Adaptor.toDouble(tableRow["Nt"]),
                        Mc3 = Adaptor.toDouble(tableRow["Mc3"]),
                        Mc2 = Adaptor.toDouble(tableRow["Mc2"]),
                        Mb = Adaptor.toDouble(tableRow["Mb"]),
                        V2 = Adaptor.toDouble(tableRow["V2"]),
                        V3 = Adaptor.toDouble(tableRow["V3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - INDIAN IS 800:2007"
        // // FrameType="Program Determined" 
        // // KMajor=51   KMinor=52   C1=50   kzy=54   kyz=55   
        // // Omega=0   GammaOV=0   
        // // Nc=0   Nt=0  Mc3=0   Mc2=0   Mb=0   V2=0   V3=0 
        private static void setOVERWRITES_STEEL_DESIGN_INDIAN_IS_800_2007(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new IS_800_2007_Overwrites
                    {
                        FrameType = getNullableEnum<IS_800_2007_Preferences.FrameTypes>(tableRow["FrameType"]),
                        Omega = Adaptor.toDouble(tableRow["Omega"]),
                        GammaOV = Adaptor.toDouble(tableRow["GammaOV"]),
                        KMajor = Adaptor.toDouble(tableRow["KMajor"]),
                        KMinor = Adaptor.toDouble(tableRow["KMinor"]),
                        C1 = Adaptor.toDouble(tableRow["C1"]),
                        kzy = Adaptor.toDouble(tableRow["kzy"]),
                        kyz = Adaptor.toDouble(tableRow["kyz"]),
                        Nc = Adaptor.toDouble(tableRow["Nc"]),
                        Nt = Adaptor.toDouble(tableRow["Nt"]),
                        Mc3 = Adaptor.toDouble(tableRow["Mc3"]),
                        Mc2 = Adaptor.toDouble(tableRow["Mc2"]),
                        Mb = Adaptor.toDouble(tableRow["Mb"]),
                        V2 = Adaptor.toDouble(tableRow["V2"]),
                        V3 = Adaptor.toDouble(tableRow["V3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - ITALIAN NTC 2008"
        // // FrameType="Program Determined"       
        // // SectClass="Program Determined"   
        // // Rolled=Yes   
        // // kyyMajor=0   kzzMinor=0   C1=0   kzy=0  kyz=0 
        // // CurveYY="Program Determined"   
        // // CurveZZ="Program Determined"   
        // // CurveLTB="Program Determined"   
        // // Omega=0   GammaOV=0   Iw=0   
        // // NcrT=0   NcrTF=0   Nc=0   Nt=0   Mc3=0    Mc2=0   Mb=0   V2=0   V3=0 
        private static void setOVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2008(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new NTC_2008_Overwrites
                    {
                        FrameType = getNullableEnum<NTC_2008_Preferences.FrameTypes>(tableRow["FrameType"]),
                        IsRolled = getNullableYesNo(tableRow["Rolled"]),
                        SectionClass = getNullableEnum<NTC_2008_Overwrites.SectionClasses>(tableRow["SectClass"]),
                        BucklingCurveYY = getNullableEnum<NTC_2008_Overwrites.BucklingCurves>(tableRow["CurveYY"]),
                        BucklingCurveZZ = getNullableEnum<NTC_2008_Overwrites.BucklingCurves>(tableRow["CurveZZ"]),
                        BucklingCurveLTB = getNullableEnum<NTC_2008_Overwrites.BucklingCurves>(tableRow["CurveLTB"]),
                        kyyMajor = Adaptor.toDouble(tableRow["kyyMajor"]),
                        kzzMinor = Adaptor.toDouble(tableRow["kzzMinor"]),
                        C1 = Adaptor.toDouble(tableRow["C1"]),
                        kzy = Adaptor.toDouble(tableRow["kzy"]),
                        kyz = Adaptor.toDouble(tableRow["kyz"]),
                        Omega = Adaptor.toDouble(tableRow["Omega"]),
                        GammaOV = Adaptor.toDouble(tableRow["GammaOV"]),
                        Iw = Adaptor.toDouble(tableRow["Iw"]),
                        NcrT = Adaptor.toDouble(tableRow["NcrT"]),
                        NcrTF = Adaptor.toDouble(tableRow["NcrTF"]),
                        Nc = Adaptor.toDouble(tableRow["Nc"]),
                        Nt = Adaptor.toDouble(tableRow["Nt"]),
                        Mc3 = Adaptor.toDouble(tableRow["Mc3"]),
                        Mc2 = Adaptor.toDouble(tableRow["Mc2"]),
                        Mb = Adaptor.toDouble(tableRow["Mb"]),
                        V2 = Adaptor.toDouble(tableRow["V2"]),
                        V3 = Adaptor.toDouble(tableRow["V3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - ITALIAN NTC 2018"
        // // FrameType="Program Determined"      
        // // SectClass="Program Determined"   
        // // Rolled=Yes   
        // // kyyMajor=0   kzzMinor=0   C1=0   kzy=0  kyz=0   
        // // CurveYY="Program Determined"   
        // // CurveZZ="Program Determined"   
        // // CurveLTB="Program Determined"   
        // // Omega=0   GammaOV=0   Iw=0   
        // // NcrT=0   NcrTF=0   Nc=0   Nt=0   Mc3=0  Mc2=0   Mb=0   V2=0   V3=0
        private static void setOVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2018(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new NTC_2018_Overwrites
                    {
                        FrameType = getNullableEnum<NTC_2018_Preferences.FrameTypes>(tableRow["FrameType"]),
                        IsRolled = getNullableYesNo(tableRow["Rolled"]),
                        SectionClass = getNullableEnum<NTC_2018_Overwrites.SectionClasses>(tableRow["SectClass"]),
                        BucklingCurveYY = getNullableEnum<NTC_2018_Overwrites.BucklingCurves>(tableRow["CurveYY"]),
                        BucklingCurveZZ = getNullableEnum<NTC_2018_Overwrites.BucklingCurves>(tableRow["CurveZZ"]),
                        BucklingCurveLTB = getNullableEnum<NTC_2018_Overwrites.BucklingCurves>(tableRow["CurveLTB"]),
                        kyyMajor = Adaptor.toDouble(tableRow["kyyMajor"]),
                        kzzMinor = Adaptor.toDouble(tableRow["kzzMinor"]),
                        C1 = Adaptor.toDouble(tableRow["C1"]),
                        kzy = Adaptor.toDouble(tableRow["kzy"]),
                        kyz = Adaptor.toDouble(tableRow["kyz"]),
                        Omega = Adaptor.toDouble(tableRow["Omega"]),
                        GammaOV = Adaptor.toDouble(tableRow["GammaOV"]),
                        Iw = Adaptor.toDouble(tableRow["Iw"]),
                        NcrT = Adaptor.toDouble(tableRow["NcrT"]),
                        NcrTF = Adaptor.toDouble(tableRow["NcrTF"]),
                        Nc = Adaptor.toDouble(tableRow["Nc"]),
                        Nt = Adaptor.toDouble(tableRow["Nt"]),
                        Mc3 = Adaptor.toDouble(tableRow["Mc3"]),
                        Mc2 = Adaptor.toDouble(tableRow["Mc2"]),
                        Mb = Adaptor.toDouble(tableRow["Mb"]),
                        V2 = Adaptor.toDouble(tableRow["V2"]),
                        V3 = Adaptor.toDouble(tableRow["V3"])
                    }
                );
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - KBC 2009"
        // //FrameType="Program Determined"  
        // //HSSReduceT="Program Determined"   HSSWelding="Program Determined"   
        // //Omega0=0   Ry=0    
        // //B1Major=0   B1Minor=0   B2Major=0   B2Minor=0  
        // //CmMajor=0   CmMinor=0   Cb=0 _   
        // //Pnc=0   Pnt=0   Mn3=0   Mn2=0   Vn2=0   Vn3=0 
        private static void setOVERWRITES_STEEL_DESIGN_KBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new KBC_2009_Overwrites
                    {
                        FrameType = getNullableEnum<KBC_2009_Preferences.FrameTypes>(tableRow["FrameType"]),
                        ReduceHSSThickness = getNullableYesNo(tableRow["HSSReduceT"]),
                        HSSWeldingType = getNullableEnum<KBC_2009_Preferences.HSSWeldingTypes>(tableRow["HSSWelding"]),
                        Omega0 = Adaptor.toDouble(tableRow["Omega0"]),
                        Ry = Adaptor.toDouble(tableRow["Ry"]),
                        B1Major = Adaptor.toDouble(tableRow["B1Major"]),
                        B1Minor = Adaptor.toDouble(tableRow["B1Minor"]),
                        B2Major = Adaptor.toDouble(tableRow["B2Major"]),
                        B2Minor = Adaptor.toDouble(tableRow["B2Minor"]),
                        CmMajor = Adaptor.toDouble(tableRow["CmMajor"]),
                        CmMinor = Adaptor.toDouble(tableRow["CmMinor"]),
                        Cb = Adaptor.toDouble(tableRow["Cb"]),
                        Pnc = Adaptor.toDouble(tableRow["Pnc"]),
                        Pnt = Adaptor.toDouble(tableRow["Pnt"]),
                        Mn3 = Adaptor.toDouble(tableRow["Mn3"]),
                        Mn2 = Adaptor.toDouble(tableRow["Mn2"]),
                        Vn2 = Adaptor.toDouble(tableRow["Vn2"]),
                        Vn3 = Adaptor.toDouble(tableRow["Vn3"])
                    }
                );
            }
        }

        //TABLE:  "OVERWRITES - STEEL DESIGN - NZS 3404-1997"
        // Frame=39   DesignSect="Program Determined"   FrameType="Program Determined"   Fy=0   RLLF=0   AreaRatio=0
        // SteelType="Program Determined"
        // XLMajor=0   XLMinor=0   XLLTB=0
        // keMajor=0   keMinor=0   keSwayMajor=0   keSwayMinor=0 
        // ktLTB=0   krLTB=0   klLTB=0
        // CmMajor=0   CmMinor=0
        // Alpham=0   Alphas=0   Kf=0   KtAxial=0   DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0   Ns=0   Nt=0   Ms3=0   Ms2=0   Mb=0   Vv2=0   Vv3=0
        // CheckDefl="Program Determined" 
        // DeflType="Program Determined"   DLRat=0   SDLAndLLRat=0   LLRat=0   TotalRat=0   NetRat=0   DLAbs=0   SDLAndLLAbs=0   LLAbs=0   TotalAbs=0   NetAbs=0   SpecCamber=0   DCLimit=0

        // Frame=40   DesignSect="Program Determined"   FrameType="Program Determined"   Fy=0   RLLF=0   AreaRatio=0   SteelType="Program Determined"   XLMajor=0   XLMinor=0   XLLTB=0   keMajor=0   keMinor=0   keSwayMajor=0   keSwayMinor=0 _
        // ktLTB = 0   krLTB=0   klLTB=0   CmMajor=0   CmMinor=0   Alpham=0   Alphas=0   Kf=0   KtAxial=0   DbMajor=0   DbMinor=0   DsMajor=0   DsMinor=0   Ns=0   Nt=0   Ms3=0   Ms2=0   Mb=0   Vv2=0   Vv3=0   CheckDefl=Yes _
        // DeflType="Program Determined"   DLRat=0   SDLAndLLRat=0   LLRat=400   TotalRat=500   NetRat=0   DLAbs=0   SDLAndLLAbs=0   LLAbs=0   TotalAbs=0   NetAbs=0   SpecCamber=0   DCLimit=0

        // TABLE:  "OVERWRITES - STEEL DESIGN - NZS 3404-1997"
        // // FrameType="Program Determined"/"Moment Frame"
        // // SteelType="Program Determined"/"Hot Finished"
        // // (Twist Restraint Factor)ktLTB=0/19   
        // // (Lateral Rotation Restraint Factor)krLTB=0/20   
        // // (Load Height Factor for LTB)klLTB=0/21
        // // (Moment Modification Factor)Alpham=0/24   
        // // (Slenderness Reduction Factor)Alphas=0/25   
        // // (Form Factor)Kf=0/30   
        // // (Axial Capacity Correction Factor)KtAxial=0/31   
        // // (Nonsway/sway Moment Factors)DbMajor=0/26   DbMinor=0/27   DsMajor=0/28   DsMinor=0/29   
        // // (Moment Coefficient) CmMajor=0/22   CmMinor=0/23
        // // (Capacities)Ns=0/33   Nt=0/34   Ms3=0/35   Ms2=0/36   Mb=0/37   Vv2=0/38   Vv3=0/39
        private static void setOVERWRITES_STEEL_DESIGN_NZS_3404_1997(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Frame frame = model.Structure.Frames[tableRow["Frame"]];
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites = setOverwritesSteelBasic(model, tableRow);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites.K1Major = Adaptor.toDouble(tableRow["keMajor"]);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites.K1Minor = Adaptor.toDouble(tableRow["keMinor"]);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites.K2Major = Adaptor.toDouble(tableRow["keSwayMajor"]);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites.K2Minor = Adaptor.toDouble(tableRow["keSwayMinor"]);
                frame.FrameDesignOverwrites.SteelDesignOverwrite.UpdateItem(
                    new NZS_3404_1997_Overwrites
                    {
                        FrameType = getNullableEnum<NZS_3404_1997_Preferences.FrameTypes>(tableRow["FrameType"]),
                        SteelType = getNullableEnum<NZS_3404_1997_Preferences.SteelTypes>(tableRow["SteelType"]),
                        CmMajor = Adaptor.toDouble(tableRow["CmMajor"]),
                        CmMinor = Adaptor.toDouble(tableRow["CmMinor"]),
                        ktLTB = Adaptor.toDouble(tableRow["ktLTB"]),
                        krLTB = Adaptor.toDouble(tableRow["krLTB"]),
                        klLTB = Adaptor.toDouble(tableRow["klLTB"]),
                        Alpha_m = Adaptor.toDouble(tableRow["Alpham"]),
                        Alpha_s = Adaptor.toDouble(tableRow["Alphas"]),
                        Kf = Adaptor.toDouble(tableRow["Kf"]),
                        KtAxial = Adaptor.toDouble(tableRow["KtAxial"]),
                        DbMajor = Adaptor.toDouble(tableRow["DbMajor"]),
                        DbMinor = Adaptor.toDouble(tableRow["DbMinor"]),
                        DsMajor = Adaptor.toDouble(tableRow["DsMajor"]),
                        DsMinor = Adaptor.toDouble(tableRow["DsMinor"]),
                        Ns = Adaptor.toDouble(tableRow["Ns"]),
                        Nt = Adaptor.toDouble(tableRow["Nt"]),
                        Ms3 = Adaptor.toDouble(tableRow["Ms3"]),
                        Ms2 = Adaptor.toDouble(tableRow["Ms2"]),
                        Mb = Adaptor.toDouble(tableRow["Mb"]),
                        Vv2 = Adaptor.toDouble(tableRow["Vv2"]),
                        Vv3 = Adaptor.toDouble(tableRow["Vv3"])
                    }
                );
            }
        }
    }
}
