// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ShellLayered.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.ObjectModel;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class ShellLayered.
    /// </summary>
    /// <seealso cref="AreaSection{T}" />
    public class ShellLayered : ModelPropertyObject<ShellLayeredProperties>
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the shell layer data.
        /// </summary>
        /// <returns>ReadOnlyCollection&lt;ShellLayerProperties&gt;.</returns>
        protected ReadOnlyCollection<ShellLayerProperties> GetLayers()
        {
            if (Properties.Layers.Count == 0)
            {

            }

            return new ReadOnlyCollection<ShellLayerProperties>(Properties.Layers);
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static ShellLayered Factory(
            string uniqueName, 
            ShellLayeredProperties properties = null)
        {
            ShellLayered areaSection = new ShellLayered(uniqueName) { _properties = properties };

            return areaSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Serialize.Models.Components.Definitions.CrossSections.Areas.ShellBase" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ShellLayered(string name) : base(name)
        {
        }
        #endregion

        public override void Set(ShellLayeredProperties properties)
        {
            set(properties);
        }
    }
}
