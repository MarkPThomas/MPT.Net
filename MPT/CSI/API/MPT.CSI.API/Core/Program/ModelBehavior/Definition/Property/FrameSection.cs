// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-11-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-11-2017
// ***********************************************************************
// <copyright file="FrameSection.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
#if BUILD_SAP2000v16
using CSiProgram = SAP2000v16;
#elif BUILD_SAP2000v17
using CSiProgram = SAP2000v17;
#elif BUILD_SAP2000v18
using CSiProgram = SAP2000v18;
#elif BUILD_SAP2000v19
using CSiProgram = SAP2000v19;
#elif BUILD_SAP2000v20
using CSiProgram = SAP2000v20;
#elif BUILD_CSiBridgev16
using CSiProgram = CSiBridge16;
#elif BUILD_CSiBridgev17
using CSiProgram = CSiBridge17;
#elif BUILD_CSiBridgev18
using CSiProgram = CSiBridge18;
#elif BUILD_CSiBridgev19
using CSiProgram = CSiBridge19;
#elif BUILD_CSiBridgev20
using CSiProgram = CSiBridge20;
#elif BUILD_ETABS2013
using CSiProgram = ETABS2013;
#elif BUILD_ETABS2015
using CSiProgram = ETABS2015;
#elif BUILD_ETABS2016
using CSiProgram = ETABS2016;
#elif BUILD_ETABS2017
using CSiProgram = ETABSv17;
#endif
using MPT.Enums;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.Frame;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Represents the frame properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.IFrameSection" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class FrameSection : CSiApiBase, IFrameSection
    {
        #region Fields
        /// <summary>
        /// The seed
        /// </summary>
        private readonly CSiApiSeed _seed;

        /// <summary>
        /// The section designer
        /// </summary>
        private SectionDesigner _sectionDesigner;
        #endregion

        #region Properties                            
        /// <summary>
        /// Gets the section designer.
        /// </summary>
        /// <value>The section designer.</value>
        public SectionDesigner SectionDesigner => _sectionDesigner ?? (_sectionDesigner = new SectionDesigner(_seed));
        #endregion

        #region Initialization        

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameSection" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public FrameSection(CSiApiSeed seed) : base(seed)
        {
            _seed = seed;
        }

        #endregion

        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing frame property.
        /// </summary>
        /// <param name="currentName">The existing name of a defined frame property.</param>
        /// <param name="newName">The new name for the frame property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        /// <inheritdoc />
        public void ChangeName(string currentName, 
            string newName)
        {
            _callCode = _sapModel.PropFrame.ChangeName(currentName, newName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Returns the total number of defined frame properties in the model.
        /// </summary>
        /// <returns>System.Int32.</returns>
        /// <inheritdoc />
        public int Count()
        {
            return _sapModel.PropFrame.Count();
        }

        /// <inheritdoc />
        /// <summary>
        /// The function deletes a specified frame property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <param name="name">The name of an existing frame property.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Delete(string name)
        {
            _callCode = _sapModel.PropFrame.Delete(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string[] GetNameList()
        {
            string[] names = new string[0];
            _callCode = _sapModel.PropFrame.GetNameList(ref _numberOfItems, ref names);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the names of all defined frame properties of the specified type.
        /// </summary>
        /// <param name="frameType">The frame type to filter the name list by.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public string[] GetNameList(eFrameSectionType frameType)
        {
            string[] names = new string[0];
            _callCode = _sapModel.PropFrame.GetNameList(ref _numberOfItems, ref names, 
                            EnumLibrary.Convert<eFrameSectionType, CSiProgram.eFramePropType>(frameType));
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return names;
        }


#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017        
        /// <inheritdoc />
        /// <summary>
        /// Gets all frame properties.
        /// </summary>
        /// <param name="names">Frame property names retrieved by the program.</param>
        /// <param name="frameType">The frame type retrieved by the program.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="t2b">The bottom flange width. [L].</param>
        /// <param name="tfb">The bottom flange thickness. [L].</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetAllFrameProperties(out string[] names,
            out eFrameSectionType[] frameType,
            out double[] t3,
            out double[] t2,
            out double[] tf,
            out double[] tw,
            out double[] t2b,
            out double[] tfb)
        {
            names = new string[0];
            frameType = new eFrameSectionType[0];
            t3 = new double[0];
            t2 = new double[0];
            tf = new double[0];
            tw = new double[0];
            t2b = new double[0];
            tfb = new double[0];
            CSiProgram.eFramePropType[] csiFrameType = new CSiProgram.eFramePropType[0];

            _callCode = _sapModel.PropFrame.GetAllFrameProperties(ref _numberOfItems, ref names, ref csiFrameType,
                ref t3, ref t2, ref tf, ref tw, ref t2b, ref tfb);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            frameType = new eFrameSectionType[_numberOfItems];
            for (int i = 0; i < _numberOfItems; i++)
            {
                frameType[i] = EnumLibrary.Convert(csiFrameType[i], frameType[i]);
            }
        }
#endif
        #endregion

        #region Methods: Section
        /// <inheritdoc />
        /// <summary>
        /// Returns properties for frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="Ag">The gross cross-sectional area. [L^2].</param>
        /// <param name="As2">The shear area for forces in the section local 2-axis direction. [L^2].</param>
        /// <param name="As3">The shear area for forces in the section local 3-axis direction. [L^2].</param>
        /// <param name="J">The torsional constant. [L^4].</param>
        /// <param name="I22">The moment of inertia for bending about the local 2 axis. [L^4].</param>
        /// <param name="I33">The moment of inertia for bending about the local 3 axis. [L^4].</param>
        /// <param name="S22">The section modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="S33">The section modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="Z22">The plastic modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="Z33">The plastic modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="r22">The radius of gyration about the local 2 axis. [L].</param>
        /// <param name="r33">The radius of gyration about the local 3 axis. [L].</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSectionProperties(string name,
            out double Ag,
            out double As2,
            out double As3,
            out double J,
            out double I22,
            out double I33,
            out double S22,
            out double S33,
            out double Z22,
            out double Z33,
            out double r22,
            out double r33)
        {
            Ag = 0;
            As2 = 0;
            As3 = 0;
            J = 0;
            I22 = 0;
            I33 = 0;
            S22 = 0;
            S33 = 0;
            Z22 = 0;
            Z33 = 0;
            r22 = 0;
            r33 = 0;

            _callCode = _sapModel.PropFrame.GetSectProps(name, ref Ag, ref As2, ref As3, ref J, ref I22, ref I33, ref S22, ref S33, ref Z22, ref Z33, ref r22, ref r33);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <inheritdoc />
        /// <summary>
        /// Returns the property type for the specified frame section property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public eFrameSectionType GetType(string name)
        {
            CSiProgram.eFramePropType csiFrameType = CSiProgram.eFramePropType.I;
            eFrameSectionType frameSectionType = 0;

            _callCode = _sapModel.PropFrame.GetTypeOAPI(name, ref csiFrameType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return EnumLibrary.Convert(csiFrameType, frameSectionType);
        }

#if !BUILD_ETABS2015
        /// <summary>
        /// Returns the rebar design type for the specified frame section property.
        /// This function applies only to the following section property types:<para />
        /// <see cref="eFrameSectionType.TSection" /> <para />
        /// <see cref="eFrameSectionType.Angle" /><para />
        /// <see cref="eFrameSectionType.Rectangular" /><para />
        /// <see cref="eFrameSectionType.Circle" /><para />
        /// Calling this function for any other type of frame section property returns an error.
        /// A nonzero rebar type is returned only if the frame section property has a concrete material.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public eRebarType GetRebarType(string name)
        {
            int csiRebarType = 0;

            _callCode = _sapModel.PropFrame.GetTypeRebar(name, ref csiRebarType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eRebarType)csiRebarType;
        }
#endif
        #endregion

        #region Methods: Imported Section

        /// <summary>
        /// This function imports a frame section property from a property file.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.
        /// This name does not need to be the same as the <paramref name="sectionName" /> item.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="fileName">The name of the frame section property file from which to get the frame section property specified by the <paramref name="sectionName" /> item.
        /// In most cases you can input just the name of the property file (e.g.Sections8.pro) and the program will be able to find it.
        /// In some cases you may have to input the full path to the property file.
        /// TODO: Handle this.</param>
        /// <param name="sectionName">The name of the frame section property, inside the property file specified by the <paramref name="fileName" /> item, that is to be imported.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void ImportSectionProperty(string name,
            string nameMaterial,
            string fileName,
            string sectionName,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.ImportProp(name, nameMaterial, fileName, sectionName, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

#if !BUILD_ETABS2015 
        /// <summary>
        /// Returns the names of the section property file from which an imported frame section originated, and it also retrieves the section name used in the property file.
        /// If the specified frame section property was not imported, blank strings are returned for <paramref name="nameInFile" /> and <paramref name="fileName" />.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameInFile">The name of the specified frame section property in the frame section property file.</param>
        /// <param name="fileName">The name of the frame section property file from which the specified frame section property was obtained.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="frameSectionType">Type of frame section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetNameInPropertyFile(string name,
            out string nameInFile,
            out string fileName,
            out string nameMaterial,
            out eFrameSectionType frameSectionType)
        {
            nameInFile = string.Empty;
            fileName = string.Empty;
            nameMaterial = string.Empty;
            frameSectionType = 0;

            CSiProgram.eFramePropType csiFrameType = CSiProgram.eFramePropType.I;

            _callCode = _sapModel.PropFrame.GetNameInPropFile(name, ref nameInFile, ref fileName, ref nameMaterial, ref csiFrameType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            frameSectionType = EnumLibrary.Convert(csiFrameType, frameSectionType);
        }
#endif

        /// <summary>
        /// Returns the names of all defined frame section properties of a specified type in a specified frame section property file.
        /// </summary>
        /// <param name="fileName">The name of the frame section property file from which to get the name list.
        /// In most cases, inputting only the name of the property file (e.g.Sections8.pro) is required, and the program will be able to find it.
        /// In some cases, inputting the full path to the property file may be necessary.</param>
        /// <param name="sectionNames">The property names obtained from the frame section property file.</param>
        /// <param name="frameSectionTypes">The frame section property type for each property obtained from the frame section property file.</param>
        /// <param name="frameSectionType">Type of frame section to filter the list by.
        /// If no value is input for <paramref name="frameSectionType" />, names are returned for all frame section properties in the specified file regardless of type.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetPropertyFileNameList(string fileName,
            out string[] sectionNames,
            out eFrameSectionType[] frameSectionTypes,
            eFrameSectionType frameSectionType = 0)
        {
            sectionNames = new string[0];
            CSiProgram.eFramePropType[] csiFrameTypes = new CSiProgram.eFramePropType[0];
            CSiProgram.eFramePropType csiFrameType = CSiProgram.eFramePropType.I;
            
            _callCode = _sapModel.PropFrame.GetPropFileNameList(fileName, ref _numberOfItems, ref sectionNames, ref csiFrameTypes, csiFrameType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            int[] frameSectionTypesNumberCode = csiFrameTypes.Cast<int>().ToArray();
            frameSectionTypes = frameSectionTypesNumberCode.Cast<eFrameSectionType>().ToArray();
        }
        #endregion

        #region Methods: Modifiers

        /// <summary>
        /// Returns the unitless modifier assignments.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public FrameModifier GetModifiers(string name)
        {
            double[] csiModifiers = new double[0];

            _callCode = _sapModel.PropFrame.GetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            FrameModifier  modifiers = new FrameModifier();
            modifiers.FromArray(csiModifiers);
            return modifiers;
        }

        /// <summary>
        /// This function defines the modifier assignment for frame properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <param name="name">The name of an existing frame property.</param>
        /// <param name="modifiers">Unitless modifiers.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModifiers(string name,
            FrameModifier modifiers)
        {
            if (modifiers == null) { return; }
            double[] csiModifiers = modifiers.ToArray();

            _callCode = _sapModel.PropFrame.SetModifiers(name, ref csiModifiers);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Methods: Get/Set Sections - Other

        /// <summary>
        /// Returns frame section property data for a general frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="Ag">The gross cross-sectional area. [L^2].</param>
        /// <param name="As2">The shear area for forces in the section local 2-axis direction. [L^2].</param>
        /// <param name="As3">The shear area for forces in the section local 3-axis direction. [L^2].</param>
        /// <param name="J">The torsional constant. [L^4].</param>
        /// <param name="I22">The moment of inertia for bending about the local 2 axis. [L^4].</param>
        /// <param name="I33">The moment of inertia for bending about the local 3 axis. [L^4].</param>
        /// <param name="S22">The section modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="S33">The section modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="Z22">The plastic modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="Z33">The plastic modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="r22">The radius of gyration about the local 2 axis. [L].</param>
        /// <param name="r33">The radius of gyration about the local 3 axis. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetGeneral(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double Ag,
            out double As2,
            out double As3,
            out double J,
            out double I22,
            out double I33,
            out double S22,
            out double S33,
            out double Z22,
            out double Z33,
            out double r22,
            out double r33,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            Ag = -1;
            As2 = -1;
            As3 = -1;
            J = -1;
            I22 = -1;
            I33 = -1;
            S22 = -1;
            S33 = -1;
            Z22 = -1;
            Z33 = -1;
            r22 = -1;
            r33 = -1;

            _callCode = _sapModel.PropFrame.GetGeneral(name, ref fileName, ref nameMaterial, ref t3, ref t2,
                ref Ag, ref As2, ref As3, ref J, ref I22, ref I33, ref S22, ref S33, ref Z22, ref Z33, ref r22, ref r33,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function initializes a general frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="Ag">The gross cross-sectional area. [L^2].</param>
        /// <param name="As2">The shear area for forces in the section local 2-axis direction. [L^2].</param>
        /// <param name="As3">The shear area for forces in the section local 3-axis direction. [L^2].</param>
        /// <param name="J">The torsional constant. [L^4].</param>
        /// <param name="I22">The moment of inertia for bending about the local 2 axis. [L^4].</param>
        /// <param name="I33">The moment of inertia for bending about the local 3 axis. [L^4].</param>
        /// <param name="S22">The section modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="S33">The section modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="Z22">The plastic modulus for bending about the local 2 axis. [L^3].</param>
        /// <param name="Z33">The plastic modulus for bending about the local 3 axis. [L^3].</param>
        /// <param name="r22">The radius of gyration about the local 2 axis. [L].</param>
        /// <param name="r33">The radius of gyration about the local 3 axis. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetGeneral(string name,
            string nameMaterial,
            double t3,
            double t2,
            double Ag,
            double As2,
            double As3,
            double J,
            double I22,
            double I33,
            double S22,
            double S33,
            double Z22,
            double Z33,
            double r22,
            double r33,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetGeneral(name, nameMaterial, t3, t2,
                Ag, As2, As3, J, I22, I33, S22, S33, Z22, Z33, r22, r33,
                color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Gets the non prismatic.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.</param>
        /// <param name="startSections">The names of the frame section properties at the start of each segment.</param>
        /// <param name="endSections">The names of the frame section properties at the start of each segment.</param>
        /// <param name="lengths">The length of each segment.
        /// The length may be variable or absolute as indicated by the <paramref name="prismaticTypes" /> item. [L] when length is absolute.</param>
        /// <param name="prismaticTypes">The prismatic length type of each segment.</param>
        /// <param name="EI33">The variation type for EI33 in each segment.</param>
        /// <param name="EI22">The variation type for EI22 in each segment.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetNonPrismatic(string name,
            out string[] startSections,
            out string[] endSections,
            out double[] lengths,
            out ePrismaticType[] prismaticTypes,
            out ePrismaticInertiaType[] EI33,
            out ePrismaticInertiaType[] EI22,
            out int color,
            out string notes,
            out string GUID)
        {
            startSections = new string[0];
            endSections = new string[0];
            lengths = new double[0];
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;

            int[] csiPrismaticTypes = new int[0];
            int[] csiEI33 = new int[0];
            int[] csiEI22 = new int[0];

            _callCode = _sapModel.PropFrame.GetNonPrismatic(name, 
                ref _numberOfItems, 
                ref startSections, 
                ref endSections, 
                ref lengths, 
                ref csiPrismaticTypes, 
                ref csiEI33, 
                ref csiEI22, 
                ref color, 
                ref notes, 
                ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            prismaticTypes = csiPrismaticTypes.Cast<ePrismaticType>().ToArray();
            EI33 = csiEI33.Cast<ePrismaticInertiaType>().ToArray();
            EI22 = csiEI22.Cast<ePrismaticInertiaType>().ToArray();
        }


        /// <summary>
        /// Assigns data to a nonprismatic frame section property.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="startSections">The names of the frame section properties at the start of each segment.
        /// Auto select lists and nonprismatic sections are not allowed.</param>
        /// <param name="endSections">The names of the frame section properties at the start of each segment.
        /// Auto select lists and nonprismatic sections are not allowed.</param>
        /// <param name="lengths">The length of each segment.
        /// The length may be variable or absolute as indicated by the <paramref name="prismaticTypes" /> item. [L] when length is absolute.</param>
        /// <param name="prismaticTypes">The prismatic length type of each segment.</param>
        /// <param name="EI33">The variation type for EI33 in each segment.</param>
        /// <param name="EI22">The variation type for EI22 in each segment.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetNonPrismatic(string name,
            string[] startSections,
            string[] endSections,
            double[] lengths,
            ePrismaticType[] prismaticTypes,
            ePrismaticInertiaType[] EI33,
            ePrismaticInertiaType[] EI22,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            arraysLengthMatch(nameof(startSections), startSections.Length, nameof(endSections), endSections.Length);
            arraysLengthMatch(nameof(startSections), startSections.Length, nameof(lengths), lengths.Length);
            arraysLengthMatch(nameof(startSections), startSections.Length, nameof(prismaticTypes), prismaticTypes.Length);
            arraysLengthMatch(nameof(startSections), startSections.Length, nameof(EI33), EI33.Length);
            arraysLengthMatch(nameof(startSections), startSections.Length, nameof(EI22), EI22.Length);

            int[] csiPrismaticTypes = prismaticTypes.Cast<int>().ToArray();
            int[] csiEI33 = EI33.Cast<int>().ToArray();
            int[] csiEI22 = EI22.Cast<int>().ToArray();

            _callCode = _sapModel.PropFrame.SetNonPrismatic(name, 
                lengths.Length, 
                ref startSections, 
                ref endSections, ref lengths, ref csiPrismaticTypes, ref csiEI33, ref csiEI22, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




#if !BUILD_ETABS2015
        /// <summary>
        /// Gets the section designer section.
        /// </summary>
        /// <param name="name">The name of an existing section designer property.</param>
        /// <param name="nameMaterial">The name of the base material property for the section.</param>
        /// <param name="shapeNames">The name of each shape in the section designer section.</param>
        /// <param name="sectionTypes">The type of each shape in the section designer section.</param>
        /// <param name="designType">The design option for the section.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSectionDesignerSection(string name,
            out string nameMaterial,
            out string[] shapeNames,
            out eSectionDesignerSectionType[] sectionTypes,
            out eSectionDesignerDesignOption designType,
            out int color,
            out string notes,
            out string GUID)
        {
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            shapeNames = new string[0];

            int[] csiSectionTypes = new int[0];
            int csiDesignType = 0;

            _callCode = _sapModel.PropFrame.GetSDSection(name, ref nameMaterial, ref _numberOfItems, ref shapeNames,
                ref csiSectionTypes, ref csiDesignType, ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode))
            {
                throw new CSiException(API_DEFAULT_ERROR_CODE);
            }


            sectionTypes = csiSectionTypes.Cast<eSectionDesignerSectionType>().ToArray();
            designType = (eSectionDesignerDesignOption) csiDesignType;
        }

        /// <summary>
        /// Sets the section designer section.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the base material property for the section.</param>
        /// <param name="designType">The design option for the section.
        /// When <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.GeneralSteel" /> is assigned, the material property specified by the <paramref name="nameMaterial" /> item must be a steel material;
        /// otherwise the program sets <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.NoDesign" />.
        /// Similarly, when <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.ConcreteColumnCheck" /> or <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.ConcreteColumnDesign" /> is assigned, the material property specified by the <paramref name="nameMaterial" /> item must be a concrete material;
        /// otherwise the program sets <paramref name="designType" /> = <see cref="eSectionDesignerDesignOption.NoDesign" />.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSectionDesignerSection(string name,
            string nameMaterial,
            eSectionDesignerDesignOption designType,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetSDSection(name, nameMaterial, (int)designType, color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Methods: Get/Set Sections - Steel: Auto-Select
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns frame section property data for an aluminum auto select list.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames" /> array.
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetAutoSelectAluminum(string name,
            out string[] sectionNames,
            out string autoStartSection,
            out string notes,
            out string GUID)
        {
            notes = string.Empty;
            GUID = string.Empty;
            autoStartSection = string.Empty;
            sectionNames = new string[0];

            _callCode = _sapModel.PropFrame.GetAutoSelectAluminum(name, ref _numberOfItems, ref sectionNames, ref autoStartSection, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Assigns frame section properties to an aluminum auto select list.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.
        /// Auto select lists and nonprismatic (variable) sections are not allowed in this array.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames" /> array.
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAutoSelectAluminum(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetAutoSelectAluminum(name, sectionNames.Length, ref sectionNames, autoStartSection, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// Returns frame section property data for cold-formed steel auto select list.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames" /> array.
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetAutoSelectColdFormed(string name,
            out string[] sectionNames,
            out string autoStartSection,
            out string notes,
            out string GUID)
        {
            notes = string.Empty;
            GUID = string.Empty;
            autoStartSection = string.Empty;
            sectionNames = new string[0];

            _callCode = _sapModel.PropFrame.GetAutoSelectColdFormed(name, ref _numberOfItems, ref sectionNames, ref autoStartSection, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Assigns frame section properties to a cold-formed steel auto select list.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.
        /// Auto select lists and nonprismatic (variable) sections are not allowed in this array.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames" /> array.
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAutoSelectColdFormed(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetAutoSelectColdFormed(name, sectionNames.Length, ref sectionNames, autoStartSection, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif



        /// <summary>
        /// Returns frame section property data for a steel auto select list.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames" /> array.
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetAutoSelectSteel(string name,
            out string[] sectionNames,
            out string autoStartSection,
            out string notes,
            out string GUID)
        {
            notes = string.Empty;
            GUID = string.Empty;
            autoStartSection = string.Empty;
            sectionNames = new string[0];

            _callCode = _sapModel.PropFrame.GetAutoSelectSteel(name, ref _numberOfItems, ref sectionNames, ref autoStartSection, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Assigns frame section properties to a steel auto select list.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.
        /// Auto select lists and nonprismatic (variable) sections are not allowed in this array.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramref name="sectionNames" /> array.
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAutoSelectSteel(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetAutoSelectSteel(name, sectionNames.Length, ref sectionNames, autoStartSection, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Methods: Get/Set Sections - Steel

#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Returns frame section property data for a steel tee-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="r">The fillet radius. [L]</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSteelTee(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out double r,
            out bool mirrorAbout3,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            tw = -1;
            r = -1;
            mirrorAbout3 = false;

            _callCode = _sapModel.PropFrame.GetSteelTee(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref tw, ref r, ref mirrorAbout3,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function initializes a steel tee-type frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="r">The fillet radius. [L]</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSteelTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double r,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetSteelTee(name, nameMaterial,
                 t3, t2, tf, tw, r, mirrorAbout3,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns frame section property data for a steel angle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="r">The fillet radius. [L]</param>
        /// <param name="mirrorAbout2">True: The section is mirrored about the local 2-axis.</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSteelAngle(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out double r,
            out bool mirrorAbout2,
            out bool mirrorAbout3,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            tw = -1;
            r = -1;
            mirrorAbout3 = false;
            mirrorAbout2 = false;

            _callCode = _sapModel.PropFrame.GetSteelAngle(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref tw, ref r, ref mirrorAbout2, ref mirrorAbout3,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a steel angle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="r">The fillet radius. [L]</param>
        /// <param name="mirrorAbout2">True: The section is mirrored about the local 2-axis.</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSteelAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double r,
            bool mirrorAbout2,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetSteelAngle(name, nameMaterial,
                 t3, t2, tf, tw, r, mirrorAbout2, mirrorAbout3,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif

        /// <summary>
        /// Returns frame section property data for a channel-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetChannel(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            tw = -1;

            _callCode = _sapModel.PropFrame.GetChannel(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref tw,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a channel-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetChannel(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetChannel(name, nameMaterial,
                 t3, t2, tf, tw,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns frame section property data for a double angle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="separation">The back-to-back distance between the angles. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetDoubleAngle(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out double separation,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            tw = -1;
            separation = -1;

            _callCode = _sapModel.PropFrame.GetDblAngle(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref tw, ref separation,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a double angle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="separation">The back-to-back distance between the angles. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDoubleAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double separation,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetDblAngle(name, nameMaterial,
                 t3, t2, tf, tw, separation,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// Returns frame section property data for a double channel-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="separation">The back-to-back distance between the channels. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetDoubleChannel(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out double separation,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            tw = -1;
            separation = -1;

            _callCode = _sapModel.PropFrame.GetDblChannel(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref tw, ref separation,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a double channel-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="separation">The back-to-back distance between the channels. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDoubleChannel(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double separation,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetDblChannel(name, nameMaterial,
                 t3, t2, tf, tw, separation,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns frame section property data for an I-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="t2Bottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetISection(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out double t2Bottom,
            out double tfBottom,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            tw = -1;
            t2Bottom = -1;
            tfBottom = -1;

            _callCode = _sapModel.PropFrame.GetISection(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref tw, ref t2Bottom, ref tfBottom,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for an I-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="t2Bottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetISection(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double t2Bottom,
            double tfBottom,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetISection(name, nameMaterial,
                 t3, t2, tf, tw, t2Bottom, tfBottom,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Methods: Get/Set Sections - Steel: Built-Up
        /// <summary>
        /// Returns frame section property data for a cover plated I-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="sectionName">The name of an existing I-type frame section property that is used for the I-section portion of the coverplated I section.</param>
        /// <param name="fyTopFlange">The yield strength of the top flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName" /> item is used.</param>
        /// <param name="fyWeb">The yield strength of the web of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName" /> item is used.</param>
        /// <param name="fyBottomFlange">The yield strength of the bottom flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName" /> item is used.</param>
        /// <param name="tc">The thickness of the top cover plate. [L]
        /// If the <paramref name="tc" /> or the <paramref name="bc" /> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bc">The width of the top cover plate. [L]
        /// If the <paramref name="tc" /> or the <paramref name="bc" /> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialTop">The name of the material property for the top cover plate.
        /// This item applies only if both the <paramref name="tc" /> and the <paramref name="bc" /> items are greater than 0.</param>
        /// <param name="tcBottom">The thickness of the bottom cover plate. [L]
        /// If the <paramref name="tcBottom" /> or the <paramref name="bcBottom" /> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bcBottom">The width of the bottom cover plate. [L]
        /// If the <paramref name="tcBottom" /> or the <paramref name="bcBottom" /> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialBottom">The name of the material property for the bottom cover plate.
        /// This item applies only if both the <paramref name="tcBottom" /> and the <paramref name="bcBottom" /> items are greater than 0.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetCoverPlatedI(string name,
            out string sectionName,
            out double fyTopFlange,
            out double fyWeb,
            out double fyBottomFlange,
            out double tc,
            out double bc,
            out string nameMaterialTop,
            out double tcBottom,
            out double bcBottom,
            out string nameMaterialBottom,
            out int color,
            out string notes,
            out string GUID)
        {
            sectionName = string.Empty;
            nameMaterialTop = string.Empty;
            nameMaterialBottom = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            fyTopFlange = -1;
            fyWeb = -1;
            fyBottomFlange = -1;
            tc = -1;
            bc = -1;
            tcBottom = -1;
            bcBottom = -1;

            _callCode = _sapModel.PropFrame.GetCoverPlatedI(name, ref sectionName, 
                ref fyTopFlange, ref fyWeb, ref fyBottomFlange,
                ref tc, ref bc, ref nameMaterialTop,
                ref tcBottom, ref bcBottom, ref nameMaterialBottom,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a cover plated I-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionName">The name of an existing I-type frame section property that is used for the I-section portion of the coverplated I section.</param>
        /// <param name="fyTopFlange">The yield strength of the top flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName" /> item is used.</param>
        /// <param name="fyWeb">The yield strength of the web of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName" /> item is used.</param>
        /// <param name="fyBottomFlange">The yield strength of the bottom flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramref name="sectionName" /> item is used.</param>
        /// <param name="tc">The thickness of the top cover plate. [L]
        /// If the <paramref name="tc" /> or the <paramref name="bc" /> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bc">The width of the top cover plate. [L]
        /// If the <paramref name="tc" /> or the <paramref name="bc" /> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialTop">The name of the material property for the top cover plate.
        /// This item applies only if both the <paramref name="tc" /> and the <paramref name="bc" /> items are greater than 0.</param>
        /// <param name="tcBottom">The thickness of the bottom cover plate. [L]
        /// If the <paramref name="tcBottom" /> or the <paramref name="bcBottom" /> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bcBottom">The width of the bottom cover plate. [L]
        /// If the <paramref name="tcBottom" /> or the <paramref name="bcBottom" /> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialBottom">The name of the material property for the bottom cover plate.
        /// This item applies only if both the <paramref name="tcBottom" /> and the <paramref name="bcBottom" /> items are greater than 0.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetCoverPlatedI(string name,
            string sectionName,
            double fyTopFlange,
            double fyWeb,
            double fyBottomFlange,
            double tc,
            double bc,
            string nameMaterialTop,
            double tcBottom,
            double bcBottom,
            string nameMaterialBottom,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetCoverPlatedI(name, sectionName,
                fyTopFlange, fyWeb, fyBottomFlange,
                tc, bc, nameMaterialTop,
                tcBottom, bcBottom, nameMaterialBottom,
                color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns frame section property data for a steel hybrid I-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="t2Bottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetHybridISection(string name,
            out string nameMaterialTopFlange,
            out string nameMaterialWeb,
            out string nameMaterialBottomFlange,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out double t2Bottom,
            out double tfBottom,
            out int color,
            out string notes,
            out string GUID)
        {
            _callCode = _sapModel.PropFrame.GetHybridISection(name, ref nameMaterialTopFlange, ref nameMaterialWeb, ref nameMaterialBottomFlange,
                ref t3, ref t2, ref tf, ref tw, ref t2Bottom, ref tfBottom,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a steel hybrid I-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The top flange width. [L].</param>
        /// <param name="tf">The top flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="t2Bottom">The bottom flange width. [L].</param>
        /// <param name="tfBottom">The bottom flange thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetHybridISection(string name,
            string nameMaterialTopFlange,
            string nameMaterialWeb,
            string nameMaterialBottomFlange,
            double t3,
            double t2,
            double tf,
            double tw,
            double t2Bottom,
            double tfBottom,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetHybridISection(name, nameMaterialTopFlange, nameMaterialWeb, nameMaterialBottomFlange,
                 t3, t2, tf, tw, t2Bottom, tfBottom,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// Returns frame section property data for a steel hybrid U-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="D1">Web Depth (vertical, inside to inside of flanges). [L].</param>
        /// <param name="B1">Web Distance at Top (CL to CL). [L].</param>
        /// <param name="B2">Bottom Flange Width. [L].</param>
        /// <param name="B3">Top Flange Width (per each). [L].</param>
        /// <param name="B4">Bottom Flange Lip (Web CL to flange edge, may be zero). [L].</param>
        /// <param name="tw">Web Thickness. [L].</param>
        /// <param name="tf">Top Flange Thickness. [L].</param>
        /// <param name="tfb">Bottom Flange Thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetHybridUSection(string name,
            out string nameMaterialTopFlange,
            out string nameMaterialWeb,
            out string nameMaterialBottomFlange,
            out double D1,
            out double B1,
            out double B2,
            out double B3,
            out double B4,
            out double tw,
            out double tf,
            out double tfb,
            out int color,
            out string notes,
            out string GUID)
        {
            double[] t = new double[0];

            _callCode = _sapModel.PropFrame.GetHybridUSection(name, ref nameMaterialTopFlange, ref nameMaterialWeb, ref nameMaterialBottomFlange,
                ref t, 
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            D1 = t[0];
            B1 = t[1];
            B2 = t[2];
            B3 = t[3];
            B4 = t[4];
            tw = t[5];
            tf = t[6];
            tfb = t[7];
        }

        /// <summary>
        /// This function initializes frame section property data for a steel hybrid U-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterialTopFlange">The name of the material property for the top flange.</param>
        /// <param name="nameMaterialWeb">The name of the material property for the web.</param>
        /// <param name="nameMaterialBottomFlange">The name of the material property for the bottom flange.</param>
        /// <param name="D1">Web Depth (vertical, inside to inside of flanges). [L].</param>
        /// <param name="B1">Web Distance at Top (CL to CL). [L].</param>
        /// <param name="B2">Bottom Flange Width. [L].</param>
        /// <param name="B3">Top Flange Width (per each). [L].</param>
        /// <param name="B4">Bottom Flange Lip (Web CL to flange edge, may be zero). [L].</param>
        /// <param name="tw">Web Thickness. [L].</param>
        /// <param name="tf">Top Flange Thickness. [L].</param>
        /// <param name="tfb">Bottom Flange Thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetHybridUSection(string name,
            string nameMaterialTopFlange,
            string nameMaterialWeb,
            string nameMaterialBottomFlange,
            double D1,
            double B1,
            double B2,
            double B3,
            double B4,
            double tw,
            double tf,
            double tfb,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            double[] t = {D1, B1, B2, B3, B4, tw, tf, tfb};
            _callCode = _sapModel.PropFrame.SetHybridUSection(name, nameMaterialTopFlange, nameMaterialWeb, nameMaterialBottomFlange,
                 ref t,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Methods: Get/Set Sections - Cold-Formed Steel
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns frame section property data for a cold formed C-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetColdC(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double thickness,
            out double radius,
            out double lipDepth,
            out int color,
            out string notes,
            out string GUID)
        {
            _callCode = _sapModel.PropFrame.GetColdC(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref thickness, ref radius, ref lipDepth,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a cold formed C-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetColdC(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetColdC(name, nameMaterial,
                 t3, t2, thickness, radius, lipDepth,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// Returns frame section property data for a cold formed hat-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetColdHat(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double thickness,
            out double radius,
            out double lipDepth,
            out int color,
            out string notes,
            out string GUID)
        {
            _callCode = _sapModel.PropFrame.GetColdHat(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref thickness, ref radius, ref lipDepth,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a cold formed hat-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetColdHat(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetColdHat(name, nameMaterial,
                 t3, t2, thickness, radius, lipDepth,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// Returns frame section property data for a cold formed Z-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="lipAngle">The lip angle measured from horizontal (0 &lt;= <paramref name="lipAngle" />  &lt;= 90). [deg].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetColdZ(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double thickness,
            out double radius,
            out double lipDepth,
            out double lipAngle,
            out int color,
            out string notes,
            out string GUID)
        {
            _callCode = _sapModel.PropFrame.GetColdZ(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref thickness, ref radius, ref lipDepth, ref lipAngle,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a cold formed Z-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="thickness">The section thickness. [L].</param>
        /// <param name="radius">The corner radius, if any. [L].</param>
        /// <param name="lipDepth">The lip depth, if any. [L].</param>
        /// <param name="lipAngle">The lip angle measured from horizontal (0 &lt;= <paramref name="lipAngle" />  &lt;= 90). [deg].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetColdZ(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            double lipAngle,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetColdZ(name, nameMaterial,
                 t3, t2, thickness, radius, lipDepth, lipAngle,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion

        #region Methods: Get/Set Sections - Steel/Concrete
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Returns frame section property data for a plate-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">Plate depth. [L].</param>
        /// <param name="t2">Plate width. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetPlate(string name,
           out string fileName,
           out string nameMaterial,
           out double t3,
           out double t2,
           out int color,
           out string notes,
           out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;

            _callCode = _sapModel.PropFrame.GetPlate(name, ref fileName, ref nameMaterial,
                ref t3, ref t2,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a plate-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">Plate depth. [L].</param>
        /// <param name="t2">Plate width. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPlate(string name,
            string nameMaterial,
            double t3,
            double t2,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetPlate(name, nameMaterial,
                 t3, t2,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Returns frame section property data for a rod-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetRod(string name,
           out string fileName,
           out string nameMaterial,
           out double t3,
           out int color,
           out string notes,
           out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;

            _callCode = _sapModel.PropFrame.GetRod(name, ref fileName, ref nameMaterial,
                ref t3,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a rod-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetRod(string name,
            string nameMaterial,
            double t3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetRod(name, nameMaterial,
                 t3,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        /// <summary>
        /// Returns frame section property data for a circle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetCircle(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;

            _callCode = _sapModel.PropFrame.GetCircle(name, ref fileName, ref nameMaterial,
                ref t3, 
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a circle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetCircle(string name,
            string nameMaterial,
            double t3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetCircle(name, nameMaterial,
                 t3, 
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns frame section property data for a rectangle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetRectangle(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            t3 = 0;
            t2 = 0;
            color = 0;
            notes = string.Empty;
            GUID = string.Empty;
            _callCode = _sapModel.PropFrame.GetRectangle(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, 
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a rectangle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetRectangle(string name,
            string nameMaterial,
            double t3,
            double t2,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetRectangle(name, nameMaterial,
                 t3, t2, 
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// Returns frame section property data for a pipe-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="tw">The wall thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetPipe(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double tw,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            tw = -1;

            _callCode = _sapModel.PropFrame.GetPipe(name, ref fileName, ref nameMaterial,
                ref t3, ref tw,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a pipe-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="tw">The wall thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPipe(string name,
            string nameMaterial,
            double t3,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetPipe(name, nameMaterial,
                 t3, tw,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// Returns frame section property data for a tube-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetTube(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            tw = -1;

            _callCode = _sapModel.PropFrame.GetTube(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref tw,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for a tube-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The section width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetTube(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetTube(name, nameMaterial,
                 t3, t2, tf, tw,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Returns frame section property data for a tee-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetTee(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            tw = -1;

            _callCode = _sapModel.PropFrame.GetTee(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref tw,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function initializes a tee-type frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section depth. [L].</param>
        /// <param name="t2">The flange width. [L].</param>
        /// <param name="tf">The flange thickness. [L].</param>
        /// <param name="tw">The web thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetTee(name, nameMaterial,
                 t3, t2, tf, tw,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns frame section property data for an angle-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetAngle(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            tw = -1;

            _callCode = _sapModel.PropFrame.GetAngle(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref tw,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// This function initializes frame section property data for an angle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="tw">The vertical leg thickness. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetAngle(name, nameMaterial,
                 t3, t2, tf, tw,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Methods: Get/Set Sections - Concrete: Reinforced
#if BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Returns frame section property data for a concrete L-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="twF">The vertical leg thickness at the flange. [L].</param>
        /// <param name="tfT">The vertical leg thickness at the tip. [L].</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetConcreteTee(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double twF,
            out double tfT,
            out bool mirrorAbout3,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            twF = -1;
            tfT = -1;
            mirrorAbout3 = false;

            _callCode = _sapModel.PropFrame.GetConcreteTee(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref twF, ref tfT, ref mirrorAbout3,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function initializes frame section property data for a concrete L-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="twF">The vertical leg thickness at the flange. [L].</param>
        /// <param name="twT">The vertical leg thickness at the tip. [L].</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetConcreteTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double twF,
            double twT,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetConcreteTee(name, nameMaterial,
                 t3, t2, tf, twF, twT, mirrorAbout3,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Returns frame section property data for a concrete L-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="twC">The vertical leg thickness at the corner. [L].</param>
        /// <param name="twT">The vertical leg thickness at the tip. [L].</param>
        /// <param name="mirrorAbout2">True: The section is mirrored about the local 2-axis.</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetConcreteL(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double twC,
            out double twT,
            out bool mirrorAbout2,
            out bool mirrorAbout3,
            out int color,
            out string notes,
            out string GUID)
        {
            fileName = string.Empty;
            nameMaterial = string.Empty;
            notes = string.Empty;
            GUID = string.Empty;
            color = -1;
            t3 = -1;
            t2 = -1;
            tf = -1;
            twC = -1;
            twT = -1;
            mirrorAbout3 = false;
            mirrorAbout2 = false;

            _callCode = _sapModel.PropFrame.GetConcreteL(name, ref fileName, ref nameMaterial,
                ref t3, ref t2, ref tf, ref twC, ref twT, ref mirrorAbout2, ref mirrorAbout3,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// This function initializes frame section property data for a concrete L-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The vertical leg depth. [L].</param>
        /// <param name="t2">The horizontal leg width. [L].</param>
        /// <param name="tf">The horizontal leg thickness. [L].</param>
        /// <param name="twC">The vertical leg thickness at the corner. [L].</param>
        /// <param name="tfT">The vertical leg thickness at the tip. [L].</param>
        /// <param name="mirrorAbout2">True: The section is mirrored about the local 2-axis.</param>
        /// <param name="mirrorAbout3">True: The section is mirrored about the local 3-axis.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetConcreteL(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double twC,
            double tfT,
            bool mirrorAbout2,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            _callCode = _sapModel.PropFrame.SetConcreteL(name, nameMaterial,
                 t3, t2, tf, twC, tfT, mirrorAbout2, mirrorAbout3,
                 color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif


        /// <summary>
        /// Returns beam rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or this function returns an error.
        /// This function applies only to the following section types. Calling this function for any other type of frame section property returns an error:
        /// <see cref="eFrameSectionType.TSection" />;
        /// <see cref="eFrameSectionType.Angle" />;
        /// <see cref="eFrameSectionType.Rectangular" />;
        /// <see cref="eFrameSectionType.Circle" />
        /// TODO: Handle
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="materialNameLongitudinal">The name of the rebar material property for the longitudinal rebar.</param>
        /// <param name="materialNameConfinement">The name of the rebar material property for the confinement rebar.</param>
        /// <param name="coverTop">The distance from the top of the beam to the centroid of the top longitudinal reinforcement. [L].</param>
        /// <param name="coverBottom">The distance from the bottom of the beam to the centroid of the bottom longitudinal reinforcement. [L].</param>
        /// <param name="topLeftArea">The total area of longitudinal reinforcement at the top left end of the beam. [L^2].</param>
        /// <param name="topRightArea">The total area of longitudinal reinforcement at the top right end of the beam. [L^2].</param>
        /// <param name="bottomLeftArea">The total area of longitudinal reinforcement at the bottom left end of the beam. [L^2].</param>
        /// <param name="bottomRightArea">The total area of longitudinal reinforcement at the bottom right end of the beam. [L^2].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetRebarBeam(string name,
            out string materialNameLongitudinal,
            out string materialNameConfinement,
            out double coverTop,
            out double coverBottom,
            out double topLeftArea,
            out double topRightArea,
            out double bottomLeftArea,
            out double bottomRightArea)
        {
            materialNameLongitudinal = string.Empty;
            materialNameConfinement = string.Empty;
            coverTop = -1;
            coverBottom = -1;
            topLeftArea = -1;
            topRightArea = -1;
            bottomLeftArea = -1;
            bottomRightArea = -1;

            _callCode = _sapModel.PropFrame.GetRebarBeam(name, 
                ref materialNameLongitudinal, ref materialNameConfinement,
                ref coverTop, ref coverBottom, ref topLeftArea, ref topRightArea, ref bottomLeftArea, ref bottomRightArea);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Assigns beam rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or this function returns an error.
        /// This function applies only to the following section types. Calling this function for any other type of frame section property returns an error:
        /// <see cref="eFrameSectionType.TSection" />;
        /// <see cref="eFrameSectionType.Angle" />;
        /// <see cref="eFrameSectionType.Rectangular" />;
        /// <see cref="eFrameSectionType.Circle" />
        /// TODO: Handle
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="materialNameLongitudinal">The material name longitudinal.</param>
        /// <param name="materialNameConfinement">The material name confinement.</param>
        /// <param name="coverTop">The cover top.</param>
        /// <param name="coverBottom">The cover bottom.</param>
        /// <param name="topLeftArea">The top left area.</param>
        /// <param name="topRightArea">The top right area.</param>
        /// <param name="bottomLeftArea">The bottom left area.</param>
        /// <param name="bottomRightArea">The bottom right area.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetRebarBeam(string name,
            string materialNameLongitudinal,
            string materialNameConfinement,
            double coverTop,
            double coverBottom,
            double topLeftArea,
            double topRightArea,
            double bottomLeftArea,
            double bottomRightArea)
        {
            _callCode = _sapModel.PropFrame.SetRebarBeam(name,
                materialNameLongitudinal, materialNameConfinement,
                coverTop, coverBottom, topLeftArea, topRightArea, bottomLeftArea, bottomRightArea);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <summary>
        /// Returns column rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or else this function returns an error.
        /// Calling this function for any type of frame section property other than the following returns an error:<para/>
        /// <see cref="eFrameSectionType.Rectangular" /><para/>
        /// <see cref="eFrameSectionType.Circle" />
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="materialNameLongitudinal">The name of the rebar material property for the longitudinal rebar.</param>
        /// <param name="materialNameConfinement">The name of the rebar material property for the confinement rebar.</param>
        /// <param name="rebarConfiguration">The rebar configuration.
        /// For circular frame section properties this item must be <see cref="eRebarConfiguration.Circular" />; otherwise an error is returned.
        /// TODO: Handle This</param>
        /// <param name="confinementType">Type of the confinement.
        /// This item applies only when <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Circular" />.
        /// If <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />, the confinement bar type is assumed to be <see cref="eConfinementType.Ties" />.</param>
        /// <param name="cover">The clear cover for the confinement steel (ties).
        /// In the special case of circular reinforcement in a rectangular column, this is the minimum clear cover. [L].</param>
        /// <param name="numberOfCircularBars">The total number of longitudinal reinforcing bars in the column.
        /// This item applies to a circular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Circular" />.</param>
        /// <param name="numberOfRectangularBars3Axis">The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 3-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.</param>
        /// <param name="numberOfRectangularBars2Axis">The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 2-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.</param>
        /// <param name="rebarSize">The rebar name for the longitudinal rebar in the column.</param>
        /// <param name="tieSize">The rebar name for the confinement rebar in the column.</param>
        /// <param name="tieSpacingLongitudinal">The longitudinal spacing of the confinement bars (ties). [L].</param>
        /// <param name="numberOfConfinementBars2Axis">It is the number of confinement bars (tie legs) running in the local 2-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.</param>
        /// <param name="numberOfConfinementBars3Axis">It is the number of confinement bars (tie legs) running in the local 3-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.</param>
        /// <param name="toBeDesigned">True: The column longitudinal rebar is to be designed; otherwise it is to be checked.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetRebarColumn(string name,
            out string materialNameLongitudinal,
            out string materialNameConfinement,
            out eRebarConfiguration rebarConfiguration,
            out eConfinementType confinementType,
            out double cover,
            out int numberOfCircularBars,
            out int numberOfRectangularBars3Axis,
            out int numberOfRectangularBars2Axis,
            out string rebarSize,
            out string tieSize,
            out double tieSpacingLongitudinal,
            out int numberOfConfinementBars2Axis,
            out int numberOfConfinementBars3Axis,
            out bool toBeDesigned)
        {
            materialNameLongitudinal = string.Empty;
            materialNameConfinement = string.Empty;
            rebarSize = string.Empty;
            tieSize = string.Empty;
            cover = -1;
            numberOfCircularBars = -1;
            numberOfRectangularBars3Axis = -1;
            tieSpacingLongitudinal = -1;
            numberOfConfinementBars2Axis = -1;
            numberOfRectangularBars2Axis = -1;
            numberOfConfinementBars3Axis = -1;
            toBeDesigned = false;

            int csiRebarConfiguration = 0;
            int csiConfinementType = 0;

            _callCode = _sapModel.PropFrame.GetRebarColumn(name,
                            ref materialNameLongitudinal,
                            ref materialNameConfinement,
                            ref csiRebarConfiguration,
                            ref csiConfinementType,
                            ref cover,
                            ref numberOfCircularBars,
                            ref numberOfRectangularBars3Axis,
                            ref numberOfRectangularBars2Axis,
                            ref rebarSize,
                            ref tieSize,
                            ref tieSpacingLongitudinal,
                            ref numberOfConfinementBars2Axis,
                            ref numberOfConfinementBars3Axis,
                            ref toBeDesigned);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            rebarConfiguration = (eRebarConfiguration)csiRebarConfiguration;
            confinementType = (eConfinementType)csiConfinementType;
        }


        /// <summary>
        /// Assigns column rebar data to frame sections.
        /// The material assigned to the specified frame section property must be concrete or else this function returns an error.
        /// Calling this function for any type of frame section property other than the following returns an error:<para/>
        /// <see cref="eFrameSectionType.Rectangular" /><para/>
        /// <see cref="eFrameSectionType.Circle" />
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="materialPropertyLongitudinal">The name of the rebar material property for the longitudinal rebar.</param>
        /// <param name="materialNameConfinement">The name of the rebar material property for the confinement rebar.</param>
        /// <param name="rebarConfiguration">The rebar configuration.
        /// For circular frame section properties this item must be <see cref="eRebarConfiguration.Circular" />; otherwise an error is returned.
        /// TODO: Handle This</param>
        /// <param name="confinementType">Type of the confinement.
        /// This item applies only when <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Circular" />.
        /// If <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />, the confinement bar type is assumed to be <see cref="eConfinementType.Ties" />.</param>
        /// <param name="cover">The clear cover for the confinement steel (ties).
        /// In the special case of circular reinforcement in a rectangular column, this is the minimum clear cover. [L].</param>
        /// <param name="numberOfCircularBars">The total number of longitudinal reinforcing bars in the column.
        /// This item applies to a circular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Circular" />.</param>
        /// <param name="numberOfRectangularBars3Axis">The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 3-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.</param>
        /// <param name="numberOfRectangularBars2Axis">The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 2-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.</param>
        /// <param name="rebarSize">The rebar name for the longitudinal rebar in the column.</param>
        /// <param name="tieSize">The rebar name for the confinement rebar in the column.</param>
        /// <param name="tieSpacingLongitudinal">The longitudinal spacing of the confinement bars (ties). [L].</param>
        /// <param name="numberOfConfinementBars2Axis">It is the number of confinement bars (tie legs) running in the local 2-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.</param>
        /// <param name="numberOfConfinementBars3Axis">It is the number of confinement bars (tie legs) running in the local 3-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />.</param>
        /// <param name="toBeDesigned">True: The column longitudinal rebar is to be designed; otherwise it is to be checked.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetRebarColumn(string name,
            string materialPropertyLongitudinal,
            string materialNameConfinement,
            eRebarConfiguration rebarConfiguration,
            eConfinementType confinementType,
            double cover,
            int numberOfCircularBars,
            int numberOfRectangularBars3Axis,
            int numberOfRectangularBars2Axis,
            string rebarSize,
            string tieSize,
            double tieSpacingLongitudinal,
            int numberOfConfinementBars2Axis,
            int numberOfConfinementBars3Axis,
            bool toBeDesigned)
        {
            _callCode = _sapModel.PropFrame.SetRebarColumn(name,
                            materialPropertyLongitudinal,
                            materialNameConfinement,
                            (int)rebarConfiguration,
                            (int)confinementType,
                            cover,
                            numberOfCircularBars,
                            numberOfRectangularBars3Axis,
                            numberOfRectangularBars2Axis,
                            rebarSize,
                            tieSize,
                            tieSpacingLongitudinal,
                            numberOfConfinementBars2Axis,
                            numberOfConfinementBars3Axis,
                            toBeDesigned);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion

        #region Methods: Get/Set Sections - Concrete: Precast
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017


        /// <summary>
        /// Returns frame section property data for a precast concrete I girder frame section.
        /// </summary>
        /// <param name="name">The name of an existing precast concrete I girder frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="T1">The web thickness dimension T1 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="T2">The web thickness dimension T2 defined on the precast concrete I girder definition form. [L].</param>
        /// <param name="C1">The bottom flange chamfer dimension, denoted as C1 on the precast concrete I girder definition form.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetPrecastI(string name,
            out string fileName,
            out string nameMaterial,
            out double B1,
            out double B2,
            out double B3,
            out double B4,
            out double D1,
            out double D2,
            out double D3,
            out double D4,
            out double D5,
            out double D6,
            out double D7,
            out double T1,
            out double T2,
            out double C1,
            out int color,
            out string notes,
            out string GUID)
        {
            double[] b = new double[4];
            double[] d = new double [7];
            double[] t = new double[2];
            _callCode = _sapModel.PropFrame.GetPrecastI_1(name, ref fileName, ref nameMaterial,
                ref b, ref d, ref t, ref C1,
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            B1 = b[0];
            B2 = b[1];
            B3 = b[2];
            B4 = b[3];

            D1 = d[0];
            D2 = d[1];
            D3 = d[2];
            D4 = d[3];
            D5 = d[4];
            D6 = d[5];
            D7 = d[6];

            T1 = t[0];
            T2 = t[1];
        }


        /// <summary>
        /// This function initializes a precast concrete I girder frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete I girder definition form. B1 &gt; 0, [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete I girder definition form. B2 &gt; 0, [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete I girder definition form. B3 &gt;= 0, [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete I girder definition form. B4 &gt;= 0, [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete I girder definition form. D1 &gt; 0, [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete I girder definition form. D2 &gt; 0, [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete I girder definition form. D3 &gt;= 0, [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete I girder definition form. D4 &gt;= 0, [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete I girder definition form. D5 &gt; 0, [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete I girder definition form. D6 &gt;= 0, [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete I girder definition form. D7 &gt;= 0, [L].</param>
        /// <param name="T1">The web thickness dimension T1 defined on the precast concrete I girder definition form. T1 &gt; 0, [L].</param>
        /// <param name="T2">The web thickness dimension T2 defined on the precast concrete I girder definition form. T2 &gt; 0, [L].</param>
        /// <param name="C1">The bottom flange chamfer dimension, denoted as C1 on the precast concrete I girder definition form.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPrecastI(string name,
            string nameMaterial,
            double B1,
            double B2,
            double B3,
            double B4,
            double D1,
            double D2,
            double D3,
            double D4,
            double D5,
            double D6,
            double D7,
            double T1,
            double T2,
            double C1,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            //TODO: Handle invalid argument data.
            double[] b = { B1, B2, B3, B4 };
            double[] d = { D1, D2, D3, D4, D5, D6, D7 };
            double[] t = { T1, T2 };

            _callCode = _sapModel.PropFrame.SetPrecastI_1(name, nameMaterial,
                ref b, ref d, ref t, ref C1,
                color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns frame section property data for a precast concrete U girder frame section.
        /// </summary>
        /// <param name="name">The name of an existing precast concrete I girder frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file.
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B5">The horizontal section dimension B5 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="B6">The horizontal section dimension B6 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete U girder definition form. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetPrecastU(string name,
            out string fileName,
            out string nameMaterial,
            out double B1,
            out double B2,
            out double B3,
            out double B4,
            out double B5,
            out double B6,
            out double D1,
            out double D2,
            out double D3,
            out double D4,
            out double D5,
            out double D6,
            out double D7,
            out int color,
            out string notes,
            out string GUID)
        {
            double[] b = new double[6];
            double[] d = new double[7];

            _callCode = _sapModel.PropFrame.GetPrecastU(name, ref fileName, ref nameMaterial,
                ref b, ref d, 
                ref color, ref notes, ref GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            B1 = b[0];
            B2 = b[1];
            B3 = b[2];
            B4 = b[3];
            B5 = b[4];
            B6 = b[5];

            D1 = d[0];
            D2 = d[1];
            D3 = d[2];
            D4 = d[3];
            D5 = d[4];
            D6 = d[5];
            D7 = d[6];
        }


        /// <summary>
        /// This function initializes a precast concrete U girder frame section property.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="B1">The horizontal section dimension B1 defined on the precast concrete U girder definition form. B1 &gt; 0, [L].</param>
        /// <param name="B2">The horizontal section dimension B2 defined on the precast concrete U girder definition form. B2 &gt; 0, [L].</param>
        /// <param name="B3">The horizontal section dimension B3 defined on the precast concrete U girder definition form. B3 &gt; 0, [L].</param>
        /// <param name="B4">The horizontal section dimension B4 defined on the precast concrete U girder definition form. B4 &gt;= 0, [L].</param>
        /// <param name="B5">The horizontal section dimension B5 defined on the precast concrete U girder definition form. B5 &gt;= 0, [L].</param>
        /// <param name="B6">The horizontal section dimension B6 defined on the precast concrete U girder definition form. B6 &gt;= 0, [L].</param>
        /// <param name="D1">The vertical section dimension D1 defined on the precast concrete U girder definition form. D1 &gt; 0, [L].</param>
        /// <param name="D2">The vertical section dimension D2 defined on the precast concrete U girder definition form. D2 &gt; 0, [L].</param>
        /// <param name="D3">The vertical section dimension D3 defined on the precast concrete U girder definition form. D3 &gt;= 0, [L].</param>
        /// <param name="D4">The vertical section dimension D4 defined on the precast concrete U girder definition form. D4 &gt;= 0, [L].</param>
        /// <param name="D5">The vertical section dimension D5 defined on the precast concrete U girder definition form. D5 &gt;= 0, [L].</param>
        /// <param name="D6">The vertical section dimension D6 defined on the precast concrete U girder definition form. D6 &gt;= 0, [L].</param>
        /// <param name="D7">The vertical section dimension D7 defined on the precast concrete U girder definition form. D7 &gt;= 0, [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramref name="color" /> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetPrecastU(string name,
            string nameMaterial,
            double B1,
            double B2,
            double B3,
            double B4,
            double B5,
            double B6,
            double D1,
            double D2,
            double D3,
            double D4,
            double D5,
            double D6,
            double D7,
            int color = -1,
            string notes = "",
            string GUID = "")
        {
            //TODO: Handle invalid argument data.
            double[] b = { B1, B2, B3, B4, B5, B6};
            double[] d = { D1, D2, D3, D4, D5, D6, D7 };

            _callCode = _sapModel.PropFrame.SetPrecastU(name, nameMaterial,
                ref b, ref d,
                color, notes, GUID);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
