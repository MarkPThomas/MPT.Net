using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.Frame;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property
{
    /// <summary>
    /// Implements the frame properties in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IObservableFrameModifiers" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableFrameModifiers" />
    public interface IFrameSection: IChangeableName, ICountable, IDeletable, IListableNames,
        IObservableFrameModifiers, IChangeableFrameModifiers
    {
        #region Properties                            
        /// <summary>
        /// Gets the section designer.
        /// </summary>
        /// <value>The section designer.</value>
        SectionDesigner SectionDesigner { get; }
        #endregion

        #region General

        /// <summary>
        /// Returns the names of all defined frame properties of the specified type.
        /// </summary>
        /// <param name="frameType">The frame type to filter the name list by.</param>
        string[] GetNameList(eFrameSectionType frameType);


#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
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
         void GetAllFrameProperties(out string[] names,
            out eFrameSectionType[] frameType,
            out double[] t3,
            out double[] t2,
            out double[] tf,
            out double[] tw,
            out double[] t2b,
            out double[] tfb);
#endif
        #endregion

        #region Methods: Section

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
        void GetSectionProperties(string name,
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
            out double r33);




        /// <summary>
        /// Returns the property type for the specified frame section property.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        eFrameSectionType GetType(string name);

#if !BUILD_ETABS2015 
        /// <summary>
        /// Returns the rebar design type for the specified frame section property.
        /// This function applies only to the following section property types: 
        /// <see cref="eFrameSectionType.TSection"/>;
        /// <see cref="eFrameSectionType.Angle"/>; 
        /// <see cref="eFrameSectionType.Rectangular"/>; 
        /// <see cref="eFrameSectionType.Circle"/>; 
        /// Calling this function for any other type of frame section property returns an error.
        /// A nonzero rebar type is returned only if the frame section property has a concrete material.
        /// TODO: Handle this. </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        eRebarType GetRebarType(string name);
#endif
        #endregion

        #region Methods: Imported Section

        /// <summary>
        /// This function imports a frame section property from a property file.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added. 
        /// This name does not need to be the same as the <paramout name="sectionName"/> item.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="fileName">The name of the frame section property file from which to get the frame section property specified by the <paramout name="sectionName"/> item.
        /// In most cases you can input just the name of the property file (e.g.Sections8.pro) and the program will be able to find it.
        /// In some cases you may have to input the full path to the property file.
        /// TODO: Handle this.</param>
        /// <param name="sectionName">The name of the frame section property, inside the property file specified by the <paramout name="fileName"/> item, that is to be imported.</param>
        /// <param name="color">The display color assigned to the section. 
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void ImportSectionProperty(string name,
            string nameMaterial,
            string fileName,
            string sectionName,
            int color = -1,
            string notes = "",
            string GUID = "");

#if !BUILD_ETABS2015 
        /// <summary>
        /// Returns the names of the section property file from which an imported frame section originated, and it also retrieves the section name used in the property file.
        /// If the specified frame section property was not imported, blank strings are returned for <paramout name="nameInFile"/> and <paramout name="fileName"/>.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameInFile">The name of the specified frame section property in the frame section property file.</param>
        /// <param name="fileName">The name of the frame section property file from which the specified frame section property was obtained.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="frameSectionType">Type of frame section.</param>
        void GetNameInPropertyFile(string name,
            out string nameInFile,
            out string fileName,
            out string nameMaterial,
            out eFrameSectionType frameSectionType);
#endif

        /// <summary>
        /// Returns the names of all defined frame section properties of a specified type in a specified frame section property file.
        /// </summary>
        /// <param name="fileName">The name of the frame section property file from which to get the name list.
        /// In most cases, inputting only the name of the property file (e.g.Sections8.pro) is required, and the program will be able to find it.
        /// In some cases, inputting the full path to the property file may be necessary. </param>
        /// <param name="sectionNames">The property names obtained from the frame section property file.</param>
        /// <param name="frameSectionTypes">The frame section property type for each property obtained from the frame section property file.</param>
        /// <param name="frameSectionType">Type of frame section to filter the list by.
        /// If no value is input for <paramout name="frameSectionType"/>, names are returned for all frame section properties in the specified file regardless of type.</param>
        void GetPropertyFileNameList(string fileName,
            out string[] sectionNames,
            out eFrameSectionType[] frameSectionTypes,
            eFrameSectionType frameSectionType);
        #endregion

        #region Methods: Get/Set Sections - Other

        /// <summary>
        ///Returns frame section property data for a general frame section.
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
        /// <param name="color">The display color assigned to the section. </param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetGeneral(string name,
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
            out string GUID);


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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetGeneral(string name,
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
            string GUID = "");




        /// <summary>
        /// Gets the non prismatic.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property.</param>
        /// <param name="startSections">The names of the frame section properties at the start of each segment.</param>
        /// <param name="endSections">The names of the frame section properties at the start of each segment.</param>
        /// <param name="lengths">The length of each segment. 
        /// The length may be variable or absolute as indicated by the <paramout name="prismaticTypes"/> item. [L] when length is absolute.</param>
        /// <param name="prismaticTypes">The prismatic length type of each segment.</param>
        /// <param name="EI33">The variation type for EI33 in each segment.</param>
        /// <param name="EI22">The variation type for EI22 in each segment.</param>
        /// <param name="color">The display color assigned to the section. </param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetNonPrismatic(string name,
            out string[] startSections,
            out string[] endSections,
            out double[] lengths,
            out ePrismaticType[] prismaticTypes,
            out ePrismaticInertiaType[] EI33,
            out ePrismaticInertiaType[] EI22,
            out int color,
            out string notes,
            out string GUID);


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
        /// The length may be variable or absolute as indicated by the <paramout name="prismaticTypes"/> item. [L] when length is absolute.</param>
        /// <param name="prismaticTypes">The prismatic length type of each segment.</param>
        /// <param name="EI33">The variation type for EI33 in each segment.</param>
        /// <param name="EI22">The variation type for EI22 in each segment.</param>
        /// <param name="color">The display color assigned to the section. 
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetNonPrismatic(string name,
            string[] startSections,
            string[] endSections,
            double[] lengths,
            ePrismaticType[] prismaticTypes,
            ePrismaticInertiaType[] EI33,
            ePrismaticInertiaType[] EI22,
            int color = -1,
            string notes = "",
            string GUID = "");




