// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-05-2018
// ***********************************************************************
// <copyright file="ModalEigen.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiModalEigen = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ModalEigen;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Eigen mode load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.Modal" />
    public class ModalEigen : Modal
    {
        #region Fields & Properties
        /// <summary>
        /// The modal Eigen API object.
        /// </summary>
        /// <value>The modal eigen.</value>
        private ApiModalEigen _apiModalEigen => getApiLoadCase(_apiApp).ModalEigen;

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
                _initialCase = new InitialCaseHelper(_apiApp, _analyzer, _apiModalEigen, Name);
                _initialCase.FillInitialCase();

                return _initialCase;
            }
        }

    // Parameters
            ref double eigenvalueShiftFrequency,
            ref double cutoffFrequencyRadius,
            ref double convergenceTolerance,
            ref bool allowAutoFrequencyShifting

        // EigenLoads
            ref eLoadTypeModal[] loadTypes,
            ref string[] loadNames,
            ref double[] targetMassParticipationRatios,
            ref bool[] isStaticCorrectionModeCalculated
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
        internal static ModalEigen Factory(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            ModalEigen loadCase = new ModalEigen(app, analyzer, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalEigen" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected ModalEigen(ApiCSiApplication app, Analyzer analyzer, string name) 
            : base(app, analyzer, name)
        { }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillParameters();
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
        internal static ModalEigen Add(ApiCSiApplication app, Analyzer analyzer, string uniqueName)
        {
            ApiModalEigen apiModalEigen = getApiLoadCase(app).ModalEigen;
            apiModalEigen?.SetCase(uniqueName);
            return Factory(app, analyzer, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiModalEigen?.SetCase(Name);
            FillData();
        }
        #endregion

        #region Fill/Set
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal Eigen load case.</param>
        /// <param name="loadTypes">The load types.</param>
        /// <param name="loadNames">This is an array that includes the name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Accel" />, this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Link" />, this item is not used.</param>
        /// <param name="targetMassParticipationRatios">The target mass participation ratios.</param>
        /// <param name="isStaticCorrectionModeCalculated">True: Static correction modes are to be calculated.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoads(string name,
            ref eLoadTypeModal[] loadTypes,
            ref string[] loadNames,
            ref double[] targetMassParticipationRatios,
            ref bool[] isStaticCorrectionModeCalculated)
        {
            // TODO: SAP2000 - FillLoads
        }

        /// <summary>
        /// Sets the load data for the specified analysis case.
        /// </summary>
        /// <param name="name">The name of an existing modal Eigen load case.</param>
        /// <param name="loadTypes">The load types.</param>
        /// <param name="loadNames">This is an array that includes the name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Accel" />, this item is UX, UY, UZ, RX, RY or RZ, indicating the direction of the load.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadTypeModal.Link" />, this item is not used.</param>
        /// <param name="targetMassParticipationRatios">The target mass participation ratios.</param>
        /// <param name="isStaticCorrectionModeCalculated">True: Static correction modes are to be calculated.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoads(string name,
            eLoadTypeModal[] loadTypes,
            string[] loadNames,
            double[] targetMassParticipationRatios,
            bool[] isStaticCorrectionModeCalculated)
        {
            // TODO: SAP2000 - SetLoads
        }



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
        /// Returns various parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal eigen load case.</param>
        /// <param name="eigenvalueShiftFrequency">The eigenvalue shift frequency. [cyc/s].</param>
        /// <param name="cutoffFrequencyRadius">The eigencutoff frequency radius. [cyc/s].</param>
        /// <param name="convergenceTolerance">The relative convergence tolerance for eigenvalues.</param>
        /// <param name="allowAutoFrequencyShifting">True: Automatic frequency shifting is allowed</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillParameters(string name,
            ref double eigenvalueShiftFrequency,
            ref double cutoffFrequencyRadius,
            ref double convergenceTolerance,
            ref bool allowAutoFrequencyShifting)
        {
            // TODO: SAP2000 - FillParameters
        }

        /// <summary>
        /// Sets various parameters for the specified modal eigen load case.
        /// </summary>
        /// <param name="name">The name of an existing modal eigen load case.</param>
        /// <param name="eigenvalueShiftFrequency">The eigenvalue shift frequency. [cyc/s].</param>
        /// <param name="cutoffFrequencyRadius">The eigencutoff frequency radius. [cyc/s].</param>
        /// <param name="convergenceTolerance">The relative convergence tolerance for eigenvalues.</param>
        /// <param name="allowAutoFrequencyShifting">True: Automatic frequency shifting is allowed.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetParameters(string name,
            double eigenvalueShiftFrequency,
            double cutoffFrequencyRadius,
            double convergenceTolerance,
            bool allowAutoFrequencyShifting)
        {
            // TODO: SAP2000 - SetParameters
        }
#endif
        #endregion
    }
}
