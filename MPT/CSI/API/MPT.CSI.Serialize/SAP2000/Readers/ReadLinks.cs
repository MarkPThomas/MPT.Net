using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Readers
{
    internal static class ReadLinks
    {

        internal static void DefineLinks(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.CONNECTIVITY_LINK, setCONNECTIVITY_LINK);
        }

        internal static void AssignLinks(SAP2000Reader reader)
        {
            reader.ReadSingleTable(SAP2000Tables.LINK_PROPERTY_ASSIGNMENTS, setLINK_PROPERTY_ASSIGNMENTS);
        }

        internal static void LoadLinks(SAP2000Reader reader)
        {

        }

        
        
        /// <summary>
        /// Sets the connectivity link.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_LINK(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Link link = model.Structure.Links.FillItem(tableRow["Link"]);
                if (tableRow.ContainsKey("GUID")) link.GUID = tableRow["GUID"];
                link.PointNames.Add(tableRow["JointI"]);
                link.PointNames.Add(tableRow["JointJ"]);
            }
        }


        /// <summary>
        /// Sets the link property assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setLINK_PROPERTY_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Dictionary<string, string> tableRow in table)
            {
                Link link = model.Structure.Links[tableRow["Link"]];
                link.SectionName = tableRow["LinkProp"];
                link.SectionNameFrequencyDependent = tableRow["LinkFDProp"];
                link.PropertyModifier = Adaptor.toDouble(tableRow["PropMod"]);
            }
        }
    }
}
