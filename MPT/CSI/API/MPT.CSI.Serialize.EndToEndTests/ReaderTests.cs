using MPT.CSI.Serialize.SAP2000;
using NUnit.Framework;

namespace MPT.CSI.Serialize.EndToEndTests
{
    [TestFixture]
    public class ReaderTests
    {
        // TODO: Mock an adaptor
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("FooBar.$2k")]
        [TestCase("FooBar.$et")]
        public void ReadFile_Returns_Empty_When_Path_Or_File_Invalid(string path)
        {
            Tables table = Reader.ReadFile(path, new SAP2000Reader());
            Assert.IsFalse(table.HasTables);
        }
        
        [TestCase(@"D:\GitHub\personal\MPT.Net\MPT\CSI\API\MPT.CSI.Serialize.SAP2000\RCDF 2004 CFD Ex003.$2k")]
        [TestCase(@"D:\GitHub\personal\MPT.Net\MPT\CSI\API\MPT.CSI.Serialize.SAP2000\RCDF 2004 CFD Ex003.$et")]
        public void ReadFile_Reads_Valid_Existing_File_To_Tables(string path)
        {
            Tables table = Reader.ReadFile(path, new SAP2000Reader());
            Assert.IsTrue(table.HasTables);
        }
    }
}
