// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-01-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-25-2018
// ***********************************************************************
// <copyright file="ConcreteDesignResults.cs" company="">
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
    /// Class ConcreteDesignResults.
    /// </summary>
    /// <seealso cref="DesignResults" />
    public abstract class ConcreteDesignResults : DesignResults
    {
        
        #region Fields & Properties

        /// <summary>
        /// The concrete results summary beam
        /// </summary>
        private List<ConcreteResultsSummaryBeam> _concreteResultsSummaryBeam;

        /// <summary>
        /// Gets the concrete results summary beam.
        /// </summary>
        /// <value>The concrete results summary beam.</value>
        public List<ConcreteResultsSummaryBeam> ConcreteResultsSummaryBeam =>
            _concreteResultsSummaryBeam ?? (_concreteResultsSummaryBeam = GetSummaryResultsBeam(Name));


        /// <summary>
        /// The concrete results summary column
        /// </summary>
        private List<ConcreteResultsSummaryColumn> _concreteResultsSummaryColumn;
        /// <summary>
        /// Gets the concrete results summary column.
        /// </summary>
        /// <value>The concrete results summary column.</value>
        public List<ConcreteResultsSummaryColumn> ConcreteResultsSummaryColumn =>
            _concreteResultsSummaryColumn ?? (_concreteResultsSummaryColumn = GetSummaryResultsColumn(Name));

        /// <summary>
        /// The concrete results summary joint
        /// </summary>
        private List<ConcreteResultsSummaryJoint> _concreteResultsSummaryJoint;
        /// <summary>
        /// Gets the concrete results summary joint.
        /// </summary>
        /// <value>The concrete results summary joint.</value>
        public List<ConcreteResultsSummaryJoint> ConcreteResultsSummaryJoint =>
            _concreteResultsSummaryJoint ?? (_concreteResultsSummaryJoint = GetSummaryResultsJoint(Name));
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteDesignResults"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected ConcreteDesignResults(string name) 
        {
            Name = name;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Retrieves summary results for concrete design of beams.
        /// Torsion results are not included for all codes.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>List&lt;ConcreteResultsSummaryBeam&gt;.</returns>
        public abstract List<ConcreteResultsSummaryBeam> GetSummaryResultsBeam(
            string name,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// Retrieves summary results for concrete design of columns.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>List&lt;ConcreteResultsSummaryColumn&gt;.</returns>
        public abstract List<ConcreteResultsSummaryColumn> GetSummaryResultsColumn(
            string name,
            eItemType itemType = eItemType.Object);

        /// <summary>
        /// Retrieves summary results for concrete design of joints.
        /// </summary>
        /// <param name="name">The name of an existing frame object or group, depending on the value of the ItemType item.</param>
        /// <param name="itemType">Type of the item.</param>
        /// <returns>List&lt;ConcreteResultsSummaryJoint&gt;.</returns>
        public abstract List<ConcreteResultsSummaryJoint> GetSummaryResultsJoint(
            string name,
            eItemType itemType = eItemType.Object);

        #endregion
    }
}
