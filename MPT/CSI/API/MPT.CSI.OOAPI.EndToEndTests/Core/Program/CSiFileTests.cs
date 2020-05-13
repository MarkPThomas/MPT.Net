using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MPT.CSI.OOAPI.Core;
using NUnit.Framework;

namespace MPT.CSI.OOAPI.EndToEndTests.Core.Program
{
    [TestFixture]
    public class CSiFileTests
    {
        protected CSiApplication _app;

        [SetUp]
        public void Setup()
        {
            _app = CSiApplication.Factory(CSiData.pathApp);
        }

        [TearDown]
        public void TearDown()
        {
            _app.Dispose();
        }

        [Test]
        public void Open_Valid_Filepath_Opens_File()
        {
            string path = Path.Combine(CSiData.pathResources, CSiData.pathModelQuery + CSiData.extension);
            bool fileIsOpened = _app.Model.File.Open(path);
            Assert.That(fileIsOpened);
        }

        [Test]
        public void ModelIsLoaded_Is_False_For_No_Model_Opened_Or_Initialized()
        {
            Assert.IsFalse(_app.Model.File.ModelIsLoaded());
        }

        [Test]
        public void ModelIsLoaded_Is_False_For_New_Model()
        {
            _app.Model.File.NewBlank();
            Assert.IsFalse(_app.Model.File.ModelIsLoaded());
        }

        [Test]
        public void ModelIsLoaded_Is_True_For_Opened_Model()
        {
            string existingFileName = CSiData.pathModelQuery + CSiData.extension;
            string path = Path.Combine(CSiData.pathResources, existingFileName);
            bool fileIsOpened = _app.Model.File.Open(path);
            Assert.That(fileIsOpened);

            Assert.IsTrue(_app.Model.File.ModelIsLoaded());
        }
        
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017

        [Test]
        public void New2DFrame(e2DFrameType tempType,
            int numberStories,
            double storyHeight,
            int numberBays,
            double bayWidth,
            bool restraint = true,
            string beam = "Default",
            string column = "Default",
            string brace = "Default")
        {
            Assert.IsTrue(false);
        }
#endif
#if BUILD_SAP2000v19 || BUILD_SAP2000v20

        [Test]
        public void New3DFrame(e3DFrameType tempType,
            int numberStories,
            double storyHeight,
            int numberBaysX, int numberBaysY,
            double bayWidthX, double bayWidthY,
            bool restraint = true,
            string beam = "Default",
            string column = "Default",
            string area = "Default",
            int numberXDivisions = 4, int numberYDivisions = 4)
        {
            Assert.IsTrue(false);
        }
        [Test]
        public void NewBeam(int numberSpans,
            double spanLength,
            bool restraint = true,
            string beam = "Default")
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void NewWall(int numberXDivisions,
            int numberZDivisions,
            double divisionWidthX,
            double divisionWidthZ,
            bool restraint = true,
            string area = "Default")
        {
            Assert.IsTrue(false);
        }

        [Test]
        public void NewSolidBlock(double xWidth,
            double yWidth,
            double height,
            bool restraint = true,
            string solid = "Default",
            int numberXDivisions = 5,
            int numberYDivisions = 8,
            int numberZDivisions = 10)
        {
            Assert.IsTrue(false);
        }

#elif BUILD_ETABS2013 || BUILD_ETABS2014 || BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017        
        
        [Test]
        public void NewGridOnly(int numberOfStorys,
                    double typicalStoryHeight,
                    double bottomStoryHeight,
                    int numberLinesX,
                    int numberLinesY,
                    double spacingX,
                    double spacingY)
        {
          Assert.IsTrue(false);
        }

        [Test]
        public void NewSteelDeck(int numberOfStorys,
                    double typicalStoryHeight,
                    double bottomStoryHeight,
                    int numberLinesX,
                    int numberLinesY,
                    double spacingX,
                    double spacingY)
        {
            Assert.IsTrue(false);
        }
#endif
    }
}
