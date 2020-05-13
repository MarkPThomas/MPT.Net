using MPT.CSI.API.Core.Program.ModelBehavior.Definition;

namespace MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings
{
    public static class Constants
    {
        public static string CoordinateSystem { get; set; } = CoordinateSystems.Global;
        public const string None = "None";
        public static bool FillAllProperties { get; set; } = true;
        
        public static double Tolerance { get; set; }
    }
}
