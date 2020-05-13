#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Frames;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.Areas
{
    class WallAutoSelect
    {
        // TODO: Complete WallAutoSelect as a composite section
        /// <summary>
        /// Gets the wall automatic select list.
        /// </summary>
        /// <param name="name">The name of an existing auto select list wall property.</param>
        /// <param name="autoSelectList">The names of the wall properties included in the auto select list.</param>
        /// <param name="startingProperty">This is Median or the name of a wall property in the AutoSelectList array. 
        /// It is the starting section for the auto select list. 
        /// Median indicates the Median Property by Thickness.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void GetWallAutoSelectList(string name,
            ref string[] autoSelectList,
            ref string startingProperty)
        {

        }

        /// <summary>
        /// Initializes property data for an auto select list wall section.
        /// </summary>
        /// <param name="name">The name of an existing auto select list wall property.</param>
        /// <param name="autoSelectList">The names of the wall properties included in the auto select list.</param>
        /// <param name="startingProperty">This is Median or the name of a wall property in the AutoSelectList array. 
        /// It is the starting section for the auto select list. 
        /// Median indicates the Median Property by Thickness.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetWallAutoSelectList(string name,
            string[] autoSelectList,
            string startingProperty = "Median")
        {

        }
    }
}
#endif
