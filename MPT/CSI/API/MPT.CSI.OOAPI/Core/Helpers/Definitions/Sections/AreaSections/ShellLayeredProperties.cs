// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="ShellLayeredProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections
{

    /// <summary>
    /// Class ShellLayeredProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.ApiProperty" />
    public class ShellLayeredProperties : ApiProperty
    {
        #region Fields & Properties

        /// <summary>
        /// The type of shell.
        /// </summary>
        /// <value>The type of the shell.</value>
        public eShellType ShellType => eShellType.ShellLayered;

        /// <summary>
        /// Gets or sets the layers.
        /// </summary>
        /// <value>The layers.</value>
        public List<ShellLayerProperties> Layers { get; set; } = new List<ShellLayerProperties>();
        #endregion

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
