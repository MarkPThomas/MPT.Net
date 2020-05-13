// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-08-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-12-2018
// ***********************************************************************
// <copyright file="Group.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Collections.Generic;
using System.Linq;

namespace MPT.SymbolicMath
{
    /// <summary>
    /// Class Group.
    /// </summary>
    public class Group
    {
        #region Fields
        /// <summary>
        /// The bracket count
        /// </summary>
        private int _bracketCount;
        /// <summary>
        /// The power bracket count
        /// </summary>
        private int _powerBracketCount;
        #endregion

        #region Properties
        // TODO: Make these immutable
        // TODO: Make method to add to these, but only in synchronized open/closed pairs
        /// <summary>
        /// The open group types
        /// </summary>
        public static List<char> OpenGroupTypes = new List<char>() { '(', '[', '{', '<' };
        /// <summary>
        /// The close group types
        /// </summary>
        public static List<char> CloseGroupTypes = new List<char>() { ')', ']', '}', '>' };

        /// <summary>
        /// Gets the bracket character.
        /// </summary>
        /// <value>The bracket character.</value>
        public char BracketCharacter { get; private set; } = Query.EMPTY;
        /// <summary>
        /// Gets the power bracket character.
        /// </summary>
        /// <value>The power bracket character.</value>
        public char PowerBracketCharacter { get; private set; } = Query.EMPTY;

        /// <summary>
        /// Gets a value indicating whether this instance is negative.
        /// </summary>
        /// <value><c>true</c> if this instance is negative; otherwise, <c>false</c>.</value>
        public bool IsNegative { get; }
        /// <summary>
        /// Gets the base.
        /// </summary>
        /// <value>The base.</value>
        public string Base { get; } = string.Empty;
        /// <summary>
        /// Gets the power.
        /// </summary>
        /// <value>The power.</value>
        public string Power { get; } = string.Empty;
        /// <summary>
        /// Gets the index of the continue.
        /// </summary>
        /// <value>The index of the continue.</value>
        public int ContinueIndex { get; private set; } = -1;
        #endregion

        #region Public
        /// <summary>
        /// Initializes a new instance of the <see cref="Group"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public Group(string value)
        {
            if (string.IsNullOrEmpty(value)) return;
            if (!BracketsAreBalanced(value)) return;

            if (isOuterNegative(value))
            {
                IsNegative = true;
                // Strip negative sign
                value = value.Substring(1);
            }

            Base = getInnerContent(value);
            Power = getOuterPower(value);
            adjustContinueIndex();
        }
        #endregion

        #region Public Static

        /// <summary>
        /// Indexes the of start of bracket group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int IndexOfStartOfBracketGroup(string value)
        {
            if (!stringHasAnyBrackets(value)) return -1;
            
            int indexCount = -1;
            foreach (char c in value)
            {
                indexCount++;
                if (OpenGroupTypes.Contains(c))
                {
                    break;
                }
                if (CloseGroupTypes.Contains(c))
                { // For closing brackets that are unmatched
                    return -1;
                }
            }

            return indexCount;
        }

        /// <summary>
        /// Indexes the of end of bracket group.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Int32.</returns>
        public static int IndexOfEndOfBracketGroup(string value)
        {
            if (!stringHasAnyBrackets(value)) return -1;

            Stack<char> brackets = new Stack<char>();
            bool hasCountedBrackets = false;
            int indexCount = -1;
            foreach (char c in value)
            {
                if (OpenGroupTypes.Contains(c))
                {
                    if (!hasCountedBrackets)
                    {
                        hasCountedBrackets = true;
                    }
                    brackets.Push(c);
                }
                else if (brackets.Count > 0 && ClosingBracket(brackets.Peek()) == c)
                {
                    brackets.Pop();
                }
                else if (CloseGroupTypes.Contains(c))
                { // For closing brackets that are unmatched
                    return -1;
                }

                indexCount++;
                if (hasCountedBrackets && brackets.Count == 0)
                {
                    break;
                }
            }

            return hasCountedBrackets ? indexCount : 0;
        }

