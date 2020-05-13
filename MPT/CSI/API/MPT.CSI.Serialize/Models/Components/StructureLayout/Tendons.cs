using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    public class Tendons : ObjectLists<Tendon>
    {
        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Tendon fillNewItem(string uniqueName)
        {
            return Tendon.Factory(uniqueName);
        }
        #endregion
    }
}