// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-17-2018
// ***********************************************************************
// <copyright file="StructureObjects.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Components.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components
{
    /// <summary>
    /// Class StructureObjects.
    /// </summary>
    public class StructureObjects
    {
        #region Fields & Properties
        /// <summary>
        /// The components properties
        /// </summary>
        internal readonly StructureComponentsProperties<FrameSection> FrameComponentProperties;

        /// <summary>
        /// The components properties
        /// </summary>
        internal readonly StructureComponentsProperties<AreaSection> AreaComponentProperties;


        /// <summary>
        /// The points
        /// </summary>
        private Points _points;
        /// <summary>
        /// Gets the points.
        /// </summary>
        /// <value>The points.</value>
        public Points Points => _points ?? (_points = new Points());

        /// <summary>
        /// The frames
        /// </summary>
        private Frames _frames;

        /// <summary>
        /// Gets the frames.
        /// </summary>
        /// <value>The frames.</value>
        public Frames Frames => _frames ?? (_frames = new Frames(FrameComponentProperties));

        /// <summary>
        /// The areas
        /// </summary>
        private Areas _areas;
        /// <summary>
        /// Gets the areas.
        /// </summary>
        /// <value>The areas.</value>
        public Areas Areas => _areas ?? (_areas = new Areas(AreaComponentProperties));

        /// <summary>
        /// The links
        /// </summary>
        private Links _links;

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>The links.</value>
        public Links Links => _links ?? (_links = new Links());

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        private Cables _cables;

        public Cables Cables => _cables ?? (_cables = new Cables());
        

        private Tendons _tendons;

        public Tendons Tendons => _tendons ?? (_tendons = new Tendons());

    
        private Solids _solids;

        public Solids Solids => _solids ?? (_solids = new Solids());
#endif
        #endregion

        #region Initialization 
        /// <summary>
        /// Initializes a new instance of the <see cref="StructureObjects" /> class.
        /// </summary>
        /// <param name="frameComponents">The frame component.</param>
        /// <param name="areaComponents">The area components.</param>
        internal StructureObjects(
            StructureComponentsProperties<FrameSection> frameComponents,
            StructureComponentsProperties<AreaSection> areaComponents)
        {
            FrameComponentProperties = frameComponents;
            FrameComponentProperties.Points = _points;

            AreaComponentProperties = areaComponents;
            AreaComponentProperties.Points = _points;
        }
        #endregion
    }
}
