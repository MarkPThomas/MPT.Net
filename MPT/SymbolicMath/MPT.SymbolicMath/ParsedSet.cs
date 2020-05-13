using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPT.SymbolicMath
{
    public class ParsedSet
    {
        #region Constants
        protected const char NEGATIVE = '-';
        // Operators
        protected const char ADD = '+';
        protected const char SUBTRACT = '-';
        protected const char MULTIPLY = '*';
        protected const char DIVIDE = '/';
        protected const char POWER = '^';

        // Other
        protected List<char> _openGroups = new List<char>
        {
            '(',
            '[',
            '{'
        };
        protected List<char> _closeGroups = new List<char>
        {
            ')',
            ']',
            '}'
        };

        private int bracketsBalance = 0;
        #endregion

        public int ValueSign { get; private set; }
        public string Value { get; private set; }
        public string Power { get; private set; }
        public string Operand { get; private set; }
        public bool IsParsed { get; private set; }
        public List<ParsedSet> ParsedValues { get; private set; } = new List<ParsedSet>();
        public IBase ParsedValue { get; private set; }

        public ParsedSet(string value, string operand = "")
        {
            Value = value;
            Operand = operand;
        }

        public void ParseStringByParentheses()
        {
            string newValue = string.Empty;
            string lastOperand = string.Empty;
            for (int i = 0; i < Value.Length; i++)
            {
                if (i == 0 && Value[i] == NEGATIVE)
                {
                    ValueSign = -1;
                    newValue += Value[i];
                    continue;
                }

                updateBracketsBalance(Value[i]);
                if (bracketsBalance == 0 &&
                    isBracketOpening(Value[i]))
                {
                    continue;
                }
                if (bracketsBalance == 0 &&
                    isBracketClosing(Value[i]))
                {
                    continue;
                }
                if (bracketsBalance == 0 &&
                    (isBracketOpening(Value[i]) || isBracketClosing(Value[i])))
                {
                    continue;
                }

                if (Value[i] == ADD || Value[i] == SUBTRACT)
                {
                    if (i == 0 || i == Value.Length - 1)
                    {
                        continue;
                    }
                    ParsedValues.Add(new ParsedSet(newValue, lastOperand));
                    lastOperand = Value[i].ToString();
                }
                else if (Value[i] == POWER || (i > 0 && Value[i-1] == POWER))
                {
                    Power += Value[i];
                }
                else
                {
                    newValue += Value[i];
                }
            }
        }

        public void ParseStringToLists()
        {
            string newValue = string.Empty;
            string lastOperand = string.Empty;
            for (int i = 0; i < Value.Length; i++)
            {
                if (i == 0 && Value[i] == NEGATIVE)
                {
                    newValue += Value[i];
                    continue;
                }

                updateBracketsBalance(Value[i]);
                if (bracketsBalance == 0 &&
                    (isBracketOpening(Value[i]) || isBracketClosing(Value[i])))
                {
                    continue;
                }

                if (Value[i] == ADD || Value[i] == SUBTRACT)
                {
                    if (i == 0 || i == Value.Length - 1)
                    {
                        continue;
                    }
                    ParsedValues.Add(new ParsedSet(newValue, lastOperand));
                    lastOperand = Value[i].ToString();
                }
                else
                {
                    newValue += Value[i];
                }
            }
        }

        // Parse by parentheses
        // Parse by +-
        // Parse by */
        


        private bool isBracketOpening(char character)
        {
            return _openGroups.Contains(character);
        }

        private bool isBracketClosing(char character)
        {
            return _closeGroups.Contains(character);
        }

        private void updateBracketsBalance(char character)
        {
            if (isBracketOpening(character)) bracketsBalance++;
            if (isBracketClosing(character)) bracketsBalance--;
        }

        public void ParseListToLists()
        {

        }

        public void ParseListsToPrimitives()
        {

        }

        public void ParseListsToSets()
        {

        }
    }
}
