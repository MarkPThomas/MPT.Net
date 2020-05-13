// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="AreaResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Results;
using AnalysisLoads = MPT.CSI.Serialize.Models.Helpers.Loads.Assignments.Load;

namespace MPT.CSI.Serialize.Models.Components.Analysis
{
    /// <summary>
    /// Class AreaResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class AreaResults : AnalysisResults
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the name of the object.
        /// </summary>
        /// <value>The name of the object.</value>
        public string ObjectName { get; protected set; }

        /// <summary>
        /// The area force shell
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, ShellForce>> _areaForceShell;
        /// <summary>
        /// Gets or sets the area force shell.
        /// </summary>
        /// <value>The area force shell.</value>
        public List<Tuple<ObjectPointResultsIdentifier, ShellForce>> AreaForceShell
        {
            get
            {
                if (_areaForceShell == null)
                {
                }

                return _areaForceShell;
            }
        }

        /// <summary>
        /// The area joint force shell
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> _areaJointForceShell;
        /// <summary>
        /// Gets or sets the area joint force shell.
        /// </summary>
        /// <value>The area joint force shell.</value>
        public List<Tuple<ObjectPointResultsIdentifier, AnalysisLoads>> AreaJointForceShell
        {
            get
            {
                if (_areaJointForceShell == null)
                {
                }

                return _areaJointForceShell;
            }
        }

        /// <summary>
        /// The area stress shell
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, ShellStress>> _areaStressShell;
        /// <summary>
        /// Gets or sets the area stress shell.
        /// </summary>
        /// <value>The area stress shell.</value>
        public List<Tuple<ObjectPointResultsIdentifier, ShellStress>> AreaStressShell
        {
            get
            {
                if (_areaStressShell == null)
                {
                }

                return _areaStressShell;
            }
        }

        /// <summary>
        /// The area stress shell layered
        /// </summary>
        private List<Tuple<ObjectPointResultsIdentifier, LayeredShellStress>> _areaStressShellLayered;
        /// <summary>
        /// Gets or sets the area stress shell layered.
        /// </summary>
        /// <value>The area stress shell layered.</value>
        public List<Tuple<ObjectPointResultsIdentifier, LayeredShellStress>> AreaStressShellLayered
        {
            get
            {
                if (_areaStressShellLayered == null)
                {
                }

                return _areaStressShellLayered;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="AreaResults" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public AreaResults(string name) 
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
            AreaForceShell?.Clear();
            AreaJointForceShell?.Clear();
            AreaStressShell?.Clear();
            AreaStressShellLayered?.Clear();
        }
        #endregion
    }
}
