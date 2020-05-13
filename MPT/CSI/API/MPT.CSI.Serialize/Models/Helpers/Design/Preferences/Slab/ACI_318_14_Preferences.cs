using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.Slab
{
    public class ACI_318_14_Preferences : SlabDesignPreferenceProperties
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
