using System.Collections.Generic;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    public class Tendon : Object
    {
        protected static TendonSection _tendonProperties => Registry.ProgramDefinitions?.Properties?.TendonSection;

        /// <summary>
        /// Returns an object of the specified name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique object.</param>
        /// <returns>Tendon.</returns>
        public static Tendon Factory(string uniqueName)
        {
            return Factory(uniqueName, _tendonProperties, Registry.Tendons);
        }

        public Tendon() : base(string.Empty)
        { }

        public Tendon(string name = "") : base(name)
        {
        }

        public override void FillData()
        {

        }


#region Loading

#endregion

        public override List<string> GetNameListOnStory()
        {
            throw new System.NotImplementedException();
        }

        public override void FillNameFromLabel()
        {
            throw new System.NotImplementedException();
        }

        public override void FillLabelFromName()
        {
            throw new System.NotImplementedException();
        }

        public override void FillGUID()
        {
            throw new System.NotImplementedException();
        }

        public override void SetGUID()
        {
            throw new System.NotImplementedException();
        }

        public override void FillElement()
        {
            throw new System.NotImplementedException();
        }

        public override void FillTransformationMatrix()
        {
            throw new System.NotImplementedException();
        }

        public override void FillLocalAxes()
        {
            throw new System.NotImplementedException();
        }

        public override void ChangeName(string newName)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete()
        {
            throw new System.NotImplementedException();
        }

        public override void GetSelected()
        {
            throw new System.NotImplementedException();
        }

        public override void FillSpringAssignment()
        {
            throw new System.NotImplementedException();
        }

        public override void SetSpringAssignment()
        {
            throw new System.NotImplementedException();
        }
    }
#endif
}
