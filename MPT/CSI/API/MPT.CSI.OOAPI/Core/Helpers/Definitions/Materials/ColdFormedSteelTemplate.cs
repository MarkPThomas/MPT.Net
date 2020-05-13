#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using System;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;

namespace MPT.CSI.OOAPI.Core.Helpers.Definitions.Materials
{
    public class ColdFormedSteelTemplate : MaterialTemplate
    {
        public ColdFormedSteelTemplate(
            eMaterialRegion region, 
            string standard, 
            string grade) : base(eMaterialPropertyType.ColdFormed, region, standard, grade)
        {
        }

    

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
#endif