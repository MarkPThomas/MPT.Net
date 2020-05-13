// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-21-2018
// ***********************************************************************
// <copyright file="Modal.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Base class for modal load cases.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    /// <inheritdoc />
    public abstract class Modal : LoadCase
    {
        #region Fields & Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The maximum number of modes requested.
        /// </summary>
        /// <value>The maximum number of modes.</value>
        public int MaxNumberOfModes { get; protected set; }

        /// <summary>
        /// The minimum number of modes requested.
        /// </summary>
        /// <value>The minimum number of modes.</value>
        public int MinNumberOfModes { get; protected set; }
#endif

        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.Modal" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected Modal(ApiCSiApplication app, Analyzer analyzer, string name) : base(app, analyzer, name)
        {
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillNumberModes();
            FillLoads();
#endif
        }
        #endregion

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the number of modes requested for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillNumberModes();

        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public abstract void FillLoads();
#endif
    }
}
