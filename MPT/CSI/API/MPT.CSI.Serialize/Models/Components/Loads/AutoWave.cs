
using MPT.CSI.Serialize.Models.Components.ProjectSettings.Misc;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    public class AutoWave : AutoLoadPattern
    {
        #region Fields & Properties
        public virtual WaveCharacteristics Characteristics { get; internal set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoWind"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        internal AutoWave(string name) : base(name)
        {
            Type = eLoadPatternType.Wave;
        }
    }
}
