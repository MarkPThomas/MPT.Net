using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.CompositeBeam
{
    public class AISC_360_05_Preferences: CompositeBeamDesignPreferenceProperties
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
