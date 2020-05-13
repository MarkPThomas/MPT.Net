using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using FrameSectionApi = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.FrameSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.FrameSections
{
    public abstract class FrameSection : CrossSection
    {
        protected static FrameSectionApi _frameSection => Registry.Application.Model.Definitions.Properties.FrameSection;

        /// <summary>
        /// Gets or sets the type of the frame.
        /// </summary>
        /// <value>The type of the frame.</value>
        public eFrameSectionType Type { get; protected set; }

        /// <summary>
        /// Gets or sets the frame modifiers.
        /// </summary>
        /// <value>The modifiers.</value>
        public FrameModifier Modifiers { get; protected set; }

        /// <summary>
        /// The section depth. [L].
        /// </summary>
        /// <value>The t3.</value>
        public double t3 { get; protected set; }

        /// <summary>
        /// The flange width. [L].
        /// </summary>
        /// <value>The t2.</value>
        public double t2 { get; protected set; }

        /// <summary>
        /// The flange thickness. [L].
        /// </summary>
        /// <value>The tf.</value>
        public double tf { get; protected set; }

        /// <summary>
        /// The web thickness. [L].
        /// </summary>
        /// <value>The tw.</value>
        public double tw { get; protected set; }

        /// <summary>
        /// The bottom flange width. [L].
        /// </summary>
        /// <value>The T2B.</value>
        public double t2b { get; protected set; }

        /// <summary>
        /// The bottom flange thickness. [L].
        /// </summary>
        /// <value>The TFB.</value>
        public double tfb { get; protected set; }

        /// <summary>
        /// The gross cross-sectional area. [L^2].
        /// </summary>
        /// <value>The ag.</value>
        public double Ag { get; protected set; }

        /// <summary>
        /// The shear area for forces in the section local 2-axis direction. [L^2].
        /// </summary>
        /// <value>The as2.</value>
        public double As2 { get; protected set; }

        /// <summary>
        /// The shear area for forces in the section local 3-axis direction. [L^2].
        /// </summary>
        /// <value>The as3.</value>
        public double As3 { get; protected set; }

        /// <summary>
        /// The torsional constant. [L^4].
        /// </summary>
        /// <value>The j.</value>
        public double J { get; protected set; }

        /// <summary>
        /// The moment of inertia for bending about the local 2 axis. [L^4].
        /// </summary>
        /// <value>The i22.</value>
        public double I22 { get; protected set; }

        /// <summary>
        /// The moment of inertia for bending about the local 3 axis. [L^4].
        /// </summary>
        /// <value>The i33.</value>
        public double I33 { get; protected set; }

        /// <summary>
        /// The section modulus for bending about the local 2 axis. [L^3].
        /// </summary>
        /// <value>The S22.</value>
        public double S22 { get; protected set; }

        /// <summary>
        /// The section modulus for bending about the local 3 axis. [L^3].
        /// </summary>
        /// <value>The S33.</value>
        public double S33 { get; protected set; }

        /// <summary>
        /// The plastic modulus for bending about the local 2 axis. [L^3].
        /// </summary>
        /// <value>The Z22.</value>
        public double Z22 { get; protected set; }

        /// <summary>
        /// The plastic modulus for bending about the local 3 axis. [L^3].
        /// </summary>
        /// <value>The Z33.</value>
        public double Z33 { get; protected set; }

        /// <summary>
        /// The radius of gyration about the local 2 axis. [L].
        /// </summary>
        /// <value>The R22.</value>
        public double r22 { get; protected set; }

        /// <summary>
        /// The radius of gyration about the local 3 axis. [L].
        /// </summary>
        /// <value>The R33.</value>
        public double r33 { get; protected set; }


        /// <summary>
        /// Gets all defined frame sections.
        /// </summary>
        /// <returns>List&lt;LoadPattern&gt;.</returns>
        public static List<FrameSection> GetAll()
        {
            List<FrameSection> objects = new List<FrameSection>();
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _frameSection.GetAllFrameProperties(
                out var names,
                out var frameType,
                out var t3,
                out var t2,
                out var tf,
                out var tw,
                out var t2b,
                out var tfb);
            for (int i = 0; i < names.Length; i++)
            {
                FrameSection frameSection = Factory(names[i], t3[i], t2[i], tf[i], tw[i], t2b[i], tfb[i]);
                objects.Add(frameSection);
            }
#else
            List<string> names = GetNameList();
            foreach (var name in names)
            {
                FrameSection frameSection = Factory(name);

                objects.Add(frameSection);
            }
#endif

            return objects;
        }

        /// <summary>
        /// Gets all defined frame sections of a specified frame type.
        /// </summary>
        /// <returns>List&lt;LoadPattern&gt;.</returns>
        public static List<FrameSection> GetAll(eFrameSectionType frameType)
        {
            List<FrameSection> objects = new List<FrameSection>();
            List<string> names = GetNameList(frameType);
            foreach (var name in names)
            {
                FrameSection frameSection = Factory(name);

                objects.Add(frameSection);
            }

            return objects;
        }


        public static FrameSection Factory(
            string uniqueName,  
            double t3,
            double t2,
            double tf,
            double tw,
            double t2b,
            double tfb)
        {
            if (Registry.FrameSections.Keys.Contains(uniqueName)) return Registry.FrameSections[uniqueName];

            FrameSection frameSection = Factory(uniqueName);
            frameSection.t3 = t3;
            frameSection.t2 = t2;
            frameSection.tf = tf;
            frameSection.tw = tw;
            frameSection.t2b = t2b;
            frameSection.tfb = tfb;
            
            Registry.FrameSections.Add(uniqueName, frameSection);
            return frameSection;
        }


        public static FrameSection Factory(string uniqueName)
        {
            if (Registry.FrameSections.Keys.Contains(uniqueName)) return Registry.FrameSections[uniqueName];

            eFrameSectionType frameSectionType = GetType(uniqueName);
            FrameSection frameSection = null;
            switch (frameSectionType)
            {
                case eFrameSectionType.Rectangular:
                case eFrameSectionType.BucklingRestrainedBrace:
                    frameSection = RectangleSection.Factory(uniqueName);
                    break;
                default:
                    return null;
            }

            if (_frameSection != null)
            {
                frameSection.GetFrameType();
                frameSection.GetSectionProperties();
            }
            Registry.FrameSections.Add(uniqueName, frameSection);
            return frameSection;
        }


        /// <summary>
        /// Returns the names of all defined frame properties.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public static List<string> GetNameList()
        {
            return new List<string>(getNameList(_frameSection));
        }

        /// <summary>
        /// Returns the names of all defined frame properties of the specified type.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public static List<string> GetNameList(eFrameSectionType frameType)
        {
            return new List<string>(_frameSection.GetNameList(frameType));
        }

        /// <summary>
        /// Gets the frame section type.
        /// </summary>
        /// <param name="name">The name of the section.</param>
        /// <returns>eFrameSectionType.</returns>
        public static eFrameSectionType GetType(string name)
        {
            return _frameSection.GetType(name);
        }



        protected FrameSection(string name, eFrameSectionType type = eFrameSectionType.All) 
            : base(name)
        {
            Type = type;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            GetFrameType();
            GetSectionProperties();
            GetModifiers();
        }


        #region Methods: Interface

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void ChangeName(string newName)
        {
            changeName(_frameSection, newName);
        }
        
        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public override void Delete()
        {
            delete(_frameSection);
        }
        
        #endregion

        #region Methods: Section
        /// <summary>
        /// Returns properties for frame section.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetSectionProperties()
        {
            _frameSection.GetSectionProperties(Name,
                out var Ag,
                out var As2,
                out var As3,
                out var J,
                out var I22,
                out var I33,
                out var S22,
                out var S33,
                out var Z22,
                out var Z33,
                out var r22,
                out var r33);
            this.Ag = Ag;
            this.As2 = As2;
            this.As3 = As3;
            this.J = J;
            this.I22 = I22;
            this.I33 = I33;
            this.S22 = S22;
            this.S33 = S33;
            this.Z22 = Z22;
            this.Z33 = Z33;
            this.r22 = r22;
            this.r33 = r33;
        }
        
        /// <summary>
        /// Returns the property type for the specified frame section property.
        /// </summary>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void GetFrameType()
        {
            Type = GetType(Name);
        }
        #endregion

        #region Methods: Imported Section

        #endregion

        #region Methods: Modifiers
        /// <summary>
        /// Returns the modifier assignment for frame properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetModifiers()
        {
            Modifiers = _frameSection.GetModifiers(Name);
        }

        /// <summary>
        /// This function defines the modifier assignment for frame properties.
        /// The default value for all modifiers is one.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModifiers()
        {
            _frameSection.SetModifiers(Name, Modifiers);
        }
        #endregion
        
        #region Methods: Get/Set

        #endregion
    }
}
