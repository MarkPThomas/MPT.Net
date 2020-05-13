// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-17-2018
// ***********************************************************************
// <copyright file="Model.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Areas;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

// TODO: For all, handle name as null or empty for FactoryGeneric methods.
// TODO: Write out to text file?

namespace MPT.CSI.Serialize.Models
{
    /// <summary>
    /// Class Model.
    /// </summary>
    public class Model
    {
        #region Fields & Properties 
        /// <summary>
        /// The file.
        /// </summary>
        private FileData _fileData;
        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <value>The file.</value>
        public FileData FileData => _fileData ?? (_fileData = new FileData());

        /// <summary>
        /// The settings.
        /// </summary>
        private Settings _settings;
        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public Settings Settings => _settings ?? (_settings = new Settings());

        private OutputSettings _outputSettings;
        /// <summary>
        /// Gets the output settings for table exports.
        /// </summary>
        /// <value>The output settings.</value>
        public OutputSettings OutputSettings => _outputSettings ?? (_outputSettings = new OutputSettings());

        /// <summary>
        /// The analysis
        /// </summary>
        private StructureAnalysis _analysis;
        /// <summary>
        /// Gets the analysis.
        /// </summary>
        /// <value>The analysis.</value>
        public StructureAnalysis Analysis => _analysis ?? (_analysis = new StructureAnalysis());

        /// <summary>
        /// The design
        /// </summary>
        private StructureDesign _design;
        /// <summary>
        /// Gets the design.
        /// </summary>
        /// <value>The design.</value>
        public StructureDesign Design => _design ?? 
                                         (_design = new StructureDesign(
                                                     Loading.Cases,
                                                     Loading.Combinations,
                                                     Groupings.Groups));

        /// <summary>
        /// The loading
        /// </summary>
        private StructureLoading _loading;
        /// <summary>
        /// Gets the loading.
        /// </summary>
        /// <value>The loading.</value>
        public StructureLoading Loading => _loading ?? 
                                           (_loading = new StructureLoading(Analysis.Analyzer));

        /// <summary>
        /// The structure components.
        /// </summary>
        private StructureComponents _components;
        /// <summary>
        /// The structural components, such as material and cross-sections.
        /// This is the model before an analysis is run.
        /// </summary>
        /// <value>The structural model.</value>
        public StructureComponents Components => _components ??
                                                 (_components = new StructureComponents());

        /// <summary>
        /// The structure.
        /// </summary>
        private StructureObjects _structure;

        /// <summary>
        /// The structural model.
        /// This is the model before an analysis is run.
        /// </summary>
        /// <value>The structural model.</value>
        public StructureObjects Structure => _structure ?? (_structure = new StructureObjects(
                                                 new StructureComponentsProperties<FrameSection>
                                                 {
                                                     CrossSections = Components.FrameSections,
                                                     Materials = Components.Materials,
                                                     Piers = Components.Piers,
                                                     Spandrels = Components.Spandrels
                                                 },
                                                 new StructureComponentsProperties<AreaSection>()
                                                 {
                                                     CrossSections = Components.AreaSections,
                                                     Materials = Components.Materials,
                                                     Piers = Components.Piers,
                                                     Spandrels = Components.Spandrels
                                                 }));

        /// <summary>
        /// The groupings
        /// </summary>
        private StructureGroupings _groupings;
        /// <summary>
        /// Gets the groupings.
        /// </summary>
        /// <value>The groupings.</value>
        public StructureGroupings Groupings => _groupings ?? (_groupings = new StructureGroupings(Structure));
        #endregion
        
        #region Initialization 

        /// <summary>
        /// Initializes a new instance of the <see cref="Model" /> class.
        /// </summary>
        internal Model() 
        {
        }
        #endregion
    }
}
