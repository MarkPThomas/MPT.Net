using MPT.CSI.Serialize.Models.Helpers.Definitions.Units;

namespace MPT.CSI.Serialize.Models.Components.ProjectSettings
{
    public class ModelUnits
    {
        /// <summary>
        /// The present units.
        /// </summary>
        /// <value>The units present.</value>
        public eUnits Units { get; internal set; }

        #region ETABS
        /// <summary>
        /// Gets or sets the present force units.
        /// </summary>
        /// <value>The units force.</value>
        public eForce Force { get; internal set; }

        /// <summary>
        /// Gets or sets the present length units.
        /// </summary>
        /// <value>The length of the units.</value>
        public eLength Length { get; internal set; }

        /// <summary>
        /// Gets or sets the present temperature units.
        /// </summary>
        /// <value>The units temperature.</value>
        public eTemperature Temperature { get; internal set; }
        #endregion  
    }
}
