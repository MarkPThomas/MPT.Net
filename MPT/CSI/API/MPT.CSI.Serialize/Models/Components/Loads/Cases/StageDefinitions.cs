// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-06-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StageDefinitions.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MPT.CSI.Serialize.Models.Helpers.Loads.Definitions;

namespace MPT.CSI.Serialize.Models.Components.Loads.Cases
{
    /// <summary>
    /// A set of stage definitions with associated data.
    /// </summary>
    public class StageDefinitions :
        IEnumerable<StageDefinition>
    {
        #region Fields & Properties
        /// <summary>
        /// List of stage definition items.
        /// </summary>
        private List<StageDefinition> _items = new List<StageDefinition>();

        /// <summary>
        /// List of stage definition items
        /// </summary>
        /// <value>The items.</value>
        public ReadOnlyCollection<StageDefinition> Items => new ReadOnlyCollection<StageDefinition>(_items);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count => _items.Count;


        /// <summary>
        /// Gets the object at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>T.</returns>
        public StageDefinition this[int index] => _items[index];

        /// <summary>
        /// The name of the load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; protected set; }
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="StageDefinitions" /> class.
        /// </summary>
        /// <param name="caseName">Name of the case.</param>
        public StageDefinitions(string caseName)
        {
            CaseName = caseName;
        }
        #endregion

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<StageDefinition> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Sets the load case name associated with the stages.
        /// </summary>
        /// <param name="name">The load case name.</param>
        public void SetCaseName(string name)
        {
            CaseName = name;
            foreach (var item in _items)
            {
                item.SetCaseName(name);
            }
        }
        #endregion

        #region Stage Definitions
        /// <summary>
        /// This function initializes the stage definition data for the load case. <para />
        /// All previous stage definition data for the case is cleared when this function is called.
        /// </summary>
        /// <param name="definitions">The definitions.</param>
        public void SetStageDefinitions(List<StageDefinition> definitions)
        {
            _items = definitions;
        }


        /// <summary>
        /// Gets the stage by stage number.
        /// </summary>
        /// <param name="stageNumber">The stage number to return.</param>
        /// <returns>StageDefinition.</returns>
        public StageDefinition GetStage(int stageNumber)
        {
            return _items[stageNumber - 1];
        }

        /// <summary>
        /// Gets the stage by index.
        /// </summary>
        /// <param name="index">The index to return.</param>
        /// <returns>StageDefinition.</returns>
        public StageDefinition GetStageAt(int index)
        {
            return _items[index];
        }

        /// <summary>
        /// Adds the specified stage definition.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(StageDefinition item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Adds the specified stage definitions.
        /// Use this method if adding multiple operations to reduce calls to the API.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Add(List<StageDefinition> items)
        {
            _items.AddRange(items);
        }

        /// <summary>
        /// Removes the specified stage by index.
        /// </summary>
        /// <param name="index">The indexto remove.</param>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        /// <summary>
        /// Removes the specified stage by number.
        /// </summary>
        /// <param name="stageNumber">The stage number to remove.</param>
        public void RemoveStage(int stageNumber)
        {
            _items.RemoveAt(stageNumber - 1);
        }

        /// <summary>
        /// Removes all stage definitions.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }
        #endregion

        #region Stage Operations
        /// <summary>
        /// Returns stage operations for the specified stage in the load case.
        /// </summary>
        public void FillStageOperations()
        {
            int stage = 0;
            foreach (var item in _items)
            {
                stage++;
                item.FillStageOperations(CaseName, stage);
            }
        }


        /// <summary>
        /// Adds the specified stage operation.
        /// </summary>
        /// <param name="stageNumber">The stage number to add the operation to.</param>
        /// <param name="item">The item.</param>
        public void AddStageOperation(int stageNumber, StageOperation item)
        {
            AddStageOperationAt(stageNumber - 1, item);
        }

        /// <summary>
        /// Adds the specified stage operation.
        /// </summary>
        /// <param name="stageIndex">The stage index to add the operation to.</param>
        /// <param name="item">The item.</param>
        public void AddStageOperationAt(int stageIndex, StageOperation item)
        {
            _items[stageIndex].AddStageOperation(item);
        }


        /// <summary>
        /// Adds the specified stage operations.
        /// Use this method if adding multiple operations to reduce calls to the API.
        /// </summary>
        /// <param name="stageNumber">The stage number to add the operations to.</param>
        /// <param name="items">The items.</param>
        public void AddStageOperations(int stageNumber, List<StageOperation> items)
        {
            AddStageOperationsAt(stageNumber - 1, items);
        }

        /// <summary>
        /// Adds the specified stage operations.
        /// Use this method if adding multiple operations to reduce calls to the API.
        /// </summary>
        /// <param name="stageIndex">The stage index to add the operations to.</param>
        /// <param name="items">The items.</param>
        public void AddStageOperationsAt(int stageIndex, List<StageOperation> items)
        {
            _items[stageIndex].AddStageOperations(items);
        }


        /// <summary>
        /// Removes the specified stage operation by stage number.
        /// </summary>
        /// <param name="stageNumber">The stage number to remove the operation from.</param>
        /// <param name="operationIndex">The operation index to remove.</param>
        public void RemoveStageOperation(int stageNumber, int operationIndex)
        {
            RemoveStageOperationAt(stageNumber - 1, operationIndex);
        }

        /// <summary>
        /// Removes the specified stage operation by index.
        /// </summary>
        /// <param name="stageIndex">The stage index to remove the operation from.</param>
        /// <param name="operationIndex">The operation index to remove.</param>
        public void RemoveStageOperationAt(int stageIndex, int operationIndex)
        {
            _items[stageIndex].RemoveStageOperation(operationIndex);
            updateStageNumbers();
        }


        /// <summary>
        /// Removes all operations from the stage.
        /// </summary>
        /// <param name="stageNumber">The stage number to clear the operations from.</param>
        public void ClearStageOperations(int stageNumber)
        {
            ClearStageOperationsAt(stageNumber - 1);
        }

        /// <summary>
        /// Removes all operations from the stage.
        /// </summary>
        /// <param name="stageIndex">The stage index to clear the operations from.</param>
        public void ClearStageOperationsAt(int stageIndex)
        {
            _items[stageIndex].ClearStageOperations();
            updateStageNumbers();
        }
        #endregion
        
        #region Protected       
        /// <summary>
        /// Updates the stage numbers.
        /// </summary>
        protected void updateStageNumbers()
        {
            int stage = 0;
            foreach (var item in _items)
            {
                stage++;
                item.SetStageNumber(stage);
            }
        }
        #endregion
    }
}
