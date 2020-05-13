using System.Collections.Generic;
using MPT.CSI.Serialize.Models;
using MPT.CSI.Serialize.Models.Components.StructureLayout;

namespace MPT.CSI.Serialize.SAP2000.Writers
{
    internal static class WriteLinks
    {

        internal static void DefineLinks(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.CONNECTIVITY_LINK, setCONNECTIVITY_LINK);
        }

        internal static void AssignLinks(SAP2000Writer writer)
        {
            writer.WriteSingleTable(SAP2000Tables.LINK_PROPERTY_ASSIGNMENTS, setLINK_PROPERTY_ASSIGNMENTS);
        }

        internal static void LoadLinks(SAP2000Writer writer)
        {
            // TODO: Complete LoadLinks
        }

        
        
        /// <summary>
        /// Sets the connectivity link.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setCONNECTIVITY_LINK(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Link link in model.Structure.Links)
            {
                Dictionary<string, string> tableRow = new Dictionary<string, string>
                {
                    { "Link", Adaptor.ToStringEntryLimited(link.Name) },
                    { "JointI", Adaptor.ToStringEntryLimited(link.PointNames[0]) },
                    { "JointJ", Adaptor.ToStringEntryLimited(link.PointNames[1]) }
                };

                if (!string.IsNullOrEmpty(link.GUID))
                {
                    tableRow["GUID"] = Adaptor.ToStringEntryLimited(link.GUID);
                }

                table.Add(tableRow);
            }
        }


        /// <summary>
        /// Sets the link property assignments.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="table">The table.</param>
        private static void setLINK_PROPERTY_ASSIGNMENTS(Model model, List<Dictionary<string, string>> table)
        {
            foreach (Link link in model.Structure.Links)
            {
                table.Add(
                    new Dictionary<string, string>
                    {
                        { "Link", Adaptor.ToStringEntryLimited(link.Name) },
                        { "LinkProp", Adaptor.ToStringEntryLimited(link.SectionName) },
                        { "LinkFDProp", Adaptor.ToStringEntryLimited(link.SectionNameFrequencyDependent) },
                        { "PropMod", Adaptor.fromDouble(link.PropertyModifier) }
                    });
            }
        }
    }
}
