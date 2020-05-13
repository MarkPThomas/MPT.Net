// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-17-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="RectangleSection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class RectangleSection.
    /// </summary>
    /// <seealso cref="FrameSection{T}" />
    public class RectangleSection : FrameSection<RectangleSectionProperties>,
        IBeamRebar, IColumnRebar
    {
        #region Fields & Properties       
        /// <summary>
        /// The column rebar for the section.
        /// </summary>
        /// <value>The column rebar.</value>
        public ColumnRebar ColumnRebar => _columnRebar;

        /// <summary>
        /// The beam rebar for the section.
        /// </summary>
        /// <value>The beam rebar.</value>
        public BeamRebar BeamRebar => _beamRebar;
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>RectangleSection.</returns>
        internal static RectangleSection Factory(
            Materials.Materials material,
            string uniqueName, 
            RectangleSectionProperties properties = null)
        {
            RectangleSection frameSection = new RectangleSection(material, uniqueName) { _sectionProperties = properties };

            return frameSection;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleSection" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="name">The name.</param>
        protected RectangleSection(
            Materials.Materials material,
            string name) : base(material, name, eFrameSectionType.Rectangular)
        {

        }
        #endregion
        
        #region Rebar
        /// <summary>
        /// Sets the beam rebar.
        /// </summary>
        /// <param name="beamRebar">The beam rebar.</param>
        public void SetBeamRebar(BeamRebarDetailing beamRebar)
        {
            setBeamRebar(beamRebar);
        }
        
        /// <summary>
        /// Sets the column rebar.
        /// </summary>
        /// <param name="columnRebar">The column rebar.</param>
        public void SetColumnRebar(ColumnRebarDetailing columnRebar)
        {
            setColumnRebar(columnRebar);
        }
        #endregion
    }
}
