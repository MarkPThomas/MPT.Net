
using System.Diagnostics;
using System.IO;
using MPT.CSI.OOAPI.Core;
using NUnit.Framework;
//using MPT.CSI.API.Core.Program;

namespace MPT.CSI.OOAPI.EndToEndTests.Core.Program
{
    [TestFixture]
    public class CSiApplicationTests : BaseTests
    {
        [Test]
        public void CSiApplication_Initialize_New_Instance_Defaults()
        {
            bool programWasOpened;
            using (CSiApplication app = CSiApplication.Factory(CSiData.pathApp))
            {
                Assert.That(app.IsInitialized);
                programWasOpened = app.IsInitialized;
            }
            Assert.IsTrue(programWasOpened);
        }

#if !BUILD_SAP2000v18 && !BUILD_SAP2000v17 && !BUILD_SAP2000v16 && !BUILD_CSiBridgev18 && !BUILD_CSiBridgev17 && !BUILD_CSiBridgev16 && !BUILD_ETABS2015
        [Test]
        public void CSiApplication_Initialize_New_Instance_By_Object_With_Defaults()
        {
            bool programWasOpened;
            using (CSiApplication app = CSiApplication.Factory())
            {
                programWasOpened = app.IsInitialized;
            }
            Assert.IsTrue(programWasOpened);
        }


        [Test]
        public void CSiApplication_Initialize_AttachToProcess()
        {
            // This test should wait until all processes are closed.
            Process[] pname = Process.GetProcessesByName(CSiData.processName);
            delayTestStart(until: (pname.Length == 0), attempts: 20, wait: 1000);
            
            ProcessStartInfo processInfo = new ProcessStartInfo(CSiData.pathApp)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(processInfo);
            
            bool programWasAttachedTo;
            using (CSiApplication app = CSiApplication.Factory(numberOfAttempts: 60, intervalBetweenAttempts: 1000,
                                                           numberOfExitAttempts: 60, intervalBetweenExitAttempts: 1000))
            {
                Assert.IsTrue(app.IsInitialized);
                programWasAttachedTo = app.IsInitialized;
            }
            Assert.IsTrue(programWasAttachedTo);
        }
#endif
        
        [Test]
        public void CSiApplication_Application_Start_with_Valid_Model_Path()
        {
            bool programWasOpened;
            using (CSiApplication app = CSiApplication.Factory(CSiData.pathApp, modelPath: Path.Combine(CSiData.pathResources, CSiData.pathModelDemo + CSiData.extension)))
            {
                Assert.That(app.IsInitialized);
                programWasOpened = app.IsInitialized;
            }
            Assert.IsTrue(programWasOpened);
        }
    }
}
