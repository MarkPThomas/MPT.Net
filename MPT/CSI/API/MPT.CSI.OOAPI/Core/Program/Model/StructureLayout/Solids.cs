#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiSolidObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.SolidObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    public class Solids : CSiOoApiBaseBase
    {
#region Fields & Properties
        /// <summary>
        /// The API application object.
        /// </summary>
        private ApiSolidObject _solidObject => _apiApp?.Model?.ObjectModel?.SolidObject;

        /// <summary>
        /// All solids loaded from the application instance.
        /// </summary>
        /// <value>The items.</value>
        public Dictionary<string, Solid> Items { get; } = new Dictionary<string, Solid>();
#endregion

#region Initialization
        internal Solids(ApiCSiApplication app) : base(app)
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
