// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-16-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="ShellLayerProperties.cs" company="">
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
    /// Class ShellLayerProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.ApiProperty" />
    public class ShellLayerProperties : ApiProperty
    {
        #region Fields & Properties

        /// <summary>
        /// Gets or sets the name of the layer.
        /// </summary>
        /// <value>The name of the layer.</value>
        public string LayerName { get; set; }


        /// <summary>
        /// Distance from the area reference surface (area object joint location plus offsets) to the mid-height of the layer. [L]
        /// </summary>
        /// <value>The distance offset.</value>
        public double DistanceOffset { get; set; }


        /// <summary>
        /// Gets or sets the thickness of the layer.
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness { get; set; }


        /// <summary>
        /// Gets or sets the type of shell layer.
        /// </summary>
        /// <value>The type of the layer.</value>
        public eShellLayerType LayerType { get; set; }


        /// <summary>
        /// The number of integration points in the thickness direction for the layer.
        /// The locations are determined by the program using standard Guass-quadrature rules.
        /// </summary>
        /// <value>The number of integration points.</value>
        public int NumberOfIntegrationPoints { get; set; }

        /// <summary>
        /// Gets or sets the material property for the layer.
        /// </summary>
        /// <value>The material property.</value>
        public string MaterialName { get; internal set; }


        /// <summary>
        /// The material angle for the layer. [deg]
        /// </summary>
        /// <value>The material angle.</value>
        public double MaterialAngle { get; set; }


        /// <summary>
        /// The material component behavior in the S11 direction.
        /// </summary>
        /// <value>The type of the S11.</value>
        public eMaterialComponentBehaviorType S11Type { get; set; }


        /// <summary>
        /// The material component behavior in the S22 direction.
        /// </summary>
        /// <value>The type of the S22.</value>
        public eMaterialComponentBehaviorType S22Type { get; set; }


        /// <summary>
        /// The material component behavior in the S12 direction.
        /// </summary>
        /// <value>The type of the S12.</value>
        public eMaterialComponentBehaviorType S12Type { get; set; }
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="ShellLayerProperties" /> class.
        /// </summary>
        public ShellLayerProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellLayerProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public ShellLayerProperties(Material material)
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
        /// <returns>ShellLayerProperties.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
