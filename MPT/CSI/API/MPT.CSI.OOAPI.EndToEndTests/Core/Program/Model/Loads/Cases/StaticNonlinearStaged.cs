// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StaticNonlinearStaged.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Linq;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiStaticNonlinearStaged = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.StaticNonlinearStaged;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Static nonlinear staged construction load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class StaticNonlinearStaged : LoadCase
    {
        #region Fields & Properties
        /// <summary>
        /// The static modal eigen API object.
        /// </summary>
        protected static ApiStaticNonlinearStaged _staticNonlinearStaged = _loadCases?.StaticNonlinearStaged;

        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public InitialCaseHelper InitialCase { get; protected set; }

        /// <summary>
        /// The mass source associated with the load case.
        /// </summary>
        /// <value>The mass source.</value>
        public MassSourceHelper MassSource { get; protected set; }
        
        /// <summary>
        /// Nonlinear settings associated with the load case.
        /// </summary>
        /// <value>The nonlinear settings.</value>
        public NonlinearSettingsHelper<ApiStaticNonlinearStaged> NonlinearSettings { get; protected set; }

        /// <summary>
        /// The saved results associated with the load case.
        /// </summary>
        /// <value>The results saved.</value>
        public StageResultsSaved ResultsSaved { get; protected set; } = new StageResultsSaved();

        /// <summary>
        /// The stage definitions associated with the load case.
        /// </summary>
        /// <value>The stage definitions.</value>
        public StageDefinitions StageDefinitions { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        public new static StaticNonlinearStaged Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (StaticNonlinearStaged)Registry.LoadCases[uniqueName];

            StaticNonlinearStaged loadCase = new StaticNonlinearStaged(uniqueName);
            if (_staticNonlinearStaged != null)
            {
                loadCase.FillData();
            }
            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticNonlinearStaged" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public StaticNonlinearStaged(string name) : base(name)
        {
            InitialCase = new InitialCaseHelper(name, _staticNonlinearStaged);
            MassSource = new MassSourceHelper(name, _staticNonlinearStaged);
            NonlinearSettings = new NonlinearSettingsHelper<ApiStaticNonlinearStaged>(name, _staticNonlinearStaged);
            StageDefinitions = new StageDefinitions(name);
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
            InitialCase.FillInitialCase();
            MassSource.FillMassSource();
            NonlinearSettings.FillData();
            StageDefinitions.FillStageDefinitions();
            StageDefinitions.FillStageOperations();
        }


        // TODO: Work into factory
        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Add(string name)
        {
            _staticNonlinearStaged?.SetCase(name);
            FillData();
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _staticNonlinearStaged?.SetCase(Name);
            FillData();
        }
        #endregion
        
        #region Fill/Set
        /// <summary>
        /// Retrieves the results saved parameters for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillResultsSaved()
        {
            if (_staticNonlinearStaged == null) return;
            _staticNonlinearStaged.GetResultsSaved(Name,
                out var stageSavedOption,
                out var minStepsForInstantanousLoad,
                out var minStepsForTimeDependentItems);

            ResultsSaved = new StageResultsSaved
            {
                StageSavedOption = stageSavedOption,
                MinStepsForInstantanousLoad = minStepsForInstantanousLoad,
                MinStepsForTimeDependentItems = minStepsForTimeDependentItems
            };
        }

        /// <summary>
        /// Sets the results saved parameters for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetResultsSaved(StageResultsSaved resultsSaved)
        {
            _staticNonlinearStaged?.SetResultsSaved(Name,
                resultsSaved.StageSavedOption,
                resultsSaved.MinStepsForInstantanousLoad,
                resultsSaved.MinStepsForTimeDependentItems);
            ResultsSaved = resultsSaved;
        }
        #endregion
    }
}
