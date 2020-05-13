﻿// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 06-10-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-08-2017
// ***********************************************************************
// <copyright file="StaticNonlinear.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using MPT.CSI.API.Core.Support;
using MPT.Enums;


namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase
{
    /// <summary>
    /// Represents the static nonlinear load case in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCase.IStaticNonlinear" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class StaticNonlinear : CSiApiBase, IStaticNonlinear
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="StaticNonlinear" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public StaticNonlinear(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Interface
        /// <inheritdoc />
        /// <summary>
        /// This function initializes a load case.
        /// If this function is called for an existing load case, all items for the case are reset to their default value.
        /// </summary>
        /// <param name="name">The name of an existing or new load case.
        /// If this is an existing case, that case is modified; otherwise, a new case is added.</param>
        /// <exception cref="T:MPT.CSI.API.Core.Support.CSiException">API_DEFAULT_ERROR_CODE</exception>
        public void SetCase(string name)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinear.SetCase(name);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



        /// <inheritdoc />
        /// <summary>
        /// Returns the initial condition assumed for the specified load case.
        /// This is blank, None, or the name of an existing analysis case.
        /// This item specifies if the load case starts from zero initial conditions, that is, an unstressed state, or if it starts using the stiffness that occurs at the end of a nonlinear static or nonlinear direct integration time history load case.
        /// If the specified initial case is a nonlinear static or nonlinear direct integration time history load case, the stiffness at the end of that case is used.
        /// If the initial case is anything else then zero initial conditions are assumed.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string GetInitialCase(string name)
        {
            string initialCase = string.Empty;
            _callCode = _sapModel.LoadCases.StaticNonlinear.GetInitialCase(name, ref initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return initialCase;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the initial condition for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="initialCase">This is blank, None, or the name of an existing analysis case.
        /// This item specifies if the load case starts from zero initial conditions, that is, an unstressed state, or if it starts using the stiffness that occurs at the end of a nonlinear static or nonlinear direct integration time history load case.
        /// If the specified initial case is a nonlinear static or nonlinear direct integration time history load case, the stiffness at the end of that case is used.
        /// If the initial case is anything else then zero initial conditions are assumed.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetInitialCase(string name,
            string initialCase)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinear.SetInitialCase(name, initialCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <inheritdoc />
        /// <summary>
        /// Returns the load data for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing steady state load case.</param>
        /// <param name="loadTypes">Either <see cref="eLoadType.Load" /> or <see cref="eLoadType.Accel" />, indicating the type of each load assigned to the load case.</param>
        /// <param name="loadNames">The name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />, this item is U1, U2, U3, R1, R2 or R3, indicating the direction of the load.</param>
        /// <param name="scaleFactor">The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoads(string name,
            out eLoadType[] loadTypes,
            out string[] loadNames,
            out double[] scaleFactor)
        {
            loadNames = new string[0];
            scaleFactor = new double[0];
            string[] csiLoadTypes = new string[0];

            _callCode = _sapModel.LoadCases.StaticNonlinear.GetLoads(name, ref _numberOfItems, ref csiLoadTypes, ref loadNames, ref scaleFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadTypes = new eLoadType[_numberOfItems - 1];
            for (int i = 0; i < _numberOfItems; i++)
            {
                loadTypes[i] = EnumLibrary.ConvertStringToEnumByDescription<eLoadType>(csiLoadTypes[i]);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the load data for the specified analysis case.
        /// </summary>
        /// <param name="name">The name of an existing steady state load case.</param>
        /// <param name="loadTypes">Either <see cref="eLoadType.Load" /> or <see cref="eLoadType.Accel" />, indicating the type of each load assigned to the load case.</param>
        /// <param name="loadNames">The name of each load assigned to the load case.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Load" />, this item is the name of a defined load pattern.
        /// If <paramref name="loadTypes" /> = <see cref="eLoadType.Accel" />, this item is U1, U2, U3, R1, R2 or R3, indicating the direction of the load.</param>
        /// <param name="scaleFactor">The scale factor of each load assigned to the load case. [L/s^2] for U1 U2 and U3; otherwise unitless.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoads(string name,
            eLoadType[] loadTypes,
            string[] loadNames,
            double[] scaleFactor)
        {
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(loadNames), loadNames.Length);
            arraysLengthMatch(nameof(loadTypes), loadTypes.Length, nameof(scaleFactor), scaleFactor.Length);

            string[] csiLoadTypes = new string[loadTypes.Length];
            for (int i = 0; i < loadTypes.Length; i++)
            {
                csiLoadTypes[i] = loadTypes[i].ToString();
            }

            _callCode = _sapModel.LoadCases.StaticNonlinear.SetLoads(name, loadTypes.Length, ref csiLoadTypes, ref loadNames, ref scaleFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the mass source to be used for the specified load case.
        /// This is the name of an existing mass source or a blank string.
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string GetMassSource(string name)
        {
            string sourceName = string.Empty;
            _callCode = _sapModel.LoadCases.StaticNonlinear.GetMassSource(name, ref sourceName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return sourceName;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the mass source to be used for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="sourceName">This is the name of an existing mass source or a blank string.
        /// Blank indicates to use the mass source from the previous load case or the default mass source if the load case starts from zero initial conditions.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMassSource(string name,
            string sourceName)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinear.SetMassSource(name, sourceName);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the modal case assigned to the specified load case.
        /// This is either None or the name of an existing modal analysis case.
        /// It specifies the modal load case on which any mode-type load assignments to the specified load case are based.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public string GetModalCase(string name)
        {
            string modalCase = String.Empty;
            _callCode = _sapModel.LoadCases.StaticNonlinear.GetModalCase(name, ref modalCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return modalCase;
        }

        /// <inheritdoc />
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
        public void SetModalCase(string name,
            string modalCase)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinear.SetModalCase(name, modalCase);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the geometric nonlinearity option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public eGeometricNonlinearity GetGeometricNonlinearity(string name)
        {
            int csiGeometricNonlinearityType = -1;

            _callCode = _sapModel.LoadCases.StaticNonlinear.GetGeometricNonlinearity(name, ref csiGeometricNonlinearityType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eGeometricNonlinearity)csiGeometricNonlinearityType;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the geometric nonlinearity option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="geometricNonlinearityType">The geometric nonlinearity option selected for the load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetGeometricNonlinearity(string name,
            eGeometricNonlinearity geometricNonlinearityType)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinear.SetGeometricNonlinearity(name, (int)geometricNonlinearityType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxTotalSteps">The maximum total steps per stage.</param>
        /// <param name="maxNullSteps">The maximum null (zero) steps per stage.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetSolutionControlParameters(string name,
            out int maxTotalSteps,
            out int maxNullSteps,
            out int maxConstantStiffnessIterationsPerStep,
            out int maxNewtonRaphsonIterationsPerStep,
            out double relativeIterationConvergenceTolerance,
            out bool useEventStepping,
            out double relativeEventLumpingTolerance,
            out int maxNumberLineSearches,
            out double relativeLineSearchAcceptanceTolerance,
            out double lineSearchStepFactor)
        {
            maxTotalSteps = -1;
            maxNullSteps = -1;
            maxConstantStiffnessIterationsPerStep = -1;
            maxNewtonRaphsonIterationsPerStep = -1;
            relativeIterationConvergenceTolerance = -1;
            useEventStepping = false;
            relativeEventLumpingTolerance = -1;
            maxNumberLineSearches = -1;
            relativeLineSearchAcceptanceTolerance = -1;
            lineSearchStepFactor = -1;

            _callCode = _sapModel.LoadCases.StaticNonlinear.GetSolControlParameters(name,
                            ref maxTotalSteps,
                            ref maxNullSteps,
                            ref maxConstantStiffnessIterationsPerStep,
                            ref maxNewtonRaphsonIterationsPerStep,
                            ref relativeIterationConvergenceTolerance,
                            ref useEventStepping,
                            ref relativeEventLumpingTolerance,
                            ref maxNumberLineSearches,
                            ref relativeLineSearchAcceptanceTolerance,
                            ref lineSearchStepFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the solution control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing nonlinear load case.</param>
        /// <param name="maxTotalSteps">The maximum total steps per stage.</param>
        /// <param name="maxNullSteps">The maximum null (zero) steps per stage.</param>
        /// <param name="maxConstantStiffnessIterationsPerStep">The maximum constant stiffness iterations per step.</param>
        /// <param name="maxNewtonRaphsonIterationsPerStep">The maximum Newton-Raphson iterations per step.</param>
        /// <param name="relativeIterationConvergenceTolerance">The relative iteration convergence tolerance.</param>
        /// <param name="useEventStepping">True: Event-to-event stepping is used.</param>
        /// <param name="relativeEventLumpingTolerance">The relative event lumping tolerance.</param>
        /// <param name="maxNumberLineSearches">The maximum number of line-searches per iteration.</param>
        /// <param name="relativeLineSearchAcceptanceTolerance">The relative line-search acceptance tolerance.</param>
        /// <param name="lineSearchStepFactor">The line-search step factor.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSolutionControlParameters(string name,
            int maxTotalSteps,
            int maxNullSteps,
            int maxConstantStiffnessIterationsPerStep,
            int maxNewtonRaphsonIterationsPerStep,
            double relativeIterationConvergenceTolerance,
            bool useEventStepping,
            double relativeEventLumpingTolerance,
            int maxNumberLineSearches,
            double relativeLineSearchAcceptanceTolerance,
            double lineSearchStepFactor)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinear.SetSolControlParameters(name,
                            maxTotalSteps,
                            maxNullSteps,
                            maxConstantStiffnessIterationsPerStep,
                            maxNewtonRaphsonIterationsPerStep,
                            relativeIterationConvergenceTolerance,
                            useEventStepping,
                            relativeEventLumpingTolerance,
                            maxNumberLineSearches,
                            relativeLineSearchAcceptanceTolerance,
                            lineSearchStepFactor);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the target force iteration parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="convergenceTolerance">The relative convergence tolerance for target force iteration.</param>
        /// <param name="maxIterations">The maximum iterations per stage for target force iteration.</param>
        /// <param name="accelerationFactor">The acceleration factor.</param>
        /// <param name="continueIfNoConvergence">True: Analysis is continued when there is no convergence in the target force iteration.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetTargetForceParameters(string name,
            out double convergenceTolerance,
            out int maxIterations,
            out double accelerationFactor,
            out bool continueIfNoConvergence)
        {
            convergenceTolerance = -1;
            maxIterations = -1;
            accelerationFactor = -1;
            continueIfNoConvergence = false;

            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.GetTargetForceParameters(name, 
                ref convergenceTolerance, 
                ref maxIterations, 
                ref accelerationFactor, 
                ref continueIfNoConvergence);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the target force iteration parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="convergenceTolerance">The relative convergence tolerance for target force iteration.</param>
        /// <param name="maxIterations">The maximum iterations per stage for target force iteration.</param>
        /// <param name="accelerationFactor">The acceleration factor.</param>
        /// <param name="continueIfNoConvergence">True: Analysis is continued when there is no convergence in the target force iteration.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetTargetForceParameters(string name,
            double convergenceTolerance,
            int maxIterations,
            double accelerationFactor,
            bool continueIfNoConvergence)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinearStaged.SetTargetForceParameters(name, convergenceTolerance, maxIterations, accelerationFactor, continueIfNoConvergence);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the hinge unloading option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public eHingeUnloadingType GetHingeUnloading(string name)
        {
            int csiHingeUnloadingType = -1;

            _callCode = _sapModel.LoadCases.StaticNonlinear.GetHingeUnloading(name, ref csiHingeUnloadingType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eHingeUnloadingType) csiHingeUnloadingType;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the hinge unloading option for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing analysis case.</param>
        /// <param name="hingeUnloadingType">Type of hinge unloading for the selected load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetHingeUnloading(string name,
            eHingeUnloadingType hingeUnloadingType)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinear.SetHingeUnloading(name, (int)hingeUnloadingType);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the results saved parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="saveMultipleSteps">True: Multiple states are saved for the nonlinear analysis.
        /// False: Only if the final state is saved.</param>
        /// <param name="minSavedStates">The minimum number of saved states.
        /// This item applies only when <paramref name="saveMultipleSteps" /> = True.</param>
        /// <param name="maxSavedStates">The maximum number of saved states.
        /// This item applies only when <paramref name="saveMultipleSteps" /> = True.</param>
        /// <param name="savePositiveDisplacementIncrementsOnly">True: Only positive displacement increments are saved.
        /// False: All displacement increments are saved.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetResultsSaved(string name,
            out bool saveMultipleSteps,
            out int minSavedStates,
            out int maxSavedStates,
            out bool savePositiveDisplacementIncrementsOnly)
        {
            saveMultipleSteps = false;
            minSavedStates = -1;
            maxSavedStates = -1;
            savePositiveDisplacementIncrementsOnly = false;

            _callCode = _sapModel.LoadCases.StaticNonlinear.GetResultsSaved(name, 
                ref saveMultipleSteps, 
                ref minSavedStates, 
                ref maxSavedStates, 
                ref savePositiveDisplacementIncrementsOnly);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the results saved parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="saveMultipleSteps">True: Multiple states are saved for the nonlinear analysis.
        /// False: Only if the final state is saved.</param>
        /// <param name="minSavedStates">The minimum number of saved states.
        /// This item applies only when <paramref name="saveMultipleSteps" /> = True.</param>
        /// <param name="maxSavedStates">The maximum number of saved states.
        /// This item applies only when <paramref name="saveMultipleSteps" /> = True.</param>
        /// <param name="savePositiveDisplacementIncrementsOnly">True: Only positive displacement increments are saved.
        /// False: All displacement increments are saved.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetResultsSaved(string name,
            bool saveMultipleSteps,
            int minSavedStates = 10,
            int maxSavedStates = 100,
            bool savePositiveDisplacementIncrementsOnly = true)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinear.SetResultsSaved(name, saveMultipleSteps, minSavedStates, maxSavedStates, savePositiveDisplacementIncrementsOnly);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the load application control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="loadControl">The load application control method.</param>
        /// <param name="controlDisplacementType">Type of control displacement.</param>
        /// <param name="targetDisplacement">The structure is loaded to a monitored displacement of this magnitude.
        /// [L] when <paramref name="degreeOfFreedom" /> = <see cref="eDegreeOfFreedom.U1" />, <see cref="eDegreeOfFreedom.U2" /> or <see cref="eDegreeOfFreedom.U3" /> and [rad] when <paramref name="degreeOfFreedom" /> = <see cref="eDegreeOfFreedom.R1" />, <see cref="eDegreeOfFreedom.R2" /> or <see cref="eDegreeOfFreedom.R3" />.
        /// This item applies only when displacement control is used, that is, <paramref name="loadControl" /> = <see cref="eLoadControl.DisplacementControl" />.</param>
        /// <param name="monitoredDisplacementType">Type of monitored displacement.</param>
        /// <param name="degreeOfFreedom">The degree of freedom for which the displacement at a point object is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" /></param>
        /// <param name="namePoint">The name of the point object at which the displacement is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" />.</param>
        /// <param name="nameGeneralizedDisplacement">The name of the generalized displacement for which the displacement is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.GeneralizedDisplacement" /></param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetLoadApplication(string name,
            out eLoadControl loadControl,
            out eLoadControlDisplacement controlDisplacementType,
            out double targetDisplacement,
            out eMonitoredDisplacementType monitoredDisplacementType,
            out eDegreeOfFreedom degreeOfFreedom,
            out string namePoint,
            out string nameGeneralizedDisplacement)
        {
            targetDisplacement = -1;
            namePoint = string.Empty;
            nameGeneralizedDisplacement = string.Empty;

            int csiLoadControl = -1;
            int csiControlDisplacementType = -1;
            int csiMonitoredDisplacementType = -1;
            int csiDegreeOfFreedom = -1;

            _callCode = _sapModel.LoadCases.StaticNonlinear.GetLoadApplication(name,
                ref csiLoadControl, 
                ref csiControlDisplacementType, 
                ref targetDisplacement, 
                ref csiMonitoredDisplacementType, 
                ref csiDegreeOfFreedom, 
                ref namePoint, 
                ref nameGeneralizedDisplacement);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            loadControl = (eLoadControl)csiLoadControl;
            controlDisplacementType = (eLoadControlDisplacement)csiControlDisplacementType;
            monitoredDisplacementType = (eMonitoredDisplacementType)csiMonitoredDisplacementType;
            degreeOfFreedom = (eDegreeOfFreedom)csiDegreeOfFreedom;
        }

        /// <inheritdoc />
        /// <summary>
        /// Sets the load application control parameters for the specified load case.
        /// </summary>
        /// <param name="name">The name of an existing static nonlinear load case.</param>
        /// <param name="loadControl">The load application control method.</param>
        /// <param name="controlDisplacementType">Type of control displacement.</param>
        /// <param name="targetDisplacement">The structure is loaded to a monitored displacement of this magnitude.
        /// [L] when <paramref name="degreeOfFreedom" /> = <see cref="eDegreeOfFreedom.U1" />, <see cref="eDegreeOfFreedom.U2" /> or <see cref="eDegreeOfFreedom.U3" /> and [rad] when <paramref name="degreeOfFreedom" /> = <see cref="eDegreeOfFreedom.R1" />, <see cref="eDegreeOfFreedom.R2" /> or <see cref="eDegreeOfFreedom.R3" />.
        /// This item applies only when displacement control is used, that is, <paramref name="loadControl" /> = <see cref="eLoadControl.DisplacementControl" />.</param>
        /// <param name="monitoredDisplacementType">Type of monitored displacement.</param>
        /// <param name="degreeOfFreedom">The degree of freedom for which the displacement at a point object is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" /></param>
        /// <param name="namePoint">The name of the point object at which the displacement is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" />.</param>
        /// <param name="nameGeneralizedDisplacement">The name of the generalized displacement for which the displacement is monitored.
        /// This item applies only when <paramref name="monitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.GeneralizedDisplacement" /></param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadApplication(string name,
            eLoadControl loadControl,
            eLoadControlDisplacement controlDisplacementType,
            double targetDisplacement,
            eMonitoredDisplacementType monitoredDisplacementType,
            eDegreeOfFreedom degreeOfFreedom,
            string namePoint,
            string nameGeneralizedDisplacement)
        {
            _callCode = _sapModel.LoadCases.StaticNonlinear.SetLoadApplication(name,
                (int)loadControl, (int)controlDisplacementType, targetDisplacement, (int)monitoredDisplacementType, (int)degreeOfFreedom, namePoint, nameGeneralizedDisplacement);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
        #endregion
    }
}
