using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ShearWall
{
    public class BS_8110_1997_Preferences : ShearWallDesignPreferenceProperties
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
