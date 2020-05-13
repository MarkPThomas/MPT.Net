// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="ModalCaseHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Class ModalCaseHelper.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class ModalCaseHelper : CSiOoApiBaseBase
    {
        #region Fields & Properties

        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The observable API object.
        /// </summary>
        private static IObservableModalCase _appGet;

        /// <summary>
        /// The changeable API object.
        /// </summary>
        private static IChangeableModalCase _appSet;

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }


        /// <summary>
        /// The modal case
        /// </summary>
        private Modal _modalCase;
        /// <summary>
        /// This is either None or the name of an existing modal analysis case.
        /// It specifies the modal load case on which any mode-type load assignments to the analysis case are based.
        /// </summary>
        /// <value>The modal case.</value>
        public string ModalCase => _modalCase == null ? Constants.NONE : _modalCase.Name;
        #endregion

        #region Initialization        
        /// <summary>
        /// Factories the specified case name.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="appGet">The application get.</param>
        /// <param name="csiApp">The csi application.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="appSet">The application set.</param>
        /// <returns>ModalCaseHelper.</returns>
        internal static ModalCaseHelper Factory(string caseName,
            IObservableModalCase appGet,
            ApiCSiApplication csiApp,
            LoadCases loadCases,
            IChangeableModalCase appSet = null)
        {
            ModalCaseHelper modalCaseHelper = new ModalCaseHelper(caseName, appGet, csiApp, loadCases, appSet);
            modalCaseHelper.FillModalCase();
            return modalCaseHelper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalCaseHelper" /> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="appGet">The application get.</param>
        /// <param name="csiApp">The csi application.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="appSet">The application set.</param>
        private ModalCaseHelper(
            string caseName, 
            IObservableModalCase appGet, 
            ApiCSiApplication csiApp, 
            LoadCases loadCases,
            IChangeableModalCase appSet = null) : base(csiApp)
        {
            CaseName = caseName;
            _loadCases = loadCases;
            _appGet = appGet;
            if (appSet != null) _appSet = appSet;
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves the modal case assigned to the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillModalCase()
        {
            LoadCase loadCase = _loadCases.FillItem(_appGet?.GetModalCase(CaseName));
            if (loadCase is ModalEigen modalEigen)
            {
                _modalCase = modalEigen;
            }
        }

        /// <summary>
        /// Sets the modal case for the specified analysis case.
        /// If the specified modal case is not actually a modal case, the program automatically replaces it with the first modal case it can find.
        /// If no modal load cases exist, an error is returned.
        /// </summary>
        /// <param name="modalCase">The modal case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModalCase(ModalEigen modalCase)
        {
            _appSet?.SetModalCase(CaseName, ModalCase);
            _modalCase = modalCase;
        }
        #endregion
    }
}
