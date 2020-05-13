// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-05-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-06-2018
// ***********************************************************************
// <copyright file="StageOperations.cs" company="">
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
    /// Stage data associated with each stage definition.
    /// </summary>
    public class StageOperations : IEnumerable<StageOperation>
    {
        #region Fields & Properties
        /// <summary>
        /// List of stage data items.
        /// </summary>
        private readonly List<StageOperation> _items = new List<StageOperation>();
        /// <summary>
        /// List of stage data items
        /// </summary>
        /// <value>The items.</value>
        public ReadOnlyCollection<StageOperation> Items => new ReadOnlyCollection<StageOperation>(_items);

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
        public StageOperation this[int index] => _items[index];

        /// <summary>
        /// The name of the load case.
        /// </summary>
        /// <value>The name of the case.</value>
        public string CaseName { get; internal set; }

        /// <summary>
        /// The stage number associated with the list of operations.
        /// </summary>
        /// <value>The stage number.</value>
        public int StageNumber { get; internal set; }
        #endregion

        #region Initialization
        #endregion

        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<StageOperation> GetEnumerator()
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
        /// Gets the stage operation by index.
        /// </summary>
        /// <param name="index">The index to return.</param>
        /// <returns>StageOperation.</returns>
        public StageOperation GetOperationAt(int index)
        {
            return _items[index];
        }

        /// <summary>
        /// Adds the specified stage operation.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(StageOperation item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Adds the specified stage operations.
        /// Use this method if adding multiple operations to reduce calls to the API.
        /// </summary>
        /// <param name="items">The items.</param>
        public void Add(List<StageOperation> items)
        {
            _items.AddRange(items);
        }

        /// <summary>
        /// Removes the specified stage operation by index.
        /// </summary>
        /// <param name="index">The index to remove.</param>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        /// <summary>
        /// Removes all operations from the stage.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }
        #endregion
    }
}
