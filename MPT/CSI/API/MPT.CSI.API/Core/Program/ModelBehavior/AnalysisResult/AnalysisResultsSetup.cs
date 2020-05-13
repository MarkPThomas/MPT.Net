// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark
// Created          : 06-08-2017
//
// Last Modified By : Mark
// Last Modified On : 10-06-2017
// ***********************************************************************
// <copyright file="AnalysisResultsSetup.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult
{
    /// <summary>
    /// Represents the analysis results setup in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResult.IAnalysisResultsSetup" />
    /// <seealso cref="MPT.CSI.API.Core.Support.CSiApiBase" />
    public class AnalysisResultsSetup : CSiApiBase, IAnalysisResultsSetup
    {
        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="AnalysisResultsSetup" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public AnalysisResultsSetup(CSiApiSeed seed) : base(seed) { }


        #endregion

        #region Methods: Public        
        /// <summary>
        /// Deselects all load cases and response combinations for output.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void DeselectAllCasesAndCombosForOutput()
        {
            _callCode = _sapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
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
            _callCode = _sapModel.Results.Setup.SelectAllSectionCutsForOutput(selected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif



        /// <summary>
        /// Gets the case selected for output.
        /// True: Specified load case is to be selected for output.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public bool GetCaseSelectedForOutput(string name)
        {
            bool selected = false;
            _callCode = _sapModel.Results.Setup.GetCaseSelectedForOutput(name, ref selected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return selected;
        }

        /// <summary>
        /// Sets the case selected for output.
        /// </summary>
        /// <param name="name">The name of an existing load case.</param>
        /// <param name="selected">True: Specified load case is to be selected for output.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetCaseSelectedForOutput(string name,
            bool selected = true)
        {
            _callCode = _sapModel.Results.Setup.SetCaseSelectedForOutput(name, selected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function checks if a load combination is selected for output.
        /// True: Specified load combination is to be selected for output.
        /// </summary>
        /// <param name="name">The name of an existing load combination.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public bool GetComboSelectedForOutput(string name)
        {
            bool selected = false;
            _callCode = _sapModel.Results.Setup.GetComboSelectedForOutput(name, ref selected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return selected;
        }

        /// <summary>
        /// Sets a load combination selected for output flag.
        /// </summary>
        /// <param name="name">The name of an existing load combination.</param>
        /// <param name="selected">True: Specified load combination is to be selected for output.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetComboSelectedForOutput(string name,
            bool selected = true)
        {
            _callCode = _sapModel.Results.Setup.SetComboSelectedForOutput(name, selected);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns the global coordinates of the location at which the base reactions are reported.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public Coordinate3DCartesian GetOptionBaseReactLocation()
        {
            double gx = 0;
            double gy = 0;
            double gz = 0;
            _callCode = _sapModel.Results.Setup.GetOptionBaseReactLoc(ref gx, ref gy, ref gz);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            Coordinate3DCartesian coordinates = new Coordinate3DCartesian
            {
                X = gx,
                Y = gy,
                Z = gz
            };
            return coordinates;
        }

        /// <summary>
        /// Sets the global coordinates of the location at which the base reactions are reported.
        /// </summary>
        /// <param name="coordinates">The global coordinates of the location at which the base reactions are reported.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionBaseReactLocation(Coordinate3DCartesian coordinates)
        {
            _callCode = _sapModel.Results.Setup.SetOptionBaseReactLoc(coordinates.X, coordinates.Y, coordinates.Z);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns the buckling modes for which buckling factors are reported.
        /// </summary>
        /// <param name="buckleModeStart">The first buckling mode for which the buckling factor is reported when the <paramref name="buckleModeAll" /> item is False.</param>
        /// <param name="buckleModeEnd">The last buckling mode for which the buckling factor is reported when the <paramref name="buckleModeAll" /> item is False.</param>
        /// <param name="buckleModeAll">True: Buckling factors are reported for all calculated buckling modes.
        /// False: Buckling factors are reported for the buckling modes indicated by the <paramref name="buckleModeStart" /> and <paramref name="buckleModeEnd" /> items.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetOptionBucklingMode(out int buckleModeStart,
            out int buckleModeEnd,
            out bool buckleModeAll)
        {
            buckleModeStart = -1;
            buckleModeEnd = -1;
            buckleModeAll = false;

            _callCode = _sapModel.Results.Setup.GetOptionBucklingMode(ref buckleModeStart, ref buckleModeEnd, ref buckleModeAll);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the buckling modes for which buckling factors are reported.
        /// </summary>
        /// <param name="buckleModeStart">The first buckling mode for which the buckling factor is reported when the <paramref name="buckleModeAll" /> item is False.</param>
        /// <param name="buckleModeEnd">The last buckling mode for which the buckling factor is reported when the <paramref name="buckleModeAll" /> item is False.</param>
        /// <param name="buckleModeAll">True: Buckling factors are reported for all calculated buckling modes.
        /// False: Buckling factors are reported for the buckling modes indicated by the <paramref name="buckleModeStart" /> and <paramref name="buckleModeEnd" /> items.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionBucklingMode(int buckleModeStart,
            int buckleModeEnd,
            bool buckleModeAll)
        {
            _callCode = _sapModel.Results.Setup.SetOptionBucklingMode(buckleModeStart, buckleModeEnd, buckleModeAll);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns the modes for which mode shapes are reported.
        /// </summary>
        /// <param name="modeShapeStart">The first mode for which results are reported when the <paramref name="modeShapesAll" />  item is False.</param>
        /// <param name="modeShapeEnd">The last mode for which results are reported when the <paramref name="modeShapesAll" /> item is False.</param>
        /// <param name="modeShapesAll">True: Results are reported for all calculated modes.
        /// False: Results are reported for the modes indicated by the <paramref name="modeShapeStart" /> and <paramref name="modeShapeEnd" /> items.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetOptionModeShape(out int modeShapeStart,
            out int modeShapeEnd,
            out bool modeShapesAll)
        {
            modeShapeStart = -1;
            modeShapeEnd = -1;
            modeShapesAll = false;

            _callCode = _sapModel.Results.Setup.GetOptionModeShape(ref modeShapeStart, ref modeShapeEnd, ref modeShapesAll);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }

        /// <summary>
        /// Sets the modes for which mode shapes are reported.
        /// </summary>
        /// <param name="modeShapeStart">The first mode for which results are reported when the <paramref name="modeShapesAll" />  item is False.</param>
        /// <param name="modeShapeEnd">The last mode for which results are reported when the <paramref name="modeShapesAll" /> item is False.</param>
        /// <param name="modeShapesAll">True: Results are reported for all calculated modes.
        /// False: Results are reported for the modes indicated by the <paramref name="modeShapeStart" /> and <paramref name="modeShapeEnd" /> items.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionModeShape(int modeShapeStart,
            int modeShapeEnd,
            bool modeShapesAll)
        {
            _callCode = _sapModel.Results.Setup.SetOptionModeShape(modeShapeStart, modeShapeEnd, modeShapesAll);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// Returns the output option for direct history results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public eAnalysisMultiStepOptions GetOptionDirectHistory()
        {
            int csiValue = 0;

            _callCode = _sapModel.Results.Setup.GetOptionDirectHist(ref csiValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eAnalysisMultiStepOptions)csiValue;
        }

        /// <summary>
        /// Sets the output option for direct history results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionDirectHistory(eAnalysisMultiStepOptions outputOption)
        {
            _callCode = _sapModel.Results.Setup.SetOptionDirectHist((int)outputOption);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// Returns the output option for multistep static linear results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public eAnalysisMultiStepOptions GetOptionMultiStepStatic()
        {
            int csiValue = 0;

            _callCode = _sapModel.Results.Setup.GetOptionMultiStepStatic(ref csiValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eAnalysisMultiStepOptions)csiValue;
        }

        /// <summary>
        /// Sets the output option for multistep static linear results..
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionMultiStepStatic(eAnalysisMultiStepOptions outputOption)
        {
            _callCode = _sapModel.Results.Setup.SetOptionMultiStepStatic((int)outputOption);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }





        /// <summary>
        /// Returns the output option for modal history results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public eAnalysisMultiStepOptions GetOptionModalHistory()
        {
            int csiValue = 0;

            _callCode = _sapModel.Results.Setup.GetOptionModalHist(ref csiValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eAnalysisMultiStepOptions)csiValue;
        }

        /// <summary>
        /// Sets the output option for modal history results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionModalHistory(eAnalysisMultiStepOptions outputOption)
        {
            _callCode = _sapModel.Results.Setup.SetOptionModalHist((int)outputOption);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// Returns the output option for nonlinear static results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public eAnalysisMultiStepOptions GetOptionNLStatic()
        {
            int csiValue = 0;

            _callCode = _sapModel.Results.Setup.GetOptionNLStatic(ref csiValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eAnalysisMultiStepOptions)csiValue;
        }

        /// <summary>
        /// Sets the output option for nonlinear static results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionNLStatic(eAnalysisMultiStepOptions outputOption)
        {
            _callCode = _sapModel.Results.Setup.SetOptionNLStatic((int)outputOption);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }


        /// <summary>
        /// Returns the output option for multi-valued load combination results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public eAnalysisMultiValuedOptions GetOptionMultiValuedCombo()
        {
            int csiValue = 0;

            _callCode = _sapModel.Results.Setup.GetOptionMultiValuedCombo(ref csiValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eAnalysisMultiValuedOptions)csiValue;
        }

        /// <summary>
        /// Sets the output option for multi-valued load combination results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionMultiValuedCombo(eAnalysisMultiValuedOptions outputOption)
        {
            _callCode = _sapModel.Results.Setup.SetOptionMultiValuedCombo((int)outputOption);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }



#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Returns the output option for power spectral density results.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public eAnalysisPSDOptions GetOptionPSD()
        {
            int csiValue = 0;

            _callCode = _sapModel.Results.Setup.GetOptionPSD(ref csiValue);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            return (eAnalysisPSDOptions)csiValue;
        }

        /// <summary>
        /// Sets the output option for power spectral density results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetOptionPSD(eAnalysisPSDOptions outputOption)
        {
            _callCode = _sapModel.Results.Setup.SetOptionPSD((int)outputOption);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }




        /// <summary>
        /// This function gets the output option for steady state results.
        /// </summary>
        /// <param name="outputOption">The output option.</param>
        /// <param name="steadyStateOption">The steady state option.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void GetOptionSteadyState(out eAnalysisSteadyStateOptions outputOption,
            out eSteadyStateOptions steadyStateOption)
        {
            int csiValue = 0;
            int csiSteadyState = 0;

            _callCode = _sapModel.Results.Setup.GetOptionSteadyState(ref csiValue, ref csiSteadyState);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }

            outputOption = (eAnalysisSteadyStateOptions)csiValue;
            steadyStateOption = (eSteadyStateOptions)csiSteadyState;
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
            _callCode = _sapModel.Results.Setup.SetOptionSteadyState((int)outputOption, (int)steadyStateOption);
            if (throwCurrentApiException(_callCode)) { throw new CSiException(API_DEFAULT_ERROR_CODE); }
        }
#endif
        #endregion
    }
}
