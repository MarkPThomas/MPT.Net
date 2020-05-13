// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-04-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-21-2018
// ***********************************************************************
// <copyright file="IBase.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace MPT.SymbolicMath
{
    /// <summary>
    /// Interface IBase
    /// </summary>
    /// <seealso cref="System.ICloneable" />
    public interface IBase : ICloneable
    {
        /// <summary>
        /// Determines whether this instance is integer.
        /// </summary>
        /// <returns><c>true</c> if this instance is integer; otherwise, <c>false</c>.</returns>
        bool IsInteger();
        /// <summary>
        /// Returns the calculated value as an integer.
        /// </summary>
        /// <returns>System.Int32.</returns>
        int AsInteger();

        /// <summary>
        /// Determines whether this instance is float.
        /// </summary>
        /// <returns><c>true</c> if this instance is float; otherwise, <c>false</c>.</returns>
        bool IsFloat();
        /// <summary>
        /// Ases the float.
        /// </summary>
        /// <returns>System.Double.</returns>
        double AsFloat();

        /// <summary>
        /// Determines whether this instance is fraction.
        /// </summary>
        /// <returns><c>true</c> if this instance is fraction; otherwise, <c>false</c>.</returns>
        bool IsFraction();
        /// <summary>
        /// Determines whether this instance is number.
        /// </summary>
        /// <returns><c>true</c> if this instance is number; otherwise, <c>false</c>.</returns>
        bool IsNumber();
        /// <summary>
        /// Determines whether this instance is symbolic.
        /// </summary>
        /// <returns><c>true</c> if this instance is symbolic; otherwise, <c>false</c>.</returns>
        bool IsSymbolic();
        /// <summary>
        /// Signs the is negative.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool SignIsNegative();
        /// <summary>
        /// Values the is negative.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        bool ValueIsNegative();
        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
        bool IsEmpty();
        /// <summary>
        /// Determines whether this instance has power.
        /// </summary>
        /// <returns><c>true</c> if this instance has power; otherwise, <c>false</c>.</returns>
        bool HasPower();

        /// <summary>
        /// Labels this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        string Label();
        /// <summary>
        /// Bases the label.
        /// </summary>
        /// <returns>System.String.</returns>
        string BaseLabel();
        /// <summary>
        /// Powers the label.
        /// </summary>
        /// <returns>System.String.</returns>
        string PowerLabel();

        /// <summary>
        /// Gets the power.
        /// </summary>
        /// <returns>IBase.</returns>
        IBase GetPower();
        /// <summary>
        /// Gets the base.
        /// </summary>
        /// <returns>IBase.</returns>
        IBase GetBase();
        /// <summary>
        /// Gets the absolute.
        /// </summary>
        /// <returns>IBase.</returns>
        IBase GetAbsolute();
        /// <summary>
        /// Gets the sign.
        /// </summary>
        /// <returns>Sign.</returns>
        Sign GetSign();

        /// <summary>
        /// Flips the sign.
        /// </summary>
        void FlipSign();
        /// <summary>
        /// Calculates this instance.
        /// </summary>
        /// <returns>System.Double.</returns>
        double Calculate();

        /// <summary>
        /// Simplifies the specified is recursive.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        IBase Simplify(bool isRecursive = false);
        /// <summary>
        /// Extracts the sign.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        IBase ExtractSign(bool isRecursive = false);
        /// <summary>
        /// Distributes the sign.
        /// </summary>
        /// <returns>IBase.</returns>
        IBase DistributeSign();
        /// <summary>
        /// Distributes the sign from power.
        /// </summary>
        /// <returns>IBase.</returns>
        IBase DistributeSignFromPower();
        /// <summary>
        /// Extracts the sign from power.
        /// </summary>
        /// <returns>IBase.</returns>
        IBase ExtractSignFromPower();


        /// <summary>
        /// Sums the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>SumDifferenceSet.</returns>
        SumDifferenceSet Sum(IBase value);
        /// <summary>
        /// Subtracts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>SumDifferenceSet.</returns>
        SumDifferenceSet Subtract(IBase value);
        /// <summary>
        /// Multiplies the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ProductQuotientSet.</returns>
        ProductQuotientSet Multiply(IBase value);
        /// <summary>
        /// Divides the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ProductQuotientSet.</returns>
        ProductQuotientSet Divide(IBase value);
    }
}
