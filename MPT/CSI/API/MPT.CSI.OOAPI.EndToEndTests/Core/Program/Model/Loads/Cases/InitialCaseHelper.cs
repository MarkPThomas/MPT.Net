using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    public class InitialCaseHelper
    {
        #region Fields & Properties
        /// <summary>
        /// The API object.
        /// </summary>
        protected static IInitialLoadCase _app;

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// The initial load case
        /// </summary>
        private LoadCase _initialLoadCase = null;
        /// <summary>
        /// This is blank, None, or the name of an existing analysis case.
        /// This item specifies if the load case starts from zero initial conditions, that is, an unstressed state, or if it starts using the stiffness that occurs at the end of a nonlinear static or nonlinear direct integration time history load case.
        /// If the specified initial case is a nonlinear static or nonlinear direct integration time history load case, the stiffness at the end of that case is used.
        /// If the initial case is anything else then zero initial conditions are assumed.
        /// </summary>
        /// <value>The initial case.</value>
        public string InitialCase => _initialLoadCase == null ? Constants.None : _initialLoadCase.Name;
        #endregion

        public InitialCaseHelper(string caseName, IInitialLoadCase app)
        {
            _app = app;
            CaseName = caseName;
        }

        /// <summary>
        /// Retrieves the initial condition assumed for the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillInitialCase()
        {
            _initialLoadCase = LoadCase.Factory(_app?.GetInitialCase(CaseName));
        }

        /// <summary>
        /// Sets the initial condition for the load case.
        /// </summary>
        /// <param name="initialCase">The initial case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetInitialCase(LoadCase initialCase)
        {
            _initialLoadCase = initialCase;
            setInitialCase();
        }

        /// <summary>
        /// Removes the initial condition for the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveInitialCase()
        {
            _initialLoadCase = null;
            setInitialCase();
        }

        /// <summary>
        /// Sets the initial case.
        /// </summary>
        protected void setInitialCase()
        {
            _app?.SetInitialCase(CaseName, InitialCase);
        }
    }
}
