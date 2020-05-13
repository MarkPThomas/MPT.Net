// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-17-2018
// ***********************************************************************
// <copyright file="CSiModel.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiCSiModel = MPT.CSI.API.Core.Program.CSiModel;

// TODO: For all, handle name as null or empty for Factory methods.
// TODO: Write out to text file?

namespace MPT.CSI.OOAPI.Core.Program
{
    /// <summary>
    /// Class CSiModel.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.IFill" />
    public class CSiModel : CSiOoApiBaseBase, IFill
    {
        #region Fields
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The API model.</value>
        private ApiCSiModel _apiModel => _apiApp?.Model;
        #endregion

        #region Fields & Properties 
        /// <summary>
        /// The file.
        /// </summary>
        private CSiFile _file;
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The file.</value>
        public CSiFile File => _file ?? (_file = new CSiFile(_apiApp));

        /// <summary>
        /// The analysis
        /// </summary>
        private StructureAnalysis _analysis;
        /// <summary>
        /// Gets the analysis.
        /// </summary>
        /// <value>The analysis.</value>
        public StructureAnalysis Analysis => _analysis ?? (_analysis = new StructureAnalysis(_apiApp));

        /// <summary>
        /// The design
        /// </summary>
        private StructureDesign _design;
        /// <summary>
        /// Gets the design.
        /// </summary>
        /// <value>The design.</value>
        public StructureDesign Design => _design ?? 
                                         (_design = new StructureDesign(
                                                     _apiApp, 
                                                     Loading.Cases,
                                                     Loading.Combinations,
                                                     Groupings.Groups));

        /// <summary>
        /// The loading
        /// </summary>
        private StructureLoading _loading;
        /// <summary>
        /// Gets the loading.
        /// </summary>
        /// <value>The loading.</value>
        public StructureLoading Loading => _loading ?? 
                                           (_loading = new StructureLoading(_apiApp, Analysis.Analyzer));

        /// <summary>
        /// The structure components.
        /// </summary>
        private StructureComponents _components;
        /// <summary>
        /// The structural components, such as material and cross-sections.
        /// This is the model before an analysis is run.
        /// </summary>
        /// <value>The structural model.</value>
        public StructureComponents Components => _components ??
                                                 (_components = new StructureComponents(_apiApp));

        /// <summary>
        /// The structure.
        /// </summary>
        private StructureObjects _structure;

        /// <summary>
        /// The structural model.
        /// This is the model before an analysis is run.
        /// </summary>
        /// <value>The structural model.</value>
        public StructureObjects Structure => _structure ?? (_structure = new StructureObjects(
                                                 _apiApp,
                                                 new StructureComponentsProperties<FrameSection>
                                                 {
                                                     CrossSections = Components.FrameSections,
                                                     Materials = Components.Materials,
                                                     Piers = Components.Piers,
                                                     Spandrels = Components.Spandrels
                                                 },
                                                 new StructureComponentsProperties<AreaSection>()
                                                 {
                                                     CrossSections = Components.AreaSections,
                                                     Materials = Components.Materials,
                                                     Piers = Components.Piers,
                                                     Spandrels = Components.Spandrels
                                                 }));

        /// <summary>
        /// The groupings
        /// </summary>
        private StructureGroupings _groupings;
        /// <summary>
        /// Gets the groupings.
        /// </summary>
        /// <value>The groupings.</value>
        public StructureGroupings Groupings => _groupings ?? (_groupings = new StructureGroupings(_apiApp, Structure));
        #endregion

        #region Properties
        /// <summary>
        /// The database units for the new model.
        /// All data is internally stored in the model in these units.
        /// </summary>
        /// <value>The units initial.</value>
        public eUnits UnitsInitial { get; protected set; }

        /// <summary>
        /// The present units.
        /// </summary>
        /// <value>The units present.</value>
        public eUnits UnitsPresent { get; protected set; }

        /// <summary>
        /// The database units.
        /// </summary>
        /// <value>The units database.</value>
        public eUnits UnitsDatabase { get; protected set; }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Gets or sets the present force units.
        /// </summary>
        /// <value>The units force.</value>
        public eForce UnitsForcePresent { get; protected set; }

        /// <summary>
        /// Gets or sets the present length units.
        /// </summary>
        /// <value>The length of the units.</value>
        public eLength UnitsLengthPresent { get; protected set; }

        /// <summary>
        /// Gets or sets the present temperature units.
        /// </summary>
        /// <value>The units temperature.</value>
        public eTemperature UnitsTemperaturePresent { get; protected set; }

        /// <summary>
        /// Gets or sets the database force units force.
        /// </summary>
        /// <value>The units force.</value>
        public eForce UnitsForceDatabase { get; protected set; }

        /// <summary>
        /// Gets or sets the database length units.
        /// </summary>
        /// <value>The length of the units.</value>
        public eLength UnitsLengthDatabase { get; protected set; }

