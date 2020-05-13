namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    public class ModalEigenLoad
    {
        public virtual string LoadType { get; internal set; }
        public virtual string LoadName { get; internal set; }
        public virtual double TargetMassParticipationRatio { get; internal set; }
        public virtual bool IsStaticCorrectionModeCalculated { get; internal set; }
    }
}
