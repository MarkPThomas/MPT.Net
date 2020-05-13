using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;
using MPT.CSI.Serialize.Models.Components.ProjectSettings.Misc;
using MPT.CSI.Serialize.Models.Helpers.Analysis;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;
using MPT.CSI.Serialize.SAP2000;
using NUnit.Framework;

namespace MPT.CSI.Serialize.EndToEndTests.SAP2000
{
    [TestFixture]
    public class SAP2000ReaderTests
    {
        public static string path = @"D:\GitHub\personal\MPT.Net\MPT\CSI\API\MPT.CSI.Serialize.SAP2000\RCDF 2004 CFD Ex003.$2k";

        [Test]
        public void FillModelFromTable_Fills_ACTIVE_DEGREES_OF_FREEDOM()
        {
            SAP2000Reader reader = new SAP2000Reader();
            Tables table = Reader.ReadFile(path, reader);

            Assert.IsTrue(table.GenerateModelFromTables(reader));

            DegreesOfFreedomGlobal dofs = table.Model.Settings.ModelInformation.ActiveDegreesOfFreedom;
            Assert.IsTrue(dofs.UX);
            Assert.IsFalse(dofs.UY);
            Assert.IsTrue(dofs.UZ);
            Assert.IsFalse(dofs.RX);
            Assert.IsTrue(dofs.RY);
            Assert.IsFalse(dofs.RZ);
        }

        [Test]
        public void FillModelFromTable_Fills_ANALYSIS_OPTIONS()
        {
            SAP2000Reader reader = new SAP2000Reader();
            Tables table = Reader.ReadFile(path, reader);
            
            Assert.IsTrue(table.GenerateModelFromTables(reader));

            Analyzer analyzer = table.Model.Analysis.Analyzer;
            Assert.AreEqual(eSolverType.Advanced, analyzer.SolverType);
            Assert.AreEqual(eSolverProcessType.Auto, analyzer.SolverProcessType);
            Assert.IsFalse(analyzer.Force32BitSolver);
            Assert.AreEqual(Constants.NONE, analyzer.StiffnessCase);
            Assert.AreEqual(Constants.NONE, analyzer.UndeformedGeometryModificationType);
            Assert.AreEqual("\"In Elements\"", analyzer.HingeOption);
        }

        [Test]
        public void FillModelFromTable_Fills_AUTO_COMBINATION_OPTION_DATA_01_GENERAL()
        {
            SAP2000Reader reader = new SAP2000Reader();
            Tables table = Reader.ReadFile(path, reader);

            Assert.IsTrue(table.GenerateModelFromTables(reader));

            ConcreteDesigner concrete = table.Model.Design.ConcreteDesigner;
            Assert.IsFalse(concrete.AutogenerateLoadCombinations);
        }

        [Test]
        public void FillModelFromTable_Fills_AUTO_WAVE_3_WAVE_CHARACTERISTICS_GENERAL()
        {
            SAP2000Reader reader = new SAP2000Reader();
            Tables table = Reader.ReadFile(path, reader);

            Assert.IsTrue(table.GenerateModelFromTables(reader));

            WaveCharacteristics waveCharacteristics = table.Model.Settings.ModelInformation.GeneralWaveCharacteristics;
            Assert.AreEqual(Constants.DEFAULT, waveCharacteristics.Characteristics);
            Assert.AreEqual("\"From Theory\"", waveCharacteristics.WaveType);
            Assert.AreEqual(1, waveCharacteristics.KinematicsFactor);
            Assert.AreEqual(150, waveCharacteristics.StormWaterDepth);
            Assert.AreEqual(60, waveCharacteristics.Height);
            Assert.AreEqual(12, waveCharacteristics.Period);
            Assert.AreEqual("Linear", waveCharacteristics.Theory);
        }
    }
}
