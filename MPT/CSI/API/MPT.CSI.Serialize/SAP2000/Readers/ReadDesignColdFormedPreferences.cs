using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ColdFormed;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadDesignColdFormedPreferences
    {
        internal static void DefinePreferences(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_COLD_FORMED_DESIGN_AISI_ASD96, setPREFERENCES_COLD_FORMED_DESIGN_AISI_ASD96);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_COLD_FORMED_DESIGN_AISI_LRFD96, setPREFERENCES_COLD_FORMED_DESIGN_AISI_LRFD96);
        }
        
        /// <summary>
        /// Sets the preferences cold formed design aisi as D96.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_COLD_FORMED_DESIGN_AISI_ASD96(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AISI_ASD_1996_Preferences preferences = new AISI_ASD_1996_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISI_ASD_1996_Preferences.FrameTypes>(tableRow["FrameType"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    OmegaBendingStiffened = Adaptor.toDouble(tableRow["OmegaBS"]),
                    OmegaBendingUnstiffened = Adaptor.toDouble(tableRow["OmegaBUS"]),
                    OmegaBendingLTB = Adaptor.toDouble(tableRow["OmegaBLTB"]),
                    OmegaShearSlender = Adaptor.toDouble(tableRow["OmegaVS"]),
                    OmegaShearNonslender = Adaptor.toDouble(tableRow["OmegaVNS"]),
                    OmegaAxialTension = Adaptor.toDouble(tableRow["OmegaT"]),
                    OmegaAxialCompression = Adaptor.toDouble(tableRow["OmegaC"])
                };

                model.Design.SteelColdFormedDesigner.SteelColdFormedDesignPreferences = SteelColdFormedDesignPreferences<AISI_ASD_1996_Preferences>.Factory(preferences);
            }
        }

        /// <summary>
        /// Sets the preferences cold formed design aisi LRF D96.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_COLD_FORMED_DESIGN_AISI_LRFD96(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AISI_LRFD_1996_Preferences preferences = new AISI_LRFD_1996_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AISI_LRFD_1996_Preferences.FrameTypes>(tableRow["FrameType"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    PhiBendingStiffened = Adaptor.toDouble(tableRow["PhiBS"]),
                    PhiBendingUnstiffened = Adaptor.toDouble(tableRow["PhiBUS"]),
                    PhiBendingLTB = Adaptor.toDouble(tableRow["PhiBLTB"]),
                    PhiShearSlender = Adaptor.toDouble(tableRow["PhiVS"]),
                    PhiShearNonslender = Adaptor.toDouble(tableRow["PhiVNS"]),
                    PhiAxialTension = Adaptor.toDouble(tableRow["PhiT"]),
                    PhiAxialCompression = Adaptor.toDouble(tableRow["PhiC"])
                };

                model.Design.SteelColdFormedDesigner.SteelColdFormedDesignPreferences = SteelColdFormedDesignPreferences<AISI_LRFD_1996_Preferences>.Factory(preferences);
            }
        }
    }
}
