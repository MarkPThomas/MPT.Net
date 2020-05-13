// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="PanelZoneResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Components.Analysis
{
    /// <summary>
    /// Class PanelZoneResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class PanelZoneResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the joint.
        /// </summary>
        /// <value>The name of the joint.</value>
        public string JointName { get; protected set; }

        /// <summary>
        /// The forces
        /// </summary>
        private List<Tuple<ElementPointResultsIdentifier, Forces>> _forces;
        /// <summary>
        /// Gets or sets the forces.
        /// </summary>
        /// <value>The forces.</value>
        public List<Tuple<ElementPointResultsIdentifier, Forces>> Forces
        {
            get
            {
                if (_forces == null)
                {
                    
                }

                return _forces;
            }
        }

        /// <summary>
        /// The deformations
        /// </summary>
        private List<Tuple<ElementResultsIdentifier, Deformations>> _deformations;
        /// <summary>
        /// Gets or sets the deformations.
        /// </summary>
        /// <value>The deformations.</value>
        public List<Tuple<ElementResultsIdentifier, Deformations>> Deformations
        {
            get
            {
                if (_deformations == null)
                {
                   
                }

                return _deformations;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="PanelZoneResults" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public PanelZoneResults(string name) 
        {
            JointName = name;
        }
        #endregion

        #region Fill
        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            Forces?.Clear();
            Deformations?.Clear();
        }
        #endregion
    }
}
