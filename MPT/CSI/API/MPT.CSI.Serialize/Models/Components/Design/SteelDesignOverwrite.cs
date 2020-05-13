using System.ComponentModel;
using MPT.CSI.Serialize.Models.Components.Definitions.CrossSections.Frames;
using MPT.CSI.Serialize.Models.Helpers.Design;

namespace MPT.CSI.Serialize.Models.Components.Design
{

    public class SteelDesignOverwrite : DesignOverwrites, IFrameDesign
    {
        /// <summary>
        /// The design section for the selected frame objects.When this overwrite is applied, any previous auto select section assigned to the frame object is removed.
        /// Program determined/null value means it is taken from the analysis section.
        /// </summary>
        /// <value>The design section.</value>
        public virtual FrameSection DesignSection { get; set; }

        /// <summary>
        /// Yield stress.
        /// Material yield strength used in the design/check.
        /// Specifying 0 means the value is program determined.
        /// The program determined value is taken from the material property assigned to the frame object.
        /// </summary>
        /// <value>The fy.</value>
        public double Fy { get; set; } = 0;

        /// <summary>
        /// Reduced Live Load Factor.
        /// The live load reduction factor.A reducible live load is multiplied by this factor to obtain the reduced live load for the frame object.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The RLLF.</value>
        public double RLLF { get; set; } = 0;

        /// <summary>
        /// Net Area to Total Area ratio.
        /// The ratio of the net area at the design section to gross cross-sectional area of the section.
        /// This ratio affects the design of axial tension members.
        /// Specifying 0 means the value is program default which is 1.
        /// </summary>
        /// <value>The area ratio.</value>
        public double AreaRatio { get; set; } = 0;

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

        /// <summary>
        /// Unbraced length ratio, LTB.
        /// Unbraced length factor for lateral-torsional buckling for the frame object.This item is specified as a fraction of the frame object length.
        /// Multiplying this factor times the frame object length gives the unbraced length for the object.
        /// Specifying 0 means the value is program determined.
        /// </summary>
        /// <value>The XLLTB.</value>
        public double XLLTB { get; set; } = 0;
        #endregion

        #region Effective Lengths        
        /// <summary>
        /// Effective Length, braced, major.
        /// Effective length factor for buckling about the frame object major axis with an assumption that the frame is braced at the joints against sidesway.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying the frame object length with this factor gives the effective length for the object.
        /// Specifying 0 means the value is program determined.For beam design, this factor is always taken as 1 regardless of what may be specified in the overwrites.
        /// This factor is used for B1 factor.  
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The k1 major.</value>
        public double? K1Major { get; set; } = null;

        /// <summary>
        /// Effective Length, braced, minor.
        /// Effective length factor for buckling about the frame object minor axis with an assumption that the frame is braced at the joints against sidesway.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying the frame object length with this factor gives the effective length for the object.
        /// Specifying 0 means the value is program determined.For beam design, this factor is always taken as 1 regardless of what may be specified in the overwrites.
        /// This factor is used for B1 factor.  
        /// For symmetrical sections minor bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The k1 minor.</value>
        public double? K1Minor { get; set; } = null;

        /// <summary>
        /// Effective Length, sway, major.
        /// Effective length factor for buckling about the frame object major axis.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying the frame object length with this factor gives the effective length for the object.
        /// Specifying 0 means the value is program determined.
        /// For beam design, this factor is always taken as 1 regardless of what may be specified in the overwrites.
        /// This factor is used for axial compression capacity.
        /// For symmetrical sections major bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) major bending is the bending about the section principal axis with the larger moment of inertia.
        /// </summary>
        /// <value>The k2 major.</value>
        public double? K2Major { get; set; } = null;

        /// <summary>
        /// Effective Length, sway, minor.
        /// Effective length factor for buckling about the frame object minor axis.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying the frame object length with this factor gives the effective length for the object.
        /// Specifying 0 means the value is program determined.
        /// For beam design, this factor is always taken as 1 regardless of what may be specified in the overwrites.
        /// This factor is used for axial compression capacity.
        /// For symmetrical sections minor bending is bending about the local 3-axis.
        /// For unsymmetrical sections (e.g., angles) minor bending is the bending about the section principal axis with the smaller moment of inertia.
        /// </summary>
        /// <value>The k2 minor.</value>
        public double? K2Minor { get; set; } = null;

