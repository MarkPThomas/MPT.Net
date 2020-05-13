namespace MPT.CSI.Serialize.Models.Helpers.Definitions
{
    public class Quadrilateral
    {
        public Coordinate3DCartesian Point1 { get; set; }
        public Coordinate3DCartesian Point2 { get; set; }
        public Coordinate3DCartesian Point3 { get; set; }
        public Coordinate3DCartesian Point4 { get; set; }

        public bool IsValidQuadrilateral()
        {
            // TODO: Complete IsValidQuadrilateral
            return false;
        }
    }
}
