// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="LoadCase.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;
using ModalEigen = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.ModalEigen;
using ModalRitz = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.ModalRitz;
using StaticLinear = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticLinear;
using StaticNonlinear = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticNonlinear;
using StaticNonlinearStaged = MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.StaticNonlinearStaged;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Represents a load case.
    /// </summary>
    public abstract class LoadCase : CSiOOAPiName
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the load case API object.
        /// </summary>
        /// <value>The load cases.</value>
        protected static LoadCases _loadCases => Registry.ProgramDefinitions?.LoadCases;

        ///// <summary>
        ///// The list of associated load patterns with their respective types and scale factors.
        ///// </summary>
        ///// <value>The patterns.</value>
        //public LoadPatternTuples Loads { get; protected set; }


        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        public eLoadCaseType Type { get; protected set; }


        /// <summary>
        /// Gets or sets the type of the sub.
        /// </summary>
        /// <value>The type of the sub.</value>
        public eLoadCaseSubType SubType { get; protected set; }


        /// <summary>
        /// Gets or sets the type of the design.
        /// </summary>
        /// <value>The type of the design.</value>
        public eLoadPatternType DesignType { get; protected set; }


        /// <summary>
        /// Gets or sets the design type option.
        /// </summary>
        /// <value>The design type option.</value>
        public eSpecificationSource DesignTypeOption { get; protected set; }


        /// <summary>
        /// Gets or sets the automatic created case.
        /// </summary>
        /// <value>The automatic created case.</value>
        public eAutoCreatedCase AutoCreatedCase { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this case is selected for analysis.
        /// </summary>
        /// <value><c>true</c> if this instance is selected for analysis; otherwise, <c>false</c>.</value>
        public bool IsSelectedForAnalysis { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load combination name.</param>
        /// <returns>Steel.</returns>
        public static LoadCase Factory(string uniqueName)
        {
            if (Registry.LoadCases.Keys.Contains(uniqueName)) return Registry.LoadCases[uniqueName];

            Tuple<eLoadCaseType, eLoadCaseSubType> loadCaseType = GetCaseTypes(uniqueName);
            LoadCase loadCase;
            switch (loadCaseType.Item1)
            {
                case eLoadCaseType.LinearStatic:
                    loadCase = StaticLinear.Factory(uniqueName);
                    break;
                case eLoadCaseType.NonlinearStatic when loadCaseType.Item2 == eLoadCaseSubType.Nonlinear:
                    loadCase = StaticNonlinear.Factory(uniqueName);
                    break;
                case eLoadCaseType.NonlinearStatic when loadCaseType.Item2 == eLoadCaseSubType.NonlinearStagedConstruction:
                    loadCase = StaticNonlinearStaged.Factory(uniqueName);
                    break;
                case eLoadCaseType.Modal when loadCaseType.Item2 == eLoadCaseSubType.Eigen:
                    loadCase = ModalEigen.Factory(uniqueName);
                    break;
                case eLoadCaseType.Modal when loadCaseType.Item2 == eLoadCaseSubType.Ritz:
                    loadCase = ModalRitz.Factory(uniqueName);
                    break;
                case eLoadCaseType.ResponseSpectrum:
                    loadCase = ResponseSpectrum.Factory(uniqueName);
                    break;
                case eLoadCaseType.LinearModalTimeHistory:
                    loadCase = TimeHistoryModalLinear.Factory(uniqueName);
                    break;
                case eLoadCaseType.NonlinearModalTimeHistory:
                    loadCase = TimeHistoryModalNonlinear.Factory(uniqueName);
                    break;
                case eLoadCaseType.LinearDirectIntegrationTimeHistory:
                    loadCase = TimeHistoryDirectLinear.Factory(uniqueName);
                    break;
                case eLoadCaseType.NonlinearDirectIntegrationTimeHistory:
                    loadCase = TimeHistoryDirectNonlinear.Factory(uniqueName);
                    break;
                default:
                    return null;
            }

            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCase" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected LoadCase(string name) : base(name) { }

        /// <summary>
        /// Fills the data.
        /// </summary>
        public override void FillData()
        {
            FillCaseTypes();
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Gets the case types.
        /// </summary>
        public void FillCaseTypes()
        {
            if (_loadCases == null) return;
            _loadCases.GetCaseTypes(Name,
                out var loadCaseType,
                out var loadCaseSubType,
                out var designType,
                out var designTypeOption,
                out var autoCreatedCase);

            Type = loadCaseType;
            SubType = GetCaseSubTypes(loadCaseType, loadCaseSubType);
            DesignType = designType;
            DesignTypeOption = designTypeOption;
            AutoCreatedCase = autoCreatedCase;
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
            if (_loadCases == null) return;
            _loadCases.SetDesignType(Name, designTypeOption, designType);
            DesignType = designType;
            DesignTypeOption = designTypeOption;
        }
        #endregion

        #region CRUD
        /// <summary>
        /// Selects for analysis.
        /// </summary>
        public void SelectForAnalysis()
        {
            IsSelectedForAnalysis = true;
            Registry.Analyzer.SetCaseSelectedForOutput(this);
        }

        /// <summary>
        /// Deselects for analysis.
        /// </summary>
        public void DeselectForAnalysis()
        {
            IsSelectedForAnalysis = false;
            Registry.Analyzer.SetCaseSelectedForOutput(this);
        }


        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        public override void ChangeName(string newName)
        {
            _loadCases?.ChangeName(Name, newName);
            Name = newName;
        }

        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        public override void Delete()
        {
            _loadCases?.Delete(Name);
        }
        #endregion

        #region Methods: Static
        /// <summary>
        /// Gets all load cases.
        /// </summary>
        /// <returns>List&lt;LoadPattern&gt;.</returns>
        public static List<LoadCase> GetAll()
        {
            List<LoadCase> objects = new List<LoadCase>();
            List<string> names = GetNameList();
            foreach (var name in names)
            {
                LoadCase loadCase = Factory(name);
                if (loadCase == null) continue;

                objects.Add(loadCase);
            }

            return objects;
        }


        /// <summary>
        /// Returns the names of all items.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetNameList()
        {
            return new List<string>(_loadCases.GetNameList());
        }

        /// <summary>
        /// Returns the names of all defined load cases of the specified type.
        /// </summary>
        /// <param name="caseType">Load case type for which names are desired.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetNameList(eLoadCaseType caseType)
        {
            return new List<string>(_loadCases.GetNameList(caseType));
        }

        /// <summary>
        /// Gets the case types.
        /// </summary>
        public static Tuple<eLoadCaseType, eLoadCaseSubType> GetCaseTypes(string name)
        {
            _loadCases.GetCaseTypes(name,
                out var loadCaseType,
                out var loadCaseSubType,
                out var designType,
                out var designTypeOption,
                out var autoCreatedCase);

            return new Tuple<eLoadCaseType, eLoadCaseSubType>(
                loadCaseType,
                GetCaseSubTypes(loadCaseType, loadCaseSubType));
        }
        #endregion

        #region Methods: Protected

        protected static eLoadCaseSubType GetCaseSubTypes(eLoadCaseType type, int subType)
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

        protected static int GetCaseSubTypes(eLoadCaseType type, eLoadCaseSubType subType)
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
