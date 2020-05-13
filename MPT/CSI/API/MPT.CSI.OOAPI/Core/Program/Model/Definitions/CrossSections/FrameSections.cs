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
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections;
using MPT.CSI.OOAPI.Core.Helpers.Definitions.Sections.FrameSections;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiFrameSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.FrameSection;
using FrameSection = MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames.FrameSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections
{
    /// <summary>
    /// Class FrameSections.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.ObjectLists{FrameSection}" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class FrameSections : ObjectLists<FrameSection>
    {
        #region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The frame section.</value>
        protected ApiFrameSection _apiFrameSection => getApiFrameSection(_apiApp);
        /// <summary>
        /// The materials
        /// </summary>
        protected readonly Materials.Materials _materials;
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="FrameSections" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="materials">The materials.</param>
        internal FrameSections(ApiCSiApplication app, Materials.Materials materials) : base(app)
        {
            _materials = materials;
        }
        #endregion

        #region Fill
        /// <summary>
        /// Gets all defined frame sections of a specified frame type.
        /// </summary>
        /// <param name="frameType">Type of the frame.</param>
        /// <returns>List&lt;LoadPattern&gt;.</returns>
        public List<FrameSection> FillAllItems(eFrameSectionType frameType)
        {
            List<FrameSection> items = new List<FrameSection>();
            List<string> itemNames = GetNameList(frameType);
            foreach (var itemName in itemNames)
            {
                FrameSection item = FillItem(itemName);
                if (item == null) continue;
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override FrameSection fillNewItem(string uniqueName)
        {
            try
            {
                return FrameSection.Factory(_apiApp, _materials, this, uniqueName);
            }
            catch (CSiException e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        #endregion

        #region Add/Remove
        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddAngle(string uniqueName, AngleSectionProperties properties)
        {
            return add(uniqueName, properties,  AngleSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddChannel(string uniqueName, ChannelSectionProperties properties)
        {
            return add(uniqueName, properties, ChannelSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddCircle(string uniqueName, CircleSectionProperties properties)
        {
            return add(uniqueName, properties, CircleSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddCoverPlatedI(string uniqueName, CoverPlatedISectionProperties properties)
        {
            return add(uniqueName, properties, CoverPlatedISection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddDoubleAngle(string uniqueName, DoubleAngleSectionProperties properties)
        {
            return add(uniqueName, properties, DoubleAngleSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddDoubleChannel(string uniqueName, DoubleChannelSectionProperties properties)
        {
            return add(uniqueName, properties, DoubleChannelSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddGeneral(string uniqueName, GeneralSectionProperties properties)
        {
            return add(uniqueName, properties, GeneralSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddI(string uniqueName, ISectionProperties properties)
        {
            return add(uniqueName, properties, ISection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddPipe(string uniqueName, PipeSectionProperties properties)
        {
            return add(uniqueName, properties, PipeSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddRectangle(string uniqueName, RectangleSectionProperties properties)
        {
            return add(uniqueName, properties, RectangleSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddTee(string uniqueName, TeeSectionProperties properties)
        {
            return add(uniqueName, properties, TeeSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddTube(string uniqueName, TubeSectionProperties properties)
        {
            return add(uniqueName, properties, TubeSection.Add);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddColdC(string uniqueName, ColdCSectionProperties properties)
        {
            return add(uniqueName, properties, ColdCSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddColdHat(string uniqueName, ColdHatSectionProperties properties)
        {
            return add(uniqueName, properties, ColdHatSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddColdZ(string uniqueName, ColdZSectionProperties properties)
        {
            return add(uniqueName, properties, ColdZSection.Add);
        }
        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddHybridI(string uniqueName, HybridISectionProperties properties)
        {
            return add(uniqueName, properties, HybridISection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddHybridU(string uniqueName, HybridUSectionProperties properties)
        {
            return add(uniqueName, properties, HybridUSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddPrecastI(string uniqueName, PrecastISectionProperties properties)
        {
            return add(uniqueName, properties, PrecastISection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddPrecastU(string uniqueName, PrecastUSectionProperties properties)
        {
            return add(uniqueName, properties, PrecastUSection.Add);
        }
#endif
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddConcreteL(string uniqueName, ConcreteLSectionProperties properties)
        {
            return add(uniqueName, properties, ConcreteLSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddConcreteTee(string uniqueName, ConcreteTeeSectionProperties properties)
        {
            return add(uniqueName, properties, ConcreteTeeSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddSteelAngle(string uniqueName, SteelAngleSectionProperties properties)
        {
            return add(uniqueName, properties, SteelAngleSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddSteelTee(string uniqueName, SteelTeeSectionProperties properties)
        {
            return add(uniqueName, properties, SteelTeeSection.Add);
        }
#endif
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddPlate(string uniqueName, RectangleSectionProperties properties)
        {
            return add(uniqueName, properties, PlateSection.Add);
        }

        /// <summary>
        /// Adds a frame section to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="properties">The properties.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public bool AddRod(string uniqueName, CircleSectionProperties properties)
        {
            return add(uniqueName, properties, RodSection.Add);
        }
#endif

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
            Func<ApiCSiApplication, Materials.Materials, string, T, FrameSection> adderFactory) where T : SectionProperties
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory(_apiApp, _materials, uniqueName, properties));
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
            Func<ApiCSiApplication, Materials.Materials, FrameSections, string, T, FrameSection> adderFactory) where T : SectionProperties
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory(_apiApp, _materials, this, uniqueName, properties));
            return true;
        }
        #endregion

        #region Query        
        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public override List<string> GetNameList()
        {
            return FrameSection.GetNameList(_apiFrameSection);
        }

        /// <summary>
        /// Returns the names of all defined frame sections of the specified type.
        /// </summary>
        /// <param name="frameType">Type of the frame.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public List<string> GetNameList(eFrameSectionType frameType)
        {
            return FrameSection.GetNameList(_apiFrameSection, frameType);
        }
        #endregion

        #region Methods: Imported Section
        /// <summary>
        /// Returns the names of all defined frame section properties of a specified type in a specified frame section property file.
        /// </summary>
        /// <param name="fileName">The name of the frame section property file from which to get the name list.
        /// In most cases, inputting only the name of the property file (e.g.Sections8.pro) is required, and the program will be able to find it.
        /// In some cases, inputting the full path to the property file may be necessary.</param>
        /// <param name="frameSectionType">Type of frame section to filter the list by.
        /// If no value is input for <paramref name="frameSectionType" />, names are returned for all frame section properties in the specified file regardless of type.</param>
        /// <exception cref="NotImplementedException"></exception>
        /// <exception cref="CSiException"></exception>
        internal void GetPropertyFileNameList(string fileName,
            eFrameSectionType frameSectionType = 0)
        {
            _apiFrameSection.GetPropertyFileNameList(fileName,
                out var sectionNames,
                out var frameSectionTypes,
                frameSectionType);
            throw new NotImplementedException();
            //for (int i = 0; i < sectionNames.Length; i++)
            //{
            //    // TODO: Finish GetPropertyFileNameList & then make public
            //    //// The property names obtained from the frame section property file.
            //    //sectionNames[i];
            //    //// The frame section property type for each property obtained from the frame section property file.
            //    //frameSectionTypes[i];
            //}
        }
        #endregion
    }
}
