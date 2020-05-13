using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.CompositeBeam
{
    public class CSA_S16_09_Preferences : CompositeBeamDesignPreferenceProperties
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
