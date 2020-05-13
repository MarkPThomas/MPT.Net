// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-15-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-16-2018
// ***********************************************************************
// <copyright file="Deck.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.AreaSections;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas
{
    /// <summary>
    /// Class Deck.
    /// </summary>
    /// <seealso cref="Shell{T}" />
    public class Deck : Shell<DeckProperties>
    {
        #region Fields & Properties
        /// <summary>
        /// The extended properties.
        /// </summary>
        /// <value>The extended.</value>
        private DeckExtended _extended;
        /// <summary>
        /// Gets the extended properties.
        /// </summary>
        /// <value>The extended.</value>
        public DeckExtended Extended
        {
            get
            {
                if (_extended == null)
                {
                    FillExtended();
                }

                return _extended;
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="material">The material.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns>AngleSection.</returns>
        internal static Deck Factory(
            Materials.Materials material,
            string uniqueName, 
            DeckProperties properties = null)
        {
            Deck areaSection = new Deck(material, uniqueName) { _sectionProperties = properties };

            return areaSection;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Deck" /> class.
        /// </summary>
        /// <param name="materials">The materials.</param>
        /// <param name="name">The name.</param>
        protected Deck(
            Materials.Materials materials, 
            string name) : base(materials, name)
        {
        }
        #endregion

        #region Fill/Set
        public override void Set(DeckProperties properties)
        {
            setProperty(properties);
        }

        /// <summary>
        /// Fills the extended.
        /// </summary>
        public void FillExtended()
        {
            switch (SectionProperties.FloorType)
            {
                case eDeckType.SolidSlab:
                    _extended = DeckSolidSlab.Factory(Name);
                    break;
                case eDeckType.Unfilled:
                    _extended = DeckUnfilled.Factory(Name);
                    break;
                case eDeckType.Filled:
                    _extended = DeckFilled.Factory(Name);
                    break;
            }
        }

        /// <summary>
        /// Sets the extended.
        /// </summary>
        /// <param name="extendedProperties">The extended properties.</param>
        public void SetExtended(DeckExtendedProperties extendedProperties)
        {
            if (extendedProperties == null) return;
            switch (extendedProperties)
            {
                case DeckSolidSlabProperties deckSolidSlabProperties:
                    _extended = DeckSolidSlab.Factory(Name, deckSolidSlabProperties);
                    break;
                case DeckUnfilledProperties deckUnfilledProperties:
                    _extended = DeckUnfilled.Factory(Name, deckUnfilledProperties);
                    break;
                case DeckFilledProperties deckFilledProperties:
                    _extended = DeckFilled.Factory(Name, deckFilledProperties);
                    break;
            }
        }
        #endregion
    }
}
