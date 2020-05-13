// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-08-2018
// ***********************************************************************
// <copyright file="CSiOoApiBaseBase.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiAreaObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.AreaObject;
using ApiAreaSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.AreaSection;
using ApiDiaphragm = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.Diaphragm;
using ApiFrameObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.FrameObject;
using ApiFrameSection = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.FrameSection;
using ApiGroups = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Groups;
using ApiLinkObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.LinkObject;
using ApiLoadCombination = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCombinations;
using ApiLoadCase = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadCases;
using ApiLoadPattern = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPatterns;
using ApiMaterial = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property.MaterialProperties;
using ApiPier = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.PierLabel;
using ApiPointObject = MPT.CSI.API.Core.Program.ModelBehavior.ObjectModel.PointObject;
using ApiSpandrel = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.SpandrelLabel;
using ApiStory = MPT.CSI.API.Core.Program.ModelBehavior.Definition.Abstraction.Story;

namespace MPT.CSI.OOAPI.Core.Support
{
    /// <summary>
    /// Class CSiOoApiBaseBase.
    /// </summary>
    public class CSiOoApiBaseBase
    {
        #region Fields        
        /// <summary>
        /// The API application object.
        /// </summary>
        protected readonly ApiCSiApplication _apiApp;
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="CSiOoApiBaseBase" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        public CSiOoApiBaseBase(ApiCSiApplication app)
        {
            _apiApp = app;
        }

        #endregion



        #region API Objects
        /// <summary>
        /// Gets the API point object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiPointObject.</returns>
        protected static ApiPointObject getApiPointObject(ApiCSiApplication app)
        {
            return app?.Model?.ObjectModel?.PointObject; 
        }

        /// <summary>
        /// Gets the API frame object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiFrameObject.</returns>
        protected static ApiFrameObject getApiFrameObject(ApiCSiApplication app)
        {
            return app?.Model?.ObjectModel?.FrameObject;
        }

        /// <summary>
        /// Gets the API area object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiAreaObject.</returns>
        protected static ApiAreaObject getApiAreaObject(ApiCSiApplication app)
        {
            return app?.Model?.ObjectModel?.AreaObject;
        }

        /// <summary>
        /// Gets the API diaphragm.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiDiaphragm.</returns>
        protected static ApiDiaphragm getApiDiaphragm(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.Abstractions?.Diaphragm;
        }

        /// <summary>
        /// Gets the API pier.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiPier.</returns>
        protected static ApiPier getApiPier(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.Abstractions?.PierLabel;
        }

        /// <summary>
        /// Gets the API spandrel.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiSpandrel.</returns>
        protected static ApiSpandrel getApiSpandrel(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.Abstractions?.SpandrelLabel;
        }

        /// <summary>
        /// Gets the API load pattern.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiLoadPattern.</returns>
        protected static ApiLoadPattern getApiLoadPattern(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.LoadPatterns;
        }

        /// <summary>
        /// Gets the API load case.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiLoadCase.</returns>
        protected static ApiLoadCase getApiLoadCase(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.LoadCases;
        }

        /// <summary>
        /// Gets the API load combination.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiLoadCombination.</returns>
        protected static ApiLoadCombination getApiLoadCombination(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.LoadCombinations;
        }

        /// <summary>
        /// Gets the API groups.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiGroups.</returns>
        protected static ApiGroups getApiGroups(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.Groups;
        }

        /// <summary>
        /// Gets the API link object.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiLinkObject.</returns>
        protected static ApiLinkObject getApiLinkObject(ApiCSiApplication app)
        {
            return app?.Model?.ObjectModel?.LinkObject;
        }

        /// <summary>
        /// Gets the API frame section.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiFrameSection.</returns>
        protected static ApiFrameSection getApiFrameSection(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.Properties?.FrameSection;
        }

        /// <summary>
        /// Gets the API area section.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiAreaSection.</returns>
        protected static ApiAreaSection getApiAreaSection(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.Properties?.AreaSection;
        }

        /// <summary>
        /// Gets the API material properties.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiMaterial.</returns>
        protected static ApiMaterial getApiMaterialProperties(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.Properties?.MaterialProperties;
        }

        /// <summary>
        /// Gets the API story.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>ApiStory.</returns>
        protected static ApiStory getApiStory(ApiCSiApplication app)
        {
            return app?.Model?.Definitions?.Abstractions?.Story;
        }
        #endregion
    }
}
