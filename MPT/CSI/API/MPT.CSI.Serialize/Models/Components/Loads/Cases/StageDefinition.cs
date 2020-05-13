// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StageDefinition.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections.Generic;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// Data related to stage construction stage definition.
    /// </summary>
    public class StageDefinition 
    {
        #region Fields & Properties        
        /// <summary>
        /// The load case name.
        /// </summary>
        /// <value>The name.</value>
        internal string Name { get; private set; }

        /// <summary>
        /// Gets or sets the stage.
        /// </summary>
        /// <value>The stage.</value>
        internal int Stage { get; private set; }

        /// <summary>
        /// The duration in days for the stage.
        /// </summary>
        /// <value>The duration.</value>
        public double Duration { get; set; }

        /// <summary>
        /// True: The analysis output is to be saved for the stage.
        /// </summary>
        /// <value><c>true</c> if [output is to be saved]; otherwise, <c>false</c>.</value>
        public bool OutputIsToBeSaved { get; set; }

        /// <summary>
        /// A user-specified output name for each stage.
        /// </summary>
        /// <value>The name output.</value>
        public string NameOutput { get; set; }

        /// <summary>
        /// A comment for each stage. <para />
        /// The comment may be a blank string.
        /// </summary>
        /// <value>The comment.</value>
        public string Comment { get; set; }

        /// <summary>
        /// The stage operations associated with the stage definition.
        /// </summary>
        /// <value>The stage operations.</value>
        public StageOperations StageOperations { get; internal set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Returns stage operations for the specified stage in the load case.
        /// </summary>
        /// <param name="name">The load case name.</param>
        /// <param name="stage">The stage.</param>
        public void FillStageOperations(string name, int stage)
        {
            Name = name;
            Stage = stage;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the stage number.
        /// </summary>
        /// <param name="stageNumber">The stage number.</param>
        public void SetStageNumber(int stageNumber)
        {
            Stage = stageNumber;
            StageOperations.StageNumber = stageNumber;
        }

        /// <summary>
        /// Sets the load case name associated with the stage.
        /// </summary>
        /// <param name="name">The load case name.</param>
        public void SetCaseName(string name)
        {
            Name = name;
            StageOperations.CaseName = name;
        }
        #endregion

        #region List
        /// <summary>
        /// Adds the specified stage operation.
        /// </summary>
        /// <param name="item">The item.</param>
        public void AddStageOperation(StageOperation item)
        {
            StageOperations?.Add(item);
        }

        /// <summary>
        /// Adds the specified stage operations.
        /// Use this method if adding multiple operations to reduce calls to the API.
        /// </summary>
        /// <param name="items">The items.</param>
        public void AddStageOperations(List<StageOperation> items)
        {
            StageOperations?.Add(items);
        }

        /// <summary>
        /// Removes the specified stage operation by index.
        /// </summary>
        /// <param name="index">The index to remove.</param>
        public void RemoveStageOperation(int index)
        {
            StageOperations?.RemoveAt(index);
        }

        /// <summary>
        /// Removes all operations from the stage.
        /// </summary>
        public void ClearStageOperations()
        {
            StageOperations?.Clear();
        }
        #endregion
    }
}
