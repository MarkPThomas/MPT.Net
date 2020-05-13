using System;
using System.Diagnostics;
using NUnit.Framework;

namespace MPT.Files.UnitTests
{
    [TestFixture]
    public class FileLibraryTests_Processes_Programs : FileLibraryTests_Base
    {
        protected string processName = "NotePad";

        protected void startProcess()
        {
            Process.Start(processName + ".exe", _pathOriginal);
        }

        protected void endProcesses()
        {
            Process[] processes = Process.GetProcessesByName(processName);
            try
            {
                foreach (Process process in processes)
                {
                    process.Kill();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [SetUp]
        public override void Init()
        {
            base.Init();
            endProcesses();
        }

        [TearDown]
        public override void CleanUp()
        {
            endProcesses();
            base.CleanUp();
        }

        [Test]
        public void ProcessIsRunning_Is_Not_Running_If_Not_Running()
        {
            Assert.IsFalse(FileLibrary.IsProcessRunning(processName));
        }

        [Test]
        public void ProcessIsRunning_Is_Running_If_Running()
        {
            Assert.IsFalse(FileLibrary.IsProcessRunning(processName));

            startProcess();
            Assert.IsTrue(FileLibrary.IsProcessRunning(processName));
        }

        [Test]
        public void ProcessIsResponding_Responds_If_Present()
        {
            startProcess();
            Assert.IsTrue(FileLibrary.IsProcessResponding(processName));
        }

        [Test]
        public void ProcessIsResponding_Does_Not_Respond_If_Not_Present()
        {
            Assert.IsFalse(FileLibrary.IsProcessResponding(processName));
        }

        [Test]
        public void ProcessIsResponding_Does_Not_Respond_If_Not_Responding()
        {
            startProcess();

            Assert.IsFalse(FileLibrary.IsProcessResponding(processName));
        }

        [Test]
        public void EndProcess_Ends_Process_If_Present()
        {
            Assert.IsFalse(FileLibrary.IsProcessRunning(processName));

            startProcess();
            Assert.IsTrue(FileLibrary.IsProcessRunning(processName));

            FileLibrary.EndProcess(processName);
            Assert.IsFalse(FileLibrary.IsProcessRunning(processName));
        }

        [Test]
        public void EndProcess_Does_Nothing_If_Not_Present()
        {
            FileLibrary.EndProcess(processName);
            Assert.IsFalse(FileLibrary.IsProcessRunning(processName));
        }
    }
}
