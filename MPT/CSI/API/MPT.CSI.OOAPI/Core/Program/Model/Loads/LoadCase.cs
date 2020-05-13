// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-21-2018
// ***********************************************************************
// <copyright file="LoadCase.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases;
using MPT.CSI.OOAPI.Core.Support;
using ModalEigen = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.ModalEigen;
using ModalRitz = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.ModalRitz;
using StaticLinear = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticLinear;
using StaticNonlinear = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticNonlinear;
using StaticNonlinearStaged = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticNonlinearStaged;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiLoadCase = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCases;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Represents a load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOOAPiName" />
    public abstract class LoadCase : CSiOOAPiName
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the load case API object.
        /// </summary>
        /// <value>The load cases.</value>
        protected ApiLoadCase _apiLoadCases => getApiLoadCase(_apiApp);

        /// <summary>
        /// The analyzer
        /// </summary>
        protected Analyzer _analyzer;


        /// <summary>
        /// The type
        /// </summary>
        private eLoadCaseType _type;

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public eLoadCaseType Type
        {
            get
            {
                if (_type == 0)
                {
                    FillCaseTypes();
                }

                return _type;
            }
        }


        /// <summary>
        /// The sub type
        /// </summary>
        private eLoadCaseSubType _subType;
        /// <summary>
        /// Gets or sets the type of the sub.
        /// </summary>
        /// <value>The type of the sub.</value>
        public eLoadCaseSubType SubType
        {
            get
            {
                if (_subType == 0)
                {
                    FillCaseTypes();
                }

                return _subType;
            }
        }


        /// <summary>
        /// The design type
        /// </summary>
        private eLoadPatternType _designType;
        /// <summary>
        /// Gets or sets the type of the design.
        /// </summary>
        /// <value>The type of the design.</value>
        public eLoadPatternType DesignType
        {
            get
            {
                if (_designType == 0)
                {
                    FillCaseTypes();
                }

                return _designType;
            }
        }


        /// <summary>
        /// The design type option
        /// </summary>
        private eSpecificationSource _designTypeOption;
        /// <summary>
        /// Gets or sets the design type option.
        /// </summary>
        /// <value>The design type option.</value>
        public eSpecificationSource DesignTypeOption
        {
            get
            {
                if (_designTypeOption == 0)
                {
                    FillCaseTypes();
                }

                return _designTypeOption;
            }
        }


        /// <summary>
        /// The automatic created case
        /// </summary>
        private eAutoCreatedCase _autoCreatedCase;
        /// <summary>
        /// Gets or sets the automatic created case.
        /// </summary>
        /// <value>The automatic created case.</value>
        public eAutoCreatedCase AutoCreatedCase
        {
            get
            {
                if (_autoCreatedCase == 0)
                {
                    FillCaseTypes();
                }

                return _autoCreatedCase;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this case is selected for analysis.
        /// </summary>
        /// <value><c>true</c> if this instance is selected for analysis; otherwise, <c>false</c>.</value>
        public bool IsSelectedForAnalysis { get; protected set; }
        #endregion

        #region Initialization        
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase.</returns>
        internal static LoadCase Factory(
            ApiCSiApplication app,
            Analyzer analyzer,
            LoadPatterns loadPatterns,
            LoadCases loadCases,
            string uniqueName)
        {
            Tuple<eLoadCaseType, eLoadCaseSubType> loadCaseType = GetCaseTypes(app?.Model?.Definitions?.LoadCases, uniqueName);
            switch (loadCaseType.Item1)
            {
                case eLoadCaseType.LinearStatic:
                    return StaticLinear.Factory(app, analyzer, loadPatterns, loadCases, uniqueName);

                case eLoadCaseType.NonlinearStatic when loadCaseType.Item2 == eLoadCaseSubType.Nonlinear:
                    return StaticNonlinear.Factory(app, analyzer, loadPatterns, loadCases, uniqueName);

                case eLoadCaseType.NonlinearStatic when loadCaseType.Item2 == eLoadCaseSubType.NonlinearStagedConstruction:
                    return StaticNonlinearStaged.Factory(app, analyzer, loadCases, uniqueName);

                case eLoadCaseType.Modal when loadCaseType.Item2 == eLoadCaseSubType.Eigen:
                    return ModalEigen.Factory(app, analyzer, uniqueName);

                case eLoadCaseType.Modal when loadCaseType.Item2 == eLoadCaseSubType.Ritz:
                    return ModalRitz.Factory(app, analyzer, uniqueName);

                case eLoadCaseType.ResponseSpectrum:
                    return ResponseSpectrum.Factory(app, analyzer, loadCases, uniqueName);

                case eLoadCaseType.LinearModalTimeHistory:
                    return TimeHistoryModalLinear.Factory(app, analyzer, uniqueName);

                case eLoadCaseType.NonlinearModalTimeHistory:
                    return TimeHistoryModalNonlinear.Factory(app, analyzer, uniqueName);

                case eLoadCaseType.LinearDirectIntegrationTimeHistory:
                    return TimeHistoryDirectLinear.Factory(app, analyzer, uniqueName);

                case eLoadCaseType.NonlinearDirectIntegrationTimeHistory:
                    return TimeHistoryDirectNonlinear.Factory(app, analyzer, uniqueName);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCase" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        protected LoadCase(ApiCSiApplication app, Analyzer analyzer, string name) : base(app, name)
        {
            _analyzer = analyzer;
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            // Does nothing
        }
        
        #endregion

        #region Fill/Set
        /// <summary>
        /// Gets the case types.
        /// </summary>
        public void FillCaseTypes()
        {
            if (_apiLoadCases == null) return;
            _apiLoadCases.GetCaseTypes(Name,
                out var loadCaseType,
                out var loadCaseSubType,
                out var designType,
                out var designTypeOption,
                out var autoCreatedCase);

            _type = loadCaseType;
            _subType = getCaseSubTypes(loadCaseType, loadCaseSubType);
            _designType = designType;
            _designTypeOption = designTypeOption;
            _autoCreatedCase = autoCreatedCase;
        }

        /// <summary>
        /// Sets the design type for the specified load case.
        /// </summary>
        /// <param name="designTypeOption">This is one of the options for <paramref name="designType" />.</param>
        /// <param name="designType">This item only applies when the <paramref name="designTypeOption" /> = <see cref="eSpecificationSource.UserSpecified"/>.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDesignType(
            eSpecificationSource designTypeOption,
            eLoadPatternType designType = eLoadPatternType.Dead)
        {
            if (_apiLoadCases == null) return;
            _apiLoadCases.SetDesignType(Name, designTypeOption, designType);
            _designType = designType;
            _designTypeOption = designTypeOption;
        }
        #endregion

        #region CRUD
        /// <summary>
        /// Selects for analysis.
        /// </summary>
        public void SelectForAnalysis()
        {
            IsSelectedForAnalysis = true;
            _analyzer.SetCaseSelectedForOutput(this);
        }

        /// <summary>
        /// Deselects for analysis.
        /// </summary>
        public void DeselectForAnalysis()
        {
            IsSelectedForAnalysis = false;
            _analyzer.SetCaseSelectedForOutput(this);
        }




        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        public override void ChangeName(string newName)
        {
            _apiLoadCases?.ChangeName(Name, newName);
            Name = newName;
        }



        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        internal override void Delete()
        {
            _apiLoadCases?.Delete(Name);
        }
        #endregion

        #region Methods: Static
        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        internal static List<string> GetNameList(ApiLoadCase loadcases)
        {
            return new List<string>(loadcases.GetNameList());
        }

        /// <summary>
        /// Returns the names of all defined load cases of the specified type.
        /// </summary>
        /// <param name="caseType">Load case type for which names are desired.</param>
        /// <param name="loadcases">The loadcases.</param>
        /// <returns>System.Collections.Generic.List&lt;System.String&gt;.</returns>
        internal static List<string> GetNameList(ApiLoadCase loadcases, eLoadCaseType caseType)
        {
            return new List<string>(loadcases.GetNameList(caseType));
        }

        /// <summary>
        /// Gets the case types.
        /// </summary>
        public static Tuple<eLoadCaseType, eLoadCaseSubType> GetCaseTypes(ApiLoadCase loadcases, string name)
        {
            loadcases.GetCaseTypes(name,
                out var loadCaseType,
                out var loadCaseSubType,
                out var designType,
                out var designTypeOption,
                out var autoCreatedCase);

            return new Tuple<eLoadCaseType, eLoadCaseSubType>(
                loadCaseType,
                getCaseSubTypes(loadCaseType, loadCaseSubType));
        }
        #endregion

        #region Methods: Protected        
        /// <summary>
        /// Gets the case sub types.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="subType">Type of the sub.</param>
        /// <returns>MPT.CSI.API.Core.Program.ModelBehavior.Definition.eLoadCaseSubType.</returns>
        protected static eLoadCaseSubType getCaseSubTypes(eLoadCaseType type, int subType)
        {
            switch (type)
            {
                case eLoadCaseType.NonlinearStatic:
                    switch (subType)
                    {
                        case 1:
                            return eLoadCaseSubType.Nonlinear;
                        case 2:
                            return eLoadCaseSubType.NonlinearStagedConstruction;
                        default:
                            return eLoadCaseSubType.Error;
                    }
                case eLoadCaseType.Modal:
                    switch (subType)
                    {
                        case 1:
                            return eLoadCaseSubType.Eigen;
                        case 2:
                            return eLoadCaseSubType.Ritz;
                        default:
                            return eLoadCaseSubType.Error;
                    }
                case eLoadCaseType.LinearModalTimeHistory:
                    switch (subType)
                    {
                        case 1:
                            return eLoadCaseSubType.Transient;
                        case 2:
                            return eLoadCaseSubType.Periodic;
                        default:
                            return eLoadCaseSubType.Error;
                    }
                default:
                    return eLoadCaseSubType.Error;
            }

        }

        /// <summary>
        /// Gets the case sub types.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="subType">Type of the sub.</param>
        /// <returns>System.Int32.</returns>
        protected static int getCaseSubTypes(eLoadCaseType type, eLoadCaseSubType subType)
        {
            switch (type)
            {
                case eLoadCaseType.NonlinearStatic:
                    switch (subType)
                    {
                        case eLoadCaseSubType.Nonlinear:
                            return 1;
                        case eLoadCaseSubType.NonlinearStagedConstruction:
                            return 2;
                        default:
                            return 0;
                    }
                case eLoadCaseType.Modal:
                    switch (subType)
                    {
                        case eLoadCaseSubType.Eigen:
                            return 1;
                        case eLoadCaseSubType.Ritz:
                            return 2;
                        default:
                            return 0;
                    }
                case eLoadCaseType.LinearModalTimeHistory:
                    switch (subType)
                    {
                        case eLoadCaseSubType.Transient:
                            return 1;
                        case eLoadCaseSubType.Periodic:
                            return 2;
                        default:
                            return 0;
                    }
                default:
                    return 0;
            }
        }
        #endregion
    }
}
