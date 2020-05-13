using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Properties;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections
{
    public class SolidProperties : ObjectLists<SolidProperty>
    {
        #region Fields & Properties
        /// <summary>
        /// The materials
        /// </summary>
        protected readonly Materials.Materials _materials;
        #endregion

        #region Initialization

        internal SolidProperties(Materials.Materials materials)
        {
            _materials = materials;
        }
        #endregion

        protected override SolidProperty fillNewItem(string uniqueName)
        {
            return SolidProperty.Factory(_materials, uniqueName);
        }
    }
}
