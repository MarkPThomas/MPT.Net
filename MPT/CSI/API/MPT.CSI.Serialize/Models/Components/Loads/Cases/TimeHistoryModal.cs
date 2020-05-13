// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-10-2018
// ***********************************************************************
// <copyright file="TimeHistoryModal.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Class TimeHistoryModal.
    /// </summary>
    /// <seealso cref="TimeHistory" />
    public abstract class TimeHistoryModal : TimeHistory
    {
        #region Fields & Properties
        public virtual eMotionType MotionType { get; internal set; }


        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

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
                _modalCase = ModalCaseHelper.Factory(Name, _loadCases);

                return _modalCase;
            }
        }
        
        /// <summary>
        /// The damping associate with the load case.
        /// </summary>
        /// <value>The damping.</value>
        private DampingHelper _damping;
        /// <summary>
        /// The damping associate with the load case.
        /// </summary>
        /// <value>The damping.</value>
        public DampingHelper Damping
        {
            get
            {
                if (_damping != null) return _damping;
                _damping = new DampingHelper(Name);

                return _damping;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryModal" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryModal(
            Analyzer analyzer,
            LoadCases loadCases, 
            string name) : base(analyzer, name)
        {
            _loadCases = loadCases;
        }

        #endregion
    }
}
