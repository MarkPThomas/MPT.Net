// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="LoadsAppliedHelper.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using LoadPatternTuple = MPT.CSI.Serialize.Models.Helpers.Loads.Definitions.LoadPatternTuple;
using LoadPatternTuples = MPT.CSI.Serialize.Models.Helpers.Loads.Definitions.LoadPatternTuples;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class LoadsAppliedHelper.
    /// </summary>
    public class LoadsAppliedHelper 
    {
        #region Fields & Properties
        /// <summary>
        /// The load patterns
        /// </summary>
        private readonly LoadPatterns _loadPatterns;

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// The list of associated load patterns with their respective types and scale factors.
        /// </summary>
        /// <value>The patterns.</value>
        public LoadPatternTuples Loads { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadsAppliedHelper" /> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loads">The loads.</param>
        internal LoadsAppliedHelper(string caseName, 
            LoadPatterns loadPatterns, 
            LoadPatternTuples loads = null) 
        {
            _loadPatterns = loadPatterns;
            CaseName = caseName;
            Loads = loads;
        }
        #endregion

        /// <summary>
        /// Adds the load data for the analysis case.
        /// This method should be used when adding multiple loads to reduce API calls to the application.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public bool AddLoads(LoadPatternTuples loads)
        {
            return Loads.AddUnique(loads);
        }

        /// <summary>
        /// Removes the load data for the analysis case.
        /// This method should be used when removing multiple loads to reduce API calls to the application.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public void RemoveLoads(LoadPatternTuples loads)
        {
            foreach (var load in loads)
            {
                Loads.Remove(load);
            }
        }

        /// <summary>
        /// Removes the load data for the analysis case by name. Multiple instances will all be removed.
        /// This method should be used when removing multiple loads to reduce API calls to the application.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public void RemoveLoadsByName(string[] loads)
        {
            foreach (var load in loads)
            {
                Loads.Remove(load);
            }
        }

        /// <summary>
        /// Clears the loads.
        /// </summary>
        public void ClearLoads()
        {
            Loads.Clear();
        }

        /// <summary>
        /// Adds the load data for the analysis case.
        /// </summary>
        /// <param name="load">The load.</param>
        public void AddLoad(LoadPatternTuple load)
        {
            if (Loads.Contains(load)) return;
            Loads.Add(load);
        }

        /// <summary>
        /// Removes the load data for the analysis case.
        /// </summary>
        /// <param name="load">The load.</param>
        public void RemoveLoad(LoadPatternTuple load)
        {
            Loads.Remove(load);
        }

        /// <summary>
        /// Removes the load data for the analysis case by name. Multiple instances will all be removed.
        /// </summary>
        /// <param name="load">The load.</param>
        public void RemoveLoadByName(string load)
        {
            Loads.Remove(load);
        }
    }
}
