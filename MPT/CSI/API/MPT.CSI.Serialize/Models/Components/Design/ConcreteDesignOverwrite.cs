using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Helpers.Design;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    public class ConcreteDesignOverwrite : DesignOverwrites, IFrameDesign
    {
        /// <summary>
        /// The design section for the selected frame objects.When this overwrite is applied, any previous auto select section assigned to the frame object is removed.
        /// Program determined/null value means it is taken from the analysis section.
        /// </summary>
        /// <value>The design section.</value>
        public virtual FrameSection DesignSection { get; set; }

        /// <summary>
        /// Reduced Live Load Factor.
        /// The live load reduction factor.A reducible live load is multiplied by this factor to obtain the reduced live load for the frame object.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The RLLF.</value>
        public double RLLF { get; set; } = 0;

        #region Moments & Buckling
        #region Unbraced Lengths
        /// <summary>
        /// Unbraced length ratio, major.
        /// Unbraced length factor for buckling about the frame object major axis.
        /// This item is specified as a fraction of the frame object length.Multiplying this factor times the frame object length gives the unbraced length for the object.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections major bending is bending about the local 3-axis.For unsymmetrical sections(e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The xl major.</value>
        public double XLMajor { get; set; } = 0;

        /// <summary>
        /// Unbraced length ratio, minor.
        /// Unbraced length factor for buckling about the frame object minor axis.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying this factor times the frame object length gives the unbraced length for the object.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections minor bending is bending about the local 2-axis.For unsymmetrical sections(e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The xl minor.</value>
        public double XLMinor { get; set; } = 0;
        #endregion

        #region Effective Lengths        
        /// <summary>
        /// Effective Length, major.
        /// Effective length factor for buckling about the frame object major axis.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying the frame object length with this factor gives the effective length for the object.
        /// This item only applies to frame objects with column-type current design sections.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The k1 major.</value>
        public double? XKMajor { get; set; } = null;

        /// <summary>
        /// Effective Length, minor.
        /// Effective length factor for buckling about the frame object minor axis.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying the frame object length with this factor gives the effective length for the object.
        /// This item only applies to frame objects with column-type current design sections.
        /// Specifying 0 means the value is program determined.
        /// For symmetrical sections minor bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The k1 minor.</value>
        public double? XKMinor { get; set; } = null;
        #endregion
        #endregion
    }
}
