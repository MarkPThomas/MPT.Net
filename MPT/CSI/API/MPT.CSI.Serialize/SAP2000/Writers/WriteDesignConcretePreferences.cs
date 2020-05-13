using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Concrete;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteDesignConcretePreferences
    {
        internal static void DefinePreferences(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_ACI_318_08_IBC_2009, setPREFERENCES_CONCRETE_DESIGN_ACI_318_08_IBC_2009);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_ACI_318_11, setPREFERENCES_CONCRETE_DESIGN_ACI_318_11);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_ACI_318_14, setPREFERENCES_CONCRETE_DESIGN_ACI_318_14);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_AS_3600_09, setPREFERENCES_CONCRETE_DESIGN_AS_3600_09);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_AS_3600_18, setPREFERENCES_CONCRETE_DESIGN_AS_3600_18);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_BS8110_97, setPREFERENCES_CONCRETE_DESIGN_BS8110_97);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_CSA_A233_04, setPREFERENCES_CONCRETE_DESIGN_CSA_A233_04);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_CSA_A233_14, setPREFERENCES_CONCRETE_DESIGN_CSA_A233_14);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_EUROCODE_2_2004, setPREFERENCES_CONCRETE_DESIGN_EUROCODE_2_2004);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_HONG_KONG_CP_2013, setPREFERENCES_CONCRETE_DESIGN_HONG_KONG_CP_2013);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_INDIAN_IS_456_2000, setPREFERENCES_CONCRETE_DESIGN_INDIAN_IS_456_2000);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_ITALIAN_NTC_2008, setPREFERENCES_CONCRETE_DESIGN_ITALIAN_NTC_2008);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_KBC_2009, setPREFERENCES_CONCRETE_DESIGN_KBC_2009);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_KBC_2016, setPREFERENCES_CONCRETE_DESIGN_KBC_2016);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2004, setPREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2004);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2017, setPREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2017);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_NZS_3101_06, setPREFERENCES_CONCRETE_DESIGN_NZS_3101_06);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_SINGAPORE_CP_65_99, setPREFERENCES_CONCRETE_DESIGN_SINGAPORE_CP_65_99);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_CONCRETE_DESIGN_TS_500_2000, setPREFERENCES_CONCRETE_DESIGN_TS_500_2000);
        }
        
        private static void setPREFERENCES_CONCRETE_DESIGN_ACI_318_08_IBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<ACI_318_08_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "SeisCat", Adaptor.fromEnum(preferences.CodeProperties.SeismicCategory) },
                        { "Rho", Adaptor.fromDouble(preferences.CodeProperties.Rho) },
                        { "Sds", Adaptor.fromDouble(preferences.CodeProperties.Sds) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiCTied", Adaptor.fromDouble(preferences.CodeProperties.PhiCTied) },
                        { "PhiCSpiral", Adaptor.fromDouble(preferences.CodeProperties.PhiCSpiral) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVSeismic", Adaptor.fromDouble(preferences.CodeProperties.PhiVSeismic) },
                        { "PhiVJoint", Adaptor.fromDouble(preferences.CodeProperties.PhiVJoint) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_ACI_318_11(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<ACI_318_11_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "SeisCat", Adaptor.fromEnum(preferences.CodeProperties.SeismicCategory) },
                        { "Rho", Adaptor.fromDouble(preferences.CodeProperties.Rho) },
                        { "Sds", Adaptor.fromDouble(preferences.CodeProperties.Sds) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiCTied", Adaptor.fromDouble(preferences.CodeProperties.PhiCTied) },
                        { "PhiCSpiral", Adaptor.fromDouble(preferences.CodeProperties.PhiCSpiral) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVSeismic", Adaptor.fromDouble(preferences.CodeProperties.PhiVSeismic) },
                        { "PhiVJoint", Adaptor.fromDouble(preferences.CodeProperties.PhiVJoint) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_ACI_318_14(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<ACI_318_14_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "SeisCat", Adaptor.fromEnum(preferences.CodeProperties.SeismicCategory) },
                        { "Rho", Adaptor.fromDouble(preferences.CodeProperties.Rho) },
                        { "Sds", Adaptor.fromDouble(preferences.CodeProperties.Sds) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiCTied", Adaptor.fromDouble(preferences.CodeProperties.PhiCTied) },
                        { "PhiCSpiral", Adaptor.fromDouble(preferences.CodeProperties.PhiCSpiral) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVSeismic", Adaptor.fromDouble(preferences.CodeProperties.PhiVSeismic) },
                        { "PhiVJoint", Adaptor.fromDouble(preferences.CodeProperties.PhiVJoint) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_AS_3600_09(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<AS_3600_2009_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "PhiT", Adaptor.fromEnum(preferences.CodeProperties.PhiT) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVSeismic", Adaptor.fromDouble(preferences.CodeProperties.PhiVSeismic) },
                        { "PhiVJoint", Adaptor.fromDouble(preferences.CodeProperties.PhiVJoint) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_AS_3600_18(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<AS_3600_2018_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "PhiT", Adaptor.fromEnum(preferences.CodeProperties.PhiT) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVSeismic", Adaptor.fromDouble(preferences.CodeProperties.PhiVSeismic) },
                        { "PhiVJoint", Adaptor.fromDouble(preferences.CodeProperties.PhiVJoint) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_BS8110_97(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<BS_8110_1997_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "GammaS", Adaptor.fromDouble(preferences.CodeProperties.GammaS) },
                        { "GammaC", Adaptor.fromDouble(preferences.CodeProperties.GammaC) },
                        { "GammaM", Adaptor.fromDouble(preferences.CodeProperties.GammaM) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_CSA_A233_04(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<CSA_A23_3_04_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "PhiS", Adaptor.fromDouble(preferences.CodeProperties.PhiS) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_CSA_A233_14(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<CSA_A23_3_14_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "PhiS", Adaptor.fromDouble(preferences.CodeProperties.PhiS) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_EUROCODE_2_2004(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<EN_2_2004_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "GammaS", Adaptor.fromDouble(preferences.CodeProperties.GammaS) },
                        { "GammaC", Adaptor.fromDouble(preferences.CodeProperties.GammaC) },
                        { "Theta0", Adaptor.fromDouble(preferences.CodeProperties.Theta0) },
                        { "AlphaCC", Adaptor.fromDouble(preferences.CodeProperties.AlphaCC) },
                        { "AlphaCT", Adaptor.fromDouble(preferences.CodeProperties.AlphaCT) },
                        { "AlphaLCC", Adaptor.fromDouble(preferences.CodeProperties.AlphaLCC) },
                        { "AlphaLCT", Adaptor.fromDouble(preferences.CodeProperties.AlphaLCT) },
                        { "CombosEq", Adaptor.fromEnum(preferences.CodeProperties.DesignCombinationEquation) },
                        { "RelClass", Adaptor.fromEnum(preferences.CodeProperties.ReliabilityClass) },
                        { "SOM", Adaptor.fromEnum(preferences.CodeProperties.SecondOrderMethod) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_HONG_KONG_CP_2013(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<HK_CP_2013_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "GammaS", Adaptor.fromDouble(preferences.CodeProperties.GammaS) },
                        { "GammaC", Adaptor.fromDouble(preferences.CodeProperties.GammaC) },
                        { "GammaM", Adaptor.fromDouble(preferences.CodeProperties.GammaM) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_INDIAN_IS_456_2000(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<IS_456_2000_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "GammaS", Adaptor.fromDouble(preferences.CodeProperties.GammaS) },
                        { "GammaC", Adaptor.fromDouble(preferences.CodeProperties.GammaC) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_ITALIAN_NTC_2008(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<NTC_2008_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "GammaS", Adaptor.fromDouble(preferences.CodeProperties.GammaS) },
                        { "GammaC", Adaptor.fromDouble(preferences.CodeProperties.GammaC) },
                        { "Theta0", Adaptor.fromDouble(preferences.CodeProperties.Theta0) },
                        { "AlphaCC", Adaptor.fromDouble(preferences.CodeProperties.AlphaCC) },
                        { "AlphaCT", Adaptor.fromDouble(preferences.CodeProperties.AlphaCT) },
                        { "AlphaLCC", Adaptor.fromDouble(preferences.CodeProperties.AlphaLCC) },
                        { "AlphaLCT", Adaptor.fromDouble(preferences.CodeProperties.AlphaLCT) },
                        { "CombosEq", Adaptor.fromEnum(preferences.CodeProperties.DesignCombinationEquation) },
                        { "RelClass", Adaptor.fromEnum(preferences.CodeProperties.ReliabilityClass) },
                        { "SOM", Adaptor.fromEnum(preferences.CodeProperties.SecondOrderMethod) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_KBC_2009(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<KBC_2009_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "Rho", Adaptor.fromDouble(preferences.CodeProperties.Rho) },
                        { "Sds", Adaptor.fromDouble(preferences.CodeProperties.Sds) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiCTied", Adaptor.fromDouble(preferences.CodeProperties.PhiCTied) },
                        { "PhiCSpiral", Adaptor.fromDouble(preferences.CodeProperties.PhiCSpiral) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVJoint", Adaptor.fromDouble(preferences.CodeProperties.PhiVJoint) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_KBC_2016(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<KBC_2016_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "Rho", Adaptor.fromDouble(preferences.CodeProperties.Rho) },
                        { "Sds", Adaptor.fromDouble(preferences.CodeProperties.Sds) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiCTied", Adaptor.fromDouble(preferences.CodeProperties.PhiCTied) },
                        { "PhiCSpiral", Adaptor.fromDouble(preferences.CodeProperties.PhiCSpiral) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVJoint", Adaptor.fromDouble(preferences.CodeProperties.PhiVJoint) }
                    });
            }
        }

        /// <summary>
        /// Sets the preferences concrete design mexican RCDF 2004.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2004(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<RCDF_2004_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiCTied", Adaptor.fromDouble(preferences.CodeProperties.PhiCTied) },
                        { "PhiCSpiral", Adaptor.fromDouble(preferences.CodeProperties.PhiCSpiral) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_MEXICAN_RCDF_2017(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<RCDF_2017_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiCTied", Adaptor.fromDouble(preferences.CodeProperties.PhiCTied) },
                        { "PhiCSpiral", Adaptor.fromDouble(preferences.CodeProperties.PhiCSpiral) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_NZS_3101_06(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<NZS_3101_2006_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "PhiB", Adaptor.fromEnum(preferences.CodeProperties.PhiB) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiT) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "Omega", Adaptor.fromDouble(preferences.CodeProperties.Omega) },
                        { "PhiZero", Adaptor.fromDouble(preferences.CodeProperties.PhiZero) },
                        { "Rm", Adaptor.fromDouble(preferences.CodeProperties.Rm) },
                        { "Rv", Adaptor.fromDouble(preferences.CodeProperties.Rv) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_SINGAPORE_CP_65_99(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<SS_CP_65_1999_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) },
                        { "GammaS", Adaptor.fromDouble(preferences.CodeProperties.GammaS) },
                        { "GammaC", Adaptor.fromDouble(preferences.CodeProperties.GammaC) },
                        { "GammaM", Adaptor.fromDouble(preferences.CodeProperties.GammaM) }
                    });
            }
        }

        private static void setPREFERENCES_CONCRETE_DESIGN_TS_500_2000(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.ConcreteDesigner.ConcreteDesignPreferences is
                ConcreteDesignPreferences<TS_500_2000_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "NumCurves", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionCurves) },
                        { "NumPoints", Adaptor.fromInteger(preferences.CodeProperties.NumberOfInteractionPoints) },
                        { "MinEccen", Adaptor.toYesNo(preferences.CodeProperties.ConsiderMinimumEccentricity) },
                        { "PatLLF", Adaptor.fromDouble(preferences.CodeProperties.PatternLiveLoadFactor) },
                        { "UFLimit", Adaptor.fromDouble(preferences.CodeProperties.UtilizationFactorLimit) }
                    });
            }
        }
    }
}
