// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="eMultiResponseCase.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences
{
    /// <summary>
    /// The options available for how results for multi-valued cases (Time history, Nonlinear static or Multi-step static) are considered in design.
    /// </summary>
    public enum eMultiResponseCase
    {
        /// <summary>
        /// Considers enveloping values for Time History and Multi-step static and last step values for Nonlinear static.
        /// </summary>
        Envelopes = 1,

        /// <summary>
        /// Considers step-by-step values for Time History and Multi-step static and last step values for Nonlinear static.
        /// Defaults to the corresponding Envelope if more than one multi-valued case is present in the combo.
        /// </summary>
        [Description("Step-by-Step")]
        StepByStep = 2,

        /// <summary>
        /// Considers last values for Time History, Multi-step static and Nonlinear static.
        /// </summary>
        [Description("Last Step")]
        LastStep = 3,

        /// <summary>
        /// Considers enveloping values for Time History, Multi-step static and Nonlinear static.
        /// </summary>
        [Description("Envelopes - All")]
        EnvelopesAll = 4,

        /// <summary>
        /// Considers step-by-step values for Time History, Multi-step static and Nonlinear static.
        /// Defaults to the corresponding Envelope if more than one multi-valued case is present in the combo.
        /// </summary>
        [Description("Step-by-Step - All")]
        StepByStepAll = 5
    }
}
