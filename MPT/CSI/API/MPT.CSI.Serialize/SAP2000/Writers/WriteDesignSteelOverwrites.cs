using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal class WriteDesignSteelOverwrites : WriteDesignOverwrites
    {
        protected WriteDesignSteelOverwrites() { }

        internal static void DefineOverwrites(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_AISC360_05_IBC2006, setOVERWRITES_STEEL_DESIGN_AISC360_05_IBC2006);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_AISC_360_10, setOVERWRITES_STEEL_DESIGN_AISC_360_10);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_AISC_360_16, setOVERWRITES_STEEL_DESIGN_AISC_360_16);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_AS_4100_1998, setOVERWRITES_STEEL_DESIGN_AS_4100_1998);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_BS5950_2000, setOVERWRITES_STEEL_DESIGN_BS5950_2000);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_CSA_S16_09, setOVERWRITES_STEEL_DESIGN_CSA_S16_09);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_CSA_S16_14, setOVERWRITES_STEEL_DESIGN_CSA_S16_14);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_EUROCODE_3_2005, setOVERWRITES_STEEL_DESIGN_EUROCODE_3_2005);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_INDIAN_IS_800_2007, setOVERWRITES_STEEL_DESIGN_INDIAN_IS_800_2007);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2008, setOVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2008);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2018, setOVERWRITES_STEEL_DESIGN_ITALIAN_NTC_2018);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_KBC_2009, setOVERWRITES_STEEL_DESIGN_KBC_2009);
            writer.WriteSingleTable(SAP2000Tables.OVERWRITES_STEEL_DESIGN_NZS_3404_1997, setOVERWRITES_STEEL_DESIGN_NZS_3404_1997);
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

        private static Dictionary<string, string> setOverwritesSteelBasic(Frame frame)
        {
            FrameDesignOverwrites<SteelDesignOverwrite, SteelDesignOverwriteProperties> overwrite = frame.FrameDesignOverwrites.SteelDesignOverwrite;

            Dictionary<string, string> tableRow = new Dictionary<string, string>
            {
                ["Frame"] = Adaptor.ToStringEntryLimited(frame.Name),
                ["DesignSect"] =
                    Adaptor.ToStringEntryLimited(getNullableFrameSection(overwrite.GenericOverwrites.DesignSection)),
                ["Fy"] = Adaptor.fromDouble(overwrite.GenericOverwrites.Fy),
                ["RLLF"] = Adaptor.fromDouble(overwrite.GenericOverwrites.RLLF),
                ["AreaRatio"] = Adaptor.fromDouble(overwrite.GenericOverwrites.AreaRatio),
                ["XLMajor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XLMajor),
                ["XLMinor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XLMinor),
                ["XLLTB"] = Adaptor.fromDouble(overwrite.GenericOverwrites.XLLTB),
                ["KLTB"] = Adaptor.fromDouble(overwrite.GenericOverwrites.KLTB),
                ["CheckDefl"] = getNullableYesNo(overwrite.GenericOverwrites.IsDeflectionConsidered),
                ["DeflType"] = getNullableEnum(overwrite.GenericOverwrites.DeflectionCheckType),
                ["DLRat"] = Adaptor.fromDouble(overwrite.GenericOverwrites.RatioDL),
                ["SDLAndLLRat"] = Adaptor.fromDouble(overwrite.GenericOverwrites.RatioSDLAndLL),
                ["LLRat"] = Adaptor.fromDouble(overwrite.GenericOverwrites.RatioLL),
                ["TotalRat"] = Adaptor.fromDouble(overwrite.GenericOverwrites.RatioTotal),
                ["NetRat"] = Adaptor.fromDouble(overwrite.GenericOverwrites.RatioNet),
                ["DLAbs"] = Adaptor.fromDouble(overwrite.GenericOverwrites.AbsoluteDL),
                ["SDLAndLLAbs"] = Adaptor.fromDouble(overwrite.GenericOverwrites.AbsoluteSDLAndLL),
                ["LLAbs"] = Adaptor.fromDouble(overwrite.GenericOverwrites.AbsoluteLL),
                ["TotalAbs"] = Adaptor.fromDouble(overwrite.GenericOverwrites.AbsoluteTotal),
                ["NetAbs"] = Adaptor.fromDouble(overwrite.GenericOverwrites.AbsoluteNet),
                ["SpecCamber"] = Adaptor.fromDouble(overwrite.GenericOverwrites.SpecifiedCamber),
                ["DCLimit"] = Adaptor.fromDouble(overwrite.GenericOverwrites.DemandCapacityRatioLimit)
            };

            if (overwrite.GenericOverwrites.K1Major.HasValue) tableRow["K1Major"] = Adaptor.fromDouble(overwrite.GenericOverwrites.K1Major.Value);
            if (overwrite.GenericOverwrites.K1Minor.HasValue) tableRow["K1Minor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.K1Minor.Value);
            if (overwrite.GenericOverwrites.K2Major.HasValue) tableRow["K2Major"] = Adaptor.fromDouble(overwrite.GenericOverwrites.K2Major.Value);
            if (overwrite.GenericOverwrites.K2Minor.HasValue) tableRow["K2Minor"] = Adaptor.fromDouble(overwrite.GenericOverwrites.K2Minor.Value);

            return tableRow;
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
            foreach (Frame frame in model.Structure.Frames)
            {
                AISC_360_05_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<AISC_360_05_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["HSSReduceT"] = getNullableYesNo(overwrite.ReduceHSSThickness);
                tableRow["HSSWelding"] = Adaptor.ToStringEntryLimited(getNullableEnum(overwrite.HSSWeldingType));
                tableRow["Omega0"] = Adaptor.fromDouble(overwrite.Omega0);
                tableRow["Ry"] = Adaptor.fromDouble(overwrite.Ry);
                tableRow["B1Major"] = Adaptor.fromDouble(overwrite.B1Major);
                tableRow["B1Minor"] = Adaptor.fromDouble(overwrite.B1Minor);
                tableRow["B2Major"] = Adaptor.fromDouble(overwrite.B2Major);
                tableRow["B2Minor"] = Adaptor.fromDouble(overwrite.B2Minor);
                tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor);
                tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor);
                tableRow["Cb"] = Adaptor.fromDouble(overwrite.Cb);
                tableRow["Pnc"] = Adaptor.fromDouble(overwrite.Pnc);
                tableRow["Pnt"] = Adaptor.fromDouble(overwrite.Pnt);
                tableRow["Mn3"] = Adaptor.fromDouble(overwrite.Mn3);
                tableRow["Mn2"] = Adaptor.fromDouble(overwrite.Mn2);
                tableRow["Vn2"] = Adaptor.fromDouble(overwrite.Vn2);
                tableRow["Vn3"] = Adaptor.fromDouble(overwrite.Vn3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                AISC_360_10_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<AISC_360_10_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["HSSReduceT"] = getNullableYesNo(overwrite.ReduceHSSThickness);
                tableRow["HSSWelding"] = Adaptor.ToStringEntryLimited(getNullableEnum(overwrite.HSSWeldingType));
                tableRow["Omega0"] = Adaptor.fromDouble(overwrite.Omega0);
                tableRow["Ry"] = Adaptor.fromDouble(overwrite.Ry);
                tableRow["B1Major"] = Adaptor.fromDouble(overwrite.B1Major);
                tableRow["B1Minor"] = Adaptor.fromDouble(overwrite.B1Minor);
                tableRow["B2Major"] = Adaptor.fromDouble(overwrite.B2Major);
                tableRow["B2Minor"] = Adaptor.fromDouble(overwrite.B2Minor);
                tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor);
                tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor);
                tableRow["Cb"] = Adaptor.fromDouble(overwrite.Cb);
                tableRow["Pnc"] = Adaptor.fromDouble(overwrite.Pnc);
                tableRow["Pnt"] = Adaptor.fromDouble(overwrite.Pnt);
                tableRow["Mn3"] = Adaptor.fromDouble(overwrite.Mn3);
                tableRow["Mn2"] = Adaptor.fromDouble(overwrite.Mn2);
                tableRow["Vn2"] = Adaptor.fromDouble(overwrite.Vn2);
                tableRow["Vn3"] = Adaptor.fromDouble(overwrite.Vn3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                AISC_360_16_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<AISC_360_16_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["HSSReduceT"] = getNullableYesNo(overwrite.ReduceHSSThickness);
                tableRow["HSSWelding"] = Adaptor.ToStringEntryLimited(getNullableEnum(overwrite.HSSWeldingType));
                tableRow["Omega0"] = Adaptor.fromDouble(overwrite.Omega0);
                tableRow["Ry"] = Adaptor.fromDouble(overwrite.Ry);
                tableRow["B1Major"] = Adaptor.fromDouble(overwrite.B1Major);
                tableRow["B1Minor"] = Adaptor.fromDouble(overwrite.B1Minor);
                tableRow["B2Major"] = Adaptor.fromDouble(overwrite.B2Major);
                tableRow["B2Minor"] = Adaptor.fromDouble(overwrite.B2Minor);
                tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor);
                tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor);
                tableRow["Cb"] = Adaptor.fromDouble(overwrite.Cb);
                tableRow["Pnc"] = Adaptor.fromDouble(overwrite.Pnc);
                tableRow["Pnt"] = Adaptor.fromDouble(overwrite.Pnt);
                tableRow["Mn3"] = Adaptor.fromDouble(overwrite.Mn3);
                tableRow["Mn2"] = Adaptor.fromDouble(overwrite.Mn2);
                tableRow["Vn2"] = Adaptor.fromDouble(overwrite.Vn2);
                tableRow["Vn3"] = Adaptor.fromDouble(overwrite.Vn3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                AS_4100_1998_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<AS_4100_1998_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);

                SteelDesignOverwrite steelDesignOverwrite = frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites;
                tableRow.Remove("K1Major");
                if (steelDesignOverwrite.K1Major.HasValue) tableRow["keMajor"] = Adaptor.fromDouble(steelDesignOverwrite.K1Major.Value);
                tableRow.Remove("K1Minor");
                if (steelDesignOverwrite.K1Minor.HasValue) tableRow["keMinor"] = Adaptor.fromDouble(steelDesignOverwrite.K1Minor.Value);
                tableRow.Remove("K2Major");
                if (steelDesignOverwrite.K2Major.HasValue) tableRow["keSwayMajor"] = Adaptor.fromDouble(steelDesignOverwrite.K2Major.Value);
                tableRow.Remove("K2Minor");
                if (steelDesignOverwrite.K2Minor.HasValue) tableRow["keSwayMinor"] = Adaptor.fromDouble(steelDesignOverwrite.K2Minor.Value);

                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["SteelType"] = getNullableEnum(overwrite.SteelType);
                tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor);
                tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor);
                tableRow["ktLTB"] = Adaptor.fromDouble(overwrite.ktLTB);
                tableRow["krLTB"] = Adaptor.fromDouble(overwrite.krLTB);
                tableRow["klLTB"] = Adaptor.fromDouble(overwrite.klLTB);
                tableRow["Alpham"] = Adaptor.fromDouble(overwrite.Alpha_m);
                tableRow["Alphas"] = Adaptor.fromDouble(overwrite.Alpha_s);
                tableRow["Kf"] = Adaptor.fromDouble(overwrite.Kf);
                tableRow["KtAxial"] = Adaptor.fromDouble(overwrite.KtAxial);
                tableRow["DbMajor"] = Adaptor.fromDouble(overwrite.DbMajor);
                tableRow["DbMinor"] = Adaptor.fromDouble(overwrite.DbMinor);
                tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor);
                tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor);
                tableRow["Ns"] = Adaptor.fromDouble(overwrite.Ns);
                tableRow["Nt"] = Adaptor.fromDouble(overwrite.Nt);
                tableRow["Ms3"] = Adaptor.fromDouble(overwrite.Ms3);
                tableRow["Ms2"] = Adaptor.fromDouble(overwrite.Ms2);
                tableRow["Mb"] = Adaptor.fromDouble(overwrite.Mb);
                tableRow["Vv2"] = Adaptor.fromDouble(overwrite.Vv2);
                tableRow["Vv3"] = Adaptor.fromDouble(overwrite.Vv3);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - BS5950 2000"
        // // FrameType="Program Determined"   
        // // MMajor=0   MMinor=0   MLT=0   
        // // Pc=0   Pt=0   Mc3=0   Mc2=0   Mb=0   Pv2=0   Pv3=0 
        private static void setOVERWRITES_STEEL_DESIGN_BS5950_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                BS_5950_2000_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<BS_5950_2000_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["MMajor"] = Adaptor.fromDouble(overwrite.MMajor);
                tableRow["MMinor"] = Adaptor.fromDouble(overwrite.MMinor);
                tableRow["MLT"] = Adaptor.fromDouble(overwrite.MLT);
                tableRow["Pc"] = Adaptor.fromDouble(overwrite.Pc);
                tableRow["Pt"] = Adaptor.fromDouble(overwrite.Pt);
                tableRow["Mc3"] = Adaptor.fromDouble(overwrite.Mc3);
                tableRow["Mc2"] = Adaptor.fromDouble(overwrite.Mc2);
                tableRow["Mb"] = Adaptor.fromDouble(overwrite.Mb);
                tableRow["Pv2"] = Adaptor.fromDouble(overwrite.Pv2);
                tableRow["Pv3"] = Adaptor.fromDouble(overwrite.Pv3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                CSA_S16_09_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<CSA_S16_09_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["NPower"] = Adaptor.fromDouble(overwrite.NPower);
                tableRow["Ry"] = Adaptor.fromDouble(overwrite.Ry);
                tableRow["U1Major"] = Adaptor.fromDouble(overwrite.U1Major);
                tableRow["U1Minor"] = Adaptor.fromDouble(overwrite.U1Minor);
                tableRow["U2Major"] = Adaptor.fromDouble(overwrite.U2Major);
                tableRow["U2Minor"] = Adaptor.fromDouble(overwrite.U2Minor);
                tableRow["Omega1Major"] = Adaptor.fromDouble(overwrite.Omega1Major);
                tableRow["Omega1Minor"] = Adaptor.fromDouble(overwrite.Omega1Minor);
                tableRow["Omega2"] = Adaptor.fromDouble(overwrite.Omega2);
                tableRow["Cr"] = Adaptor.fromDouble(overwrite.Cr);
                tableRow["Tr"] = Adaptor.fromDouble(overwrite.Tr);
                tableRow["Mr3"] = Adaptor.fromDouble(overwrite.Mr3);
                tableRow["Mr2"] = Adaptor.fromDouble(overwrite.Mr2);
                tableRow["Vr2"] = Adaptor.fromDouble(overwrite.Vr2);
                tableRow["Vr3"] = Adaptor.fromDouble(overwrite.Vr3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                CSA_S16_14_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<CSA_S16_14_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["NPower"] = Adaptor.fromDouble(overwrite.NPower);
                tableRow["Ry"] = Adaptor.fromDouble(overwrite.Ry);
                tableRow["U1Major"] = Adaptor.fromDouble(overwrite.U1Major);
                tableRow["U1Minor"] = Adaptor.fromDouble(overwrite.U1Minor);
                tableRow["U2Major"] = Adaptor.fromDouble(overwrite.U2Major);
                tableRow["U2Minor"] = Adaptor.fromDouble(overwrite.U2Minor);
                tableRow["Omega1Major"] = Adaptor.fromDouble(overwrite.Omega1Major);
                tableRow["Omega1Minor"] = Adaptor.fromDouble(overwrite.Omega1Minor);
                tableRow["Omega2"] = Adaptor.fromDouble(overwrite.Omega2);
                tableRow["Cr"] = Adaptor.fromDouble(overwrite.Cr);
                tableRow["Tr"] = Adaptor.fromDouble(overwrite.Tr);
                tableRow["Mr3"] = Adaptor.fromDouble(overwrite.Mr3);
                tableRow["Mr2"] = Adaptor.fromDouble(overwrite.Mr2);
                tableRow["Vr2"] = Adaptor.fromDouble(overwrite.Vr2);
                tableRow["Vr3"] = Adaptor.fromDouble(overwrite.Vr3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                EN_3_2005_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<EN_3_2005_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["Rolled"] = getNullableYesNo(overwrite.IsRolled);
                tableRow["SectClass"] = Adaptor.fromNullableEnum(overwrite.SectionClass);
                tableRow["CurveYY"] = Adaptor.fromNullableEnum(overwrite.BucklingCurveYY);
                tableRow["CurveZZ"] = Adaptor.fromNullableEnum(overwrite.BucklingCurveZZ);
                tableRow["CurveLTB"] = Adaptor.fromNullableEnum(overwrite.BucklingCurveLTB);
                tableRow["kyyMajor"] = Adaptor.fromDouble(overwrite.kyyMajor);
                tableRow["kzzMinor"] = Adaptor.fromDouble(overwrite.kzzMinor);
                tableRow["C1"] = Adaptor.fromDouble(overwrite.C1);
                tableRow["kzy"] = Adaptor.fromDouble(overwrite.kzy);
                tableRow["kyz"] = Adaptor.fromDouble(overwrite.kyz);
                tableRow["Omega"] = Adaptor.fromDouble(overwrite.Omega);
                tableRow["GammaOV"] = Adaptor.fromDouble(overwrite.GammaOV);
                tableRow["Iw"] = Adaptor.fromDouble(overwrite.Iw);
                tableRow["NcrT"] = Adaptor.fromDouble(overwrite.NcrT);
                tableRow["NcrTF"] = Adaptor.fromDouble(overwrite.NcrTF);
                tableRow["Nc"] = Adaptor.fromDouble(overwrite.Nc);
                tableRow["Nt"] = Adaptor.fromDouble(overwrite.Nt);
                tableRow["Mc3"] = Adaptor.fromDouble(overwrite.Mc3);
                tableRow["Mc2"] = Adaptor.fromDouble(overwrite.Mc2);
                tableRow["Mb"] = Adaptor.fromDouble(overwrite.Mb);
                tableRow["V2"] = Adaptor.fromDouble(overwrite.V2);
                tableRow["V3"] = Adaptor.fromDouble(overwrite.V3);

                table.Add(tableRow);
            }
        }

        // TABLE:  "OVERWRITES - STEEL DESIGN - INDIAN IS 800:2007"
        // // FrameType="Program Determined" 
        // // KMajor=51   KMinor=52   C1=50   kzy=54   kyz=55   
        // // Omega=0   GammaOV=0   
        // // Nc=0   Nt=0  Mc3=0   Mc2=0   Mb=0   V2=0   V3=0 
        private static void setOVERWRITES_STEEL_DESIGN_INDIAN_IS_800_2007(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Frame frame in model.Structure.Frames)
            {
                IS_800_2007_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<IS_800_2007_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["Omega"] = Adaptor.fromDouble(overwrite.Omega);
                tableRow["GammaOV"] = Adaptor.fromDouble(overwrite.GammaOV);
                tableRow["KMajor"] = Adaptor.fromDouble(overwrite.KMajor);
                tableRow["KMinor"] = Adaptor.fromDouble(overwrite.KMinor);
                tableRow["C1"] = Adaptor.fromDouble(overwrite.C1);
                tableRow["kzy"] = Adaptor.fromDouble(overwrite.kzy);
                tableRow["kyz"] = Adaptor.fromDouble(overwrite.kyz);
                tableRow["Nc"] = Adaptor.fromDouble(overwrite.Nc);
                tableRow["Nt"] = Adaptor.fromDouble(overwrite.Nt);
                tableRow["Mc3"] = Adaptor.fromDouble(overwrite.Mc3);
                tableRow["Mc2"] = Adaptor.fromDouble(overwrite.Mc2);
                tableRow["Mb"] = Adaptor.fromDouble(overwrite.Mb);
                tableRow["V2"] = Adaptor.fromDouble(overwrite.V2);
                tableRow["V3"] = Adaptor.fromDouble(overwrite.V3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                NTC_2008_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<NTC_2008_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["Rolled"] = getNullableYesNo(overwrite.IsRolled);
                tableRow["SectClass"] = Adaptor.fromNullableEnum(overwrite.SectionClass);
                tableRow["CurveYY"] = Adaptor.fromNullableEnum(overwrite.BucklingCurveYY);
                tableRow["CurveZZ"] = Adaptor.fromNullableEnum(overwrite.BucklingCurveZZ);
                tableRow["CurveLTB"] = Adaptor.fromNullableEnum(overwrite.BucklingCurveLTB);
                tableRow["kyyMajor"] = Adaptor.fromDouble(overwrite.kyyMajor);
                tableRow["kzzMinor"] = Adaptor.fromDouble(overwrite.kzzMinor);
                tableRow["C1"] = Adaptor.fromDouble(overwrite.C1);
                tableRow["kzy"] = Adaptor.fromDouble(overwrite.kzy);
                tableRow["kyz"] = Adaptor.fromDouble(overwrite.kyz);
                tableRow["Omega"] = Adaptor.fromDouble(overwrite.Omega);
                tableRow["GammaOV"] = Adaptor.fromDouble(overwrite.GammaOV);
                tableRow["Iw"] = Adaptor.fromDouble(overwrite.Iw);
                tableRow["NcrT"] = Adaptor.fromDouble(overwrite.NcrT);
                tableRow["NcrTF"] = Adaptor.fromDouble(overwrite.NcrTF);
                tableRow["Nc"] = Adaptor.fromDouble(overwrite.Nc);
                tableRow["Nt"] = Adaptor.fromDouble(overwrite.Nt);
                tableRow["Mc3"] = Adaptor.fromDouble(overwrite.Mc3);
                tableRow["Mc2"] = Adaptor.fromDouble(overwrite.Mc2);
                tableRow["Mb"] = Adaptor.fromDouble(overwrite.Mb);
                tableRow["V2"] = Adaptor.fromDouble(overwrite.V2);
                tableRow["V3"] = Adaptor.fromDouble(overwrite.V3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                NTC_2018_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<NTC_2018_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["Rolled"] = getNullableYesNo(overwrite.IsRolled);
                tableRow["SectClass"] = Adaptor.fromNullableEnum(overwrite.SectionClass);
                tableRow["CurveYY"] = Adaptor.fromNullableEnum(overwrite.BucklingCurveYY);
                tableRow["CurveZZ"] = Adaptor.fromNullableEnum(overwrite.BucklingCurveZZ);
                tableRow["CurveLTB"] = Adaptor.fromNullableEnum(overwrite.BucklingCurveLTB);
                tableRow["kyyMajor"] = Adaptor.fromDouble(overwrite.kyyMajor);
                tableRow["kzzMinor"] = Adaptor.fromDouble(overwrite.kzzMinor);
                tableRow["C1"] = Adaptor.fromDouble(overwrite.C1);
                tableRow["kzy"] = Adaptor.fromDouble(overwrite.kzy);
                tableRow["kyz"] = Adaptor.fromDouble(overwrite.kyz);
                tableRow["Omega"] = Adaptor.fromDouble(overwrite.Omega);
                tableRow["GammaOV"] = Adaptor.fromDouble(overwrite.GammaOV);
                tableRow["Iw"] = Adaptor.fromDouble(overwrite.Iw);
                tableRow["NcrT"] = Adaptor.fromDouble(overwrite.NcrT);
                tableRow["NcrTF"] = Adaptor.fromDouble(overwrite.NcrTF);
                tableRow["Nc"] = Adaptor.fromDouble(overwrite.Nc);
                tableRow["Nt"] = Adaptor.fromDouble(overwrite.Nt);
                tableRow["Mc3"] = Adaptor.fromDouble(overwrite.Mc3);
                tableRow["Mc2"] = Adaptor.fromDouble(overwrite.Mc2);
                tableRow["Mb"] = Adaptor.fromDouble(overwrite.Mb);
                tableRow["V2"] = Adaptor.fromDouble(overwrite.V2);
                tableRow["V3"] = Adaptor.fromDouble(overwrite.V3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                KBC_2009_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<KBC_2009_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);
                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["HSSReduceT"] = getNullableYesNo(overwrite.ReduceHSSThickness);
                tableRow["HSSWelding"] = Adaptor.ToStringEntryLimited(getNullableEnum(overwrite.HSSWeldingType));
                tableRow["Omega0"] = Adaptor.fromDouble(overwrite.Omega0);
                tableRow["Ry"] = Adaptor.fromDouble(overwrite.Ry);
                tableRow["B1Major"] = Adaptor.fromDouble(overwrite.B1Major);
                tableRow["B1Minor"] = Adaptor.fromDouble(overwrite.B1Minor);
                tableRow["B2Major"] = Adaptor.fromDouble(overwrite.B2Major);
                tableRow["B2Minor"] = Adaptor.fromDouble(overwrite.B2Minor);
                tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor);
                tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor);
                tableRow["Cb"] = Adaptor.fromDouble(overwrite.Cb);
                tableRow["Pnc"] = Adaptor.fromDouble(overwrite.Pnc);
                tableRow["Pnt"] = Adaptor.fromDouble(overwrite.Pnt);
                tableRow["Mn3"] = Adaptor.fromDouble(overwrite.Mn3);
                tableRow["Mn2"] = Adaptor.fromDouble(overwrite.Mn2);
                tableRow["Vn2"] = Adaptor.fromDouble(overwrite.Vn2);
                tableRow["Vn3"] = Adaptor.fromDouble(overwrite.Vn3);

                table.Add(tableRow);
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
            foreach (Frame frame in model.Structure.Frames)
            {
                NZS_3404_1997_Overwrites overwrite =
                    frame.FrameDesignOverwrites.SteelDesignOverwrite.GetItem<NZS_3404_1997_Overwrites>();

                Dictionary<string, string> tableRow = setOverwritesSteelBasic(frame);

                SteelDesignOverwrite steelDesignOverwrite = frame.FrameDesignOverwrites.SteelDesignOverwrite.GenericOverwrites;
                tableRow.Remove("K1Major");
                if (steelDesignOverwrite.K1Major.HasValue) tableRow["keMajor"] = Adaptor.fromDouble(steelDesignOverwrite.K1Major.Value);
                tableRow.Remove("K1Minor");
                if (steelDesignOverwrite.K1Minor.HasValue) tableRow["keMinor"] = Adaptor.fromDouble(steelDesignOverwrite.K1Minor.Value);
                tableRow.Remove("K2Major");
                if (steelDesignOverwrite.K2Major.HasValue) tableRow["keSwayMajor"] = Adaptor.fromDouble(steelDesignOverwrite.K2Major.Value);
                tableRow.Remove("K2Minor");
                if (steelDesignOverwrite.K2Minor.HasValue) tableRow["keSwayMinor"] = Adaptor.fromDouble(steelDesignOverwrite.K2Minor.Value);

                tableRow["FrameType"] = getNullableEnum(overwrite.FrameType);
                tableRow["SteelType"] = getNullableEnum(overwrite.SteelType);
                tableRow["CmMajor"] = Adaptor.fromDouble(overwrite.CmMajor);
                tableRow["CmMinor"] = Adaptor.fromDouble(overwrite.CmMinor);
                tableRow["ktLTB"] = Adaptor.fromDouble(overwrite.ktLTB);
                tableRow["krLTB"] = Adaptor.fromDouble(overwrite.krLTB);
                tableRow["klLTB"] = Adaptor.fromDouble(overwrite.klLTB);
                tableRow["Alpham"] = Adaptor.fromDouble(overwrite.Alpha_m);
                tableRow["Alphas"] = Adaptor.fromDouble(overwrite.Alpha_s);
                tableRow["Kf"] = Adaptor.fromDouble(overwrite.Kf);
                tableRow["KtAxial"] = Adaptor.fromDouble(overwrite.KtAxial);
                tableRow["DbMajor"] = Adaptor.fromDouble(overwrite.DbMajor);
                tableRow["DbMinor"] = Adaptor.fromDouble(overwrite.DbMinor);
                tableRow["DsMajor"] = Adaptor.fromDouble(overwrite.DsMajor);
                tableRow["DsMinor"] = Adaptor.fromDouble(overwrite.DsMinor);
                tableRow["Ns"] = Adaptor.fromDouble(overwrite.Ns);
                tableRow["Nt"] = Adaptor.fromDouble(overwrite.Nt);
                tableRow["Ms3"] = Adaptor.fromDouble(overwrite.Ms3);
                tableRow["Ms2"] = Adaptor.fromDouble(overwrite.Ms2);
                tableRow["Mb"] = Adaptor.fromDouble(overwrite.Mb);
                tableRow["Vv2"] = Adaptor.fromDouble(overwrite.Vv2);
                tableRow["Vv3"] = Adaptor.fromDouble(overwrite.Vv3);

                table.Add(tableRow);
            }
        }
    }
}
