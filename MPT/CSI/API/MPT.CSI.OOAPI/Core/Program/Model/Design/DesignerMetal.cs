// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-21-2018
// ***********************************************************************
// <copyright file="DesignerMetal.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    /// <summary>
    /// Class DesignerMetal.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Design.Designer{T}" />
    public abstract class DesignerMetal<T> : Designer<T>
        where T : 
            IAutoSection, 
            IComboStrength, 
            IComboDeflection, 
            ITargetPeriod, 
            ITargetDisplacement
    {
        #region Fields & Properties
        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;


        /// <summary>
        /// The API combo deflection
        /// </summary>
        private readonly IComboDeflection _apiComboDeflection;
        /// <summary>
        /// The load combinations deflection
        /// </summary>
        private LoadCombinationsDeflection _loadCombinationsDeflection;
        /// <summary>
        /// Gets the load combinations deflection.
        /// </summary>
        /// <value>The load combinations deflection.</value>
        public LoadCombinationsDeflection LoadCombinationsDeflection =>
            _loadCombinationsDeflection ??
            (_loadCombinationsDeflection = new LoadCombinationsDeflection(_apiComboDeflection, _loadCombinations));

        /// <summary>
        /// The API target period
        /// </summary>
        private readonly ITargetPeriod _apiTargetPeriod;
        /// <summary>
        /// The target periods
        /// </summary>
        private TargetPeriods _targetPeriods;
        /// <summary>
        /// Gets the target periods.
        /// </summary>
        /// <value>The target periods.</value>
        public TargetPeriods TargetPeriods =>
            _targetPeriods ??
            (_targetPeriods = new TargetPeriods(_apiTargetPeriod, _loadCases));

        /// <summary>
        /// The API target displacement
        /// </summary>
        private readonly ITargetDisplacement _apiTargetDisplacement;
        /// <summary>
        /// The target displacements
        /// </summary>
        private TargetDisplacements _targetDisplacements;
        /// <summary>
        /// Gets the target displacements.
        /// </summary>
        /// <value>The target displacements.</value>
        public TargetDisplacements TargetDisplacements =>
            _targetDisplacements ??
            (_targetDisplacements = new TargetDisplacements(_apiTargetDisplacement));
        #endregion

        #region Initialization


        /// <summary>
        /// Initializes a new instance of the <see cref="DesignerMetal{T}"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="groups">The groups.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="apiObject">The API object.</param>
        protected DesignerMetal(
            ApiCSiApplication app,
            Groups groups,
            LoadCombinations loadCombinations,
            LoadCases loadCases,
            T apiObject) : base(app, groups, loadCombinations, apiObject)
        {
            _loadCases = loadCases;
            _apiComboDeflection = apiObject;
            _apiTargetPeriod = apiObject;
            _apiTargetDisplacement = apiObject;
        }
        #endregion
        
    }
}
