#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    public class SteelColdFormedDesigner : DesignerMetal
    {

        #region Initialization


        protected SteelColdFormedDesigner(ApiCSiApplication app) : base(app) { }
        #endregion
    }
}
#endif
