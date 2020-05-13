namespace MPT.CSI.OOAPI.Core.Program.Model.Loads.Cases
{
    public abstract class TimeHistoryModal : TimeHistory
    {
        #region Fields & Properties
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// The modal case associated with the load case.
        /// </summary>
        /// <value>The modal case.</value>
        public ModalCaseHelper ModalCase { get; protected set; }

        /// <summary>
        /// The damping associate with the load case.
        /// </summary>
        /// <value>The damping.</value>
        public DampingHelper Damping { get; protected set; }
#endif

        #endregion

        #region Initialization


        protected TimeHistoryModal(string name) : base(name)
        {
        }

        /// <summary>
        /// Fills the class with all data from the application.
        /// </summary>
        public override void FillData()
        {
            base.FillData();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            ModalCase.FillModalCase();
            Damping.FillData();
#endif
        }

        #endregion
        
        #region API Functions


#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        
        
#endif
        #endregion
    }
}
