using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    public class LoadsAppliedHelper
    {
        #region Fields & Properties
        /// <summary>
        /// The API object.
        /// </summary>
        protected static ILoadStatic _app;

        /// <summary>
        /// The name of the associated load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; set; }

        /// <summary>
        /// The list of associated load patterns with their respective types and scale factors.
        /// </summary>
        /// <value>The patterns.</value>
        public LoadPatternTuples Loads { get; protected set; }
        #endregion

        public LoadsAppliedHelper(string caseName, ILoadStatic app, LoadPatternTuples loads = null)
        {
            _app = app;
            CaseName = caseName;
            if (loads == null) Loads = new LoadPatternTuples();
        }

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

            for (int i = 0; i < loadTypes.Length; i++)
            {
                LoadPatternTuple load = new LoadPatternTuple()
                {
                    Load = LoadPattern.Factory(loadNames[i]),
                    ScaleFactor = scaleFactors[i],
                    LoadType = loadTypes[i]
                };
                Loads.Add(load);
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
                Loads.ToArrayLoadTypes(),
                Loads.ToArrayNames(),
                Loads.ToArrayScaleFactors());
        }
    }
}
