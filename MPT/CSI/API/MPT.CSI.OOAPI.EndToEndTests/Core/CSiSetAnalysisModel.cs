using MPT.CSI.API.Core.Program;
using NUnit.Framework;

namespace MPT.CSI.OOAPI.EndToEndTests.Core
{
    [TestFixture]
    public abstract class CsiSetAnalysisModel : CsiSet
    {
        [SetUp]
        public new void Setup()
        {
            _app.Model.Analyze.CreateAnalysisModel();
        }
    }
}
