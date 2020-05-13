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
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Loads.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiResponseSpectrum = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.ResponseSpectrum;
using OOApiDiaphragm = MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions.Diaphragm;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Response spectrum load case.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.LoadCase" />
    public sealed class ResponseSpectrum : LoadCase
    {
        #region Fields & Properties
        /// <summary>
        /// The response spectrum API object.
        /// </summary>
        /// <value>The API response spectrum.</value>
        private ApiResponseSpectrum _apiResponseSpectrum => getApiLoadCase(_apiApp).ResponseSpectrum;

        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The list of associated load functions with their respective scale factors and other properties.
        /// </summary>
        /// <value>The patterns.</value>
        private List<LoadResponseSpectrum> _loads;
        /// <summary>
        /// The list of associated load functions with their respective scale factors and other properties.
        /// </summary>
        /// <value>The patterns.</value>
        public List<LoadResponseSpectrum> Loads
        {
            get
            {
                if (_loads == null)
                {
                    FillLoads();
                }

                return _loads;
            }
        }


        /// <summary>
        /// The modal case associated with the load case.
        /// </summary>
        /// <value>The modal case.</value>
        private ModalCaseHelper _modalCase;
        /// <summary>
        /// The modal case associated with the load case.
        /// </summary>
        /// <value>The modal case.</value>
        public ModalCaseHelper ModalCase
        {
            get
            {
                if (_modalCase != null) return _modalCase;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
                _modalCase = new ModalCaseHelper(Name, _apiResponseSpectrum, _apiApp, _apiResponseSpectrum);
#else
                _modalCase = ModalCaseHelper.Factory(Name, _apiResponseSpectrum, _apiApp, _loadCases);
#endif
                _modalCase.FillModalCase();

                return _modalCase;
            }
        }

        /// <summary>
        /// Gets or sets the direction combination.
        /// </summary>
        /// <value>The direction combination.</value>
        private DirectionCombination _directionCombination;
        /// <summary>
        /// Gets or sets the direction combination.
        /// </summary>
        /// <value>The direction combination.</value>
        public DirectionCombination DirectionCombination
        {
            get
            {
                if (_directionCombination == null)
                {
                    FillDirectionalCombination();
                }
                return _directionCombination;
            }
        }

        /// <summary>
        /// Gets or sets the modal combination.
        /// </summary>
        /// <value>The modal combination.</value>
        private ModalCombination _modalCombination;
        /// <summary>
        /// Gets or sets the modal combination.
        /// </summary>
        /// <value>The modal combination.</value>
        public ModalCombination ModalCombination
        {
            get
            {
                if (_modalCombination == null)
                {
                    FillModalCombination();
                }
                return _modalCombination;
            }
        }

        /// <summary>
        /// The eccentricity
        /// </summary>
        private double? _eccentricity;
        /// <summary>
        /// Eccentricity ratio that applies to all diaphragms for the load case.
        /// </summary>
        /// <value>The eccentricity.</value>
        public double Eccentricity
        {
            get
            {
                if (_eccentricity == null)
                {
                    FillEccentricity();
                }

                return _eccentricity ?? 0;
            }
        }

        /// <summary>
        /// The diaphragm eccentricity overwrites
        /// </summary>
        private List<DiaphragmEccentricityOverride> _diaphragmEccentricityOverwrites;
        /// <summary>
        /// The diaphragm eccentricity overwrites of diaphragm, overwrite.
        /// </summary>
        /// <value>The diaphragm eccentricity overwrites.</value>
        public List<DiaphragmEccentricityOverride> DiaphragmEccentricityOverwrites
        {
            get
            {
                if (_diaphragmEccentricityOverwrites == null)
                {
                    FillDiaphragmEccentricityOverride();
                }

                return _diaphragmEccentricityOverwrites;
            }
        }

        /// <summary>
        /// The damping associated with the load case.
        /// </summary>
        /// <value>The damping.</value>
        private DampingHelper _damping;
        /// <summary>
        /// The damping associated with the load case.
        /// </summary>
        /// <value>The damping.</value>
        public DampingHelper Damping => _damping ?? (_damping = new DampingHelper(_apiResponseSpectrum, Name));
        #endregion

        #region Initialization
        /// <summary>
        /// Returns a new load case class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">Unique load case name.</param>
        /// <returns>Steel.</returns>
        internal static ResponseSpectrum Factory(
            ApiCSiApplication app,
            Analyzer analyzer,
            LoadCases loadCases, 
            string uniqueName)
        {
            ResponseSpectrum loadCase = new ResponseSpectrum(app, analyzer, loadCases, uniqueName);
            loadCase.FillData();

            return loadCase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.ResponseSpectrum" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        private ResponseSpectrum(
            ApiCSiApplication app, 
            Analyzer analyzer,
            LoadCases loadCases,
            string name) : base(app, analyzer, name)
        {
            _loadCases = loadCases;
        }

        /// <summary>
        /// Adds a new load case.
        /// If the name is not unique, the existing load case will be returned.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="uniqueName">The unique name.</param>
        /// <returns>StaticLinear.</returns>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        internal static ResponseSpectrum Add(
            ApiCSiApplication app, 
            Analyzer analyzer,
            LoadCases loadCases,
            string uniqueName)
        {
            ApiResponseSpectrum apiResponseSpectrum = getApiLoadCase(app).ResponseSpectrum;
            apiResponseSpectrum?.SetCase(uniqueName);
            return Factory(app, analyzer, loadCases, uniqueName);
        }

        /// <summary>
        /// Resets to all items for the case to their default values.
        /// </summary>
        public void ResetToDefault()
        {
            _apiResponseSpectrum?.SetCase(Name);
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
            if (_apiResponseSpectrum == null) return;
            _apiResponseSpectrum.GetLoads(Name,
                out var loadDirections,
                out var functions,
                out var scaleFactor,
                out var coordinateSystems,
                out var angles);

            _loads = new List<LoadResponseSpectrum>();
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
                _loads.Add(loadFunction);
            }
        }

        /// <summary>
        /// Returns the modal combination option assigned to the specified load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillModalCombination()
        {
            if (_apiResponseSpectrum == null) return;
            _apiResponseSpectrum.GetModalCombination(Name,
                out var modalCombination,
                out var gmcF1,
                out var gmcF2,
                out var periodicPlusRigidModalCombination,
                out var td);

            _modalCombination = new ModalCombination()
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
            if (_apiResponseSpectrum == null) return;
            _apiResponseSpectrum.GetDirectionalCombination(Name,
                out var directionalCombination,
                out var scaleFactor);

            _directionCombination = new DirectionCombination()
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
            if (_apiResponseSpectrum == null) return;
            _eccentricity = _apiResponseSpectrum.GetEccentricity(Name);
        }


        /// <summary>
        /// Returns the diaphragm eccentricity overrides for a load case.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillDiaphragmEccentricityOverride()
        {
            if (_apiResponseSpectrum == null) return;
            _apiResponseSpectrum.GetDiaphragmEccentricityOverride(Name,
                out var diaphragms,
                out var eccentricities);

            _diaphragmEccentricityOverwrites = new List<DiaphragmEccentricityOverride>();
            for (int i = 0; i < diaphragms.Length; i++)
            {
                DiaphragmEccentricityOverride diaphragmOverwrite = new DiaphragmEccentricityOverride()
                {
                        Diaphragm = OOApiDiaphragm.Factory(_apiApp, diaphragms[i]),
                        Eccentricity = eccentricities[i]
                };
                _diaphragmEccentricityOverwrites.Add(diaphragmOverwrite);
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
                throw new NotImplementedException();
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
                throw new NotImplementedException();
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
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
        throw new NotImplementedException();
        }
#endif
        #endregion
    }
}
