using MPT.CSI.API.Core.Program.ModelBehavior.Definition.Property;

namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    public abstract class Metal : Material
    {
        /// <summary>
        /// The minimum yield stress. [F/L^2].
        /// </summary>
        /// <value>The fye.</value>
        public double Fy { get; set; }

        /// <summary>
        /// The minimum tensile stress. [F/L^2].
        /// </summary>
        /// <value>The fye.</value>
        public double Fu { get; set; }


        /// <summary>
        /// The stress-strain hysteresis type.
        /// </summary>
        /// <value>The type of the stress strain hysteresis.</value>
        public eHysteresisType StressStrainHysteresisType { get; set; }


        /// <summary>
        /// This item applies only to parametric stress-strain curves.
        /// It is a multiplier on the material modulus of elasticity, E.
        /// This value multiplied times E gives the final slope of the curve.
        /// </summary>
        /// <value>The final slope.</value>
        public double FinalSlope { get; set; }

        protected Metal(string name) : base(name)
        {
        }
    }
}
