namespace MPT.CSI.Serialize.Models.Components.Definitions.FunctionDefinitions
{
    public class TimeHistoryRampFunction : TimeHistoryFunction
    {
        #region Fields & Properties
        /// <summary>
        /// Gets the amplitude.
        /// </summary>
        /// <value>The amplitude.</value>
        public double Amplitude { get; internal set; }

        /// <summary>
        /// Gets the ramp time.
        /// </summary>
        /// <value>The ramp time.</value>
        public double RampTime { get; internal set; }

        /// <summary>
        /// Gets the maximum time.
        /// </summary>
        /// <value>The maximum time.</value>
        public double MaxTime { get; internal set; }
        #endregion

        #region Initialization
        internal static TimeHistoryRampFunction Factory(string uniqueName)
        {
            return new TimeHistoryRampFunction(uniqueName);
        }

        internal TimeHistoryRampFunction(string name) : base(name)
        {
        }
        #endregion
    }
}
