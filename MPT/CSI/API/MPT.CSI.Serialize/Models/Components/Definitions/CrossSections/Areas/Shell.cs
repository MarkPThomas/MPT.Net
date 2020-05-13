// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="Shell.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Shell.
    /// </summary>
    /// <seealso cref="Shell{T}" />
    public class Shell : Shell<ShellProperties>
    {
        #region Fields & Properties        
        /// <summary>
        /// Gets the shell layers.
        /// </summary>
        /// <value>The layers.</value>
        public ShellLayered Layers => getShellLayered();
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static Shell Factory(
            Materials.Materials material,
            string uniqueName, 
            ShellProperties properties = null)
        {
            Shell areaSection = new Shell(material, uniqueName) { _sectionProperties = properties };

            return areaSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellBase" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected Shell(
            Materials.Materials material,
            string name) : base(material, name)
        {
        }
        #endregion

        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public override void Set(ShellProperties properties)
        {
            setProperty(properties);
        }
    }
}