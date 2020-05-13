// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-04-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-28-2018
// ***********************************************************************
// <copyright file="PrimitiveUnit.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;
using System.Linq;

namespace MPT.SymbolicMath
{
    /// <summary>
    /// Class PrimitiveUnit.
    /// </summary>
    /// <seealso cref="MPT.SymbolicMath.BaseUnit" />
    public class PrimitiveUnit : BaseUnit
    {
        #region Fields
        /// <summary>
        /// The base is number
        /// </summary>
        protected bool _baseIsNumber;
        /// <summary>
        /// The base is integer
        /// </summary>
        protected bool _baseIsInteger;
        /// <summary>
        /// The base is float
        /// </summary>
        protected bool _baseIsFloat;
        /// <summary>
        /// The base is fraction
        /// </summary>
        protected bool _baseIsFraction;

        /// <summary>
        /// The base
        /// </summary>
        protected string _base = string.Empty;
        #endregion

        #region Public
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveUnit"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="power">The power.</param>
        public PrimitiveUnit(string value, IBase power = null)
        {
            initialize(value, power);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveUnit"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="power">The power.</param>
        public PrimitiveUnit(int value, IBase power = null)
        {
            _sign = new Sign(value);
            _base = Sign.RemoveNegativeSign(value.ToString());
            addPower(power);
            _baseIsNumber = true;
            _baseIsInteger = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveUnit"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="power">The power.</param>
        public PrimitiveUnit(double value, IBase power = null)
        {
            _sign = new Sign(value);
            _base = Sign.RemoveNegativeSign(value.ToString(CultureInfo.InvariantCulture));
            addPower(power);
            _baseIsNumber = true;
            _baseIsFloat = true;
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
        public override bool IsEmpty()
        {
            return (_base == null || string.IsNullOrEmpty(_base));
        }

        /// <summary>
        /// Determines whether this instance is integer.
        /// </summary>
        /// <returns><c>true</c> if this instance is integer; otherwise, <c>false</c>.</returns>
        public override bool IsInteger()
        {
            return _baseIsInteger &&
                   (!HasPower() || 
                    (_power.IsInteger() && !_power.SignIsNegative()));
        }

        /// <summary>
        /// Determines whether this instance is float.
        /// </summary>
        /// <returns><c>true</c> if this instance is float; otherwise, <c>false</c>.</returns>
        public override bool IsFloat()
        {
            if (!_baseIsNumber || (HasPower() && _power.IsSymbolic()))
            {
                return false;
            }

            return !IsInteger();
        }

        /// <summary>
        /// Determines whether this instance is fraction.
        /// </summary>
        /// <returns><c>true</c> if this instance is fraction; otherwise, <c>false</c>.</returns>
        public override bool IsFraction()
        {
            return _baseIsFraction &&
                   (!HasPower() || _power.IsFraction());
        }

        /// <summary>
        /// Determines whether this instance is number.
        /// </summary>
        /// <returns><c>true</c> if this instance is number; otherwise, <c>false</c>.</returns>
        public override bool IsNumber()
        {
            return _baseIsNumber &&
                   (!HasPower() || _power.IsNumber());
        }

        /// <summary>
        /// Determines whether this instance is symbolic.
        /// </summary>
        /// <returns><c>true</c> if this instance is symbolic; otherwise, <c>false</c>.</returns>
        public override bool IsSymbolic()
        {
            return !IsNumber();
        }

        /// <summary>
        /// Values the is negative.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool ValueIsNegative()
        {
            return SignIsNegative();
        }

        /// <summary>
        /// Labels this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string Label()
        {
            return string.IsNullOrEmpty(_base) ? 
                string.Empty : 
                label(_base, addBracketsToBase(), addBracketsToPower());
        }

        /// <summary>
        /// Adds the brackets to base.
        /// </summary>
        /// <param name="addBrackets">if set to <c>true</c> [add brackets].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private bool addBracketsToBase(bool addBrackets = false)
        {
            return addBrackets || (_baseIsFraction && HasPower());
        }

        /// <summary>
        /// Bases the label.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string BaseLabel()
        {
            return string.IsNullOrEmpty(_base) ? string.Empty : baseLabel(_base);
        }

        /// <summary>
        /// Ases the integer.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int AsInteger()
        {
            double asDouble = AsFloat();
            if (double.IsPositiveInfinity(asDouble))
            {
                return int.MaxValue;
            }
            if (double.IsNegativeInfinity(asDouble))
            {
                return int.MinValue;
            }
            if (double.IsNaN(asDouble))
            {
                return 0;
            }
            return (int)Math.Round(asDouble);
        }

        /// <summary>
        /// Ases the float.
        /// </summary>
        /// <returns>System.Double.</returns>
        public override double AsFloat()
        {
            return (IsNumber()) ? Calculate() : 0;
        }

        /// <summary>
        /// Gets the base.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase GetBase()
        {
            return new PrimitiveUnit(_base);
        }

        /// <summary>
        /// Gets the absolute.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase GetAbsolute()
        {
            return new PrimitiveUnit(_base, _power);
        }

        /// <summary>
        /// Calculates this instance.
        /// </summary>
        /// <returns>System.Double.</returns>
        /// <exception cref="Exception"></exception>
        public override double Calculate()
        {
            if (IsSymbolic())
            {
                if (Strict)
                {
                    throw new Exception(valueSymbolicExceptionMessage(this));
                }
                return 0;
            }

            double baseValue;
            if (!_baseIsFraction && _baseIsNumber)
            {
                baseValue = double.Parse(_base);
            }
            else
            {
                string[] values = _base.Split('/');
                baseValue = double.Parse(values[0]) / double.Parse(values[1]);
            }

            return calculate(baseValue);
        }

        /// <summary>
        /// Simplifies the specified is recursive.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public override IBase Simplify(bool isRecursive = false)
        {
            if (string.IsNullOrEmpty(_base)) return new PrimitiveUnit(string.Empty);
            switch (PowerLabel())
            {
                case "0":
                    return new PrimitiveUnit(1);
                case "1":
                case "":
                    return SimplifyBase();
                default:
                    IBase newPower = SimplifyPower(isRecursive);
                    if (newPower.Label() == "0") return new PrimitiveUnit(1);
                    IBase newBase;
                    
                    if (newPower.ValueIsNegative())
                    { // Flip the fraction and use a positive power
                        newBase = getInvertedSimplifiedBase(newPower, isRecursive);
                        IBase simplifiedUnit = new ProductQuotientSet(1);
                        return simplifiedUnit.Divide(newBase);
                    }

                    newBase = SimplifyBase(newPower);
                    newBase.ExtractSign(isRecursive);
                    return newBase;
            }
        }

        /// <summary>
        /// Simplifies the base.
        /// </summary>
        /// <param name="power">The power.</param>
        /// <returns>IBase.</returns>
        public IBase SimplifyBase(IBase power = null)
        {
            if (string.IsNullOrEmpty(_base)) return new PrimitiveUnit(string.Empty);
            string newBase = IsFraction() ? Query.SimplifiedFraction(_base) : _base;
            return new PrimitiveUnit(_sign * newBase, power);
        }

        /// <summary>
        /// Simplifies the power.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public override IBase SimplifyPower(bool isRecursive = false)
        {
            return string.IsNullOrEmpty(_base) ? 
                new PrimitiveUnit(string.Empty) : 
                base.SimplifyPower(isRecursive);
        }

        /// <summary>
        /// Distributes the sign.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase DistributeSign()
        {   // Nothing to do
            return CloneUnit();
        }

        /// <summary>
        /// Extracts the sign.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public override IBase ExtractSign(bool isRecursive = false)
        {   // Nothing to do
            return CloneUnit();
        }

        /// <summary>
        /// Distributes the sign from power.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase DistributeSignFromPower()
        {
            if (IsEmpty()) return new PrimitiveUnit(string.Empty);
            PrimitiveUnit newSet = CloneUnit();
            //for (int i = 0; i < _units.Count; i++)
            //{
            //    if (_operators[i] == Query.EMPTY.ToString())
            //    {
            //        newSet._units[i].FlipSign();
            //    }
            //    else if (_operators[i] == Query.ADD.ToString())
            //    {
            //        newSet._operators[i] = Query.SUBTRACT.ToString();
            //    }
            //    else if (_operators[i] == Query.SUBTRACT.ToString())
            //    {
            //        newSet._operators[i] = Query.ADD.ToString();
            //    }
            //}

            return newSet;
        }

        /// <summary>
        /// Extracts the sign from power.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase ExtractSignFromPower()
        {
            if (IsEmpty()) return new PrimitiveUnit(string.Empty);
            PrimitiveUnit newSet = CloneUnit();
            IBase power = newSet.GetPower()?.ExtractSign();
            newSet.addPower(power);
            return newSet;
        }

        /// <summary>
        /// Determines whether [has valid powers] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="isPowerValueOnly">if set to <c>true</c> [is power value only].</param>
        /// <returns><c>true</c> if [has valid powers] [the specified value]; otherwise, <c>false</c>.</returns>
        public static bool HasValidPowers(string value, bool isPowerValueOnly = false)
        {
            string[] values = value.Split(Query.POWER);
            if (!(value.Count(x => x == Query.POWER) > 1))
            {   // Single Power
                return powerIsValid(values, isPowerValueOnly);
            }

            // Multiple powers must be enclosed in parentheses for power groups
            if (!value.Intersect(Group.CloseGroupTypes)
                      .Any()) return false;

            // Any power grouping higher than A^(n^m) needs to be created through a set. e.g. A^(n^(m^o))
            if (values.Length < 3) return true;     
            
            // Check all but innermost power for grouping
            return powerGroupIsValid(values, isPowerValueOnly);
        }

        #endregion

        #region Private

        /// <summary>
        /// Initializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="power">The power.</param>
        private void initialize(string value, IBase power)
        {
            if (string.IsNullOrEmpty(value) ||
                (value.Length == 1 && value[0] == '.') ||
                !HasValidPowers(value))
            {
                return;
            }

            _sign = new Sign(value);

            if (IsEmpty(power) && value.Contains(Query.POWER))
            {
                power = initializePowerObject(value);
                value = parseBaseValue(value);
                if (string.IsNullOrEmpty(value)) return;
                value = Group.RemoveOuterBrackets(value);
            }
            addPower(power);

            // Clean trailing decimals for numbers
            if (value[value.Length - 1] == '.')
            {
                value = value.Substring(0, value.Length - 1);
            }

            // Check properties
            _baseIsFraction = Query.IsSymbolicFraction(value);
            _baseIsNumber = Query.IsNumericFraction(value) || Query.IsNumeric(value);
            if (_baseIsNumber)
            {
                _baseIsInteger = Query.IsInteger(value);
                _baseIsFloat = !_baseIsInteger;
            }
            
            parseValue(value);
        }

        /// <summary>
        /// Parses the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="ArgumentException"></exception>
        private void parseValue(string value)
        {
            value = Sign.RemoveNegativeSign(value);

            // Check that Value is either purely numeric or purely a variable
            if (!Query.IsNumeric(value) && !Query.IsSymbolicFraction(value) && !Query.IsAllLetters(value))
            {
                if (Strict)
                {
                    throw new ArgumentException($"Value must contain only letters, or a number: {value}");
                }
                return;
            }
            _base = value;
        }

        /// <summary>
        /// Gets the inverted simplified base.
        /// </summary>
        /// <param name="newPower">The new power.</param>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        private IBase getInvertedSimplifiedBase(IBase newPower, bool isRecursive)
        {
            newPower = newPower.ExtractSign(isRecursive);
            newPower.FlipSign();
            if (newPower.SignIsNegative() && !newPower.ValueIsNegative())
            {
                newPower = newPower.DistributeSign();
            }

            IBase newBase = SimplifyBase(newPower);
            newBase.ExtractSign(isRecursive);

            return newBase;
        }
        #endregion

        #region Private: Static
        /// <summary>
        /// Initializes the power object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>PrimitiveUnit.</returns>
        private static PrimitiveUnit initializePowerObject(string value)
        {
            string powerValue = parseExponentValue(value);
            if (powerValue.Length > 1 && powerValue[0] == Sign.NEGATIVE)
            {
                powerValue = powerValue.Substring(1);
                if (Group.HasOuterParentheses(powerValue))
                {
                    powerValue = Group.RemoveOuterBrackets(powerValue);
                }
                powerValue = Sign.NEGATIVE + powerValue;
            }
            else if (Group.HasOuterParentheses(powerValue))
            {
                powerValue = Group.RemoveOuterBrackets(powerValue);
            }

            PrimitiveUnit power = new PrimitiveUnit(powerValue);
            return power;
        }

        /// <summary>
        /// Parses the base value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private static string parseBaseValue(string value)
        {
            string newValue = string.Empty;
            foreach (char character in value)
            {
                if (character == Query.POWER)
                {
                    break;
                }
                newValue += character;
            }

            return newValue;
        }

        /// <summary>
        /// Parses the exponent value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        private static string parseExponentValue(string value)
        {
            string newPower = string.Empty;
            bool isPower = false;
            foreach (char character in value)
            {
                if (!isPower && character == Query.POWER)
                {
                    isPower = true;
                    continue;
                }

                if (isPower)
                {
                    newPower += character;
                }
            }
            return newPower;
        }


        /// <summary>
        /// Powers the is valid.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="isPowerValueOnly">if set to <c>true</c> [is power value only].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected static bool powerIsValid(string[] values, bool isPowerValueOnly = false)
        {
            for (int i = 0; i < values.Length; i++)
            {
                // Skip first value as it is the base, unless the direct power is given
                if (i == 0 && !isPowerValueOnly) continue;
                if (values[i].Contains(Query.MULTIPLY)) return false;   // A^(n*m) is invalid
                if (Query.ContainsAddSubtract(values[i])) return false; // A^(n+m) and A^(n-m) are invalid
                // A^(n/m) is valid since exponent can be treated as a fraction power
            }

            return true;
        }

        /// <summary>
        /// Powers the group is valid.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <param name="isPowerValueOnly">if set to <c>true</c> [is power value only].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected static bool powerGroupIsValid(string[] values, bool isPowerValueOnly = false)
        {
            for (int i = 0; i < values.Length - 1; i++)
            { // Leave the last power uninspected as it could be a non-grouped single power
                // Skip first value as it is the base, unless the direct power is given
                if (i == 0 && !isPowerValueOnly) continue;
                //if (values[i].Contains(Query.MULTIPLY)) return false;
                if (!Group.OpenGroupTypes.Contains(values[i][0])) return false;
            }

            return true;
        }
        #endregion

        #region Overrides        
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            return IsEmpty() ? base.ToString() : Label();
        }

        /// <summary>
        /// Clones the unit.
        /// </summary>
        /// <returns>PrimitiveUnit.</returns>
        public PrimitiveUnit CloneUnit()
        {
            return (PrimitiveUnit)Clone();
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return new PrimitiveUnit(Label());
        }
        #endregion

        #region Equals
        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(PrimitiveUnit value1, PrimitiveUnit value2)
        {
            return value1?.Equals(value2) ?? ReferenceEquals(value2, null);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(PrimitiveUnit value1, PrimitiveUnit value2)
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
            if (!(obj is PrimitiveUnit)) return false;
            PrimitiveUnit other = (PrimitiveUnit) obj;
            if (IsEmpty() && other.IsEmpty())
            {
                return true;
            }

            return Equals(other);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool Equals(PrimitiveUnit other)
        {   // Equality is ignoring possible simplifications
            if (other == null) return false;
            return base.Equals(other) &&
                   _baseIsNumber == other._baseIsNumber && 
                   _baseIsInteger == other._baseIsInteger && 
                   _baseIsFloat == other._baseIsFloat && 
                   _baseIsFraction == other._baseIsFraction && 
                   string.Equals(_base, other._base);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ _baseIsNumber.GetHashCode();
                hashCode = (hashCode * 397) ^ _baseIsInteger.GetHashCode();
                hashCode = (hashCode * 397) ^ _baseIsFloat.GetHashCode();
                hashCode = (hashCode * 397) ^ _baseIsFraction.GetHashCode();
                hashCode = (hashCode * 397) ^ (_base != null ? _base.GetHashCode() : 0);
                return hashCode;
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator +(PrimitiveUnit value1, PrimitiveUnit value2)
        {
            checkNull(value1, value2);
            return bothAreNumeric(value1, value2) ? 
                new SumDifferenceSet(addNumericAsType(value1, value2)) : 
                addSymbolic(value1, value2);
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator -(PrimitiveUnit value1, PrimitiveUnit value2)
        {
            checkNull(value1, value2);
            return bothAreNumeric(value1, value2) ? 
                new SumDifferenceSet(subtractNumericAsType(value1, value2)) : 
                subtractSymbolic(value1, value2);
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator *(PrimitiveUnit value1, PrimitiveUnit value2)
        {
            checkNull(value1, value2);
            return bothAreNumeric(value1, value2) ?
                new ProductQuotientSet(multiplyNumericAsType(value1, value2)) : 
                multiplySymbolic(value1, value2);
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator /(PrimitiveUnit value1, PrimitiveUnit value2)
        {
            checkNull(value1, value2);
            return bothAreNumeric(value1, value2) ?
                new ProductQuotientSet(divideNumericAsType(value1, value2)) : 
                divideSymbolic(value1, value2);
        }
        #endregion

        #region Operators: Cross-Types IBase

        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static SumDifferenceSet operator +(PrimitiveUnit value1, IBase value)
        {
            checkNull(value1, value);
            switch (value)
            {
                case PrimitiveUnit unit:
                    return value1 + unit;
                case SumDifferenceSet differenceSet:
                    return value1 + differenceSet;
                case ProductQuotientSet quotientset:
                    return value1 + quotientset;
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="value1">The value1.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static SumDifferenceSet operator +(IBase value, PrimitiveUnit value1)
        {
            checkNull(value1, value);
            switch (value)
            {
                case PrimitiveUnit unit:
                    return unit + value1;
                case SumDifferenceSet differenceSet:
                    return differenceSet + value1;
                case ProductQuotientSet quotientset:
                    return quotientset + value1;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static SumDifferenceSet operator -(PrimitiveUnit value1, IBase value)
        {
            checkNull(value1, value);
            switch (value)
            {
                case PrimitiveUnit unit:
                    return value1 - unit;
                case SumDifferenceSet differenceSet:
                    return value1 - differenceSet;
                case ProductQuotientSet quotientset:
                    return value1 - quotientset;
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="value1">The value1.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static SumDifferenceSet operator -(IBase value, PrimitiveUnit value1)
        {
            checkNull(value1, value);
            switch (value)
            {
                case PrimitiveUnit unit:
                    return unit - value1;
                case SumDifferenceSet differenceSet:
                    return differenceSet - value1;
                case ProductQuotientSet quotientset:
                    return quotientset - value1;
                default:
                    throw new NotImplementedException();
            }
        }


        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ProductQuotientSet operator *(PrimitiveUnit value1, IBase value)
        {
            checkNull(value1, value);
            switch (value)
            {
                case PrimitiveUnit unit:
                    return value1 * unit;
                case SumDifferenceSet differenceSet:
                    return value1 * differenceSet;
                case ProductQuotientSet quotientset:
                    return value1 * quotientset;
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="value1">The value1.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ProductQuotientSet operator *(IBase value, PrimitiveUnit value1)
        {
            checkNull(value1, value);
            switch (value)
            {
                case PrimitiveUnit unit:
                    return unit * value1;
                case SumDifferenceSet differenceSet:
                    return differenceSet * value1;
                case ProductQuotientSet quotientset:
                    return quotientset * value1;
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ProductQuotientSet operator /(PrimitiveUnit value1, IBase value)
        {
            checkNull(value1, value);
            switch (value)
            {
                case PrimitiveUnit unit:
                    return value1 / unit;
                case SumDifferenceSet differenceSet:
                    return value1 / differenceSet;
                case ProductQuotientSet quotientset:
                    return value1 / quotientset;
                default:
                    throw new NotImplementedException();
            }
        }
        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="value1">The value1.</param>
        /// <returns>The result of the operator.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static ProductQuotientSet operator /(IBase value, PrimitiveUnit value1)
        {
            checkNull(value1, value);
            switch (value)
            {
                case PrimitiveUnit unit:
                    return unit / value1;
                case SumDifferenceSet differenceSet:
                    return differenceSet / value1;
                case ProductQuotientSet quotientset:
                    return quotientset / value1;
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion

        #region Operators: Cross-Types Integer
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator +(PrimitiveUnit set, int value)
        {
            checkNull(set);
            return set + new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator +(int value, PrimitiveUnit set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) + set;
        }


        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator -(PrimitiveUnit set, int value)
        {
            checkNull(set);
            return set - new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator -(int value, PrimitiveUnit set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) - set;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator *(PrimitiveUnit set, int value)
        {
            checkNull(set);
            return set * new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator *(int value, PrimitiveUnit set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) * set;
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator /(PrimitiveUnit set, int value)
        {
            checkNull(set);
            return set / new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator /(int value, PrimitiveUnit set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) / set;
        }
        #endregion

        #region Operators: Cross-Types Double
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator +(PrimitiveUnit set, double value)
        {
            checkNull(set);
            return set + new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator +(double value, PrimitiveUnit set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) + set;
        }


        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator -(PrimitiveUnit set, double value)
        {
            checkNull(set);
            return set - new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator -(double value, PrimitiveUnit set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) - set;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator *(PrimitiveUnit set, double value)
        {
            checkNull(set);
            return set * new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator *(double value, PrimitiveUnit set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) * set;
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator /(PrimitiveUnit set, double value)
        {
            checkNull(set);
            return set / new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator /(double value, PrimitiveUnit set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) / set;
        }
        #endregion

        #region Operators: Cross-Types String
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator +(PrimitiveUnit set, string value)
        {
            checkNull(set, value);
            return set + new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the + operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator +(string value, PrimitiveUnit set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) + set;
        }


        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator -(PrimitiveUnit set, string value)
        {
            checkNull(set, value);
            return set - new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the - operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static SumDifferenceSet operator -(string value, PrimitiveUnit set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) - set;
        }

        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator *(PrimitiveUnit set, string value)
        {
            checkNull(set, value);
            return set * new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the * operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator *(string value, PrimitiveUnit set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) * set;
        }


        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="set">The set.</param>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator /(PrimitiveUnit set, string value)
        {
            checkNull(set, value);
            return set / new PrimitiveUnit(value);
        }
        /// <summary>
        /// Implements the / operator.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="set">The set.</param>
        /// <returns>The result of the operator.</returns>
        public static ProductQuotientSet operator /(string value, PrimitiveUnit set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) / set;
        }
        #endregion
    }
}
