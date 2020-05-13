using MPT.CSI.OOAPI.Core.Program.Model.StructureLayout;

namespace MPT.CSI.OOAPI.Core.Program.Model.Assignments.Frames
{
    public class LocalAxis
    {
        /// <summary>
        /// Rotation of 2-3 plane about the 1-1 axis.
        /// </summary>
        /// <value>The local axis alpha.</value>
        public double Alpha { get; protected set; }
        
        public void SetLocalAxis(Node nodeI, Node nodeJ)
        {
            double angle = nodeI.AngleZ(nodeJ);
            bool isNegative = angle < 0;
            double modulo = angle % 360;
            SetLocalAxis(modulo, isNegative);
        }

        public void SetLocalAxis(double value)
        {
            bool isNegative = value < 0;
            SetLocalAxis(value, isNegative);
        }

        public void SetLocalAxis(double value, bool isNegative)
        {
            if (isNegative)
            {
                Alpha = 360 - value;
            }
            else
            {
                Alpha = value;
            }
        }
    }
}
