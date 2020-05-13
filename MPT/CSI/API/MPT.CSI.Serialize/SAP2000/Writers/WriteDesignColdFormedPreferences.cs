using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ColdFormed;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteDesignColdFormedPreferences
    {
        internal static void DefinePreferences(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_COLD_FORMED_DESIGN_AISI_ASD96, setPREFERENCES_COLD_FORMED_DESIGN_AISI_ASD96);
            writer.WriteSingleTable(SAP2000Tables.PREFERENCES_COLD_FORMED_DESIGN_AISI_LRFD96, setPREFERENCES_COLD_FORMED_DESIGN_AISI_LRFD96);
        }
        
        /// <summary>
        /// Sets the preferences cold formed design aisi as D96.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_COLD_FORMED_DESIGN_AISI_ASD96(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelColdFormedDesigner.SteelColdFormedDesignPreferences is
                SteelColdFormedDesignPreferences<AISI_ASD_1996_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "OmegaBS", Adaptor.fromDouble(preferences.CodeProperties.OmegaBendingStiffened) },
                        { "OmegaBUS", Adaptor.fromDouble(preferences.CodeProperties.OmegaBendingUnstiffened) },
                        { "OmegaBLTB", Adaptor.fromDouble(preferences.CodeProperties.OmegaBendingLTB) },
                        { "OmegaVS", Adaptor.fromDouble(preferences.CodeProperties.OmegaShearSlender) },
                        { "OmegaVNS", Adaptor.fromDouble(preferences.CodeProperties.OmegaShearNonslender) },
                        { "OmegaT", Adaptor.fromDouble(preferences.CodeProperties.OmegaAxialTension) },
                        { "OmegaC", Adaptor.fromDouble(preferences.CodeProperties.OmegaAxialCompression) }
                    });
            }
        }

        /// <summary>
        /// Sets the preferences cold formed design aisi LRF D96.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_COLD_FORMED_DESIGN_AISI_LRFD96(Model model, List<Dictionary<string, string>> table)
        {
            if (model.Design.SteelColdFormedDesigner.SteelColdFormedDesignPreferences is
                SteelColdFormedDesignPreferences<AISI_LRFD_1996_Preferences> preferences)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "THDesign", Adaptor.fromEnum(preferences.CodeProperties.MultiResponseCase) },
                        { "FrameType", Adaptor.fromEnum(preferences.CodeProperties.FrameType) },
                        { "SRatioLimit", Adaptor.fromDouble(preferences.CodeProperties.DemandCapacityRatioLimit) },
                        { "MaxIter", Adaptor.fromInteger(preferences.CodeProperties.MaximumIterations) },
                        { "PhiBS", Adaptor.fromDouble(preferences.CodeProperties.PhiBendingStiffened) },
                        { "PhiBUS", Adaptor.fromDouble(preferences.CodeProperties.PhiBendingUnstiffened) },
                        { "PhiBLTB", Adaptor.fromDouble(preferences.CodeProperties.PhiBendingLTB) },
                        { "PhiVS", Adaptor.fromDouble(preferences.CodeProperties.PhiShearSlender) },
                        { "PhiVNS", Adaptor.fromDouble(preferences.CodeProperties.PhiShearNonslender) },
                        { "PhiT", Adaptor.fromDouble(preferences.CodeProperties.PhiAxialTension) },
                        { "PhiC", Adaptor.fromDouble(preferences.CodeProperties.PhiAxialCompression) }
                    });
            }
        }
    }
}
