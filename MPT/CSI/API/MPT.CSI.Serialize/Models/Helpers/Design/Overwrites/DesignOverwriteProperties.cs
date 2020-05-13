using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites
{
    public class DesignOverwriteProperties : ModelProperty
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
