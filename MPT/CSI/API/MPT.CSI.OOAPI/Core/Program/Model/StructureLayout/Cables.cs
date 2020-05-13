#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiCableObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.CableObject;

namespace MPT.CSI.OOAPI.Core.Program.Model.StructureLayout
{
    public class Cables : CSiOoApiBaseBase
    {
#region Fields & Properties

        /// <summary>
        /// The API application object.
        /// </summary>
        private ApiCableObject _cableObject => _apiApp?.Model?.ObjectModel?.CableObject;



        /// <summary>
        /// All cables loaded from the application instance.
        /// </summary>
        /// <value>The items.</value>
        public Dictionary<string, Cable> Items { get; } = new Dictionary<string, Cable>();
#endregion

#region Initialization

        internal Cables(ApiCSiApplication app) : base(app)
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