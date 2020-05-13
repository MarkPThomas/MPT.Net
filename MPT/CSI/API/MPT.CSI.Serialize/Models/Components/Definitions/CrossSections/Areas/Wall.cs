// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="Wall.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Wall.
    /// </summary>
    /// <seealso cref="AreaSection{T}" />
    public class Wall : Shell<WallProperties>
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
        internal static Wall Factory(
            Materials.Materials material,
            string uniqueName, 
            WallProperties properties = null)
        {
            Wall areaSection = new Wall(material, uniqueName) { _sectionProperties = properties };

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Wall" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected Wall(
            Materials.Materials material,
            string name) : base(material, name) { }
        #endregion
        
    }
}
