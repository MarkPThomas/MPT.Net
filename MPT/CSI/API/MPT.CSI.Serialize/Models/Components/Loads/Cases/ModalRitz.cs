// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="ModalRitz.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Analysis;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class ModalRitz.
    /// </summary>
    /// <seealso cref="Modal" />
    public class ModalRitz : Modal
    {
        #region Fields & Properties
        public virtual List<ModalRitzLoad> ModalRitzLoads { get; internal set; } = new List<ModalRitzLoad>();
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static ModalRitz Factory(Analyzer analyzer, string uniqueName)
        {
            ModalRitz loadCase = new ModalRitz(analyzer, uniqueName);

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalRitz" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected ModalRitz(Analyzer analyzer, string name) 
            : base(analyzer, name) { }
        #endregion
    }
}
