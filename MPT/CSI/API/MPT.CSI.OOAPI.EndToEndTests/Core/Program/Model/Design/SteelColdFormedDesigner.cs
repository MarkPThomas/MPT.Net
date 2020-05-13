namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    public class SteelColdFormedDesigner : DesignerMetal
    {

        public static SteelColdFormedDesigner Instance { get; } = new SteelColdFormedDesigner();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SteelColdFormedDesigner()
        {
        }

        private SteelColdFormedDesigner()
        {
        }
    }
#endif
}
