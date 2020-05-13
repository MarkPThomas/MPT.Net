// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-04-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-28-2018
// ***********************************************************************
// <copyright file="BaseUnit.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Globalization;

namespace MPT.SymbolicMath
{
    /// <summary>
    /// Class BaseUnit.
    /// </summary>
    /// <seealso cref="MPT.SymbolicMath.IBase" />
    public abstract class BaseUnit : IBase
    {
        #region Fields
        /// <summary>
        /// The sign
        /// </summary>
        protected Sign _sign = new Sign();
        /// <summary>
        /// The power
        /// </summary>
        protected IBase _power;
        #endregion

        #region Properties
        /// <summary>
        /// The strict
        /// </summary>
        public static bool Strict = false;
        /// <summary>
        /// The tolerance
        /// </summary>
        public static double Tolerance = 10E-6;
        #endregion

        #region Public
        /// <summary>
        /// Signs the is negative.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SignIsNegative()
        {
            return (_sign.IsNegative());
        }

        /// <summary>
        /// Determines whether this instance has power.
        /// </summary>
        /// <returns><c>true</c> if this instance has power; otherwise, <c>false</c>.</returns>
        public bool HasPower()
        {
            return !(_power == null ||
                     (_power.Label() == "1"));
        }

        /// <summary>
        /// Gets the power.
        /// </summary>
        /// <returns>IBase.</returns>
        public IBase GetPower()
        {
            return (IBase) _power?.Clone();
        }

        /// <summary>
        /// Gets the sign.
        /// </summary>
        /// <returns>Sign.</returns>
        public Sign GetSign()
        {
            return new Sign(_sign.Label());
        }

        /// <summary>
        /// Powers the label.
        /// </summary>
        /// <returns>System.String.</returns>
        public string PowerLabel()
        {
            return HasPower() ? _power.Label() : string.Empty;
        }

        /// <summary>
        /// Flips the sign.
        /// </summary>
        public void FlipSign()
        {
            _sign.FlipSign();
        }

        /// <summary>
        /// Simplifies the power.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public virtual IBase SimplifyPower(bool isRecursive = false)
        {
            if (!HasPower()) return new PrimitiveUnit(string.Empty);
            string powerLabel = PowerLabel();
            if (powerLabel == "0" || powerLabel == "-0")
            {
                return new ProductQuotientSet(0);
            }

            Sign powerSign = GetPower().GetSign();
            if (string.IsNullOrEmpty(powerLabel) || powerLabel == "1")
            {
                return new ProductQuotientSet(powerSign * powerLabel);
            }
            if (isRecursive)
            {
                IBase newSimplifiedPower = GetPower().Simplify(true);
                powerLabel = newSimplifiedPower.Label();
            }
            else
            {
                powerLabel = GetPower().Label();
            }

            // If power is negative, make it positive for fraction simplification
            bool powerIsNegative = Query.IsNegative(powerLabel, 0);
            if (powerIsNegative && powerLabel.Length > 1)
            {
                powerLabel = powerLabel.Substring(1);
                if (powerLabel == "0") return new PrimitiveUnit("1");
            }
            powerLabel = Query.IsSymbolicFraction(powerLabel) ? Query.SimplifiedFraction(powerLabel) : powerLabel;
            IBase newPower = new ProductQuotientSet(powerSign * powerLabel);

            return newPower;
        }
        #endregion

        #region Methods: Abstract

        /// <summary>
        /// Determines whether this instance is integer.
        /// </summary>
        /// <returns><c>true</c> if this instance is integer; otherwise, <c>false</c>.</returns>
        public abstract bool IsInteger();

        /// <summary>
        /// Determines whether this instance is float.
        /// </summary>
        /// <returns><c>true</c> if this instance is float; otherwise, <c>false</c>.</returns>
        public abstract bool IsFloat();

        /// <summary>
        /// Determines whether this instance is fraction.
        /// </summary>
        /// <returns><c>true</c> if this instance is fraction; otherwise, <c>false</c>.</returns>
        public abstract bool IsFraction();

        /// <summary>
        /// Determines whether this instance is number.
        /// </summary>
        /// <returns><c>true</c> if this instance is number; otherwise, <c>false</c>.</returns>
        public abstract bool IsNumber();

        /// <summary>
        /// Determines whether this instance is symbolic.
        /// </summary>
        /// <returns><c>true</c> if this instance is symbolic; otherwise, <c>false</c>.</returns>
        public abstract bool IsSymbolic();

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
        public abstract bool IsEmpty();

        /// <summary>
        /// Values the is negative.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool ValueIsNegative();

