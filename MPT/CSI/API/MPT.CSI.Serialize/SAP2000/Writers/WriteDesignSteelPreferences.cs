using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteDesignSteelPreferences
    {
        internal static void DefinePreferences(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_AISC360_05_IBC2006, setPREFERENCES_STEEL_DESIGN_AISC360_05_IBC2006);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_AISC_360_10, setPREFERENCES_STEEL_DESIGN_AISC_360_10);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_AISC_360_16, setPREFERENCES_STEEL_DESIGN_AISC_360_16);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_AS_4100_1998, setPREFERENCES_STEEL_DESIGN_AS_4100_1998);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_BS5950_2000, setPREFERENCES_STEEL_DESIGN_BS5950_2000);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_CSA_S16_09, setPREFERENCES_STEEL_DESIGN_CSA_S16_09);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_CSA_S16_14, setPREFERENCES_STEEL_DESIGN_CSA_S16_14);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_EUROCODE_3_2005, setPREFERENCES_STEEL_DESIGN_EUROCODE_3_2005);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_INDIAN_IS_800_2007, setPREFERENCES_STEEL_DESIGN_INDIAN_IS_800_2007);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2008, setPREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2008);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2018, setPREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2018);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_KBC_2009, setPREFERENCES_STEEL_DESIGN_KBC_2009);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_NZS_3404_1997, setPREFERENCES_STEEL_DESIGN_NZS_3404_1997);
        }
        
        /// <summary>
        /// Sets the preferences steel design.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_STEEL_DESIGN_AISC360_05_IBC2006(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<AISC_360_05_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiTY", Adaptor.fromDouble(preferences.CodeProperties.PhiTY) },
                        { "PhiTF", Adaptor.fromDouble(preferences.CodeProperties.PhiTF) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVRolledI", Adaptor.fromDouble(preferences.CodeProperties.PhiVRolledI) },
                        { "PhiVT", Adaptor.fromDouble(preferences.CodeProperties.PhiVT) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) },
                        { "HSSWelding", Adaptor.fromEnum(preferences.CodeProperties.HSSWeldingType) },
                        { "HSSReduceT", Adaptor.toYesNo(preferences.CodeProperties.ReduceHSSThickness) },
                        { "AMethod", Adaptor.fromEnum(preferences.CodeProperties.AnalysisMethod) },
                        { "SOMethod", Adaptor.fromEnum(preferences.CodeProperties.SecondOrderMethod) },
                        { "SRMethod", Adaptor.fromEnum(preferences.CodeProperties.StiffnessReductionMethod) },
                        { "Provision", Adaptor.fromEnum(preferences.CodeProperties.DesignProvision) },
                        { "SDC", Adaptor.fromEnum(preferences.CodeProperties.SeismicDesignCategory) },
                        { "ImpFactor", Adaptor.fromDouble(preferences.CodeProperties.ImportanceFactor) },
                        { "SystemRho", Adaptor.fromDouble(preferences.CodeProperties.Rho) },
                        { "SystemSds", Adaptor.fromDouble(preferences.CodeProperties.Sds) },
                        { "SystemR", Adaptor.fromDouble(preferences.CodeProperties.R) },
                        { "SystemCd", Adaptor.fromDouble(preferences.CodeProperties.Cd) },
                        { "Omega0", Adaptor.fromDouble(preferences.CodeProperties.Omega0) },
                        { "NLCoeff", Adaptor.fromDouble(preferences.CodeProperties.NLCoeff) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_AISC_360_10(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<AISC_360_10_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiTY", Adaptor.fromDouble(preferences.CodeProperties.PhiTY) },
                        { "PhiTF", Adaptor.fromDouble(preferences.CodeProperties.PhiTF) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVRolledI", Adaptor.fromDouble(preferences.CodeProperties.PhiVRolledI) },
                        { "PhiVT", Adaptor.fromDouble(preferences.CodeProperties.PhiVT) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) },
                        { "HSSWelding", Adaptor.fromEnum(preferences.CodeProperties.HSSWeldingType) },
                        { "HSSReduceT", Adaptor.toYesNo(preferences.CodeProperties.ReduceHSSThickness) },
                        { "AMethod", Adaptor.fromEnum(preferences.CodeProperties.AnalysisMethod) },
                        { "SOMethod", Adaptor.fromEnum(preferences.CodeProperties.SecondOrderMethod) },
                        { "SRMethod", Adaptor.fromEnum(preferences.CodeProperties.StiffnessReductionMethod) },
                        { "Provision", Adaptor.fromEnum(preferences.CodeProperties.DesignProvision) },
                        { "SDC", Adaptor.fromEnum(preferences.CodeProperties.SeismicDesignCategory) },
                        { "ImpFactor", Adaptor.fromDouble(preferences.CodeProperties.ImportanceFactor) },
                        { "SystemRho", Adaptor.fromDouble(preferences.CodeProperties.Rho) },
                        { "SystemSds", Adaptor.fromDouble(preferences.CodeProperties.Sds) },
                        { "SystemR", Adaptor.fromDouble(preferences.CodeProperties.R) },
                        { "SystemCd", Adaptor.fromDouble(preferences.CodeProperties.Cd) },
                        { "Omega0", Adaptor.fromDouble(preferences.CodeProperties.Omega0) },
                        { "NLCoeff", Adaptor.fromDouble(preferences.CodeProperties.NLCoeff) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_AISC_360_16(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<AISC_360_16_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiTY", Adaptor.fromDouble(preferences.CodeProperties.PhiTY) },
                        { "PhiTF", Adaptor.fromDouble(preferences.CodeProperties.PhiTF) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVRolledI", Adaptor.fromDouble(preferences.CodeProperties.PhiVRolledI) },
                        { "PhiVT", Adaptor.fromDouble(preferences.CodeProperties.PhiVT) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) },
                        { "HSSWelding", Adaptor.fromEnum(preferences.CodeProperties.HSSWeldingType) },
                        { "HSSReduceT", Adaptor.toYesNo(preferences.CodeProperties.ReduceHSSThickness) },
                        { "AMethod", Adaptor.fromEnum(preferences.CodeProperties.AnalysisMethod) },
                        { "SOMethod", Adaptor.fromEnum(preferences.CodeProperties.SecondOrderMethod) },
                        { "SRMethod", Adaptor.fromEnum(preferences.CodeProperties.StiffnessReductionMethod) },
                        { "Provision", Adaptor.fromEnum(preferences.CodeProperties.DesignProvision) },
                        { "SDC", Adaptor.fromEnum(preferences.CodeProperties.SeismicDesignCategory) },
                        { "ImpFactor", Adaptor.fromDouble(preferences.CodeProperties.ImportanceFactor) },
                        { "SystemRho", Adaptor.fromDouble(preferences.CodeProperties.Rho) },
                        { "SystemSds", Adaptor.fromDouble(preferences.CodeProperties.Sds) },
                        { "SystemR", Adaptor.fromDouble(preferences.CodeProperties.R) },
                        { "SystemCd", Adaptor.fromDouble(preferences.CodeProperties.Cd) },
                        { "Omega0", Adaptor.fromDouble(preferences.CodeProperties.Omega0) },
                        { "NLCoeff", Adaptor.fromDouble(preferences.CodeProperties.NLCoeff) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_AS_4100_1998(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<AS_4100_1998_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiTY", Adaptor.fromDouble(preferences.CodeProperties.PhiTY) },
                        { "PhiTF", Adaptor.fromDouble(preferences.CodeProperties.PhiTF) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "AMethod", Adaptor.fromEnum(preferences.CodeProperties.AnalysisMethod) },
                        { "SteelType", Adaptor.fromEnum(preferences.CodeProperties.SteelType) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_BS5950_2000(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<BS_5950_2000_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_CSA_S16_09(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<CSA_S16_09_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) },
                        { "SlenderMod", Adaptor.fromEnum(preferences.CodeProperties.SlenderSectionModification) },
                        { "AccRat", Adaptor.fromDouble(preferences.CodeProperties.SpectralAccelerationRatio) },
                        { "DuctFact", Adaptor.fromDouble(preferences.CodeProperties.Rd) },
                        { "OverFact", Adaptor.fromDouble(preferences.CodeProperties.Fa) },
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_CSA_S16_14(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<CSA_S16_14_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) },
                        { "SlenderMod", Adaptor.fromEnum(preferences.CodeProperties.SlenderSectionModification) },
                        { "AccRat", Adaptor.fromDouble(preferences.CodeProperties.SpectralAccelerationRatio) },
                        { "DuctFact", Adaptor.fromDouble(preferences.CodeProperties.Rd) },
                        { "OverFact", Adaptor.fromDouble(preferences.CodeProperties.Fa) },
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_EUROCODE_3_2005(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<EN_3_2005_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "GammaM0", Adaptor.fromDouble(preferences.CodeProperties.GammaM0) },
                        { "GammaM1", Adaptor.fromDouble(preferences.CodeProperties.GammaM1) },
                        { "GammaM2", Adaptor.fromDouble(preferences.CodeProperties.GammaM2) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) },
                        { "CombosEq", Adaptor.fromEnum(preferences.CodeProperties.DesignCombinationEquation) },
                        { "RelClass", Adaptor.fromEnum(preferences.CodeProperties.ReliabilityClass) },
                        { "KFactorMethod", Adaptor.fromEnum(preferences.CodeProperties.InteractionFactorsMethod) },
                        { "PDelta", Adaptor.toYesNo(preferences.CodeProperties.ConsiderPDeltaDone) },
                        { "CTorsion", Adaptor.toYesNo(preferences.CodeProperties.ConsiderTorsion) },
                        { "q", Adaptor.fromDouble(preferences.CodeProperties.q) },
                        { "Omega", Adaptor.fromDouble(preferences.CodeProperties.Omega) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_INDIAN_IS_800_2007(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<IS_800_2007_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "GammaM0", Adaptor.fromDouble(preferences.CodeProperties.GammaM0) },
                        { "GammaM1", Adaptor.fromDouble(preferences.CodeProperties.GammaM1) },
                        { "GammaM2", Adaptor.fromDouble(preferences.CodeProperties.GammaM2) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2008(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<NTC_2008_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "GammaM0", Adaptor.fromDouble(preferences.CodeProperties.GammaM0) },
                        { "GammaM1", Adaptor.fromDouble(preferences.CodeProperties.GammaM1) },
                        { "GammaM2", Adaptor.fromDouble(preferences.CodeProperties.GammaM2) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) },
                        { "CombosEq", Adaptor.fromEnum(preferences.CodeProperties.DesignCombinationEquation) },
                        { "RelClass", Adaptor.fromEnum(preferences.CodeProperties.ReliabilityClass) },
                        { "KFactorMethod", Adaptor.fromEnum(preferences.CodeProperties.InteractionFactorsMethod) },
                        { "q", Adaptor.fromDouble(preferences.CodeProperties.q0) },
                        { "Omega", Adaptor.fromDouble(preferences.CodeProperties.Omega) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2018(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<NTC_2018_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "GammaM0", Adaptor.fromDouble(preferences.CodeProperties.GammaM0) },
                        { "GammaM1", Adaptor.fromDouble(preferences.CodeProperties.GammaM1) },
                        { "GammaM2", Adaptor.fromDouble(preferences.CodeProperties.GammaM2) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) },
                        { "CombosEq", Adaptor.fromEnum(preferences.CodeProperties.DesignCombinationEquation) },
                        { "RelClass", Adaptor.fromEnum(preferences.CodeProperties.ReliabilityClass) },
                        { "KFactorMethod", Adaptor.fromEnum(preferences.CodeProperties.InteractionFactorsMethod) },
                        { "q", Adaptor.fromDouble(preferences.CodeProperties.q0) },
                        { "Omega", Adaptor.fromDouble(preferences.CodeProperties.Omega) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_KBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<AISC_360_16_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiTY", Adaptor.fromDouble(preferences.CodeProperties.PhiTY) },
                        { "PhiTF", Adaptor.fromDouble(preferences.CodeProperties.PhiTF) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVRolledI", Adaptor.fromDouble(preferences.CodeProperties.PhiVRolledI) },
                        { "PhiVT", Adaptor.fromDouble(preferences.CodeProperties.PhiVT) },
                        { "SeisCode", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicCode) },
                        { "SeisLoad", Adaptor.toYesNo(preferences.CodeProperties.UseSeismicLoading) },
                        { "PlugWeld", Adaptor.toYesNo(preferences.CodeProperties.IsDoublerPlatePlugWelded) },
                        { "HSSWelding", Adaptor.fromEnum(preferences.CodeProperties.HSSWeldingType) },
                        { "HSSReduceT", Adaptor.toYesNo(preferences.CodeProperties.ReduceHSSThickness) },
                        { "AMethod", Adaptor.fromEnum(preferences.CodeProperties.AnalysisMethod) },
                        { "SOMethod", Adaptor.fromEnum(preferences.CodeProperties.SecondOrderMethod) },
                        { "SRMethod", Adaptor.fromEnum(preferences.CodeProperties.StiffnessReductionMethod) },
                        { "Provision", Adaptor.fromEnum(preferences.CodeProperties.DesignProvision) },
                        { "SDC", Adaptor.fromEnum(preferences.CodeProperties.SeismicDesignCategory) },
                        { "ImpFactor", Adaptor.fromDouble(preferences.CodeProperties.ImportanceFactor) },
                        { "SystemRho", Adaptor.fromDouble(preferences.CodeProperties.Rho) },
                        { "SystemSds", Adaptor.fromDouble(preferences.CodeProperties.Sds) },
                        { "SystemR", Adaptor.fromDouble(preferences.CodeProperties.R) },
                        { "SystemCd", Adaptor.fromDouble(preferences.CodeProperties.Cd) },
                        { "Omega0", Adaptor.fromDouble(preferences.CodeProperties.Omega0) },
                        { "NLCoeff", Adaptor.fromDouble(preferences.CodeProperties.NLCoeff) }
                    });
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_NZS_3404_1997(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelDesigner.SteelDesignPreferences is
                SteelDesignPreferences<NZS_3404_1997_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "CheckDefl", Adaptor.toYesNo(preferences.CodeProperties.IsDeflectionConsidered) },
                        { "DLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioDL) },
                        { "SDLAndLLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioSDLAndLL) },
                        { "LLRat", Adaptor.fromDouble(preferences.CodeProperties.RatioLL) },
                        { "TotalRat", Adaptor.fromDouble(preferences.CodeProperties.RatioTotal) },
                        { "NetRat", Adaptor.fromDouble(preferences.CodeProperties.RatioNet) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiTY", Adaptor.fromDouble(preferences.CodeProperties.PhiTY) },
                        { "PhiTF", Adaptor.fromDouble(preferences.CodeProperties.PhiTF) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "AMethod", Adaptor.fromEnum(preferences.CodeProperties.AnalysisMethod) },
                        { "SteelType", Adaptor.fromEnum(preferences.CodeProperties.SteelType) }
                    });
            }
        }
    }
}
