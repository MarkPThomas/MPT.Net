using System.ComponentModel;

namespace MPT.Reporting.Core
{
    /// <summary>
    /// Enumerations for common prompt actions. To be used for custom forms in the program.
    /// </summary>
    /// <remarks></remarks>
    public enum eMessageActions
    {
        None = 0,
        [Description("Yes")] Yes,
        [Description("No")] No,
        [Description("OK")] OK,
        [Description("Cancel")] Cancel,
        [Description("Abort")] Abort,
        [Description("Retry")] Retry,
        [Description("Ignore")] Ignore
    }
}
