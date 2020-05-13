// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-25-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="LoadCombinationsDeflection.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MPT.CSI.Serialize.Models.Components.Loads
{
    /// <summary>
    /// Class LoadCombinationsDeflection. This class cannot be inherited.
    /// </summary>
    public sealed class LoadCombinationsDeflection
    {
        #region Fields & Properties
        /// <summary>
        /// The load combinations
        /// </summary>
        private readonly LoadCombinations _loadCombinations;
        
        /// <summary>
        /// Gets or sets the deflection load combination names.
        /// </summary>
        /// <value>The combinations deflection.</value>
        internal List<string> CombinationNames { get; set; }

        /// <summary>
        /// The combinations
        /// </summary>
        private List<LoadCombination> _combinations;
        /// <summary>
        /// Gets or sets the deflection load combination.
        /// </summary>
        /// <value>The combinations deflection.</value>
        public ReadOnlyCollection<LoadCombination> Combinations
        {
            get
            {
                if (_combinations != null) return new ReadOnlyCollection<LoadCombination>(_combinations);

                _combinations = new List<LoadCombination>();
                foreach (var combinationName in CombinationNames)
                {
                    _combinations.Add(_loadCombinations.FillItem(combinationName));
                }
                return new ReadOnlyCollection<LoadCombination>(_combinations);
            }
        }
        #endregion

        #region Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadCombinationsDeflection"/> class.
        /// </summary>
        /// <param name="loadCombinations">The load combinations.</param>
        public LoadCombinationsDeflection(LoadCombinations loadCombinations)
        {
            _loadCombinations = loadCombinations;
        }


        #endregion

        #region Fill/Set
        /// <summary>
        /// Adds the combination.
        /// </summary>
        /// <param name="loadCombination">The load combination.</param>
        public void Add(LoadCombination loadCombination)
        {
            set(loadCombination, selectLoadCombination: true);
        }

        /// <summary>
        /// Removes the combination.
        /// </summary>
        /// <param name="loadCombination">The load combination.</param>
        public void Remove(LoadCombination loadCombination)
        {
            set(loadCombination, selectLoadCombination: false);
        }

        /// <summary>
        /// Selects or deselects a load combination for deflection design.
        /// </summary>
        /// <param name="loadCombination">An existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for deflection design.
        /// False: The combination is not selected for deflection design.</param>
        private void set(LoadCombination loadCombination, bool selectLoadCombination)
        {
            if (selectLoadCombination)
            {
                if (!CombinationNames.Contains(loadCombination.Name)) CombinationNames.Add(loadCombination.Name);
                if (!Combinations.Contains(loadCombination)) _combinations.Add(loadCombination);
            }
            else
            {
                CombinationNames.Remove(loadCombination.Name);
                _combinations.Remove(loadCombination);
            }
        }
        #endregion
    }
}
