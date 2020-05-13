using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiTimeHistory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.TimeHistoryDirectLinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Linear direct integration time history load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class TimeHistoryDirectLinear : TimeHistoryDirectIntegration
    {
        #region Fields & Properties
        /// <summary>
        /// The linear time history API object.
        /// </summary>
        protected static ApiTimeHistory _timeHistoryDirectLinear = _loadCases?.TimeHistoryDirectLinear;



        
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        public new static TimeHistoryDirectLinear Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (TimeHistoryDirectLinear)Registry.LoadCases[uniqueName];

            TimeHistoryDirectLinear loadCase = new TimeHistoryDirectLinear(uniqueName);
            if (_loadCases != null)
            {
                loadCase.FillData();
            }
            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }

        public TimeHistoryDirectLinear(string name) : base(name)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            InitialCase = new InitialCaseHelper(name, _timeHistoryDirectLinear);
#endif
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        // TODO: Work into factory
        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Add(string name)
        {
            _timeHistoryDirectLinear?.SetCase(name);
            FillData();
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _timeHistoryDirectLinear?.SetCase(Name);
            FillData();
        }
#endif
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        public override void FillLoads()
        {
            fillLoads(_timeHistoryDirectLinear);
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the loads.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public override void SetLoads(LoadsTimeHistory loads)
        {
        }

        public override void FillDampingProportional()
        {
            fillDampingProportional(_timeHistoryDirectLinear);
        }

        public override void SetDampingProportional()
        {
            setDampingProportional(_timeHistoryDirectNonlinear);
        }
#endif
        #endregion



#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

#endif
    }
}
