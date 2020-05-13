using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Aluminum;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.ColdFormed;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.CompositeBeam;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Concrete;
using MPT.CSI.Serialize.Models.Helpers.Design.Overwrites.Steel;
using MPT.CSI.Serialize.Models.Support;

namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    /// <summary>
    /// Frame design overwrites for all applicable code material types.
    /// </summary>
    public class FrameDesignOverwrites
    {
        /// <summary>
        /// The steel design overwrite
        /// </summary>
        protected FrameDesignOverwrites<SteelDesignOverwrite, SteelDesignOverwriteProperties> _steelDesignOverwrite;
        /// <summary>
        /// Steel design overwrite.
        /// </summary>
        /// <value>The steel design overwrite.</value>
        public virtual FrameDesignOverwrites<SteelDesignOverwrite, SteelDesignOverwriteProperties> SteelDesignOverwrite => 
            _steelDesignOverwrite ?? (_steelDesignOverwrite = new FrameDesignOverwrites<SteelDesignOverwrite, SteelDesignOverwriteProperties>());


        /// <summary>
        /// The concrete design overwrite
        /// </summary>
        protected FrameDesignOverwrites<ConcreteDesignOverwrite, ConcreteDesignOverwriteProperties> _concreteDesignOverwrite;
        /// <summary>
        /// Concrete design overwrite.
        /// </summary>
        /// <value>The concrete design overwrite.</value>
        public virtual FrameDesignOverwrites<ConcreteDesignOverwrite, ConcreteDesignOverwriteProperties> ConcreteDesignOverwrite => 
            _concreteDesignOverwrite ?? (_concreteDesignOverwrite = new FrameDesignOverwrites<ConcreteDesignOverwrite, ConcreteDesignOverwriteProperties>());

        protected FrameDesignOverwrites<AluminumDesignOverwrite, AluminumDesignOverwriteProperties> _aluminumDesignOverwrite;
        /// <summary>
        /// Aluminum beam design overwrite.
        /// </summary>
        /// <value>The composite beam design overwrite.</value>
        public virtual FrameDesignOverwrites<AluminumDesignOverwrite, AluminumDesignOverwriteProperties> AluminumDesignOverwrite => 
            _aluminumDesignOverwrite ?? (_aluminumDesignOverwrite = new FrameDesignOverwrites<AluminumDesignOverwrite, AluminumDesignOverwriteProperties>());


        protected FrameDesignOverwrites<SteelColdFormedDesignOverwrite, SteelColdFormedDesignOverwriteProperties> _steelColdFormedDesignOverwrite;
        /// <summary>
        /// Cold-formed steel beam design overwrite.
        /// </summary>
        /// <value>The composite beam design overwrite.</value>
        public virtual FrameDesignOverwrites<SteelColdFormedDesignOverwrite, SteelColdFormedDesignOverwriteProperties> SteelColdFormedDesignOverwrite => 
            _steelColdFormedDesignOverwrite ?? (_steelColdFormedDesignOverwrite = new FrameDesignOverwrites<SteelColdFormedDesignOverwrite, SteelColdFormedDesignOverwriteProperties>());

        /// <summary>
        /// The composite beam design overwrite
        /// </summary>
        protected FrameDesignOverwrites<CompositeBeamDesignOverwrite, CompositeBeamDesignOverwriteProperties> _compositeBeamDesignOverwrite;
        /// <summary>
        /// Composite beam design overwrite.
        /// </summary>
        /// <value>The composite beam design overwrite.</value>
        public FrameDesignOverwrites<CompositeBeamDesignOverwrite, CompositeBeamDesignOverwriteProperties> CompositeBeamDesignOverwrite =>
            _compositeBeamDesignOverwrite ?? (_compositeBeamDesignOverwrite = new FrameDesignOverwrites<CompositeBeamDesignOverwrite, CompositeBeamDesignOverwriteProperties>());
    }

    /// <summary>
    /// Frame overwrites for any code that applies to the specified design material type.
    /// </summary>
    /// <typeparam name="T1">The type of overwrites common to a material.</typeparam>
    /// <typeparam name="T2">The type of overwrites specific to a particular code from the material.</typeparam>
    /// <seealso cref="MPT.CSI.Serialize.Models.Support.TypeList{T2}" />
    public class FrameDesignOverwrites<T1, T2> : TypeList<T2> 
        where T1 : DesignOverwrites, IFrameDesign, new()
        where T2 : DesignOverwriteProperties, new()
    {
        // Some overwrites apply to all codes & are always visible ('global')
        // Some overwrites are specific to a code but are recoverable after swapping codes
        // Some overwrites are specific to cross section (e.g. concrete column vs. beam)

        #region Fields & Properties        
        /// <summary>
        /// Basic overwrite properties common to all codes of the frame design type.
        /// </summary>
        /// <value>The generic overwrites.</value>
        public T1 GenericOverwrites { get; set; } = new T1();
        #endregion
    }
}
