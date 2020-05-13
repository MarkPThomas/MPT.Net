namespace MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames
{
    public interface INotionalSize
    {
        // TODO: Decide whether to keep INotionalSize. It only seems to apply to SAP2000 and need not be included in model text files.
        eNotionalSizeType SizeType { get; set; } // = eNotionalSizeType.Auto;
        double AutoScaleFactorSize { get; set; } //= 1;
        double UserValueSize { get; set; } //= 1;
    }
}
