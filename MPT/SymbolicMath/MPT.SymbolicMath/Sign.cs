// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-04-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-29-2018
// ***********************************************************************
// <copyright file="Sign.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace MPT.SymbolicMath
{
    /// <summary>
    /// Class Sign.
    /// </summary>
    public class Sign
    {
        #region Constants
        /// <summary>
        /// The negative
        /// </summary>
        public const char NEGATIVE = '-';
        #endregion

        #region Fields
        /// <summary>
        /// The sign
        /// </summary>
        protected int _sign = 1;
        #endregion

        #region Public

        /// <summary>
        /// Initializes a new instance of the <see cref="Sign"/> class.
        /// </summary>
        public Sign(){}

        /// <summary>
        /// Initializes a new instance of the <see cref="Sign"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Sign(string value)
        {
            setSign(IsPositive(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sign"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Sign(int value)
        {
            setSign(value >= 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sign"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Sign(double value)
        {
            setSign(value >= 0);
        }


        /// <summary>
        /// Labels this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public string Label()
        {
            return IsNegative() ? NEGATIVE.ToString() : string.Empty;
        }

        /// <summary>
        /// Determines whether this instance is negative.
        /// </summary>
        /// <returns><c>true</c> if this instance is negative; otherwise, <c>false</c>.</returns>
        public bool IsNegative()
        {
            return !IsPositive();
        }

        /// <summary>
        /// Determines whether this instance is positive.
        /// </summary>
        /// <returns><c>true</c> if this instance is positive; otherwise, <c>false</c>.</returns>
        public bool IsPositive()
        {
            return _sign == 1;
        }



        /// <summary>
        /// Flips the sign.
        /// </summary>
        public void FlipSign()
        {
            _sign *= -1;
        }
        #endregion

        #region Public: Static

        /// <summary>
        /// Determines whether the specified symbolic value is positive.
        /// </summary>
        /// <param name="symbolicValue">The symbolic value.</param>
        /// <returns><c>true</c> if the specified symbolic value is positive; otherwise, <c>false</c>.</returns>
        public static bool IsPositive(string symbolicValue)
        {
            return !IsNegative(symbolicValue);
        }

        /// <summary>
        /// Determines whether the specified symbolic value is negative.
        /// </summary>
        /// <param name="symbolicValue">The symbolic value.</param>
        /// <returns><c>true</c> if the specified symbolic value is negative; otherwise, <c>false</c>.</returns>
        public static bool IsNegative(string symbolicValue)
        {
            if (string.IsNullOrEmpty(symbolicValue)) return false;
            return  symbolicValue[0] == NEGATIVE;
        }

        /// <summary>
        /// Removes the negative sign.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string RemoveNegativeSign(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return (value[0] == NEGATIVE) ? value.Substring(1) : value;
        }
        #endregion

        #region Override

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return base.ToString() + " (" + _sign + ")";
        }

        #endregion

        #region Private
        /// <summary>
        /// Sets the sign.
        /// </summary>
        /// <param name="isPositive">if set to <c>true</c> [is positive].</param>
        protected void setSign(bool isPositive)
        {
            _sign = isPositive ? 1 : -1;
        }
        #endregion

        #region Equals
        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Sign value1, Sign value2)
        {
            return value1?.Equals(value2) ?? ReferenceEquals(value2, null);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Sign value1, Sign value2)
        {
            return !value1?.Equals(value2) ?? !ReferenceEquals(value2, null);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Sign)) return false;
            Sign other = (Sign)obj;

            return Equals(other);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(Sign other)
        {
            if (other == null) return false;
            return _sign == other._sign;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            return _sign;
        }

        #endregion

        #region Operators
        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="sign1">The sign1.</param>
        /// <param name="sign2">The sign2.</param>
        /// <returns>The result of the operator.</returns>
        public static Sign operator *(Sign sign1, Sign sign2)
        {
            return new Sign(sign1._sign * sign2._sign);
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="sign">The sign.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static string operator *(Sign sign, string value)
        {
            return sign.Label() + value;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="sign">The sign.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static int operator *(Sign sign, int value)
        {
            return sign._sign * value;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="sign">The sign.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static double operator *(Sign sign, double value)
        {
            return sign._sign * value;
        }
        #endregion
    }
}
