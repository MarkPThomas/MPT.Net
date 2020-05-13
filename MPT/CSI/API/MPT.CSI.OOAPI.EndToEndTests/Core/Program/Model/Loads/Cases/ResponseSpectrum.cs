// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="ResponseSpectrum.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ApiResponseSpectrum = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ResponseSpectrum;
using OOApiDiaphragm = MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions.Diaphragm;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Response spectrum load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    /// <inheritdoc />
    /// <seealso cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public class ResponseSpectrum : LoadCase
    {
        #region Fields & Properties
        /// <summary>
        /// The response spectrum API object.
        /// </summary>
        protected static ApiResponseSpectrum _responseSpectrum = _loadCases?.ResponseSpectrum;

        /// <summary>
        /// The list of associated load functions with their respective scale factors and other properties.
        /// </summary>
        /// <value>The patterns.</value>
        public List<LoadResponseSpectrum> Loads { get; protected set; } = new List<LoadResponseSpectrum>();

        /// <summary>
        /// The modal case associated with the load case.
        /// </summary>
        /// <value>The modal case.</value>
        public ModalCaseHelper ModalCase { get; protected set; }

        /// <summary>
        /// Gets or sets the direction combination.
        /// </summary>
        /// <value>The direction combination.</value>
        public DirectionCombination DirectionCombination { get; protected set; }

        /// <summary>
        /// Gets or sets the modal combination.
        /// </summary>
        /// <value>The modal combination.</value>
        public ModalCombination ModalCombination { get; protected set; }

        /// <summary>
        /// Eccentricity ratio that applies to all diaphragms for the load case.
        /// </summary>
        /// <value>The eccentricity.</value>
        public double Eccentricity { get; protected set; }

        /// <summary>
        /// The diaphragm eccentricity overwrites of diaphragm, overwrite.
        /// </summary>
        /// <value>The diaphragm eccentricity overwrites.</value>
        public List<DiaphragmEccentricityOverride> DiaphragmEccentricityOverwrites { get; protected set; } = new List<DiaphragmEccentricityOverride>();

        /// <summary>
        /// The damping associate with the load case.
        /// </summary>
        /// <value>The damping.</value>
        public DampingHelper Damping { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        public new static ResponseSpectrum Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (ResponseSpectrum)Registry.LoadCases[uniqueName];

            ResponseSpectrum loadCase = new ResponseSpectrum(uniqueName);
            if (_responseSpectrum != null)
            {
                loadCase.FillData();
            }
            Registry.LoadCases.Add(uniqueName, loadCase);
            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.ResponseSpectrum" /> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        public ResponseSpectrum(string name) : base(name)
        {
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            ModalCase = new ModalCaseHelper(name, _responseSpectrum, _responseSpectrum);
#else
            ModalCase = new ModalCaseHelper(name, _responseSpectrum);
#endif
            Damping = new DampingHelper(name, _responseSpectrum);
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
            ModalCase.FillModalCase();
            Damping.FillData();
            FillLoads();
            FillModalCombination();
            FillDirectionalCombination();
            FillEccentricity();
            FillDiaphragmEccentricityOverride();
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
            _responseSpectrum?.SetCase(name);
            FillData();
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _responseSpectrum?.SetCase(Name);
            FillData();
        }
#endregion

        #region Fill/Set
        /// <summary>
        /// Returns the load data for the load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillLoads()
        {
            if (_responseSpectrum == null) return;
            _responseSpectrum.GetLoads(Name,
                out var loadDirections,
                out var functions,
                out var scaleFactor,
                out var coordinateSystems,
                out var angles);

            for (int i = 0; i < loadDirections.Length; i++)
            {
                LoadResponseSpectrum loadFunction = new LoadResponseSpectrum()
                {
                    Function = functions[i],
                    ScaleFactor = scaleFactor[i],
                    CoordinateSystem = coordinateSystems[i],
                    Angle = angles[i],
                    Direction = loadDirections[i]
                };
                Loads.Add(loadFunction);
            }
        }

        /// <summary>
        /// Returns the modal combination option assigned to the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillModalCombination()
        {
            if (_responseSpectrum == null) return;
            _responseSpectrum.GetModalCombination(Name,
                out var modalCombination,
                out var gmcF1,
                out var gmcF2,
                out var periodicPlusRigidModalCombination,
                out var td);

            ModalCombination = new ModalCombination()
            {
                Combination = modalCombination,
                GmcF1 = gmcF1,
                GmcF2 = gmcF2,
                PeriodicPlusRigidModalCombination = periodicPlusRigidModalCombination,
                Td = td
            };
        }


        /// <summary>
        /// Returns the directional combination option assigned to the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDirectionalCombination()
        {
            if (_responseSpectrum == null) return;
            _responseSpectrum.GetDirectionalCombination(Name,
                out var directionalCombination,
                out var scaleFactor);

            DirectionCombination = new DirectionCombination()
            {
                DirectionalCombination = directionalCombination,
                ScaleFactor = scaleFactor
            };
        }


        /// <summary>
        /// Returns the eccentricity ratio that applies to all diaphragms for the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillEccentricity()
        {
            if (_responseSpectrum == null) return;
            Eccentricity = _responseSpectrum.GetEccentricity(Name);
        }


        /// <summary>
        /// Returns the diaphragm eccentricity overrides for a load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDiaphragmEccentricityOverride()
        {
            if (_responseSpectrum == null) return;
            _responseSpectrum.GetDiaphragmEccentricityOverride(Name,
                out var diaphragms,
                out var eccentricities);
            
            for (int i = 0; i < diaphragms.Length; i++)
            {
                DiaphragmEccentricityOverride diaphragmOverwrite = new DiaphragmEccentricityOverride()
                {
                        Diaphragm = OOApiDiaphragm.Factory(diaphragms[i]),
                        Eccentricity = eccentricities[i]
                };
                DiaphragmEccentricityOverwrites.Add(diaphragmOverwrite);
            }
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Sets the load data for the specified analysis case.
        /// </summary>
        /// <param name="loadDirections">U1, U2, U3, R1, R2 or R3, indicating the direction of each load.</param>
        /// <param name="functions">The name of the steady state function associated with each load.</param>
        /// <param name="scaleFactor">The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.</param>
        /// <param name="coordinateSystems">This is an array that includes the name of the coordinate system associated with each load.
        /// If this item is a blank string, the Global coordinate system is assumed.</param>
        /// <param name="angles">This is an array that includes the angle between the acceleration local 1 axis and the +X-axis of the coordinate system specified by <paramref name="coordinateSystems" />.
        /// The rotation is about the Z-axis of the specified coordinate system. [deg].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoads(
            eDegreeOfFreedom[] loadDirections,
            string[] functions,
            double[] scaleFactor,
            string[] coordinateSystems,
            double[] angles)
        {
            // TODO: Finish for SAP2000
        }

        /// <summary>
        /// Sets the modal case for the specified analysis case.
        /// If the specified modal case is not actually a modal case, the program automatically replaces it with the first modal case it can find.
        /// If no modal load cases exist, an error is returned.
        /// TODO: Handle this.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="modalCase">This is either None or the name of an existing modal analysis case.
        /// It specifies the modal load case on which any mode-type load assignments to the specified load case are based.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModalCase(string modalCase)
        {

        }

        /// <summary>
        /// Returns the modal combination option assigned to the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="modalCombination">The modal combination option.</param>
        /// <param name="gmcF1">The GMC f1 factor [cyc/s].
        /// This item does not apply when <paramref name="modalCombination" /> = <see cref="eModalCombination.ABS" />.</param>
        /// <param name="gmcF2">The GMC f2 factor [cyc/s].
        /// This item does not apply when <paramref name="modalCombination" /> = <see cref="eModalCombination.ABS" />.</param>
        /// <param name="periodicPlusRigidModalCombination">The periodic plus rigid modal combination option.</param>
        /// <param name="td">The factor td [s].
        /// This item applies only when <paramref name="modalCombination" /> = <see cref="eModalCombination.DoubleSum" />.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetModalCombination(string name,
            eModalCombination modalCombination,
            double gmcF1,
            double gmcF2,
            ePeriodicPlusRigidModalCombination periodicPlusRigidModalCombination,
            double td)
        {

        }

        /// <summary>
        /// Sets the directional combination option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="directionalCombination">The directional combination option.</param>
        /// <param name="scaleFactor">The abslute value scale factor.
        /// This item applies only when <paramref name="directionalCombination" /> = <see cref="eDirectionalCombination.ABS" /></param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDirectionalCombination(string name,
            eDirectionalCombination directionalCombination,
            double scaleFactor)
        {

        }

        /// <summary>
        /// Sets the eccentricity ratio that applies to all diaphragms for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="eccentricity">The eccentricity ratio that applies to all diaphragms.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetEccentricity(string name,
            double eccentricity)
        {

        }

        /// <summary>
        /// Sets the eccentricity ratio that applies to all diaphragms for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="diaphragm">The name of an existing special rigid diaphragm constraint, that is, a diaphragm constraint with the following features:
        /// 1. The constraint type is <see cref="eConstraintType.Diaphragm" />;
        /// 2. The constraint coordinate system is <see cref="CoordinateSystems.Global" />;
        /// 3. The constraint axis is Z;</param>
        /// <param name="eccentricities">The eccentricity applied to each diaphragm. [L].</param>
        /// <param name="delete">True: The eccentricity override for the specified diaphragm is deleted.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetDiaphragmEccentricityOverride(string name,
            string diaphragm,
            double eccentricities,
            bool delete = false)
        {

        }
#endif
#endregion
    }
}
