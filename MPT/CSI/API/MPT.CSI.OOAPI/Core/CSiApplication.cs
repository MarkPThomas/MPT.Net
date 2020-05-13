// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="CSiApplication.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using CSiModel = MPT.CSI.OOAPI.Core.Program.CSiModel;

namespace MPT.CSI.OOAPI.Core
{
    /// <summary>
    /// Class CSiApplication.
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class CSiApplication :  IDisposable
    {
        #region Fields        
        /// <summary>
        /// The API application object.
        /// </summary>
        private readonly ApiCSiApplication _apiApp;

        /// <summary>
        /// The model.
        /// </summary>
        private CSiModel _model;
        #endregion

        #region Properties               

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The file.</value>
        public CSiModel Model => _model ?? (!IsInitialized ? _model = null : _model = new CSiModel(_apiApp));

        /// <summary>
        /// Path to the CSi application that the class manipulates.
        /// This might not be specifed if the object attaches to a process or initializes the default isntalled program.
        /// </summary>
        /// <value>The path.</value>
        public string Path => _apiApp.Path;

        /// <summary>
        /// Gets a value indicating whether the application is initialized.
        /// </summary>
        /// <value><c>true</c> if the application is initialized; otherwise, <c>false</c>.</value>
        public bool IsInitialized => (_apiApp.IsInitialized);

        /// <summary>
        /// The number of attempts before the library stops attempting to attach to the application.
        /// </summary>
        /// <value>The number of attempts.</value>
        public int NumberOfAttachmentAttempts { get; } = 1;

        /// <summary>
        /// The interval between attempts of attaching to the application.
        /// </summary>
        /// <value>The interval between attempts.</value>
        public int IntervalBetweenAttachmentAttachmentAttempts { get; } = 0;

        /// <summary>
        /// The number of exit attempts before the library stops attempting to exit the application.
        /// </summary>
        /// <value>The number of exit attempts.</value>
        public int NumberOfExitAttempts { get; } = 1;

        /// <summary>
        /// The interval between exit attempts of the application.
        /// </summary>
        /// <value>The interval between exit attempts.</value>
        public int IntervalBetweenExitAttempts { get; } = 0;
        #endregion

        #region Initialization     
        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApplication" /> class at the specified path.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="applicationPath">The application path.</param>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <param name="numberOfExitAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenExitAttempts">The interval between exit attempts of the application.</param>
        /// <returns>CSiApplication.</returns>
        public static CSiApplication Factory(string applicationPath,
            eUnits units = eUnits.kip_in_F,
            bool visible = true,
            string modelPath = "",
            int numberOfExitAttempts = 1,
            int intervalBetweenExitAttempts = 0)
        {
            try
            {
                CSiApplication app = new CSiApplication(applicationPath,
                    units,
                    visible,
                    modelPath,
                    numberOfExitAttempts,
                    intervalBetweenExitAttempts);
                return app;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApplication" /> class at the specified path.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="applicationPath">The application path.</param>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <param name="numberOfExitAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenExitAttempts">The interval between exit attempts of the application.</param>
        protected CSiApplication(string applicationPath,
            eUnits units = eUnits.kip_in_F,
            bool visible = true,
            string modelPath = "",
            int numberOfExitAttempts = 1,
            int intervalBetweenExitAttempts = 0)
        {
            _apiApp = new ApiCSiApplication(applicationPath,
                units,
                visible,
                modelPath,
                numberOfExitAttempts,
                intervalBetweenExitAttempts);
            NumberOfExitAttempts = numberOfExitAttempts;
            IntervalBetweenExitAttempts = intervalBetweenExitAttempts;
        }


#if !BUILD_SAP2000v18 && !BUILD_SAP2000v17 && !BUILD_SAP2000v16 && !BUILD_CSiBridgev18 && !BUILD_CSiBridgev17 && !BUILD_CSiBridgev16 && !BUILD_ETABS2015
        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApplication" /> class using the default installed application.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <param name="numberOfExitAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenExitAttempts">The interval between exit attempts of the application.</param>
        /// <returns>CSiApplication.</returns>
        public static CSiApplication Factory(eUnits units = eUnits.kip_in_F,
            bool visible = true,
            string modelPath = "",
            int numberOfExitAttempts = 1,
            int intervalBetweenExitAttempts = 0)
        {
            try
            {
                CSiApplication app = new CSiApplication(units,
                    visible,
                    modelPath,
                    numberOfExitAttempts,
                    intervalBetweenExitAttempts);
                return app;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApplication" /> class using the default installed application.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <param name="numberOfExitAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenExitAttempts">The interval between exit attempts of the application.</param>
        protected CSiApplication(eUnits units = eUnits.kip_in_F,
            bool visible = true,
            string modelPath = "",
            int numberOfExitAttempts = 1,
            int intervalBetweenExitAttempts = 0)
        {
            _apiApp = new ApiCSiApplication(units,
                visible,
                modelPath,
                numberOfExitAttempts,
                intervalBetweenExitAttempts);
            NumberOfExitAttempts = numberOfExitAttempts;
            IntervalBetweenExitAttempts = intervalBetweenExitAttempts;
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApplication" /> class by attaching to an existing process.
        /// </summary>
        /// <param name="numberOfAttempts">The number of attempts before the library stops attempting to attach to the application.</param>
        /// <param name="intervalBetweenAttempts">The interval between attempts of attaching to the application.</param>
        /// <param name="numberOfExitAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenExitAttempts">The interval between exit attempts of the application.</param>
        /// <returns>CSiApplication.</returns>
        public static CSiApplication Factory(int numberOfAttempts,
            int intervalBetweenAttempts,
            int numberOfExitAttempts = 1,
            int intervalBetweenExitAttempts = 0)
        {
            try
            {
                CSiApplication app = new CSiApplication(numberOfAttempts,
                    intervalBetweenAttempts,
                    numberOfExitAttempts,
                    intervalBetweenExitAttempts);
                return app;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiApplication" /> class by attaching to an existing process.
        /// </summary>
        /// <param name="numberOfAttachmentAttempts">The number of attempts before the library stops attempting to attach to the application.</param>
        /// <param name="intervalBetweenAttachmentAttempts">The interval between attempts of attaching to the application.</param>
        /// <param name="numberOfExitAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenExitAttempts">The interval between exit attempts of the application.</param>
        protected CSiApplication(int numberOfAttachmentAttempts,
            int intervalBetweenAttachmentAttempts,
            int numberOfExitAttempts = 1,
            int intervalBetweenExitAttempts = 0)
        {
            _apiApp = new ApiCSiApplication(numberOfAttachmentAttempts,
                        intervalBetweenAttachmentAttempts,
                        numberOfExitAttempts,
                        intervalBetweenExitAttempts);
            NumberOfAttachmentAttempts = numberOfAttachmentAttempts;
            IntervalBetweenAttachmentAttachmentAttempts = intervalBetweenAttachmentAttempts;
            NumberOfExitAttempts = numberOfExitAttempts;
            IntervalBetweenExitAttempts = intervalBetweenExitAttempts;
        }
#endif
        #endregion

        #region Methods: Public
        /// <summary>
        /// Opens a fresh instance of the CSi program.
        /// When the model is not visible it does not appear on screen and it does not appear in the Windows task bar.
        /// If no filename is specified, you can later open a model or create a model through the API.
        /// The file name must have an .sdb, .$2k, .s2k, .xls, or .mdb extension.
        /// Files with .sdb extensions are opened as standard SAP2000 files.
        /// Files with .$2k and .s2k extensions are imported as text files.
        /// Files with .xls extensions are imported as Microsoft Excel files.
        /// Files with .mdb extensions are imported as Microsoft Access files.
        /// </summary>
        /// <param name="units">The database units used when a new model is created.
        /// Data is internally stored in the program in the database units.</param>
        /// <param name="visible">True: The application is visible when started.
        /// False: The application is hidden when started.</param>
        /// <param name="modelPath">The full path of a model file to be opened when the application is started.
        /// If no file name is specified, the application starts without loading an existing model.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void InitializeNewProgram(eUnits units = eUnits.kip_in_F,
                                    bool visible = true,
                                    string modelPath = "")
        {
            _apiApp.InitializeNewProgram(units, visible, modelPath);
        }

        /// <summary>
        /// Attaches to an existing process.
        /// The program can be ended manually or by calling ApplicationExit
        /// Please note that if multiple instances of ETABS are manually started, an API client can only attach to the instance that was started first.
        /// </summary>
        /// <returns><c>true</c> if the process is successfully attached to, <c>false</c> otherwise.</returns>
        public bool AttachToProcess()
        {
            return _apiApp.AttachToProcess();
        }

        /// <summary>
        /// This function closes the application.
        /// If the model file is saved then it is saved with its current name.
        /// </summary>
        /// <param name="fileSave">True: The existing model file is saved prior to closing program.</param>
        /// <param name="numberOfAttempts">The number of exit attempts before the library stops attempting to exit the application.</param>
        /// <param name="intervalBetweenAttempts">The interval between exit attempts of the application.</param>
        /// <exception cref="CSiException">currentAttemptNumber + " of " + numberOfAttempts + " attempts to close the application failed."</exception>
        /// <exception cref="CSiException">"The application was unable to be closed."</exception>
        public void ApplicationExit(bool fileSave,
            int numberOfAttempts = -1,
            int intervalBetweenAttempts = -1)
        {
            _apiApp.ApplicationExit(fileSave, numberOfAttempts, intervalBetweenAttempts);
        }

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Retrieves the OAPI version number.
        /// </summary>
        /// <returns>System.Double.</returns>
        public double GetOAPIVersionNumber()
        {
            return _apiApp.GetOAPIVersionNumber();
        }
#endif

        /// <summary>
        /// This function hides the application.
        /// When the application is hidden it is not visible on the screen or on the Windows task bar.
        /// If the application is already hidden, no action is taken.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Hide()
        {
            _apiApp.Hide();
        }

        /// <summary>
        /// This function unhides the application.
        /// When the application is hidden it is not visible on the screen or on the Windows task bar.
        /// If the application is already visible, no action is taken.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void Unhide()
        {
            _apiApp.Unhide();
        }

        /// <summary>
        /// True: The application is visible on the screen.
        /// </summary>
        /// <returns><c>true</c> if this instance is visible; otherwise, <c>false</c>.</returns>
        public bool IsVisible()
        {
            return _apiApp.Visible();
        }

        /// <summary>
        /// Sets the active instance of a _SapObject in the system Running Object Table (ROT), replacing the previous instance(s).
        /// When a new _SapObject is created using the OAPI, it is automatically added to the system ROT if none is already present.
        /// Subsequent instances of the _SapObject do not alter the ROT as long as at least one active instance of a _SapObject is present in the ROT.
        /// The Windows API call GetObject() can be used to attach to the active _SapObject instance registered in the ROT.
        /// When the application is started normally (i.e. not from the OAPI), it does not add an instance of the _SapObject to the ROT, hence the GetObject() call cannot be used to attach to the active _SapObject instance.
        /// The Windows API call CreateObject() or New Sap2000v16._SapObject command can be used to attach to an instance of SAP2000 that is started normally (i.e. not from the OAPI).
        /// If there are multiple such instances, the first instance that will be attached to is the one that is started first.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void SetAsActiveObject()
        {
            _apiApp.SetAsActiveObject();
        }

        /// <summary>
        /// This function removes the current instance of a _sapObject from the system Running Object Table (ROT).
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void UnsetAsActiveObject()
        {
            _apiApp.UnsetAsActiveObject();
        }
        #endregion


        #region IDisposable        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            ApplicationExit(fileSave: false);
        }
        #endregion
    }
}
