// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-20-2018
// ***********************************************************************
// <copyright file="MassSource.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Masses
{
    /// <summary>
    /// Represents the mass source in the application.
    /// </summary>
    public class MassSource
    {

#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017        
        /// <summary>
        /// The material properties API object.
        /// </summary>
        protected static MaterialProperties _materialProperties = Registry.ProgramDefinitions?.Properties?.MaterialProperties;
#else
        /// <summary>
        /// The material properties API object.
        /// </summary>
        protected static MassSource _materialProperties = Registry.ProgramDefinitions?.MassSource;
#endif

        /// <summary>
        /// The mass source name.
        /// </summary>
        /// <value>The name.</value>
        public string Name {get; protected set; }

        /// <summary>
        /// True: Element self mass is included in the mass.
        /// </summary>
        /// <value><c>true</c> if this instance is from elements; otherwise, <c>false</c>.</value>
        public bool IsFromElements { get; protected set; }

        /// <summary>
        /// True: Assigned masses are included in the mass.
        /// </summary>
        /// <value><c>true</c> if this instance is from masses; otherwise, <c>false</c>.</value>
        public bool IsFromMasses { get; protected set; }

        /// <summary>
        /// True: Specified load patterns are included in the mass.
        /// </summary>
        /// <value><c>true</c> if this instance is from loads; otherwise, <c>false</c>.</value>
        public bool IsFromLoads { get; protected set; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// True: Mass source is the default mass source.
        /// Only one mass source can be the default mass source so when this assignment is True all other mass sources are automatically set to have the IsDefault flag False.
        /// </summary>
        /// <value><c>true</c> if this instance is default; otherwise, <c>false</c>.</value>
        public bool IsDefault { get; protected set; }
#endif        
        /// <summary>
        /// Load pattern names specified for the mass source.
        /// </summary>
        /// <value>The names load patterns.</value>
        public string[] NamesLoadPatterns { get; protected set; }

        /// <summary>
        /// Load pattern multipliers specified for the mass source.
        /// </summary>
        /// <value>The scale factors.</value>
        public double[] ScaleFactors { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MassSource"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public MassSource(string name)
        {
            Name = name;
        }
        
        /// <summary>
        /// Retrieves the mass source data for an existing mass source.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void FillMassSource()
        {
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _materialProperties.GetMassSource(
                out var isMassFromElements,
                out var isMassFromMasses,
                out var isMassFromLoads,
                out var namesLoadPatterns,
                out var scaleFactors);
#else
            _materialProperties.GetMassSource(
                out var nameMassSource,
                out var isMassFromElements,
                out var isMassFromMasses,
                out var isMassFromLoads,
                out var isDefault,
                out var namesLoadPatterns,
                out var scaleFactors);

            Name = nameMassSource;
            IsDefault = isDefault;
#endif

            IsFromElements = isMassFromElements;
            IsFromMasses = isMassFromMasses;
            IsFromLoads = isMassFromLoads;
            NamesLoadPatterns = namesLoadPatterns;
            ScaleFactors = scaleFactors;
        }

        /// <summary>
        /// Adds a new mass source to the model or reinitializes an existing mass source.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetMassSource()
        {
#if BUILD_ETABS2015 || BUILD_ETABS2016 || BUILD_ETABS2017
            _materialProperties.SetMassSource(
                IsFromElements,
                IsFromMasses,
                IsFromLoads,
                NamesLoadPatterns,
                ScaleFactors);
#else
             _materialProperties.SetMassSource(
                Name,
                IsFromElements,
                IsFromMasses,
                IsFromLoads,
                IsDefault,
                NamesLoadPatterns,
                ScaleFactors);
#endif
        }
    }
}
