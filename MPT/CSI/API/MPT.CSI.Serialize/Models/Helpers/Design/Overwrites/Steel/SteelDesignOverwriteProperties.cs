﻿using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel
{
    public class SteelDesignOverwriteProperties : DesignOverwriteProperties
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
