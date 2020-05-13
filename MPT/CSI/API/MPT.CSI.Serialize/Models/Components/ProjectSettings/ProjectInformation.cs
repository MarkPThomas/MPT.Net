using System.Collections.Generic;

namespace MPT.CSI.Serialize.Models.Components.ProjectSettings
{
    public class ProjectInformation
    {
        /// <summary>
        /// The project information items.
        /// </summary>
        /// <value>The project information items.</value>
        public List<string> ProjectInfoItems { get; internal set; } = new List<string>();

        /// <summary>
        /// The project information data.
        /// </summary>
        /// <value>The project information data.</value>
        public List<string> ProjectInfoData { get; internal set; } = new List<string>();

        /// <summary>
        /// The user comment.
        /// </summary>
        /// <value>The user comment.</value>
        public string UserComment { get; internal set; }
    }
}