        /// <summary>
        /// Ases the integer.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public abstract int AsInteger();

        /// <summary>
        /// Ases the float.
        /// </summary>
        /// <returns>System.Double.</returns>
        public abstract double AsFloat();

        /// <summary>
        /// Gets the base.
        /// </summary>
        /// <returns>IBase.</returns>
        public abstract IBase GetBase();

        /// <summary>
        /// Gets the absolute.
        /// </summary>
        /// <returns>IBase.</returns>
        public abstract IBase GetAbsolute();

        /// <summary>
        /// Labels this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public abstract string Label();

        /// <summary>
        /// Bases the label.
        /// </summary>
        /// <returns>System.String.</returns>
        public abstract string BaseLabel();

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public abstract object Clone();


        /// <summary>
        /// Calculates this instance.
        /// </summary>
        /// <returns>System.Double.</returns>
        public abstract double Calculate();

        /// <summary>
        /// Simplifies the specified is recursive.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public abstract IBase Simplify(bool isRecursive = false);

        /// <summary>
        /// Distributes the sign.
        /// </summary>
        /// <returns>IBase.</returns>
        public abstract IBase DistributeSign();

        /// <summary>
        /// Extracts the sign.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public abstract IBase ExtractSign(bool isRecursive = false);

        /// <summary>
        /// Distributes the sign from power.
        /// </summary>
        /// <returns>IBase.</returns>
        public abstract IBase DistributeSignFromPower();

        /// <summary>
        /// Extracts the sign from power.
        /// </summary>
        /// <returns>IBase.</returns>
        public abstract IBase ExtractSignFromPower();

        #endregion

        #region Private

        /// <summary>
        /// Adds the power.
        /// </summary>
        /// <param name="power">The power.</param>
        protected void addPower(IBase power)
        {
            if (power != null && !power.IsEmpty())
            {
                _power = (IBase)power.Clone();
            }
        }

        /// <summary>
        /// Labels the specified base item.
        /// </summary>
        /// <param name="baseItem">The base item.</param>
        /// <param name="addParenthesesToBase">if set to <c>true</c> [add parentheses to base].</param>
        /// <param name="addParenthesesToPower">if set to <c>true</c> [add parentheses to power].</param>
        /// <returns>System.String.</returns>
        protected string label(string baseItem, bool addParenthesesToBase = false, bool addParenthesesToPower = false)
        {
            addParenthesesToPower = addBracketsToPower(addParenthesesToPower);
            return baseLabel(baseItem, addParenthesesToBase) + powerLabel(addParenthesesToPower);
        }

        /// <summary>
        /// Adds the brackets to power.
        /// </summary>
        /// <param name="addBrackets">if set to <c>true</c> [add brackets].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool addBracketsToPower(bool addBrackets = false)
        {
            if (addBrackets) return true;

            if (_power is BaseSet powerUnits && powerUnits.Count > 1)
            {
                return true;
            }

            if (!HasPower()) return false;
           return _power.IsFraction() || _power.HasPower();
        }

        /// <summary>
        /// Bases the label.
        /// </summary>
        /// <param name="baseItem">The base item.</param>
        /// <param name="addParenthesesToBase">if set to <c>true</c> [add parentheses to base].</param>
        /// <returns>System.String.</returns>
        protected string baseLabel(string baseItem, bool addParenthesesToBase = false)
        {
            string baseLabel = addParenthesesToBase ? Group.AddOuterBrackets(baseItem) : baseItem;
            return _sign * baseLabel;
        }

        /// <summary>
        /// Powers the label.
        /// </summary>
        /// <param name="includeParentheses">if set to <c>true</c> [include parentheses].</param>
        /// <returns>System.String.</returns>
        protected string powerLabel(bool includeParentheses = false)
        {
            if (!HasPower()) return string.Empty;
            
            string totalPowerLabel;
            if (_power.HasPower())
            {   // Keep negative sign inside of any brackets
                totalPowerLabel = Group.AddOuterBrackets(_power.Label());
            }
            else if (includeParentheses)
            {   // Keep negative sign outside of any brackets
                Sign sign = _power.ValueIsNegative() ? new Sign(-1) : new Sign(1); 
                totalPowerLabel = sign * Group.AddOuterBrackets(_power.GetAbsolute().Label());
            }
            else
            {
                totalPowerLabel = _power.Label();
            }
                
            return Query.POWER + totalPowerLabel;

        }

