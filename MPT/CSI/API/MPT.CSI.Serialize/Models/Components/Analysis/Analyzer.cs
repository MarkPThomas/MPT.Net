// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-21-2018
// ***********************************************************************
// <copyright file="Analyzer.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Components.Analysis
{
    /// <summary>
    /// Class Analyzer. This class cannot be inherited.
    /// </summary>
    public sealed class Analyzer 
    {
        #region Fields & Properties

        public eSolverType SolverType { get; internal set; }
        public eSolverProcessType SolverProcessType { get; internal set; }
        public bool Force32BitSolver { get; internal set; }
        public string StiffnessCase { get; internal set; }
        public string UndeformedGeometryModificationType { get; internal set; }
        public string HingeOption { get; internal set; }



        /// <summary>
        /// The global coordinates of the location at which the base reactions are reported.
        /// </summary>
        /// <value>The option base react location.</value>
        public Coordinate3DCartesian OptionBaseReactLocation { get; internal set; }


        /// <summary>
        /// The first buckling mode for which the buckling factor is reported when the <see cref="BuckleModesAll" /> item is False.
        /// </summary>
        /// <value>The buckle mode start.</value>
        public int BuckleModeStart { get; internal set; }

        /// <summary>
        /// The last buckling mode for which the buckling factor is reported when the <see cref="BuckleModesAll" /> item is False.
        /// </summary>
        /// <value>The buckle mode end.</value>
        public int BuckleModeEnd { get; internal set; }

        /// <summary>
        /// True: Buckling factors are reported for all calculated buckling modes.
        /// False: Buckling factors are reported for the buckling modes indicated by the <see cref="BuckleModeStart" /> and <see cref="BuckleModeEnd" /> items.
        /// </summary>
        /// <value><c>true</c> if [buckle mode all]; otherwise, <c>false</c>.</value>
        public bool BuckleModesAll { get; internal set; }


        /// <summary>
        /// The first mode for which results are reported when the <see cref="ModeShapesAll" /> item is False.
        /// </summary>
        /// <value>The mode shape start.</value>
        public int ModeShapeStart { get; internal set; }

        /// <summary>
        /// The last mode for which results are reported when the <see cref="ModeShapesAll" /> item is False.
        /// </summary>
        /// <value>The mode shape end.</value>
        public int ModeShapeEnd { get; internal set; }

        /// <summary>
        /// True: Results are reported for all calculated buckling modes.
        /// False: Results are reported for the buckling modes indicated by the <see cref="ModeShapeStart" /> and <see cref="ModeShapeEnd" /> items.
        /// </summary>
        /// <value><c>true</c> if [mode shapes all]; otherwise, <c>false</c>.</value>
        public bool ModeShapesAll { get; internal set; }


        /// <summary>
        /// The output option for direct history results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiStepOptions OptionDirectHistory { get; internal set; }

        /// <summary>
        /// The output option for multistep static linear results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiStepOptions OptionMultiStepStatic { get; internal set; }

        /// <summary>
        /// The output option for modal history results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiStepOptions OptionModalHistory { get; internal set; }

        /// <summary>
        /// The output option for nonlinear static results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiStepOptions OptionNLStatic { get; internal set; }

        /// <summary>
        /// The output option for multi-valued load combination results.
        /// </summary>
        /// <value>The analysis multi step option.</value>
        public eAnalysisMultiValuedOptions OptionMultiValuedCombo { get; internal set; }


        /// <summary>
        /// Gets the cases selected for output.
        /// </summary>
        /// <value>The cases selected for output.</value>
        public List<LoadCase> CasesSelectedForOutput { get; internal set; } = new List<LoadCase>();


        /// <summary>
        /// Gets the combos selected for output.
        /// </summary>
        /// <value>The combos selected for output.</value>
        public List<LoadCombination> CombosSelectedForOutput { get; internal set; } = new List<LoadCombination>();

        // TODO: For SAP2000
        ///// <summary>
        ///// The output option for power spectral density results.
        ///// SAP2000 only.
        ///// </summary>
        ///// <value>The output PSD option.</value>
        //eAnalysisPSDOptions OutputPSDOption { get; internal set; }

        ///// <summary>
        ///// The output option for steady state results.
        ///// SAP2000 only.
        ///// </summary>
        ///// <value>The output steady state option.</value>
        //eAnalysisSteadyStateOptions OutputSteadyStateOption { get; internal set; }

        ///// <summary>
        ///// The steady state option.
        ///// SAP2000 only.
        ///// </summary>
        ///// <value>The steady state option.</value>
        //eSteadyStateOptions SteadyStateOption { get; internal set; }
        #endregion

        #region Fill/Set       
        /// <summary>
        /// Sets the case selected for output.
        /// </summary>
        /// <param name="loadCase">An existing load case.</param>
        public void SetCaseSelectedForOutput(LoadCase loadCase)
        {
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
        /// Sets a load combination selected for output flag.
        /// </summary>
        /// <param name="loadCombination">An existing load combination.</param>
        public void SetComboSelectedForOutput(LoadCombination loadCombination)
        {
            if (loadCombination.IsSelectedForAnalysis)
            {
                if (!CombosSelectedForOutput.Contains(loadCombination)) CombosSelectedForOutput.Add(loadCombination);
            }
            else
            {
                CombosSelectedForOutput.Remove(loadCombination);
            }
        }
    #endregion
    }
}
