// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 11-18-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-21-2018
// ***********************************************************************
// <copyright file="Modal.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using MPT.CSI.Serialize.Models.Components.Analysis;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Base class for modal load cases.
    /// </summary>
    /// <seealso cref="LoadCase" />
    public abstract class Modal : LoadCase, IInitialCase
    {
        #region Fields & Properties
        /// <summary>
        /// The load cases
        /// </summary>
        private readonly LoadCases _loadCases;

        /// <summary>
        /// The initial case
        /// </summary>
        private InitialCaseHelper _initialCase;
        /// <summary>
        /// The initial load case.
        /// </summary>
        /// <value>The initial case.</value>
        public InitialCaseHelper InitialCase
        {
            get
            {
                if (_initialCase != null) return _initialCase;
                _initialCase = InitialCaseHelper.Factory(_loadCases, Name);

                return _initialCase;
            }
        }

        /// <summary>
        /// The maximum number of modes requested.
        /// SAP2000 only.
        /// </summary>
        /// <value>The maximum number of modes.</value>
        public int MaxNumberOfModes { get; internal set; }

        /// <summary>
        /// The minimum number of modes requested.
        /// SAP2000 only.
        /// </summary>
        /// <value>The minimum number of modes.</value>
        public int MinNumberOfModes { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="T:MPT.CSI.Serialize.Models.Components.Loads.Cases.Modal" /> class.
        /// </summary>
        /// <param name="analyzer">The analyzer.</param>
        /// <param name="name">The name.</param>
        /// <inheritdoc />
        protected Modal(Analyzer analyzer, string name) : base(analyzer, name)
        {
        }
        #endregion
    }
}
