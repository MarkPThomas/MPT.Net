// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="ModalEigen.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Analysis;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Eigen mode load case.
    /// </summary>
    /// <seealso cref="Modal" />
    public class ModalEigen : Modal
    {
        #region Fields & Properties
        public virtual double ShiftFrequency { get; internal set; }
        public virtual double CutoffFrequencyRadius { get; internal set; }
        public virtual double ConvergenceTolerance { get; internal set; }
        public virtual bool AllowAutoFrequencyShifting { get; internal set; }

        public virtual List<ModalEigenLoad> ModalEigenLoads { get; internal set; } = new List<ModalEigenLoad>();
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static ModalEigen Factory(Analyzer analyzer, string uniqueName)
        {
            ModalEigen loadCase = new ModalEigen(analyzer, uniqueName);

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalEigen" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected ModalEigen(Analyzer analyzer, string name) 
            : base(analyzer, name)
        { }
        
        #endregion
    }
}