#if !BUILD_ETABS2015
        /// <summary>
        /// Gets the section designer section.
        /// </summary>
        /// <param name="name">The name of an existing section designer pro.</param>
        /// <param name="nameMaterial">The name of the base material property for the sec.</param>
        /// <param name="shapeNames">The name of each shape in the section designer section.</param>
        /// <param name="sectionTypes">The type of each shape in the section designer section.</param>
        /// <param name="designType">The design option for the section.</param>
        /// <param name="color">The display color assigned to the section. </param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetSectionDesignerSection(string name,
            out string nameMaterial,
            out string[] shapeNames,
            out eSectionDesignerSectionType[] sectionTypes,
            out eSectionDesignerDesignOption designType,
            out int color,
            out string notes,
            out string GUID);

        /// <summary>
        /// Sets the section designer section.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the base material property for the section.</param>
        /// <param name="designType">The design option for the section.
        /// When <paramout name="designType"/> = <see cref="eSectionDesignerDesignOption.GeneralSteel"/> is assigned, the material property specified by the <paramout name="nameMaterial"/> item must be a steel material; 
        /// otherwise the program sets <paramout name="designType"/> = <see cref="eSectionDesignerDesignOption.NoDesign"/>.
        /// Similarly, when <paramout name="designType"/> = <see cref="eSectionDesignerDesignOption.ConcreteColumnCheck"/> or <paramout name="designType"/> = <see cref="eSectionDesignerDesignOption.ConcreteColumnDesign"/> is assigned, the material property specified by the <paramout name="nameMaterial"/> item must be a concrete material; 
        /// otherwise the program sets <paramout name="designType"/> = <see cref="eSectionDesignerDesignOption.NoDesign"/>.</param>
        /// <param name="color">The display color assigned to the section. 
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetSectionDesignerSection(string name,
            string nameMaterial,
            eSectionDesignerDesignOption designType,
            int color = -1,
            string notes = "",
            string GUID = "");
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
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramout name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetAutoSelectAluminum(string name,
            out string[] sectionNames,
            out string autoStartSection,
            out string notes,
            out string GUID);


        /// <summary>
        /// Assigns frame section properties to an aluminum auto select list. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.
        /// Auto select lists and nonprismatic (variable) sections are not allowed in this array.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramout name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetAutoSelectAluminum(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "");



        /// <summary>
        /// Returns frame section property data for cold-formed steel auto select list.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramout name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetAutoSelectColdFormed(string name,
            out string[] sectionNames,
            out string autoStartSection,
            out string notes,
            out string GUID);

        /// <summary>
        /// Assigns frame section properties to a cold-formed steel auto select list. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.
        /// Auto select lists and nonprismatic (variable) sections are not allowed in this array.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramout name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetAutoSelectColdFormed(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "");
