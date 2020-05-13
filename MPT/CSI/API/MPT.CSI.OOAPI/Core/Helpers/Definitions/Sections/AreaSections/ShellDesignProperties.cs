// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="ShellDesignProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections
{
    /// <summary>
    /// Class ShellDesignProperties.
    /// </summary>
    public class ShellDesignProperties : ApiProperty
    {
        #region Fields & Properties
        /// <summary>
        /// Gets or sets the material property for the layer.
        /// </summary>
        /// <value>The material property.</value>
        public string MaterialName { get; internal set; }

        /// <summary>
        /// Gets or sets the rebar layout option.
        /// </summary>
        /// <value>The rebar layout.</value>
        public eShellSteelLayoutOption RebarLayout { get; set; }

        /// <summary>
        /// The cover to the centroid of the top reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <see cref="RebarLayout" /> = <see cref="eShellSteelLayoutOption.OneLayer" /> or <see cref="eShellSteelLayoutOption.TwoLayers" />.
        /// </summary>
        /// <value>The cover top direction1.</value>
        public double CoverTopDirection1 { get; set; }

        /// <summary>
        /// The cover to the centroid of the top reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <see cref="RebarLayout" /> = <see cref="eShellSteelLayoutOption.OneLayer" /> or <see cref="eShellSteelLayoutOption.TwoLayers" />.
        /// </summary>
        /// <value>The cover top direction2.</value>
        public double CoverTopDirection2 { get; set; }

        /// <summary>
        /// The cover to the centroid of the bottom reinforcing steel running in the local 1 axis directions of the area object. [L]
        /// This item applies only when <see cref="RebarLayout" /> = <see cref="eShellSteelLayoutOption.TwoLayers" />.
        /// </summary>
        /// <value>The cover bottom direction1.</value>
        public double CoverBottomDirection1 { get; set; }

        /// <summary>
        /// The cover to the centroid of the bottom reinforcing steel running in the local 2 axis directions of the area object. [L]
        /// This item applies only when <see cref="RebarLayout" /> = <see cref="eShellSteelLayoutOption.TwoLayers" />.
        /// </summary>
        /// <value>The cover bottom direction2.</value>
        public double CoverBottomDirection2 { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellLayerProperties" /> class.
        /// </summary>
        public ShellDesignProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellLayerProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public ShellDesignProperties(Material material)
        {
            MaterialName = material.Name;
        }

        /// <summary>
        /// Sets the material.
        /// </summary>
        /// <param name="material">The material.</param>
        public void SetMaterial(Material material)
        {
            MaterialName = material.Name;
        }

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>ShellDesignProperties.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
