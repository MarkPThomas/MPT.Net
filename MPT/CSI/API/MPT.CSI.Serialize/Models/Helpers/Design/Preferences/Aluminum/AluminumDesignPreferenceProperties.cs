using System;
using System.ComponentModel;
using MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ColdFormed;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Aluminum
{
    public abstract class AluminumDesignPreferenceProperties : ModelProperty, IFrameDesign
    {
        /// <summary>
        /// Indicates how results for multivalued cases (Time history, Nonlinear static or Multi-step static) are considered in the design.
        /// </summary>
        /// <value>The multi response case.</value>
        public eMultiResponseCase MultiResponseCase { get; set; } = eMultiResponseCase.Envelopes;

        /// <summary>
        /// The demand/capacity ratio limit to be used for acceptability.
        /// D/C ratios that are less than or equal to this value are considered acceptable.
        /// </summary>
        /// <value>The demand capacity ratio limit.</value>
        public double DemandCapacityRatioLimit { get; set; } = 1;

        /// <summary>
        /// Gets or sets the maximum iterations.
        /// </summary>
        /// <value>The maximum iterations.</value>
        public int MaximumIterations { get; set; } = 1;

        /// <summary>
        /// Types of frame used for ductility considerations in the design.
        /// </summary>
        public enum FrameTypes
        {
            /// <summary>
            /// Moment Frame
            /// </summary>
            [Description("Moment Frame")]
            MomentFrame = 1,

            /// <summary>
            /// Braced Frame
            /// </summary>
            [Description("Braced Frame")]
            BracedFrame = 2,

            /// <summary>
            /// Braced Frame
            /// </summary>
            [Description("Program Determined")]
            ProgramDetermined = 3
        }

        /// <summary>
        /// Type of frame used for ductility considerations in the design.
        /// </summary>
        /// <value>The type of the frame.</value>
        public FrameTypes FrameType { get; set; } = FrameTypes.MomentFrame;

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>System.Object.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