#endif



        /// <summary>
        /// Returns frame section property data for a steel auto select list.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramout name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetAutoSelectSteel(string name,
            out string[] sectionNames,
            out string autoStartSection,
            out string notes,
            out string GUID);

        /// <summary>
        /// Assigns frame section properties to a steel auto select list. 
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionNames">The section names of the frame section properties included in the auto select list.
        /// Auto select lists and nonprismatic (variable) sections are not allowed in this array.</param>
        /// <param name="autoStartSection">The Median or the name of a frame section property in the <paramout name="sectionNames"/> array. 
        /// It is the starting section for the auto select list.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section. 
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetAutoSelectSteel(string name,
            string[] sectionNames,
            string autoStartSection,
            string notes = "",
            string GUID = "");
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
        void GetSteelTee(string name,
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
            out string GUID);


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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetSteelTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double r,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "");




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
        void GetSteelAngle(string name,
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
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetSteelAngle(string name,
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
            string GUID = "");
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
        void GetChannel(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out int color,
            out string notes,
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetChannel(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");




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
        void GetDoubleAngle(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out double separation,
            out int color,
            out string notes,
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetDoubleAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double separation,
            int color = -1,
            string notes = "",
            string GUID = "");





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
        /// <param name="separation">The back-to-back distance between the angles. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetDoubleChannel(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out double separation,
            out int color,
            out string notes,
            out string GUID);

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
        /// <param name="separation">The back-to-back distance between the angles. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetDoubleChannel(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double separation,
            int color = -1,
            string notes = "",
            string GUID = "");




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
        void GetISection(string name,
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
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetISection(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            double t2Bottom,
            double tfBottom,
            int color = -1,
            string notes = "",
            string GUID = "");
#endregion

#region Methods: Get/Set Sections - Steel: Built-Up

        /// <summary>
        /// Returns frame section property data for a cover plated I-Section-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="sectionName">The name of an existing I-type frame section property that is used for the I-section portion of the coverplated I section.</param>
        /// <param name="fyTopFlange">The yield strength of the top flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramout name="sectionName"/> item is used.</param>
        /// <param name="fyWeb">The yield strength of the web of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramout name="sectionName"/> item is used.</param>
        /// <param name="fyBottomFlange">The yield strength of the bottom flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramout name="sectionName"/> item is used.</param>
        /// <param name="tc">The thickness of the top cover plate. [L]
        /// If the <paramout name="tc"/> or the <paramout name="bc"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bc">The width of the top cover plate. [L]
        /// If the <paramout name="tc"/> or the <paramout name="bc"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialTop">The name of the material property for the top cover plate.
        /// This item applies only if both the <paramout name="tc"/> and the <paramout name="bc"/> items are greater than 0.</param>
        /// <param name="tcBottom">The thickness of the bottom cover plate. [L]
        /// If the <paramout name="tcBottom"/> or the <paramout name="bcBottom"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bcBottom">The width of the bottom cover plate. [L]
        /// If the <paramout name="tcBottom"/> or the <paramout name="bcBottom"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialBottom">The name of the material property for the bottom cover plate.
        /// This item applies only if both the <paramout name="tcBottom"/> and the <paramout name="bcBottom"/> items are greater than 0.</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetCoverPlatedI(string name,
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
            out string GUID);

        /// <summary>
        /// This function initializes frame section property data for a cover plated I-Section-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="sectionName">The name of an existing I-type frame section property that is used for the I-section portion of the coverplated I section.</param>
        /// <param name="fyTopFlange">The yield strength of the top flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramout name="sectionName"/> item is used.</param>
        /// <param name="fyWeb">The yield strength of the web of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramout name="sectionName"/> item is used.</param>
        /// <param name="fyBottomFlange">The yield strength of the bottom flange of the I-section. [F/L^2]
        /// If this item is 0, the yield strength of the I-section specified by the <paramout name="sectionName"/> item is used.</param>
        /// <param name="tc">The thickness of the top cover plate. [L]
        /// If the <paramout name="tc"/> or the <paramout name="bc"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bc">The width of the top cover plate. [L]
        /// If the <paramout name="tc"/> or the <paramout name="bc"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialTop">The name of the material property for the top cover plate.
        /// This item applies only if both the <paramout name="tc"/> and the <paramout name="bc"/> items are greater than 0.</param>
        /// <param name="tcBottom">The thickness of the bottom cover plate. [L]
        /// If the <paramout name="tcBottom"/> or the <paramout name="bcBottom"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="bcBottom">The width of the bottom cover plate. [L]
        /// If the <paramout name="tcBottom"/> or the <paramout name="bcBottom"/> item is less than or equal to 0, no top cover plate exists.</param>
        /// <param name="nameMaterialBottom">The name of the material property for the bottom cover plate.
        /// This item applies only if both the <paramout name="tcBottom"/> and the <paramout name="bcBottom"/> items are greater than 0.</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetCoverPlatedI(string name,
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
            string GUID = "");




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
        void GetHybridISection(string name,
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
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetHybridISection(string name,
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
            string GUID = "");





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
        void GetHybridUSection(string name,
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
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetHybridUSection(string name,
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
            string GUID = "");
#endif
#endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
#region Methods: Get/Set Sections - Cold-Formed Steel

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
        void GetColdC(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double thickness,
            out double radius,
            out double lipDepth,
            out int color,
            out string notes,
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetColdC(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            int color = -1,
            string notes = "",
            string GUID = "");





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
        void GetColdHat(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double thickness,
            out double radius,
            out double lipDepth,
            out int color,
            out string notes,
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetColdHat(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            int color = -1,
            string notes = "",
            string GUID = "");





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
        /// <param name="lipAngle">The lip angle measured from horizontal (0 &lt;= <paramout name="lipAngle"/>  &lt;= 90). [deg].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetColdZ(string name,
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
            out string GUID);

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
        /// <param name="lipAngle">The lip angle measured from horizontal (0 &lt;= <paramout name="lipAngle"/>  &lt;= 90). [deg].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetColdZ(string name,
            string nameMaterial,
            double t3,
            double t2,
            double thickness,
            double radius,
            double lipDepth,
            double lipAngle,
            int color = -1,
            string notes = "",
            string GUID = "");
#endregion
#endif

#region Methods: Get/Set Sections - Steel/Concrete
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Returns frame section property data for a plate-type frame section.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="fileName">If the section property was imported from a property file, this is the name of that file. 
        /// If the section property was not imported, this item is blank.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t2">Plate width. [L].</param>
        /// <param name="t3">Plate depth. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void GetPlate(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out int color,
            out string notes,
            out string GUID);

        /// <summary>
        /// This function initializes frame section property data for a plate-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t2">Plate width. [L].</param>
        /// <param name="t3">Plate depth. [L].</param>
        /// <param name="color">The display color assigned to the section.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.</param>
        void SetPlate(string name,
            string nameMaterial,
            double t3,
            double t2,
            int color = -1,
            string notes = "",
            string GUID = "");


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
        void GetRod(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out int color,
            out string notes,
            out string GUID);

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
        void SetRod(string name,
            string nameMaterial,
            double t3,
            int color = -1,
            string notes = "",
            string GUID = "");
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
        void GetCircle(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out int color,
            out string notes,
            out string GUID);

        /// <summary>
        /// This function initializes frame section property data for a circle-type frame section.
        /// If this function is called for an existing frame section property, all items for the section are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new frame section property. 
        /// If this is an existing property, that property is modified; otherwise, a new property is added.</param>
        /// <param name="nameMaterial">The name of the material property for the section.</param>
        /// <param name="t3">The section diameter. [L].</param>
        /// <param name="color">The display color assigned to the section.
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetCircle(string name,
            string nameMaterial,
            double t3,
            int color = -1,
            string notes = "",
            string GUID = "");




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
        void GetRectangle(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out int color,
            out string notes,
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetRectangle(string name,
            string nameMaterial,
            double t3,
            double t2,
            int color = -1,
            string notes = "",
            string GUID = "");





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
        void GetPipe(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double tw,
            out int color,
            out string notes,
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetPipe(string name,
            string nameMaterial,
            double t3,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");





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
        void GetTube(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out int color,
            out string notes,
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetTube(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");


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
        void GetTee(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out int color,
            out string notes,
            out string GUID);


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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");




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
        void GetAngle(string name,
            out string fileName,
            out string nameMaterial,
            out double t3,
            out double t2,
            out double tf,
            out double tw,
            out int color,
            out string notes,
            out string GUID);

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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetAngle(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double tw,
            int color = -1,
            string notes = "",
            string GUID = "");
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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void GetConcreteTee(string name,
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
            out string GUID);


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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetConcreteTee(string name,
            string nameMaterial,
            double t3,
            double t2,
            double tf,
            double twF,
            double twT,
            bool mirrorAbout3,
            int color = -1,
            string notes = "",
            string GUID = "");


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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void GetConcreteL(string name,
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
            out string GUID);


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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetConcreteL(string name,
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
            string GUID = "");
#endif


        /// <summary>
        /// Returns beam rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or this function returns an error.
        /// This function applies only to the following section types. Calling this function for any other type of frame section property returns an error:
        /// <see cref="eFrameSectionType.TSection"/>; 
        /// <see cref="eFrameSectionType.Angle"/>;
        /// <see cref="eFrameSectionType.Rectangular"/>;
        /// <see cref="eFrameSectionType.Circle"/>
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
        void GetRebarBeam(string name,
            out string materialNameLongitudinal,
            out string materialNameConfinement,
            out double coverTop,
            out double coverBottom,
            out double topLeftArea,
            out double topRightArea,
            out double bottomLeftArea,
            out double bottomRightArea);


        /// <summary>
        /// Assigns beam rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or this function returns an error.
        /// This function applies only to the following section types. Calling this function for any other type of frame section property returns an error:
        /// <see cref="eFrameSectionType.TSection"/>; 
        /// <see cref="eFrameSectionType.Angle"/>;
        /// <see cref="eFrameSectionType.Rectangular"/>;
        /// <see cref="eFrameSectionType.Circle"/>
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
        void SetRebarBeam(string name,
            string materialNameLongitudinal,
            string materialNameConfinement,
            double coverTop,
            double coverBottom,
            double topLeftArea,
            double topRightArea,
            double bottomLeftArea,
            double bottomRightArea);



        /// <summary>
        /// Returns column rebar data for frame sections.
        /// The material assigned to the specified frame section property must be concrete or else this function returns an error.
        /// Calling this function for any type of frame section property other than the following returns an error:
        /// <see cref="eFrameSectionType.Rectangular"/>;
        /// <see cref="eFrameSectionType.Circle"/>;
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="materialNameLongitudinal">The name of the rebar material property for the longitudinal rebar.</param>
        /// <param name="materialNameConfinement">The name of the rebar material property for the confinement rebar.</param>
        /// <param name="rebarConfiguration">The rebar configuration.
        /// For circular frame section properties this item must be <see cref="eRebarConfiguration.Circular"/>; otherwise an error is returned.
        /// TODO: Handle This</param>
        /// <param name="confinementType">Type of the confinement.
        /// This item applies only when <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Circular" />.
        /// If <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />, the confinement bar type is assumed to be <see cref="eConfinementType.Ties" />.</param>
        /// <param name="cover">The clear cover for the confinement steel (ties). 
        /// In the special case of circular reinforcement in a rectangular column, this is the minimum clear cover. [L].</param>
        /// <param name="numberOfCircularBars">The total number of longitudinal reinforcing bars in the column.
        /// This item applies to a circular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Circular"/>.</param>
        /// <param name="numberOfRectangularBars3Axis">The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 3-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="numberOfRectangularBars2Axis">is the number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 2-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="rebarSize">The rebar name for the longitudinal rebar in the column.</param>
        /// <param name="tieSize">The rebar name for the confinement rebar in the column.</param>
        /// <param name="tieSpacingLongitudinal">The longitudinal spacing of the confinement bars (ties). [L].</param>
        /// <param name="numberOfConfinementBars2Axis">It is the number of confinement bars (tie legs) running in the local 2-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="numberOfConfinementBars3Axis">It is the number of confinement bars (tie legs) running in the local 3-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="toBeDesigned">True: The column longitudinal rebar is to be designed; otherwise it is to be checked.</param>
        void GetRebarColumn(string name,
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
            out bool toBeDesigned);


        /// <summary>
        /// Assigns column rebar data to frame sections.
        /// The material assigned to the specified frame section property must be concrete or else this function returns an error.
        /// Calling this function for any type of frame section property other than the following returns an error:
        /// <see cref="eFrameSectionType.Rectangular"/>;
        /// <see cref="eFrameSectionType.Circle"/>;
        /// </summary>
        /// <param name="name">The name of an existing frame section property.</param>
        /// <param name="materialPropertyLongitudinal">The name of the rebar material property for the longitudinal rebar.</param>
        /// <param name="materialNameConfinement">The name of the rebar material property for the confinement rebar.</param>
        /// <param name="rebarConfiguration">The rebar configuration.
        /// For circular frame section properties this item must be <see cref="eRebarConfiguration.Circular"/>; otherwise an error is returned.
        /// TODO: Handle This</param>
        /// <param name="confinementType">Type of the confinement.
        /// This item applies only when <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Circular" />.
        /// If <paramref name="rebarConfiguration" /> = <see cref="eRebarConfiguration.Rectangular" />, the confinement bar type is assumed to be <see cref="eConfinementType.Ties" />.</param>
        /// <param name="cover">The clear cover for the confinement steel (ties). 
        /// In the special case of circular reinforcement in a rectangular column, this is the minimum clear cover. [L].</param>
        /// <param name="numberOfCircularBars">The total number of longitudinal reinforcing bars in the column.
        /// This item applies to a circular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Circular"/>.</param>
        /// <param name="numberOfRectangularBars3Axis">The number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 3-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="numberOfRectangularBars2Axis">is the number of longitudinal bars (including the corner bar) on each face of the column that is parallel to the local 2-axis of the column.
        /// This item applies to a rectangular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="rebarSize">The rebar name for the longitudinal rebar in the column.</param>
        /// <param name="tieSize">The rebar name for the confinement rebar in the column.</param>
        /// <param name="tieSpacingLongitudinal">The longitudinal spacing of the confinement bars (ties). [L].</param>
        /// <param name="numberOfConfinementBars2Axis">It is the number of confinement bars (tie legs) running in the local 2-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="numberOfConfinementBars3Axis">It is the number of confinement bars (tie legs) running in the local 3-axis direction of the column.
        /// This item applies to a rectangular rebar configuration, <paramout name="rebarConfiguration"/> = <see cref="eRebarConfiguration.Rectangular"/>.</param>
        /// <param name="toBeDesigned">True: The column longitudinal rebar is to be designed; otherwise it is to be checked.</param>
        void SetRebarColumn(string name,
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
            bool toBeDesigned);
#endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
#region Methods: Get/Set Sections - Concrete: Precast


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
        void GetPrecastI(string name,
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
            out string GUID);


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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetPrecastI(string name,
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
            string GUID = "");




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
        void GetPrecastU(string name,
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
            out string GUID);


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
        /// If <paramout name="color"/> is specified as -1, the program will automatically assign a color.</param>
        /// <param name="notes">The notes, if any, assigned to the section.</param>
        /// <param name="GUID">The GUID (global unique identifier), if any, assigned to the section.
        /// If this item is input as Default, the program assigns a GUID to the section.</param>
        void SetPrecastU(string name,
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
            string GUID = "");

#endregion
#endif
    }
}