using MPT.CSI.Serialize.Models.Helpers.StructureLayout;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    public class MonitoredPointDOFTuple
    {
        /// <summary>
        /// The degree of freedom for which the displacement at a point object is monitored.
        /// This item applies only when <see cref="MonitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" />
        /// </summary>
        /// <value>The degree of freedom.</value>
        public eDegreeOfFreedom DegreeOfFreedom { get; set; }

        /// <summary>
        /// The name of the point object at which the displacement is monitored.
        /// This item applies only when <see cref="MonitoredDisplacementType" /> = <see cref="eMonitoredDisplacementType.AtSpecifiedPoint" />.
        /// </summary>
        /// <value>The name point.</value>
        public string NamePoint { get; set; }
    }
}
