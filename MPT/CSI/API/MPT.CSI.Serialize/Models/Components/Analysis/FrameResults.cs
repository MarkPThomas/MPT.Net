// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="FrameResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Results;
using PointLoads = MPT.CSI.Serialize.Models.Helpers.Loads.Definitions.Loads;

namespace MPT.CSI.Serialize.Models.Components.Analysis
{
    /// <summary>
    /// Class FrameResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class FrameResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>The name of the object.</value>
        public string ObjectName { get; protected set; }

        /// <summary>
        /// The frame forces
        /// </summary>
        private List<Tuple<StationResultsIdentifier, Forces>> _frameForces;
        /// <summary>
        /// Gets or sets the frame forces.
        /// </summary>
        /// <value>The frame forces.</value>
        public List<Tuple<StationResultsIdentifier, Forces>> FrameForces
        {
            get
            {
                if (_frameForces == null)
                {
                    
                }

                return _frameForces;
            }
        }

        /// <summary>
        /// The point forces
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, PointLoads>> _pointForces;
        /// <summary>
        /// Gets or sets the point forces.
        /// </summary>
        /// <value>The point forces.</value>
        public List<Tuple<ObjectPointResultsIdentifier, PointLoads>> PointForces
        {
            get
            {
                if (_pointForces == null)
                {
                 
                }

                return _pointForces;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameResults" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public FrameResults(string name)
        {
            ObjectName = name;
        }
        #endregion

        #region Fill

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            FrameForces?.Clear();
            PointForces?.Clear();
        }
        #endregion
    }
}
