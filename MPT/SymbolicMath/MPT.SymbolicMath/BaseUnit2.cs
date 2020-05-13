using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MPT.SymbolicMath
{
    //public class BaseUnit : BaseBaseUnit, 
    //    IBase
    //{
    //    #region Fields
    //    private bool _isInteger;
    //    private bool _isFloat;
    //    private bool _isFraction;
    //    private bool _isNumber;
        
    //    private Sign _sign = new Sign();
    //    private string _quantity = string.Empty;
    //    private string _variable = string.Empty;
    //    #endregion

    //    #region Public
    //    public BaseUnit(string value)
    //    {
    //        initialize(value);
    //    }

    //    public BaseUnit(int value)
    //    {
    //        _quantity = Sign.RemoveNegativeSign(value.ToString());
    //        _isNumber = true;
    //        _isInteger = true;
    //        _sign = new Sign(value);
    //    }

    //    public BaseUnit(double value)
    //    {
    //        _quantity = Sign.RemoveNegativeSign(value.ToString(CultureInfo.InvariantCulture));
    //        _isNumber = true;
    //        _isFloat = true;
    //        _sign = new Sign(value);
    //    }



    //    public bool IsInteger()
    //    {
    //        return _isInteger;
    //    }

    //    public bool IsFloat()
    //    {
    //        return _isFloat;
    //    }

    //    public bool IsFraction()
    //    {
    //        return _isFraction;
    //    }

    //    public bool IsNumber()
    //    {
    //        return _isNumber;
    //    }

    //    public bool IsSymbolic()
    //    {
    //        return !string.IsNullOrEmpty(_variable);
    //    }

    //    public bool IsEmpty()
    //    {
    //        return (string.IsNullOrEmpty(_quantity) && string.IsNullOrEmpty(_variable));
    //    }

    //    public string Label()
    //    {
    //        string value;
    //        if (hasVariable(this) && _quantity == "1")
    //        {   // 1*X = X
    //            value = _variable;
    //        }
    //        else if (hasVariable(this) && _quantity.Contains('/'))
    //        {   // 5/4X = (5/4)X
    //            value = '(' + _quantity + ')' + _variable;
    //        }
    //        else
    //        {   // 5X
    //            value = _quantity + _variable;
    //        }
    //        return _sign * value;
    //    }

    //    public int AsInteger()
    //    {
    //        return (int)Math.Round(AsFloat());
    //    }

    //    public double AsFloat()
    //    {
    //        return (IsSymbolic() || !IsNumber()) ? 0 : _sign * Calculate();
    //    }

    //    public void FlipSign()
    //    {
    //        _sign.FlipSign();
    //    }

    //    public double Calculate()
    //    {
    //        if (!_isFraction) return double.Parse(_quantity);

    //        string[] values = _quantity.Split('/');
    //        return double.Parse(values[0]) / double.Parse(values[1]);
    //    }

    //    public IBase Simplify(bool isRecursive)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IBase Consolidate(bool isRecursive)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    #endregion

    //    #region Private

    //    private void initialize(string value)
    //    {
    //        if (string.IsNullOrEmpty(value) ||
    //            (value.Length == 1 && value[0] == '.'))
    //        {
    //            return;
    //        }

    //        // Clean trailing decimals for numbers
    //        if (value[value.Length - 1] == '.')
    //        {
    //            value = value.Substring(0, value.Length - 1);
    //        }

    //        // Check properties
    //        _isFraction = isFraction(value);

    //        _isNumber = _isFraction || isNumeric(value);
    //        if (_isNumber)
    //        {
    //            _isInteger = isInteger(value);
    //            _isFloat = !_isInteger;
    //        }

    //        _sign = new Sign(value);

    //        parseValue(value);
    //    }

    //    private void parseValue(string value)
    //    {
    //        value = Sign.RemoveNegativeSign(value);

    //        // Assign quantity and variable
    //        // 1. Test for numeric (take as quantity)
    //        if (_isNumber)
    //        {
    //            _quantity = value;
    //            return;
    //        }

    //        // 2. Get quantity and variable
    //        string quantity;
    //        string variable;

    //        // 2a. Split by * gives 1 or 2 entries.
    //        string[] values = value.Split('*');
    //        switch (values.Length)
    //        {
    //            case 1:
    //                // 2b. Split by index of first non-numeric that is not decimal
    //                quantity = new string(value.TakeWhile(c => !Char.IsLetter(c)).ToArray());

    //                // Multiplication of variables only allowed for fractions if within parentheses
    //                if (quantity.Contains('/') && (quantity[0] != '(' || quantity[quantity.Length - 1] != ')'))
    //                {
    //                    return;
    //                }
    //                variable = value.Substring(quantity.Length);
    //                quantity = stripParenthesesOfFractionalVariables(value, quantity);

    //                // Swap if quantity is not numeric/numeric or numeric
    //                if (!isFraction(quantity) && !isNumeric(quantity))
    //                {
    //                    variable = quantity;
    //                    quantity = string.Empty;
    //                }

    //                // Variable should always be something if quantity is empty
    //                if (string.IsNullOrEmpty(quantity) && string.IsNullOrEmpty(variable))
    //                {
    //                    variable = value;
    //                }
                    
    //                break;
    //            case 2:
    //                quantity = values[0];
    //                variable = values[1];
    //                quantity = stripParenthesesOfFractionalVariables(value, quantity);
    //                break;
    //            default:
    //                // There is more than one *
    //                return;
    //        }

    //        // Clean quantity string
    //        if (string.IsNullOrEmpty(quantity))
    //        {
    //            quantity = "1";
    //        }

    //        // 3. First is numeric (take as quantity), second is not (take as variable). 
    //        if ((quantity == string.Empty && variable == string.Empty) ||
    //            ((!isNumeric(quantity) && !isFraction(quantity)) || isNumeric(variable)))
    //        {
    //            if (STRICT)
    //            {
    //                throw new ArgumentException($"Value does not contain a numeric quantity or a non-numeric variable: {value}");
    //            }
    //            return;
    //        }

    //        // 4. Check for any illegal operators in either item
    //        if (quantity.Count(x => x == '/') > 1 || 
    //            containsIllegalOperators(quantity.Replace("/", "")) || 
    //            containsIllegalOperators(variable))
    //        {
    //            if (STRICT)
    //            {
    //                throw new ArgumentException($"Value cannot contain any mathematical operators or parenthetical groupings: {value}");
    //            }
    //            return;
    //        }
    //        _quantity = quantity;
    //        _variable = variable;
    //    }


    //    private static string stripParenthesesOfFractionalVariables(string value, string quantity)
    //    {
    //        if (quantity.Length > 0 && quantity.Length < value.Length)
    //        {
    //            return removeParentheses(quantity);
    //        }

    //        return quantity;
    //    }
        

    //    private static bool isFraction(string value)
    //    {
    //        string[] values = value.Split('/');
    //        return ((values.Length == 2) && (isNumeric(values[0]) && isNumeric(values[1])));
    //    }

    //    private static bool isNumeric(string value)
    //    {
    //        return (double.TryParse(value, out var valueDouble) && value.Count(x => x == '.') <= 1);
    //    }

    //    private static bool isInteger(string value)
    //    {
    //        return int.TryParse(value, out var valueInt);
    //    }
        
    //    private static bool containsIllegalOperators(string value)
    //    {

    //        List<char> illegalOperators = new List<char>(){'+', '-', '*', '^', '(', ')', '{', '}', '[', ']'};
    //        foreach (char item in illegalOperators)
    //        {
    //            if (value.Contains(item))
    //            {
    //                return true;
    //            }
    //        }

    //        return false;
    //    }

    //    private static bool hasVariable(BaseUnit value)
    //    {
    //        return !string.IsNullOrEmpty(value._variable);
    //    }

    //    private static bool matchingVariable(BaseUnit value1, BaseUnit value2)
    //    {
    //        return value1._variable == value2._variable;
    //    }
    //    #endregion

    //    #region Overrides        
    //    public override string ToString()
    //    {
    //        return (string.IsNullOrEmpty(_quantity) && string.IsNullOrEmpty(_variable)) ? base.ToString() : Label();
    //    }

    //    public BaseUnit CloneUnit()
    //    {
    //        return (BaseUnit)Clone();
    //    }

    //    public object Clone()
    //    {
    //        return new BaseUnit(Label());
    //    }

    //    #endregion

    //    #region Operators

    //    public static BaseUnit operator +(BaseUnit value1, BaseUnit value2)
    //    {
    //        // 1. Null
    //        if (IsEmpty(value1) && IsEmpty(value2))
    //        {
    //            return value1;
    //        }
    //        if (IsEmpty(value1))
    //        {
    //            return value2.CloneUnit();
    //        }
    //        if (IsEmpty(value2))
    //        {
    //            return value1.CloneUnit();
    //        }

    //        // 2. No variables, Matching Variables
    //        if (matchingVariable(value1, value2))
    //        {
    //            if (value1.IsInteger() && value2.IsInteger())
    //            {
    //                int integerQuantity = int.Parse(value1.Label()) + int.Parse(value2.Label());
    //                return new BaseUnit(integerQuantity);
    //            }


    //            double quantity = double.Parse(value1._sign * value1._quantity) + double.Parse(value2._sign * value2._quantity);
    //            if (!hasVariable(value1)) return new BaseUnit(quantity);

    //            string variable = value1._variable;
    //            return new BaseUnit(quantity + variable);
    //        }


    //        // 5. Different variables
    //        throw new NotImplementedException();
    //    }

    //    public static BaseUnit operator -(BaseUnit value1, BaseUnit value2)
    //    {
    //        // 1. Null
    //        if (IsEmpty(value1) && IsEmpty(value2))
    //        {
    //            return value1;
    //        }
    //        if (IsEmpty(value1))
    //        {
    //            BaseUnit newUnit = value2.CloneUnit();
    //            newUnit.FlipSign();
    //            return newUnit;
    //        }
    //        if (IsEmpty(value2))
    //        {
    //            return value1.CloneUnit();
    //        }

    //        // 2. No variables, Matching Variables
    //        if (matchingVariable(value1, value2))
    //        {
    //            if (value1.IsInteger() && value2.IsInteger())
    //            {
    //                int integerQuantity = int.Parse(value1.Label()) - int.Parse(value2.Label());
    //                return new BaseUnit(integerQuantity);
    //            }


    //            double quantity = double.Parse(value1._sign * value1._quantity) - double.Parse(value2._sign * value2._quantity);
    //            if (!hasVariable(value1)) return new BaseUnit(quantity);

    //            string variable = value1._variable;
    //            return new BaseUnit(quantity + variable);
    //        }


    //        // 5. Different variables
    //        throw new NotImplementedException();
    //    }


    //    public static BaseUnit operator *(BaseUnit value1, BaseUnit value2)
    //    {
    //        // 1. Null
    //        if (IsEmpty(value1) || IsEmpty(value2))
    //        {
    //            return new BaseUnit(string.Empty);
    //        }

    //        // 2. No variables, Matching Variables
    //        if (matchingVariable(value1, value2))
    //        {
    //            int sign = (value1._sign.IsNegative() ^ value2._sign.IsNegative()) ? -1 : 1;

    //            if (value1._isInteger && value2._isInteger)
    //            {
    //                int integerQuantity = int.Parse(value1._quantity) * int.Parse(value2._quantity);
    //                return new BaseUnit(sign * integerQuantity);
    //            }


    //            double quantity = double.Parse(value1._sign * value1._quantity) * double.Parse(value2._sign * value2._quantity);
    //            if (hasVariable(value1))
    //            {
    //                string variable = value1._variable;
    //                return new BaseUnit((sign * quantity) + variable + "^2");
    //            }
    //            return new BaseUnit(sign * quantity);
    //        }


    //        // 5. Different variables
    //        throw new NotImplementedException();
    //    }



    //    public static BaseUnit operator /(BaseUnit value1, BaseUnit value2)
    //    {
    //        // 1. Null
    //        if (IsEmpty(value1) || IsEmpty(value2))
    //        {
    //            return new BaseUnit(string.Empty);
    //        }

    //        // 2. No variables, Matching Variables
    //        if (matchingVariable(value1, value2))
    //        {
    //            int sign = (value1._sign.IsNegative() ^ value2._sign.IsNegative()) ? -1 : 1;

    //            if (value1._isInteger && value2._isInteger)
    //            {
    //                int integerQuantity = int.Parse(value1._quantity) / int.Parse(value2._quantity);
    //                return new BaseUnit(sign * integerQuantity);
    //            }


    //            double quantity = double.Parse(value1._sign * value1._quantity) / double.Parse(value2._sign * value2._quantity);
    //            if (hasVariable(value1))
    //            {
    //                string variable = value1._variable;
    //                return new BaseUnit((sign * quantity) + variable + "^2");
    //            }
    //            return new BaseUnit(sign * quantity);
    //        }


    //        // 5. Different variables
    //        throw new NotImplementedException();
    //    }
    //    #endregion
    //}
}
