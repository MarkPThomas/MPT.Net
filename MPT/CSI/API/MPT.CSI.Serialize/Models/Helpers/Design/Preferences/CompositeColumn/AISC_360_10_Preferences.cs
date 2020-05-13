using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.CompositeColumn
{
    public class AISC_360_10_Preferences: CompositeColumnDesignPreferenceProperties
    {
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
