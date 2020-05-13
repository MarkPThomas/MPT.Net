using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.CompositeBeam
{
    public class EN_4_2004_Preferences : CompositeBeamDesignPreferenceProperties
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
