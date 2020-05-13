namespace MPT.CSI.OOAPI.Core.Program.Model.Definitions.Materials
{
    public abstract class SteelMetal : Metal
    {
        /// <summary>
        /// The expected yield stress. [F/L^2].
        /// </summary>
        /// <value>The fye.</value>
        public double Fye { get; set; }

        /// <summary>
        /// The expected tensile stress. [F/L^2].
        /// </summary>
        /// <value>The fye.</value>
        public double Fue { get; set; }
        
        /// <summary>
        /// The strain at the onset of strain hardening.
        /// This item applies only to parametric stress-strain curves.
        /// </summary>
        /// <value>The strain at hardening.</value>
        public double StrainAtHardening { get; set; }

        protected SteelMetal(string name) : base(name)
        {
        }
    }
}
