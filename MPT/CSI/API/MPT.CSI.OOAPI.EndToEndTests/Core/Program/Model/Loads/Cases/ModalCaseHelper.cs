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

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Class ModalCaseHelper.
    /// </summary>
    public class ModalCaseHelper
    {
        #region Fields & Properties
        /// <summary>
        /// The observable API object.
        /// </summary>
        protected static IObservableModalCase _appGet;

        /// <summary>
        /// The changeable API object.
        /// </summary>
        protected static IChangeableModalCase _appSet;

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
        public string ModalCase => _modalCase == null ? Constants.None : _modalCase.Name;
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalCaseHelper"/> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="appGet">The application get.</param>
        /// <param name="appSet">The application set.</param>
        public ModalCaseHelper(string caseName, IObservableModalCase appGet, IChangeableModalCase appSet = null)
        {
            CaseName = caseName;
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
            _modalCase = new ModalEigen(_appGet?.GetModalCase(CaseName));
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
