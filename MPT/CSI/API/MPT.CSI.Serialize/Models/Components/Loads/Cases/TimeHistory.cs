// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistory.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Time history load case.
    /// </summary>
    /// <seealso cref="LoadCase" />
    public abstract class TimeHistory : LoadCase
    {
        #region Fields & Properties    
        /// <summary>
        /// The loads associated with the load case.
        /// </summary>
        public virtual LoadsTimeHistory Loads { get; internal set; }

        /// <summary>
        /// The number of output steps calculated for the load case.
        /// </summary>
        /// <value>The output steps.</value>
        public virtual int OutputSteps { get; internal set; }

        /// <summary>
        /// The length in time [s] for each output step.
        /// </summary>
        /// <value>The size of the step.</value>
        public virtual double StepSize { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistory" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistory(Analyzer analyzer, string name) : base(analyzer, name)
        {
        }
        #endregion
    }
}
