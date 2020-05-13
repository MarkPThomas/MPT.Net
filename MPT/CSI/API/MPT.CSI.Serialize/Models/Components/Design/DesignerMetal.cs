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

using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Components.Loads;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    /// <summary>
    /// Class DesignerMetal.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Designer{T}" />
    public abstract class DesignerMetal<T> : Designer<T>
        //where T : 
        //    IAutoSection, 
        //    IComboStrength, 
        //    IComboDeflection, 
        //    ITargetPeriod, 
        //    ITargetDisplacement
    {
        #region Fields & Properties
        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;
        

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
            (_loadCombinationsDeflection = new LoadCombinationsDeflection(_loadCombinations));
        

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
            (_targetPeriods = new TargetPeriods(_loadCases));


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
            (_targetDisplacements = new TargetDisplacements());
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignerMetal{T}"/> class.
        /// </summary>
        /// <param name="groups">The groups.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="loadCases">The load cases.</param>
        protected DesignerMetal(
            Groups groups,
            LoadCombinations loadCombinations,
            LoadCases loadCases) : base(groups, loadCombinations)
        {
            _loadCases = loadCases;
        }
        #endregion
        
    }
}
