// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="FrameSections.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Sections.FrameSections;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections
{
    /// <summary>
    /// Class FrameSections.
    /// </summary>
    /// <seealso cref="ObjectLists{T}" />
    public class FrameSections : ObjectLists<FrameSection>
    {
        #region Fields & Properties
        /// <summary>
        /// The materials
        /// </summary>
        protected readonly Materials.Materials _materials;

        /// <summary>
        /// Gets or sets the type of the frame section type to be used for generic fill operations.
        /// </summary>
        /// <value>The type of frame section.</value>
        public static eFrameSectionType FrameSectionType { get; set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameSections" /> class.
        /// </summary>
        /// <param name="materials">The materials.</param>
        internal FrameSections(Materials.Materials materials) 
        {
            _materials = materials;
        }
        #endregion
        
        #region Add/Remove
        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddAngle(string uniqueName, AngleSectionProperties properties)
        {
            return add(uniqueName, properties,  AngleSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddChannel(string uniqueName, ChannelSectionProperties properties)
        {
            return add(uniqueName, properties, ChannelSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddCircle(string uniqueName, CircleSectionProperties properties)
        {
            return add(uniqueName, properties, CircleSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddCoverPlatedI(string uniqueName, CoverPlatedISectionProperties properties)
        {
            return add(uniqueName, properties, CoverPlatedISection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddDoubleAngle(string uniqueName, DoubleAngleSectionProperties properties)
        {
            return add(uniqueName, properties, DoubleAngleSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddDoubleChannel(string uniqueName, DoubleChannelSectionProperties properties)
        {
            return add(uniqueName, properties, DoubleChannelSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddGeneral(string uniqueName, GeneralSectionProperties properties)
        {
            return add(uniqueName, properties, GeneralSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddI(string uniqueName, ISectionProperties properties)
        {
            return add(uniqueName, properties, ISection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddPipe(string uniqueName, PipeSectionProperties properties)
        {
            return add(uniqueName, properties, PipeSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddRectangle(string uniqueName, RectangleSectionProperties properties)
        {
            return add(uniqueName, properties, RectangleSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddTee(string uniqueName, TeeSectionProperties properties)
        {
            return add(uniqueName, properties, TeeSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddTube(string uniqueName, TubeSectionProperties properties)
        {
            return add(uniqueName, properties, TubeSection.Factory);
        }
        
        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        public bool AddColdC(string uniqueName, ColdCSectionProperties properties)
        {
            return add(uniqueName, properties, ColdCSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        public bool AddColdHat(string uniqueName, ColdHatSectionProperties properties)
        {
            return add(uniqueName, properties, ColdHatSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        public bool AddColdZ(string uniqueName, ColdZSectionProperties properties)
        {
            return add(uniqueName, properties, ColdZSection.Factory);
        }
        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        public bool AddHybridI(string uniqueName, HybridISectionProperties properties)
        {
            return add(uniqueName, properties, HybridISection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        public bool AddHybridU(string uniqueName, HybridUSectionProperties properties)
        {
            return add(uniqueName, properties, HybridUSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        public bool AddPrecastI(string uniqueName, PrecastISectionProperties properties)
        {
            return add(uniqueName, properties, PrecastISection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        public bool AddPrecastU(string uniqueName, PrecastUSectionProperties properties)
        {
            return add(uniqueName, properties, PrecastUSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddConcreteL(string uniqueName, ConcreteLSectionProperties properties)
        {
            return add(uniqueName, properties, ConcreteLSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddConcreteTee(string uniqueName, ConcreteTeeSectionProperties properties)
        {
            return add(uniqueName, properties, ConcreteTeeSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddSteelAngle(string uniqueName, SteelAngleSectionProperties properties)
        {
            return add(uniqueName, properties, SteelAngleSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddSteelTee(string uniqueName, SteelTeeSectionProperties properties)
        {
            return add(uniqueName, properties, SteelTeeSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddPlate(string uniqueName, RectangleSectionProperties properties)
        {
            return add(uniqueName, properties, PlateSection.Factory);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddRod(string uniqueName, CircleSectionProperties properties)
        {
            return add(uniqueName, properties, RodSection.Factory);
        }

        /// <summary>
        /// Adds a new frame section to the application.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="adderFactory">The adderFactory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add<T>(
            string uniqueName, 
            T properties, 
            Func<Materials.Materials, string, T, FrameSection> adderFactory) where T : SectionProperties
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory(_materials, uniqueName, properties));
            return true;
        }

        /// <summary>
        /// Adds a new frame section to the application.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="adderFactory">The adderFactory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add<T>(
            string uniqueName,
            T properties,
            Func<Materials.Materials, FrameSections, string, T, FrameSection> adderFactory) where T : SectionProperties
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory( _materials, this, uniqueName, properties));
            return true;
        }
        #endregion
        
        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override FrameSection fillNewItem(string uniqueName)
        {
            return FrameSection.Factory(_materials, this, uniqueName, FrameSectionType); 
        }
        #endregion
    }
}
