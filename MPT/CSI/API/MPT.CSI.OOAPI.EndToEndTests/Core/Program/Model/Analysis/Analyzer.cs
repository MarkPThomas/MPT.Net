using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Analysis
{
    public sealed class Analyzer : IFill
    {
        /// <summary>
        /// The analysis results setup
        /// </summary>
        private readonly AnalysisResultsSetup _analysisResultsSetup = Registry.Application?.Model?.Results?.AnalysisResultsSetup;

        public static Analyzer Instance { get; } = new Analyzer();

        /// <summary>
        /// The global coordinates of the location at which the base reactions are reported.
        /// </summary>
        /// <value>The option base react location.</value>
        public Coordinate3DCartesian OptionBaseReactLocation { get; private set; }


        /// <summary>
        /// The first buckling mode for which the buckling factor is reported when the <see cref="BucklesModeAll" /> item is False.
        /// </summary>
        /// <value>The buckle mode start.</value>
        public int BuckleModeStart { get; private set; }

        /// <summary>
        /// The last buckling mode for which the buckling factor is reported when the <see cref="BuckleModesAll" /> item is False.
        /// </summary>
        /// <value>The buckle mode end.</value>
        public int BuckleModeEnd { get; private set; }

        /// <summary>
        /// True: Buckling factors are reported for all calculated buckling modes.
        /// False: Buckling factors are reported for the buckling modes indicated by the <see cref="BuckleModeStart" /> and <see cref="BuckleModeEnd" /> items.
        /// </summary>
        /// <value><c>true</c> if [buckle mode all]; otherwise, <c>false</c>.</value>
        public bool BuckleModesAll { get; private set; }


        /// <summary>
        /// The first mode for which results are reported when the <see cref="ModeShapesAll" /> item is False.
        /// </summary>
        /// <value>The mode shape start.</value>
        public int ModeShapeStart { get; private set; }

        /// <summary>
        /// The last mode for which results are reported when the <see cref="ModeShapesAll" /> item is False.
        /// </summary>
        /// <value>The mode shape end.</value>
        public int ModeShapeEnd { get; private set; }

        /// <summary>
        /// True: Results are reported for all calculated buckling modes.
        /// False: Results are reported for the buckling modes indicated by the <see cref="ModeShapeStart" /> and <see cref="ModeShapeEnd" /> items.
        /// </summary>
        /// <value><c>true</c> if [mode shapes all]; otherwise, <c>false</c>.</value>
        public bool ModeShapesAll { get; private set; }


        /// <summary>
        /// The output option for direct history results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiStepOptions OptionDirectHistory { get; private set; }

        /// <summary>
        /// The output option for multistep static linear results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiStepOptions OptionMultiStepStatic { get; private set; }

        /// <summary>
        /// The output option for modal history results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiStepOptions OptionModalHistory { get; private set; }

        /// <summary>
        /// The output option for nonlinear static results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiStepOptions OptionNLStatic { get; private set; }

        /// <summary>
        /// The output option for multi-valued load combination results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiValuedOptions OptionMultiValuedCombo { get; private set; }


        public List<LoadCase> CasesSelectedForOutput { get; private set; } = new List<LoadCase>();


        public List<LoadCombination> CombosSelectedForOutput { get; private set; } = new List<LoadCombination>();

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The output option for power spectral density results.
        /// </summary>
        /// <value>The output PSD option.</value>
        eAnalysisPSDOptions OutputPSDOption { get; private set; }

        /// <summary>
        /// The output option for steady state results.
        /// </summary>
        /// <value>The output steady state option.</value>
        eAnalysisSteadyStateOptions OutputSteadyStateOption { get; private set; }

        /// <summary>
        /// The steady state option.
        /// </summary>
        /// <value>The steady state option.</value>
        eSteadyStateOptions SteadyStateOption { get; private set; }
#endif

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static Analyzer(){}

        private Analyzer(){}

        /// <summary>
        /// Fills the data.
        /// </summary>
        public void FillData()
        {
            FillOptionBaseReactLocation();
            FillOptionBucklingMode();
            FillOptionModeShape();
            FillOptionDirectHistory();
            FillOptionMultiStepStatic();
            FillOptionModalHistory();
            FillOptionNLStatic();
            FillOptionMultiValuedCombo();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            FillOptionPSD();
            FillOptionSteadyState();
#endif
        }

        #region Methods: Public        
        /// <summary>
        /// Deselects all load cases and response combinations for output.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void DeselectAllCasesAndCombosForOutput()
        {
            _analysisResultsSetup.DeselectAllCasesAndCombosForOutput();
        }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Selects or deselects all section cuts for output.
        /// Please note that all section cuts are, by default, selected for output when they are created.
        /// </summary>
        /// <param name="selected">if set to <c>true</c> [selected].</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SelectAllSectionCutsForOutput(bool selected)
        {
            _analysisResultsSetup.SelectAllSectionCutsForOutput(selected);
        }
#endif

        /// <summary>
        /// Gets the case selected for output.
        /// True: Specified load case is to be selected for output.
        /// </summary>
        /// <param name="loadCase">An existing load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public bool GetCaseSelectedForOutput(LoadCase loadCase)
        {
            return _analysisResultsSetup.GetCaseSelectedForOutput(loadCase.Name);
        }

        /// <summary>
        /// Sets the case selected for output.
        /// </summary>
        /// <param name="loadCase">An existing load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetCaseSelectedForOutput(LoadCase loadCase)
        {
            _analysisResultsSetup.SetCaseSelectedForOutput(loadCase.Name, loadCase.IsSelectedForAnalysis);
            if (loadCase.IsSelectedForAnalysis)
            {
                if (!CasesSelectedForOutput.Contains(loadCase)) CasesSelectedForOutput.Add(loadCase);
            }
            else
            {
                CasesSelectedForOutput.Remove(loadCase);
            }
        }


        /// <summary>
        /// This function checks if a load combination is selected for output.
        /// True: Specified load combination is to be selected for output.
        /// </summary>
        /// <param name="loadCombination">An existing load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public bool GetComboSelectedForOutput(LoadCombination loadCombination)
        {
            return _analysisResultsSetup.GetComboSelectedForOutput(loadCombination.Name);
        }

        /// <summary>
        /// Sets a load combination selected for output flag.
        /// </summary>
        /// <param name="loadCombination">An existing load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetComboSelectedForOutput(LoadCombination loadCombination)
        {
            _analysisResultsSetup.SetComboSelectedForOutput(loadCombination.Name, loadCombination.IsSelectedForAnalysis);
            if (loadCombination.IsSelectedForAnalysis)
            {
                if (!CombosSelectedForOutput.Contains(loadCombination)) CombosSelectedForOutput.Add(loadCombination);
            }
            else
            {
                CombosSelectedForOutput.Remove(loadCombination);
            }
        }


        /// <summary>
        /// Returns the global coordinates of the location at which the base reactions are reported.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillOptionBaseReactLocation()
        {
            OptionBaseReactLocation = _analysisResultsSetup.GetOptionBaseReactLocation();
        }

        /// <summary>
        /// Sets the global coordinates of the location at which the base reactions are reported.
        /// </summary>
        /// <param name="coordinates">The global coordinates of the location at which the base reactions are reported.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionBaseReactLocation(Coordinate3DCartesian coordinates)
        {
            _analysisResultsSetup.SetOptionBaseReactLocation(coordinates);
            OptionBaseReactLocation = coordinates;
        }


        /// <summary>
        /// Returns the buckling modes for which buckling factors are reported.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillOptionBucklingMode()
        {
            _analysisResultsSetup.GetOptionBucklingMode(
                out var buckleModeStart,
                out var buckleModeEnd,
                out var buckleModesAll);

            BuckleModeStart = buckleModeStart;
            BuckleModeEnd = buckleModeEnd;
            BuckleModesAll = buckleModesAll;
        }

        /// <summary>
        /// Sets the buckling modes for which buckling factors are reported.
        /// </summary>
        /// <param name="buckleModeStart">The first buckling mode for which the buckling factor is reported when the <paramref name="buckleModesAll" /> item is False.</param>
        /// <param name="buckleModeEnd">The last buckling mode for which the buckling factor is reported when the <paramref name="buckleModesAll" /> item is False.</param>
        /// <param name="buckleModesAll">True: Buckling factors are reported for all calculated buckling modes.
        /// False: Buckling factors are reported for the buckling modes indicated by the <paramref name="buckleModeStart" /> and <paramref name="buckleModeEnd" /> items.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionBucklingMode(int buckleModeStart,
            int buckleModeEnd,
            bool buckleModesAll)
        {
            _analysisResultsSetup.SetOptionBucklingMode(
                buckleModeStart,
                buckleModeEnd,
                buckleModesAll);

            BuckleModeStart = buckleModeStart;
            BuckleModeEnd = buckleModeEnd;
            BuckleModesAll = buckleModesAll;
        }


        /// <summary>
        /// Returns the modes for which mode shapes are reported.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillOptionModeShape()
        {
            _analysisResultsSetup.GetOptionModeShape(
                out var modeShapeStart,
                out var modeShapeEnd,
                out var modeShapesAll);

            ModeShapeStart = modeShapeStart;
            ModeShapeEnd = modeShapeEnd;
            ModeShapesAll = modeShapesAll;
        }

        /// <summary>
        /// Sets the modes for which mode shapes are reported.
        /// </summary>
        /// <param name="modeShapeStart">The first mode for which the buckling factor is reported when the <paramref name="modeShapesAll" /> item is False.</param>
        /// <param name="modeShapeEnd">The last mode for which the buckling factor is reported when the <paramref name="modeShapesAll" /> item is False.</param>
        /// <param name="modeShapesAll">True: Results are reported for all calculated modes.
        /// False: Results are reported for the modes indicated by the <paramref name="modeShapeStart" /> and <paramref name="modeShapeEnd" /> items.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionModeShape(int modeShapeStart,
            int modeShapeEnd,
            bool modeShapesAll)
        {
            _analysisResultsSetup.SetOptionModeShape(
                modeShapeStart,
                modeShapeEnd,
                modeShapesAll);

            ModeShapeStart = modeShapeStart;
            ModeShapeEnd = modeShapeEnd;
            ModeShapesAll = modeShapesAll;
        }


        /// <summary>
        /// Returns the output option for direct history results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillOptionDirectHistory()
        {
            OptionDirectHistory = _analysisResultsSetup.GetOptionDirectHistory();
        }

        /// <summary>
        /// Sets the output option for direct history results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionDirectHistory(eAnalysisMultiStepOptions outputOption)
        {
            _analysisResultsSetup.SetOptionDirectHistory(outputOption);
            OptionDirectHistory = outputOption;
        }


        /// <summary>
        /// Returns the output option for multistep static linear results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillOptionMultiStepStatic()
        {
            OptionMultiStepStatic = _analysisResultsSetup.GetOptionMultiStepStatic();
        }

        /// <summary>
        /// Sets the output option for multistep static linear results..
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionMultiStepStatic(eAnalysisMultiStepOptions outputOption)
        {
            _analysisResultsSetup.SetOptionMultiStepStatic(outputOption);
            OptionMultiStepStatic = outputOption;
        }


        /// <summary>
        /// Returns the output option for modal history results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillOptionModalHistory()
        {
            OptionModalHistory = _analysisResultsSetup.GetOptionModalHistory();
        }

        /// <summary>
        /// Sets the output option for modal history results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionModalHistory(eAnalysisMultiStepOptions outputOption)
        {
            _analysisResultsSetup.SetOptionModalHistory(outputOption);
            OptionModalHistory = outputOption;
        }


        /// <summary>
        /// Returns the output option for nonlinear static results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillOptionNLStatic()
        {
            OptionNLStatic = _analysisResultsSetup.GetOptionNLStatic();
        }

        /// <summary>
        /// Sets the output option for nonlinear static results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionNLStatic(eAnalysisMultiStepOptions outputOption)
        {
            _analysisResultsSetup.SetOptionNLStatic(outputOption);
            OptionNLStatic = outputOption;
        }


        /// <summary>
        /// Returns the output option for multi-valued load combination results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillOptionMultiValuedCombo()
        {
            OptionMultiValuedCombo = _analysisResultsSetup.GetOptionMultiValuedCombo();
        }

        /// <summary>
        /// Sets the output option for multi-valued load combination results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionMultiValuedCombo(eAnalysisMultiValuedOptions outputOption)
        {
            _analysisResultsSetup.SetOptionMultiValuedCombo(outputOption);
            OptionMultiValuedCombo = outputOption;
        }


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the output option for power spectral density results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public eAnalysisPSDOptions FillOptionPSD()
        {
            OutputPSDOption = _analysisResultsSetup.GetOptionPSD();
        }

        /// <summary>
        /// Sets the output option for power spectral density results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionPSD(eAnalysisPSDOptions outputOption)
        {
            _analysisResultsSetup.SetOptionPSD(outputOption);

            OutputPSDOption = outputOption;
        }


        /// <summary>
        /// This function gets the output option for steady state results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void FillOptionSteadyState()
        {
            _analysisResultsSetup.GetOptionSteadyState(
                out var outputOption
                out var steadyStateOption);

            OutputSteadyStateOption = outputOption;
            SteadyStateOption = steadyStateOption;
        }

        /// <summary>
        /// Sets the output option for steady state results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <param name="steadyStateOption">The steady state option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionSteadyState(eAnalysisSteadyStateOptions outputOption,
            eSteadyStateOptions steadyStateOption)
        {
            _analysisResultsSetup.SetOptionDirectHistory(outputOption, steadyStateOption);

            OutputSteadyStateOption = outputOption;
            SteadyStateOption = steadyStateOption;
        }
#endif
#endregion
    }
}
