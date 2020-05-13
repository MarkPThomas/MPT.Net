using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Aluminum;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadDesignAluminumPreferences
    {
        internal static void DefinePreferences(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_ALUMINUM_DESIGN_AA_ASD_2000, setPREFERENCES_ALUMINUM_DESIGN_AA_ASD_2000);
            reader.ReadSingleTable(SAP2000Tables.PREFERENCES_ALUMINUM_DESIGN_AA_LRFD_2000, setPREFERENCES_ALUMINUM_DESIGN_AA_LRFD_2000);
        }
        
        /// <summary>
        /// Sets the preferences aluminum design aa asd 2000.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_ALUMINUM_DESIGN_AA_ASD_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AA_ASD_2000_Preferences preferences = new AA_ASD_2000_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AA_ASD_2000_Preferences.FrameTypes>(tableRow["FrameType"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    LateralFactor = Adaptor.toDouble(tableRow["LatFact"]),
                    UseLateralFactor = Adaptor.fromYesNo(tableRow["UseLatFact"]),
                    IsBridgeTypeStructure = Adaptor.fromYesNo(tableRow["Bridge"])
                };

                model.Design.AluminumDesigner.AluminumDesignPreferences = AluminumDesignPreferences<AA_ASD_2000_Preferences>.Factory(preferences);
            }
        }

        /// <summary>
        /// Sets the preferences aluminum design aa LRFD 2000.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setPREFERENCES_ALUMINUM_DESIGN_AA_LRFD_2000(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                AA_LRFD_2000_Preferences preferences = new AA_LRFD_2000_Preferences
                {
                    MultiResponseCase = Enums.EnumLibrary.ConvertStringToEnumByDescription<eMultiResponseCase>(tableRow["THDesign"]),
                    FrameType = Enums.EnumLibrary.ConvertStringToEnumByDescription<AA_LRFD_2000_Preferences.FrameTypes>(tableRow["FrameType"]),
                    DemandCapacityRatioLimit = Adaptor.toDouble(tableRow["SRatioLimit"]),
                    MaximumIterations = Adaptor.toInteger(tableRow["MaxIter"]),
                    PhiY = Adaptor.toDouble(tableRow["PhiY"]),
                    PhiB = Adaptor.toDouble(tableRow["PhiB"]),
                    PhiC = Adaptor.toDouble(tableRow["PhiC"]),
                    PhiU = Adaptor.toDouble(tableRow["PhiU"]),
                    PhiCC = Adaptor.toDouble(tableRow["PhiCC"]),
                    PhiCP = Adaptor.toDouble(tableRow["PhiCP"]),
                    PhiV = Adaptor.toDouble(tableRow["PhiV"]),
                    PhiVP = Adaptor.toDouble(tableRow["PhiVP"]),
                    PhiW = Adaptor.toDouble(tableRow["PhiW"])
                };

                model.Design.AluminumDesigner.AluminumDesignPreferences = AluminumDesignPreferences<AA_LRFD_2000_Preferences>.Factory(preferences);
            }
        }
    }
}
