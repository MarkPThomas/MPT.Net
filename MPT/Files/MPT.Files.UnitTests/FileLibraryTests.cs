using System;
using System.IO;
using NUnit.Framework;

namespace MPT.Files.UnitTests
{
    [TestFixture]
    public class FileLibraryTests : FileLibraryTests_Base
    {

        [TearDown]
        public override void CleanUp()
        {
            cleanupFiles(_pathOriginal);
            cleanupFiles(_pathCopied);
        }

        #region Querying
        [Test]
        public void GetFileDateModified_Gets_File_Date_When_Modified()
        {
            _pathOriginal = Path.Combine(_assemblyFolder, _fileNameOriginal);

            if (File.Exists(_pathOriginal))
            {
                cleanupFiles(_pathOriginal);
            }

            var myFile = File.Create(_pathOriginal);
            myFile.Close();

            string expectedFileDate = DateTime.Today.ToShortDateString();

            Assert.AreEqual(expectedFileDate, FileLibrary.GetFileDateModified(_pathOriginal));
        }
        #endregion

        #region File Access
        [Test]
        public void OpenFile()
        {

        }

        [Test]
        public void FileInUse()
        {

        }

        [Test]
        public void FileInUseAction()
        {

        }

        [Test]
        public void WaitUntilFileAvailable()
        {

        }
        #endregion

        #region Create File
        [Test]
        public void WriteTextFile_Of_New_File_Writes_New_File()
        {

        }

        [Test]
        public void WriteTextFile_Of_Existing_File_Appends_To_File()
        {

        }

        [Test]
        public void WriteTextFile_Of_Existing_File_Replaces_File()
        {

        }

        [Test]
        public void WriteTextFile_Of_Empty_FileName_xxxx()
        {

        }

        [Test]
        public void WriteTextFile()
        {

        }
        #endregion

        #region Initialization File
        [Test]
        public void ReadIniFile()
        {

        }

        [Test]
        public void WriteIniFile()
        {

        }

        [Test]
        public void InitializeInstallIniFile()
        {

        }

        [Test]
        public void ChangeInstallIniFile()
        {

        }
        #endregion
    }
}
