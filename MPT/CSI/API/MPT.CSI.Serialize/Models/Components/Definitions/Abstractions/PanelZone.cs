// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-24-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="PanelZone.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Abstractions;

namespace MPT.CSI.Serialize.Models.Components.Definitions.Abstractions
{
    /// <summary>
    /// Class PanelZone.
    /// </summary>
    public class PanelZone 
    {
        /// <summary>
        /// The results
        /// </summary>
        private PanelZoneResults _results;
        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>The results.</value>
        public PanelZoneResults Results => _results ?? (_results = new PanelZoneResults(NodeName));

        /// <summary>
        /// Name of the node associated with the panel zone.
        /// </summary>
        /// <value>The name of the node.</value>
        public string NodeName { get; protected set; }

        /// <summary>
        /// Gets the panel zone properties.
        /// </summary>
        /// <value>The panel zone properties.</value>
        public PanelZoneProperties PanelZoneProperties { get; internal set; }


        /// <summary>
        /// Returns the panel zone assignment data for a point object.
        /// If no panel zone assignment is made to the point object, an error is returned.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>PanelZone.</returns>
        internal static PanelZone Factory(string name)
        {
            PanelZone panelZone = new PanelZone(name);
            return panelZone;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PanelZone" /> class.
        /// </summary>
        /// <param name="name">The name of the node associated with the panel zone.</param>
        protected PanelZone(string name)
        {
            NodeName = name;
        }
    }
}
