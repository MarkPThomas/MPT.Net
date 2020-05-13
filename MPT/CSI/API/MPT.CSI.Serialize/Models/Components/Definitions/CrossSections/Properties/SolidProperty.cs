// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-26-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-26-2019
// ***********************************************************************
// <copyright file="SolidProperties.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Definitions.Materials;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties
{
    /// <summary>
    /// Class SolidProperties.
    /// </summary>
    public class SolidProperty : ObjectProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The materials
        /// </summary>
        protected readonly Materials.Materials _materials;

        /// <summary>
        /// The name of the material.
        /// </summary>
        /// <value>The name of the material.</value>
        internal virtual string MaterialName { get; set; }
        /// <summary>
        /// The material
        /// </summary>
        protected Material _material;
        /// <summary>
        /// Gets or sets the material.
        /// </summary>
        /// <value>The material.</value>
        public virtual Material Material => _material ??
                                            (_material = null);


        /// <summary>
        /// Gets a value indicating whether [incompatible modes].
        /// </summary>
        /// <value><c>true</c> if [incompatible modes]; otherwise, <c>false</c>.</value>
        public bool IncompatibleModes { get; internal set; }
        /// <summary>
        /// Gets the material angle a.
        /// </summary>
        /// <value>The material angle a.</value>
        public double MaterialAngleA { get; internal set; }
        /// <summary>
        /// Gets the material angle b.
        /// </summary>
        /// <value>The material angle b.</value>
        public double MaterialAngleB { get; internal set; }
        /// <summary>
        /// Gets the material angle c.
        /// </summary>
        /// <value>The material angle c.</value>
        public double MaterialAngleC { get; internal set; }
        #endregion

        #region Initialization

        internal static SolidProperty Factory(Materials.Materials materials, string name)
        {
            SolidProperty property = new SolidProperty(name, materials);
            return property;
        }

        protected SolidProperty(string name, Materials.Materials materials) : base(name)
        {
            _materials = materials;
        }
        #endregion
    }
}
