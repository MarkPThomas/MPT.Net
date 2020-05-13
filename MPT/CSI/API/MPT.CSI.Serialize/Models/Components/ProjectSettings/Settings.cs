namespace MPT.CSI.Serialize.Models.Components.ProjectSettings
{
    public class Settings
    {
        public ProgramInformation Program { get; internal set; } = new ProgramInformation();

        public ProjectInformation ProjectInformation { get; internal set; } = new ProjectInformation();

        public ModelInformation ModelInformation { get; internal set; } = new ModelInformation();
    }
}
