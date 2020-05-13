using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Loads;
using MPT.CSI.Serialize.Models.Helpers.ProjectSettings;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Components.ProjectSettings
{
    public class TableSet : IUniqueName
    {
        public string Name { get; }
        public string SortOrder { get; internal set; }
        public bool IsUnformatted { get; internal set; }

        public int ModeStart { get; internal set; }
        public int ModeEnd { get; internal set; }
        public bool AllModesUsed { get; internal set; } = true;
        
        public double BaseReactionXCoordinate { get; internal set; }
        public double BaseReactionYCoordinate { get; internal set; }
        public double BaseReactionZCoordinate { get; internal set; }

        public eMultiStepResultsOutput ModalHistoryOutput { get; internal set; }
        public eMultiStepResultsOutput DirectIntegrationHistoryOutput { get; internal set; }
        public eMultiStepResultsOutput NonlinearStaticOutput { get; internal set; }
        public eLoadCombinationOutput LoadCombinationOutput { get; internal set; }
        public string SteadyStateOutput { get; internal set; }
        public string SteadyStateOutputOption { get; internal set; }
        public string PowerSpectralDensityOption { get; internal set; }
        public eMultiStepResultsOutput MultiStepStaticOutput { get; internal set; }

        public virtual List<LoadPattern> LoadPatterns { get; } = new List<LoadPattern>();
        public virtual List<LoadCase> LoadCases { get; } = new List<LoadCase>();
        public virtual List<LoadCombination> LoadCombinations { get; } = new List<LoadCombination>();
        public virtual List<string> TableNames { get; } = new List<string>();

        public TableSet(string name)
        {
            Name = name;
        }
    }
}
