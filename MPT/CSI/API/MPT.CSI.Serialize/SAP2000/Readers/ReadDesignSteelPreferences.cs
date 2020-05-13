using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Steel;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadDesignSteelPreferences
    {
        internal static void DefinePreferences(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_AISC360_05_IBC2006, setPREFERENCES_STEEL_DESIGN_AISC360_05_IBC2006);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_AISC_360_10, setPREFERENCES_STEEL_DESIGN_AISC_360_10);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_AISC_360_16, setPREFERENCES_STEEL_DESIGN_AISC_360_16);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_AS_4100_1998, setPREFERENCES_STEEL_DESIGN_AS_4100_1998);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_BS5950_2000, setPREFERENCES_STEEL_DESIGN_BS5950_2000);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_CSA_S16_09, setPREFERENCES_STEEL_DESIGN_CSA_S16_09);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_CSA_S16_14, setPREFERENCES_STEEL_DESIGN_CSA_S16_14);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_EUROCODE_3_2005, setPREFERENCES_STEEL_DESIGN_EUROCODE_3_2005);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_INDIAN_IS_800_2007, setPREFERENCES_STEEL_DESIGN_INDIAN_IS_800_2007);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2008, setPREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2008);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2018, setPREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2018);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_KBC_2009, setPREFERENCES_STEEL_DESIGN_KBC_2009);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_STEEL_DESIGN_NZS_3404_1997, setPREFERENCES_STEEL_DESIGN_NZS_3404_1997);
        }
        
        /// <summary>
        /// Sets the preferences steel design.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_STEEL_DESIGN_AISC360_05_IBC2006(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AISC_360_05_Preferences preferences = new AISC_360_05_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_05_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiTY = Adaptor.toDouble(tableRow["PhiTY"]),
                    PhiTF = Adaptor.toDouble(tableRow["PhiTF"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVRolledI = Adaptor.toDouble(tableRow["PhiVRolledI"]),
                    PhiVT = Adaptor.toDouble(tableRow["PhiVT"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"]),
                    HSSWeldingType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_05_Preferences.HSSWeldingTypes>(tableRow["HSSWelding"]),
                    ReduceHSSThickness = Adaptor.fromYesNo(tableRow["HSSReduceT"]),
                    AnalysisMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_05_Preferences.AnalysisMethods>(tableRow["AMethod"]),
                    SecondOrderMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_05_Preferences.SecondOrderMethods>(tableRow["SOMethod"]),
                    StiffnessReductionMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_05_Preferences.StiffnessReductionMethods>(tableRow["SRMethod"]),
                    DesignProvision = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_05_Preferences.DesignProvisions>(tableRow["Provision"]),
                    SeismicDesignCategory = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_05_Preferences.SeismicDesignCategories>(tableRow["SDC"]),
                    ImportanceFactor = Adaptor.toDouble(tableRow["ImpFactor"]),
                    Rho = Adaptor.toDouble(tableRow["SystemRho"]),
                    Sds = Adaptor.toDouble(tableRow["SystemSds"]),
                    R = Adaptor.toDouble(tableRow["SystemR"]),
                    Cd = Adaptor.toDouble(tableRow["SystemCd"]),
                    Omega0 = Adaptor.toDouble(tableRow["Omega0"]),
                    NLCoeff = Adaptor.toDouble(tableRow["NLCoeff"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<AISC_360_05_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_AISC_360_10(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AISC_360_10_Preferences preferences = new AISC_360_10_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_10_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiTY = Adaptor.toDouble(tableRow["PhiTY"]),
                    PhiTF = Adaptor.toDouble(tableRow["PhiTF"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVRolledI = Adaptor.toDouble(tableRow["PhiVRolledI"]),
                    PhiVT = Adaptor.toDouble(tableRow["PhiVT"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"]),
                    HSSWeldingType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_10_Preferences.HSSWeldingTypes>(tableRow["HSSWelding"]),
                    ReduceHSSThickness = Adaptor.fromYesNo(tableRow["HSSReduceT"]),
                    AnalysisMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_10_Preferences.AnalysisMethods>(tableRow["AMethod"]),
                    SecondOrderMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_10_Preferences.SecondOrderMethods>(tableRow["SOMethod"]),
                    StiffnessReductionMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_10_Preferences.StiffnessReductionMethods>(tableRow["SRMethod"]),
                    DesignProvision = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_10_Preferences.DesignProvisions>(tableRow["Provision"]),
                    SeismicDesignCategory = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_10_Preferences.SeismicDesignCategories>(tableRow["SDC"]),
                    ImportanceFactor = Adaptor.toDouble(tableRow["ImpFactor"]),
                    Rho = Adaptor.toDouble(tableRow["SystemRho"]),
                    Sds = Adaptor.toDouble(tableRow["SystemSds"]),
                    R = Adaptor.toDouble(tableRow["SystemR"]),
                    Cd = Adaptor.toDouble(tableRow["SystemCd"]),
                    Omega0 = Adaptor.toDouble(tableRow["Omega0"]),
                    NLCoeff = Adaptor.toDouble(tableRow["NLCoeff"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<AISC_360_10_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_AISC_360_16(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AISC_360_16_Preferences preferences = new AISC_360_16_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_16_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiTY = Adaptor.toDouble(tableRow["PhiTY"]),
                    PhiTF = Adaptor.toDouble(tableRow["PhiTF"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVRolledI = Adaptor.toDouble(tableRow["PhiVRolledI"]),
                    PhiVT = Adaptor.toDouble(tableRow["PhiVT"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"]),
                    HSSWeldingType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_16_Preferences.HSSWeldingTypes>(tableRow["HSSWelding"]),
                    ReduceHSSThickness = Adaptor.fromYesNo(tableRow["HSSReduceT"]),
                    AnalysisMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_16_Preferences.AnalysisMethods>(tableRow["AMethod"]),
                    SecondOrderMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_16_Preferences.SecondOrderMethods>(tableRow["SOMethod"]),
                    StiffnessReductionMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_16_Preferences.StiffnessReductionMethods>(tableRow["SRMethod"]),
                    DesignProvision = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_16_Preferences.DesignProvisions>(tableRow["Provision"]),
                    SeismicDesignCategory = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISC_360_16_Preferences.SeismicDesignCategories>(tableRow["SDC"]),
                    ImportanceFactor = Adaptor.toDouble(tableRow["ImpFactor"]),
                    Rho = Adaptor.toDouble(tableRow["SystemRho"]),
                    Sds = Adaptor.toDouble(tableRow["SystemSds"]),
                    R = Adaptor.toDouble(tableRow["SystemR"]),
                    Cd = Adaptor.toDouble(tableRow["SystemCd"]),
                    Omega0 = Adaptor.toDouble(tableRow["Omega0"]),
                    NLCoeff = Adaptor.toDouble(tableRow["NLCoeff"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<AISC_360_16_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_AS_4100_1998(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AS_4100_1998_Preferences preferences = new AS_4100_1998_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AS_4100_1998_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiTY = Adaptor.toDouble(tableRow["PhiTY"]),
                    PhiTF = Adaptor.toDouble(tableRow["PhiTF"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    AnalysisMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<AS_4100_1998_Preferences.AnalysisMethods>(tableRow["AMethod"]),
                    SteelType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AS_4100_1998_Preferences.SteelTypes>(tableRow["SteelType"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<AS_4100_1998_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_BS5950_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                BS_5950_2000_Preferences preferences = new BS_5950_2000_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<BS_5950_2000_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<BS_5950_2000_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_CSA_S16_09(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                CSA_S16_09_Preferences preferences = new CSA_S16_09_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<CSA_S16_09_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"]),
                    SlenderSectionModification = Enums.EnumLibrary.ConvertStringToEnumByDescription<CSA_S16_09_Preferences.SlenderSectionModifications>(tableRow["SlenderMod"]),
                    SpectralAccelerationRatio = Adaptor.toDouble(tableRow["AccRat"]),
                    Rd = Adaptor.toDouble(tableRow["DuctFact"]),
                    Fa = Adaptor.toDouble(tableRow["OverFact"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<CSA_S16_09_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_CSA_S16_14(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                CSA_S16_14_Preferences preferences = new CSA_S16_14_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<CSA_S16_14_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"]),
                    SlenderSectionModification = Enums.EnumLibrary.ConvertStringToEnumByDescription<CSA_S16_14_Preferences.SlenderSectionModifications>(tableRow["SlenderMod"]),
                    SpectralAccelerationRatio = Adaptor.toDouble(tableRow["AccRat"]),
                    Rd = Adaptor.toDouble(tableRow["DuctFact"]),
                    Fa = Adaptor.toDouble(tableRow["OverFact"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<CSA_S16_14_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_EUROCODE_3_2005(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                EN_3_2005_Preferences preferences = new EN_3_2005_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<EN_3_2005_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    GammaM0 = Adaptor.toDouble(tableRow["GammaM0"]),
                    GammaM1 = Adaptor.toDouble(tableRow["GammaM1"]),
                    GammaM2 = Adaptor.toDouble(tableRow["GammaM2"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"]),
                    DesignCombinationEquation = Enums.EnumLibrary.ConvertStringToEnumByDescription<EN_3_2005_Preferences.DesignCombinationEquations>(tableRow["CombosEq"]),
                    ReliabilityClass = Enums.EnumLibrary.ConvertStringToEnumByDescription<EN_3_2005_Preferences.ReliabilityClasses>(tableRow["RelClass"]),
                    InteractionFactorsMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<EN_3_2005_Preferences.InteractionFactorsMethods>(tableRow["KFactorMethod"]),
                    ConsiderPDeltaDone = Adaptor.fromYesNo(tableRow["PDelta"]),
                    ConsiderTorsion = Adaptor.fromYesNo(tableRow["CTorsion"]),
                    q = Adaptor.toDouble(tableRow["q"]),
                    Omega = Adaptor.toDouble(tableRow["Omega"])
                };
                preferences.SetCountryDefault(Enums.EnumLibrary.ConvertStringToEnumByDescription<EN_3_2005_Preferences.Countries>(tableRow["Country"]));

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<EN_3_2005_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_INDIAN_IS_800_2007(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                IS_800_2007_Preferences preferences = new IS_800_2007_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<IS_800_2007_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    GammaM0 = Adaptor.toDouble(tableRow["GammaM0"]),
                    GammaM1 = Adaptor.toDouble(tableRow["GammaM1"]),
                    GammaM2 = Adaptor.toDouble(tableRow["GammaM2"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<IS_800_2007_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2008(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                NTC_2008_Preferences preferences = new NTC_2008_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2008_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    GammaM0 = Adaptor.toDouble(tableRow["GammaM0"]),
                    GammaM1 = Adaptor.toDouble(tableRow["GammaM1"]),
                    GammaM2 = Adaptor.toDouble(tableRow["GammaM2"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"]),
                    DesignCombinationEquation = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2008_Preferences.DesignCombinationEquations>(tableRow["CombosEq"]),
                    ReliabilityClass = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2008_Preferences.ReliabilityClasses>(tableRow["RelClass"]),
                    InteractionFactorsMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2008_Preferences.InteractionFactorsMethods>(tableRow["KFactorMethod"]),
                    q0 = Adaptor.toDouble(tableRow["q"]),
                    Omega = Adaptor.toDouble(tableRow["Omega"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<NTC_2008_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_ITALIAN_NTC_2018(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                NTC_2018_Preferences preferences = new NTC_2018_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2018_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    GammaM0 = Adaptor.toDouble(tableRow["GammaM0"]),
                    GammaM1 = Adaptor.toDouble(tableRow["GammaM1"]),
                    GammaM2 = Adaptor.toDouble(tableRow["GammaM2"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"]),
                    DesignCombinationEquation = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2018_Preferences.DesignCombinationEquations>(tableRow["CombosEq"]),
                    ReliabilityClass = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2018_Preferences.ReliabilityClasses>(tableRow["RelClass"]),
                    InteractionFactorsMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2018_Preferences.InteractionFactorsMethods>(tableRow["KFactorMethod"]),
                    q0 = Adaptor.toDouble(tableRow["q"]),
                    Omega = Adaptor.toDouble(tableRow["Omega"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<NTC_2018_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_KBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                KBC_2009_Preferences preferences = new KBC_2009_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<KBC_2009_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiTY = Adaptor.toDouble(tableRow["PhiTY"]),
                    PhiTF = Adaptor.toDouble(tableRow["PhiTF"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVRolledI = Adaptor.toDouble(tableRow["PhiVRolledI"]),
                    PhiVT = Adaptor.toDouble(tableRow["PhiVT"]),
                    UseSeismicCode = Adaptor.fromYesNo(tableRow["SeisCode"]),
                    UseSeismicLoading = Adaptor.fromYesNo(tableRow["SeisLoad"]),
                    IsDoublerPlatePlugWelded = Adaptor.fromYesNo(tableRow["PlugWeld"]),
                    HSSWeldingType = Enums.EnumLibrary.ConvertStringToEnumByDescription<KBC_2009_Preferences.HSSWeldingTypes>(tableRow["HSSWelding"]),
                    ReduceHSSThickness = Adaptor.fromYesNo(tableRow["HSSReduceT"]),
                    AnalysisMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<KBC_2009_Preferences.AnalysisMethods>(tableRow["AMethod"]),
                    SecondOrderMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<KBC_2009_Preferences.SecondOrderMethods>(tableRow["SOMethod"]),
                    StiffnessReductionMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<KBC_2009_Preferences.StiffnessReductionMethods>(tableRow["SRMethod"]),
                    SeismicDesignCategory = Enums.EnumLibrary.ConvertStringToEnumByDescription<KBC_2009_Preferences.SeismicDesignCategories>(tableRow["SDC"]),
                    ImportanceFactor = Adaptor.toDouble(tableRow["ImpFactor"]),
                    Rho = Adaptor.toDouble(tableRow["SystemRho"]),
                    Sds = Adaptor.toDouble(tableRow["SystemSds"]),
                    R = Adaptor.toDouble(tableRow["SystemR"]),
                    Cd = Adaptor.toDouble(tableRow["SystemCd"]),
                    Omega0 = Adaptor.toDouble(tableRow["Omega0"]),
                    NLCoeff = Adaptor.toDouble(tableRow["NLCoeff"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<KBC_2009_Preferences>.Factory(preferences)
                    );
            }
        }

        private static void setPREFERENCES_STEEL_DESIGN_NZS_3404_1997(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                NZS_3404_1997_Preferences preferences = new NZS_3404_1997_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<NZS_3404_1997_Preferences.FrameTypes>(tableRow["FrameType"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    IsDeflectionConsidered = Adaptor.fromYesNo(tableRow["CheckDefl"]),
                    RatioDL = Adaptor.toDouble(tableRow["DLRat"]),
                    RatioSDLAndLL = Adaptor.toDouble(tableRow["SDLAndLLRat"]),
                    RatioLL = Adaptor.toDouble(tableRow["LLRat"]),
                    RatioTotal = Adaptor.toDouble(tableRow["TotalRat"]),
                    RatioNet = Adaptor.toDouble(tableRow["NetRat"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiTY = Adaptor.toDouble(tableRow["PhiTY"]),
                    PhiTF = Adaptor.toDouble(tableRow["PhiTF"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    AnalysisMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<NZS_3404_1997_Preferences.AnalysisMethods>(tableRow["AMethod"]),
                    SteelType = Enums.EnumLibrary.ConvertStringToEnumByDescription<NZS_3404_1997_Preferences.SteelTypes>(tableRow["SteelType"])
                };

                model.Design.SteelDesigner.SteelDesignPreferences.UpdateItem(
                    SteelDesignPreferences<NZS_3404_1997_Preferences>.Factory(preferences)
                    );
            }
        }
    }
}
