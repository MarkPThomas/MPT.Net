using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Slab
{
    public class NZS_3101_2006_Preferences : SlabDesignPreferenceProperties
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
