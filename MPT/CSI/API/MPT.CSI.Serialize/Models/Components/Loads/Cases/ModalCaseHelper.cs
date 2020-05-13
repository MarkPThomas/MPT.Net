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

using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class ModalCaseHelper.
    /// </summary>
    public class ModalCaseHelper 
    {
        #region Fields & Properties

        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

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
        /// <param name="loadCases">The load cases.</param>
        /// <returns>ModalCaseHelper.</returns>
        internal static ModalCaseHelper Factory(
            string caseName,
            LoadCases loadCases)
        {
            ModalCaseHelper modalCaseHelper = new ModalCaseHelper(caseName, loadCases);
            return modalCaseHelper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalCaseHelper" /> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="loadCases">The load cases.</param>
        private ModalCaseHelper(
            string caseName, 
            LoadCases loadCases) 
        {
            CaseName = caseName;
            _loadCases = loadCases;
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Sets the modal case for the specified analysis case.
        /// If the specified modal case is not actually a modal case, the program automatically replaces it with the first modal case it can find.
        /// If no modal load cases exist, an error is returned.
        /// </summary>
        /// <param name="modalCase">The modal case.</param>
        public void SetModalCase(ModalEigen modalCase)
        {
            _modalCase = modalCase;
        }
        #endregion
    }
}
