using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using NCrunch.Framework;

namespace Csi.Testing.ExamplesReleaser.Core.Tests
{
    [TestFixture]
    public class ExampleReleaserTests
    {
        public string RunPath = "";
        public string OtherLocationPath = "";
        public string DestinationPath = "";

        protected List<string> _directoriesToClean;
        protected List<string> _filesToClean;

        string assemblyCodeBase = Assembly.GetExecutingAssembly().CodeBase;

        public const string PROGRAM = "SAP2000";
        public const string PATH_SOURCE = @"c:\foo";
        public const string PATH_RELEASE = @"c:\bar";

        [SetUp]
        public void SetUp()
        {
            _directoriesToClean = new List<string>();
            _filesToClean = new List<string>();
        }

        [TearDown]
        public void TearDown()
        {
            if (string.IsNullOrEmpty(RunPath)) return;

            // Remove directories created for the test.
            foreach (string path in _directoriesToClean)
            {
                deleteDirectory(path);
            }

            // Remove files created for the test.
            foreach (string path in _filesToClean)
            {
                deleteFile(path);
            }

            string sourcePath = Path.Combine(RunPath, ExampleReleaser.DIR_SOURCE);
            deleteDirectory(sourcePath);

            string releasePath = Path.Combine(RunPath, PROGRAM, ExampleReleaser.DIR_RELEASE);
            deleteDirectory(releasePath);

            string releasePathCustomBridge = Path.Combine(RunPath, "CSiBridge", ExampleReleaser.DIR_RELEASE);
            deleteDirectory(releasePathCustomBridge);

            string releasePathCustomETABS = Path.Combine(RunPath, "ETABS", ExampleReleaser.DIR_RELEASE);
            deleteDirectory(releasePathCustomBridge);

            RunPath = string.Empty;
        }


