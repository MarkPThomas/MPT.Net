// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistory.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Time history load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public abstract class TimeHistory : LoadCase
    {
        #region Fields & Properties        
        /// <summary>
        /// The loads
        /// </summary>
        private LoadsTimeHistory _loads;

        /// <summary>
        /// The loads associated with the load case.
        /// </summary>
        public LoadsTimeHistory Loads
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
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.TimeHistory" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistory(ApiCSiApplication app, Analyzer analyzer, string name) : base(app, analyzer, name)
        {
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillLoads();

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        public abstract void SetLoads(LoadsTimeHistory loads);


        public abstract void FillTimeStep(out int numberOfOutputSteps, out double timeStepSize);


        public abstract void SetTimeStep(int numberOfOutputSteps, double timeStepSize);
#endif  
        #endregion

        #region API Functions
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void fillLoads(ILoadTimeHistory app)
        {
            if (app == null) return;
            app.GetLoads(Name,
                out var loadTypes,
                out var loadNames,
                out var loadFunctions,
                out var scaleFactor,
                out var timeFactor,
                out var arrivalTime,
                out var coordinateSystems,
                out var angles);

            _loads = new LoadsTimeHistory();
            for (int i = 0; i < loadTypes.Length; i++)
            {
                LoadTimeHistory loadTimeHistory = new LoadTimeHistory()
                {
                    LoadType = loadTypes[i],
                    Load = LoadPattern.Factory(_apiApp, loadNames[i]),
                    Function = loadFunctions[i],
                    ScaleFactor = scaleFactor[i],
                    TimeFactor = timeFactor[i],
                    ArrivalTime = arrivalTime[i],
                    CoordinateSystem = coordinateSystems[i],
                    Angle = angles[i],
                };
                Loads.Add(loadTimeHistory);
            }
        }

        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="loads">The loads.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoads(ITimeHistoryModalLinear app, LoadsTimeHistory loads)
        {
            app?.SetLoads(Name,
                loads.Items.Select(x => x.LoadType).ToArray(),
                loads.Items.Select(x => x.Load.Name).ToArray(),
                loads.Items.Select(x => x.Function).ToArray(),
                loads.Items.Select(x => x.ScaleFactor).ToArray(),
                loads.Items.Select(x => x.TimeFactor).ToArray(),
                loads.Items.Select(x => x.ArrivalTime).ToArray(),
                loads.Items.Select(x => x.CoordinateSystem).ToArray(),
                loads.Items.Select(x => x.Angle).ToArray());

            _loads = loads;
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoads(ILoadTimeHistory app, LoadsTimeHistory loads)
        {
            app?.SetLoads(Name,
                loads.Items.Select(x => x.LoadType).ToArray(),
                loads.Items.Select(x => x.Load.Name).ToArray(),
                loads.Items.Select(x => x.Function).ToArray(),
                loads.Items.Select(x => x.ScaleFactor).ToArray(),
                loads.Items.Select(x => x.TimeFactor).ToArray(),
                loads.Items.Select(x => x.ArrivalTime).ToArray(),
                loads.Items.Select(x => x.CoordinateSystem).ToArray(),
                loads.Items.Select(x => x.Angle).ToArray());

            Loads = loads;
        }

        // ========= Time Step =========

        /// <summary>
        /// Returns the time step data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing time history load casee.</param>
        /// <param name="numberOfOutputSteps">The number of output time steps.</param>
        /// <param name="timeStepSize">The output time step size.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void getTimeStep(string name,
            out int numberOfOutputSteps,
            out double timeStepSize)
        {
        // TODO: SAP2000 - getTimeStep
        }


        /// <summary>
        /// Sets the time step data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing time history load casee.</param>
        /// <param name="numberOfOutputSteps">The number of output time steps.</param>
        /// <param name="timeStepSize">The output time step size.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setTimeStep(string name,
            int numberOfOutputSteps,
            double timeStepSize)
        {
        // TODO: SAP2000 - setTimeStep
        }
#endif
        #endregion
    }
}