        /// <summary>
        /// Bracketses the are balanced.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="limitToSingleBrackets">if set to <c>true</c> [limit to single brackets].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool BracketsAreBalanced(string value, bool limitToSingleBrackets = false)
        {
            if (string.IsNullOrEmpty(value)) return false;

            // See if string has any of the brackets
            if (!OpenGroupTypes.Intersect(value).Any() && !CloseGroupTypes.Intersect(value).Any())
            {
                return true;
            }

            if ((!OpenGroupTypes.Intersect(value).Any() || !CloseGroupTypes.Intersect(value).Any()))
            {
                return false;
            }

            Stack<char> brackets = new Stack<char>();
            bool hasCountedBrackets = false;
            foreach (char c in value)
            {
                if (limitToSingleBrackets && hasCountedBrackets && brackets.Count == 0 &&
                    (Query.OperatorStandardTypes.Contains(c) || OpenGroupTypes.Contains(c)))
                { // For multiple brackets that are combined (e.g. (A)+(B)
                    return false;
                }
                else if (OpenGroupTypes.Contains(c))
                {
                    if (!hasCountedBrackets)
                    {
                        hasCountedBrackets = true;
                    }
                    brackets.Push(c);
                }
                else if (brackets.Count > 0 && ClosingBracket(brackets.Peek()) == c)
                {
                    brackets.Pop();
                }
                else if (CloseGroupTypes.Contains(c))
                { // For closing brackets that are unmatched
                    return false;
                }
            }

            return brackets.Count == 0;
        }

        /// <summary>
        /// Closings the bracket.
        /// </summary>
        /// <param name="openingBracket">The opening bracket.</param>
        /// <returns>System.Char.</returns>
        public static char ClosingBracket(char openingBracket)
        {
            int index = OpenGroupTypes.IndexOf(openingBracket);
            if (-1 < index && index < CloseGroupTypes.Count)
            {
                return CloseGroupTypes[OpenGroupTypes.IndexOf(openingBracket)];
            }

            return Query.EMPTY;
        }

        /// <summary>
        /// Adds the outer brackets.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="groupIndex">Index of the group.</param>
        /// <returns>System.String.</returns>
        public static string AddOuterBrackets(string value, int groupIndex = 0)
        {
            if (string.IsNullOrEmpty(value) || HasOuterParentheses(value)) return value;
            if (groupIndex > OpenGroupTypes.Count - 1)
            {
                groupIndex = 0;
            }
            return OpenGroupTypes[groupIndex] + value + CloseGroupTypes[groupIndex];
        }

        /// <summary>
        /// Removes the outer brackets.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string RemoveOuterBrackets(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 2) return value;
            return HasOuterParentheses(value) ? value.Substring(1, value.Length - 2) : value;
        }

        /// <summary>
        /// Determines whether [has outer parentheses] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [has outer parentheses] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool HasOuterParentheses(string value)
        {
            if (string.IsNullOrEmpty(value) || !BracketsAreBalanced(value, limitToSingleBrackets: true)) return false;
            return OpenGroupTypes.Contains(value[0]) && ClosingBracket(value[0]) == value[value.Length - 1];
        }
        #endregion

        #region Private

        /// <summary>
        /// Gets the content of the inner.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private string getInnerContent(string value)
        {
            // It is assumed that any value given begins with a bracket. If not, the original string is returned.
            if (!OpenGroupTypes.Contains(value[0]))
            {
                ContinueIndex = value.Length;
                return value;
            }

            string innerContent = string.Empty;
            BracketCharacter = getOpenBracket(value);

            for (int i = 0; i < value.Length; i++)
            {
                updateBracketCountsAtIndex(value, i, BracketCharacter, ref _bracketCount);

                if ((isInsideBase() || isInsideBasePower()))
                {
                    innerContent += value[i];
                }
                else if (isOutsideAll())
                {
                    innerContent += value[i];
                    ContinueIndex = i + 1;  
                    break;
                }
            }

            return RemoveOuterBrackets(innerContent);
        }

        /// <summary>
        /// Gets the outer power.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private string getOuterPower(string value)
        {
            if (value.Length > ContinueIndex && ContinueIndex >= 0) value = value.Substring(ContinueIndex);
            if (string.IsNullOrEmpty(value)) return string.Empty;

            // It is assumed that values begin in the form of a power ^ character.
            if (value[0] != Query.POWER) return string.Empty;
            // Strip power character
            value = value.Substring(1);

            PowerBracketCharacter = getOpenBracket(value);
            return PowerBracketCharacter != Query.EMPTY ?
                getOuterPowerGrouped(value) :
                getOuterPowerUngrouped(value);
        }

        /// <summary>
        /// Gets the outer power grouped.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private string getOuterPowerGrouped(string value)
        {
            string innerContent = string.Empty;
            for (int i = 0; i < value.Length; i++)
            {
                updateBracketCountsAtIndex(value, i, PowerBracketCharacter, ref _powerBracketCount);

                innerContent += value[i];

                if (isInsideOutsidePower() || (i == 0 && innerContent == "-")) continue;

                ContinueIndex += (i + 1);
                return RemoveOuterBrackets(innerContent);
            }

            // This can never be reached due to the balanced brace check, but included for static compiler warnings.
            return value; 
        }

