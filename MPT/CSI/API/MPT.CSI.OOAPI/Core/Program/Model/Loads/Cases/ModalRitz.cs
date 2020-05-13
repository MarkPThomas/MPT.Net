// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="ModalRitz.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiModalRitz = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ModalRitz;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Class ModalRitz.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.Modal" />
    public class ModalRitz : Modal
    {
        #region Fields & Properties
        /// <summary>
        /// The modal Ritz API object.
        /// </summary>
        /// <value>The modal ritz.</value>
        private ApiModalRitz _apiModalRitz => getApiLoadCase(_apiApp).ModalRitz;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The initial case
        /// </summary>
        private InitialCaseHelper _initialCase;
        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public InitialCaseHelper InitialCase
        {
            get
            {
                if (_initialCase != null) return _initialCase;
                _initialCase = new InitialCaseHelper(_apiApp, _analyzer, _apiModalRitz, Name);
                _initialCase.FillInitialCase();

                return _initialCase;
            }
        }
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
        internal static ModalRitz Factory(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            ModalRitz loadCase = new ModalRitz(app, analyzer, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalRitz" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected ModalRitz(ApiCSiApplication app, Analyzer analyzer, string name) 
            : base(app, analyzer, name) { }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">The unique name.</param>
        /// <returns>StaticLinear.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static ModalRitz Add(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            ApiStaticLinear apiModalRitz = getApiLoadCase(app).ModalRitz;
            apiModalRitz?.SetCase(uniqueName);
            return Factory(app, analyzer, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiModalRitz?.SetCase(Name);
            FillData();
        }
#endif
        #endregion

        #region Fill/Set
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the number of modes requested for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillNumberModes(string name,
            ref int maxNumberModes,
            ref int minNumberModes)
        {
            // TODO: SAP2000 - FillNumberModes
        }

        /// <summary>
        /// Sets the number of modes requested for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetNumberModes(string name,
            int maxNumberModes,
            int minNumberModes)
        {
            // TODO: SAP2000 - SetNumberModes
        }


        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal Ritz load case.</param>
        /// <param name="loadTypes">The load types.</param>
        /// <param name="loadNames">This is an array that includes the name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Accel" />, this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Link" />, this item is not used.</param>
        /// <param name="maxNumberGenerationCycles">The maximum number generation cycles to be performed for the specified Ritz starting vector.
        /// A value of 0 means there is no limit on the number of cycles.</param>
        /// <param name="targetDynamicParticipationRatio">The target dynamic participation ratio.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoads(string name,
            ref eLoadTypeModal[] loadTypes,
            ref string[] loadNames,
            ref int[] maxNumberGenerationCycles,
            ref double[] targetDynamicParticipationRatio)
        {
            // TODO: SAP2000 - FillLoads
        }

        /// <summary>
        /// Sets the load data for the specified analysis case.
        /// </summary>
        /// <param name="name">The name of an existing modal Ritz load case.</param>
        /// <param name="loadTypes">The load types.</param>
        /// <param name="loadNames">This is an array that includes the name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Accel" />, this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Link" />, this item is not used.</param>
        /// <param name="maxNumberGenerationCycles">The maximum number generation cycles to be performed for the specified Ritz starting vector.
        /// A value of 0 means there is no limit on the number of cycles.</param>
        /// <param name="targetDynamicParticipationRatio">The target dynamic participation ratio.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoads(string name,
            eLoadTypeModal[] loadTypes,
            string[] loadNames,
            int[] maxNumberGenerationCycles,
            double[] targetDynamicParticipationRatio)
        {
            // TODO: SAP2000 - SetLoads
        }
#endif
        #endregion
    }
}
