using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiTimeHistory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.TimeHistoryDirectLinear;

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
        /// The linear time history API object.
        /// </summary>
        protected static ApiTimeHistory _timeHistoryDirectLinear = _loadCases?.TimeHistoryDirectLinear;

        /// <summary>
        /// The loads associated with the load case.
        /// </summary>
        public LoadsTimeHistory Loads = new LoadsTimeHistory();
        #endregion

        #region Initialization


        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.TimeHistory" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected TimeHistory(string name) : base(name)
        {
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
            FillLoads();
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


        public abstract void GetTimeStep(out int numberOfOutputSteps, out double timeStepSize);


        public abstract void SetTimeStep(int numberOfOutputSteps, double timeStepSize);
#endif  
        #endregion

        #region API Functions
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
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

            Loads = new LoadsTimeHistory();
            for (int i = 0; i < loadTypes.Length; i++)
            {
                LoadTimeHistory loadTimeHistory = new LoadTimeHistory()
                {
                    LoadType = loadTypes[i],
                    Load = LoadPattern.Factory(loadNames[i]),
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
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoads(ITimeHistoryModalLinear app, LoadsTimeHistory loads)
        {
            app?.SetLoads(Name,
                loads.ToArrayLoadTypes(),
                loads.ToArrayNames(),
                loads.ToArrayFunctions(),
                loads.ToArrayScaleFactors(),
                loads.ToArrayTimeFactor(),
                loads.ToArrayArrivalTime(),
                loads.ToArrayCoordinateSystems(),
                loads.ToArrayAngles());

            Loads = loads;
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
// TODO: SAP2000 complete time history
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        protected void setLoads(ILoadTimeHistory app, LoadsTimeHistory loads)
        {
            app?.SetLoads(Name,
                loads.ToArrayLoadTypes(),
                loads.ToArrayNames(),
                loads.ToArrayFunctions(),
                loads.ToArrayScaleFactors(),
                loads.ToArrayTimeFactor(),
                loads.ToArrayArrivalTime(),
                loads.ToArrayCoordinateSystems(),
                loads.ToArrayAngles());

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

        }
#endif
        #endregion
    }
}
