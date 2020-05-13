using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Aluminum;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteDesignAluminumPreferences
    {
        internal static void DefinePreferences(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_ALUMINUM_DESIGN_AA_ASD_2000, setPREFERENCES_ALUMINUM_DESIGN_AA_ASD_2000);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_ALUMINUM_DESIGN_AA_LRFD_2000, setPREFERENCES_ALUMINUM_DESIGN_AA_LRFD_2000);
        }
        
        /// <summary>
        /// Sets the preferences aluminum design aa asd 2000.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_ALUMINUM_DESIGN_AA_ASD_2000(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.AluminumDesigner.AluminumDesignPreferences is 
                    AluminumDesignPreferences<AA_ASD_2000_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "LatFact", Adaptor.fromDouble(preferences.CodeProperties.LateralFactor) },
                        { "UseLatFact", Adaptor.toYesNo(preferences.CodeProperties.UseLateralFactor) },
                        { "Bridge", Adaptor.toYesNo(preferences.CodeProperties.IsBridgeTypeStructure) }
                    });
            }
        }

        /// <summary>
        /// Sets the preferences aluminum design aa LRFD 2000.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_ALUMINUM_DESIGN_AA_LRFD_2000(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.AluminumDesigner.AluminumDesignPreferences is
                AluminumDesignPreferences<AA_LRFD_2000_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "PhiY", Adaptor.fromDouble(preferences.CodeProperties.PhiY) },
                        { "PhiB", Adaptor.fromDouble(preferences.CodeProperties.PhiB) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiC) },
                        { "PhiU", Adaptor.fromDouble(preferences.CodeProperties.PhiU) },
                        { "PhiCC", Adaptor.fromDouble(preferences.CodeProperties.PhiCC) },
                        { "PhiCP", Adaptor.fromDouble(preferences.CodeProperties.PhiCP) },
                        { "PhiV", Adaptor.fromDouble(preferences.CodeProperties.PhiV) },
                        { "PhiVP", Adaptor.fromDouble(preferences.CodeProperties.PhiVP) },
                        { "PhiW", Adaptor.fromDouble(preferences.CodeProperties.PhiW) }
                    });
            }
        }
    }
}
