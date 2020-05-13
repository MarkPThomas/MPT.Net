using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    public class Cable : Object
    {
        protected static CableObject _cableObject => Registry.ObjectModeler?.CableObject;

        /// <summary>
        /// Returns an object of the specified name.
        /// </summary>
        /// <param name="uniqueName">Name of the unique object.</param>
        /// <returns>Tendon.</returns>
        public static Cable Factory(string uniqueName)
        {
            return Factory(uniqueName, _cableObject, Registry.Cables);
        }



        public Cable(string name = "") : base(name)
        { }

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
