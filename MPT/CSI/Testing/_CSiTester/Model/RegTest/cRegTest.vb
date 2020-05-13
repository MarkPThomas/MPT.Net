﻿Option Explicit On
Option Strict On

Imports System.Collections.ObjectModel

Imports MPT.Enums.EnumLibrary
Imports MPT.FileSystem.FoldersLibrary
Imports MPT.FileSystem.PathLibrary
Imports MPT.Reporting

Imports CSiTester.cLibrary

Imports CSiTester.cXmlReadWriteRegTest
Imports CSiTester.cMCValidator
Imports CSiTester.cSettings
Imports CSiTester.cPathSettings

''' <summary>
''' This class mostly is a version of the regTest.xml stored in memory.
''' It also has newly generated properties and a few methods directly related to the input/output of the regTest.xml
''' </summary>
''' <remarks></remarks>
Public Class cRegTest
    Implements IMessengerEvent
    Implements ILoggerEvent
    Public Event Log(exception As LoggerEventArgs) Implements ILoggerEvent.Log
    Public Event Messenger(messenger As MessengerEventArgs) Implements IMessengerEvent.Messenger

#Region "Variables"
    ''' <summary>
    ''' The tester installation type to start up with.
    ''' </summary>
    ''' <remarks></remarks>
    Private _csiTesterInstallMethod As eCSiInstallMethod

    ''' <summary>
    ''' Data object representation of the XML file.
    ''' </summary>
    ''' <remarks></remarks>
    Private _regTestXML As New xmlRegTest

#End Region

#Region "Constants: Private"
    Private Const _PROMPT_REGTEST_LOG_NOT_EXIST As String = "The regtest_log.xml file cannot be found. Would you like to try again?"
    Private Const _PROMPT_REGTEST_LOG_NOT_EXIST_END As String = "Run cannot be continued." & vbNewLine & vbNewLine &
                                                                "Exiting process ..."""
    Private Const _TITLE_REGTEST_LOG_NOT_EXIST As String = "Fatal Error"
#End Region

#Region "Constants: Friend"
    Friend Const PROCESS_REGTEST As String = "regtest"

    '=== Installation Directories & Files
    Friend Const DIR_NAME_REGTEST As String = "regtest"
    Friend Const DIR_NAME_REGTEST_TEMPLATE As String = "regtest\templates"

    Friend Const FILENAME_REGTEST_CONTROL As String = "regtest.xml"
    Friend Const FILENAME_CONTROL As String = "control.xml"
    Friend Const FILENAME_REGTEST_LOG As String = "regtest_log.xml"
#End Region

#Region "Properties: regTest XML & Supporting Files"
    Private _reInitialize As Boolean
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property reInitialize As Boolean
        Get
            Return _reInitialize
        End Get
    End Property

    ''' <summary>
    ''' Path of the regTest.EXE file directory
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property regTestFile As New cPath

    ''' <summary>
    ''' Name of the regTest.xml file. Set to regTest.xml unless specified otherwise
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property regTestXmlName As String

    ''' <summary>
    ''' Path to the location of the program that CSiTester and regTest were installed with.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property programFileInstall As New cPath

    ''' <summary>
    ''' Path to the regTest.xml file to be read from and written to.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlFile As New cPath

    ''' <summary>
    ''' Path to the regTest.xml file that is installed with the corresponding analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlInstallationFile As New cPath

    ''' <summary>
    ''' Path to the regtest control.xml file that is installed with the corresponding analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property xmlControlInstallationFile As New cPath

    ' ''' <summary>
    ' ''' Path to the output directory. This is automatically set to the models destination path or subdirectory path if those options are chosen.
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Property output_directory As String

    ''' <summary>
    ''' Path to the log file generated by regTest during a run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property regtest_log As New cPath

    Public Property autoFolders As Boolean
#End Region

