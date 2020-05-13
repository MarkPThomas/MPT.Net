using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPT.SymbolicMath
{
    public class Parser
    {
        public static List<char> OpenGroupTypes = new List<char>() { '(', '[', '{' };
        public static List<char> CloseGroupTypes = new List<char>() { ')', '[', '{' };

        private int _bracketCount = 0;
        private int _powerBracketCount = 0;
        private char _bracketCharacter = '\0';

        public bool IsNegative { get; private set; }
        public string Base { get; private set; }
        public string Power { get; private set; }
        public int ContinueIndex { get; private set; } = -1;

        public Parser(string value)
        {
            if (IsOuterNegative(value))
            {
                IsNegative = true;
                value = value.Substring(1);
            }
            
            Base = GetInnerContent(value);
            Power = GetOuterPower(value);
        }

        public bool IsOuterNegative(string value)
        {
            return (!string.IsNullOrEmpty(value) && value.Length > 2 &&
                    value[0] == Query.NEGATIVE && OpenGroupTypes.Contains(value[1]));
        }

        private void updateBracketCountsAtIndex(string value, int i)
        {
            if (i > 1 && value[i - 1] == Query.POWER)
            {
                _powerBracketCount++;
            }
            else
            {
                _bracketCount++;
            }
        }

        public string GetInnerContent(string value)
        {
            string innerContent = string.Empty;
            string outerContent = string.Empty;
            bool hasEnteredGroup = false;
            _bracketCharacter = getOpenBracket(value);
            
            for (int i = 0; i < value.Length; i++)
            {
                if (isOpenBracket(value[i], _bracketCharacter))
                {
                    updateBracketCountsAtIndex(value, i);
                }

                if (isInsideBase())
                {
                    hasEnteredGroup = true;
                }

                if (isOutsideAll() && !hasEnteredGroup)
                {
                    outerContent += value[i];
                }
                else if ((isInsideBase() || isInsideBasePower()) && hasEnteredGroup)
                {
                    innerContent += value[i];
                }
                else if (isInsideOutsidePower() || (isOutsideAll() && hasEnteredGroup))
                {
                    ContinueIndex = i;
                }
            }

            return !string.IsNullOrEmpty(innerContent)? innerContent : outerContent;
        }

        private char getOpenBracket(string value)
        {
            foreach (char character in value)
            {
                if (!OpenGroupTypes.Contains(character)) continue;
                return character;
            }

            return '\0';
        }

        private bool isOpenBracket(char character, char bracketCharacter)
        {
            if (bracketCharacter == '\0')
            {
                return OpenGroupTypes.Contains(character);
            }

            return character == bracketCharacter;
        }

        private bool isOutsideAll()
        {
            return (_bracketCount == 0 && _powerBracketCount == 0);
        }

        private bool isInsideBase()
        {
            return (_bracketCount != 0 && _powerBracketCount == 0);
        }

        private bool isInsidePower()
        {
            return (_powerBracketCount != 0);
        }

        private bool isInsideBasePower()
        {
            return (_bracketCount != 0 && _powerBracketCount != 0);
        }

        private bool isInsideOutsidePower()
        {
            return (_bracketCount == 0 && _powerBracketCount != 0);
        }

        public string GetOuterPower(string value)
        {
            // ContinueIndex
            return string.Empty;
        }



        // addOuterParentheses
        public static string addParentheses(string value, int groupIndex = 0)
        {
            if (string.IsNullOrEmpty(value) || Query.HasOuterParentheses(value)) return value;
            if (groupIndex > OpenGroupTypes.Count - 1)
            {
                groupIndex = 0;
            }
            return OpenGroupTypes[groupIndex] + value + CloseGroupTypes[groupIndex];
        }

        // removeOuterParentheses
        public static string removeParentheses(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 3) return value;
            return Query.HasOuterParentheses(value) ? value.Substring(1, value.Length - 2) : value;
        }
    }
}
