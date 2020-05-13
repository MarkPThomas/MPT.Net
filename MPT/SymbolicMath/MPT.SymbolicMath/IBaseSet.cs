// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-11-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-26-2018
// ***********************************************************************
// <copyright file="IBaseSet.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.SymbolicMath
{
    /// <summary>
    /// Interface IBaseSet
    /// </summary>
    /// <seealso cref="MPT.SymbolicMath.IBase" />
    public interface IBaseSet : IBase
    { // TODO: Future versions, using newer .Net Framework & then also derive from IReadOnlyList<T> & IReadOnlyCollection<T>
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        int Count { get; }

        /// <summary>
        /// Gets the <see cref="UnitOperatorPair"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>UnitOperatorPair.</returns>
        UnitOperatorPair this[int index]
        {
            get;
        }

        /// <summary>
        /// Appends the item to group.
        /// </summary>
        /// <param name="lastOperand">The last operand.</param>
        /// <param name="newValuePrimitive">The new value primitive.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool AppendItemToGroup(char lastOperand, IBase newValuePrimitive);
        /// <summary>
        /// Factories the specified new value primitive.
        /// </summary>
        /// <param name="newValuePrimitive">The new value primitive.</param>
        /// <returns>IBaseSet.</returns>
        IBaseSet Factory(IBase newValuePrimitive);

        /// <summary>
        /// Simplifies the fractional.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        IBase SimplifyFractional(bool isRecursive = false);
        /// <summary>
        /// Simplifies the units of one.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        IBaseSet SimplifyUnitsOfOne(bool isRecursive = true);
        /// <summary>
        /// Simplifies the variables.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        IBaseSet SimplifyVariables(bool isRecursive = false);
        /// <summary>
        /// Simplifies the operand groups.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        IBaseSet SimplifyOperandGroups(bool isRecursive = false);
    }
}
