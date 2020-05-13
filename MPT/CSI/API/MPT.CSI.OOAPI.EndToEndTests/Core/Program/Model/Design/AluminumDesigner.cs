namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
    public class AluminumDesigner : DesignerMetal
    {

        public static AluminumDesigner Instance { get; } = new AluminumDesigner();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static AluminumDesigner()
        {
        }

        private AluminumDesigner()
        {
        }
    }
#endif
}