        /// <summary>
        /// Effective Length, LTB.
        /// Effective length factor for lateral-torsional buckling.
        /// This item is specified as a fraction of the frame object length.
        /// Multiplying the frame object length with this factor gives the effective length for the object.Specifying 0 means the value is program determined.
        /// For beam design, this factor is taken as 1 by default.
        /// This should be set by the user.  
        /// </summary>
        /// <value>The KLTB.</value>
        public double KLTB { get; set; } = 0;
        #endregion
        #endregion

        #region Deflection
        /// <summary>
        /// Toggle to consider whether deflection limitations should be considered in design.
        /// </summary>
        /// <value><c>true</c> if this instance is deflection considered; otherwise, <c>false</c>.</value>
        public bool? IsDeflectionConsidered { get; set; }

        /// <summary>
        /// Toggles to consider deflection limitations as absolute or as divisor of beam length (relative).
        /// </summary>
        public enum DeflectionCheckTypes
        {
            [Description("Program Determined")]
            ProgramDetermined = 0,
            Both = 1,
            Ratio = 2,
            Absolute = 3
        }
        /// <summary>
        /// Toggle to consider deflection limitations as absolute or as divisor of beam length (relative).
        /// </summary>
        /// <value>The type of the deflection check.</value>
        public DeflectionCheckTypes? DeflectionCheckType { get; set; } = DeflectionCheckTypes.ProgramDetermined;

        #region Deflection: Ratios
        /// <summary>
        /// Deflection limitation for dead load.
        /// Inputting 120 means that the limit is L/120. 
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio dl.</value>
        public double RatioDL { get; set; } = 0;

        /// <summary>
        /// Deflection limitation for superimposed dead plus live load.
        /// Inputting 120 means that the limit is L/120.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio SDL and ll.</value>
        public double RatioSDLAndLL { get; set; } = 0;

        /// <summary>
        /// Deflection limitation for superimposed live load.
        /// Inputting 360 means that the limit is L/360.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio ll.</value>
        public double RatioLL { get; set; } = 0;

        /// <summary>
        /// Deflection limitation for total load.
        /// Inputting 240 means that the limit is L/240.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio total.</value>
        public double RatioTotal { get; set; } = 0;

        /// <summary>
        /// Limitation for net deflection.
        /// Camber is subtracted from the total load deflection to get net deflection.
        /// Inputting 240 means that the limit is L/240.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio net.</value>
        public double RatioNet { get; set; } = 0;
        #endregion

        #region Deflection: Absolute

        /// <summary>
        /// Deflection limitation for dead load. [L]
        /// It is unit dependent.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio dl.</value>
        public double AbsoluteDL { get; set; } = 0;

        /// <summary>
        /// Deflection limitation for superimposed dead plus live load. [L]
        /// It is unit dependent.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio SDL and ll.</value>
        public double AbsoluteSDLAndLL { get; set; } = 0;

        /// <summary>
        /// Deflection limitation for superimposed live load. [L]
        /// It is unit dependent.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio ll.</value>
        public double AbsoluteLL { get; set; } = 0;

        /// <summary>
        /// Deflection limitation for total load. [L]
        /// It is unit dependent.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio total.</value>
        public double AbsoluteTotal { get; set; } = 0;

        /// <summary>
        /// Limitation for net deflection. [L]
        /// It is unit dependent.
        /// Camber is subtracted from the total load deflection to get net deflection.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The ratio net.</value>
        public double AbsoluteNet { get; set; } = 0;

        /// <summary>
        /// The specified amount of camber to be reported in the design output and to be used in the net deflection check. [L]
        /// It is unit dependent.
        /// Inputting zero is special, since it means no check has to be made for this item.
        /// </summary>
        /// <value>The specified camber.</value>
        public double SpecifiedCamber { get; set; } = 0;
        #endregion
        #endregion

        /// <summary>
        /// The demand/capacity ratio limit to be used for acceptability.
        /// D/C ratios that are less than or equal to this value are considered acceptable.
        /// </summary>
        /// <value>The demand capacity ratio limit.</value>
        public double DemandCapacityRatioLimit { get; set; } = 0;
    }
}
