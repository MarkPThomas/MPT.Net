using System;

namespace MPT.CSI.Serialize.Models.Helpers.Design.Preferences.CompositeColumn
{
    public abstract class CompositeColumnDesignPreferenceProperties : ModelProperty
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
