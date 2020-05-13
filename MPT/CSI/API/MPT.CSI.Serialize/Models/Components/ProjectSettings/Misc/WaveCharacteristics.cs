namespace MPT.CSI.Serialize.Models.Components.ProjectSettings.Misc
{
    public class WaveCharacteristics
    {
        #region Fields & Properties
        public virtual string Characteristics { get; internal set; }
        public virtual string WaveType { get; internal set; }
        public virtual double KinematicsFactor { get; internal set; }
        public virtual double StormWaterDepth { get; internal set; }
        public virtual double Height { get; internal set; }
        public virtual double Period { get; internal set; }
        public virtual string Theory { get; internal set; }


        #endregion
    }
}
