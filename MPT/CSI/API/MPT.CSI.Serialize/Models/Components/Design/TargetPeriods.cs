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
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Components.Loads.Cases;
using MPT.CSI.Serialize.Models.Helpers.Design;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    /// <summary>
    /// Class TargetPeriods. This class cannot be inherited.
    /// </summary>
    public sealed class TargetPeriods
    {
        #region Fields & Properties
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

                //LoadCase loadCase = _loadCases.FillItem(ModalCaseName);
                //if (loadCase is Modal modalCase)
                //{
                //    _modalCase = modalCase;
                //}
                //else
                //{
                //    return null;
                //}

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
                }

                return new ReadOnlyCollection<TargetPeriod>(_targetPeriods);
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetPeriods"/> class.
        /// </summary>
        /// <param name="loadCases">The load cases.</param>
        public TargetPeriods(LoadCases loadCases)
        {
            _loadCases = loadCases;
        }


        #endregion

        #region Fill/Set
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
        private void set(
            List<TargetPeriod> periods, 
            Modal modalCase, 
            bool allSpecifiedTargetsActive)
        {
            _allSpecifiedPeriodTargetsActive = allSpecifiedTargetsActive;
            _modalCaseName = modalCase.Name;
            _modalCase = modalCase;
            _targetPeriods = periods;
        }
        #endregion
    }
}