        /// <summary>
        /// Calculates the specified base value.
        /// </summary>
        /// <param name="baseValue">The base value.</param>
        /// <returns>System.Double.</returns>
        protected double calculate(double baseValue)
        {
            if (_power == null)
            {
                return _sign * baseValue;
            }

            double powerValue = _power.Calculate();
            return (Math.Abs(baseValue) < Tolerance && Math.Abs(powerValue) < Tolerance) ?
                double.NaN :
                Math.Pow(_sign * baseValue, powerValue);
        }
        #endregion

        #region Public: Static
        /// <summary>
        /// Determines whether the specified value is empty.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns><c>true</c> if the specified value is empty; otherwise, <c>false</c>.</returns>
        public static bool IsEmpty(IBase value)
        {
            return (value == null || value.IsEmpty());
        }
        #endregion

        #region Private: Static Numeric Math Helper Functions

        /// <summary>
        /// Adds the type of the numeric as.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>PrimitiveUnit.</returns>
        protected static PrimitiveUnit addNumericAsType<T>(T value1, T value2) where T : IBase
        {
            return operationResultNumeric(value1, value2, addNumeric(value1, value2));
        }

        /// <summary>
        /// Subtracts the type of the numeric as.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>PrimitiveUnit.</returns>
        protected static PrimitiveUnit subtractNumericAsType<T>(T value1, T value2) where T : IBase
        {
            return operationResultNumeric(value1, value2, subtractNumeric(value1, value2));
        }

        /// <summary>
        /// Multiplies the type of the numeric as.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>PrimitiveUnit.</returns>
        protected static PrimitiveUnit multiplyNumericAsType<T>(T value1, T value2) where T : IBase
        {
            return operationResultNumeric(value1, value2, multiplyNumeric(value1, value2));
        }

        /// <summary>
        /// Divides the type of the numeric as.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>PrimitiveUnit.</returns>
        protected static PrimitiveUnit divideNumericAsType<T>(T value1, T value2) where T : IBase
        {
            return operationResultNumeric(value1, value2, divideNumeric(value1, value2));
        }



        /// <summary>
        /// Adds the numeric.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>System.Double.</returns>
        protected static double addNumeric<T>(T value1, T value2) where T : IBase
        {
            return value1.Calculate() + value2.Calculate();
        }

        /// <summary>
        /// Subtracts the numeric.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>System.Double.</returns>
        protected static double subtractNumeric<T>(T value1, T value2) where T : IBase
        {
            return value1.Calculate() - value2.Calculate();
        }

        /// <summary>
        /// Multiplies the numeric.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>System.Double.</returns>
        protected static double multiplyNumeric<T>(T value1, T value2) where T : IBase
        {
            return value1.Calculate() * value2.Calculate();
        }

        /// <summary>
        /// Divides the numeric.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>System.Double.</returns>
        protected static double divideNumeric<T>(T value1, T value2) where T : IBase
        {
            double numerator = value1.Calculate();
            double denominator = value2.Calculate();
            double doubleQuantity;
            if (Math.Abs(numerator) < Tolerance && Math.Abs(denominator) < Tolerance)
            {
                doubleQuantity = double.NaN;
            }
            else if (Math.Abs(numerator) > Tolerance && Math.Abs(denominator) < Tolerance)
            {
                doubleQuantity = value1.SignIsNegative() ? double.NegativeInfinity : double.PositiveInfinity;
            }
            else
            {
                doubleQuantity = numerator / denominator;
            }
            return doubleQuantity;
        }

        /// <summary>
        /// Operations the result numeric.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <param name="doubleQuantity">The double quantity.</param>
        /// <returns>PrimitiveUnit.</returns>
        protected static PrimitiveUnit operationResultNumeric<T>(T value1, T value2, double doubleQuantity) where T : IBase
        {
            if (!Query.IsInteger(doubleQuantity.ToString(CultureInfo.InvariantCulture))) return new PrimitiveUnit(doubleQuantity);

            int integerQuantity = (int)Math.Round(doubleQuantity);
            return new PrimitiveUnit(integerQuantity);
        }

        #endregion

        #region Private: Static Symbolic Math Helper Functions
        /// <summary>
        /// Adds the symbolic.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>SumDifferenceSet.</returns>
        protected static SumDifferenceSet addSymbolic(IBase value1, IBase value2)
        {
            SumDifferenceSet set;
            if (value1 is SumDifferenceSet differenceSet)
            {
                set = differenceSet.CloneSet();
            }
            else
            {
                set = new SumDifferenceSet(value1);
            }
            set.SumItem(value2);
            return set;
        }

