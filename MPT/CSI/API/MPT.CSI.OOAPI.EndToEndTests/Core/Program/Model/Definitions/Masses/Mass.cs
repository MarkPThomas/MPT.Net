using MPT.CSI.API.Core.Helpers;
using MassGeneric = MPT.CSI.API.Core.Helpers.Mass;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Masses
{
    public class Mass
    {
        public bool IsLocalCoordinateSystem { get; protected set; }
        private API.Core.Helpers.Mass _mass;
        private MassWeight _massAsWeight;
        private MassVolume _massAsVolume;

        public Mass(API.Core.Helpers.Mass mass)
        {
            _mass = mass;
        }

        public MassGeneric GetMass()
        {
            return _mass;
        }

        public MassWeight GetMassByWeight()
        {
            return _massAsWeight;
        }

        /// <summary>
        /// Gets the mass by volume.
        /// The program calculates the mass by multiplying the specified values by the mass per unit volume of the specified material property.
        /// </summary>
        /// <param name="materialProperty">The material property.</param>
        /// <returns>MassVolume.</returns>
        public MassVolume GetMassByVolume(string materialProperty)
        {
            // TODO: Finish GetMassByVolume.
            return _massAsVolume;
        }
    }
}
