using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Csi.Testing.ExamplesReleaser;
using Csi.Testing.ExamplesReleaser.Core;
using NUnit.Framework;
using NCrunch.Framework;
using System.Reflection;
using System.Threading;

namespace Csi.Testing.ExamplesReleaser.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        public string RunPath = "";
        public string ConsoleName = "ExamplesReleaser.exe";
        public const string PROGRAM = "SAP2000";

        protected List<string> _directoriesToClean;
        protected List<string> _filesToClean;


        protected TextWriter _consoleNormalOutput;
        protected StringWriter _consoleTestingOutput;
        protected StringBuilder _testingStringBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            // Set current folder to testing folder
            string assemblyCodeBase = Assembly.GetExecutingAssembly().CodeBase;

            // Get directory name
            string dirName = Path.GetDirectoryName(assemblyCodeBase);
            if (dirName == null) return;

            // Remove URL-prefix if it exists.
            if (dirName.StartsWith("file:\\"))
                dirName = dirName.Substring(6);

            // Set current folder.
            Environment.CurrentDirectory = dirName;

            // Initialize string builder to replace console.
            _testingStringBuilder = new StringBuilder();
            _consoleTestingOutput = new StringWriter(_testingStringBuilder);

            // Swap normal output console with testing console - to reuse it later.
            _consoleNormalOutput = Console.Out;
            Console.SetOut(_consoleTestingOutput);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            // Set normal output stream to the console.
            Console.SetOut(_consoleNormalOutput);
        }

        [SetUp]
        public void SetUp()
        {
            // Clear string builder.
            _testingStringBuilder.Remove(0, _testingStringBuilder.Length);
            
            // Create lists of files and directories to clean up
            _directoriesToClean = new List<string>();
            _filesToClean = new List<string>();
        }

        [TearDown]
        public void TearDown()
        {
            // Verbose output in console.
            _consoleNormalOutput.Write(_testingStringBuilder.ToString());

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

            string releasePathCustom = Path.Combine(RunPath, "ETABS", ExampleReleaser.DIR_RELEASE);
            deleteDirectory(releasePathCustom);

            RunPath = string.Empty;
        }

        [Test]
        public void Main_NoArguments_Releases_Examples_From_Default_Config_File()
        {
            RunPath = Environment.CurrentDirectory;
            string[] paths = NCrunchEnvironment.GetAllAssemblyLocations();
            foreach (string path in paths)
            {
                if (!path.Contains(ConsoleName)) continue;
                RunPath = Path.GetDirectoryName(path);
                break;
            }
            string sourceBasePath = Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources-testing");
            string pathSource = Path.Combine(RunPath, @"..\", ExampleReleaser.DIR_SOURCE);

            copyDirectory(sourceBasePath, pathSource);
            _directoriesToClean.Add(pathSource);

            Assert.That(Directory.Exists(pathSource));

            string pathRelease = Path.Combine(RunPath, PROGRAM, ExampleReleaser.DIR_RELEASE);
            Assert.That(!Directory.Exists(pathRelease));

            // Method under test
            Program.Main(null);

            // Check results
            Assert.That(Directory.Exists(pathRelease));

            string[] directoriesPathRelease = Directory.GetDirectories(pathRelease);
            Assert.That(directoriesPathRelease.Length, Is.EqualTo(2));

            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Steel Frame", "AISC 360-05 SFD Ex001.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-001.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-004.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 7-002.sdb")));
        }

        [Test]
        public void Main_EXE_NoArguments_Releases_Examples_From_Default_Config_File()
        {
            RunPath = Environment.CurrentDirectory;
            string[] paths = NCrunchEnvironment.GetAllAssemblyLocations();
            foreach (string path in paths)
            {
                if (!path.Contains(ConsoleName)) continue;
                RunPath = Path.GetDirectoryName(path);
                break;
            }
            string sourceBasePath = Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName;
            sourceBasePath = Path.Combine(sourceBasePath, "resources-testing");
            string pathSource = Path.Combine(RunPath, @"..\", ExampleReleaser.DIR_SOURCE);

            copyDirectory(sourceBasePath, pathSource);
            _directoriesToClean.Add(pathSource);

            Assert.That(Directory.Exists(pathSource));

            string pathRelease = Path.Combine(RunPath, PROGRAM, ExampleReleaser.DIR_RELEASE);
            Assert.That(!Directory.Exists(pathRelease));

            // Method under test
            startConsoleApplication(Path.Combine(RunPath, ConsoleName), workingDirectory: RunPath);
            
            // Check results
            Assert.That(Directory.Exists(pathRelease));

            string[] directoriesPathRelease = Directory.GetDirectories(pathRelease);
            Assert.That(directoriesPathRelease.Length, Is.EqualTo(2));

            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Steel Frame", "AISC 360-05 SFD Ex001.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-001.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-004.sdb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 7-002.sdb")));
        }

        [Test]
        public void Main_With_File_Argument_Releases_Examples_From_Specified_Config_File()
        {
            RunPath = Environment.CurrentDirectory;
            string[] paths = NCrunchEnvironment.GetAllAssemblyLocations();
            foreach (string path in paths)
            {
                if (!path.Contains(ConsoleName)) continue;
                RunPath = Path.GetDirectoryName(path);
                break;
            }
            
            string sourceBasePath = Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName;

            // Set up custom config file
            string configFile = "ExamplesReleaser_CustomName.Config.xml";
            string sourceBasePathFile = Path.Combine(sourceBasePath, "resources-testing");
            string fileDestination = Path.Combine(RunPath, configFile);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePathFile, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);

            // Set up test directories
            sourceBasePath = Path.Combine(sourceBasePath, "resources-testing");
            string pathSource = Path.Combine(RunPath, @"..\", ExampleReleaser.DIR_SOURCE);

            copyDirectory(sourceBasePath, pathSource);
            _directoriesToClean.Add(pathSource);

            Assert.That(Directory.Exists(pathSource));

            string pathRelease = Path.Combine(RunPath, "ETABS", ExampleReleaser.DIR_RELEASE);
            Assert.That(!Directory.Exists(pathRelease));

            // Method under test
            string[] parameters = { configFile };
            Program.Main(parameters);

            // Check results
            Assert.That(Directory.Exists(pathRelease));

            string[] directoriesPathRelease = Directory.GetDirectories(pathRelease);
            Assert.That(directoriesPathRelease.Length, Is.EqualTo(2));

            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-003.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 6-010.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "EQ6-010-trans.txt")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Steel Frame", "BS 5950-2000 SFD Ex002.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Shear Wall", "ACI 318-14 SWD Ex002.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Shear Wall", "CSA A23.3-04 SWD Ex001.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Concrete Frame", "ACI 318-14 CFD Ex001.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Composite Beam", "BS 5950-1990 CBD Ex001.edb")));
        }

        [Test]
        public void Main_With_File_Path_Argument_Releases_Examples_From_Specified_Config_File_at_Specified_Location()
        {
            RunPath = Environment.CurrentDirectory;
            string[] paths = NCrunchEnvironment.GetAllAssemblyLocations();
            foreach (string path in paths)
            {
                if (!path.Contains(ConsoleName)) continue;
                RunPath = Path.GetDirectoryName(path);
                break;
            }
            
            string sourceBasePath = Directory.GetParent(NCrunchEnvironment.GetOriginalSolutionPath()).FullName;

            // Set up custom config file
            string configFile = "ExamplesReleaser_CustomName.Config.xml";
            string sourceBasePathFile = Path.Combine(sourceBasePath, "resources-testing");
            string otherLocationPath = Path.Combine(RunPath, "otherLocation");
            string fileDestination = Path.Combine(otherLocationPath, configFile);

            if (!Directory.Exists(otherLocationPath))
            {
                Directory.CreateDirectory(otherLocationPath);
            }
            _directoriesToClean.Add(otherLocationPath);
            if (!File.Exists(fileDestination))
            {
                File.Copy(Path.Combine(sourceBasePathFile, configFile), fileDestination);
            }
            _filesToClean.Add(fileDestination);


            // Set up test directories
            sourceBasePath = Path.Combine(sourceBasePath, "resources-testing");
            string pathSource = Path.Combine(RunPath, @"..\", ExampleReleaser.DIR_SOURCE);

            copyDirectory(sourceBasePath, pathSource);
            _directoriesToClean.Add(pathSource);

            Assert.That(Directory.Exists(pathSource));

            string pathRelease = Path.Combine(RunPath, "ETABS", ExampleReleaser.DIR_RELEASE);
            Assert.That(!Directory.Exists(pathRelease));

            // Method under test
            string[] parameters = { fileDestination };
            Program.Main(parameters);

            // Check results
            Assert.That(Directory.Exists(pathRelease));

            string[] directoriesPathRelease = Directory.GetDirectories(pathRelease);
            Assert.That(directoriesPathRelease.Length, Is.EqualTo(2));

            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 1-003.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "Example 6-010.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Analysis", "EQ6-010-trans.txt")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Steel Frame", "BS 5950-2000 SFD Ex002.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Shear Wall", "ACI 318-14 SWD Ex002.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Shear Wall", "CSA A23.3-04 SWD Ex001.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Concrete Frame", "ACI 318-14 CFD Ex001.edb")));
            Assert.That(File.Exists(Path.Combine(pathRelease, "Design", "Composite Beam", "BS 5950-1990 CBD Ex001.edb")));
        }

        /// <summary>
        /// Starts the console application.
        /// </summary>
        /// <param name="applicationName">Name of the console application to run.</param>
        /// <param name="arguments">The arguments. Provide an empty string for no arguments.</param>
        /// <returns>System.Int32.</returns>
        protected int startConsoleApplication(string applicationName, string arguments = "", string workingDirectory = "")
        {
            // Initialize process here
            Process process = new Process
            {
                StartInfo =
                {
                    FileName = applicationName,

                    // add arguments as whole string
                    Arguments = arguments,          

                    // Use it to start from testing environment
                    UseShellExecute = false,        

                    // redirect outputs to have it in testing console
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,   

                    // set working directory
                    WorkingDirectory = (string.IsNullOrEmpty(workingDirectory))? Environment.CurrentDirectory : workingDirectory
                }
            };

            // start and wait for exit
            process.Start();
            process.WaitForExit();

            // get output to testing console.
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            Console.Write(process.StandardError.ReadToEnd());

            // return exit code
            return process.ExitCode;
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
