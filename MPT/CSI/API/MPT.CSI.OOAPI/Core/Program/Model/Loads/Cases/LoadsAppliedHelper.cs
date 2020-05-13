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

using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Class LoadsAppliedHelper.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class LoadsAppliedHelper : CSiOoApiBaseBase
    {
        #region Fields & Properties
        /// <summary>
        /// The API object.
        /// </summary>
        private static ILoadStatic _app;

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
        /// The loads
        /// </summary>
        private LoadPatternTuples _loads;
        /// <summary>
        /// The list of associated load patterns with their respective types and scale factors.
        /// </summary>
        /// <value>The patterns.</value>
        public LoadPatternTuples Loads
        {
            get
            {
                if (_loads == null)
                {
                    FillLoads();
                }

                return _loads;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadsAppliedHelper" /> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        /// <param name="app">The application.</param>
        /// <param name="csiApp">The csi application.</param>
        /// <param name="loadPatterns">The load patterns.</param>
        /// <param name="loads">The loads.</param>
        internal LoadsAppliedHelper(string caseName, 
            ILoadStatic app, 
            ApiCSiApplication csiApp, 
            LoadPatterns loadPatterns, 
            LoadPatternTuples loads = null) 
            : base(csiApp)
        {
            _app = app;
            _loadPatterns = loadPatterns;
            CaseName = caseName;
            _loads = loads;
        }
        #endregion



        /// <summary>
        /// Returns the load data for the analysis case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoads()
        {
            if (_app == null) return;
            _app.GetLoads(CaseName,
                out var loadTypes,
                out var loadNames,
                out var scaleFactors);

            _loads = new LoadPatternTuples();
            for (int i = 0; i < loadTypes.Length; i++)
            {
                LoadPatternTuple load = new LoadPatternTuple()
                {
                    Load = _loadPatterns.FillItem(loadNames[i]),
                    ScaleFactor = scaleFactors[i],
                    LoadType = loadTypes[i]
                };
                _loads.Add(load);
            }
        }

        /// <summary>
        /// Adds the load data for the analysis case.
        /// This method should be used when adding multiple loads to reduce API calls to the application.
        /// </summary>
        /// <param name="loads">The loads.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddLoads(LoadPatternTuples loads)
        {
            if (Loads.AddUnique(loads)) setLoads();
        }

        /// <summary>
        /// Removes the load data for the analysis case.
        /// This method should be used when removing multiple loads to reduce API calls to the application.
        /// </summary>
        /// <param name="loads">The loads.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveLoads(LoadPatternTuples loads)
        {
            foreach (var load in loads)
            {
                Loads.Remove(load);
            }
            setLoads();
        }

        /// <summary>
        /// Removes the load data for the analysis case by name. Multiple instances will all be removed.
        /// This method should be used when removing multiple loads to reduce API calls to the application.
        /// </summary>
        /// <param name="loads">The loads.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveLoadsByName(string[] loads)
        {
            foreach (var load in loads)
            {
                Loads.Remove(load);
            }
            setLoads();
        }

        /// <summary>
        /// Clears the loads.
        /// </summary>
        public void ClearLoads()
        {
            Loads.Clear();
            setLoads();
        }

        /// <summary>
        /// Adds the load data for the analysis case.
        /// </summary>
        /// <param name="load">The load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void AddLoad(LoadPatternTuple load)
        {
            if (Loads.Contains(load)) return;
            Loads.Add(load);
            setLoads();
        }

        /// <summary>
        /// Removes the load data for the analysis case.
        /// </summary>
        /// <param name="load">The load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveLoad(LoadPatternTuple load)
        {
            Loads.Remove(load);
            setLoads();
        }

        /// <summary>
        /// Removes the load data for the analysis case by name. Multiple instances will all be removed.
        /// </summary>
        /// <param name="load">The load.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void RemoveLoadByName(string load)
        {
            Loads.Remove(load);
            setLoads();
        }



        /// <summary>
        /// Sets the loads.
        /// </summary>
        protected void setLoads()
        {
            _app?.SetLoads(CaseName,
                Loads.Items.Select(o => o.LoadType).ToArray(),
                Loads.Items.Select(o => o.Load.Name).ToArray(),
                Loads.Items.Select(o => o.ScaleFactor).ToArray());
        }
    }
}
