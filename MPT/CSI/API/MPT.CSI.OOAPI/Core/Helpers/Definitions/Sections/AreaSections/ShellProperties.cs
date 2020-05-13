// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="ShellProperties.cs" company="">
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
    /// Class ShellProperties.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.AreaSections.AreaSectionProperties" />
    public class ShellProperties : AreaSectionProperties
    {
        #region Fields & Properties
        /// <summary>
        /// The type of area.
        /// </summary>
        /// <value>The type of the area.</value>
        public override eAreaSectionType AreaType => eAreaSectionType.Shell;


        /// <summary>
        /// Gets or sets the type of shell.
        /// </summary>
        /// <value>The type of the shell.</value>
        public virtual eShellType ShellType { get; set; }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// True: Drilling degrees of freedom are included in the element formulation in the analysis model.
        /// This item does not apply when <see cref="ShellType" /> = <see cref="eShellType.PlateThin" />,  <see cref="eShellType.PlateThick" /> or  <see cref="eShellType.ShellLayered" />.
        /// </summary>
        /// <value><c>true</c> if [include drilling dof]; otherwise, <c>false</c>.</value>
        public bool IncludeDrillingDOF { get; set; }

        /// <summary>
        /// The material angle. [deg]
        /// This item does not apply when <see cref="ShellType" /> = <see cref="eShellType.ShellLayered" />.
        /// </summary>
        /// <value>The material angle.</value>
        public double MaterialAngle { get; set; }

        /// <summary>
        /// The membrane thickness. [L]
        /// This item does not apply when <see cref="ShellType" /> = <see cref="eShellType.ShellLayered" />.
        /// </summary>
        /// <value>The membrane thickness.</value>
        public double MembraneThickness { get; set; }

        /// <summary>
        /// The bending thickness. [L]
        /// This item does not apply when <see cref="ShellType" /> = <see cref="eShellType.ShellLayered" />.
        /// </summary>
        /// <value>The bending thickness.</value>
        public double BendingThickness { get; set; }
#endif
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellProperties" /> class.
        /// </summary>
        public ShellProperties() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ShellProperties" /> class.
        /// </summary>
        /// <param name="material">The material.</param>
        public ShellProperties(Material material) : base(material)
        {

        }

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
