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

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Components.Loads.Cases;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Represents a load case.
    /// </summary>
    /// <seealso cref="UniqueName" />
    public abstract class LoadCase : UniqueName
    {
        #region Fields & Properties

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
        public eLoadCaseType Type { get; internal set; }
        
        /// <summary>
        /// Gets or sets the type of the sub.
        /// </summary>
        /// <value>The type of the sub.</value>
        public eLoadCaseSubType SubType { get; internal set; }
        
        /// <summary>
        /// Gets or sets the type of the design.
        /// </summary>
        /// <value>The type of the design.</value>
        public eLoadPatternType DesignType { get; internal set; }
        
        /// <summary>
        /// Gets or sets the design type option.
        /// </summary>
        /// <value>The design type option.</value>
        public eSpecificationSource DesignTypeOption { get; internal set; }

        /// <summary>
        /// Gets or sets the bridge type of design.
        /// </summary>
        /// <value>The type of the design.</value>
        public eBridgeDesignAction BridgeDesignType { get; internal set; }

        /// <summary>
        /// Gets or sets the bridge design type option.
        /// </summary>
        /// <value>The design type option.</value>
        public eSpecificationSource BridgeDesignTypeOption { get; internal set; }

        /// <summary>
        /// Gets or sets the automatic created case.
        /// </summary>
        /// <value>The automatic created case.</value>
        public eAutoCreatedCase AutoCreatedCase { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this case is selected for analysis.
        /// </summary>
        /// <value><c>true</c> if this instance is selected for analysis; otherwise, <c>false</c>.</value>
        public bool IsSelectedForAnalysis { get; internal set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }
        #endregion

        #region Initialization        
        /// <summary>
        /// Factories the specified application.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <returns>MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase.</returns>
        internal static LoadCase Factory(
            Analyzer analyzer,
            LoadPatterns loadPatterns,
            LoadCases loadCases,
            string uniqueName,
            eLoadCaseType loadCaseType,
            eLoadCaseSubType loadCaseSubType)
        {
            switch (loadCaseType)
            {
                case eLoadCaseType.LinearStatic:
                    return StaticLinear.Factory(analyzer, loadPatterns, loadCases, uniqueName);

                case eLoadCaseType.NonlinearStatic when loadCaseSubType == eLoadCaseSubType.Nonlinear:
                    return StaticNonlinear.Factory(analyzer, loadPatterns, loadCases, uniqueName);

                case eLoadCaseType.NonlinearStatic when loadCaseSubType == eLoadCaseSubType.NonlinearStagedConstruction:
                    return StaticNonlinearStaged.Factory(analyzer, loadCases, uniqueName);

                case eLoadCaseType.Modal when loadCaseSubType == eLoadCaseSubType.Eigen:
                    return ModalEigen.Factory(analyzer, uniqueName);

                case eLoadCaseType.Modal when loadCaseSubType == eLoadCaseSubType.Ritz:
                    return ModalRitz.Factory(analyzer, uniqueName);

                case eLoadCaseType.ResponseSpectrum:
                    return ResponseSpectrum.Factory(analyzer, loadCases, uniqueName);

                case eLoadCaseType.LinearModalTimeHistory:
                    return TimeHistoryModalLinear.Factory(analyzer, loadCases, uniqueName);

                case eLoadCaseType.NonlinearModalTimeHistory:
                    return TimeHistoryModalNonlinear.Factory(analyzer, loadCases, uniqueName);

                case eLoadCaseType.LinearDirectIntegrationTimeHistory:
                    return TimeHistoryDirectLinear.Factory(analyzer, loadCases, uniqueName);

                case eLoadCaseType.NonlinearDirectIntegrationTimeHistory:
                    return TimeHistoryDirectNonlinear.Factory(analyzer, loadCases, uniqueName);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCase" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        protected LoadCase(Analyzer analyzer, string name) : base(name)
        {
            _analyzer = analyzer;
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
