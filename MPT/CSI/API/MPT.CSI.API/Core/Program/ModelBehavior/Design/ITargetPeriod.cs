// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 12-02-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-02-2018
// ***********************************************************************
// <copyright file="ITargetPeriod.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Design
{
    /// <summary>
    /// Implements a target period interface for all frame elements.
    /// </summary>
    public interface ITargetPeriod
    {
        /// <summary>
        /// Retrieves time period targets for design.
        /// </summary>
        /// <param name="modalCase">Name of the modal load case for which the target applies.</param>
        /// <param name="modeNumbers">Mode number associated with each target.</param>
        /// <param name="periodTargets">Target periods. [s]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        void GetTargetPeriod(
            out string modalCase,
            out int[] modeNumbers,
            out double[] periodTargets,
            out bool allSpecifiedTargetsActive);

        /// <summary>
        /// Sets time period targets for design.
        /// </summary>
        /// <param name="modalCase">Name of the modal load case for which the target applies.</param>
        /// <param name="modeNumbers">Mode number associated with each target.</param>
        /// <param name="periodTargets">Target periods. [s]</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        void SetTargetPeriod(
            string modalCase,
            int[] modeNumbers,
            double[] periodTargets,
            bool allSpecifiedTargetsActive);
    }
}
