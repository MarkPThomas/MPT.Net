// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-15-2018
// ***********************************************************************
// <copyright file="Slab.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Slab.
    /// </summary>
    /// <seealso cref="Shell{T}" />
    public class Slab : Shell<SlabProperties>
    {
        #region Fields & Properties
        /// <summary>
        /// The extended properties.
        /// </summary>
        /// <value>The extended.</value>
        private SlabExtended _extended;
        /// <summary>
        /// Gets the extended properties.
        /// </summary>
        /// <value>The extended.</value>
        public SlabExtended Extended {
            get
            {
                if (_extended == null)
                {
                    FillExtended();
                }

                return _extended;
            }
        }

        /// <summary>
        /// Gets the shell layers.
        /// </summary>
        /// <value>The layers.</value>
        public ShellLayered Layers => getShellLayered();
        #endregion

        #region Initialization
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static Slab Factory(
            Materials.Materials material,
            string uniqueName, 
            SlabProperties properties = null)
        {
            Slab areaSection = new Slab(material, uniqueName) { _sectionProperties = properties };

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Slab" /> class.
        /// </summary>
        /// <param name="materials">The materials.</param>
        /// <param name="name">The name.</param>
        protected Slab(
            Materials.Materials materials,
            string name) : base(materials, name)
        {
            
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Sets the specified properties.
        /// </summary>
        /// <param name="properties">The properties.</param>
        public override void Set(SlabProperties properties)
        {
            setProperty(properties);
        }
        
        /// <summary>
        /// Fills the extended.
        /// </summary>
        public void FillExtended()
        {
            if (SectionProperties.ShellType == eShellType.ShellLayered)
            {
                _extended = null;
                return;
            }

            switch (SectionProperties.FloorType)
            {
                case eSlabType.Ribbed:
                    _extended = SlabRibbed.Factory(Name);
                    break;
                case eSlabType.Waffle:
                    _extended = SlabWaffle.Factory(Name);
                    break;
                case eSlabType.Slab:
                case eSlabType.Drop:
                    _extended = null;
                    break;
            }
        }

        /// <summary>
        /// Sets the extended.
        /// </summary>
        /// <param name="extendedProperties">The extended properties.</param>
        public void SetExtended(SlabExtendedProperties extendedProperties)
        {
            switch (extendedProperties)
            {
                case SlabRibbedProperties slabRibbedProperties:
                    _extended = SlabRibbed.Factory(Name, slabRibbedProperties);
                    _layerProperties = null;
                    break;
                case SlabWaffleProperties slabWaffleProperties:
                    _extended = SlabWaffle.Factory(Name, slabWaffleProperties);
                    _layerProperties = null;
                    break;
                default:
                    _extended = null;
                    break;
            }
        }
        #endregion
    }
}
