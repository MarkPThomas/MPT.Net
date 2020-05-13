// ***********************************************************************
// Assembly         : MPT.CSI.OOAPI
// Author           : Mark Thomas
// Created          : 12-13-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 12-13-2018
// ***********************************************************************
// <copyright file="ObjectExtensions.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Reflection;
using System.ArrayExtensions;
// See ref: https://github.com/Burtsev-Alexey/net-object-deep-copy/blob/master/ObjectExtensions.cs
// See ref: https://stackoverflow.com/questions/129389/how-do-you-do-a-deep-copy-of-an-object-in-net-c-specifically/11308879#11308879

// ReSharper disable once CheckNamespace
namespace System
{
    /// <summary>
    /// Class ObjectExtensions.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// The clone method
        /// </summary>
        private static readonly MethodInfo _cloneMethod = typeof(Object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        /// <summary>
        /// Determines whether the specified type is primitive.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if the specified type is primitive; otherwise, <c>false</c>.</returns>
        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(String)) return true;
            return (type.IsValueType & type.IsPrimitive);
        }

        /// <summary>
        /// Copies the specified original object.
        /// </summary>
        /// <param name="originalObject">The original object.</param>
        /// <returns>Object.</returns>
        public static Object Copy(this Object originalObject)
        {
            return internalCopy(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
        }
        /// <summary>
        /// Internals the copy.
        /// </summary>
        /// <param name="originalObject">The original object.</param>
        /// <param name="visited">The visited.</param>
        /// <returns>Object.</returns>
        private static Object internalCopy(Object originalObject, IDictionary<Object, Object> visited)
        {
            if (originalObject == null) return null;
            var typeToReflect = originalObject.GetType();
            if (IsPrimitive(typeToReflect)) return originalObject;
            if (visited.ContainsKey(originalObject)) return visited[originalObject];
            if (typeof(Delegate).IsAssignableFrom(typeToReflect)) return null;
            var cloneObject = _cloneMethod.Invoke(originalObject, null);
            if (typeToReflect.IsArray)
            {
                var arrayType = typeToReflect.GetElementType();
                if (IsPrimitive(arrayType) == false)
                {
                    Array clonedArray = (Array)cloneObject;
                    clonedArray.ForEach((array, indices) => array.SetValue(internalCopy(clonedArray.GetValue(indices), visited), indices));
                }

            }
            visited.Add(originalObject, cloneObject);
            copyFields(originalObject, visited, cloneObject, typeToReflect);
            recursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
            return cloneObject;
        }

        /// <summary>
        /// Recursives the copy base type private fields.
        /// </summary>
        /// <param name="originalObject">The original object.</param>
        /// <param name="visited">The visited.</param>
        /// <param name="cloneObject">The clone object.</param>
        /// <param name="typeToReflect">The type to reflect.</param>
        private static void recursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect)
        {
            if (typeToReflect.BaseType != null)
            {
                recursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
                copyFields(originalObject, visited, cloneObject, typeToReflect.BaseType, BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
            }
        }

        /// <summary>
        /// Copies the fields.
        /// </summary>
        /// <param name="originalObject">The original object.</param>
        /// <param name="visited">The visited.</param>
        /// <param name="cloneObject">The clone object.</param>
        /// <param name="typeToReflect">The type to reflect.</param>
        /// <param name="bindingFlags">The binding flags.</param>
        /// <param name="filter">The filter.</param>
        private static void copyFields(object originalObject, IDictionary<object, object> visited, object cloneObject, Type typeToReflect, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy, Func<FieldInfo, bool> filter = null)
        {
            foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
            {
                if (filter != null && filter(fieldInfo) == false) continue;
                if (IsPrimitive(fieldInfo.FieldType)) continue;
                var originalFieldValue = fieldInfo.GetValue(originalObject);
                var clonedFieldValue = internalCopy(originalFieldValue, visited);
                fieldInfo.SetValue(cloneObject, clonedFieldValue);
            }
        }
        /// <summary>
        /// Copies the specified original.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original">The original.</param>
        /// <returns>T.</returns>
        public static T Copy<T>(this T original)
        {
            return (T)Copy((Object)original);
        }
    }

    /// <summary>
    /// Class ReferenceEqualityComparer.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.EqualityComparer{Object}" />
    public class ReferenceEqualityComparer : EqualityComparer<Object>
    {
        /// <summary>
        /// When overridden in a derived class, determines whether two objects of type T are equal.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns><see langword="true" /> if the specified objects are equal; otherwise, <see langword="false" />.</returns>
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }
        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object for which to get a hash code.</param>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode(object obj)
        {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }

    namespace ArrayExtensions
    {
        /// <summary>
        /// Class ArrayExtensions.
        /// </summary>
        public static class ArrayExtensions
        {
            /// <summary>
            /// Fors the each.
            /// </summary>
            /// <param name="array">The array.</param>
            /// <param name="action">The action.</param>
            public static void ForEach(this Array array, Action<Array, int[]> action)
            {
                if (array.LongLength == 0) return;
                ArrayTraverse walker = new ArrayTraverse(array);
                do action(array, walker.Position);
                while (walker.Step());
            }
        }

        /// <summary>
        /// Class ArrayTraverse.
        /// </summary>
        internal class ArrayTraverse
        {
            /// <summary>
            /// The position
            /// </summary>
            public int[] Position;
            /// <summary>
            /// The maximum lengths
            /// </summary>
            private readonly int[] _maxLengths;

            /// <summary>
            /// Initializes a new instance of the <see cref="ArrayTraverse"/> class.
            /// </summary>
            /// <param name="array">The array.</param>
            public ArrayTraverse(Array array)
            {
                _maxLengths = new int[array.Rank];
                for (int i = 0; i < array.Rank; ++i)
                {
                    _maxLengths[i] = array.GetLength(i) - 1;
                }
                Position = new int[array.Rank];
            }

            /// <summary>
            /// Steps this instance.
            /// </summary>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            public bool Step()
            {
                for (int i = 0; i < Position.Length; ++i)
                {
                    if (Position[i] < _maxLengths[i])
                    {
                        Position[i]++;
                        for (int j = 0; j < i; j++)
                        {
                            Position[j] = 0;
                        }
                        return true;
                    }
                }
                return false;
            }
        }
    }

}