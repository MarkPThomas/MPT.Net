// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 11-19-2018
// ***********************************************************************
// <copyright file="LoadPattern.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Program.Model.ProjectSettings;
using MPT.CSI.OOAPI.Core.Support;
using ApiLoadPattern = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPatterns;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Represents a load pattern.
    /// </summary>
    public class LoadPattern : CSiOOAPiName
    {
        /// <summary>
        /// The load pattern API object.
        /// </summary>
        protected static ApiLoadPattern _loadPattern = Registry.ProgramDefinitions.LoadPatterns;


        /// <summary>
        /// Load Pattern type.
        /// </summary>
        /// <value>The type.</value>
        public eLoadPatternType Type { get; set; }

        /// <summary>
        /// Gets or sets the self weight multiplier.
        /// </summary>
        /// <value>The self weight multiplier.</value>
        public double SelfWeightMultiplier { get; set; }

        /// <summary>
        /// Gets or sets the automatic load pattern.
        /// </summary>
        /// <value>The automatic load pattern.</value>
        public AutoLoadPattern AutoLoadPattern { get; protected set; }



        /// <summary>
        /// Gets all load patterns.
        /// </summary>
        /// <returns>List&lt;LoadPattern&gt;.</returns>
        public static List<LoadPattern> GetAll()
        {
            List<LoadPattern> loadPatterns = new List<LoadPattern>();
            List<string> loadPatternNames = GetNameList();
            foreach (var loadPatternName in loadPatternNames)
            {
                LoadPattern loadPattern = Factory(loadPatternName);

                loadPatterns.Add(loadPattern);
            }

            return loadPatterns;
        }

        /// <summary>
        /// Returns a new load pattern class.
        /// </summary>
        /// <param name="uniqueName">Unique load pattern name.</param>
        /// <returns>Steel.</returns>
        public static LoadPattern Factory(string uniqueName)
        {
            if (Registry.Materials.Keys.Contains(uniqueName)) return Registry.LoadPatterns[uniqueName];

            LoadPattern loadPattern = new LoadPattern(uniqueName);
            if (_loadPattern != null)
            {
                loadPattern.FillData();
            }
            Registry.LoadPatterns.Add(uniqueName, loadPattern);
            return loadPattern;
        }
        
        /// <summary>
        /// Gets the name list.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public static List<string> GetNameList()
        {
            return _loadPattern == null ? new List<string>() : new List<string>(_loadPattern.GetNameList());
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="LoadPattern"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public LoadPattern(string name) : base(name) { }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public override void FillData()
        {
            FillLoadType();
            FillSelfWtMultiplier();
            FillAutoCode();
        }

        // TODO: Work into factory
        /// <summary>
        /// Adds a new load pattern.
        /// An error is returned if the <paramref name="name" /> item is already used for an existing load pattern.
        /// </summary>
        /// <param name="name">Name for the new load pattern.</param>
        /// <param name="loadPatternType">Load pattern type.</param>
        /// <param name="selfWeightMultiplier">Self weight multiplier for the new load pattern.</param>
        /// <param name="addLoadCase">True: A linear static load case corresponding to the new load pattern is added.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void Add(string name,
            eLoadPatternType loadPatternType,
            double selfWeightMultiplier = 0,
            bool addLoadCase = true)
        {
            // TODO: Decide how to handle this: An error is returned if the Name item is already used for an existing load pattern. 
            _loadPattern.Add(Name, loadPatternType, selfWeightMultiplier, addLoadCase);
        }

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        public override void ChangeName(string newName)
        {
            _loadPattern?.ChangeName(Name, newName);
            Name = newName;
        }

        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        public override void Delete()
        {
            _loadPattern?.Delete(Name);
        }


        /// <summary>
        /// Gets the type of load.
        /// </summary>
        public void FillLoadType()
        {
            if (_loadPattern == null) return;
            Type = _loadPattern.GetLoadType(Name);
        }

        /// <summary>
        /// Assigns a load type to a load pattern.
        /// </summary>
        /// <param name="loadPatternType">This is one of the items in the eLoadPatternType enumeration.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadType(eLoadPatternType loadPatternType)
        {
            _loadPattern?.SetLoadType(Name, loadPatternType);
            Type = loadPatternType;
        }


        /// <summary>
        /// Gets the self weight multiplier.
        /// </summary>
        public void FillSelfWtMultiplier()
        {
            if (_loadPattern == null) return;
            SelfWeightMultiplier = _loadPattern.GetSelfWtMultiplier(Name);
        }

        /// <summary>
        /// Assigns a self weight multiplier to a load case.
        /// </summary>
        /// <param name="selfWeightMultiplier">The self weight multiplier for the specified load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSelfWtMultiplier(double selfWeightMultiplier)
        {
            _loadPattern?.SetSelfWtMultiplier(Name, selfWeightMultiplier);
            SelfWeightMultiplier = selfWeightMultiplier;
        }

        /// <summary>
        /// Gets the automatic code-based lateral load pattern.
        /// </summary>
        public void FillAutoCode()
        {
            string seismicCodeName = _loadPattern?.GetAutoSeismicCode(Name);
            if (!string.IsNullOrEmpty(seismicCodeName))
            {
                AutoLoadPattern = new AutoSeismic(seismicCodeName);
                return;
            }

            string windCodeName = _loadPattern?.GetAutoWindCode(Name);
            if (!string.IsNullOrEmpty(windCodeName))
            {
                AutoLoadPattern = new AutoSeismic(windCodeName);
                return;
            }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            string waveCodeName = _loadPattern?.GetAutoWaveCode(Name);
            if (!string.IsNullOrEmpty(waveCodeName))
            {
                AutoLoadPattern = new AutoWave(waveCodeName);
                return;
            }
#endif
            AutoLoadPattern = null;
        }
    }
}
