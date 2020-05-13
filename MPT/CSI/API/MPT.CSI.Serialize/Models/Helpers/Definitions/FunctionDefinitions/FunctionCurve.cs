// ***********************************************************************
// Assembly         : MPT.CSI.Serialize
// Author           : Mark Thomas
// Created          : 02-01-2019
//
// Last Modified By : Mark Thomas
// Last Modified On : 02-01-2019
// ***********************************************************************
// <copyright file="FunctionCurve.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;

namespace MPT.CSI.Serialize.Models.Helpers.Definitions.FunctionDefinitions
{
    /// <summary>
    /// Class FunctionCurve.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.Serialize.Models.Helpers.ModelProperty" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public class FunctionCurve<T> :
        ModelProperty,
        IEnumerable<T> where T : FunctionPoint
    {
        #region Fields & Properties
        /// <summary>
        /// The function points.
        /// </summary>
        protected List<T> _items = new List<T>();

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count => _items.Count;

        /// <summary>
        /// Gets the <see cref="T" /> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>T.</returns>
        public T this[int index] => _items[index];
        #endregion

        #region Methods
        /// <summary>
        /// Adds the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        public void Add(T point)
        {
            if (point == null) return;

            int index = 0;
            while (_items[index].X < point.X &&
                   index < _items.Count)
            {
                index++;
            }
            insert(point, index);
        }

        /// <summary>
        /// Inserts the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="index">The index.</param>
        protected virtual void insert(T point, int index)
        {
            if (!isValidNewPoint(point)) return;

            validatePointContext(point, index);
            _items.Insert(index, point);
        }


        /// <summary>
        /// Removes the specified point.
        /// </summary>
        /// <param name="point">The point.</param>
        public virtual void Remove(T point)
        {
            int index = _items.IndexOf(point);
            Remove(index);
        }

        /// <summary>
        /// Removes the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        public virtual void Remove(int index)
        {
            T pointRemoved = _items[index];
            
            _items.Remove(pointRemoved);
        }

        /// <summary>
        /// Determines whether [is valid new point] [the specified point].
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns><c>true</c> if [is valid new point] [the specified point]; otherwise, <c>false</c>.</returns>
        private bool isValidNewPoint(T point)
        {
            return (point != null);
        }

        #endregion

        #region Validation
        /// <summary>
        /// Validates the point context.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="index">The index.</param>
        /// <exception cref="ArgumentException">The Xs must increase monotonically.</exception>
        protected virtual void validatePointContext(T point, int index)
        {
            if (!xCoordinatessChangeMonotonically(point, index))
            {
                throw new ArgumentException("The Xs must increase monotonically.");
            }
        }

        /// <summary>
        /// Xs increase monotonically.
        /// The Xs must increase monotonically.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="index">The index.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool xCoordinatessChangeMonotonically(T point, int index)
        {
            if (point.X < 0)
            {
                return _items[index + 1].X > point.X;
            }
            return _items[index - 1].X < point.X;
        }
        #endregion


        #region IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
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

        #region List Query
        /// <summary>
        /// Determines whether [contains] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [contains] [the specified value]; otherwise, <c>false</c>.</returns>
        public bool Contains(object value)
        {
            return (value is FunctionPoint variable) && Contains(variable);
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.</returns>
        public bool Contains(T item)
        {
            return _items.Contains(item);
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public int IndexOf(object value)
        {
            return (value is FunctionPoint variable) ? IndexOf(variable) : -1;
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.Int32.</returns>
        public int IndexOf(T item)
        {
            return _items.IndexOf(item);
        }
        #endregion

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return this.Copy();
        }
    }
}
