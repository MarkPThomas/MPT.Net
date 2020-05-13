// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="LinkResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Results;
using AnalysisLoads = MPT.CSI.Serialize.Models.Helpers.Loads.Definitions.Loads;

namespace MPT.CSI.Serialize.Models.Components.Analysis
{
    /// <summary>
    /// Class LinkResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class LinkResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>The name of the object.</value>
        public string ObjectName { get; protected set; }

        /// <summary>
        /// The link forces
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, Forces>> _linkForces;
        /// <summary>
        /// Gets or sets the link forces.
        /// </summary>
        /// <value>The link forces.</value>
        public List<Tuple<ObjectPointResultsIdentifier, Forces>> LinkForces
        {
            get
            {
                if (_linkForces == null)
                {
                   
                }

                return _linkForces;
            }
        }

        /// <summary>
        /// The link joint force
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> _linkJointForce;
        /// <summary>
        /// Gets or sets the link joint force.
        /// </summary>
        /// <value>The link joint force.</value>
        public List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> LinkJointForce
        {
            get
            {
                if (_linkJointForce == null)
                {
                   
                }

                return _linkJointForce;
            }
        }

        /// <summary>
        /// The link deformations
        /// </summary>
        private List<Tuple<ObjectResultsIdentifier, Deformations>> _linkDeformations;
        /// <summary>
        /// Gets or sets the link deformations.
        /// </summary>
        /// <value>The link deformations.</value>
        public List<Tuple<ObjectResultsIdentifier, Deformations>> LinkDeformations
        {
            get
            {
                if (_linkDeformations == null)
                {
                   
                }

                return _linkDeformations;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkResults" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LinkResults(string name)
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
            LinkForces?.Clear();
            LinkDeformations?.Clear();
            LinkJointForce?.Clear();
        }
        #endregion
    }
}
