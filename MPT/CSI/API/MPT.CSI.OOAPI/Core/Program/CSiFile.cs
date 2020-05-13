// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="CSiFile.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.IO;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Helpers.StructureLayout;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiFile = MPT.CSI.API.Core.Program.CSiFile;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Program
{
    /// <summary>
    /// Class CSiFile.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    public class CSiFile : CSiOoApiBaseBase
    {
        #region Fields
        /// <summary>
        /// The API application object.
        /// </summary>
        /// <value>The API file.</value>
        private ApiCSiFile _apiFile =>_apiApp?.Model?.File;
        #endregion

        #region Properties
        /// <summary>
        /// Filename of the current model file, with or without the full path.
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName => _apiFile?.FileName;

        /// <summary>
        /// Path to the current model file.
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath => _apiFile?.FilePath;

        #endregion

        #region Initialization 
        /// <summary>
        /// Initializes a new instance of the <see cref="CSiFile"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        internal CSiFile(ApiCSiApplication app) : base(app)
        {
        }

        #endregion

        #region Methods: Public
        /// <summary>
        /// Opens the specified model file if it exists.
        /// The file name must have an sdb, $2k, s2k, xlsx, xls, or mdb extension.
        /// Files with sdb extensions are opened as standard Sap2000 files.
        /// Files with $2k and s2k extensions are imported as text files.
        /// Files with xlsx and xls extensions are imported as Microsoft Excel files.
        /// Files with mdb extensions are imported as Microsoft Access files.
        /// Returns zero if the file is successfully opened and nonzero if it is not opened.
        /// The function is only applicable when you are accessing the application API from an external application.
        /// It will return an error if you call it from VBA inside Sap2000.
        /// </summary>
        /// <param name="filePath">The full path of a model file to be opened in the application.</param>
        /// <returns><c>true</c> if model is successfully opened, <c>false</c> otherwise.</returns>
        /// <exception cref="IOException">The following file does not exist: " + <paramref name="filePath" /></exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public bool Open(string filePath)
    {
        return _apiFile.Open(filePath);
    }

        /// <summary>
        /// Saves the model to a file.
        /// If no file name is specified, the file is saved using its current name.
        /// </summary>
        /// <param name="filePath">The full path to which the model file is saved.</param>
        /// <returns><c>true</c> if the file has been saved, <c>false</c> otherwise.</returns>
        /// <exception cref="IOException">The current model has not been previously saved. Please provide a file name.</exception>
        /// <exception cref="IOException">The saved path provided is for a read only file.\n Please change the file access or provide a different file name.</exception>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public bool Save(string filePath = "")
        {
            return _apiFile.Save(filePath);
        }



        /// <summary>
        /// The function returns a string that represents the filename of the current model, with or without the full path.
        /// Object properties are also updated to the model.
        /// </summary>
        /// <param name="includePath">True: Returned filename includes the full path where the file is located.</param>
        /// <returns>System.String.</returns>
        public string UpdateModelFileName(bool includePath)
        {
            return _apiFile.UpdateModelFileName(includePath);
        }

        /// <summary>
        /// Returns a string that represents the filepath of the current model.
        /// Object properties are also updated to the model.
        /// </summary>
        /// <returns>System.String.</returns>
        public string UpdateModelFilePath()
        {
            return _apiFile.UpdateModelFilePath();
        }

        /// <summary>
        /// The function returns a string that represents the filename of the current model, with or without the full path.
        /// </summary>
        /// <param name="includePath">True: Returned filename includes the full path where the file is located.</param>
        /// <returns>System.String.</returns>
        public string GetModelFileName(bool includePath = false)
        {
            return _apiFile.GetModelFileName(includePath);
        }

        /// <summary>
        /// Returns 'true' if the models is loaded.
        /// Otherwise, either the model is new and unsaved or is still in the process of loading.
        /// </summary>
        /// <returns><c>true</c> if the model is loaded, <c>false</c> otherwise.</returns>
        public bool ModelIsLoaded()
        {
            return _apiFile.ModelIsLoaded();
        }

        #endregion

        #region Template Models

        /// <summary>
        /// Creates a new blank model from template.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void NewBlank()
        {
            _apiFile?.NewBlank();
        }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Creates a new template model of a 2D Frame.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void New2DFrame(Frame2DTemplate frame2DTemplate)
        {
            _apiFile.New2DFrame(
                frame2DTemplate.TemplateType,
                frame2DTemplate.NumberOfStories,
                frame2DTemplate.StoryHeight,
                frame2DTemplate.NumberOfBays,
                frame2DTemplate.BayWidth,
                frame2DTemplate.AddRestraints,
                frame2DTemplate.Beam,
                frame2DTemplate.Column,
                frame2DTemplate.Brace);
        }
#endif
#if BUILD_SAP2000v19 || BUILD_SAP2000v20
        /// <summary>
        /// Creates a new template model of a 3D Frame.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void New3DFrame(Frame3DTemplate frame3DTemplate)
        {
            _apiFile.New3DFrame(
                frame3DTemplate.TemplateType,
                frame3DTemplate.NumberOfStories,
                frame3DTemplate.StoryHeight,
                frame3DTemplate.NumberOfBaysX,
                frame3DTemplate.NumberOfBaysY,
                frame3DTemplate.BayWidthX,
                frame3DTemplate.BayWidthY,
                frame3DTemplate.NumberOfXDivisions,
                frame3DTemplate.NumberOfYDivisions,
                frame3DTemplate.AddRestraints,
                frame3DTemplate.Beam,
                frame3DTemplate.Column,
                frame3DTemplate.Area);
        }

        /// <summary>
        /// Creates a new template model of a Beam.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void NewBeam(BeamTemplate beamTemplate)
        {
            _apiFile.NewBeam(
                beamTemplate.NumberOfSpans,
                beamTemplate.SpanLength,
                beamTemplate.AddRestraints,
                beamTemplate.Beam);
        }

        /// <summary>
        /// Creates a new template model of a Wall.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void NewWall(WallTemplate wallTemplate)
        {
            _apiFile.NewWall(
                wallTemplate.NumberOfXDivisions,
                wallTemplate.NumberOfZDivisions,
                wallTemplate.DivisionWidthX,
                wallTemplate.DivisionWidthZ,
                wallTemplate.AddRestraints,
                wallTemplate.Area);
        }

        /// <summary>
        /// Creates a new template model of a Solid.
        /// Do not use this function to add to an existing model.
        /// This function should be used only for creating a new model and typically would be preceded by calls to ApplicationStart or InitializeNewModel.
        /// </summary>
        /// <exception cref="MPT.CSI.API.Core.Support.CSiException"></exception>
        public void NewSolidBlock(SolidBlockTemplate solidBlockTemplate)
        {
            _apiFile.NewSolidBlock(
                solidBlockTemplate.WidthX,
                solidBlockTemplate.WidthY,
                solidBlockTemplate.Height,
                solidBlockTemplate.AddRestraints,
                solidBlockTemplate.Solid,
                solidBlockTemplate.NumberOfXDivisions,
                solidBlockTemplate.NumberOfYDivisions,
                solidBlockTemplate.NumberOfZDivisions);
        }
#elif BUILD_ETABS2013 || BUILD_ETABS2014 || BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
        /// <summary>
        /// Creates a new grid-only model from template.
        /// </summary>
        /// <param name="gridTemplate">The grid template.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void NewGridOnly(GridTemplate gridTemplate)
        {
            _apiFile.NewGridOnly(
                gridTemplate.NumberOfStories,
                gridTemplate.TypicalStoryHeight,
                gridTemplate.BottomStoryHeight,
                gridTemplate.NumberOfLinesX,
                gridTemplate.NumberOfLinesY,
                gridTemplate.SpacingX,
                gridTemplate.SpacingY);
        }

        /// <summary>
        /// Creates a new steel deck model from template.
        /// </summary>
        /// <param name="steelDeckTemplate">The steel deck template.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void NewSteelDeck(SteelDeckTemplate steelDeckTemplate)
        {
            _apiFile.NewSteelDeck(
                steelDeckTemplate.NumberOfStories,
                steelDeckTemplate.TypicalStoryHeight,
                steelDeckTemplate.BottomStoryHeight,
                steelDeckTemplate.NumberOfLinesX,
                steelDeckTemplate.NumberOfLinesY,
                steelDeckTemplate.SpacingX,
                steelDeckTemplate.SpacingY);
        }
#endif
        #endregion
    }
}
