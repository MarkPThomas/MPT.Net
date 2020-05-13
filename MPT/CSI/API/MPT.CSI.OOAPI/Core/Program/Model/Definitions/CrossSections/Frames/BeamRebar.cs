// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="BeamRebar.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiFrameSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.FrameSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Class BeamRebar.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class BeamRebar : CSiOoApiBaseBase
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The frame section.</value>
        protected ApiFrameSection _apiFrameSection => getApiFrameSection(_apiApp);

        /// <summary>
        /// Gets or sets the name of the cross section.
        /// </summary>
        /// <value>The name of the cross section.</value>
        internal string CrossSectionName { get; set; }

        /// <summary>
        /// Gets or sets the detailing.
        /// </summary>
        /// <value>The detailing.</value>
        public BeamRebarDetailing Detailing { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="BeamRebar"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        public BeamRebar(ApiCSiApplication app, string name) : base(app)
        {
            CrossSectionName = name;
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns beam rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete.
        /// This function applies only to the following section types:
        /// <see cref="eFrameSectionType.TSection" />;
        /// <see cref="eFrameSectionType.Angle" />;
        /// <see cref="eFrameSectionType.Rectangular" />;
        /// <see cref="eFrameSectionType.Circle" />
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Fill()
        {
            try
            {
                _apiFrameSection.GetRebarBeam(CrossSectionName,
                    out var materialNameLongitudinal,
                    out var materialNameConfinement,
                    out var coverTop,
                    out var coverBottom,
                    out var topLeftArea,
                    out var topRightArea,
                    out var bottomLeftArea,
                    out var bottomRightArea);

                Detailing = new BeamRebarDetailing()
                {
                    MaterialNameLongitudinal = materialNameLongitudinal,
                    MaterialNameConfinement = materialNameConfinement,
                    CoverTop = coverTop,
                    CoverBottom = coverBottom,
                    TopLeftArea = topLeftArea,
                    TopRightArea = topRightArea,
                    BottomLeftArea = bottomLeftArea,
                    BottomRightArea = bottomRightArea
                };
            }
            catch (CSiException e)
            {
                Console.WriteLine(e);
                Detailing = null;
            }
        }


        /// <summary>
        /// Assigns beam rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete.
        /// This function applies only to the following section types:
        /// <see cref="eFrameSectionType.TSection" />;
        /// <see cref="eFrameSectionType.Angle" />;
        /// <see cref="eFrameSectionType.Rectangular" />;
        /// <see cref="eFrameSectionType.Circle" />
        /// </summary>
        /// <param name="beamRebar">The beam rebar.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Set(BeamRebarDetailing beamRebar)
        {
            try
            {
                _apiFrameSection.SetRebarBeam(CrossSectionName,
                    beamRebar.MaterialNameLongitudinal,
                    beamRebar.MaterialNameConfinement,
                    beamRebar.CoverTop,
                    beamRebar.CoverBottom,
                    beamRebar.TopLeftArea,
                    beamRebar.TopRightArea,
                    beamRebar.BottomLeftArea,
                    beamRebar.BottomRightArea);

                Detailing = beamRebar;
            }
            catch (CSiException e)
            {
                Console.WriteLine(e);
                Detailing = null;
            }
        }
        #endregion
    }
}
