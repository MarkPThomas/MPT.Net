// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="CompositeBeamDesignResults.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers;
using MPT.CSI.Serialize.Models.Helpers.Design;

namespace MPT.CSI.Serialize.Models.Components.Design
{
    /// <summary>
    /// Class CompositeBeamDesignResults.
    /// </summary>
    /// <seealso cref="DesignResults" />
    public abstract class CompositeBeamDesignResults : DesignResults
    {
        #region Fields & Properties

        /// <summary>
        /// The composite beam results summary
        /// </summary>
        List<CompositeBeamResultsSummary> _compositeBeamResultsSummary;

        /// <summary>
        /// Gets the composite beam results summary.
        /// </summary>
        /// <value>The composite beam results summary.</value>
        public List<CompositeBeamResultsSummary> CompositeBeamResultsSummary =>
            _compositeBeamResultsSummary ?? (_compositeBeamResultsSummary = GetSummaryResults(Name));
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeBeamDesignResults"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected CompositeBeamDesignResults(string name)
        {
            Name = name;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>List&lt;CompositeBeamResultsSummary&gt;.</returns>
        public abstract List<CompositeBeamResultsSummary> GetSummaryResults(
            string name,
            eItemType itemType = eItemType.Object);

        #endregion
    }
}
