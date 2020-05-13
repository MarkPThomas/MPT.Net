// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-04-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-29-2018
// ***********************************************************************
// <copyright file="BaseSet.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MPT.SymbolicMath
{
    /// <summary>
    /// Class BaseSet.
    /// </summary>
    /// <seealso cref="MPT.SymbolicMath.BaseUnit" />
    /// <seealso cref="MPT.SymbolicMath.IBaseSet" />
    public abstract class BaseSet : BaseUnit, IBaseSet, IEnumerable
    {
        #region Fields             
        /// <summary>
        /// The unit object and corresponding operator.
        /// </summary>
        protected List<UnitOperatorPair> _unitOperatorPair = new List<UnitOperatorPair>();
        #endregion

        #region Properties
        /// <inheritdoc />
        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>The count.</value>
        public int Count => _unitOperatorPair.Count;
        #endregion

        #region Methods: Public
        /// <summary>
        /// Determines whether this instance is integer.
        /// </summary>
        /// <returns><c>true</c> if this instance is integer; otherwise, <c>false</c>.</returns>
        public override bool IsInteger()
        {
            foreach (UnitOperatorPair item in _unitOperatorPair)
            {
                if (!item.Unit.IsInteger()) return false;
            }
            return ((!HasPower() || _power.IsInteger()) && _unitOperatorPair.All(o => o.Operator != Query.DIVIDE.ToString()));
        }

        /// <summary>
        /// Ases the integer.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public override int AsInteger()
        {
            return (int)Math.Round(AsFloat());
        }

        /// <summary>
        /// Determines whether this instance is float.
        /// </summary>
        /// <returns><c>true</c> if this instance is float; otherwise, <c>false</c>.</returns>
        public override bool IsFloat()
        {
            bool isFloat = false;
            foreach (UnitOperatorPair item in _unitOperatorPair)
            {
                if (!item.Unit.IsNumber()) return false;
                if (!isFloat && item.Unit.IsFloat())
                {
                    isFloat = true;
                }
            }

            // Rough solution to fractions
            if (!isFloat)
            {
                isFloat = _unitOperatorPair.Any(x => x.OperatorEquals(Query.DIVIDE));
            }
            if (isFloat) return (!HasPower() || (HasPower() && !_power.IsSymbolic()));

            // TODO: Precise solution to fractions. May incorporate later.
            // Precise solution to fractions
            //double value = Calculate(); // Not Implemented yet
            //if (Math.Abs(value) > Tolerance && !isInteger(value.ToString(CultureInfo.InvariantCulture)))
            //{
            //    return true;
            //}

            return (HasPower() && !_power.IsSymbolic() && !_power.IsInteger());
        }

        /// <summary>
        /// Ases the float.
        /// </summary>
        /// <returns>System.Double.</returns>
        public override double AsFloat()
        {
            return (IsSymbolic() || !IsNumber()) ? 0 : _sign * Calculate();
        }

        /// <summary>
        /// Determines whether this instance is fraction.
        /// </summary>
        /// <returns><c>true</c> if this instance is fraction; otherwise, <c>false</c>.</returns>
        public override bool IsFraction()
        {
            return (((isSingleUnit() 
                      && _unitOperatorPair[0].Unit.IsFraction()) 
                     || _unitOperatorPair.Any(x => x.OperatorEquals(Query.DIVIDE))) 
                    && (!HasPower() || PowerLabel() == "1"));
        }

        /// <summary>
        /// Determines whether this instance is number.
        /// </summary>
        /// <returns><c>true</c> if this instance is number; otherwise, <c>false</c>.</returns>
        public override bool IsNumber()
        {
            foreach (UnitOperatorPair item in _unitOperatorPair)
            {
                if (!item.Unit.IsNumber()) return false;
            }
            return (!HasPower() || _power.IsNumber());
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
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns><c>true</c> if this instance is empty; otherwise, <c>false</c>.</returns>
        public override bool IsEmpty()
        {
            if (Count == 0)
            {
                return true;
            }

            foreach (UnitOperatorPair item in _unitOperatorPair)
            {
                if (!item.Unit.IsEmpty())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Bases the label.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string BaseLabel()
        {
            string label = rawBaseLabel();
            return baseLabel(label);
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

            double baseValue = 0;
            for (int i = 0; i < Count; i++)
            {
                double unitValue = _unitOperatorPair[i].Unit.Calculate();
                string operand = _unitOperatorPair[i].Operator;
                switch (operand)
                {
                    case "+":
                        baseValue += unitValue;
                        break;
                    case "-":
                        baseValue -= unitValue;
                        break;
                    case "*":
                        baseValue *= unitValue;
                        break;
                    case "/":
                        baseValue /= unitValue;
                        break;
                    default:
                        baseValue = unitValue;
                        break;
                }
            }
            
            return calculate(baseValue);
        }



        #endregion

        #region Methods: Abstract
        /// <summary>
        /// Gets the base.
        /// </summary>
        /// <returns>IBase.</returns>
        public abstract override IBase GetBase();

        /// <summary>
        /// Appends the item to group.
        /// </summary>
        /// <param name="lastOperand">The last operand.</param>
        /// <param name="newValuePrimitive">The new value primitive.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool AppendItemToGroup(char lastOperand, IBase newValuePrimitive);

        /// <summary>
        /// Factories the specified new value primitive.
        /// </summary>
        /// <param name="newValuePrimitive">The new value primitive.</param>
        /// <returns>IBaseSet.</returns>
        public abstract IBaseSet Factory(IBase newValuePrimitive);

        /// <summary>
        /// Simplifies the units of one.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        public abstract IBaseSet SimplifyUnitsOfOne(bool isRecursive = true);

        /// <summary>
        /// Simplifies the fractional.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public abstract IBase SimplifyFractional(bool isRecursive = false);

        /// <summary>
        /// Simplifies the variables.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        public abstract IBaseSet SimplifyVariables(bool isRecursive = false);

        /// <summary>
        /// Simplifies the operand groups.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        public abstract IBaseSet SimplifyOperandGroups(bool isRecursive = false);

        /// <summary>
        /// Simplifies the fractional.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newSetGeneric">The new set generic.</param>
        /// <returns>ProductQuotientSet.</returns>
        protected abstract ProductQuotientSet simplifyFractional<T>(T newSetGeneric) where T : BaseSet;
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
        #endregion

        #region Equals
        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(BaseSet value1, BaseSet value2)
        {
            return value1?.Equals(value2) ?? ReferenceEquals(value2, null);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="value1">The value1.</param>
        /// <param name="value2">The value2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(BaseSet value1, BaseSet value2)
        {
            return !value1?.Equals(value2) ?? !ReferenceEquals(value2, null);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is BaseSet)) return false;
            BaseSet other = (BaseSet)obj;
            if (IsEmpty() && other.IsEmpty())
            {
                return true;
            }

            return Equals(other);
        }

        public bool Equals(BaseSet other)
        {   // Equality is ignoring possible simplifications
            if (other == null) return false;
            if (Count != other.Count) return false;
            if (!base.Equals(other)) return false;
            switch (Count)
            {
                case 0 when other.Count == 0:
                    return true;
                case 1 when other.Count == 1:
                    return _unitOperatorPair[0].Unit.Equals(other._unitOperatorPair[0].Unit);
                default:
                    return baseUnitsAreEqual(other);
            }
        }

        protected bool baseUnitsAreEqual(BaseSet other)
        {
            // Check that operators contain equal numbers of +, -, or *, /
            if ((operatorsCount(Query.ADD) != other.operatorsCount(Query.ADD)) ||
                (operatorsCount(Query.SUBTRACT) != other.operatorsCount(Query.SUBTRACT)) ||
                (operatorsCount(Query.MULTIPLY) != other.operatorsCount(Query.MULTIPLY)) ||
                (operatorsCount(Query.DIVIDE) != other.operatorsCount(Query.DIVIDE))) return false;

            // Check each unit+operator pair
            foreach (UnitOperatorPair unitOperator in _unitOperatorPair)
            {
                if (!isUnitMatching(unitOperator.Unit, unitOperator.Operator, other)) return false;
            }
            return true;
        }

        protected bool isUnitMatching(IBase unit, string targetOperator, BaseSet other)
        {
            foreach (UnitOperatorPair otherUnitOperator in other._unitOperatorPair)
            {
                if (targetOperator == otherUnitOperator.Operator
                    && unit.Equals(otherUnitOperator.Unit)) return true;
            }

            return false;
        }

        protected int operatorsCount(char targetOperator)
        {
            return _unitOperatorPair.Count(x => x.Operator == targetOperator.ToString());
        }



        public override int GetHashCode()
        {  
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ getUnitOperandHashcodes();
                return hashCode;
            }
        }

        protected int getUnitOperandHashcodes()
        {
            unchecked
            {
                if (_unitOperatorPair == null || _unitOperatorPair.Count == 0) return 0;
                var hashCode = 1;
                foreach (UnitOperatorPair item in _unitOperatorPair)
                {
                    hashCode = (hashCode * 397) ^ item.Unit.GetHashCode();
                    hashCode = (hashCode * 397) ^ item.Operator.GetHashCode();
                }
                return hashCode;
            }
        }
        #endregion

        #region Private

        protected bool isSingleUnit()
        {
            return Count == 1;
        }

        protected IBase getUnit(int index, bool getAbsolute = false)
        {
            IBase unit = _unitOperatorPair[index].Unit;
            if (getAbsolute)
            {
                unit = unit.GetAbsolute();
            }

            return unit;
        }

        protected IBaseSet simplifyFractionalSet<T>(bool isRecursive = false) where T : BaseSet, new()
        {
            if (IsEmpty()) return new T();
            T newSet = (T)Clone();

            if (!isRecursive) return simplifyFractional(newSet);

            for (int i = 0; i < newSet.Count; i++)
            {
                if (newSet._unitOperatorPair[i].Unit is IBaseSet baseSet)
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateUnit(baseSet.SimplifyFractional(isRecursive: true));
                }
            }
            return simplifyFractional(newSet);
        }

        protected static ProductQuotientSet fractionSet(BaseSet newSet, int i)
        {
            IBase unit = newSet._unitOperatorPair[i].Unit;
            if (unit is PrimitiveUnit newSubUnit &&
                newSubUnit.IsFraction())
            {
                return new ProductQuotientSet(newSubUnit.Label());
            }

            if (unit is ProductQuotientSet productQuotientSet &&
                (0 < productQuotientSet.Count && productQuotientSet.Count <= 2))
            {
                return productQuotientSet;
            }

            if (unit is SumDifferenceSet sumDifferenceSet &&
                (0 < sumDifferenceSet.Count && sumDifferenceSet.Count <= 2))
            {
                return new ProductQuotientSet(sumDifferenceSet.Label());
            }

            return new ProductQuotientSet(unit.Label() + "/1");
        }

        protected static ProductQuotientSet unitSet(BaseSet newSet, int i)
        {
            if (i > newSet.Count - 1)
            {
                return new ProductQuotientSet(1);
            }

            IBase unit = newSet._unitOperatorPair[i].Unit;
            if (unit is PrimitiveUnit newSubUnit &&
                newSubUnit.IsFraction())
            {
                return new ProductQuotientSet(newSubUnit.Label());
            }

            if (unit is ProductQuotientSet productQuotientSet &&
                (0 < productQuotientSet.Count && productQuotientSet.Count <= 2))
            {
                return productQuotientSet;
            }

            if (unit is SumDifferenceSet sumDifferenceSet &&
                (0 < sumDifferenceSet.Count && sumDifferenceSet.Count <= 2))
            {
                return new ProductQuotientSet(sumDifferenceSet.Label());
            }

            return new ProductQuotientSet(unit.Label());
        }

        protected static T simplifyNumeric<T>(IBaseSet set) where T : IBaseSet, new()
        {
            if (!set.IsNumber()) return (T)set;

            IBase newValuePrimitive = new PrimitiveUnit(set.Calculate());
            set = new T();
            set = set.Factory(newValuePrimitive);

            return (T)set;
        }
        #endregion

        #region Private: Add/Subtract

        protected bool currentOperatorIsDifference(int i)
        {
            return _unitOperatorPair[i].OperatorEquals(Query.SUBTRACT);
        }

        protected bool currentOperatorIsSum(int i)
        {
            return _unitOperatorPair[i].OperatorEquals(Query.ADD);
        }

        protected bool currentOperatorIsSumDifference(int i)
        {
            return (_unitOperatorPair[i].OperatorEquals(Query.SUBTRACT) ||
                    _unitOperatorPair[i].OperatorEquals(Query.ADD));
        }

        protected bool priorOperatorIsDifference(int i)
        {
            return (0 <= i - 1 && _unitOperatorPair[i - 1].OperatorEquals(Query.SUBTRACT));
        }

        protected bool priorOperatorIsSum(int i)
        {
            return (0 <= i - 1 && _unitOperatorPair[i - 1].OperatorEquals(Query.ADD));
        }

        protected bool priorOperatorIsSumDifference(int i)
        {
            return (0 <= i - 1 &&
                    (_unitOperatorPair[i - 1].OperatorEquals(Query.SUBTRACT) ||
                     _unitOperatorPair[i - 1].OperatorEquals(Query.ADD)));
        }
        #endregion

        #region Private: Units/Labels 

        protected string rawBaseLabel()
        {
            string label = string.Empty;
            for (int i = 0; i < Count; i++)
            {
                string unitsLabel = groupUnitsLabel(_unitOperatorPair[i].Unit.Label(), i);
                if (i != 0)
                {
                    label += _unitOperatorPair[i].Operator;
                }
                label += unitsLabel;
            }

            return label;
        }

        protected string groupUnitsLabel(string unitsLabel, int i)
        {
            bool labelIsNegative = (unitsLabel.Length > 1 && unitsLabel[0] == Sign.NEGATIVE);
            if (addGroupingInsideSign(unitsLabel, labelIsNegative, i))
            {
                unitsLabel = unitsLabel.Substring(1);
                unitsLabel = Group.AddOuterBrackets(unitsLabel);
                return Sign.NEGATIVE + unitsLabel;
            }

            return addGrouping(unitsLabel, labelIsNegative) ?
                Group.AddOuterBrackets(unitsLabel) :
                unitsLabel;
        }
        /// <summary>
        /// Adds the grouping for any case of (a+b), (a-b), (a*b), (a/b), etc.
        /// </summary>
        /// <param name="unitsLabel">The units label.</param>
        /// <param name="labelIsNegative">The label is negative.</param>
        /// <returns>System.Boolean.</returns>
        protected bool addGrouping(string unitsLabel, bool labelIsNegative)
        {
            return (Count > 1
                    && (unitsLabel.Contains(Query.ADD)
                        || (unitsLabel.Length > 1
                            && unitsLabel.Substring(1).Contains(Query.SUBTRACT)
                            && !(unitsLabel.Length > 4 // Avoid case of -(a^-b) becoming (-(a^-b))
                                 && labelIsNegative
                                 && Group.OpenGroupTypes.Contains(unitsLabel[1])
                                 && unitsLabel.Substring(1).Contains(Query.POWER.ToString() + Sign.NEGATIVE)))
                        || unitsLabel.Contains(Query.MULTIPLY)
                        || unitsLabel.Contains(Query.DIVIDE)));
        }

        /// <summary>
        /// Adds the grouping inside the sign for any case of -(a^m)+b, -(a*b)+c, -(a/b)+c, etc.
        /// </summary>
        /// <param name="unitsLabel">The units label.</param>
        /// <param name="labelIsNegative">The label is negative.</param>
        /// <param name="i">The unit index.</param>
        /// <returns>System.Boolean.</returns>
        protected bool addGroupingInsideSign(string unitsLabel, bool labelIsNegative, int i)
        {
            return (i == 0
                    && Count > 1
                    && labelIsNegative
                    && addGrouping(unitsLabel, labelIsNegative: true)
                    && Query.AddSubtractTypes.Contains(_unitOperatorPair[i + 1].Operator[0])
                    && (_unitOperatorPair[i].Unit is PrimitiveUnit
                        || _unitOperatorPair[i].Unit is ProductQuotientSet));
        }

        #endregion

        #region Private: Initialize & Parse
        protected void initialize(string initialValue, IBase power, bool isNegative)
        {
            if (string.IsNullOrWhiteSpace(initialValue)) return;

            initialValue = preParse(initialValue, ref power, ref isNegative);
            IBase initialValueObject = parseInitialValue(initialValue);

            if (((this is ProductQuotientSet 
                  && initialValueObject is ProductQuotientSet) 
                 || (this is SumDifferenceSet 
                     && initialValueObject is SumDifferenceSet)) 
                && (power == null && !isNegative))
            { // Merge identical set types
                BaseSet newInitialValueObject = initialValueObject as BaseSet;
                for (int i = 0; i < newInitialValueObject.Count; i++)
                {
                    addUnitOperatorPair(newInitialValueObject._unitOperatorPair[i].Unit,
                                        newInitialValueObject._unitOperatorPair[i].Operator);
                }
                addPower(newInitialValueObject._power);
                if (newInitialValueObject._sign.IsNegative()) _sign = new Sign(-1);
            }
            else
            {   // Add set or unit type
                initialize(initialValueObject, power, isNegative);
            }
        }

        protected void initialize(IBase initialValue, IBase power, bool isNegative)
        {
            if (initialValue != null)
            {
                addUnitOperatorPair(initialValue, string.Empty);
            }
            addPower(power);
            if (isNegative) _sign = new Sign(-1);
        }

        protected void mergeUnits(bool isRecursive = true)
        {
            for (int i = 0; i < Count; i++)
            {
                if (!(_unitOperatorPair[i].Unit is BaseSet baseSetUnit)) continue;

                baseSetUnit.mergeUnits(isRecursive);
                if (baseSetUnit.Count == 1
                    && !baseSetUnit.HasPower()
                    && !baseSetUnit.SignIsNegative()
                    && baseSetUnit._unitOperatorPair[0].Unit is PrimitiveUnit primitiveUnit)
                {
                    _unitOperatorPair[i] = _unitOperatorPair[i].UpdateUnit(primitiveUnit);
                }
            }

            finalizeUnits();
        }

        protected void finalizeUnits()
        {
            if (Count != 1) return;
            IBase initialValueObject = _unitOperatorPair[0].Unit;

            if (((!(this is ProductQuotientSet) || !(initialValueObject is ProductQuotientSet)) &&
                 (!(this is SumDifferenceSet) || !(initialValueObject is SumDifferenceSet))) ||
                HasPower() && initialValueObject.HasPower()) return; 
            
            // Merge identical set types
            BaseSet newInitialValueObject = initialValueObject as BaseSet;
            _unitOperatorPair.Clear();
            for (int i = 0; i < newInitialValueObject.Count; i++)
            {
                addUnitOperatorPair(newInitialValueObject._unitOperatorPair[i].Unit,
                                    newInitialValueObject._unitOperatorPair[i].Operator);
            }
            _sign = _sign * newInitialValueObject._sign;

            if (!HasPower()) addPower(newInitialValueObject._power);
        }


        protected void addUnitOperatorPair(IBase unit, string unitOperator)
        {
            addUnitOperatorPair(unit, unitOperator, _unitOperatorPair);
        }

        protected bool appendItem(IBase item, AppendUnitDelegate appendUnit)
        {
            if (item.IsEmpty()) return false;
            appendUnit(_unitOperatorPair, item);
            return true;
        }

        protected delegate void AppendUnitDelegate(List<UnitOperatorPair> unitOperatorPairs, IBase item);
        protected static void appendUnitOperatorPair(List<UnitOperatorPair> unitOperatorPairs, IBase item, char newOperator)
        {
            IBase unitToAdd = (IBase) item.Clone();
            string operatorToAdd = unitOperatorPairs.Count == 0 ? string.Empty : newOperator.ToString();
            addUnitOperatorPair(unitToAdd, operatorToAdd, unitOperatorPairs);
        }

        protected static void addUnitOperatorPair(IBase unit, string unitOperator, List<UnitOperatorPair> unitOperatorPairs)
        {
            unitOperatorPairs.Add(new UnitOperatorPair(unit, unitOperator));
        }



        private static string preParse(string value, ref IBase power, ref bool isNegative)
        {
            string groupPower = string.Empty;
            bool groupIsNegative = false;
            int closingBaseGroupIndex = -1;

            if (!hasGroupPower(value, ref groupPower, ref groupIsNegative, ref closingBaseGroupIndex)) return value;
            
            power = setPower(power, groupPower);

            string newValue = value;
            updateValuesForBracketGroup(
                groupIsNegative,
                ref isNegative,
                ref newValue,
                ref closingBaseGroupIndex);

            return updateValueForClosingGroup(newValue, ref closingBaseGroupIndex);
        }


        private static bool hasGroupPower(
            string value,
            ref string groupPower,
            ref bool groupIsNegative,
            ref int closingBaseGroupIndex)
        {
            // Check for grouping based on brackets
            int parenthesesCount = 0;
            int parenthesesPowerCount = 0;
            bool isInBase = false;
            bool parenthesesBalanced = true;
            bool baseClosed = false;
            bool parenthesesPowerBalanced = true;
            bool powerClosed = false;
            bool isInPower = false;

            foreach (char character in value)
            {
                if (!baseClosed)
                {
                    closingBaseGroupIndex++;
                }

                if (!isInBase && character == Query.SUBTRACT)
                {
                    groupIsNegative = true;
                    continue;
                }

                if (!isInPower && !baseClosed)
                {
                    if (Group.OpenGroupTypes.Contains(character))
                    {
                        parenthesesCount++;
                        parenthesesBalanced = false;
                        isInBase = true;
                        continue;
                    }

                    if (Group.CloseGroupTypes.Contains(character))
                    {
                        if (parenthesesCount == 1)
                        {
                            parenthesesBalanced = true;
                            baseClosed = true;
                        }
                        parenthesesCount--;
                        continue;
                    }
                }
                else if (character == Query.POWER)
                {
                    isInPower = true;
                    continue;
                }
                else if (isInPower)
                {
                    if (Group.OpenGroupTypes.Contains(character))
                    {
                        groupPower += character;
                        parenthesesPowerCount++;
                        parenthesesPowerBalanced = false;
                        continue;
                    }

                    if (Group.CloseGroupTypes.Contains(character))
                    {
                        groupPower += character;
                        if (parenthesesPowerCount == 1)
                        {
                            parenthesesPowerBalanced = true;
                            isInPower = false;
                            powerClosed = true;
                        }
                        parenthesesPowerCount--;
                        continue;
                    }
                }

                if ((powerClosed && parenthesesPowerBalanced && !isInPower) ||
                    (parenthesesBalanced && isInPower && Query.OperatorAllTypes.Contains(character)) ||
                    (parenthesesBalanced && !isInPower))
                {
                    //return value;
                    return false;
                }

                if (isInPower)
                {
                    groupPower += character;
                }
            }

            return true;
        }

        private static void updateValuesForBracketGroup(
            bool groupIsNegative,
            ref bool isNegative, 
            ref string newValue, 
            ref int closingBaseGroupIndex)
        {
            if (newValue[0] != Query.SUBTRACT) return;

            isNegative = groupIsNegative;
            newValue = newValue.Substring(1);
            closingBaseGroupIndex--;
        }

        private static string updateValueForClosingGroup(
            string newValue, 
            ref int closingBaseGroupIndex)
        {   // Trim brackets and outside values
            if (Group.OpenGroupTypes.Contains(newValue[0]))
            {   // Remove opening bracket
                closingBaseGroupIndex--;
                newValue = newValue.Substring(1);
            }

            if (closingBaseGroupIndex > 0 && 
                Group.CloseGroupTypes.Contains(newValue[closingBaseGroupIndex]))
            {   // Remove closing bracket & values beyond
                return newValue.Substring(0, closingBaseGroupIndex);
            }

            return newValue;
        }

        private static IBase setPower(IBase power, string groupPower)
        {
            return (!string.IsNullOrEmpty(groupPower)) ? 
                parseToObject(groupPower) :  
                power;
        }

        private static IBase parseInitialValue(string value)
        {
            value = value.Trim();
            return parseToObject(value);
        }

        private static IBase parseToObject(string value)
        {
            int indexOfAddSubtract = Query.IndexOfNextOperator(value, Query.AddSubtractTypes.ToArray());
            int indexOfProductQuotient = Query.IndexOfNextOperator(value, Query.MultiplyDivideTypes.ToArray());

            if (((indexOfAddSubtract == -1 && indexOfProductQuotient == -1)) && 
                PrimitiveUnit.HasValidPowers(value))
            {    // No operators remain
                return new PrimitiveUnit(value);
            }

            if (0 < indexOfAddSubtract && (indexOfAddSubtract < indexOfProductQuotient || indexOfProductQuotient == -1))
            {   // Add/Subtract occur first, outside of groups
                return parseAsSumDifference(value);
            }


            if ((0 < indexOfProductQuotient && (indexOfProductQuotient < indexOfAddSubtract || indexOfAddSubtract == -1)) ||
                !PrimitiveUnit.HasValidPowers(value))
            {   // Multiply/divide occur first, outside of groups, or the powers are not valid for direct parsing in a primitive unit
                return parseAsProductQuotient(value);
            }

            return new PrimitiveUnit(value);
        }


        private static IBase parseAsSumDifference(string value)
        {
            string newValue = string.Empty;
            char lastOperand = Query.EMPTY;
            SumDifferenceSet sumDifferenceSet = null;

            aggregateAndCombineItems(value, ref sumDifferenceSet, ref lastOperand, ref newValue);

            return finalizeSumDifferenceSet(sumDifferenceSet, newValue, lastOperand);
        }

        private static void aggregateAndCombineItems(
            string value,
            ref SumDifferenceSet sumDifferenceSet,
            ref char lastOperand,
            ref string newValue)
        {
            int continueIndex = -1;
            for (int i = 0; i < value.Length; i++)
            {
                if (i < continueIndex) continue;
                IBase newValuePrimitive;
                if (isGroupByProductQuotient(value, i, lastOperand))
                {   // newValuePrimitive needs to be formulated from all values leading up to next SumDifference
                    newValuePrimitive = newValueFromProductQuotientGroup(value, i, out continueIndex, newValue);
                }
                else if (addNewSumDifference(newValue, value, i))
                {
                    newValuePrimitive = parseToObject(newValue);
                }
                else if (isGroupByBrackets(value, i))
                {
                    newValuePrimitive = newValueFromBracketGroup(value, i, out continueIndex, newValue);
                }
                else
                {
                    newValuePrimitive = null;
                }

                sumDifferenceSet = (SumDifferenceSet)updateSet<SumDifferenceSet>(
                    value, i, 
                    ref newValue, 
                    ref lastOperand, 
                    sumDifferenceSet, 
                    newValuePrimitive);
            }
        }

        private static SumDifferenceSet finalizeSumDifferenceSet(SumDifferenceSet sumDifferenceSet,
            string newValue, 
            char lastOperand)
        {
            IBase newValuePrimitive = parseToObject(newValue);

            if (sumDifferenceSet == null)
            {
                return new SumDifferenceSet(newValuePrimitive);
            }
            sumDifferenceSet.AppendItemToGroup(lastOperand, newValuePrimitive);

            return sumDifferenceSet;
        }



        private static IBase parseAsProductQuotient(string value)
        {
            string newValue = string.Empty;
            char lastOperand = Query.EMPTY;
            ProductQuotientSet productQuotientSet = null;

            aggregateAndCombineItems(value, ref productQuotientSet, ref lastOperand, ref newValue);

            // Combine final item and return
            return finalizeProductQuotientSet(productQuotientSet, newValue, lastOperand);
        }

        private static void aggregateAndCombineItems(
            string value,
            ref ProductQuotientSet productQuotientSet,
            ref char lastOperand,
            ref string newValue)
        {
            int continueIndex = -1;

            // Aggregate and combine items
            for (int i = 0; i < value.Length; i++)
            {
                if (i < continueIndex) continue;
                IBase newValuePrimitive;
                if (isGroupByProductQuotient(value, i, lastOperand))
                {   // newValuePrimitive needs to be formulated from all values leading up to next SumDifference
                    newValuePrimitive = newValueFromProductQuotientGroup(value, i, out continueIndex, newValue);
                }
                else if (addNewProductQuotient(newValue, value[i]))
                {
                    newValuePrimitive = parseToObject(newValue);
                }
                else if (addNewSumDifference(newValue, value, i))
                {
                    newValuePrimitive = parseToObject(newValue);
                }
                else if (isGroupByBrackets(value, i))
                {
                    newValuePrimitive = newValueFromBracketGroup(value, i, out continueIndex, newValue);
                    if (lastOperand == Query.ADD || lastOperand == Query.SUBTRACT)
                    {
                        string remainingValue = value.Substring(i);
                        newValue += remainingValue;
                        continueIndex += remainingValue.Length;
                        newValuePrimitive = parseToObject(newValue);
                    }
                }
                else if (value[i] == Query.POWER &&
                         (newValue.Contains(Query.POWER) || lastOperand == Query.POWER))
                {
                    newValuePrimitive = parseToObject(newValue);
                }
                else
                {
                    newValuePrimitive = null;
                }

                productQuotientSet = (ProductQuotientSet)updateSet<ProductQuotientSet>(
                    value, i, 
                    ref newValue, 
                    ref lastOperand, 
                    productQuotientSet, 
                    newValuePrimitive);
            }
        }

        private static ProductQuotientSet finalizeProductQuotientSet(ProductQuotientSet productQuotientSet, 
            string newValue, 
            char lastOperand)
        {
            IBase newValuePrimitive = parseToObject(newValue);
            if (productQuotientSet == null)
            {
                return new ProductQuotientSet(newValuePrimitive);
            }

            switch (lastOperand)
            {
                case Query.POWER when !productQuotientSet.HasPower():
                    productQuotientSet.SetPower(newValuePrimitive);
                    break;
                case Query.POWER:
                    productQuotientSet = new ProductQuotientSet(productQuotientSet);
                    productQuotientSet.SetPower(newValuePrimitive);
                    break;
                default:
                    productQuotientSet.AppendItemToGroup(lastOperand, newValuePrimitive);
                    break;
            }

            return productQuotientSet;
        }


        private static IBaseSet updateSet<T>(string value, int i,
            ref string newValue, ref char lastOperand,
            IBaseSet set, IBase newValuePrimitive) where T : IBaseSet, new()
        {
            char currentCharacter = value[i];
            if (newValuePrimitive != null)
            {
                if (set == null)
                {
                    set = new T();
                    set = set.Factory(newValuePrimitive);
                }
                else
                {
                    set.AppendItemToGroup(lastOperand, newValuePrimitive);
                }

                updateOperand(value, i, out lastOperand);
                newValue = string.Empty;
            }
            else if (Query.IsSumDifference(value, i) || Query.IsProductQuotient(value, i))
            {
                lastOperand = currentCharacter;
            }
            else if (!char.IsWhiteSpace(currentCharacter))
            {
                newValue += currentCharacter;
            }

            return set;
        }
        

        private static IBase newValueFromBracketGroup(string value, int i, out int continueIndex, string newValue = "")
        {
            // Check for negative sign & adjust index to include
            if ((i > 0 && value[i - 1] == Sign.NEGATIVE) &&
                (i == 1 || (i > 1 && Query.OperatorAllTypes.Contains(value[i - 2]))))
            {
                i--;
            }

            // Create new item from grouped parentheses
            Group group = new Group(value.Substring(i));

            continueIndex = group.ContinueIndex >= 0 ? i + group.ContinueIndex : i;

            bool isNegative = group.IsNegative;
            IBase newValueBase = string.IsNullOrEmpty(group.Base) ? null : parseToObject(group.Base);
            IBase newPower = string.IsNullOrEmpty(group.Power) ? null : parseToObject(group.Power);
            IBase newGroup;

            if (newValueBase == null)
            {
                return null;
            }

            if (newPower == null && !isNegative)
            {
                newGroup = newValueBase;
            }
            else
            {
                newGroup = new ProductQuotientSet(newValueBase, newPower, isNegative);
            }

            if (!string.IsNullOrEmpty(newValue) && (i > 0 && value[i - 1] == Query.POWER))
            {
                return new ProductQuotientSet(newValue, newGroup);
            }

            return newGroup;
        }

        private static IBase newValueFromProductQuotientGroup(string value, int i, out int continueIndex, string newValue = "")
        {
            // (1) Create temporary group
            // Insert open parentheses
            value = value.Substring(0, i - 1) + Group.OpenGroupTypes[0] + value.Substring(i - 1);
            string tempGroupValue = value;

            // Get extent of group
            for (int j = i; j < value.Length; j++)
            {
                // Insert close parentheses
                if (Query.IsSumDifference(value, j) ||
                    isGroupByBrackets(value, j))
                {
                    tempGroupValue = value.Substring(0, j) + Group.CloseGroupTypes[0] + value.Substring(j);
                    break;
                }

                if (j != value.Length - 1) continue;

                tempGroupValue += Group.CloseGroupTypes[0];
                break;
            }

            // (2) Get values from temporary group
            IBase newGroup = newValueFromBracketGroup(tempGroupValue, i - 1, out continueIndex, newValue);

            // (3) Reverse index increment for temporary group parentheses & return group value
            continueIndex -= 2;
            return newGroup;
        }
        
        private static bool isGroupByBrackets(string value, int index)
        {
            return Group.OpenGroupTypes.Contains(value[index]);
        }

        private static bool isGroupByProductQuotient(string value, int index, char lastOperand)
        {
            return ((lastOperand == Query.ADD || lastOperand == Query.SUBTRACT) &&
                    Query.IsProductQuotient(value, index));
        }

        private static void updateOperand(string value, int i, out char lastOperand)
        {
            char lastCharacter = (i == 0) ? Query.EMPTY : value[i - 1];
            char currentCharacter = (i == 1 && Query.IsNegative(value, i - 1))? Query.EMPTY : value[i]; // Ignores negative sign if at beginning

            lastOperand = !Group.OpenGroupTypes.Contains(currentCharacter) ? currentCharacter : lastCharacter;
        }

        private static bool addNewSumDifference(string newValue, string value, int index)
        {
            char character = value[index];
            char lastCharacter = index > 0 ? value[index - 1] : Query.EMPTY;
            return (!string.IsNullOrEmpty(newValue) &&
                    Query.AddSubtractTypes.Contains(character) &&
                    lastCharacter != Query.POWER);
        }

        private static bool addNewProductQuotient(string newValue, char character)
        {
            return (!string.IsNullOrEmpty(newValue) &&
                    (Query.MultiplyDivideTypes.Contains(character)));
        }
        #endregion

        #region Units Access

        public UnitOperatorPair this[int index] => _unitOperatorPair[index];

        public IEnumerator GetEnumerator()
        {
            return new BaseSetEnumerator(_unitOperatorPair);
        }

        protected class BaseSetEnumerator : IEnumerator
        {
            /// <summary>
            /// The position used for iteration.
            /// </summary>
            private int position = -1;

            public List<UnitOperatorPair> _unitOperatorPair;

            public BaseSetEnumerator(List<UnitOperatorPair> unitOperatorPair)
            {
                _unitOperatorPair = unitOperatorPair;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _unitOperatorPair.Count);
            }

            public void Reset()
            {
                position = 0;
            }

            public object Current
            {
                get
                {
                    try
                    {
                        return _unitOperatorPair[position];
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                }
            }
        }
        #endregion

    }
}
