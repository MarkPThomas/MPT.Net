// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ShellDesign.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************


using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class ShellDesign.
    /// </summary>
    /// <seealso cref="ModelPropertyObject" />
    public class ShellDesign : ModelPropertyObject<ShellDesignProperties>
    {
        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static ShellDesign Factory(string uniqueName, ShellDesignProperties properties = null)
        {
            ShellDesign areaSection = new ShellDesign(uniqueName) { _properties = properties };

            return areaSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellDesign" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ShellDesign(string name) : base(name)
        {
        }
        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public override void Set(ShellDesignProperties properties)
        {
            set(properties);
        }
        #endregion
    }
}
