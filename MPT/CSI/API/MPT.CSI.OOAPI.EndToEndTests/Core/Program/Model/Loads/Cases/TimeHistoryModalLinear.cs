using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiTimeHistory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.TimeHistoryModalLinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Linear modal time history load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class TimeHistoryModalLinear : TimeHistoryModal
    {
        #region Fields & Properties
        /// <summary>
        /// The linear time history API object.
        /// </summary>
        protected static ApiTimeHistory _timeHistoryModalLinear = _loadCases?.TimeHistoryModalLinear;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        
        public eMotionType MotionType { get; protected set; }
#endif
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        public new static TimeHistoryModalLinear Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (TimeHistoryModalLinear)Registry.LoadCases[uniqueName];

            TimeHistoryModalLinear loadCase = new TimeHistoryModalLinear(uniqueName);
            if (_loadCases != null)
            {
                loadCase.FillData();
            }
            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }

        public TimeHistoryModalLinear(string name) : base(name)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            ModalCase = new ModalCaseHelper(name, _timeHistoryModalLinear, _timeHistoryModalLinear);
            Damping = new DampingHelper(name, _timeHistoryModalLinear);
#endif
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillMotionType();
#endif
        }


        // TODO: Work into factory
        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void Add(string name)
        {
            _timeHistoryModalLinear?.SetCase(name);
            FillData();
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _timeHistoryModalLinear?.SetCase(Name);
            FillData();
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        public override void FillLoads()
        {
            fillLoads(_timeHistoryModalLinear);
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the loads.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public override void SetLoads(LoadsTimeHistory loads)
#else
        /// <summary>
        /// Sets the loads.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public void SetLoads(LoadsTimeHistory loads)
#endif
        {
            setLoads(_timeHistoryModalLinear, loads);
        }



#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the motion type for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillMotionType()
        {
            if (_timeHistoryModalLinear == null) return;
            MotionType = _timeHistoryModalLinear.GetMotionType(Name);
        }

        /// <summary>
        /// Sets the motion type for the specified analysis case.
        /// </summary>
        /// <param name="motionType">The time history motion type.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMotionType(eMotionType motionType)
        {
            _timeHistoryModalLinear?.SetMotionType(Name, motionType);
            MotionType = motionType;
        }
#endif
        #endregion

        #region Damping
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
// TODO: SAP2000 complete time history
#endif
        #endregion
    }
}
