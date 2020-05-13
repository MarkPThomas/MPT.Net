#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program.Model.Design
{
    public class AluminumDesigner : DesignerMetal
    {
    
        #region Initialization


        internal AluminumDesigner(ApiCSiApplication app) : base(app) { }
        #endregion
    }
}
#endif
