using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    public class Solids : ObjectLists<Solid>
    {
        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Solid fillNewItem(string uniqueName)
        {
            return Solid.Factory(uniqueName);
        }
        #endregion
    }
}
