using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ShearWall
{
    public class ACI_318_08_Preferences: ShearWallDesignPreferenceProperties
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
