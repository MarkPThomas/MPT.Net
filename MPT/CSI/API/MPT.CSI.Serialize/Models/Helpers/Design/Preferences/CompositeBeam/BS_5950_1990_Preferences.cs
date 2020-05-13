using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.CompositeBeam
{
    public class BS_5950_1990_Preferences : CompositeBeamDesignPreferenceProperties
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
