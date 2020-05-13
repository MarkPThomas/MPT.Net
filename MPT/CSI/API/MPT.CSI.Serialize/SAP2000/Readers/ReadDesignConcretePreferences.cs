using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadDesignConcretePreferences
    {
        internal static void DefinePreferences(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_ACI_318_08_IBC_2009, setPREFERENCES_CONCRETE_DESIGN_ACI_318_08_IBC_2009);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_ACI_318_11, setPREFERENCES_CONCRETE_DESIGN_ACI_318_11);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_ACI_318_14, setPREFERENCES_CONCRETE_DESIGN_ACI_318_14);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_AS_3600_09, setPREFERENCES_CONCRETE_DESIGN_AS_3600_09);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_AS_3600_18, setPREFERENCES_CONCRETE_DESIGN_AS_3600_18);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_BS8110_97, setPREFERENCES_CONCRETE_DESIGN_BS8110_97);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_CSA_A233_04, setPREFERENCES_CONCRETE_DESIGN_CSA_A233_04);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_CSA_A233_14, setPREFERENCES_CONCRETE_DESIGN_CSA_A233_14);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_EUROCODE_2_2004, setPREFERENCES_CONCRETE_DESIGN_EUROCODE_2_2004);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_HONG_KONG_CP_2013, setPREFERENCES_CONCRETE_DESIGN_HONG_KONG_CP_2013);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_INDIAN_IS_456_2000, setPREFERENCES_CONCRETE_DESIGN_INDIAN_IS_456_2000);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_ITALIAN_NTC_2008, setPREFERENCES_CONCRETE_DESIGN_ITALIAN_NTC_2008);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_KBC_2009, setPREFERENCES_CONCRETE_DESIGN_KBC_2009);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_KBC_2016, setPREFERENCES_CONCRETE_DESIGN_KBC_2016);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2004, setPREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2004);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2017, setPREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2017);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_NZS_3101_06, setPREFERENCES_CONCRETE_DESIGN_NZS_3101_06);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_SINGAPORE_CP_65_99, setPREFERENCES_CONCRETE_DESIGN_SINGAPORE_CP_65_99);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_TS_500_2000, setPREFERENCES_CONCRETE_DESIGN_TS_500_2000);
        }
        
        private static void setPREFERENCES_CONCRETE_DESIGN_ACI_318_08_IBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                ACI_318_08_Preferences preferences = new ACI_318_08_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    SeismicCategory = Enums.EnumLibrary.ConvertStringToEnumByDescription<ACI_318_08_Preferences.SeismicCategories>(tableRow["SeisCat"]),
                    Rho = Adaptor.toDouble(tableRow["Rho"]),
                    Sds = Adaptor.toDouble(tableRow["Sds"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiCTied = Adaptor.toDouble(tableRow["PhiCTied"]),
                    PhiCSpiral = Adaptor.toDouble(tableRow["PhiCSpiral"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVSeismic = Adaptor.toDouble(tableRow["PhiVSeismic"]),
                    PhiVJoint = Adaptor.toDouble(tableRow["PhiVJoint"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<ACI_318_08_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_ACI_318_11(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                ACI_318_11_Preferences preferences = new ACI_318_11_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    SeismicCategory = Enums.EnumLibrary.ConvertStringToEnumByDescription<ACI_318_11_Preferences.SeismicCategories>(tableRow["SeisCat"]),
                    Rho = Adaptor.toDouble(tableRow["Rho"]),
                    Sds = Adaptor.toDouble(tableRow["Sds"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiCTied = Adaptor.toDouble(tableRow["PhiCTied"]),
                    PhiCSpiral = Adaptor.toDouble(tableRow["PhiCSpiral"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVSeismic = Adaptor.toDouble(tableRow["PhiVSeismic"]),
                    PhiVJoint = Adaptor.toDouble(tableRow["PhiVJoint"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<ACI_318_11_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_ACI_318_14(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                ACI_318_14_Preferences preferences = new ACI_318_14_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    SeismicCategory = Enums.EnumLibrary.ConvertStringToEnumByDescription<ACI_318_14_Preferences.SeismicCategories>(tableRow["SeisCat"]),
                    Rho = Adaptor.toDouble(tableRow["Rho"]),
                    Sds = Adaptor.toDouble(tableRow["Sds"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiCTied = Adaptor.toDouble(tableRow["PhiCTied"]),
                    PhiCSpiral = Adaptor.toDouble(tableRow["PhiCSpiral"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVSeismic = Adaptor.toDouble(tableRow["PhiVSeismic"]),
                    PhiVJoint = Adaptor.toDouble(tableRow["PhiVJoint"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<ACI_318_14_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_AS_3600_09(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AS_3600_2009_Preferences preferences = new AS_3600_2009_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVSeismic = Adaptor.toDouble(tableRow["PhiVSeismic"]),
                    PhiVJoint = Adaptor.toDouble(tableRow["PhiVJoint"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<AS_3600_2009_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_AS_3600_18(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AS_3600_2018_Preferences preferences = new AS_3600_2018_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVSeismic = Adaptor.toDouble(tableRow["PhiVSeismic"]),
                    PhiVJoint = Adaptor.toDouble(tableRow["PhiVJoint"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<AS_3600_2018_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_BS8110_97(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                BS_8110_1997_Preferences preferences = new BS_8110_1997_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    GammaS = Adaptor.toDouble(tableRow["GammaS"]),
                    GammaC = Adaptor.toDouble(tableRow["GammaC"]),
                    GammaM = Adaptor.toDouble(tableRow["GammaM"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<BS_8110_1997_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_CSA_A233_04(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                CSA_A23_3_04_Preferences preferences = new CSA_A23_3_04_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    PhiS = Adaptor.toDouble(tableRow["PhiS"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<CSA_A23_3_04_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_CSA_A233_14(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                CSA_A23_3_14_Preferences preferences = new CSA_A23_3_14_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    PhiS = Adaptor.toDouble(tableRow["PhiS"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<CSA_A23_3_14_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_EUROCODE_2_2004(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                EN_2_2004_Preferences preferences = new EN_2_2004_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    GammaS = Adaptor.toDouble(tableRow["GammaS"]),
                    GammaC = Adaptor.toDouble(tableRow["GammaC"]),
                    Theta0 = Adaptor.toDouble(tableRow["Theta0"]),
                    AlphaCC = Adaptor.toDouble(tableRow["AlphaCC"]),
                    AlphaCT = Adaptor.toDouble(tableRow["AlphaCT"]),
                    AlphaLCC = Adaptor.toDouble(tableRow["AlphaLCC"]),
                    AlphaLCT = Adaptor.toDouble(tableRow["AlphaLCT"]),
                    DesignCombinationEquation = Enums.EnumLibrary.ConvertStringToEnumByDescription<EN_2_2004_Preferences.DesignCombinationEquations>(tableRow["CombosEq"]),
                    ReliabilityClass = Enums.EnumLibrary.ConvertStringToEnumByDescription<EN_2_2004_Preferences.ReliabilityClasses>(tableRow["RelClass"]),
                    SecondOrderMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<EN_2_2004_Preferences.SecondOrderMethods>(tableRow["SOM"])
                };
                preferences.SetCountryDefault(Enums.EnumLibrary.ConvertStringToEnumByDescription<EN_2_2004_Preferences.Countries>(tableRow["Country"]));

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<EN_2_2004_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_HONG_KONG_CP_2013(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                HK_CP_2013_Preferences preferences = new HK_CP_2013_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    GammaS = Adaptor.toDouble(tableRow["GammaS"]),
                    GammaC = Adaptor.toDouble(tableRow["GammaC"]),
                    GammaM = Adaptor.toDouble(tableRow["GammaM"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<HK_CP_2013_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_INDIAN_IS_456_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                IS_456_2000_Preferences preferences = new IS_456_2000_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    GammaS = Adaptor.toDouble(tableRow["GammaS"]),
                    GammaC = Adaptor.toDouble(tableRow["GammaC"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<IS_456_2000_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_ITALIAN_NTC_2008(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                NTC_2008_Preferences preferences = new NTC_2008_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    GammaS = Adaptor.toDouble(tableRow["GammaS"]),
                    GammaC = Adaptor.toDouble(tableRow["GammaC"]),
                    Theta0 = Adaptor.toDouble(tableRow["Theta0"]),
                    AlphaCC = Adaptor.toDouble(tableRow["AlphaCC"]),
                    AlphaCT = Adaptor.toDouble(tableRow["AlphaCT"]),
                    AlphaLCC = Adaptor.toDouble(tableRow["AlphaLCC"]),
                    AlphaLCT = Adaptor.toDouble(tableRow["AlphaLCT"]),
                    DesignCombinationEquation = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2008_Preferences.DesignCombinationEquations>(tableRow["CombosEq"]),
                    ReliabilityClass = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2008_Preferences.ReliabilityClasses>(tableRow["RelClass"]),
                    SecondOrderMethod = Enums.EnumLibrary.ConvertStringToEnumByDescription<NTC_2008_Preferences.SecondOrderMethods>(tableRow["SOM"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<NTC_2008_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_KBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                KBC_2009_Preferences preferences = new KBC_2009_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    Rho = Adaptor.toDouble(tableRow["Rho"]),
                    Sds = Adaptor.toDouble(tableRow["Sds"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiCTied = Adaptor.toDouble(tableRow["PhiCTied"]),
                    PhiCSpiral = Adaptor.toDouble(tableRow["PhiCSpiral"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVJoint = Adaptor.toDouble(tableRow["PhiVJoint"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<KBC_2009_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_KBC_2016(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                KBC_2016_Preferences preferences = new KBC_2016_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    Rho = Adaptor.toDouble(tableRow["Rho"]),
                    Sds = Adaptor.toDouble(tableRow["Sds"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiCTied = Adaptor.toDouble(tableRow["PhiCTied"]),
                    PhiCSpiral = Adaptor.toDouble(tableRow["PhiCSpiral"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVJoint = Adaptor.toDouble(tableRow["PhiVJoint"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<KBC_2016_Preferences>.Factory(preferences);
            }
        }

        /// <summary>
        /// Sets the preferences concrete design mexican RCDF 2004.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2004(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                RCDF_2004_Preferences preferences = new RCDF_2004_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiCTied = Adaptor.toDouble(tableRow["PhiCTied"]),
                    PhiCSpiral = Adaptor.toDouble(tableRow["PhiCSpiral"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<RCDF_2004_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2017(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                RCDF_2017_Preferences preferences = new RCDF_2017_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiCTied = Adaptor.toDouble(tableRow["PhiCTied"]),
                    PhiCSpiral = Adaptor.toDouble(tableRow["PhiCSpiral"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<RCDF_2017_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_NZS_3101_06(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                NZS_3101_2006_Preferences preferences = new NZS_3101_2006_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiT = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    Omega = Adaptor.toDouble(tableRow["Omega"]),
                    PhiZero = Adaptor.toDouble(tableRow["PhiZero"]),
                    Rm = Adaptor.toDouble(tableRow["Rm"]),
                    Rv = Adaptor.toDouble(tableRow["Rv"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<NZS_3101_2006_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_SINGAPORE_CP_65_99(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                SS_CP_65_1999_Preferences preferences = new SS_CP_65_1999_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                    GammaS = Adaptor.toDouble(tableRow["GammaS"]),
                    GammaC = Adaptor.toDouble(tableRow["GammaC"]),
                    GammaM = Adaptor.toDouble(tableRow["GammaM"])
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<SS_CP_65_1999_Preferences>.Factory(preferences);
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_TS_500_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                TS_500_2000_Preferences preferences = new TS_500_2000_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    NumberOfInteractionCurves = Adaptor.toInteger(tableRow["NumCurves"]),
                    NumberOfInteractionPoints = Adaptor.toInteger(tableRow["NumPoints"]),
                    ConsiderMinimumEccentricity = Adaptor.fromYesNo(tableRow["MinEccen"]),
                    PatternLiveLoadFactor = Adaptor.toDouble(tableRow["PatLLF"]),
                    UtilizationFactorLimit = Adaptor.toDouble(tableRow["UFLimit"]),
                };

                model.Design.ConcreteDesigner.ConcreteDesignPreferences = ConcreteDesignPreferences<TS_500_2000_Preferences>.Factory(preferences);
            }
        }
    }
}
