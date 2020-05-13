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
using MPT.CSI.API.Core.Program.ModelBehavior.Definition;
using MPT.CSI.API.Core.Support;
using MPT.CSI.OOAPI.Core.Support;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;
using ApiLoadPattern = MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPatterns;

namespace MPT.CSI.OOAPI.Core.Program.Model.Loads
{
    /// <summary>
    /// Represents a load pattern.
    /// </summary>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOOAPiName" />
    public class LoadPattern : CSiOOAPiName
    {
        #region Fields & Properties
        /// <summary>
        /// The load pattern API object.
        /// </summary>
        /// <value>The load pattern.</value>
        protected ApiLoadPattern _apiLoadPattern => getApiLoadPattern(_apiApp);

        private eLoadPatternType _type;
        /// <summary>
        /// Load Pattern type.
        /// </summary>
        /// <value>The type.</value>
        public eLoadPatternType Type
        {
            get
            {
                if (_type == 0)
                {
                    FillLoadType();
                }

                return _type;
            }
        }

        private double? _selfWeightMultiplier;
        /// <summary>
        /// Gets or sets the self weight multiplier.
        /// </summary>
        /// <value>The self weight multiplier.</value>
        public double SelfWeightMultiplier
        {
            get
            {
                if (_selfWeightMultiplier == null)
                {
                    FillSelfWtMultiplier();
                }

                return _selfWeightMultiplier ?? 0;
            }
        }

        private AutoLoadPattern _autoLoadPattern;
        /// <summary>
        /// Gets or sets the automatic load pattern.
        /// </summary>
        /// <value>The automatic load pattern.</value>
        public AutoLoadPattern AutoLoadPattern
        {
            get
            {
                if (_autoLoadPattern == null)
                {
                    FillAutoCode();
                }

                return _autoLoadPattern;
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadPattern" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="name">The name.</param>
        protected LoadPattern(ApiCSiApplication app, string name) : base(app, name) { }

        /// <summary>
        /// Fills the data from the application using the API.
        /// </summary>
        public override void FillData()
        {
            // Does nothing.
        }

        /// <summary>
        /// Returns a new load pattern class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Unique load pattern name.</param>
        /// <returns>Steel.</returns>
        internal static LoadPattern Factory(ApiCSiApplication app, string uniqueName)
        {
            LoadPattern loadPattern = new LoadPattern(app, uniqueName);
            loadPattern.FillData();

            return loadPattern;
        }
        #endregion

        #region Fill/Set
        /// <summary>
        /// Gets the type of load.
        /// </summary>
        public void FillLoadType()
        {
            if (_apiLoadPattern == null) return;
            _type = _apiLoadPattern.GetLoadType(Name);
        }

        /// <summary>
        /// Assigns a load type to a load pattern.
        /// </summary>
        /// <param name="loadPatternType">This is one of the items in the eLoadPatternType enumeration.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetLoadType(eLoadPatternType loadPatternType)
        {
            _apiLoadPattern?.SetLoadType(Name, loadPatternType);
            _type = loadPatternType;
        }


        /// <summary>
        /// Gets the self weight multiplier.
        /// </summary>
        public void FillSelfWtMultiplier()
        {
            if (_apiLoadPattern == null) return;
            _selfWeightMultiplier = _apiLoadPattern.GetSelfWtMultiplier(Name);
        }

        /// <summary>
        /// Assigns a self weight multiplier to a load case.
        /// </summary>
        /// <param name="selfWeightMultiplier">The self weight multiplier for the specified load pattern.</param>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        public void SetSelfWtMultiplier(double selfWeightMultiplier)
        {
            _apiLoadPattern?.SetSelfWtMultiplier(Name, selfWeightMultiplier);
            _selfWeightMultiplier = selfWeightMultiplier;
        }

        /// <summary>
        /// Gets the automatic code-based lateral load pattern.
        /// </summary>
        public void FillAutoCode()
        {
            string seismicCodeName = _apiLoadPattern?.GetAutoSeismicCode(Name);
            if (!string.IsNullOrEmpty(seismicCodeName))
            {
                _autoLoadPattern = new AutoSeismic(seismicCodeName);
                return;
            }

            string windCodeName = _apiLoadPattern?.GetAutoWindCode(Name);
            if (!string.IsNullOrEmpty(windCodeName))
            {
                _autoLoadPattern = new AutoSeismic(windCodeName);
                return;
            }
#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
            string waveCodeName = _apiLoadPattern?.GetAutoWaveCode(Name);
            if (!string.IsNullOrEmpty(waveCodeName))
            {
                _autoLoadPattern = new AutoWave(waveCodeName);
                return;
            }
#endif
            _autoLoadPattern = null;
        }
        #endregion

        #region CRUD
        /// <summary>
        /// Adds a new load pattern.
        /// An error is returned if the <paramref name="uniqueName" /> item is already used for an existing load pattern.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="uniqueName">Name for the new load pattern.</param>
        /// <param name="loadPatternType">Load pattern type.</param>
        /// <param name="selfWeightMultiplier">Self weight multiplier for the new load pattern.</param>
        /// <param name="addLoadCase">True: A linear static load case corresponding to the new load pattern is added.</param>
        /// <returns>LoadPattern.</returns>
        /// <exception cref="CSiException"><see cref="CSiApiBase.API_DEFAULT_ERROR_CODE" /></exception>
        internal static LoadPattern Add(ApiCSiApplication app, 
            string uniqueName,
            eLoadPatternType loadPatternType,
            double selfWeightMultiplier = 0,
            bool addLoadCase = true)
        {
            ApiLoadPattern apiLoadPatterns = getApiLoadPattern(app);
            apiLoadPatterns.Add(uniqueName, loadPatternType, selfWeightMultiplier, addLoadCase);
            return Factory(app, uniqueName);
        }

        /// <summary>
        /// This function changes the name of an existing property.
        /// </summary>
        /// <param name="newName">The new name for the property.</param>
        public override void ChangeName(string newName)
        {
            _apiLoadPattern?.ChangeName(Name, newName);
            Name = newName;
        }

        /// <summary>
        /// The function deletes a specified property.
        /// It returns an error if the specified property can not be deleted; for example, if it is currently used by a staged construction load case.
        /// </summary>
        internal override void Delete()
        {
            _apiLoadPattern?.Delete(Name);
        }
        #endregion

        #region Static
        /// <summary>
        /// Gets the name list.
        /// </summary>
        /// <param name="loadpattern">The loadpattern.</param>
        /// <returns>List&lt;System.String&gt;.</returns>
        internal static List<string> GetNameList(ApiLoadPattern loadpattern)
        {
            return new List<string>(loadpattern.GetNameList());
        }
        #endregion

        #region Protected



        #endregion

        #region API Functions



        #endregion
    }
}
