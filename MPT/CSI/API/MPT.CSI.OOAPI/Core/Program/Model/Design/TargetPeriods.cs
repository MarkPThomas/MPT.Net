// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-25-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="TargetPeriods.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Design;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    /// <summary>
    /// Class TargetPeriods. This class cannot be inherited.
    /// </summary>
    public sealed class TargetPeriods
    {
        #region Fields & Properties

        /// <summary>
        /// The API target period
        /// </summary>
        private readonly ITargetPeriod _apiTargetPeriod;

        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// All specified period targets active
        /// </summary>
        private bool? _allSpecifiedPeriodTargetsActive;
        /// <summary>
        /// True: All specified period targets are active.
        /// False: They are inactive.
        /// </summary>
        /// <value><c>true</c> if [all specified period targets active]; otherwise, <c>false</c>.</value>
        public bool AllSpecifiedPeriodTargetsActive
        {
            get
            {
                if (_allSpecifiedPeriodTargetsActive == null)
                {
                    Fill();
                }

                return _allSpecifiedPeriodTargetsActive ?? false;
            }
        }

        /// <summary>
        /// The modal case name
        /// </summary>
        private string _modalCaseName;
        /// <summary>
        /// Name of the modal load case for which the target applies.
        /// </summary>
        /// <value>The modal case.</value>
        internal string ModalCaseName
        {
            get
            {
                if (_modalCaseName == null)
                {
                    Fill();
                }

                return _modalCaseName;
            }
        }

        /// <summary>
        /// The modal case
        /// </summary>
        private Modal _modalCase;
        /// <summary>
        /// The modal load case for which the target applies.
        /// </summary>
        /// <value>The modal case.</value>
        public Modal ModalCase
        {
            get
            {
                if (_modalCase != null) return _modalCase;

                LoadCase loadCase = _loadCases.FillItem(ModalCaseName);
                if (loadCase is Modal modalCase)
                {
                    _modalCase = modalCase;
                }
                else
                {
                    return null;
                }

                return _modalCase;
            }
        }

        /// <summary>
        /// The target periods
        /// </summary>
        private List<TargetPeriod> _targetPeriods;
        /// <summary>
        /// Gets or sets the target periods.
        /// </summary>
        /// <value>The target period.</value>
        public ReadOnlyCollection<TargetPeriod> Periods
        {
            get
            {
                if (_targetPeriods == null)
                {
                    Fill();
                }

                return new ReadOnlyCollection<TargetPeriod>(_targetPeriods);
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetPeriods"/> class.
        /// </summary>
        /// <param name="apiTargetPeriod">The API target period.</param>
        /// <param name="loadCases">The load cases.</param>
        public TargetPeriods(ITargetPeriod apiTargetPeriod, LoadCases loadCases)
        {
            _apiTargetPeriod = apiTargetPeriod;
            _loadCases = loadCases;
        }


        #endregion

        #region Fill/Set
        /// <summary>
        /// Retrieves time period targets for steel design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Fill()
        {
            _apiTargetPeriod.GetTargetPeriod(
                out var modalCase,
                out var modeNumbers,
                out var periodTargets,
                out var allSpecifiedTargetsActive);

            _modalCaseName = modalCase;
            _allSpecifiedPeriodTargetsActive = allSpecifiedTargetsActive;

            _targetPeriods = new List<TargetPeriod>();
            for (int i = 0; i < modeNumbers.Length; i++)
            {
                TargetPeriod targetPeriod = new TargetPeriod()
                {
                    ModeNumber = modeNumbers[i],
                    Value = periodTargets[i]
                };
                _targetPeriods.Add(targetPeriod);
            }
        }

        /// <summary>
        /// Adds the target periods.
        /// </summary>
        /// <param name="periods">The periods.</param>
        public void SetTargetPeriods(List<TargetPeriod> periods)
        {
            set(periods, _modalCase, AllSpecifiedPeriodTargetsActive);
        }

        /// <summary>
        /// Adds the modal case.
        /// </summary>
        /// <param name="modalCase">The modal case.</param>
        public void SetModalCase(Modal modalCase)
        {
            set(_targetPeriods, modalCase, AllSpecifiedPeriodTargetsActive);
        }

        /// <summary>
        /// Activates the target periods.
        /// </summary>
        public void ActivateTargetPeriods()
        {
            set(_targetPeriods, _modalCase, allSpecifiedTargetsActive: true);
        }

        /// <summary>
        /// Deactivates the target periods.
        /// </summary>
        public void DeactivateTargetPeriods()
        {
            set(_targetPeriods, _modalCase, allSpecifiedTargetsActive: false);
        }

        /// <summary>
        /// Sets time period targets for steel design.
        /// </summary>
        /// <param name="periods">The periods.</param>
        /// <param name="modalCase">The modal case.</param>
        /// <param name="allSpecifiedTargetsActive">True: All specified targets are active.
        /// False: They are inactive.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        private void set(
            List<TargetPeriod> periods, 
            Modal modalCase, 
            bool allSpecifiedTargetsActive)
        {
            _apiTargetPeriod.SetTargetPeriod(
                modalCase.Name,
                periods.Select(o => o.ModeNumber).ToArray(), 
                periods.Select(o => o.Value).ToArray(), 
                allSpecifiedTargetsActive);

            _allSpecifiedPeriodTargetsActive = allSpecifiedTargetsActive;
            _modalCaseName = modalCase.Name;
            _modalCase = modalCase;
            _targetPeriods = periods;
        }
        #endregion
    }
}
