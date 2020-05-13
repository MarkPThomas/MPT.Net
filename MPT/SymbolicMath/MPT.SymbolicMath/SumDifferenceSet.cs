// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-03-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-25-2018
// ***********************************************************************
// <copyright file="SumDifferenceSet.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPT.SymbolicMath
{
    /// <summary>
    /// Class SumDifferenceSet.
    /// </summary>
    /// <seealso cref="MPT.SymbolicMath.BaseSet" />
    public class SumDifferenceSet : BaseSet
    {
        #region Methods: Public
        /// <summary>
        /// Initializes a new instance of the <see cref="SumDifferenceSet"/> class.
        /// </summary>
        public SumDifferenceSet() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SumDifferenceSet"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="power">The power.</param>
        /// <param name="isNegative">if set to <c>true</c> [is negative].</param>
        public SumDifferenceSet(IBase initialValue, IBase power = null, bool isNegative = false)
        {
            initialize(initialValue, power, isNegative);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SumDifferenceSet"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="power">The power.</param>
        /// <param name="isNegative">if set to <c>true</c> [is negative].</param>
        public SumDifferenceSet(string initialValue, IBase power = null, bool isNegative = false)
        {
            initialize(initialValue, power, isNegative);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SumDifferenceSet"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="power">The power.</param>
        /// <param name="isNegative">if set to <c>true</c> [is negative].</param>
        public SumDifferenceSet(int initialValue, IBase power = null, bool isNegative = false)
        {
            initialize(new PrimitiveUnit(initialValue), power, isNegative);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SumDifferenceSet"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="power">The power.</param>
        /// <param name="isNegative">if set to <c>true</c> [is negative].</param>
        public SumDifferenceSet(double initialValue, IBase power = null, bool isNegative = false)
        {
            initialize(new PrimitiveUnit(initialValue), power, isNegative);
        }

        /// <summary>
        /// Values the is negative.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool ValueIsNegative()
        {
            if (IsNumber())
            {
                return (Calculate() < 0);
            }

            // Cannot determine negative value from symbols
            // Best guess is returned
            return SignIsNegative();
        }

        /// <summary>
        /// Distributes the sign.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase DistributeSign()
        {
            SumDifferenceSet newSet = CloneSet();
            for (int i = 0; i < Count; i++)
            {
                if (_unitOperatorPair[i].OperatorEquals(Query.EMPTY))
                {
                    newSet._unitOperatorPair[i].Unit.FlipSign();
                }
                else if (_unitOperatorPair[i].OperatorEquals(Query.ADD))
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateOperator(Query.SUBTRACT);
                }
                else if (_unitOperatorPair[i].OperatorEquals(Query.SUBTRACT))
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateOperator(Query.ADD);
                }
            }

            return newSet;
        }

        /// <summary>
        /// Extracts the sign.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public override IBase ExtractSign(bool isRecursive = false)
        {
            if (IsEmpty()) return new SumDifferenceSet();
            SumDifferenceSet newSet = CloneSet();
            return extractSign(newSet, isRecursive);
        }

        /// <summary>
        /// Distributes the sign from power.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase DistributeSignFromPower()
        {
            if (IsEmpty()) return new SumDifferenceSet();
            SumDifferenceSet newSet = CloneSet();
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
            if (IsEmpty()) return new SumDifferenceSet();
            SumDifferenceSet newSet = CloneSet();
            IBase power = newSet.GetPower()?.ExtractSign();
            newSet.addPower(power);
            return newSet;
        }

        /// <summary>
        /// Sums the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SumItem(IBase item)
        {
            return appendItem(item, appendUnitWithAdditionOperator);
        }

        /// <summary>
        /// Subtracts the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool SubtractItem(IBase item)
        {
            return appendItem(item, appendUnitWithSubtractionOperator);
        }

        
        protected static void appendUnitWithAdditionOperator(List<UnitOperatorPair> unitOperatorPairs, IBase item)
        {
            appendUnitOperatorPair(unitOperatorPairs, item, Query.ADD);
        }
        protected static void appendUnitWithSubtractionOperator(List<UnitOperatorPair> unitOperatorPairs, IBase item)
        {
            appendUnitOperatorPair(unitOperatorPairs, item, Query.SUBTRACT);
        }

        /// <summary>
        /// Labels this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string Label()
        {
            string baseLabel = rawBaseLabel();
            if (string.IsNullOrEmpty(baseLabel)) return string.Empty;

            bool includeParenthesesBase = ((!isSingleUnit() || _unitOperatorPair[0].Unit.HasPower()) && HasPower()) ||
                                          SignIsNegative() ||
                                          ((baseLabel.Contains(Query.ADD) || baseLabel.Substring(1).Contains(Query.SUBTRACT) ||
                                            baseLabel.Contains(Query.MULTIPLY) || baseLabel.Contains(Query.DIVIDE)) && HasPower());

            IBase power = GetPower();
            string powerLabel = (power == null) ? string.Empty : power.Label();
            bool includeParenthesesPower = HasPower() &&
                                           ((power != null && power.IsFraction()) ||
                                            (powerLabel.Contains(Query.ADD) || powerLabel.Substring(1).Contains(Query.SUBTRACT) ||
                                             powerLabel.Contains(Query.MULTIPLY) || powerLabel.Contains(Query.DIVIDE)));

            return label(baseLabel, includeParenthesesBase, includeParenthesesPower);
        }

        /// <summary>
        /// Gets the base.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase GetBase()
        {
            return getBase();
        }

        /// <summary>
        /// Gets the absolute.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase GetAbsolute()
        {
            return Count == 1 ?
                new SumDifferenceSet(_unitOperatorPair[0].Unit.GetAbsolute(), _power) :
                new SumDifferenceSet(GetBase(), _power);
        }

        /// <summary>
        /// Simplifies the specified is recursive.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public override IBase Simplify(bool isRecursive = false)
        {
            if (IsEmpty()) return new SumDifferenceSet();
            SumDifferenceSet newSet = CloneSet();

            // Simplify base & power items
            if (isRecursive)
            {
                for (int i = 0; i < newSet.Count; i++)
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateUnit(newSet._unitOperatorPair[i].Unit.Simplify(isRecursive: true));
                }
            }
            
            // Simplified Group
            // TODO: Simplified Group
            //newSet = SimplifyOperandGroups(isRecursive);

            // Sort Numeric vs. Variable
            // TODO: Sort Numeric vs. Variable
            //newSet = SortNumericBeforeVariables(isRecursive);

            // Combine powers
            newSet = combinePowers(newSet);

            // Simplify Signs
            newSet = extractSign(newSet, isRecursive);
            newSet = (SumDifferenceSet) newSet.ExtractSignFromPower();

            // Simplified Fractional
            //newSet = new SumDifferenceSet(simplifyFractional(newSet));

            // Combine Numeric
            // TODO: Combine Numeric
            //newSet = CombineNumeric(isRecursive);

            // Combine Variables
            // TODO: Simplified Variables
            //newSet = CombineVariables(isRecursive);

            return newSet;
        }

        /// <summary>
        /// Simplifies the fractional.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public override IBase SimplifyFractional(bool isRecursive = false)
        {
            return simplifyFractionalSet<SumDifferenceSet>(isRecursive);
        }

        /// <summary>
        /// Simplifies the units of one.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        public override IBaseSet SimplifyUnitsOfOne(bool isRecursive = true)
        {
            if (IsEmpty()) return new SumDifferenceSet();
            SumDifferenceSet newSet = CloneSet();

            if (!isRecursive) return newSet;

            // Simplify 
            for (int i = 0; i < newSet.Count; i++)
            {
                if (_unitOperatorPair[i].Unit is IBaseSet unitSet)
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateUnit(unitSet.SimplifyUnitsOfOne(isRecursive: true));
                }
            }

            return newSet;
        }

        /// <summary>
        /// Gathers the variables.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="count">The count.</param>
        /// <returns>ProductQuotientSet.</returns>
        private ProductQuotientSet gatherVariables(string label, int count)
        {
            if (count == 1)
            {
                return new ProductQuotientSet(label);
            }
            else if (count == -1)
            {
                return new ProductQuotientSet(label, null, isNegative: true);
            }
            else
            {
                ProductQuotientSet newSetMultiple = new ProductQuotientSet(count);
                newSetMultiple.MultiplyItem(new ProductQuotientSet(label));
                return newSetMultiple;
            }
        }

        /// <summary>
        /// Simplifies the variables.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override IBaseSet SimplifyVariables(bool isRecursive = false)
        {
            Dictionary<string, int> variables = ExtractVariableAndValue();

            SumDifferenceSet newSet = null;
            foreach (var variableSet in variables)
            {
                if (variables.Count == 1 && variableSet.Value == 0)
                {
                    return new ProductQuotientSet(0);
                }
                if (variableSet.Value == 0) continue;

                bool variableIsPositive = variableSet.Value > 0;
                ProductQuotientSet currentVariable = gatherVariables(variableSet.Key, variableSet.Value);

                if (newSet == null)
                {
                    newSet = new SumDifferenceSet(currentVariable);
                }
                else if (variableIsPositive)
                {
                    newSet.SumItem(currentVariable);
                }
                else
                {
                    currentVariable.FlipSign();
                    newSet.SubtractItem(currentVariable);
                }
            }

            return newSet == null ? CloneSet() : newSet.SimplifyUnitsOfOne();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Simplifies the operand groups.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public override IBaseSet SimplifyOperandGroups(bool isRecursive = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether [is unit positive] [the specified i].
        /// </summary>
        /// <param name="i">The i.</param>
        /// <returns><c>true</c> if [is unit positive] [the specified i]; otherwise, <c>false</c>.</returns>
        protected bool isUnitPositive(int i)
        {
            return (!_unitOperatorPair[i].Unit.ValueIsNegative() &&
                    (_unitOperatorPair[i].OperatorEquals(Query.EMPTY) ||
                     _unitOperatorPair[i].OperatorEquals(Query.ADD))) ||
                   (_unitOperatorPair[i].Unit.ValueIsNegative() &&
                    _unitOperatorPair[i].OperatorEquals(Query.SUBTRACT));
        }

        public Dictionary<string, int> ExtractVariableAndValue()
        {
            List<string> variableLabels = new List<string>();
            List<int> variableCounts = new List<int>();

            // Populate variable lists with variable, count
            for (int i = 0; i < Count; i++)
            {
                string absoluteValue = _unitOperatorPair[i].Unit.GetAbsolute().Label();
                bool isCurrentUnitPositive = isUnitPositive(i);

                // Get variable count
                string[] absoluteValueAndVariable = absoluteValue.Split(Query.MULTIPLY);
                int variableCount = 1;
                switch (absoluteValueAndVariable.Length)
                {
                    case 2 when Query.IsNumeric(absoluteValueAndVariable[0]):
                        absoluteValue = absoluteValueAndVariable[1];
                        int.TryParse(absoluteValueAndVariable[0], out variableCount);
                        break;
                    case 2 when Query.IsNumeric(absoluteValueAndVariable[1]):
                        absoluteValue = absoluteValueAndVariable[0];
                        int.TryParse(absoluteValueAndVariable[1], out variableCount);
                        break;
                    default:
                        // Do nothing
                        break;
                }

                int valueAdd = isCurrentUnitPositive ? variableCount : -variableCount;
                if (!variableLabels.Contains(absoluteValue))
                {
                    variableLabels.Add(absoluteValue);
                    
                    variableCounts.Add(valueAdd);
                }
                else
                {
                    int variableIndex = variableLabels.IndexOf(absoluteValue);
                    variableCounts[variableIndex] += valueAdd;
                    //if (isCurrentUnitPositive)
                    //{
                    //    variableCounts[variableIndex] += variableCount;
                    //}
                    //else
                    //{
                    //    variableCounts[variableIndex]--;
                    //}
                }
            }

            // Package lists into a dictionary with the resulting sign added to the label
            Dictionary<string, int> variables = new Dictionary<string, int>();
            for (int i = 0; i < variableLabels.Count; i++)
            {
                variables.Add(variableLabels[i], variableCounts[i]);
            }
            return variables;
        }

        public override bool AppendItemToGroup(char lastOperand, IBase newValuePrimitive)
        {
            switch (lastOperand)
            {
                case Query.ADD:
                    return SumItem(newValuePrimitive);
                case Query.SUBTRACT:
                    return SubtractItem(newValuePrimitive);
                case Query.MULTIPLY:
                case Query.DIVIDE:
                    IBase lastUnit = _unitOperatorPair[_unitOperatorPair.Count - 1].Unit;
                    ProductQuotientSet productQuotientSet;
                    if (!(lastUnit is ProductQuotientSet))
                    {
                        productQuotientSet = new ProductQuotientSet(lastUnit);
                    }
                    else
                    {
                        productQuotientSet = lastUnit as ProductQuotientSet;
                    }
                    productQuotientSet.AppendItemToGroup(lastOperand, newValuePrimitive);
                    _unitOperatorPair[_unitOperatorPair.Count - 1] = _unitOperatorPair[_unitOperatorPair.Count - 1].UpdateUnit(productQuotientSet);
                    return true;
                default:
                    return false;
            }
        }

        public override IBaseSet Factory(IBase newValuePrimitive)
        {
            return new SumDifferenceSet(newValuePrimitive);
        }
        #endregion

        #region Private
        protected IBase getBase(bool getAbsolute = false)
        {
            if (Count == 0) return new SumDifferenceSet();
            IBase unitBase = getUnit(0, getAbsolute);
            SumDifferenceSet newSet = new SumDifferenceSet(unitBase);
            for (int i = 1; i < Count; i++)
            {
                if (_unitOperatorPair[i].OperatorEquals(Query.ADD))
                {
                    newSet.SumItem(_unitOperatorPair[i].Unit);
                }
                if (_unitOperatorPair[i].OperatorEquals(Query.SUBTRACT))
                {
                    newSet.SubtractItem(_unitOperatorPair[i].Unit);
                }
            }

            return newSet;
        }

        protected SumDifferenceSet extractSign(SumDifferenceSet newSet, bool isRecursive = false)
        {
            bool firstValueWasNegative = newSet._unitOperatorPair[0].Unit.SignIsNegative();

            if (isRecursive)
            {
                for (int i = 0; i < newSet.Count; i++)
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateUnit(newSet._unitOperatorPair[i].Unit.ExtractSign(isRecursive: true));
                }
            }

            newSet = factorOutNegativeSigns(newSet);

            if (!firstValueWasNegative) return newSet;

            newSet.FlipSign();
            newSet = invertOperators(newSet);
            return newSet;
        }

        /// <summary>
        /// Factors the out negative signs (all +- and -- cases).
        /// </summary>
        /// <param name="newSet">The new set.</param>
        /// <returns>MPT.SymbolicMath.SumDifferenceSet.</returns>
        protected SumDifferenceSet factorOutNegativeSigns(SumDifferenceSet newSet)
        {
            for (int i = 0; i < newSet.Count; i++)
            {
                if (!newSet._unitOperatorPair[i].Unit.SignIsNegative()) continue;

                // Flip sign of unit and flip operator
                newSet._unitOperatorPair[i].Unit.FlipSign();
                if (newSet._unitOperatorPair[i].OperatorEquals(Query.ADD))
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateOperator(Query.SUBTRACT);
                }
                else if (newSet._unitOperatorPair[i].OperatorEquals(Query.SUBTRACT))
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateOperator(Query.ADD);
                }
            }

            return newSet;
        }

        /// <summary>
        /// Inverts all operators (flips all + &amp; - operators).
        /// </summary>
        /// <param name="newSet">The new set.</param>
        /// <returns>MPT.SymbolicMath.SumDifferenceSet.</returns>
        protected SumDifferenceSet invertOperators(SumDifferenceSet newSet)
        {
            for (int i = 0; i < newSet.Count; i++)
            {
                if (newSet._unitOperatorPair[i].OperatorEquals(Query.ADD))
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateOperator(Query.SUBTRACT);
                }
                else if (newSet._unitOperatorPair[i].OperatorEquals(Query.SUBTRACT))
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateOperator(Query.ADD);
                }
            }

            return newSet;
        }

        protected SumDifferenceSet combinePowers(SumDifferenceSet newSet)
        {
            for (int i = 0; i < newSet.Count - 1; i++)
            {
                bool baseAIsNegative = newSet._unitOperatorPair[i].Unit.GetSign().IsNegative();
                IBase baseA = newSet._unitOperatorPair[i].Unit.GetBase();
                IBase powerA = newSet._unitOperatorPair[i].Unit.GetPower() ?? new PrimitiveUnit(1);
                IBase baseB = newSet._unitOperatorPair[i + 1].Unit.GetBase();
                IBase powerB = newSet._unitOperatorPair[i + 1].Unit.GetPower() ?? new PrimitiveUnit(1);
                bool baseSignIsSame = newSet._unitOperatorPair[i].Unit.GetSign().Equals(newSet._unitOperatorPair[i + 1].Unit.GetSign());

                bool isSum = (newSet._unitOperatorPair[i + 1].OperatorEquals(Query.ADD));
                bool baseIsSame = baseA.GetBase().Equals(baseB.GetBase());
                bool exponentIsSame = powerA.GetBase().Equals(powerB.GetBase());
                bool powerSignIsSame = powerA.GetSign().Equals(powerB.GetSign());

                // (1a) Same Base, Same exponent, Same exponent sign
                if (baseIsSame && exponentIsSame && powerSignIsSame)
                {
                    if ((isSum && baseSignIsSame) ||
                        (!isSum && !baseSignIsSame))
                    { // Add Exponent Same Sign
                        ProductQuotientSet newBasePower = new ProductQuotientSet(2);
                        newBasePower.MultiplyItem(new ProductQuotientSet(baseA, powerA));
                        newSet = new SumDifferenceSet(newBasePower, null, baseAIsNegative);
                    }
                    else
                    { // Subtract Exponent Same Sign
                        newSet = new SumDifferenceSet(0);
                    }
                }

                // (1b) Same Base, Same exponent, Different sign
                // (2) Same Base, Different exponent (ignore sign)
                // (3) Different Base, Same exponent (ignore sign)
                // (4) Different Base, Different exponent (ignore sign)
                if (!baseIsSame && !exponentIsSame)
                {
                    // Do Nothing
                }
            }

            // Calculate if numeric
            IBase power = newSet.GetPower();
            if (power == null || !power.IsNumber()) return newSet;

            power = new PrimitiveUnit(power.Calculate());
            newSet = new SumDifferenceSet(
                new ProductQuotientSet(newSet.GetBase(), power, newSet.SignIsNegative())
                );

            return newSet;
        }
        
        protected override ProductQuotientSet simplifyFractional<T>(T newSetGeneric)
        {
            if (!(newSetGeneric is SumDifferenceSet)) return new ProductQuotientSet(newSetGeneric);
            SumDifferenceSet newSet = newSetGeneric as SumDifferenceSet;
            if (newSet.Count < 2) return new ProductQuotientSet(newSet);

            ProductQuotientSet leftSet = fractionSet(newSet, 0);

            for (int i = 1; i < newSet.Count; i++)
            {
                ProductQuotientSet rightSet = fractionSet(newSet, i);
                IBase unitA = unitSet(leftSet, 0);
                IBase unitB = unitSet(leftSet, 1);
                IBase unitC = unitSet(rightSet, 0);
                IBase unitD = unitSet(rightSet, 1);

                ProductQuotientSet numeratorSetLeft = new ProductQuotientSet(unitA);
                numeratorSetLeft.MultiplyItem(unitD);
                numeratorSetLeft = simplifyNumeric<ProductQuotientSet>(numeratorSetLeft);
                ProductQuotientSet numeratorSetRight = new ProductQuotientSet(unitC);
                numeratorSetRight.MultiplyItem(unitB);
                numeratorSetRight = simplifyNumeric<ProductQuotientSet>(numeratorSetRight);

                ProductQuotientSet denominatorSet = new ProductQuotientSet(unitB);
                denominatorSet.MultiplyItem(unitD);
                denominatorSet = simplifyNumeric<ProductQuotientSet>(denominatorSet);

                SumDifferenceSet numeratorSet = new SumDifferenceSet(numeratorSetLeft);
                if (newSet._unitOperatorPair[i].OperatorEquals(Query.ADD))
                {
                    numeratorSet.SumItem(numeratorSetRight);
                }
                else if (newSet._unitOperatorPair[i].OperatorEquals(Query.SUBTRACT))
                {
                    numeratorSet.SubtractItem(numeratorSetRight);
                }
                numeratorSet = simplifyNumeric<SumDifferenceSet>(numeratorSet);

                leftSet = new ProductQuotientSet(numeratorSet);
                leftSet.DivideItem(denominatorSet);
            }

            string simplifiedFraction = Query.SimplifiedFraction(leftSet.Label());
            ProductQuotientSet set = new ProductQuotientSet(simplifiedFraction);
            return (ProductQuotientSet)set.SimplifyUnitsOfOne();
        }

        #endregion

        #region Overrides     

        public SumDifferenceSet CloneSet()
        {
            return (SumDifferenceSet)Clone();
        }

        public override object Clone()
        {
            IBase unit = Count > 0 ? _unitOperatorPair[0].Unit : null;
            SumDifferenceSet newSet = new SumDifferenceSet(unit, _power, SignIsNegative());
            for (int i = 1; i < Count; i++)
            {
                if (_unitOperatorPair[i].OperatorEquals(Query.ADD))
                {
                    newSet.SumItem(_unitOperatorPair[i].Unit);
                }
                if (_unitOperatorPair[i].OperatorEquals(Query.SUBTRACT))
                {
                    newSet.SubtractItem(_unitOperatorPair[i].Unit);
                }
            }

            return newSet;
        }
        #endregion

        #region Operators

        public static SumDifferenceSet operator +(SumDifferenceSet set1, SumDifferenceSet set2)
        {
            checkNull(set1, set2);
            return bothAreNumeric(set1, set2) ?
                new SumDifferenceSet(addNumeric(set1, set2)) : 
                addSymbolic(set1, set2);
        }

        public static SumDifferenceSet operator -(SumDifferenceSet set1, SumDifferenceSet set2)
        {
            checkNull(set1, set2);
            return bothAreNumeric(set1, set2) ?
                new SumDifferenceSet(subtractNumeric(set1, set2)) : 
                subtractSymbolic(set1, set2);
        }

        public static ProductQuotientSet operator *(SumDifferenceSet set1, SumDifferenceSet set2)
        {
            checkNull(set1, set2);
            return bothAreNumeric(set1, set2) ?
                new ProductQuotientSet(multiplyNumeric(set1, set2)) : 
                multiplySymbolic(set1, set2);
        }

        public static ProductQuotientSet operator /(SumDifferenceSet set1, SumDifferenceSet set2)
        {
            checkNull(set1, set2);
            return bothAreNumeric(set1, set2) ?
                new ProductQuotientSet(divideNumeric(set1, set2)) : 
                divideSymbolic(set1, set2);
        }
        #endregion

        #region Operators: Cross-Types IBase
        public static SumDifferenceSet operator +(SumDifferenceSet value1, IBase value)
        {
            checkNull(value1, value);
            value1 = value1.CloneSet();
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
        public static SumDifferenceSet operator +(IBase value, SumDifferenceSet value1)
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

        public static SumDifferenceSet operator -(SumDifferenceSet value1, IBase value)
        {
            checkNull(value1, value);
            value1 = value1.CloneSet();
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
        public static SumDifferenceSet operator -(IBase value, SumDifferenceSet value1)
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
        #endregion

        #region Operators: Cross-Types PrimitiveUnit


        public static SumDifferenceSet operator +(SumDifferenceSet set, PrimitiveUnit value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            set.SumItem(value);
            return set;
        }
        public static SumDifferenceSet operator +(PrimitiveUnit value, SumDifferenceSet set)
        {
            checkNull(set, value);
            return new SumDifferenceSet(value) + set;
        }


        public static SumDifferenceSet operator -(SumDifferenceSet set, PrimitiveUnit value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            set.SubtractItem(value);
            return set;
        }
        public static SumDifferenceSet operator -(PrimitiveUnit value, SumDifferenceSet set)
        {
            checkNull(set, value);
            return new SumDifferenceSet(value) - set;
        }

        public static ProductQuotientSet operator *(SumDifferenceSet set, PrimitiveUnit value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return new ProductQuotientSet(set) * value;
        }
        public static ProductQuotientSet operator *(PrimitiveUnit value, SumDifferenceSet set)
        {
            checkNull(set, value);
            return value * new ProductQuotientSet(set);
        }


        public static ProductQuotientSet operator /(SumDifferenceSet set, PrimitiveUnit value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return new ProductQuotientSet(set) / value;
        }
        public static ProductQuotientSet operator /(PrimitiveUnit value, SumDifferenceSet set)
        {
            checkNull(set, value);
            return value / new ProductQuotientSet(set);
        }
        #endregion
        
        #region Operators: Cross-Types Integer
        public static SumDifferenceSet operator +(SumDifferenceSet set, int value)
        {
            checkNull(set);
            return set + new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator +(int value, SumDifferenceSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) + set;
        }


        public static SumDifferenceSet operator -(SumDifferenceSet set, int value)
        {
            checkNull(set);
            return set - new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator -(int value, SumDifferenceSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) - set;
        }

        public static ProductQuotientSet operator *(SumDifferenceSet set, int value)
        {
            checkNull(set);
            return set * new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator *(int value, SumDifferenceSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) * set;
        }


        public static ProductQuotientSet operator /(SumDifferenceSet set, int value)
        {
            checkNull(set);
            return set / new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator /(int value, SumDifferenceSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) / set;
        }
        #endregion

        #region Operators: Cross-Types Double
        public static SumDifferenceSet operator +(SumDifferenceSet set, double value)
        {
            checkNull(set);
            return set + new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator +(double value, SumDifferenceSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) + set;
        }


        public static SumDifferenceSet operator -(SumDifferenceSet set, double value)
        {
            checkNull(set);
            return set - new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator -(double value, SumDifferenceSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) - set;
        }

        public static ProductQuotientSet operator *(SumDifferenceSet set, double value)
        {
            checkNull(set);
            return set * new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator *(double value, SumDifferenceSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) * set;
        }


        public static ProductQuotientSet operator /(SumDifferenceSet set, double value)
        {
            checkNull(set);
            return set / new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator /(double value, SumDifferenceSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) / set;
        }
        #endregion

        #region Operators: Cross-Types String
        public static SumDifferenceSet operator +(SumDifferenceSet set, string value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set + new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator +(string value, SumDifferenceSet set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) + set;
        }


        public static SumDifferenceSet operator -(SumDifferenceSet set, string value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set - new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator -(string value, SumDifferenceSet set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) - set;
        }

        public static ProductQuotientSet operator *(SumDifferenceSet set, string value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set * new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator *(string value, SumDifferenceSet set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) * set;
        }


        public static ProductQuotientSet operator /(SumDifferenceSet set, string value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set / new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator /(string value, SumDifferenceSet set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) / set;
        }
        #endregion
    }
}
