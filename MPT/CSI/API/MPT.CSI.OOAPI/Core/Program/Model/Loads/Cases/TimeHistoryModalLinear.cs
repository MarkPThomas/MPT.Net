// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistoryModalLinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiTimeHistory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.TimeHistoryModalLinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Linear modal time history load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.TimeHistoryModal" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class TimeHistoryModalLinear : TimeHistoryModal
    {
        #region Fields & Properties
        /// <summary>
        /// The linear time history API object.
        /// </summary>
        /// <value>The time history modal linear.</value>
        private ApiTimeHistory _apiTimeHistoryModalLinear => getApiLoadCase(_apiApp).TimeHistoryModalLinear;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        
        public eMotionType MotionType { get; protected set; }
#endif
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static TimeHistoryModalLinear Factory(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            TimeHistoryModalLinear loadCase = new TimeHistoryModalLinear(app, analyzer, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryModalLinear" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryModalLinear(ApiCSiApplication app, Analyzer analyzer, string name) 
            : base(app, analyzer, name)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            _apiModal = _apiTimeHistoryModalLinear;
            _apiDampingModal = _apiTimeHistoryModalLinear;
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

        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">The unique name.</param>
        /// <returns>StaticLinear.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static TimeHistoryModalLinear Add(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            ApiTimeHistory apiTimeHistoryModalLinear = getApiLoadCase(app).TimeHistoryModalLinear;
            apiTimeHistoryModalLinear?.SetCase(uniqueName);
            return Factory(app, analyzer, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiTimeHistoryModalLinear?.SetCase(Name);
            FillData();
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        public override void FillLoads()
        {
            fillLoads(_apiTimeHistoryModalLinear);
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
            setLoads(_apiTimeHistoryModalLinear, loads);
        }



#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the motion type for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillMotionType()
        {
            if (_apiTimeHistoryModalLinear == null) return;
            MotionType = _apiTimeHistoryModalLinear.GetMotionType(Name);
        }

        /// <summary>
        /// Sets the motion type for the specified analysis case.
        /// </summary>
        /// <param name="motionType">The time history motion type.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMotionType(eMotionType motionType)
        {
            _apiTimeHistoryModalLinear?.SetMotionType(Name, motionType);
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
