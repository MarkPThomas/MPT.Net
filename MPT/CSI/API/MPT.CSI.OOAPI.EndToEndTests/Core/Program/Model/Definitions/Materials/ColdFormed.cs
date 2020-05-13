using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    public class ColdFormed : Material
    {
        public new static ColdFormed Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (ColdFormed)Registry.Materials[uniqueName];

            ColdFormed material = new ColdFormed(uniqueName);
            if (_materialProperties != null)
            {
                material.FillData();
            }
            Registry.Materials.Add(uniqueName, material);
            return material;
        }

        public ColdFormed(string name) : base(name)
        {
        }

        public override void FillData()
        {

        }
    }
}
