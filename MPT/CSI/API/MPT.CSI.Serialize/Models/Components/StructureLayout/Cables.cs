using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.StructureLayout
{
    public class Cables : ObjectLists<Cable>
    {
        #region Fill        
        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected override Cable fillNewItem(string uniqueName)
        {
            return Cable.Factory(uniqueName);
        }
        #endregion
    }
}