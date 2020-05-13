#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiTendonObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.TendonObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    public class Tendons : CSiOoApiBaseBase
    {
#region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        private ApiTendonObject _tendonObject => _apiApp?.Model?.ObjectModel?.TendonObject;

        /// <summary>
        /// All tendons loaded from the application instance.
        /// </summary>
        /// <value>The items.</value>
        public Dictionary<string, Tendon> Items { get; } = new Dictionary<string, Tendon>();
#endregion

#region Initialization
        internal Tendons(ApiCSiApplication app) : base(app)
        {
        }
#endregion

#region Factories



#endregion

#region Fill



#endregion
    }
}
    
#endif