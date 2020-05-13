﻿// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-12-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="IObservableModifiers.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;

namespace MPT.CSI.API.Core.Program.ModelBehavior
{
    /// <summary>
    /// Object can return stiffness, weight, and mass modifiers for frame objects.
    /// </summary>
    public interface IObservableFrameModifiers
    {
        /// <summary>
        /// Returns the unitless modifier assignment.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing element or object.</param>
        FrameModifier GetModifiers(string name);
    }
}
