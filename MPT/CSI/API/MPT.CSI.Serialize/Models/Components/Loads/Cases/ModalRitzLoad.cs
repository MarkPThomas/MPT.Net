namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    public class ModalRitzLoad
    {
        public virtual string LoadType { get; internal set; }
        public virtual string LoadName { get; internal set; }
        public virtual double MaximumCycles { get; internal set; }
        public virtual double TargetDynamicParticipationRatio { get; internal set; }
    }
}