        /// <summary>
        /// Subtracts the symbolic.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>SumDifferenceSet.</returns>
        protected static SumDifferenceSet subtractSymbolic(IBase value1, IBase value2)
        {
            SumDifferenceSet set;
            if (value1 is SumDifferenceSet differenceSet)
            {
                set = differenceSet.CloneSet();
            }
            else
            {
                set = new SumDifferenceSet(value1);
            }
            set.SubtractItem(value2);
            return set;
        }

        /// <summary>
        /// Multiplies the symbolic.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>ProductQuotientSet.</returns>
        protected static ProductQuotientSet multiplySymbolic(IBase value1, IBase value2)
        {
            ProductQuotientSet set;
            switch (value1)
            {
                case ProductQuotientSet set1 when set1.Count > 1 && value2 is ProductQuotientSet set2 && set2.Count > 1:
                    return new ProductQuotientSet(
                        Group.AddOuterBrackets(value1.Label()) + Query.MULTIPLY + Group.AddOuterBrackets(value2.Label()));
                case ProductQuotientSet differenceSet:
                    set = differenceSet.CloneSet();
                    break;
                default:
                    set = new ProductQuotientSet(value1);
                    break;
            }

            set.MultiplyItem(value2);
            return set;
        }

        /// <summary>
        /// Divides the symbolic.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>ProductQuotientSet.</returns>
        protected static ProductQuotientSet divideSymbolic(IBase value1, IBase value2)
        {
            ProductQuotientSet set;
            switch (value1)
            {
                case ProductQuotientSet set1 when set1.Count > 1 && value2 is ProductQuotientSet set2 && set2.Count > 1:
                    return new ProductQuotientSet(
                        Group.AddOuterBrackets(value1.Label()) + Query.DIVIDE + Group.AddOuterBrackets(value2.Label()));
                case ProductQuotientSet productQuotientSet:
                    set = productQuotientSet.CloneSet();
                    break;
                default:
                    set = new ProductQuotientSet(value1);
                    break;
            }

            set.DivideItem(value2);
            return set;
        }
        #endregion

        // TODO: See about usage of commented methods. They could be useful but currently are not being used.
        #region Private: Static Comparison Query Helper Functions

        //protected static bool bothAreEmpty(IBase value1, IBase value2)
        //{
        //    return IsEmpty(value1) && IsEmpty(value2);
        //}

        /// <summary>
        /// Bothes the are numeric.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected static bool bothAreNumeric(IBase value1, IBase value2)
        {
            return value1.IsNumber() && value2.IsNumber();
        }

        //protected static bool bothAreInteger(IBase value1, IBase value2)
        //{
        //    return value1.IsInteger() && value2.IsInteger();
        //}

        //protected static bool bothAreSymbolic(IBase value1, IBase value2)
        //{
        //    return value1.IsSymbolic() && value2.IsSymbolic();
        //}

        //protected static bool bothHaveSameVariables(IBase value1, IBase value2)
        //{
        //    return (bothAreSymbolic(value1, value2) &&
        //            value1.Label() == value2.Label());
        //}

        ///// <summary>
        ///// True: Both bases are the same, including sign.
        ///// </summary>
        ///// <param name="value1"></param>
        ///// <param name="value2"></param>
        ///// <returns></returns>
        //protected static bool bothBasesAreSame(IBase value1, IBase value2)
        //{
        //    return value1.GetBase().Label() == value2.GetBase().Label();
        //}

        //// TODO: Implement checks & handling for this
        ///// <summary>
        ///// True: Both bases are the same, but of opposing sign.
        ///// </summary>
        ///// <param name="value1"></param>
        ///// <param name="value2"></param>
        ///// <returns></returns>
        //protected static bool bothBasesAreSameOppositeSign(IBase value1, IBase value2)
        //{
        //    string value1Label = value1.GetBase().Label();
        //    string value2Label = value2.GetBase().Label();
        //    return ((value1Label != value2Label) && 
        //            (Sign.RemoveNegativeSign(value1Label) == Sign.RemoveNegativeSign(value2Label)));
        //}

        ///// <summary>
        ///// True: Both exponents are the same, including sign.
        ///// </summary>
        ///// <param name="value1"></param>
        ///// <param name="value2"></param>
        ///// <returns></returns>
        //protected static bool bothExponentsAreSame(IBase value1, IBase value2)
        //{
        //    if (IsEmpty(value1) || IsEmpty(value2)) return false;

        //    IBase power1 = value1.GetPower();
        //    IBase power2 = value2.GetPower();
        //    if (bothAreEmpty(power1, power2)) return true;
        //    if (IsEmpty(power1) || IsEmpty(power2)) return false;
        //    return power1.Label() == power2.Label();
        //}

