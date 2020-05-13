// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-21-2018
// ***********************************************************************
// <copyright file="Designer.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiDesigner = MPT.CSI.API.Core.Program.ModelBehavior.Designer;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{

    /// <summary>
    /// Class Designer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Design.Designer" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public abstract class Designer<T> : Designer
        where T : IAutoSection, IComboStrength
    {
        #region Fields & Properties        
        /// <summary>
        /// The load combinations
        /// </summary>
        protected readonly LoadCombinations _loadCombinations;

        /// <summary>
        /// The groups
        /// </summary>
        protected readonly Groups _groups;

        /// <summary>
        /// The API automatic section
        /// </summary>
        private readonly IAutoSection _apiAutoSection;
        /// <summary>
        /// The design groups
        /// </summary>
        private GroupsDesign _designGroups;
        /// <summary>
        /// Gets the design groups.
        /// </summary>
        /// <value>The design groups.</value>
        public GroupsDesign DesignGroups =>
            _designGroups ??
            (_designGroups = new GroupsDesign(_apiAutoSection, _groups));

#if !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
    private bool? _autogenerateLoadCombinations;    
    public bool AutogenerateLoadCombinations
        {
            get
            {
                if (_autogenerateLoadCombinations == null)
                {
                    FillComboAutoGenerate();
                }

                return _autogenerateLoadCombinations ?? false;
            }
        }
#endif
        /// <summary>
        /// The API combo strength
        /// </summary>
        private readonly IComboStrength _apiComboStrength;
        /// <summary>
        /// The load combinations strength
        /// </summary>
        private LoadCombinationsStrength _loadCombinationsStrength;
        /// <summary>
        /// Gets the load combinations strength.
        /// </summary>
        /// <value>The load combinations strength.</value>
        public LoadCombinationsStrength LoadCombinationsStrength =>
            _loadCombinationsStrength ??
            (_loadCombinationsStrength = new LoadCombinationsStrength(_apiComboStrength, _loadCombinations));
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="Designer{T}" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="groups">The groups.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="apiObject">The API object.</param>
        protected Designer(
            ApiCSiApplication app,
            Groups groups,
            LoadCombinations loadCombinations,
            T apiObject) : base(app)
        {
            _apiComboStrength = apiObject;
            _apiAutoSection = apiObject;
            _groups = groups;
            _loadCombinations = loadCombinations;
        }
        #endregion

        #region Abstract Methods        
        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void DeleteResults();

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void ResetOverwrites();

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// Retrieves the value of the automatically generated code-based design load combinations option.
        /// True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillComboAutoGenerate();

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetComboAutoGenerate(bool autoGenerate);

        // ===
#endif
        #endregion

        #region API Functions
        /// <summary>
        /// Deletes all frame design results.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void deleteResults(IResettable app)
        {
            app.DeleteResults();
        }

        /// <summary>
        /// Resets all frame design overwrites to default values.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void resetOverwrites(IResettable app)
        {
            app.ResetOverwrites();
        }

        // === Get/Set ===
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017 && !BUILD_SAP2000v16 && !BUILD_SAP2000v17
        /// <summary>
        /// Retrieves the value of the automatically generated code-based design load combinations option.
        /// True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getComboAutoGenerate(IComboAuto app)
        {
            AutogenerateLoadCombinations = app.GetComboAutoGenerate();
        }

        /// <summary>
        /// Sets the value of the automatically generated code-based design load combinations option.
        /// </summary>
        /// <param name="autoGenerate">True: Option to automatically generate code-based design load combinations for concrete frame design is turned on.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setComboAutoGenerate(IComboAuto app, bool autoGenerate)
        {
            app.SetComboAutoGenerate(AutogenerateLoadCombinations);
        }

        // ===
#endif
        #endregion
    }

    /// <summary>
    /// Class Designer.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Design.Designer" />
    public abstract class Designer : CSiOoApiBaseBase
    {
        #region Fields & Properties        
        /// <summary>
        /// Gets the designer.
        /// </summary>
        /// <value>The designer.</value>
        protected ApiDesigner _designer => _apiApp?.Model?.Design;

        /// <summary>
        /// The code
        /// </summary>
        protected string _code;
        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        public string Code
        {
            get
            {
                if (_code == null)
                {
                    FillCode();
                }

                return _code;
            }
        }
        #endregion

        #region Initialization


        /// <summary>
        /// Initializes a new instance of the <see cref="Designer{T}" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        protected Designer(ApiCSiApplication app) : base(app) { }
        #endregion

        #region Abstract Methods        
        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void StartDesign();

        // === Get/Set ===
        /// <summary>
        /// Gets the code name.
        /// </summary>
        public abstract void FillCode();

        /// <summary>
        /// Sets the code.
        /// </summary>
        /// <param name="codeName">Name of the code.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void SetCode(string codeName);

        // === Design Section
        // TODO: Consider injecting designer into frame and calling this internally based on frame assignments
        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="frame">Frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        public abstract string GetDesignSection(Frame frame);

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="frame">Frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        public abstract void FillDesignSection(Frame frame);

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        /// <param name="section">An existing frame section property to be used as the design section for the specified frame objects.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void AddDesignSection(Frame frame, FrameSection section);

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="frame">An existing frame object.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void RemoveDesignSection(Frame frame);
        #endregion

        #region API Functions
        /// <summary>
        /// Starts the frame design.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void startDesign(IDesignRun app)
        {
            app.StartDesign();
        }

        // === Get/Set ===
        /// <summary>
        /// Gets the code name.
        /// </summary>
        /// <param name="app">The application.</param>
        protected void getCode(IDesignCode app)
        {
            _code = app.GetCode();
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="codeName">Name of the design code.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setCode(IDesignCode app, string codeName)
        {
            app.SetCode(Code);
            _code = codeName;
        }

        // ===        
        /// <summary>
        /// Fills the design section.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="frame">The frame.</param>
        protected void fillDesignSection(IDesignCode app, Frame frame)
        {
            frame.SetDesignSection(getDesignSection(app, frame.Name));
        }

        /// <summary>
        /// Adds the design section.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="frame">The frame.</param>
        /// <param name="section">The section.</param>
        protected void addDesignSection(IDesignCode app, Frame frame, FrameSection section)
        {
            setDesignSection(app, frame.Name, section.Name, resetToLastAnalysisSection: false);
            frame.SetDesignSection(section.Name);
        }

        /// <summary>
        /// Removes the design section.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="frame">The frame.</param>
        protected void removeDesignSection(IDesignCode app, Frame frame)
        {
            setDesignSection(app, frame.Name, string.Empty, resetToLastAnalysisSection: true);
            frame.RemoveDesignSection();
        }

        /// <summary>
        /// Retrieves the design section for a specified frame object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="nameFrame">Name of a frame object with a frame design procedure.</param>
        /// <returns>System.String.</returns>
        protected string getDesignSection(IDesignCode app, string nameFrame)
        {
            return app.GetDesignSection(nameFrame);
        }

        /// <summary>
        /// Modifies the design section for all specified frame objects that have a frame design procedure.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="nameFrame">Name of an existing frame object.</param>
        /// <param name="nameSection">Name of an existing frame section property to be used as the design section for the specified frame objects.
        /// This item applies only when resetToLastAnalysisSection = False.</param>
        /// <param name="resetToLastAnalysisSection">True: The design section for the specified frame objects is reset to the last analysis section for the frame object.
        /// False: The design section is set to that specified by nameFrame.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setDesignSection(IDesignCode app,
            string nameFrame,
            string nameSection,
            bool resetToLastAnalysisSection)
        {
            app.SetDesignSection(nameFrame, nameSection, resetToLastAnalysisSection);
        }
        #endregion
    }
}
