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
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model
{
    /// <summary>
    /// Class StructureObjects.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class StructureObjects : CSiOoApiBaseBase
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
        public Points Points => _points ?? (_points = new Points(_apiApp));

        /// <summary>
        /// The frames
        /// </summary>
        private Frames _frames;

        /// <summary>
        /// Gets the frames.
        /// </summary>
        /// <value>The frames.</value>
        public Frames Frames => _frames ?? (_frames = new Frames(_apiApp, FrameComponentProperties));

        /// <summary>
        /// The areas
        /// </summary>
        private Areas _areas;
        /// <summary>
        /// Gets the areas.
        /// </summary>
        /// <value>The areas.</value>
        public Areas Areas => _areas ?? (_areas = new Areas(_apiApp, AreaComponentProperties));

        /// <summary>
        /// The links
        /// </summary>
        private Links _links;

        /// <summary>
        /// Gets the links.
        /// </summary>
        /// <value>The links.</value>
        public Links Links => _links ?? (_links = new Links(_apiApp));

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        private Cables _cables;

        public Cables Cables => _cables ?? (_cables = new Cables(_apiApp));
        

        private Tendons _tendons;

        public Tendons Tendons => _tendons ?? (_tendons = new Tendons(_apiApp));

    
        private Solids _solids;

        public Solids Solids => _solids ?? (_solids = new Solids(_apiApp));
#endif
        #endregion

        #region Initialization 
        /// <summary>
        /// Initializes a new instance of the <see cref="StructureObjects" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="frameComponents">The frame component.</param>
        /// <param name="areaComponents">The area components.</param>
        internal StructureObjects(ApiCSiApplication app,
            StructureComponentsProperties<FrameSection> frameComponents,
            StructureComponentsProperties<AreaSection> areaComponents) : base(app)
        {
            FrameComponentProperties = frameComponents;
            FrameComponentProperties.Points = _points;

            AreaComponentProperties = areaComponents;
            AreaComponentProperties.Points = _points;
        }
        #endregion
    }
}
