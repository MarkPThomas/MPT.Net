using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    /// <summary>
    /// All design preferences applicable to a frame of a given material design type.
    /// </summary>
    /// <typeparam name="T">The type of code preferences common to a material.</typeparam>
    /// <seealso cref="MPT.CSI.Serialize.Models.Support.TypeList{T}" />
    public class FrameDesignPreferences<T>: TypeList<T> where T : DesignPreferences, IFrameDesign, new()
    {
        // Preferences are specific to a code but are recoverable after swapping codes
    }
}
