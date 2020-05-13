using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Definitions.Masses;
using MPT.CSI.Serialize.Models.Components.Grids;
using MPT.CSI.Serialize.Models.Components.ProjectSettings.Misc;
using MPT.CSI.Serialize.Models.Helpers.Definitions.Units;
using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Components.ProjectSettings
{
    public class ModelInformation
    {
        /// <summary>
        /// The database units for the new model.
        /// All data is internally stored in the model in these units.
        /// </summary>
        /// <value>The units initial.</value>
        public eUnits UnitsInitial { get; internal set; }


        public ModelUnits UnitsPresent { get; } = new ModelUnits();


        public ModelUnits UnitsDatabase { get; } = new ModelUnits();

        public CoordinateSystems CoordinateSystems { get; } = new CoordinateSystems();

        /// <summary>
        /// The program dimensional preferences.
        /// </summary>
        /// <value>The merge tolerance.</value>
        public DimensionalPreferences DimensionalPreferences { get; } = new DimensionalPreferences();


        /// <summary>
        /// The name of a defined coordinate system.
        /// </summary>
        /// <value>The present coord system.</value>
        public string PresentCoordinateSystem { get; internal set; }


        public DegreesOfFreedomGlobal ActiveDegreesOfFreedom { get; internal set; } = new DegreesOfFreedomGlobal();


        public WaveCharacteristics GeneralWaveCharacteristics { get;  } = new WaveCharacteristics();

        public List<string> PatternDefinitions { get; internal set; } = new List<string>();

        public bool AutoRegenerateHingesAfterImport { get; internal set; }

        public MassSources MassSources { get; internal set; }
    }
}