#Region "Properties: XML File"
    ' General
    ''' <summary>
    ''' Path to the source directory from which models are to be copied from. If CSiTester is using a *.ini file, this is automatically set to the verification models of the corresponding analysis program.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property models_database_directory As New cPathRegTest

    ''' <summary>
    ''' Path to the destination directory to which models are copied, and from which models are run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property models_run_directory As New cPathRegTest

    ''' <summary>
    ''' If true, creates a subdirectory named as the test ID within the run directory to be used as the regTest run directory for the corresponding test ID run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property models_run_directory_attribCreateSubDir As Boolean

    ''' <summary>
    ''' If true, regTest will abort the run if the subdirectory named as the test ID already exists in order to avoid replacing the files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property models_run_directory_attribNoReplace As Boolean

    ''' <summary>
    ''' If true, the 'output_directory' path is taken to be the same as the 'models_run_directory' path.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property output_directory_attribSameAsModelsRun As Boolean

    ''' <summary>
    ''' Path to the output directory where regTest output files are to be located.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property output_directory As New cPathRegTest

    ''' <summary>
    ''' If true, creates a subdirectory named as the test ID within the output directory to be used as the regTest output directory for the corresponding test ID run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property output_directory_attribCreateSubDir As Boolean

    ''' <summary>
    ''' If true, regTest will abort the run if the subdirectory named as the test ID already exists in order to avoid replacing the files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property output_directory_attribNoReplace As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property computer_id As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property computer_description As String

    ''' <summary>
    ''' If true, log files will be written for a regTest run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property write_log_files As Boolean

    ' Actions
    ''' <summary>
    ''' Attribute to specify whether or not the "copy models mirror" action should be run or not. 
    ''' This action copies the contents of the model database directory to the models source directory while keeping the contents hierarchy the same.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property copy_models_mirror_attribRun As Boolean

    ''' <summary>
    '''  If yes, runs a local test for which all the models are run locally on a single machine that initiated the test.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property run_local_test As Boolean

    ''' <summary>
    ''' Attribute to specify whether the local test should be run using batch file mode for which the program is opened once and all the models are run sequentially (this helps to identify memory leaks, etc.) or whether it should be run by opening the program, running the model and closing the program for each new model.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property run_using_batch_file As Boolean

    ' Testing
    ''' <summary>
    ''' ID to be used in naming and tracking the regTest run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property test_id As String

    ''' <summary>
    ''' Description of the particular regTest run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property test_description As String

    '=== test_to_run ===
    ''' <summary>
    ''' Test style to run. Options are:
    ''' run as is: Just runs the model files 'as is'.
    ''' run as is with different sets of analysis parameters: Runs the model files 'as is', but changes analysis settings parameters. A run is generated for each parameter in order to test all 9 combinations.
    ''' update bridge: Updates bridge objects.
    ''' update bridge and run: Updates bridge objects and runs the analysis.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property test_to_run As String

    '=== program ===
    ''' <summary>
    ''' Name of the analysis program to be used by regTest to run model files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property program_name As eCSiProgram

    ''' <summary>
    ''' Version of the analysis program to be used by regTest to run model files.l
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property program_version As String

    ''' <summary>
    ''' Build number of the analysis program to be used by regTest to run model files.l
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property program_build As String

    ''' <summary>
    ''' Path to the analysis program *.exe file to be used by regTest to run model files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property program_file As New cPathRegTest

    '===
    ''' <summary>
    ''' Default command line used by regTest in performing a run. This is overwritten by any command line specified in a model control XMl file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property command_line As String

    '=== selections ===
    ''' <summary>
    ''' Use the list of model IDs recorded in 'regTest.xml' in order to select which models will be run based on data recorded in the model control xml file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property model_ids_attribUse As Boolean

    ''' <summary>
    ''' Array of model ids that are used by regTest to select models to run.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property model_id As String()
    '===

    ''' <summary>
    ''' Path to the previous test results 'latest.xml' file used by regTest for comparing run results to a prior run. 
    ''' This file is identical to the file generated as a summary of a regTest run in the specified output directory.
    ''' To be updated, a desired regTest run summary file is to be copied to this location and renamed as 'latest.xml'
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property previous_test_results_file As New cPathRegTest

    ''' <summary>
    ''' Attribute to specify whether or not the email notifications should be sent out. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property email_notifications_attribUse As Boolean

    ''' <summary>
    ''' Individual email addresses to which the email notifications should be sent out.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property email_address As String()

    ''' <summary>
    ''' List of email addresses to which the notifications should be sent out.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property email_address_List As String()


    ' Reporting
    ''' <summary>
    ''' Number of decimal digits by which percent difference is displayed in regTest output files.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property percent_difference_decimal_digits As String

    ' Advanced
    ''' <summary>
    ''' Path to the 'control.xml' file used in coordinating runs between CSiTester.exe and regTest.exe
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property control_xml_file As New cPathRegTest

    ''' <summary>
    '''  Sets whether or not the path is relative or absolute to regTest.exe as recorded in the XML file.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property control_xml_file_Path_attribType As String

    ''' <summary>
    ''' Controls whether screenshots should be taken after a model run that failed due to time out. 
    ''' Reviewing such screenshots is an efficient way to determine what might have caused the time out. 
    ''' The screenshots are taken via external program nircmd.exe, located in regtest/utils/nircmd/nircmd.exe and are saved in the output directory.
    ''' For distribution to the end users (such as when shipping RegTest with CSI software), the value should be set to "no" and nircmd.exe should be removed from the distribution directory structure. 
    ''' This way, we do not have to monitor the nircmd.exe license. 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property take_screenshots_after_model_run_timeouts As Boolean
#End Region

#Region "Initialization"
    ''' <summary>
    ''' Generates regTest class populated with XML data based on the default location
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByVal myCsiTesterInstallMethod As eCSiInstallMethod)
        _csiTesterInstallMethod = myCsiTesterInstallMethod
        InitializeControl()
    End Sub

    ''' <summary>
    ''' Generates regTest class populated with XML data based on a user-defined location.
    ''' </summary>
    ''' <param name="newSuitePath">Path to the regTest.Exe file directory.</param>
    ''' <param name="newXMLPath">Optional: New path to the regTest.xml file.</param>
    ''' <remarks></remarks>
    Friend Sub New(ByVal myCsiTesterInstallMethod As eCSiInstallMethod,
                   ByVal newSuitePath As String,
                   ByVal newXMLPath As String,
                   ByVal reInitializeAfterCopy As Boolean)
        _csiTesterInstallMethod = myCsiTesterInstallMethod
        InitializeControl(newSuitePath, newXMLPath, reInitializeAfterCopy)
    End Sub

    ''' <summary>
    ''' Initializes XML reading methods of the RegTest class.
    ''' </summary>
    '''<param name="newSuitePath">Optional: New path to the regTest.exe file.</param>
    ''' <param name="newXMLPath">Optional: New path to the regTest.xml file.</param>
    ''' <remarks></remarks>
    Private Sub InitializeControl(Optional ByVal newSuitePath As String = "",
                                  Optional ByVal newXMLPath As String = "",
                                  Optional ByVal reInitializeAfterCopy As Boolean = False)
        'TODO: Remove later when allowed to be set by program, where it will be stored in the settings class and referenced into here?
        autoFolders = False
        Try
            Dim fileName As String

            'Set default RegTest location
            If String.IsNullOrEmpty(newSuitePath) Then
                regTestFile.SetProperties(pathStartup() & "\" & DIR_NAME_CSITESTER)
            Else
                regTestFile.SetProperties(newSuitePath)
            End If

            'Initializization XML Properties
            fileName = testerSettings.regTestName
            regTestXmlName = fileName

            If String.IsNullOrEmpty(newXMLPath) Then
                xmlFile.SetProperties(regTestFile.path & DIR_NAME_REGTEST & "\" & fileName)
            Else
                xmlFile.SetProperties(newXMLPath)
            End If

            'Set if this is a new initialization of a copied file
            _reInitialize = reInitializeAfterCopy

            'Make adjustments if the testing suite location has been moved. 
            CheckProgramLocationChanged()

            ReadXMLFile(Me)

            'Sets paths to defaults if they were not transformed correctly during the reading process
            ValidateRegTestXMLValues()
            SetVersionBuild()
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try

    End Sub

    ''' <summary>
    ''' Check if path is for a CSi program. 
    ''' If the testing suite is moved relative to the program, some path relative/absolute conversions will not work.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CheckProgramLocationChanged()
        If (testerSettings.programLocationChanged AndAlso
            Not _csiTesterInstallMethod = eCSiInstallMethod.UseIni) Then
            'Adjust program paths
            ExceptionProgramSource(program_file.path, GetEnumDescription(testerSettings.programName))
        End If
    End Sub

    ''' <summary>
    ''' Saves the RegTest class values to the RegTest.XML
    ''' </summary>
    ''' <param name="resetDefaults">Optional: If true, the class won't be automatically updated before save.</param>
    ''' <remarks></remarks>
    Friend Sub SaveRegTest(Optional ByVal resetDefaults As Boolean = False)
        Try
            If Not resetDefaults Then
                'Update RegTest class of any values not automatically updated
                model_id = myCsiTester.exampleRunIDs.ToArray
                SetFolderInitialization()
            End If

            SaveRegTestXML(Me)
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub

    ''' <summary>
    ''' Saves the regTest XML with only updating the relevant copy options for whether or not destination folder initialization is needed.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SaveFolderInitializationSettings()
        SaveFolderInitializationSettingsToXML(Me)
    End Sub

    ''' <summary>
    ''' Returns the path of the regTest validation results.
    ''' </summary>
    ''' <param name="p_pathRegTestValidation">The path of the regTest file used to perform the validation.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function RegTestResultsPath(ByVal p_pathRegTestValidation As String) As String
        Return RegTestResultsPathFromXML(p_pathRegTestValidation)
    End Function
#End Region

#Region "Methods: Friend"
    ''' <summary>
    ''' Determines the version and build of a specified CSi analysis program and assigns the values to the corresponding properties.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetVersionBuild()
        Dim fileVersion As String

        If IO.File.Exists(program_file.path) Then
            fileVersion = FileVersionInfo.GetVersionInfo(program_file.path).FileVersion
            program_build = GetSuffix(fileVersion, ".")
            program_version = Left(fileVersion, Len(fileVersion) - Len(program_build) - 1)
        End If
    End Sub

    ''' <summary>
    ''' Takes the path to the tester destination directory and adjusts it for the destination of the model files to be copied.
    ''' </summary>
    ''' <param name="testerDestinationDir">Path to the tester destination directory.</param>
    ''' <remarks></remarks>
    Friend Sub SetModelsRunDirectory(ByVal testerDestinationDir As String)
        If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
            models_run_directory.SetProperties(testerDestinationDir & "\" & DIR_NAME_MODELS_DESTINATION)
        Else
            'TODO: Reinstate this older behavior.
            If autoFolders Then
                SetTestRunResultsAction()
                models_run_directory.SetProperties(testerDestinationDir & "\" & test_id)
            Else
                models_run_directory.SetProperties(testerDestinationDir & "\" & DIR_NAME_MODELS_DESTINATION)
                output_directory.SetProperties(testerDestinationDir & "\" & DIR_NAME_RESULTS_DESTINATION)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Sets up parameters in regTest based on common testing scenarios.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTestRunResultsAction()
        If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
            'Set a constant custom test ID based on what is specified in regTest.xml
            SetTestID(test_id)
        Else
            'Set Auto ID, which changes between each run based on date/time
            'TODO: Reinstate this older behavior.
            If autoFolders Then
                SetTestID()
            Else
                SetTestID(test_id)
            End If
        End If

        SetTestRunResultsAction(Me)

        'Update Output Directory
        SetOutPutDirectory()
    End Sub

    ''' <summary>
    ''' Sets the path location to the location of RegTest log. Returns "true" if the path is in the output directory.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetRegTestLog() As Boolean
        Dim i As Integer = 0
        Dim continueRun As Boolean = False

        SetRegTestLog = False

        'System.Threading.Thread.Sleep(1000)
        If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
            regtest_log.SetProperties(myRegTest.output_directory.path & "\" & FILENAME_REGTEST_LOG)
            While Not continueRun
                'Waiting until file is created
                If IO.File.Exists(regtest_log.path) Then Return True
            End While
        Else
            While Not continueRun
                If IO.File.Exists(myRegTest.output_directory.path & "\" & FILENAME_REGTEST_LOG) Then
                    regtest_log.SetProperties(myRegTest.output_directory.path & "\" & FILENAME_REGTEST_LOG)
                    Return True
                ElseIf IO.File.Exists(pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_REGTEST_LOG) Then
                    regtest_log.SetProperties(pathStartup() & "\" & DIR_NAME_CSITESTER & "\" & FILENAME_REGTEST_LOG)
                    Return True
                ElseIf IO.File.Exists(myRegTest.models_run_directory.path & "\" & FILENAME_REGTEST_LOG) Then
                    regtest_log.SetProperties(myRegTest.models_run_directory.path & "\" & FILENAME_REGTEST_LOG)
                    Return True
                Else            'Try 10 times at 1 second intervals
                    If i = 10 Then
                        Select Case MessengerPrompt.Prompt(New MessageDetails(eMessageActionSets.YesNo, eMessageType.Hand),
                                            _PROMPT_REGTEST_LOG_NOT_EXIST,
                                            _TITLE_REGTEST_LOG_NOT_EXIST)
                            Case eMessageActions.Yes
                                'Reset counter and keep trying
                                i = 0
                            Case eMessageActions.No
                                'Abort process
                                RaiseEvent Messenger(New MessengerEventArgs(New MessageDetails(eMessageActionSets.OkOnly, eMessageType.Stop),
                                                                            _PROMPT_REGTEST_LOG_NOT_EXIST_END,
                                                                            _TITLE_REGTEST_LOG_NOT_EXIST))
                                myCsiTester.CancelRegTest()
                                myCsiTester.checkRunOngoing = False
                                Return False
                        End Select
                    End If
                    System.Threading.Thread.Sleep(1000)
                End If
                i += 1
            End While
        End If
    End Function

    ''' <summary>
    ''' Copies over model files into the destination directory, or not, depending on if the operation is needed, based on a property updated in cCSiTester.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFolderInitialization()
        Try
            copy_models_mirror_attribRun = myCsiTester.initializeModelDestinationFolder
        Catch ex As Exception
            RaiseEvent Log(New LoggerEventArgs(ex))
        End Try
    End Sub
#End Region

#Region "Methods: Private"
    ''' <summary>
    ''' Sets paths to defaults if they were not transformed correctly during the reading process.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateRegTestXMLValues()
        If Not IO.File.Exists(program_file.path) Then
            program_file.SetProperties(DIR_TESTER_PROGRAM_PATH_DEFAULT)
        End If

        If Not IO.Directory.Exists(models_database_directory.path) Then
            models_database_directory.SetProperties(DIR_TESTER_SOURCE_DIR_DEFAULT)
        End If

        'Checks 1 directory higher as dirNameModelsDest might not exist
        If Not IO.Directory.Exists(GetPathDirectorySubStub(models_run_directory.path, 1)) Then
            models_run_directory.SetProperties(DIR_TESTER_DESTINATION_DIR_DEFAULT & "\" & DIR_NAME_MODELS_DESTINATION)
        End If

        'Checks 1 directory higher as dirNameResultsDest might not exist
        If Not IO.Directory.Exists(GetPathDirectorySubStub(output_directory.path, 1)) Then
            output_directory.SetProperties(DIR_TESTER_DESTINATION_DIR_DEFAULT & "\" & DIR_NAME_RESULTS_DESTINATION)
        End If

        If Not IO.Directory.Exists(previous_test_results_file.path) Then
            previous_test_results_file.SetProperties(pathStartup() & cPathRegTest.pathRelativeToProgram & "previous_test_results" & "\" & GetEnumDescription(program_name) & "\" & "run as is\latest.xml")
        End If
    End Sub

    ''' <summary>
    ''' Creates the test ID if the program is using an auto-generated test ID.
    ''' </summary>
    ''' <param name="customID">Optional: Sets the test ID to the custom ID specified and responds to the possibility of an existing output path.</param>
    ''' <remarks></remarks>
    Private Sub SetTestID(Optional ByVal customID As String = "")
        If String.IsNullOrEmpty(customID) Then
            test_id = myCsiTester.AutoTestId()
        Else
            If IO.Directory.Exists(customID) And Not output_directory_attribNoReplace Then
                'TODO:
                'Prompt User to Specify a Unique Name, or allow the folder to be replaced.
                Exit Sub
            Else
                test_id = customID
            End If
        End If

    End Sub

    ''' <summary>
    ''' Automatically sets the output directory to the same name as the test ID
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetOutPutDirectory()
        Dim outputDirTemp As String

        'Set Overall Location of Output Direcotry
        If output_directory_attribSameAsModelsRun Then
            outputDirTemp = models_run_directory.path
        Else
            outputDirTemp = output_directory.path
        End If

        'Append output folder name to path if results are to be stored in a subdirectory
        If output_directory_attribCreateSubDir Then outputDirTemp = outputDirTemp & "\" & test_id

        output_directory.SetProperties(outputDirTemp)
    End Sub

    ' Set Properties States
    ''' <summary>
    ''' Sets up defaults if not specified
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaults()
        SetDefaults(Me)
        SetterRegTest.SetDefaults(Me, _regTestXML)
    End Sub

    ''' <summary>
    ''' Sets up defaults if not specified
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaults(ByRef p_regTest As cRegTest)
        With p_regTest
            .copy_models_mirror_attribRun = True

            .run_local_test = True
            .run_using_batch_file = False

            .test_to_run = "run as is"

            .email_notifications_attribUse = False
        End With
    End Sub

    ''' <summary>
    ''' Sets up parameters in regTest based on common testing scenarios.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetTestRunResultsAction(ByRef p_regTest As cRegTest)
        With p_regTest
            'Get Test ID
            If testerSettings.csiTesterlevel = eCSiTesterLevel.Published Then
                'Set a constant custom test ID based on what is specified in regTest.xml
                'Set option to have all models, and resulting files, use the same, constant folder, named after the custom test ID specified in regTest.xml
                .models_run_directory_attribCreateSubDir = False
                .models_run_directory_attribNoReplace = False

                .output_directory_attribSameAsModelsRun = False
                .output_directory_attribCreateSubDir = False
                .output_directory_attribNoReplace = False
            Else
                'Set Auto ID, which changes between each run based on date/time
                'TODO: Reinstate this older behavior.
                If .autoFolders Then
                    'set option to have all models, and resulting files, copied to a new folder autonamed as the auto test id
                    .models_run_directory_attribCreateSubDir = True
                    .models_run_directory_attribNoReplace = True

                    .output_directory_attribSameAsModelsRun = True
                    .output_directory_attribCreateSubDir = False
                    .output_directory_attribNoReplace = False
                Else
                    'Set option to have all models, and resulting files, use the same, constant folder, named after the custom test ID specified in regTest.xml
                    .models_run_directory_attribCreateSubDir = False
                    .models_run_directory_attribNoReplace = False

                    .output_directory_attribSameAsModelsRun = False
                    .output_directory_attribCreateSubDir = False
                    .output_directory_attribNoReplace = False
                End If

                'TODO: Create enum of common test action combinations, to be specified in the function
                ''Set option to have all regTest result copied to a new folder autonamed as the auto test ID, but for the models destination folder files to be re-used (i.e. constant run-to-run)
                'models_run_directory_attribCreateSubDir = False
                'models_run_directory_attribNoReplace = False

                'output_directory_attribSameAsModelsRun = True
                'output_directory_attribCreateSubDir = True
                'output_directory_attribNoReplace = True
            End If
        End With
    End Sub
#End Region

End Class
