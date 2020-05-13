// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-11-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="ObjectLists.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MPT.CSI.OOAPI.Core.Program.Model.Definitions;
using ApiCSiApplication = MPT.CSI.API.Core.Program.CSiApplication;

namespace MPT.CSI.OOAPI.Core.Support
{
    /// <summary>
    /// Class ObjectLists.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="MPT.CSI.OOAPI.Core.Support.CSiOoApiBaseBase" />
    /// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
    public abstract class ObjectLists<T> : CSiOoApiBaseBase,
        IEnumerable<T> where T: IUniqueName
    {
        #region Fields & Properties
        /// <summary>
        /// The items
        /// </summary>
        protected List<T> _items = new List<T>();

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>The items.</value>
        public IReadOnlyList<T> Items => new ReadOnlyCollection<T>(_items);

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count => _items.Count;

        /// <summary>
        /// Gets the object with the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>T.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public T this[string name]
        {
            get
            {
                int index = IndexOf(name);
                if (index < 0) throw new ArgumentOutOfRangeException();
                return this[index];
            }
        }

        /// <summary>
        /// Gets the object at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>T.</returns>
        public T this[int index] => _items[index];
        #endregion

        #region Initialization
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectLists{T}" /> class.
        /// </summary>
        /// <param name="app">The application.</param>
        protected ObjectLists(ApiCSiApplication app) : base(app)
        {
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
            return (value is T variable) && Contains(variable);
        }

        /// <summary>
        /// Determines whether [contains] [the specified item].
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.</returns>
        public bool Contains(T item)
        {
            return Contains(item.Name);
        }

        /// <summary>
        /// Determines whether [contains] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if [contains] [the specified name]; otherwise, <c>false</c>.</returns>
        public bool Contains(string name)
        {
            return _items.Any(p => p.Name == name);
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public int IndexOf(object value)
        {
            return (value is T variable) ? IndexOf(variable) : -1;
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.Int32.</returns>
        public int IndexOf(T item)
        {
            return IndexOf(item.Name);
        }

        /// <summary>
        /// Indexes the of.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>System.Int32.</returns>
        public int IndexOf(string name)
        {
            return _items.IndexOf(_items.FirstOrDefault(p => p.Name != name));
        }
        #endregion

        #region Add/Remove
        /// <summary>
        /// Adds the specified application.
        /// </summary>
        /// <param name="uniqueName">Name of the unique.</param>
        /// <param name="factory">The factory.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool add(string uniqueName, Func<ApiCSiApplication, string, T> factory)
        {
            if (Contains(uniqueName)) return false;
            _items.Add(factory(_apiApp, uniqueName));
            return true;
        }


        /// <summary>
        /// Deletes the object of the specified name from the list and the application.
        /// </summary>
        /// <param name="name">Name of the object to delete.</param>
        public void Delete(string name)
        {
            if (!Contains(name)) return;
            var item = this[name];

            // TODO: Determine better way to generically delete items using an internal method. Internal interfaces do not work.
            // Handles all objects, definitions, loads
            var itemToDelete = item as CSiOOAPiName;
            itemToDelete?.Delete();
            // Handles groups
            var groupToDelete = item as Group;
            groupToDelete?.Delete();

            _items.Remove(item);
        }
        #endregion

        #region Fill
        /// <summary>
        /// Gets all items from the list, or fills all of them from the application if they don't yet exist.
        /// </summary>
        /// <returns>List&lt;Story&gt;.</returns>
        public virtual List<T> FillAllItems()
        {
            _items = new List<T>();
            List<T> items = new List<T>();
            if (_apiApp == null) return items;

            List<string> itemNames = GetNameList();
            foreach (var itemName in itemNames)
            {
                T item = FillItem(itemName);
                if (item == null) continue;
                items.Add(item);
            }

            return items;
        }

        /// <summary>
        /// Gets the item from the list, or fills it from the application if it doesn't yet exist.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        public virtual T FillItem(string uniqueName)
        {
            if (Contains(uniqueName)) return this[uniqueName];

            T item = fillNewItem(uniqueName);
            if (item != null)
            {
                _items.Add(item);
            }
            return item;
        }

        /// <summary>
        /// Fills the new item from the application.
        /// </summary>
        /// <param name="uniqueName">Unique name of the item.</param>
        /// <returns>T.</returns>
        protected abstract T fillNewItem(string uniqueName);
        #endregion

        #region Query
        /// <summary>
        /// Returns a list of all of the unique names in the application of the contained object type.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public abstract List<string> GetNameList();
        #endregion
    }
}
