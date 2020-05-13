// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-12-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-12-2018
// ***********************************************************************
// <copyright file="ColumnRebar.cs" company="">
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
    /// Class ColumnRebar.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class ColumnRebar : CSiOoApiBaseBase
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
        public ColumnRebarDetailing Detailing { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnRebar"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        public ColumnRebar(ApiCSiApplication app, string name) : base(app)
        {
            CrossSectionName = name;
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns column rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or else this function returns an error.
        /// Calling this function for any type of frame section property other than the following returns an error:<para /><see cref="eFrameSectionType.Rectangular" /><para /><see cref="eFrameSectionType.Circle" />
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Fill()
        {
            try
            {
                _apiFrameSection.GetRebarColumn(CrossSectionName,
                    out var materialNameLongitudinal,
                    out var materialNameConfinement,
                    out var rebarConfiguration,
                    out var confinementType,
                    out var cover,
                    out var numberOfCircularBars,
                    out var numberOfRectangularBars3Axis,
                    out var numberOfRectangularBars2Axis,
                    out var rebarSize,
                    out var tieSize,
                    out var tieSpacingLongitudinal,
                    out var numberOfConfinementBars2Axis,
                    out var numberOfConfinementBars3Axis,
                    out var toBeDesigned);

                Detailing = new ColumnRebarDetailing()
                {
                    MaterialNameLongitudinal = materialNameLongitudinal,
                    MaterialNameConfinement = materialNameConfinement,
                    RebarConfiguration = rebarConfiguration,
                    ConfinementType = confinementType,
                    Cover = cover,
                    NumberOfCircularBars = numberOfCircularBars,
                    NumberOfRectangularBars3Axis = numberOfRectangularBars3Axis,
                    NumberOfRectangularBars2Axis = numberOfRectangularBars2Axis,
                    RebarSize = rebarSize,
                    TieSize = tieSize,
                    TieSpacingLongitudinal = tieSpacingLongitudinal,
                    NumberOfConfinementBars2Axis = numberOfConfinementBars2Axis,
                    NumberOfConfinementBars3Axis = numberOfConfinementBars3Axis,
                    ToBeDesigned = toBeDesigned
                };
            }
            catch (CSiException e)
            {
                Console.WriteLine(e);
                Detailing = null;
            }
        }


        /// <summary>
        /// Assigns column rebar data to frame sections.
        /// The material assigned to the specified frame section property must be concrete or else this function returns an error.
        /// Calling this function for any type of frame section property other than the following returns an error:<para /><see cref="eFrameSectionType.Rectangular" /><para /><see cref="eFrameSectionType.Circle" />
        /// </summary>
        /// <param name="columnRebar">The column rebar.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Set(ColumnRebarDetailing columnRebar)
        {
            try
            {
                _apiFrameSection.SetRebarColumn(CrossSectionName,
                    columnRebar.MaterialNameLongitudinal,
                    columnRebar.MaterialNameConfinement,
                    columnRebar.RebarConfiguration,
                    columnRebar.ConfinementType,
                    columnRebar.Cover,
                    columnRebar.NumberOfCircularBars,
                    columnRebar.NumberOfRectangularBars3Axis,
                    columnRebar.NumberOfRectangularBars2Axis,
                    columnRebar.RebarSize,
                    columnRebar.TieSize,
                    columnRebar.TieSpacingLongitudinal,
                    columnRebar.NumberOfConfinementBars2Axis,
                    columnRebar.NumberOfConfinementBars3Axis,
                    columnRebar.ToBeDesigned);

                Detailing = columnRebar;
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
