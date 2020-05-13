// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 01-13-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 01-27-2019
// ***********************************************************************
// <copyright file="Cable.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Components.Definitions.Materials;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Objects.Frames;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    /// <summary>
    /// Class Cable.
    /// </summary>
    /// <seealso cref="MPT.CSI.Serialize.Models.Components.StructureLayout.StructureObject2D{MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties.CableProperty}" />
    /// <seealso cref="StructureObject2D{CableProperties}" />
    public class Cable : StructureObject2D<CableProperty>
    {
        #region Fields & Properties
        /// <summary>
        /// The material overwrite
        /// </summary>
        protected Material _materialOverwrite;
        /// <summary>
        /// The material overwrite assigned to the object.
        /// This overwrites the material used in the cross-section.
        /// </summary>
        /// <value>The material overwrite.</value>
        public virtual Material MaterialOverwrite => _materialOverwrite ?? (_materialOverwrite = null);

        /// <summary>
        /// The material used.
        /// </summary>
        /// <value>The material used.</value>
        public Material MaterialUsed => MaterialOverwrite ?? CrossSection.Material;

        /// <summary>
        /// The mass
        /// </summary>
        private double? _mass;
        /// <summary>
        /// Mass per unit length or pass per unit area assignment from objects, depending on whether the object is a line or area. [M/L] or [M/L^2]
        /// </summary>
        /// <value>The mass.</value>
        public virtual double Mass => _mass ?? 0;

        /// <summary>
        /// Gets the cable geometry definition.
        /// </summary>
        /// <value>The cable geometry definition.</value>
        public eCableGeometryDefinition CableGeometryDefinition { get; internal set; }

        /// <summary>
        /// Gets the number of segments.
        /// </summary>
        /// <value>The number of segments.</value>
        public int NumberOfSegments { get; internal set; }

        /// <summary>
        /// Gets the tension i. [F]
        /// </summary>
        /// <value>The tension i.</value>
        public double TensionI { get; internal set; }

        /// <summary>
        /// Gets the tension j. [F]
        /// </summary>
        /// <value>The tension j.</value>
        public double TensionJ { get; internal set; }

        /// <summary>
        /// Gets the horizontal tension. [F]
        /// </summary>
        /// <value>The horizontal tension.</value>
        public double HorizontalTension { get; internal set; }

        /// <summary>
        /// Gets the maximum vertical sag. [L]
        /// </summary>
        /// <value>The maximum vertical sag.</value>
        public double MaximumVerticalSag { get; internal set; }

        /// <summary>
        /// Gets the low point vertical sag. [L]
        /// </summary>
        /// <value>The low point vertical sag.</value>
        public double LowPointVerticalSag { get; internal set; }

        /// <summary>
        /// Gets the length of the undeformed cable. [L]
        /// </summary>
        /// <value>The length of the undeformed.</value>
        public double UndeformedLength { get; internal set; }

        /// <summary>
        /// Gets the relative length of the undeformed cable.
        /// </summary>
        /// <value>The length of the undeformed relative.</value>
        public double UndeformedRelativeLength { get; internal set; }

        /// <summary>
        /// The added weight per unit length. [F/L]
        /// </summary>
        /// <value>The added weight.</value>
        public double AddedWeight { get; internal set; }

        /// <summary>
        /// Gets the projected uniform gravity load. [F/L]
        /// </summary>
        /// <value>The projected uniform gravity load.</value>
        public double ProjectedUniformGravityLoad { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether [model cable using straight frame objects].
        /// </summary>
        /// <value><c>true</c> if [model cable using straight frame objects]; otherwise, <c>false</c>.</value>
        public bool ModelCableUsingStraightFrameObjects { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether [use deformed geometry].
        /// </summary>
        /// <value><c>true</c> if [use deformed geometry]; otherwise, <c>false</c>.</value>
        public bool UseDeformedGeometry { get; internal set; }

        /// <summary>
        /// Gets or sets the output stations.
        /// </summary>
        /// <value>The output stations.</value>
        public virtual FrameOutputStation OutputStations { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a frame object of the specified name.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>Frame.</returns>
        internal static Cable Factory(
            StructureComponentsProperties<CableProperty> componentsProperties, 
            string uniqueName)
        {
            Cable item = new Cable(
                componentsProperties,
                uniqueName);
            return item;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Cable" /> class.
        /// </summary>
        /// <param name="componentsProperties">The components properties.</param>
        /// <param name="name">The name.</param>
        internal Cable(
            StructureComponentsProperties<CableProperty> componentsProperties,
            string name) : base(componentsProperties,
            name)
        { }
        #endregion

        #region Cross-Section & Material Properties
        ///// <summary>
        ///// Assigns mass per unit length to objects.
        ///// </summary>
        ///// <param name="value">The mass per unit length assigned to the object. [M/L]</param>
        ///// <param name="replace">True: All existing mass assignments to the object are removed before assigning the specified mass.
        ///// False: The specified mass is added to any existing mass already assigned to the object.</param>
        //public override void SetMass(double value, bool replace)
        //{
        //    setMass(value, replace);
        //}

        ///// <summary>
        ///// Deletes all mass assignments for the specified objects.
        ///// </summary>
        //public override void DeleteMass()
        //{
        //    deleteMass();
        //}



        ///// <summary>
        ///// Returns the material overwrite assigned, if any.
        ///// These overwrite the material assigned to the cross section used in the object.
        ///// The material property name is indicated as None if there is no material overwrite assignment.
        ///// </summary>
        ///// <returns>System.String.</returns>
        //public override string GetMaterialOverwriteName()
        //{
        //    return getMaterialOverwriteName();
        //}

        /// <summary>
        /// Adds the material overwrite assignment for objects.
        /// These overwrite the material assigned to the cross section used in the object.
        /// </summary>
        /// <param name="material">An existing material property.</param>
        public void AddMaterialOverwrite(Material material)
        {
            _materialOverwrite = material;
        }

        ///// <summary>
        ///// Removes the material overwrite assignment for objects.
        ///// These overwrite the material assigned to the cross section used in the object.
        ///// </summary>
        //public override void RemoveMaterialOverwrite()
        //{
        //    setMaterialOverwrite(null);
        //}


        ///// <summary>
        ///// Returns the section property name assigned.
        ///// This item is None if there is no section property assigned to the element/object.
        ///// </summary>
        ///// <returns>System.String.</returns>
        //public override string GetSectionName()
        //{
        //    return getSectionName();
        //}

        ///// <summary>
        ///// Assigns the section property to a frame object.
        ///// </summary>
        ///// <param name="section">The section.</param>
        //public void SetSection(FrameSection section)
        //{
        //    if (section == null) return;
        //    setSection(section);
        //}
        #endregion
    }
}
