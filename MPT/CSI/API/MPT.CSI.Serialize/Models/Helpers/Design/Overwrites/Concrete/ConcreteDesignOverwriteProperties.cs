using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete
{
    public class ConcreteDesignOverwriteProperties : DesignOverwriteProperties
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
