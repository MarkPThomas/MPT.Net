using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteGroups
    {
        /// <summary>
        /// Sets the groups 1 definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetGROUPS_1_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Group group in model.Groupings.Groups)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"GroupName",  group.Name},
                        {"Color",  group.ColorName},
                        {"Selection", Adaptor.toYesNo(group.SpecifiedForSelection)},
                        {"SectionCut", Adaptor.toYesNo(group.SpecifiedForSectionCutDefinition)},
                        {"Steel", Adaptor.toYesNo(group.SpecifiedForSteelDesign)},
                        {"Concrete", Adaptor.toYesNo(group.SpecifiedForConcreteDesign)},
                        {"Aluminum", Adaptor.toYesNo(group.SpecifiedForAluminumDesign)},
                        {"ColdFormed", Adaptor.toYesNo(group.SpecifiedForColdFormedDesign)},
                        {"Stage", Adaptor.toYesNo(group.SpecifiedForStaticNLActiveStage)},
                        {"Bridge", Adaptor.toYesNo(group.SpecifiedForBridgeResponseOutput)},
                        {"AutoSeismic", Adaptor.toYesNo(group.SpecifiedForAutoSeismicOutput)},
                        {"AutoWind", Adaptor.toYesNo(group.SpecifiedForAutoWindOutput)},
                        {"SelDesSteel", Adaptor.toYesNo(group.SelectedForSteelDesign)},
                        {"SelDesAlum", Adaptor.toYesNo(group.SelectedForAluminumDesign)},
                        {"SelDesCold", Adaptor.toYesNo(group.SelectedForColdFormedDesign)},
                        {"MassWeight", Adaptor.toYesNo(group.SpecifiedForMassAndWeight)}
                    }
                    );
            }
        }

        /// <summary>
        /// Sets the groups 2 assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetGroups_2_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Group group in model.Groupings.Groups)
            {
                SetGroups_2_ASSIGNMENTS("Joint", group.Points, table);
                SetGroups_2_ASSIGNMENTS("Frame", group.Frames, table);
                SetGroups_2_ASSIGNMENTS("Area", group.Areas, table);
                SetGroups_2_ASSIGNMENTS("Link", group.Links, table);
                SetGroups_2_ASSIGNMENTS("Tendon", group.Tendons, table);
                SetGroups_2_ASSIGNMENTS("Cable", group.Cables, table);
                SetGroups_2_ASSIGNMENTS("Solid", group.Solids, table);
            }
        }

        private static void SetGroups_2_ASSIGNMENTS<T>(
            string objectType,
            List<T> items,
            List<Dictionary<string, string>> table) where T : IUniqueName
        {
            foreach (T item in items)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        {"ObjectType", objectType},
                        {"ObjectLabel", item.Name}
                    }
                );
            }
        }
    }
}
