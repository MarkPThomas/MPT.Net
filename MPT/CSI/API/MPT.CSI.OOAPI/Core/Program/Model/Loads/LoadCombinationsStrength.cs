// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-25-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="LoadCombinationsStrength.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.API.Core.Program.ModelBehavior.Design;
using MPT.CSI.API.Core.Support;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Class LoadCombinationsStrength. This class cannot be inherited.
    /// </summary>
    public sealed class LoadCombinationsStrength
    {
        #region Fields & Properties

        /// <summary>
        /// The API combo strength
        /// </summary>
        private readonly IComboStrength _apiComboStrength;

        /// <summary>
        /// The load combinations
        /// </summary>
        private readonly LoadCombinations _loadCombinations;

        /// <summary>
        /// The combination names
        /// </summary>
        private List<string> _combinationNames;
        /// <summary>
        /// Gets or sets the stregnth load combination names.
        /// </summary>
        /// <value>The strength load combination names</value>
        internal List<string> CombinationNames
        {
            get
            {
                if (_combinationNames == null)
                {
                    Fill();
                }

                return _combinationNames;
            }
        }

        /// <summary>
        /// The combinations
        /// </summary>
        private List<LoadCombination> _combinations;
        /// <summary>
        /// Gets or sets the stregnth load combinations.
        /// </summary>
        /// <value>The strength load combinations.</value>
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
        /// Initializes a new instance of the <see cref="LoadCombinationsStrength"/> class.
        /// </summary>
        /// <param name="apiComboStrength">The API combo strength.</param>
        /// <param name="loadCombinations">The load combinations.</param>
        public LoadCombinationsStrength(IComboStrength apiComboStrength, LoadCombinations loadCombinations)
        {
            _apiComboStrength = apiComboStrength;
            _loadCombinations = loadCombinations;
        }



        #endregion

        #region Fill/Set
        /// <summary>
        /// Gets the load combination selected for strength design.
        /// </summary>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Fill()
        {
            _combinationNames = new List<string>(_apiComboStrength.GetComboStrength());
        }

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
        /// Selects or deselects a load combination for strength design.
        /// </summary>
        /// <param name="loadCombination">An existing load combination.</param>
        /// <param name="selectLoadCombination">True: The specified load combination is selected as a design combination for strength design.
        /// False: The combination is not selected for strength design.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        private void set(LoadCombination loadCombination, bool selectLoadCombination)
        {
            _apiComboStrength.SetComboStrength(loadCombination.Name, selectLoadCombination);

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