        /// <summary>
        /// Gets or sets the database temperature units.
        /// </summary>
        /// <value>The units temperature.</value>
        public eTemperature UnitsTemperatureDatabase { get; protected set; }
#endif        
        /// <summary>
        /// The program auto merge tolerance. [L]
        /// </summary>
        /// <value>The merge tolerance.</value>
        public double MergeTolerance { get; protected set; }

        /// <summary>
        /// The name of a defined coordinate system.
        /// </summary>
        /// <value>The present coord system.</value>
        public string PresentCoordinateSystem { get; protected set; }

        /// <summary>
        /// The project information items.
        /// </summary>
        /// <value>The project information items.</value>
        public List<string> ProjectInfoItems { get; protected set; } = new List<string>();

        /// <summary>
        /// The project information data.
        /// </summary>
        /// <value>The project information data.</value>
        public List<string> ProjectInfoData { get; protected set; } = new List<string>();

        /// <summary>
        /// The program version name that is externally displayed to the user.
        /// </summary>
        /// <value>The name of the version.</value>
        public string VersionName { get; protected set; }

        /// <summary>
        /// The program version number that is used internally by the program and not displayed to the user.
        /// </summary>
        /// <value>The version number.</value>
        public double VersionNumber { get; protected set; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The user comment.
        /// </summary>
        /// <value>The user comment.</value>
        public string UserComment { get; protected set; }
#endif        
        #endregion

        #region Initialization 

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiModel" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal CSiModel(ApiCSiApplication app) : base(app)
        {
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public void FillData()
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillUserComment();
#endif
            FillDatabaseUnits();
            FillPresentUnits();
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            FillDatabaseUnitsComponents();
            FillPresentUnitsComponents();
#endif
            FillMergeTolerance();
            FillPresentCoordSystem();
            FillProjectInfo();
            FillVersion();
        }

        #endregion

        #region Methods: Public

        /// <summary>
        /// This function clears the previous model and initializes the program for a new model.
        /// If it is later needed, you should save your previous model prior to calling this function.
        /// After calling the InitializeNewModel function, it is not necessary to also call the ApplicationStart function because the functionality of the ApplicationStart function is included in the InitializeNewModel function.
        /// </summary>
        /// <param name="units">The database units for the new model.
        /// All data is internally stored in the model in these units.</param>
        /// <returns><c>true</c> if a nuew model is successfully initialized, <c>false</c> otherwise.</returns>
        public bool InitializeNewModel(eUnits units = eUnits.kip_in_F)
    {
        UnitsInitial = units;
        return _apiModel.InitializeNewModel(units);
    }

        // ==== Model States ===

        /// <summary>
        /// True: Model is locked.
        /// With some exceptions, definitions and assignments cannot be changed in a model while the model is locked.
        /// If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool IsModelLocked()
        {
            return _apiModel.GetModelIsLocked();
        }

        /// <summary>
        /// The model is locked.
        /// With some exceptions, definitions and assignments can not be changed in a model while the model is locked.
        /// If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void LockModel()
        {
            setModelIsLocked(isLocked: true);
        }

        /// <summary>
        /// The model is unlocked.
        /// With some exceptions, definitions and assignments can not be changed in a model while the model is locked.
        /// If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void UnlockModel()
        {
            setModelIsLocked(isLocked: false);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // ==== User Comment ===
        /// <summary>
        /// Retrieves the data in the user comments and log.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillUserComment()
        {
            UserComment = _apiModel.GetUserComment();
        }

        /// <summary>
        /// Sets the user comments and log data.
        /// </summary>
        /// <param name="commentUser">The data to be added to the user comments and log.</param>
        /// <param name="numberLinesBlank">The number of carriage return and line feeds to be included before the specified comment.
        /// This item is ignored if there are no existing comments.</param>
        /// <param name="replace">True: All existing comments are replaced with the specified comment.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetUserComment(string commentUser, 
            int numberLinesBlank = 1, 
            bool replace = false)
        {
            if (numberLinesBlank < 0) return;

            _apiModel.SetUserComment(commentUser, numberLinesBlank, replace);

            if (replace)
            {
                UserComment = commentUser
            }
            else
            {
                StringBuilder builder = new StringBuilder(UserComment);
                if (!string.IsNullOrEmpty(UserComment))
                {
                    for (int i = 0; i < numberLinesBlank - 1; i++)
                    {
                        builder.AppendLine();
                    }
                }
                builder.Append(commentUser);
                UserComment = builder.ToString();
            }
        }
#endif

        // ==== Units ===
        /// <summary>
        /// Returns the database units for the model.
        /// All data is internally stored in the model in these units and converted to the present units as needed.
        /// </summary>
        /// <returns>eUnits.</returns>
        public void FillDatabaseUnits()
        {
            UnitsDatabase = _apiModel.GetDatabaseUnits();
        }

        /// <summary>
        /// Returns the units presently specified for the model.
        /// </summary>
        /// <returns>eUnits.</returns>
        public void FillPresentUnits()
        {
            UnitsPresent = _apiModel.GetPresentUnits();
        }

        /// <summary>
        /// Sets the units presently specified for the model.
        /// </summary>
        /// <param name="units">The units.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetPresentUnits(eUnits units)
        {
            _apiModel.SetPresentUnits(units);
            UnitsPresent = units;
        }


#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Returns the database units for the model.
        /// All data is internally stored in the model in these units and converted to the present units as needed.
        /// </summary>
        public void FillDatabaseUnitsComponents()
        {
            _apiModel.GetDatabaseUnits(out var forceUnits, out var lengthUnits, out var temperatureUnits);
            UnitsForceDatabase = forceUnits;
            UnitsLengthDatabase = lengthUnits;
            UnitsTemperatureDatabase = temperatureUnits;
        }

        /// <summary>
        /// Returns the units presently specified for the model.
        /// </summary>
        public void FillPresentUnitsComponents()
        {
            _apiModel.GetPresentUnits(out var forceUnits, out var lengthUnits, out var temperatureUnits);
            UnitsForcePresent = forceUnits;
            UnitsLengthPresent = lengthUnits;
            UnitsTemperaturePresent = temperatureUnits;
        }

        /// <summary>
        /// Sets the units presently specified for the model.
        /// </summary>
        /// <param name="forceUnits">The force units.</param>
        /// <param name="lengthUnits">The length units.</param>
        /// <param name="temperatureUnits">The temperature units.</param>
        public void SetPresentUnits(eForce forceUnits,
            eLength lengthUnits,
            eTemperature temperatureUnits)
        {
            _apiModel.SetPresentUnits(forceUnits, lengthUnits, temperatureUnits);
            UnitsForcePresent = forceUnits;
            UnitsLengthPresent = lengthUnits;
            UnitsTemperaturePresent = temperatureUnits;
        }
#endif

        // ==== Get/Set Methods ===

        /// <summary>
        /// Retrieves the value of the program auto merge tolerance.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillMergeTolerance()
        {
            MergeTolerance = _apiModel.GetMergeTolerance();
        }

        /// <summary>
        /// Sets the value of the program auto merge tolerance.
        /// </summary>
        /// <param name="mergeTolerance">The program auto merge tolerance. [L]</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetMergeTolerance(double mergeTolerance)
        {
            _apiModel.SetMergeTolerance(mergeTolerance);
            MergeTolerance = mergeTolerance;
        }

        /// <summary>
        /// Returns the name of the present coordinate system.
        /// </summary>
        /// <returns>System.String.</returns>
        public void FillPresentCoordSystem()
        {
            PresentCoordinateSystem = _apiModel.GetPresentCoordSystem();
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the present coordinate system.
        /// </summary>
        /// <param name="nameCoordinateSystem">The name of a defined coordinate system.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetPresentCoordSystem(string nameCoordinateSystem)
        {
            _apiModel.SetPresentCoordSystem(nameCoordinateSystem);
            PresentCoordinateSystem = nameCoordinateSystem;
        }
#endif

        /// <summary>
        /// Retrieves the project information data.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillProjectInfo()
        {
            _apiModel.GetProjectInfo(out var projectInfoItems, out var projectInfoData);

            ProjectInfoItems = new List<string>();
            ProjectInfoData = new List<string>();
            for (int i = 0; i < projectInfoItems.Length; i++)
            {
                ProjectInfoItems.Add(projectInfoItems[i]);
                ProjectInfoData.Add(projectInfoData[i]);
            }
        }

        /// <summary>
        /// Sets the data for an item in the project information.
        /// </summary>
        /// <param name="projectInfoItem">Name of the project information item</param>
        /// <param name="projectInfoData">Data for the specified project information item.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetProjectInfo(string projectInfoItem,
            string projectInfoData)
        {
            _apiModel.SetProjectInfo(projectInfoItem, projectInfoData);
            ProjectInfoItems.Add(projectInfoItem);
            ProjectInfoData.Add(projectInfoData);
        }


        /// <summary>
        /// Returns the program version.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillVersion()
        {
            _apiModel.GetVersion(out var versionName, out var versionNumber);
            VersionName = versionName;
            VersionNumber = versionNumber;
        }

        #endregion

        #region Protected
        /// <summary>
        /// Sets whether or not the model is locked.
        /// With some exceptions, definitions and assignments can not be changed in a model while the model is locked.
        /// If an attempt is made to change a definition or assignment while the model is locked and that change is not allowed in a locked model, an error will be returned.
        /// </summary>
        /// <param name="isLocked">if set to <c>true</c> [is locked].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        protected void setModelIsLocked(bool isLocked)
        {
            _apiModel.SetModelIsLocked(isLocked);
        }
        #endregion
    }
}
