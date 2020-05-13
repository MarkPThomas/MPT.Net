using System;
using System.Collections.Generic;
using MPT.CSI.API.Core.Helpers;
using MPT.CSI.API.Core.Program;
using MPT.CSI.API.Core.Program.ModelBehavior;
using MPT.CSI.OOAPI.Core.Helpers.Results;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Abstractions;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.CrossSections.FrameSections;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials;
using MPT.CSI.OOAPI.Core.Program.Model.Design;
using MPT.CSI.OOAPI.Core.Program.Model.Loads;
using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;
using ProgramDefinitions = MPT.CSI.API.Core.Program.ModelBehavior.Definitions;
using ProgramAnalysisResults = MPT.CSI.API.Core.Program.ModelBehavior.AnalysisResults;
using AnalysisLoads = MPT.CSI.API.Core.Helpers.Loads;
using Designer = MPT.CSI.API.Core.Program.ModelBehavior.Designer;
using Analyzer = MPT.CSI.OOAPI.Core.Program.Model.Analysis.Analyzer;

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
using Tendon = MPT.CSI.OOAPI.Core.Program.Model.StructureLayout.Tendon;
#endif

namespace MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings
{
    /// <summary>
    /// Serves to hold a list of each object instantiated of a given group/type.
    /// This class can be used to avoid redundant initialization of API objects.
    /// </summary>
    public static class Registry
    {
        public static CSiApplication Application { get; private set; }

        #region API Objects  
        public static ProgramDefinitions ProgramDefinitions => Application?.Model?.Definitions;
        public static ObjectModeler ObjectModeler => Application?.Model?.ObjectModel;
        public static AnalysisModeler AnalysisModeler => Application?.Model?.AnalysisModel;
        public static ProgramAnalysisResults AnalysisResults => Application?.Model?.Results;
        public static Designer Designer => Application?.Model?.Design;
        public static Editor Editor => Application?.Model?.Editor;
        #endregion

        #region Components
        public static Dictionary<string, Material> Materials { get; } = new Dictionary<string, Material>();
        public static Dictionary<string, FrameSection> FrameSections { get; } = new Dictionary<string, FrameSection>();
        public static Dictionary<string, AreaSection> AreaSections { get; } = new Dictionary<string, AreaSection>();
        #endregion

        #region Objects
        public static Dictionary<string, Node> Nodes { get; } = new Dictionary<string, Node>();
        public static Dictionary<string, Frame> Frames { get; } = new Dictionary<string, Frame>();
        public static Dictionary<string, Area> Areas { get; } = new Dictionary<string, Area>();
        public static Dictionary<string, Link> Links { get; } = new Dictionary<string, Link>();
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        public static Dictionary<string, Cable> Cables { get; } = new Dictionary<string, Cable>();
        public static Dictionary<string, Tendon> Tendons { get; } = new Dictionary<string, Tendon>();
        public static Dictionary<string, Solid> Solids { get; } = new Dictionary<string, Solid>();
#endif
        #endregion

        #region Abstractions & Groupings
        public static Dictionary<string, Diaphragm> Diaphragms { get; } = new Dictionary<string, Diaphragm>();
        public static Dictionary<string, Pier> Piers { get; } = new Dictionary<string, Pier>();
        public static Dictionary<string, Spandrel> Spandrels { get; } = new Dictionary<string, Spandrel>();

        public static Dictionary<string, Group> Groups { get; } = new Dictionary<string, Group>();
        #endregion

        #region Loads  
        public static Dictionary<string, LoadPattern> LoadPatterns { get; } = new Dictionary<string, LoadPattern>();
        public static Dictionary<string, LoadCase> LoadCases { get; } = new Dictionary<string, LoadCase>();
        public static Dictionary<string, LoadCombination> LoadCombinations { get; } = new Dictionary<string, LoadCombination>();
        #endregion

        #region Analysis AnalysisResults
        public static Analyzer Analyzer { get; } = Analyzer.Instance;
        public static List<Tuple<PierSpandrelResultsIdentifier, Forces>> PierResults { get; } = new List<Tuple<PierSpandrelResultsIdentifier, Forces>>();
        public static List<Tuple<PierSpandrelResultsIdentifier, Forces>> SpandrelResults { get; } = new List<Tuple<PierSpandrelResultsIdentifier, Forces>>();

        public static List<Tuple<SectionCutResultsIdentifier, Forces>> SectionCutDesignForces { get; } = new List<Tuple<SectionCutResultsIdentifier, Forces>>();
        public static List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>> SectionCutAnalysisForces { get; } = new List<Tuple<SectionCutResultsIdentifier, AnalysisLoads>>();

        public static List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>> ModalLoadParticipationRatios { get; } = new List<Tuple<ModalLoadParticipationResultsIdentifier, ModalLoadParticipationRatio>>();
        public static List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>> ModalParticipatingMassRatios { get; } = new List<Tuple<StepResultsIdentifier, ModalParticipatingMassRatio>>();
        public static List<Tuple<StepResultsIdentifier, ModalParticipationFactor>> ModalParticipationFactors { get; } = new List<Tuple<StepResultsIdentifier, ModalParticipationFactor>>();
        public static List<Tuple<StepResultsIdentifier, ModalPeriod>> ModalPeriods { get; } = new List<Tuple<StepResultsIdentifier, ModalPeriod>>();

#if BUILD_ETABS2016 || BUILD_ETABS2017
        public static List<Tuple<JointLabelNameResultsIdentifier, JointDrifts>> JointDrifts { get; } = new List<Tuple<JointLabelNameResultsIdentifier, JointDrifts>>();
        public static List<Tuple<LabelNameResultsIdentifier, StoryDrifts>> StoryDrifts { get; } = new List<Tuple<LabelNameResultsIdentifier, StoryDrifts>>();
#endif

        public static List<Tuple<StepResultsIdentifier, double>> BucklingFactors { get; } = new List<Tuple<StepResultsIdentifier, double>>();
        #endregion

        #region Design/Analysis Results
        public static SteelDesigner SteelDesigner { get; } = SteelDesigner.Instance;
        public static ConcreteDesigner ConcreteDesigner { get; } = ConcreteDesigner.Instance;
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        public static AluminumDesigner AluminumDesigner { get; } = AluminumDesigner.Instance;
        public static SteelColdFormedDesigner SteelColdFormedDesigner { get; } = SteelColdFormedDesigner.Instance;
#else
        public static CompositeBeamDesigner CompositeBeamDesigner { get; } = CompositeBeamDesigner.Instance;
        // public static ShearWallDesigner ShearWallDesigner { get; } = ShearWallDesigner.Instance;
        // public static SlabDesigner SlabDesigner { get; } = SlabDesigner.Instance;
#endif
        #endregion
            
        // TODO: For all, handle name as null or empty for Factory methods.
        // TODO: Write out to text file?

        public static void SetApplication(CSiApplication app)
        {
            Application = app;
        }
    }
}
