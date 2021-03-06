﻿// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-20-2018
// ***********************************************************************
// <copyright file="LoadsTimeHistory.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MPT.CSI.Serialize.Models.Components.ProjectSettings;

namespace MPT.CSI.Serialize.Models.Helpers.Loads.Definitions
{
    /// <summary>
    /// Class LoadsTimeHistory.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEnumerable{LoadTimeHistory}" />
    public class LoadsTimeHistory : 
        ModelProperty,
        IEnumerable<LoadTimeHistory>
    {
        /// <summary>
        /// List of load items as pattern/acceleration name, scale factor, and load type.
        /// </summary>
        private readonly List<LoadTimeHistory> _items = new List<LoadTimeHistory>();
        /// <summary>
        /// List of load items as pattern/acceleration name, scale factor, and load type.
        /// </summary>
        /// <value>The items.</value>
        public ReadOnlyCollection<LoadTimeHistory> Items => new ReadOnlyCollection<LoadTimeHistory>(_items);

        #region Implementation of IEnumerable
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<LoadTimeHistory> GetEnumerator()
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


        /// <summary>
        /// Adds the load data for the analysis case.
        /// This method should be used when adding multiple loads to reduce API calls to the application.
        /// </summary>
        /// <param name="loads">The loads.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool AddUnique(LoadsTimeHistory loads)
        {
            bool loadsAdded = false;
            foreach (var load in loads)
            {
                if (_items.Contains(load)) continue;
                _items.Add(load);
                loadsAdded = true;
            }

            return loadsAdded;
        }

        /// <summary>
        /// Removes the load data for the analysis case.
        /// This method should be used when removing multiple loads to reduce API calls to the application.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public void Remove(LoadsTimeHistory loads)
        {
            foreach (var loadPatternTuple in loads)
            {
                _items.Remove(loadPatternTuple);
            }
        }

        /// <summary>
        /// Removes the load data for the analysis case by name. Multiple instances will all be removed.
        /// This method should be used when removing multiple loads to reduce API calls to the application.
        /// </summary>
        /// <param name="loads">The loads.</param>
        public void RemoveByName(string[] loads)
        {
            foreach (var load in loads)
            {
                Remove(load);
            }
        }


        /// <summary>
        /// Adds the specified load.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(LoadTimeHistory item)
        {
            _items.Add(item);
        }

        /// <summary>
        /// Removes the specified name.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Remove(LoadTimeHistory item)
        {
            Remove(item.Load.Name, item.ScaleFactor);
        }

        /// <summary>
        /// Removes the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="scaleFactor">The scale factor.
        /// If not specified, then all loads of a specified name will be removed.
        /// Otherwise, only the name with the associated factor will be removed.</param>
        public void Remove(string name, double scaleFactor = -1)
        {
            List<int> removeIndices = new List<int>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Load.Name != name) continue;
                if (scaleFactor >= 0 && Math.Abs(_items[i].ScaleFactor - scaleFactor) > Constants.Tolerance) continue;
                removeIndices.Add(i);
                break;
            }

            foreach (int index in removeIndices)
            {
                _items.RemoveAt(index);
            }
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.</returns>
        public bool Contains(LoadTimeHistory item)
        {
            return _items.Any(lt => lt.Load == item.Load && Math.Abs(lt.ScaleFactor - item.ScaleFactor) < Constants.Tolerance);
        }

        /// <summary>
        /// Gets the name, scale factor, and load type of the specified load.
        /// If more than one match is found, all matches will be returned.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="scaleFactor">The scale factor.</param>
        /// <returns>Tuple&lt;System.String, System.Double, eLoadType&gt;[].</returns>
        public LoadTimeHistory[] GetItem(string name, double scaleFactor = -1)
        {
            List<int> getIndices = new List<int>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Load.Name != name) continue;
                if (scaleFactor >= 0 && Math.Abs(_items[i].ScaleFactor - scaleFactor) > Constants.Tolerance) continue;
                getIndices.Add(i);
                break;
            }

            LoadTimeHistory[] items = new LoadTimeHistory[getIndices.Count];
            for (int i = 0; i < getIndices.Count; i++)
            {
                items[i] = _items[i];
            }

            return items;
        }

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
