using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    public class Aluminum : Material
    {
        public new static Aluminum Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return (Aluminum)Registry.Materials[uniqueName];

            Aluminum material = new Aluminum(uniqueName);
            if (_materialProperties != null)
            {
                material.FillData();
            }
            Registry.Materials.Add(uniqueName, material);
            return material;
        }

        public Aluminum(string name) : base(name)
        {
        }

        public override void FillData()
        {

        }
    }
}
