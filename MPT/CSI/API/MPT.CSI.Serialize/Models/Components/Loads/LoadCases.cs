// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="LoadCases.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Components.Loads.Cases;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Class LoadCases.
    /// </summary>
    public class LoadCases : ObjectLists<LoadCase>
    {
        #region Fields & Properties

        /// <summary>
        /// The load patterns
        /// </summary>
        private readonly LoadPatterns _loadPatterns;

        /// <summary>
        /// Gets the analyzer.
        /// </summary>
        /// <value>The analyzer.</value>
        private readonly Analyzer _analyzer;

        /// <summary>
        /// Gets or sets the type of load case.
        /// </summary>
        /// <value>The type of load case.</value>
        public static eLoadCaseType LoadCaseType { get; set; }

        /// <summary>
        /// Gets or sets the sub-type of load case.
        /// </summary>
        /// <value>The sub-type of load case.</value>
        public static eLoadCaseSubType LoadCaseSubType { get; set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCases" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        internal LoadCases(Analyzer analyzer, LoadPatterns loadPatterns)
        {
            _analyzer = analyzer;
            _loadPatterns = loadPatterns;
        }
        #endregion

        #region Add/Remove

        /// <summary>
        /// Adds the specified unique name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="caseType">Type of the case.</param>
        /// <param name="loadCaseSubType">Type of the load case sub.</param>
        /// <returns>System.Boolean.</returns>
        public bool Add(
            string uniqueName,
            eLoadCaseType caseType,
            eLoadCaseSubType loadCaseSubType = eLoadCaseSubType.None)
        {
            switch (caseType)
            {
                case eLoadCaseType.LinearStatic:
                    return AddStaticLinear(uniqueName);
                case eLoadCaseType.NonlinearStatic when loadCaseSubType == eLoadCaseSubType.Nonlinear:
                    return AddStaticNonlinear(uniqueName);
                case eLoadCaseType.NonlinearStatic when loadCaseSubType == eLoadCaseSubType.NonlinearStagedConstruction:
                    return AddStaticNonlinearStaged(uniqueName);
                case eLoadCaseType.ResponseSpectrum:
                    return AddResponseSpectrum(uniqueName);
                case eLoadCaseType.LinearDirectIntegrationTimeHistory:
                    return AddTimeHistoryDirectLinear(uniqueName);
                case eLoadCaseType.NonlinearDirectIntegrationTimeHistory:
                    return AddTimeHistoryDirectNonlinear(uniqueName);
                case eLoadCaseType.LinearModalTimeHistory:
                    return AddTimeHistoryModalLinear(uniqueName);
                case eLoadCaseType.NonlinearModalTimeHistory:
                    return AddTimeHistoryModalNonlinear(uniqueName);
                case eLoadCaseType.Modal when loadCaseSubType == eLoadCaseSubType.Eigen:
                    return AddModalEigen(uniqueName);
                case eLoadCaseType.Modal when loadCaseSubType == eLoadCaseSubType.Ritz:
                    return AddModalRitz(uniqueName);
                default:
                    return false;
            }
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddStaticLinear(string uniqueName)
        {
            return add(uniqueName, StaticLinear.Factory);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddStaticNonlinear(string uniqueName)
        {
            return add(uniqueName, StaticNonlinear.Factory);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddStaticNonlinearStaged(string uniqueName)
        {
            return add(uniqueName, StaticNonlinearStaged.Factory);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddResponseSpectrum(string uniqueName)
        {
            return add(uniqueName, ResponseSpectrum.Factory);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddTimeHistoryDirectLinear(string uniqueName)
        {
            return add(uniqueName, TimeHistoryDirectLinear.Factory);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddTimeHistoryDirectNonlinear(string uniqueName)
        {
            return add(uniqueName, TimeHistoryDirectNonlinear.Factory);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddTimeHistoryModalLinear(string uniqueName)
        {
            return add(uniqueName, TimeHistoryModalLinear.Factory);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddTimeHistoryModalNonlinear(string uniqueName)
        {
            return add(uniqueName, TimeHistoryModalNonlinear.Factory);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddModalEigen(string uniqueName)
        {
            return add(uniqueName, ModalEigen.Factory);
        }

        /// <summary>
        /// Adds a load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddModalRitz(string uniqueName)
        {
            return add(uniqueName, ModalRitz.Factory);
        }

        /// <summary>
        /// Adds a new load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <param name="adderFactory">The adderFactory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add(
            string uniqueName,
            Func<Analyzer, string, LoadCase> adderFactory)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory(_analyzer, uniqueName));
            return true;
        }

        /// <summary>
        /// Adds a new load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <param name="adderFactory">The adderFactory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add(
            string uniqueName,
            Func<Analyzer, LoadCases, string, LoadCase> adderFactory)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory(_analyzer, this, uniqueName));
            return true;
        }

        /// <summary>
        /// Adds a new load case to the application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique load case.</param>
        /// <param name="adderFactory">The adderFactory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add(
            string uniqueName,
            Func<Analyzer, LoadPatterns, LoadCases, string, LoadCase> adderFactory)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(adderFactory(_analyzer, _loadPatterns, this, uniqueName));
            return true;
        }
        #endregion


        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override LoadCase fillNewItem(string uniqueName)
        {
            return LoadCase.Factory(_analyzer, _loadPatterns, this, uniqueName, LoadCaseType, LoadCaseSubType);
        }
        #endregion
    }
}