        /// <summary>
        /// Gets the outer power ungrouped.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private string getOuterPowerUngrouped(string value)
        {
            string innerContent = string.Empty;
            for (int i = 0; i < value.Length; i++)
            {
                if (Query.OperatorAllTypes.Contains(value[i]))
                {
                    ContinueIndex += i;
                    return innerContent;
                }
                innerContent += value[i];
            }

            ContinueIndex += value.Length;
            return innerContent;
        }




        /// <summary>
        /// Determines whether [is outside all].
        /// </summary>
        /// <returns><c>true</c> if [is outside all]; otherwise, <c>false</c>.</returns>
        private bool isOutsideAll()
        {
            return (_bracketCount == 0 && _powerBracketCount == 0);
        }

        /// <summary>
        /// Determines whether [is inside base].
        /// </summary>
        /// <returns><c>true</c> if [is inside base]; otherwise, <c>false</c>.</returns>
        private bool isInsideBase()
        {
            return (_bracketCount != 0 && _powerBracketCount == 0);
        }

        /// <summary>
        /// Determines whether [is inside base power].
        /// </summary>
        /// <returns><c>true</c> if [is inside base power]; otherwise, <c>false</c>.</returns>
        private bool isInsideBasePower()
        {
            return (_bracketCount != 0 && _powerBracketCount != 0);
        }

        /// <summary>
        /// Determines whether [is inside outside power].
        /// </summary>
        /// <returns><c>true</c> if [is inside outside power]; otherwise, <c>false</c>.</returns>
        private bool isInsideOutsidePower()
        {
            return (_bracketCount == 0 && _powerBracketCount != 0);
        }


        /// <summary>
        /// Adjusts the index of the continue.
        /// </summary>
        private void adjustContinueIndex()
        {
            if (IsNegative)
            { // Extra count is for the negative sign that was stripped before the recording functions were called.
                ContinueIndex++;
            }
            if (!string.IsNullOrEmpty(Power))
            { // Extra count is for the power sign that was stripped in the power function.
                ContinueIndex++;
            }
        }
        #endregion

        #region Private Static

        /// <summary>
        /// Determines whether [is outer negative] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if [is outer negative] [the specified value]; otherwise, <c>false</c>.</returns>
        private static bool isOuterNegative(string value)
        {
            return (!string.IsNullOrEmpty(value) && value.Length > 2 &&
                    value[0] == Sign.NEGATIVE && OpenGroupTypes.Contains(value[1]));
        }

        /// <summary>
        /// Gets the open bracket.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.Char.</returns>
        private static char getOpenBracket(string value)
        {
            foreach (char character in value)
            {
                if (!OpenGroupTypes.Contains(character)) continue;
                return character;
            }

            return Query.EMPTY;
        }

        /// <summary>
        /// Determines whether [is open bracket] [the specified character].
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="openingBracketCharacter">The opening bracket character.</param>
        /// <returns><c>true</c> if [is open bracket] [the specified character]; otherwise, <c>false</c>.</returns>
        private static bool isOpenBracket(char character, char openingBracketCharacter)
        {
            if (openingBracketCharacter == Query.EMPTY)
            {
                return OpenGroupTypes.Contains(character);
            }

            return character == openingBracketCharacter;
        }

        /// <summary>
        /// Determines whether [is close bracket] [the specified character].
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="openingBracketCharacter">The opening bracket character.</param>
        /// <returns><c>true</c> if [is close bracket] [the specified character]; otherwise, <c>false</c>.</returns>
        private static bool isCloseBracket(char character, char openingBracketCharacter)
        {
            char closingBracketCharacter = ClosingBracket(openingBracketCharacter);
            if (closingBracketCharacter == Query.EMPTY)
            {
                return CloseGroupTypes.Contains(character);
            }

            return character == closingBracketCharacter;
        }

        /// <summary>
        /// Updates the index of the bracket counts at.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="i">The i.</param>
        /// <param name="openingBracketCharacter">The opening bracket character.</param>
        /// <param name="bracketCount">The bracket count.</param>
        private static void updateBracketCountsAtIndex(string value, int i, char openingBracketCharacter, ref int bracketCount)
        {
            if (isOpenBracket(value[i], openingBracketCharacter))
            {
                bracketCount++;
            }
            else if (isCloseBracket(value[i], openingBracketCharacter))
            {
                bracketCount--;
            }
        }

        /// <summary>
        /// Strings the has any brackets.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool stringHasAnyBrackets(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            // See if string has any of the brackets
            if (!OpenGroupTypes.Intersect(value).Any() && !CloseGroupTypes.Intersect(value).Any())
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
