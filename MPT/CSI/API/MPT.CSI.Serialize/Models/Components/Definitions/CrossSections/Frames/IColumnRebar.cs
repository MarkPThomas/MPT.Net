﻿// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-27-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-27-2019
// ***********************************************************************
// <copyright file="IColumnRebar.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    /// <summary>
    /// Interface IColumnRebar
    /// </summary>
    public interface IColumnRebar
    {
        /// <summary>
        /// The column rebar for the section.
        /// </summary>
        /// <value>The column rebar.</value>
        ColumnRebar ColumnRebar { get; }

        /// <summary>
        /// Sets the column rebar.
        /// </summary>
        /// <param name="columnRebar">The column rebar.</param>
        void SetColumnRebar(ColumnRebarDetailing columnRebar);
    }
}
