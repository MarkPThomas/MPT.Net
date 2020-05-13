using System.Collections.Generic;

namespace MPT.CSI.Serialize.Models.Support
{
    /// <summary>
    /// A list of objects deriving from a shared type, where only one of each object type is allowed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TypeList<T> where T : new()
    {
        protected List<T> _items = new List<T>();

        /// <summary>
        /// Updates the item if it exists, or adds one if it does not.
        /// </summary>
        /// <typeparam name="T1">The type of the t1.</typeparam>
        /// <param name="newItem">The new item.</param>
        public void UpdateItem<T1>(T1 newItem) where T1 : T
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (!(_items[i] is T1)) continue;
                _items[i] = newItem;
                return;
            }
            _items.Add(newItem);
        }

        /// <summary>
        /// Gets the item if it exists, or initializes a new instance if it does not.
        /// </summary>
        /// <typeparam name="T1">The type of the t1.</typeparam>
        /// <returns>T1.</returns>
        public T1 GetItem<T1>() where T1 : T, new()
        {
            foreach (T item in _items)
            {
                if (item is T1 existingItem)
                {
                    return existingItem;
                }
            }
            T1 newItem = new T1();

            _items.Add(newItem);
            return newItem;
        }
    }
}
