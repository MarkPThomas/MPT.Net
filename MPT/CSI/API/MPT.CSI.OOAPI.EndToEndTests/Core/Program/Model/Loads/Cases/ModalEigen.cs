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
using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
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
        protected static ApiModalEigen _modalEigen = _loadCases?.ModalEigen;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public InitialCaseHelper InitialCase { get; protected set; }
#endif
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        public new static ModalEigen Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (ModalEigen)Registry.LoadCases[uniqueName];

            ModalEigen loadCase = new ModalEigen(uniqueName);
            if (_modalEigen != null)
            {
                loadCase.FillData();
            }
            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalEigen" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        public ModalEigen(string name) : base(name)
        {
#if !BUILD_ETABS2016 && !BUILD_ETABS2017
            InitialCase = new InitialCaseHelper(name, _modalEigen);
#endif
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            InitialCase.FillInitialCase();
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
            _modalEigen?.SetCase(name);
            FillData();
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _modalEigen?.SetCase(Name);
            FillData();
        }
        #endregion

        #region Fill/Set
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the number of modes requested for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal load case.</param>
        /// <param name="maxNumberModes">The maximum number of modes requested.</param>
        /// <param name="minNumberModes">The minimum number of modes requested.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetNumberModes(string name,
            ref int maxNumberModes,
            ref int minNumberModes)
        {
            // TODO: SAP2000 complete

        }

        /// <summary>
        /// Sets the number of modes requested for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing modal load case.</param>
        /// <param name="maxNumberModes">The maximum number of modes requested.</param>
        /// <param name="minNumberModes">The minimum number of modes requested.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetNumberModes(string name,
            int maxNumberModes,
            int minNumberModes)
        {

        }


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
        public void GetLoads(string name,
            ref eLoadTypeModal[] loadTypes,
            ref string[] loadNames,
            ref double[] targetMassParticipationRatios,
            ref bool[] isStaticCorrectionModeCalculated)
        {

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
        public void GetParameters(string name,
            ref double eigenvalueShiftFrequency,
            ref double cutoffFrequencyRadius,
            ref double convergenceTolerance,
            ref bool allowAutoFrequencyShifting)
        {

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

        }
#endif
        #endregion
    }
}
