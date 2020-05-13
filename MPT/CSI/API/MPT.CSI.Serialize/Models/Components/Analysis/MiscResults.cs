// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-30-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-01-2018
// ***********************************************************************
// <copyright file="MiscResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Results;

namespace MPT.CSI.Serialize.Models.Components.Analysis
{
    /// <summary>
    /// Class MiscResults.
    /// </summary>
    /// <seealso cref="AnalysisResults" />
    public class MiscResults : AnalysisResults
    {
        #region Fields & Properties

        /// <summary>
        /// The modal load participation ratios
        /// </summary>
        private List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>> _modalLoadParticipationRatios;
        /// <summary>
        /// Gets the modal load participation ratios.
        /// </summary>
        /// <value>The modal load participation ratios.</value>
        public List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>> ModalLoadParticipationRatios
        {
            get
            {
                if (_modalLoadParticipationRatios == null)
                {
                }

                return _modalLoadParticipationRatios;
            }
        }

        /// <summary>
        /// The modal participating mass ratios
        /// </summary>
        private List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>> _modalParticipatingMassRatios;
        /// <summary>
        /// Gets the modal participating mass ratios.
        /// </summary>
        /// <value>The modal participating mass ratios.</value>
        public List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>> ModalParticipatingMassRatios
        {
            get
            {
                if (_modalParticipatingMassRatios == null)
                {
                }

                return _modalParticipatingMassRatios;
            }
        }

        /// <summary>
        /// The modal participation factors
        /// </summary>
        private List<Tuple<StepResultsIdentifier, ModalParticipationFactor>> _modalParticipationFactors;
        /// <summary>
        /// Gets the modal participation factors.
        /// </summary>
        /// <value>The modal participation factors.</value>
        public List<Tuple<StepResultsIdentifier, ModalParticipationFactor>> ModalParticipationFactors
        {
            get
            {
                if (_modalParticipationFactors == null)
                {
                }

                return _modalParticipationFactors;
            }
        }

        /// <summary>
        /// The modal periods
        /// </summary>
        private List<Tuple<StepResultsIdentifier, ModalPeriod>> _modalPeriods;
        /// <summary>
        /// Gets the modal periods.
        /// </summary>
        /// <value>The modal periods.</value>
        public List<Tuple<StepResultsIdentifier, ModalPeriod>> ModalPeriods
        {
            get
            {
                if (_modalPeriods == null)
                {
                }

                return _modalPeriods;
            }
        }

        /// <summary>
        /// The buckling factors
        /// </summary>
        private List<Tuple<StepResultsIdentifier, double>> _bucklingFactors;
        /// <summary>
        /// Gets the buckling factors.
        /// </summary>
        /// <value>The buckling factors.</value>
        public List<Tuple<StepResultsIdentifier, double>> BucklingFactors
        {
            get
            {
                if (_bucklingFactors == null)
                {
                }

                return _bucklingFactors;
            }
        }
        #endregion

        #region Initialization        
        /// <summary>
        /// Initializes a new instance of the <see cref="MiscResults" /> class.
        /// </summary>
        internal MiscResults()  { }

        #endregion

        #region Fill

        /// <summary>
        /// Empties the results.
        /// </summary>
        public override void EmptyResults()
        {
            _bucklingFactors.Clear();
            _modalLoadParticipationRatios.Clear();
            _modalParticipatingMassRatios.Clear();
            _modalParticipationFactors.Clear();
            _modalPeriods.Clear();
        }


        
        #endregion
    }
}
