// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistoryDirectLinear.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiTimeHistory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.TimeHistoryDirectLinear;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Linear direct integration time history load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.TimeHistoryDirectIntegration" />
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class TimeHistoryDirectLinear : TimeHistoryDirectIntegration
    {
        #region Fields & Properties
        /// <summary>
        /// The linear time history API object.
        /// </summary>
        /// <value>The time history direct linear.</value>
        private ApiTimeHistory _apiTimeHistoryDirectLinear => getApiLoadCase(_apiApp).TimeHistoryDirectLinear;
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static TimeHistoryDirectLinear Factory(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            TimeHistoryDirectLinear loadCase = new TimeHistoryDirectLinear(app, analyzer, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryDirectLinear" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryDirectLinear(ApiCSiApplication app, Analyzer analyzer, string name) 
            : base(app, analyzer, name)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            _apiInitialLoadCase = _apiTimeHistoryDirectLinear;
            _apiDampingProportional = _apiTimeHistoryDirectLinear;
#endif
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">The unique name.</param>
        /// <returns>StaticLinear.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static TimeHistoryDirectLinear Add(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            ApiTimeHistory apiTimeHistoryDirectLinear = getApiLoadCase(app).TimeHistoryDirectLinear;
            apiTimeHistoryDirectLinear?.SetCase(uniqueName);
            return Factory(app, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiTimeHistoryDirectLinear?.SetCase(Name);
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
            fillLoads(_apiTimeHistoryDirectLinear);
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
            fillDampingProportional(_apiTimeHistoryDirectLinear);
        }

        public override void SetDampingProportional()
        {
            setDampingProportional(_timeHistoryDirectNonlinear);
        }
#endif
#endregion
    }
}
