using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Components.StructureLayout;
using MPT.CSI.Serialize.Models.Helpers.Definitions;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadGroups
    {
        /// <summary>
        /// Sets the groups 1 definitions.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetGROUPS_1_DEFINITIONS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Group group = model.Groupings.Groups.FillItem(tableRow["GroupName"]);
                group.ColorName = tableRow["Color"];

                GroupProperties properties = new GroupProperties
                {
                    SpecifiedForSelection = Adaptor.fromYesNo(tableRow["Selection"]),
                    SpecifiedForSectionCutDefinition = Adaptor.fromYesNo(tableRow["SectionCut"]),
                    SpecifiedForSteelDesign = Adaptor.fromYesNo(tableRow["Steel"]),
                    SpecifiedForConcreteDesign = Adaptor.fromYesNo(tableRow["Concrete"]),
                    SpecifiedForAluminumDesign = Adaptor.fromYesNo(tableRow["Aluminum"]),
                    SpecifiedForColdFormedDesign = Adaptor.fromYesNo(tableRow["ColdFormed"]),
                    SpecifiedForStaticNLActiveStage = Adaptor.fromYesNo(tableRow["Stage"]),
                    SpecifiedForBridgeResponseOutput = Adaptor.fromYesNo(tableRow["Bridge"]),
                    SpecifiedForAutoSeismicOutput = Adaptor.fromYesNo(tableRow["AutoSeismic"]),
                    SpecifiedForAutoWindOutput = Adaptor.fromYesNo(tableRow["AutoWind"]),
                    SelectedForSteelDesign = Adaptor.fromYesNo(tableRow["SelDesSteel"]),
                    SelectedForAluminumDesign = Adaptor.fromYesNo(tableRow["SelDesAlum"]),
                    SelectedForColdFormedDesign = Adaptor.fromYesNo(tableRow["SelDesCold"]),
                    SpecifiedForMassAndWeight = Adaptor.fromYesNo(tableRow["MassWeight"]),
                };
                group.SetProperties(properties);
            }
        }

        /// <summary>
        /// Sets the groups 2 assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        internal static void SetGroups_2_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Group group = model.Groupings.Groups.FillItem(tableRow["GroupName"]);
                switch (tableRow["ObjectType"])
                {
                    case "Joint":
                        Point point = model.Structure.Points.FillItem(tableRow["ObjectLabel"]);
                        group.Add(point);
                        break;
                    case "Frame":
                        Frame frame = model.Structure.Frames.FillItem(tableRow["ObjectLabel"]);
                        group.Add(frame);
                        break;
                    case "Area":
                        Area area = model.Structure.Areas.FillItem(tableRow["ObjectLabel"]);
                        group.Add(area);
                        break;
                    case "Link":
                        Link link = model.Structure.Links.FillItem(tableRow["ObjectLabel"]);
                        group.Add(link);
                        break;
                    case "Tendon":
                        Tendon tendon = model.Structure.Tendons.FillItem(tableRow["ObjectLabel"]);
                        group.Add(tendon);
                        break;
                    case "Cable":
                        Cable cable = model.Structure.Cables.FillItem(tableRow["ObjectLabel"]);
                        group.Add(cable);
                        break;
                    case "Solid":
                        Solid solid = model.Structure.Solids.FillItem(tableRow["ObjectLabel"]);
                        group.Add(solid);
                        break;
                }
            }
        }
    }
}
