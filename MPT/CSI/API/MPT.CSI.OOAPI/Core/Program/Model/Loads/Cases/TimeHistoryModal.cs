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

using MPT.CSI.OOAPI.Core.Program.Model.Analysis;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    /// <summary>
    /// Class TimeHistoryModal.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases.TimeHistory" />
    public abstract class TimeHistoryModal : TimeHistory
    {
        #region Fields & Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        
        protected IModal _apiModal;
        protected IDampingModal _apiDampingModal;

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
                _modalCase = new ModalCaseHelper(Name, _apiModal, _apiApp, _apiModal);
                _modalCase.FillModalCase();

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
                _damping = new DampingHelper(_apiDampingModal, Name);
                _damping.FillData();

                return _damping;
            }
        }
#endif
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeHistoryModal" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected TimeHistoryModal(ApiCSiApplication app, Analyzer analyzer, string name) : base(app, analyzer, name)
        {
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            
            Damping.FillData();
#endif
        }

#endregion
    }
}
