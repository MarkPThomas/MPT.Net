using System.Collections.Generic;
using MPT.CSI.API.Core.Program;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using ProgramStory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.Story;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions
{
    public class Story
    {
        protected static ProgramStory _app => Registry.ProgramDefinitions.Abstractions.Story;

        public string Name { get; protected set; }

        public Story(string name)
        {
            Name = name;
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Returns the names of all defined point object properties for a given story.
        /// </summary>
        public List<string> GetNodeNameList(CSiApplication app)
        {
            return new List<string>(app.Model.ObjectModel.PointObject.GetNameListOnStory(Name));
        }

        /// <summary>
        /// Returns the names of all defined frame object properties for a given story.
        /// </summary>
        public List<string> GetFrameNameList(CSiApplication app)
        {
            return new List<string>(app.Model.ObjectModel.FrameObject.GetNameListOnStory(Name));
        }


        /// <summary>
        /// Returns the names of all defined area properties for a given story.
        /// </summary>
        public List<string> GetAreaNameList(CSiApplication app)
        {
            return new List<string>(app.Model.ObjectModel.AreaObject.GetNameListOnStory(Name));
        }
#endif
    }
}