        [Test]
        public void ExampleReleaser_Initialization_Specified()
        {
            RunPath = Environment.CurrentDirectory;
            DestinationPath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;

            Assert.That(ExampleReleaser.MODEL_CONTROL_XML_FILE_PATTERN, Is.EqualTo("*_MC.xml"));
            Assert.That(ExampleReleaser.CONFIG_FILENAME, Is.EqualTo("ExamplesReleaser.Config.xml"));
            Assert.That(ExampleReleaser.DIR_RELEASE, Is.EqualTo("Verification"));
            Assert.That(ExampleReleaser.DIR_SOURCE, Is.EqualTo("Verification"));
            Assert.That(ExampleReleaser.StandardSuites[0], Is.EqualTo("Analysis"));
            Assert.That(ExampleReleaser.StandardSuites[1], Is.EqualTo("Design"));

            ExampleReleaser exampleReleaser = new ExampleReleaser(PROGRAM, RunPath, DestinationPath);

            //Assert.That(exampleReleaser.PathRoot, Is.EqualTo(PATH_SOURCE)); // Source is at location of program. Not useful for testing.
            Assert.That(exampleReleaser.Application, Is.EqualTo(PROGRAM));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(RunPath));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(DestinationPath));

        }

        [Test]
        public void ExampleReleaser_Initialization_Specified_Invalid_Source_Destination_Directories()
        {
            RunPath = @"c:\foo";
            DestinationPath = @"c:\bar";

            ExampleReleaser exampleReleaser = new ExampleReleaser(PROGRAM, RunPath, DestinationPath);

            //Assert.That(exampleReleaser.PathRoot, Is.EqualTo(PATH_SOURCE)); // Source is at location of program. Not useful for testing.
            Assert.That(exampleReleaser.Application, Is.EqualTo(PROGRAM));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, @"..\" + ExampleReleaser.DIR_SOURCE)));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, PROGRAM, ExampleReleaser.DIR_RELEASE)));

        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_Defaults()
        {
            ExampleReleaser exampleReleaser = new ExampleReleaser();

            Assert.That(exampleReleaser.Application, Is.EqualTo(PROGRAM));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, @"..\" + ExampleReleaser.DIR_SOURCE)));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, PROGRAM, ExampleReleaser.DIR_RELEASE)));
        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_By_FileName_Only()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_CustomName.Config.xml";
            string program = "ETABS";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            string fileDestination = Path.Combine(exampleReleaserPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);

            // Test
            ExampleReleaser exampleReleaser = new ExampleReleaser(configFile);

            // Check results
            Assert.That(exampleReleaser.Application, Is.EqualTo(program));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, @"..\" + ExampleReleaser.DIR_SOURCE)));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, program, ExampleReleaser.DIR_RELEASE)));
        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_By_Full_File_Path()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_CustomName.Config.xml";
            string program = "ETABS";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");
            string fileDestination = Path.Combine(OtherLocationPath, configFile);

            if (!Directory.Exists(OtherLocationPath))
            {
                Directory.CreateDirectory(OtherLocationPath);
            }
            _directoriesToClean.Add(OtherLocationPath);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);


            // Test
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            // Check results
            Assert.That(exampleReleaser.Application, Is.EqualTo(program));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, @"..\" + ExampleReleaser.DIR_SOURCE)));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, program, ExampleReleaser.DIR_RELEASE)));
        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_Empty()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_Empty.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            string fileDestination = Path.Combine(exampleReleaserPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);

            // Test
            ExampleReleaser exampleReleaser = new ExampleReleaser(configFile);

            // Check results
            Assert.That(exampleReleaser.Application, Is.EqualTo(null));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(null));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(null));

            // Should do nothing with a malformed object
            Assert.DoesNotThrow(() => exampleReleaser.Release());
        }

        [Test]
        public void ExampleReleaser_Initialization_From_Config_File_Empty_Properties()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_EmptyProperties.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            string fileDestination = Path.Combine(exampleReleaserPath.PathRoot, configFile);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing", configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);

            // Test
            ExampleReleaser exampleReleaser = new ExampleReleaser(configFile);

            // Check results
            Assert.That(exampleReleaser.Application, Is.EqualTo(string.Empty));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, @"..\", ExampleReleaser.DIR_SOURCE)));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, ExampleReleaser.DIR_RELEASE)));

            // Should do nothing with a malformed object
            Assert.DoesNotThrow(() => exampleReleaser.Release());
        }

        [Test]
        public void ExampleReleaser_Initialization_Custom_PathRoot()
        {
            string customRootPath = @"C:\\Foo\Bar";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();
            string pathConfig = Path.Combine(exampleReleaserPath.PathRoot, ExampleReleaser.CONFIG_FILENAME);
            ExampleReleaser exampleReleaser = new ExampleReleaser(pathConfig, customRootPath);

            Assert.That(exampleReleaser.PathRoot, Is.EqualTo(customRootPath));
        }

        [Test]
        public void ExampleReleaser_Does_Nothing_When_PathRoot_Does_Not_Exist()
        {
            ExampleReleaser exampleReleaser = new ExampleReleaser(pathRoot: @"c:\Foo\Bar");

            Assert.DoesNotThrow(() => exampleReleaser.Release());
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(0));
        }

        [Test]
        public void ExampleReleaser_Does_Nothing_When_Required_Directories_Do_Not_Exist()
        {
            ExampleReleaser exampleReleaserBase = new ExampleReleaser(PROGRAM);
            string pathSource = Path.Combine(exampleReleaserBase.PathRoot, ExampleReleaser.DIR_SOURCE);
            string pathRelease = Path.Combine(exampleReleaserBase.PathRoot, ExampleReleaser.DIR_RELEASE);

            ExampleReleaser exampleReleaser = new ExampleReleaser(PROGRAM, pathSource, pathRelease);
            RunPath = exampleReleaser.PathRoot;

            
            if (Directory.Exists(pathSource))
                Directory.Delete(pathSource, recursive: true);

            Assert.DoesNotThrow(() => exampleReleaser.Release());
            
            if (Directory.Exists(pathRelease))
                Directory.Delete(pathRelease, recursive: true);

            Assert.DoesNotThrow(() => exampleReleaser.Release());
            Assert.IsFalse(Directory.Exists(pathRelease));
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(0));
        }

        [Test]
        public void ExampleReleaser_Does_Nothing_When_Required_Properties_Are_NullOrEmpty()
        {
            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(null, @"c:\", @"c:\Program Files");

            // Method under test
            Assert.DoesNotThrow(() => exampleReleaser.Release());
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(0));
        }

        [Test]
        public void ExampleReleaser_Correctly_Reads_Custom_Config_File()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_modelsDB.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");
            string fileDestination = Path.Combine(OtherLocationPath, configFile);

            if (!Directory.Exists(OtherLocationPath))
            {
                Directory.CreateDirectory(OtherLocationPath);
            }
            _directoriesToClean.Add(OtherLocationPath);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);

            // Copy models
            string modelsDatabasePath = Path.Combine(sourceBasePath, "_modelsDB");
            string modelsDatabaseDestinationPath = Path.Combine(OtherLocationPath, "_modelsDB");
            if (!Directory.Exists(modelsDatabaseDestinationPath))
            {
                copyDirectory(modelsDatabasePath, modelsDatabaseDestinationPath);
            }
            _directoriesToClean.Add(modelsDatabaseDestinationPath);

            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            // Method under test
            Assert.That(exampleReleaser.Application, Is.EqualTo(PROGRAM));
            Assert.That(exampleReleaser.PathExamplesSource, Is.EqualTo(modelsDatabaseDestinationPath));
            Assert.That(exampleReleaser.PathExamplesRelease, Is.EqualTo(Path.Combine(exampleReleaser.PathRoot, PROGRAM, ExampleReleaser.DIR_RELEASE)));
        }

        [Test]
        public void ExampleReleaser_Releases_Examples_Of_Program_Set_For_Release_And_None_Others()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_modelsDB.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");
            string fileDestination = Path.Combine(OtherLocationPath, configFile);

            if (!Directory.Exists(OtherLocationPath))
            {
                Directory.CreateDirectory(OtherLocationPath);
            }
            _directoriesToClean.Add(OtherLocationPath);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);

            // Copy models
            string modelsDatabasePath = Path.Combine(sourceBasePath, "_modelsDB");
            string modelsDatabaseDestinationPath = Path.Combine(OtherLocationPath, "_modelsDB");
            if (!Directory.Exists(modelsDatabaseDestinationPath))
            {
                copyDirectory(modelsDatabasePath, modelsDatabaseDestinationPath);
            }
            _directoriesToClean.Add(modelsDatabaseDestinationPath);

            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            RunPath = exampleReleaser.PathRoot;
            string pathRelease = Path.Combine(RunPath, PROGRAM, ExampleReleaser.DIR_RELEASE);
            _directoriesToClean.Add(pathRelease);
            Assert.That(!Directory.Exists(pathRelease));

            // Method under test
            exampleReleaser.Release();

            // Check results
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(4));

            Assert.That(Directory.Exists(pathRelease));

            string[] directoriesPathRelease = Directory.GetDirectories(pathRelease);
            Assert.That(directoriesPathRelease.Length, Is.EqualTo(2));

            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Steel Frame", "AISC 360-05 SFD Ex001.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-001.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-004.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 7-002.sdb")));
        }

        [Test]
        public void ReleaseExample_Duplicate_Example_Names_Only_Includes_First()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_modelsDB_SAP2000_Duplicates.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");
            string fileDestination = Path.Combine(OtherLocationPath, configFile);

            if (!Directory.Exists(OtherLocationPath))
            {
                Directory.CreateDirectory(OtherLocationPath);
            }
            _directoriesToClean.Add(OtherLocationPath);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);

            // Copy models
            string modelsDatabasePath = Path.Combine(sourceBasePath, "_modelsDB");
            string modelsDatabaseDestinationPath = Path.Combine(OtherLocationPath, "_modelsDB");
            if (!Directory.Exists(modelsDatabaseDestinationPath))
            {
                copyDirectory(modelsDatabasePath, modelsDatabaseDestinationPath);
            }
            _directoriesToClean.Add(modelsDatabaseDestinationPath);

            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            RunPath = exampleReleaser.PathRoot;
            string pathRelease = Path.Combine(RunPath, PROGRAM, ExampleReleaser.DIR_RELEASE);
            _directoriesToClean.Add(pathRelease);
            Assert.That(!Directory.Exists(pathRelease));

            // Method under test
            exampleReleaser.Release();

            // Check results
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(1));

            Assert.That(Directory.Exists(pathRelease));

            string[] directoriesPathRelease = Directory.GetDirectories(pathRelease);
            Assert.That(directoriesPathRelease.Length, Is.EqualTo(1));
            
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-001.sdb")));
        }

        [Test]
        public void ReleaseExample_Copy_File_Overwrite_of_ReadOnly_Does_Not_Throw_Exception()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_modelsDB_ETABS_RedundantReadOnly.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");
            string fileDestination = Path.Combine(OtherLocationPath, configFile);

            if (!Directory.Exists(OtherLocationPath))
            {
                Directory.CreateDirectory(OtherLocationPath);
            }
            _directoriesToClean.Add(OtherLocationPath);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);

            // Copy models
            string modelsDatabasePath = Path.Combine(sourceBasePath, "_modelsDB");
            string modelsDatabaseDestinationPath = Path.Combine(OtherLocationPath, "_modelsDB");
            if (!Directory.Exists(modelsDatabaseDestinationPath))
            {
                copyDirectory(modelsDatabasePath, modelsDatabaseDestinationPath);
            }
            _directoriesToClean.Add(modelsDatabaseDestinationPath);

            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            RunPath = exampleReleaser.PathRoot;
            string pathRelease = Path.Combine(RunPath, "ETABS", ExampleReleaser.DIR_RELEASE);
            _directoriesToClean.Add(pathRelease);
            Assert.That(!Directory.Exists(pathRelease));

            // Method under test
            Assert.DoesNotThrow(() => exampleReleaser.Release());
            
            // Check results
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(8));

            Assert.That(Directory.Exists(pathRelease));

            string[] directoriesPathRelease = Directory.GetDirectories(pathRelease);
            Assert.That(directoriesPathRelease.Length, Is.EqualTo(2));

            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-003.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 6-011a.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 6-011b.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "EQ6-011.txt")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Steel Frame", "BS 5950-2000 SFD Ex002.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Shear Wall", "ACI 318-14 SWD Ex002.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Shear Wall", "CSA A23.3-04 SWD Ex001.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Concrete Frame", "ACI 318-14 CFD Ex001.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Composite Beam", "BS 5950-1990 CBD Ex001.edb")));
        }

        [Test]
        public void ReleaseExample_Releases_CSiBrige_Examples_From_SAP2000_Directory()
        {
            // Set up custom config file
            string configFile = "ExamplesReleaser_modelsDB_CSiBridge.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");
            string fileDestination = Path.Combine(OtherLocationPath, configFile);

            if (!Directory.Exists(OtherLocationPath))
            {
                Directory.CreateDirectory(OtherLocationPath);
                _directoriesToClean.Add(OtherLocationPath);
            }
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
                _filesToClean.Add(fileDestination);
            }

            // Copy models
            string modelsDatabasePath = Path.Combine(sourceBasePath, "_modelsDB");
            string modelsDatabaseDestinationPath = Path.Combine(OtherLocationPath, "_modelsDB");
            if (!Directory.Exists(modelsDatabaseDestinationPath))
            {
                copyDirectory(modelsDatabasePath, modelsDatabaseDestinationPath);
                _directoriesToClean.Add(modelsDatabaseDestinationPath);
            }

            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            RunPath = exampleReleaser.PathRoot;
            string pathRelease = Path.Combine(RunPath, "CSiBridge", ExampleReleaser.DIR_RELEASE);
            _directoriesToClean.Add(pathRelease);
            Assert.That(!Directory.Exists(pathRelease));

            // Method under test
            exampleReleaser.Release();

            // Check results
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(3));

            Assert.That(Directory.Exists(pathRelease));

            string[] directoriesPathRelease = Directory.GetDirectories(pathRelease);
            Assert.That(directoriesPathRelease.Length, Is.EqualTo(1));

            Assert.That(!File.Exists(Path.Combine(pathRelease, "Design", "Steel Frame", "AISC 360-05 SFD Ex001.bdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-001.bdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-004.bdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 7-002.bdb")));
        }

        [Test]
        public void ReleaseExample_Clears_Old_Releases_Before_Releasing()
        {

            // Set up custom config file
            string configFile = "ExamplesReleaser_modelsDB_SAP2000.Config.xml";
            string configFileAlternate = "ExamplesReleaser_modelsDB_SAP2000_Alternate.Config.xml";
            ExampleReleaser exampleReleaserPath = new ExampleReleaser();

            string sourceBasePath = Directory.GetParent(Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "ExamplesReleaser", "resources-testing");
            OtherLocationPath = Path.Combine(exampleReleaserPath.PathRoot, "otherLocation");
            string fileDestination = Path.Combine(OtherLocationPath, configFile);
            string fileDestinationAlternate = Path.Combine(OtherLocationPath, configFileAlternate);

            if (!Directory.Exists(OtherLocationPath))
            {
                Directory.CreateDirectory(OtherLocationPath);
            }
            _directoriesToClean.Add(OtherLocationPath);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePath, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);
            if (!File.Exists(fileDestinationAlternate))
            {
                File.Copy(Path.Combine(sourceBasePath, configFileAlternate), fileDestinationAlternate);
            }
            _filesToClean.Add(fileDestinationAlternate);

            // Copy models
            string modelsDatabasePath = Path.Combine(sourceBasePath, "_modelsDB");
            string modelsDatabaseDestinationPath = Path.Combine(OtherLocationPath, "_modelsDB");
            if (!Directory.Exists(modelsDatabaseDestinationPath))
            {
                copyDirectory(modelsDatabasePath, modelsDatabaseDestinationPath);
            }
            _directoriesToClean.Add(modelsDatabaseDestinationPath);

            // Set up object
            ExampleReleaser exampleReleaser = new ExampleReleaser(fileDestination);

            RunPath = exampleReleaser.PathRoot;
            string pathRelease = Path.Combine(RunPath, PROGRAM, ExampleReleaser.DIR_RELEASE);
            _directoriesToClean.Add(pathRelease);
            Assert.That(!Directory.Exists(pathRelease));

            // First copy
            exampleReleaser.Release();

            // Set up new object
            exampleReleaser = new ExampleReleaser(fileDestinationAlternate);

            // Second copy - Method under test
            exampleReleaser.Release();

            // Check results
            Assert.That(exampleReleaser.ExamplesToRelease.Count, Is.EqualTo(2));

            Assert.That(Directory.Exists(pathRelease));

            string[] directoriesPathRelease = Directory.GetDirectories(pathRelease);
            Assert.That(directoriesPathRelease.Length, Is.EqualTo(2));

            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Steel Frame", "AISC 360-05 SFD Ex001.sdb")));
            Assert.That(!File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-001.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-004.sdb")));
            Assert.That(!File.Exists(Path.Combine(pathRelease, "Analysis", "Example 7-002.sdb")));
            Assert.That(!File.Exists(Path.Combine(pathRelease, "Analysis", "Example 7-003.sdb")));
        }

        private static void copyDirectory(string sourceFolder, string outputFolder)
        {
            // Create subdirectory structure in destination    
            foreach (string directory in Directory.GetDirectories(sourceFolder, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(Path.Combine(outputFolder, directory.Substring(sourceFolder.Length + 1)));
            }

            // Copy files over
            foreach (string fileName in Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories))
            {
                string fileDestination = fileName.Substring(sourceFolder.Length + 1);
                if (File.Exists(fileDestination)) continue;
                fileDestination = Path.Combine(outputFolder, fileDestination);
                while (!File.Exists(fileDestination))
                {
                    try
                    {
                        File.Copy(fileName, fileDestination);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        Thread.Sleep(0);
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the directory.
        /// </summary>
        /// <see cref="http://zacharykniebel.com/blog/web-development/2013/june/21/solving-the-csharp-bug-when-recursively-deleting-directories"/>
        /// <param name="path">The path.</param>
        private static void deleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    deleteFile(file);
                }
            }

            if (Directory.Exists(path))
            {
                string[] directories = Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                    deleteDirectory(directory);
                }
            }

            while (Directory.Exists(path))
            {
                try
                {
                    Directory.Delete(path, true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(0);
                }
            }
        }

        private static void deleteFile(string path)
        {
            while (File.Exists(path))
            {
                try
                {
                    File.SetAttributes(path, FileAttributes.Normal);
                    File.Delete(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(0);
                }
            }
        }
    }
}
