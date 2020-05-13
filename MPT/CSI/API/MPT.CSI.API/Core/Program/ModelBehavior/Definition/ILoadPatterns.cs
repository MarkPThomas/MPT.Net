// ***********************************************************************
// Assembly         : MPT.CSI.API
// Author           : Mark Thomas
// Created          : 10-07-2017
//
// Last Modified By : Mark Thomas
// Last Modified On : 10-07-2017
// ***********************************************************************
// <copyright file="ILoadPatterns.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using MPT.CSI.API.Core.Program.ModelBehavior.Definition.LoadPattern;

namespace MPT.CSI.API.Core.Program.ModelBehavior.Definition
{
    /// <summary>
    /// Implements load patterns in the application.
    /// </summary>
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IChangeableName" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.ICountable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IDeletable" />
    /// <seealso cref="MPT.CSI.API.Core.Program.ModelBehavior.IListableNames" />
    public interface ILoadPatterns: IChangeableName, ICountable, IDeletable, IListableNames
    {
        #region Properties

        /// <summary>
        /// Represents an auto seismic load pattern in the application.
        /// </summary>
        /// <value>The automatic seismic pattern.</value>
        AutoSeismic AutoSeismicPattern { get; }

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Represents an auto wind load pattern in the application.
        /// </summary>
        /// <value>The automatic wind pattern.</value>
        AutoWind AutoWindPattern { get; }

        /// <summary>
        /// Represents an auto wave load pattern in the application.
        /// </summary>
        /// <value>The automatic wave pattern.</value>
        AutoWave AutoWavePattern { get; }
#endif
        #endregion

        #region Methods: Public
        /// <summary>
        /// Adds a new load pattern.
        /// An error is returned if the <paramref name="name" /> item is already used for an existing load pattern.
        /// </summary>
        /// <param name="name">Name for the new load pattern.</param>
        /// <param name="loadPatternType">Load pattern type.</param>
        /// <param name="selfWeightMultiplier">Self weight multiplier for the new load pattern.</param>
        /// <param name="addLoadCase">True: A linear static load case corresponding to the new load pattern is added.</param>
        void Add(string name,
            eLoadPatternType loadPatternType,
            double selfWeightMultiplier = 0,
            bool addLoadCase = true);

        /// <summary>
        /// Returns the load type for a specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing load pattern.</param>
        eLoadPatternType GetLoadType(string name);

        /// <summary>
        /// Assigns a load type to a load pattern.
        /// </summary>
        /// <param name="name">The name of an existing load pattern.</param>
        /// <param name="loadPatternType">This is one of the items in the eLoadPatternType enumeration.</param>
        void SetLoadType(string name,
            eLoadPatternType loadPatternType);

        // ===

        /// <summary>
        /// Returns the self weight multiplier for a specified load pattern.
        /// </summary>
        /// <param name="name">The name of an existing load pattern.</param>
        double GetSelfWtMultiplier(string name);

        /// <summary>
        /// Assigns a self weight multiplier to a load case.
        /// </summary>
        /// <param name="name">The name of an existing load pattern.</param>
        /// <param name="selfWeightMultiplier">The self weight multiplier for the specified load pattern.</param>
        void SetSelfWtMultiplier(string name,
            double selfWeightMultiplier);

        // ===

        /// <summary>
        /// Retrieves the code name used for auto seismic parameters in Quake-type load patterns.
        /// This is either blank or the name code used for the auto seismic parameters.
        /// Blank means no auto seismic load is specified for the Quake-type load pattern.
        /// </summary>
        /// <param name="name">The name of an existing Quake-type load pattern.</param>
        string GetAutoSeismicCode(string name);

        /// <summary>
        /// Retrieves the code name used for auto wind parameters in wind-type load patterns.
        /// This is either blank or the name code used for the auto wind parameters.
        /// Blank means no auto wind load is specified for the wind-type load pattern.
        /// </summary>
        /// <param name="name">The name of an existing wind-type load pattern.</param>
        string GetAutoWindCode(string name);

#if !BUILD_ETABS2015 && !BUILD_ETABS2016 && !BUILD_ETABS2017
        /// <summary>
        /// Retrieves the code name used for auto wave parameters in wave-type load patterns.
        /// This is either blank or the name code used for the auto wave parameters.
        /// Blank means no auto wave load is specified for the wave-type load pattern.
        /// </summary>
        /// <param name="name">The name of an existing wave-type load pattern.</param>
        string GetAutoWaveCode(string name);
#endif

        #endregion
    }
}