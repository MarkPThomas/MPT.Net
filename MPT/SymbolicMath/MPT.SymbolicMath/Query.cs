// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-27-2018
// ***********************************************************************
// <copyright file="Query.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MPT.SymbolicMath
{
    /// <summary>
    /// Class Query.
    /// </summary>
    public static class Query
    {
        #region Constants
        // Operators
        /// <summary>
        /// The empty
        /// </summary>
        public const char EMPTY = '\0';
        /// <summary>
        /// The add
        /// </summary>
        public const char ADD = '+';
        /// <summary>
        /// The subtract
        /// </summary>
        public const char SUBTRACT = '-';
        /// <summary>
        /// The multiply
        /// </summary>
        public const char MULTIPLY = '*';
        /// <summary>
        /// The divide
        /// </summary>
        public const char DIVIDE = '/';
        /// <summary>
        /// The power
        /// </summary>
        public const char POWER = '^';

        // Groups
        /// <summary>
        /// The add subtract types
        /// </summary>
        public static List<char> AddSubtractTypes = new List<char>() { ADD, SUBTRACT };
        /// <summary>
        /// The multiply divide types
        /// </summary>
        public static List<char> MultiplyDivideTypes = new List<char>() { MULTIPLY, DIVIDE };
        /// <summary>
        /// The operator standard types
        /// </summary>
        public static List<char> OperatorStandardTypes = new List<char>() { ADD, SUBTRACT, MULTIPLY, DIVIDE };
        /// <summary>
        /// The operator all types
        /// </summary>
        public static List<char> OperatorAllTypes = new List<char>() { ADD, SUBTRACT, MULTIPLY, DIVIDE, POWER };
        #endregion

        /// <summary>
        /// Determines whether [is symbolic fraction] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is symbolic fraction] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsSymbolicFraction(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            //  Account for existence of outer brackets & fail for any inner brackets
            value = Group.RemoveOuterBrackets(value);
            if (value.Intersect(Group.OpenGroupTypes).Any() ||
                value.Intersect(Group.CloseGroupTypes).Any())
            {
                return false;
            }

            // Check for 1 numerator & 1 denominator
            string[] values = value.Split('/');
            if (values.Length != 2) return false;

            // Check that no operators exist for either value
            string numerator = values[0];
            string denominator = values[1];
            if (numerator.Substring(1).Intersect(OperatorAllTypes).Any() ||
                denominator.Substring(1).Intersect(OperatorAllTypes).Any())
            {
                return false;
            }

            // Check that either value is either purely a number or purely symbolic
            numerator = (numerator[0] == Sign.NEGATIVE)? numerator.Substring(1) : numerator;
            denominator = (denominator[0] == Sign.NEGATIVE) ? denominator.Substring(1) : denominator;
            return ((IsNumeric(numerator) || IsAllLetters(numerator)) &&
                (IsNumeric(denominator) || IsAllLetters(denominator)));
        }

        /// <summary>
        /// Determines whether [is numeric fraction] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is numeric fraction] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsNumericFraction(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            value = Group.RemoveOuterBrackets(value);
            string[] values = value.Split('/');
            return ((values.Length == 2) && (IsNumeric(values[0]) && IsNumeric(values[1])));
        }

        /// <summary>
        /// Determines whether the specified value is numeric.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is numeric; otherwise, <c>false</c>.</returns>
        public static bool IsNumeric(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return (double.TryParse(value, out _) && value.Count(x => x == '.') <= 1);
        }

        /// <summary>
        /// Determines whether [is all letters] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is all letters] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsAllLetters(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            foreach (char character in value)
            {
                if (!Char.IsLetter(character))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines whether the specified value is integer.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is integer; otherwise, <c>false</c>.</returns>
        public static bool IsInteger(string value)
        {
            return !string.IsNullOrEmpty(value) && int.TryParse(value, out _);
        }


        /// <summary>
        /// Determines whether [is sum difference] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="characterIndex">Index of the character.</param>
        /// <returns><c>true</c> if [is sum difference] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsSumDifference(string value, int characterIndex)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return (value[characterIndex] == ADD || IsSubtraction(value, characterIndex));
        }

        /// <summary>
        /// Determines whether [is product quotient] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="characterIndex">Index of the character.</param>
        /// <returns><c>true</c> if [is product quotient] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool IsProductQuotient(string value, int characterIndex)
        {
            return !string.IsNullOrEmpty(value) && MultiplyDivideTypes.Contains(value[characterIndex]);
        }

        /// <summary>
        /// Bases the is negative.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool BaseIsNegative(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return value[0] == SUBTRACT;
        }

        /// <summary>
        /// Determines whether the specified value is negative.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="characterIndex">Index of the character.</param>
        /// <returns><c>true</c> if the specified value is negative; otherwise, <c>false</c>.</returns>
        public static bool IsNegative(string value, int characterIndex)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return ((characterIndex == 0 && value[characterIndex] == Sign.NEGATIVE) ||   // -A, -(
                    (characterIndex > 0 && ((value[characterIndex] == Sign.NEGATIVE && Group.OpenGroupTypes.Contains(value[characterIndex - 1])) ||    // (-A
                                            (value[characterIndex] == Sign.NEGATIVE && OperatorAllTypes.Contains(value[characterIndex - 1])))));   // B--A, B+-A, B*-A, B/-A, B^-A
        }

        /// <summary>
        /// Determines whether the specified value is subtraction.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="characterIndex">Index of the character.</param>
        /// <returns><c>true</c> if the specified value is subtraction; otherwise, <c>false</c>.</returns>
        public static bool IsSubtraction(string value, int characterIndex)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return (value[characterIndex] == SUBTRACT && !IsNegative(value, characterIndex));
        }

        /// <summary>
        /// Swaps the negative characters.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetOperator">The target operator.</param>
        /// <returns>System.String.</returns>
        private static string swapNegativeCharacters(string value, char targetOperator)
        {
            if (targetOperator != SUBTRACT) return value;

            char[] replacedValues = value.ToCharArray();
            if (replacedValues[0] == SUBTRACT)
            {   // Swap negative sign
                replacedValues[0] = '_';
            }
            for (int i = 0; i < replacedValues.Length; i++)
            {   // Swap negative characters next to all operators and groupings
                if ((OperatorAllTypes.Contains(replacedValues[i]) || targetOperator == replacedValues[i] || Group.OpenGroupTypes.Contains(replacedValues[i])) &&
                    (i + 1 < replacedValues.Length && replacedValues[i + 1] == SUBTRACT))
                {
                    replacedValues[i + 1] = '_';
                }
            }

            value = new string(replacedValues);

            return value;
        }

        /// <summary>
        /// Indexes the of next operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetOperator">The target operator.</param>
        /// <param name="ignoreInGroups">if set to <c>true</c> [ignore in groups].</param>
        /// <returns>System.Int32.</returns>
        public static int IndexOfNextOperator(string value, char targetOperator, bool ignoreInGroups = true)
        {
            if (string.IsNullOrEmpty(value) || targetOperator == EMPTY) return -1;
            value = swapNegativeCharacters(value, targetOperator);
            int index = value.IndexOf(targetOperator);
            if (!ignoreInGroups) return index;

            // Adjust index to ignore operators in groups
            if (index < Group.IndexOfStartOfBracketGroup(value)) return index;

            int maxGroupIndex = Group.IndexOfEndOfBracketGroup(value);
            if (maxGroupIndex < 0) return index;
            value = value.Substring(maxGroupIndex);
            return maxGroupIndex + value.IndexOf(targetOperator);
        }

        /// <summary>
        /// Indexes the of next operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetOperators">The target operators.</param>
        /// <param name="ignoreInGroups">if set to <c>true</c> [ignore in groups].</param>
        /// <returns>System.Int32.</returns>
        public static int IndexOfNextOperator(string value, char[] targetOperators, bool ignoreInGroups = true)
        {
            if (string.IsNullOrEmpty(value) || targetOperators == null || targetOperators.Length == 0) return -1;
            value = targetOperators.Aggregate(value, swapNegativeCharacters);

            int index = value.IndexOfAny(targetOperators);
            if (!ignoreInGroups) return index;

            // Adjust index to ignore operators in groups
            if (index < Group.IndexOfStartOfBracketGroup(value)) return index;

            int maxGroupIndex = Group.IndexOfEndOfBracketGroup(value);
            if (maxGroupIndex < 0) return index;
            value = value.Substring(maxGroupIndex);
            return maxGroupIndex + value.IndexOfAny(targetOperators);
        }

        /// <summary>
        /// Determines whether [contains multiply divide] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [contains multiply divide] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool ContainsMultiplyDivide(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return (value.Contains(MULTIPLY) || value.Contains(DIVIDE));
        }

        /// <summary>
        /// Determines whether [contains add subtract] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [contains add subtract] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool ContainsAddSubtract(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            return value.Contains(ADD) || ContainsSubtraction(value);
        }

        // TODO: Update to ignore powers?
        /// <summary>
        /// Determines whether the specified value contains subtraction.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value contains subtraction; otherwise, <c>false</c>.</returns>
        public static bool ContainsSubtraction(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (!value.Contains(SUBTRACT)) return false;

            // Count number of '-' signs
            int subtractionCount = value.Count(c => c == SUBTRACT);

            // Subtract all '-' signs that are negative signs and not used for subtraction.
            for (int i = 0; i < value.Length; i++)
            {
                if (IsNegative(value, i))
                {
                    subtractionCount--;
                }
            }
            
            return subtractionCount > 0;
        }

        public static string SimplifiedFraction(string value, double zeroTolerance = 10E-14)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            string[] values = value.Split(DIVIDE);
            if (values.Length != 2) return value;
            string numerator = values[0];
            string denominator = values[1];

            bool numeratorIsNumber = double.TryParse(numerator, out var numeratorValue);
            bool denominatorIsNumber = double.TryParse(denominator, out var denominatorValue);

            // Fraction is non-reducible symbolic
            if (!numeratorIsNumber || !denominatorIsNumber) return value;

            // Division by Zero
            switch (denominatorValue)
            {
                case 0 when Math.Abs(numeratorValue) < zeroTolerance:
                    return double.NaN.ToString(CultureInfo.InvariantCulture);
                case 0 when Math.Sign(numeratorValue) > 0:
                    return double.PositiveInfinity.ToString(CultureInfo.InvariantCulture);
                case 0 when Math.Sign(numeratorValue) < 0:
                    return double.NegativeInfinity.ToString(CultureInfo.InvariantCulture);
            }

            // Division yields integer (by doubles or integers)
            double division = numeratorValue / denominatorValue;
            string divisionAsString = division.ToString(CultureInfo.InvariantCulture);
            if (IsInteger(divisionAsString))
            {
                return division.ToString(CultureInfo.InvariantCulture);
            }

            // Either numerator or denominator is not an integer
            if (!IsInteger(numerator) || !IsInteger(denominator)) return value;

            // Greatest Common Divisor for numerator/denominator yields new integers
            int greatestCommonDivisor =
                GreatestCommonDenominator(
                    Convert.ToInt32(numeratorValue),
                    Convert.ToInt32(denominatorValue),
                    1);

            int sign = Math.Sign(numeratorValue) * Math.Sign(denominatorValue);
            string newNumerator = (sign * Math.Abs(numeratorValue) / greatestCommonDivisor).ToString(CultureInfo.InvariantCulture);
            string newDenominator = ((Math.Abs(denominatorValue) / greatestCommonDivisor)).ToString(CultureInfo.InvariantCulture);
            return newNumerator + DIVIDE + newDenominator;
        }

        /// <summary>
        /// Greatests the common denominator. Uses the Binary Method.
        /// </summary>
        /// <param name="a">Value a</param>
        /// <param name="b">Value b</param>
        /// <param name="result">The result, the greatest common denominator.</param>
        /// <returns>System.Int32.</returns>
        public static int GreatestCommonDenominator(int a, int b, int result)
        {
            // Only non-zero positive integers are allowed
            if (a < 0) a = Math.Abs(a);
            if (b < 0) b = Math.Abs(b);
            if (a == 0 || b == 0) return 0;

            while (true)
            {
                if (a == b) return result * a;
                if (a % 2 == 0 && b % 2 == 0)
                {
                    a /= 2;
                    b /= 2;
                    result *= 2;
                    continue;
                }

                if (a % 2 == 0)
                {
                    a /= 2;
                    continue;
                }

                if (b % 2 == 0)
                {
                    b /= 2;
                    continue;
                }

                // For when original a >> b or b >> a
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }
        }
    }
}
