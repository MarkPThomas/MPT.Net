using MPT.CSI.Serialize.Models.Components.Design;

namespace MPT.CSI.Serialize.Models.Helpers.Design
{
    public class FrameDesignResults
    {
        /// <summary>
        /// The steel design results
        /// </summary>
        protected SteelDesignResults _steelDesignResults;
        /// <summary>
        /// Steel design results.
        /// </summary>
        /// <value>The steel design results.</value>
        public virtual SteelDesignResults SteelDesignResults => _steelDesignResults ?? (_steelDesignResults = null);


        /// <summary>
        /// The concrete design results
        /// </summary>
        protected ConcreteDesignResults _concreteDesignResults;
        /// <summary>
        /// Concrete design results.
        /// </summary>
        /// <value>The concrete design results.</value>
        public virtual ConcreteDesignResults ConcreteDesignResults => _concreteDesignResults ?? (_concreteDesignResults = null);

        protected AluminumDesignResults _aluminumDesignResults;
        /// <summary>
        /// Aluminum beam design results.
        /// </summary>
        /// <value>The composite beam design results.</value>
        public virtual AluminumDesignResults AluminumDesignResults => _aluminumDesignResults ?? (_aluminumDesignResults = null);


        protected SteelColdFormedDesignResults _steelColdFormedDesignResults;
        /// <summary>
        /// Cold-formed steel beam design results.
        /// </summary>
        /// <value>The composite beam design results.</value>
        public virtual SteelColdFormedDesignResults SteelColdFormedDesignResults => _steelColdFormedDesignResults ?? (_steelColdFormedDesignResults = null);

        /// <summary>
        /// The composite beam design results
        /// </summary>
        protected CompositeBeamDesignResults _compositeBeamDesignResults;
        /// <summary>
        /// Composite beam design results.
        /// </summary>
        /// <value>The composite beam design results.</value>
        public CompositeBeamDesignResults CompositeBeamDesignResults => _compositeBeamDesignResults ?? (_compositeBeamDesignResults = null);

    }
}
