﻿using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.ShearWall
{
    public class CSA_A23_3_04_Preferences : ShearWallDesignPreferenceProperties
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
