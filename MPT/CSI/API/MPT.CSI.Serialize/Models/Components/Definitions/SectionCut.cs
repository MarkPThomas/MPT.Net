// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-21-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-22-2018
// ***********************************************************************
// <copyright file="SectionCut.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Definitions;
using MPT.CSI.Serialize.Models.Helpers.Results;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;
using MPT.CSI.Serialize.Models.Support;
using AnalysisLoads = MPT.CSI.Serialize.Models.Helpers.Loads.Definitions.Loads;

namespace MPT.CSI.Serialize.Models.Components.Definitions
{
    /// <summary>
    /// Class SectionCut.
    /// </summary>
    /// <seealso cref="UniqueName" />
    public class SectionCut : UniqueName
    {
        #region Fields & Properties
        public eSectionCut CutDefinition { get; internal set; }

        /// <summary>
        /// The group used to define a section cut.
        /// The group needs to contain the elements to cut and the connecting nodes on the side/plane to cut.
        /// </summary>
        /// <value>The group.</value>
        public Group Group { get; internal set; }

        public eSectionResultType SectionCutType { get; internal set; }

        public bool IsReportedAtDefaultLocation { get; internal set; }

        public Coordinate3DCartesian UserDefinedLocation { get; internal set; } = new Coordinate3DCartesian();

        /// <summary>
        /// Gets a value indicating whether this instance is positive element side.
        /// Positive also coincides with 'Top' & 'Right' locations.
        /// </summary>
        /// <value><c>true</c> if this instance is positive element side; otherwise, <c>false</c>.</value>
        public bool IsPositiveElementSide { get; internal set; }

        /// <summary>
        /// The angle of the analysis section cut local axis.
        /// </summary>
        /// <value>The angle.</value>
        public AngleLocalAxes Angle { get; internal set; } = new AngleLocalAxes();

        /// <summary>
        /// Gets a value indicating whether the analysis section angle [uses advanced axes].
        /// </summary>
        /// <value><c>true</c> if [uses advanced axes]; otherwise, <c>false</c>.</value>
        public bool UsesAdvancedAxes { get; internal set; }

        /// <summary>
        /// The angle from Global X to Local 2 of the design section cut localt axis.
        /// </summary>
        /// <value>The design angle.</value>
        public double DesignAngle { get; internal set; }

        /// <summary>
        /// The quadrilateral planes that define the section cut.
        /// </summary>
        public List<Quadrilateral> Quadrilaterals = new List<Quadrilateral>();

        /// <summary>
        /// The results
        /// </summary>
        private readonly SectionCutResults _results;

        /// <summary>
        /// The analysis forces
        /// </summary>
        private List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> _analysisForces;
        /// <summary>
        /// Gets or sets the analysis forces.
        /// </summary>
        /// <value>The analysis forces.</value>
        public List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> AnalysisForces
        {
            get
            {
                if (_analysisForces == null)
                {
                }

                return _analysisForces;
            }
        }


        /// <summary>
        /// The design forces
        /// </summary>
        private List<Tuple<SectionCutResultsIdentifier, Forces>> _designForces;
        /// <summary>
        /// Gets or sets the design forces.
        /// </summary>
        /// <value>The design forces.</value>
        public List<Tuple<SectionCutResultsIdentifier, Forces>> DesignForces
        {
            get
            {
                if (_designForces == null)
                {
                }

                return _designForces;
            }
        }
        #endregion

        #region Initialization    
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static SectionCut Factory(SectionCutResults results, string uniqueName)
        {
            SectionCut item = new SectionCut(results, uniqueName);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SectionCut" /> class.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <param name="name">The name.</param>
        protected SectionCut(SectionCutResults results, string name) : base(name)
        {
            _results = results;
        }

        #endregion

        #region Creation
        /// <summary>
        /// Adds a new section cut defined by a group to the model or reinitializes an existing section cut to be defined by a group.
        /// </summary>
        /// <param name="uniqueName">Name of the unique section cut.</param>
        /// <param name="group">The group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        /// <returns>SectionCut.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static SectionCut AddByGroup(
            string uniqueName,
            Group group,
            eSectionResultType sectionCutType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new section cut defined by a quadrilateral to the model or reinitializes an existing section cut to be defined by a quadrilateral.
        /// </summary>
        /// <param name="uniqueName">Name of the unique section cut.</param>
        /// <param name="group">The group associated with the section cut.</param>
        /// <param name="sectionCutType">The result type of the section cut.</param>
        /// <param name="coordinate1">This is one of four coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="coordinate2">This is one of four coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="coordinate3">This is one of four coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <param name="coordinate4">This is one of four coordinates, one for each of the four points defining the quadrilateral.</param>
        /// <returns>SectionCut.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static SectionCut AddByQuadrilateral(
            string uniqueName,
            Group group,
            eSectionResultType sectionCutType,
            Coordinate3DCartesian coordinate1,
            Coordinate3DCartesian coordinate2,
            Coordinate3DCartesian coordinate3,
            Coordinate3DCartesian coordinate4)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
