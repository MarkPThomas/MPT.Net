// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-24-2018
// ***********************************************************************
// <copyright file="StructureDesign.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Components.Definitions;
using MPT.CSI.Serialize.Models.Components.Design;
using MPT.CSI.Serialize.Models.Components.Loads;

namespace MPT.CSI.Serialize.Models.Components
{
    /// <summary>
    /// Class StructureDesign.
    /// </summary>
    public class StructureDesign
    {
        #region Fields & Properties 
        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The load combinations
        /// </summary>
        protected readonly LoadCombinations _loadCombinations;

        /// <summary>
        /// The groups
        /// </summary>
        protected readonly Groups _groups;


        /// <summary>
        /// The steel designer
        /// </summary>
        private SteelDesigner _steelDesigner;
        /// <summary>
        /// Gets the steel designer.
        /// </summary>
        /// <value>The steel designer.</value>
        public SteelDesigner SteelDesigner => _steelDesigner ?? 
                                              (_steelDesigner = new SteelDesigner(_groups, _loadCombinations, _loadCases));
        

        private AluminumDesigner _aluminumDesigner;

        public AluminumDesigner AluminumDesigner => _aluminumDesigner ?? 
                                                    (_aluminumDesigner = new AluminumDesigner(_groups, _loadCombinations, _loadCases));


        private SteelColdFormedDesigner _steelColdFormedDesigner;

        public SteelColdFormedDesigner SteelColdFormedDesigner => _steelColdFormedDesigner ?? 
                                                                    (_steelColdFormedDesigner = new SteelColdFormedDesigner(_groups, _loadCombinations, _loadCases));
        /// <summary>
        /// The concrete designer
        /// </summary>
        private ConcreteDesigner _concreteDesigner;
        /// <summary>
        /// Gets the concrete designer.
        /// </summary>
        /// <value>The concrete designer.</value>
        public ConcreteDesigner ConcreteDesigner => _concreteDesigner ??
                                                    (_concreteDesigner = new ConcreteDesigner(_groups, _loadCombinations));

        /// <summary>
        /// The composite beam designer
        /// </summary>
        private CompositeBeamDesigner _compositeBeamDesigner;
        /// <summary>
        /// Gets the composite beam designer.
        /// </summary>
        /// <value>The composite beam designer.</value>
        public CompositeBeamDesigner CompositeBeamDesigner => _compositeBeamDesigner ??
                                (_compositeBeamDesigner = new CompositeBeamDesigner(_groups, _loadCombinations, _loadCases));

        // TODO: Finish shear wall designer
        // private ShearWallDesigner _shearWallDesigner;

        // public ShearWallDesigner ShearWallDesigner => _shearWallDesigner ?? (_shearWallDesigner = new ShearWallDesigner(_apiApp));

        // TODO: Finish slab designer
        // private SlabDesigner _slabDesigner;

        // public SlabDesigner SlabDesigner => _slabDesigner ?? (_slabDesigner = new SlabDesigner(_apiApp));
        
        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="StructureDesign" /> class.
        /// </summary>
        /// <param name="loadCases">The load cases.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        /// <param name="groups">The groups.</param>
        internal StructureDesign(
            LoadCases loadCases,
            LoadCombinations loadCombinations,
            Groups groups) 
        {
            _loadCases = loadCases;
            _loadCombinations = loadCombinations;
            _groups = groups;
        }
    }
}
