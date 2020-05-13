using System.ComponentModel;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    public enum eBridgeDesignAction
    {
        Error = 0,

        [Description("Non-Composite")]
        NonComposite = 1,

        [Description("Short-Term Composite")]
        ShortTermComposite = 2,

        [Description("Long-Term Composite")]
        LongTermComposite = 3,

        Staged = 4,

        Other = 5
    }
}
