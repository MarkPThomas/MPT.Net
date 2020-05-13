// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="SteelDesignResults.cs" company="">
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
    /// Class SteelDesignResults.
    /// </summary>
    /// <seealso cref="DesignResults" />
    public abstract class SteelDesignResults : DesignResults
    {
        #region Fields & Properties
        /// <summary>
        /// The steel results summary
        /// </summary>
        private List<SteelResultsSummary> _steelResultsSummary;
        /// <summary>
        /// Gets the steel results summary.
        /// </summary>
        /// <value>The steel results summary.</value>
        public List<SteelResultsSummary> SteelResultsSummary => _steelResultsSummary ?? 
                                                                (_steelResultsSummary = GetSummaryResults(Name));
       
        /// <summary>
        /// The steel results summary expanded
        /// </summary>
        private List<SteelResultsSummaryExpanded> _steelResultsSummaryExpanded;
        /// <summary>
        /// Gets the steel results summary expanded.
        /// </summary>
        /// <value>The steel results summary expanded.</value>
        public List<SteelResultsSummaryExpanded> SteelResultsSummaryExpanded => 
             _steelResultsSummaryExpanded ??
            (_steelResultsSummaryExpanded = GetSummaryResultsExpanded(Name));
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="SteelDesignResults"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected SteelDesignResults(string name)
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
        /// <returns>List&lt;SteelResultsSummary&gt;.</returns>
        public abstract List<SteelResultsSummary> GetSummaryResults(
            string name,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// Retrieves summary results for frame design.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>List&lt;SteelResultsSummaryExpanded&gt;.</returns>
        public abstract List<SteelResultsSummaryExpanded> GetSummaryResultsExpanded(
            string name, 
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// Returns the design results from steel design output database tables.
        /// Notes that the summary table of all design codes is not included in this function.
        /// </summary>
        /// <param name="name">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="table">Table ID of the steel design output database Tables.
        /// The table names are input as the representative table numbers and are code-based.
        /// Please see the appendix at the bottom of the steel class.</param>
        /// <param name="field">Field name with TEXT output data type in the specified steel design result database Tables.
        /// The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
        public abstract List<SteelResultsDetailedText> GetDetailedResultsText(
            string name,
            int table,
            string field,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// Returns the design results from steel design output database tables.
        /// Notes that the summary table of all design codes is not included in this function.
        /// </summary>
        /// <param name="itemName">Name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="table">Table ID of the steel design output database Tables.
        /// The table names are input as the representative table numbers and are code-based.
        /// Please see the appendix at the bottom of the steel class.</param>
        /// <param name="field">Field name with Numerical output data type in the specified steel design result database Tables.
        /// The Field Names need to be the exactly same as the names in the specified steel design output database tables except the case is insensitive.</param>
        public abstract List<SteelResultsDetailedNumeric> GetDetailedResultsNumerical(
            string itemName,
            int table,
            string field,
            eItemType itemType = eItemType.Object);

        #endregion
    }
}
