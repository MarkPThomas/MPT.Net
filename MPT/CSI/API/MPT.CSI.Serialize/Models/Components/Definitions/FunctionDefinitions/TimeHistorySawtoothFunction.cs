namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    public class TimeHistorySawtoothFunction : TimeHistoryFunction
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the period.
        /// </summary>
        /// <value>The period.</value>
        public double Period { get; internal set; }

        /// <summary>
        /// Gets the amplitude.
        /// </summary>
        /// <value>The amplitude.</value>
        public double Amplitude { get; internal set; }

        /// <summary>
        /// Gets the number of cycles.
        /// </summary>
        /// <value>The number of cycles.</value>
        public int NumberOfCycles { get; internal set; }

        /// <summary>
        /// Gets the ramp time.
        /// </summary>
        /// <value>The ramp time.</value>
        public double RampTime { get; internal set; }
        #endregion

        #region Initialization
        internal static TimeHistorySawtoothFunction Factory(string uniqueName)
        {
            return new TimeHistorySawtoothFunction(uniqueName);
        }

        internal TimeHistorySawtoothFunction(string name) : base(name)
        {
        }
        #endregion
    }
}