        //// TODO: Implement checks & handling for this
        ///// <summary>
        ///// True: Both exponents are the same, but of opposing sign.
        ///// </summary>
        ///// <param name="value1"></param>
        ///// <param name="value2"></param>
        ///// <returns></returns>
        //protected static bool bothExponentsAreSameOppositeSign(IBase value1, IBase value2)
        //{
        //    if (IsEmpty(value1) || IsEmpty(value2)) return false;

        //    IBase power1 = value1.GetPower();
        //    IBase power2 = value2.GetPower();
        //    if (bothAreEmpty(power1, power2)) return false;
        //    if (IsEmpty(power1) || IsEmpty(power2)) return false;
        //    string power1Label = power1.Label();
        //    string power2Label = power2.Label();
        //    return ((power1Label != power2Label) &&
        //            (Sign.RemoveNegativeSign(power1Label) == Sign.RemoveNegativeSign(power2Label)));
        //}

        //protected static bool bothExponentsAreZero(IBase value1, IBase value2)
        //{
        //    return exponentIsZero(value1) && exponentIsZero(value2);
        //}

        //protected static bool bothExponentsAreOne(IBase value1, IBase value2)
        //{
        //    return exponentIsOne(value1) && exponentIsOne(value2);
        //}


        /// <summary>
        /// Checks the null.
        /// </summary>
        /// <param name="operand1">The operand1.</param>
        /// <param name="operand2">The operand2.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected static void checkNull(IBase operand1, IBase operand2)
        {
            if (IsEmpty(operand1) || IsEmpty(operand2))
            {
                throw new ArgumentNullException(operandArgumentNullExceptionMessage(operand1, operand2));
            }
        }

        /// <summary>
        /// Checks the null.
        /// </summary>
        /// <param name="operand1">The operand1.</param>
        /// <param name="operand2">The operand2.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PrimitiveUnit"></exception>
        protected static void checkNull(IBase operand1, string operand2)
        {
            if (IsEmpty(operand1) || string.IsNullOrWhiteSpace(operand2))
            {
                throw new ArgumentNullException(operandArgumentNullExceptionMessage(operand1, new PrimitiveUnit(operand2)));
            }
        }

        /// <summary>
        /// Checks the null.
        /// </summary>
        /// <param name="operand">The operand.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected static void checkNull(IBase operand)
        {
            if (IsEmpty(operand))
            {
                throw new ArgumentNullException(operandArgumentNullExceptionMessage(operand));
            }
        }

        /// <summary>
        /// Operands the argument null exception message.
        /// </summary>
        /// <param name="operand1">The operand1.</param>
        /// <param name="operand2">The operand2.</param>
        /// <returns>System.String.</returns>
        protected static string operandArgumentNullExceptionMessage(IBase operand1, IBase operand2 = null)
        {
            return $"Operand is either null or empty: {operand1?.Label()}, {operand2?.Label()} ";
        }

        /// <summary>
        /// Values the symbolic exception message.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>System.String.</returns>
        protected static string valueSymbolicExceptionMessage(IBase item)
        {
            return $"Cannot calculate a symbolic value. It must be numeric: {item.Label()}";
        }
        #endregion

        #region Equals
        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(BaseUnit value1, BaseUnit value2)
        {
            return value1?.Equals(value2) ?? ReferenceEquals(value2, null);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(BaseUnit value1, BaseUnit value2)
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
            if (!(obj is BaseUnit)) return false;
            BaseUnit other = (BaseUnit)obj;
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
        public bool Equals(BaseUnit other)
        {
            if (other == null) return false;
            return (_sign != null && _sign.Equals(other._sign)) &&
                   ((_power == null && other._power == null) ||
                    (_power != null && _power.Equals(other._power)));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((_sign != null ? _sign.GetHashCode() : 0) * 397) ^ (_power != null ? _power.GetHashCode() : 0);
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Sums the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>SumDifferenceSet.</returns>
        public SumDifferenceSet Sum(IBase value)
        {
            return new SumDifferenceSet(this) + value;
        }

        /// <summary>
        /// Subtracts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>SumDifferenceSet.</returns>
        public SumDifferenceSet Subtract(IBase value)
        {
            return new SumDifferenceSet(this) - value;
        }

        /// <summary>
        /// Multiplies the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ProductQuotientSet.</returns>
        public ProductQuotientSet Multiply(IBase value)
        {
            return new ProductQuotientSet(this) * value;
        }

        /// <summary>
        /// Divides the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ProductQuotientSet.</returns>
        public ProductQuotientSet Divide(IBase value)
        {
            return new ProductQuotientSet(this) / value;
        }
        #endregion
        
    }
}
