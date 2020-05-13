// ***********************************************************************
// Assembly         : MPT.SymbolicMath
// Author           : Mark Thomas
// Created          : 08-03-2018
//
// Last Modified By : Mark Thomas
// Last Modified On : 08-29-2018
// ***********************************************************************
// <copyright file="ProductQuotientSet.cs" company="">
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
    /// Class ProductQuotientSet.
    /// </summary>
    /// <seealso cref="MPT.SymbolicMath.BaseSet" />
    public class ProductQuotientSet : BaseSet
    {
        #region Methods: Public
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductQuotientSet"/> class.
        /// </summary>
        public ProductQuotientSet() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductQuotientSet"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="power">The power.</param>
        /// <param name="isNegative">if set to <c>true</c> [is negative].</param>
        public ProductQuotientSet(IBase initialValue, IBase power = null, bool isNegative = false)
        {
            initialize(initialValue, power, isNegative);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductQuotientSet"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="power">The power.</param>
        /// <param name="isNegative">if set to <c>true</c> [is negative].</param>
        public ProductQuotientSet(string initialValue, IBase power = null, bool isNegative = false)
        {
            initialize(initialValue, power, isNegative);
            finalizeUnits();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductQuotientSet"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="power">The power.</param>
        /// <param name="isNegative">if set to <c>true</c> [is negative].</param>
        public ProductQuotientSet(int initialValue, IBase power = null, bool isNegative = false)
        {
            initialize(new PrimitiveUnit(initialValue), power, isNegative);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductQuotientSet"/> class.
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="power">The power.</param>
        /// <param name="isNegative">if set to <c>true</c> [is negative].</param>
        public ProductQuotientSet(double initialValue, IBase power = null, bool isNegative = false)
        {
            initialize(new PrimitiveUnit(initialValue), power, isNegative);
        }

        /// <summary>
        /// Values the is negative.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool ValueIsNegative()
        {
            int numberOfNegative = 0;
            if (SignIsNegative())
            {
                numberOfNegative++;
            }

            foreach (UnitOperatorPair item in _unitOperatorPair)
            {
                IBase unit = item.Unit;
                if (unit.ValueIsNegative())
                {
                    numberOfNegative++;
                }
            }

            return (numberOfNegative % 2 != 0);
        }

        /// <summary>
        /// Distributes the sign.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase DistributeSign()
        {
            ProductQuotientSet newSet = CloneSet();
            for (int i = 0; i < newSet.Count; i++)
            {
                if (newSet._unitOperatorPair[i].Operator == string.Empty)
                {
                    newSet._unitOperatorPair[i].Unit.FlipSign();
                }
                else if (newSet._unitOperatorPair[i].OperatorEquals(Query.ADD))
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateOperator(Query.SUBTRACT);
                }
                else if (newSet._unitOperatorPair[i].OperatorEquals(Query.SUBTRACT))
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateOperator(Query.ADD);
                }
            }
            newSet.FlipSign();

            return newSet;
        }

        /// <summary>
        /// Extracts the sign.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public override IBase ExtractSign(bool isRecursive = false)
        {
            if (IsEmpty()) return new ProductQuotientSet();
            ProductQuotientSet newSet = CloneSet();
            return extractSign(newSet, isRecursive);
        }

        /// <summary>
        /// Distributes the sign from power.
        /// </summary>
        /// <returns>IBase.</returns>
        public override IBase DistributeSignFromPower()
        {
            if (IsEmpty()) return new ProductQuotientSet();
            ProductQuotientSet newSet = CloneSet();
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
            if (IsEmpty()) return new ProductQuotientSet();
            ProductQuotientSet newSet = CloneSet();
            IBase power = newSet.GetPower()?.ExtractSign();
            newSet.addPower(power);
            return newSet;
        }

        /// <summary>
        /// Multiplies the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool MultiplyItem(IBase item)
        {
            return appendItem(item, appendUnitWithMultiplierOperator);
        }

        /// <summary>
        /// Divides the item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool DivideItem(IBase item)
        {
            return appendItem(item, appendUnitWithDivisionOperator);
        }
        
        protected static void appendUnitWithMultiplierOperator(List<UnitOperatorPair> unitOperatorPairs, IBase item)
        {
            //appendUnitOperatorPair(unitOperatorPairs, item, Query.MULTIPLY);
            IBase newItem = new PrimitiveUnit(item.Label());
            appendUnitOperatorPair(unitOperatorPairs,
                (newItem.Label() == item.Label()) ? newItem : item,
                Query.MULTIPLY);
        }
        protected static void appendUnitWithDivisionOperator(List<UnitOperatorPair> unitOperatorPairs, IBase item)
        {
            //appendUnitOperatorPair(unitOperatorPairs, item, Query.DIVIDE);
            IBase newItem = new PrimitiveUnit(item.Label());
            appendUnitOperatorPair(unitOperatorPairs,
                (newItem.Label() == item.Label()) ? newItem : item,
                Query.DIVIDE);
        }

        /// <summary>
        /// Sets the power.
        /// </summary>
        /// <param name="power">The power.</param>
        public void SetPower(IBase power)
        {
            addPower(power);
        }

        /// <summary>
        /// Labels this instance.
        /// </summary>
        /// <returns>System.String.</returns>
        public override string Label()
        {
            string baseLabel = rawBaseLabel();
            if (string.IsNullOrEmpty(baseLabel)) return string.Empty;

            // (a^n)^m 
            bool includeParenthesesBase = _unitOperatorPair[0].Unit.HasPower() && HasPower();

            // -(b^c) 
            if (!includeParenthesesBase)
            {
                includeParenthesesBase = _unitOperatorPair[0].Unit.HasPower() && SignIsNegative();
            }

            // (a*b)^m or (a/b)^m
            if (!includeParenthesesBase)
            {
                includeParenthesesBase = !isSingleUnit() && HasPower();
            }

            // ((a*b))^m, or (a+b)^m, or (a-b)^m, etc.
            if (!includeParenthesesBase)
            {
                includeParenthesesBase =
                    (baseLabel.Contains(Query.ADD) 
                     || baseLabel.Substring(1).Contains(Query.SUBTRACT)
                     || baseLabel.Contains(Query.MULTIPLY) 
                     || baseLabel.Contains(Query.DIVIDE))
                    && HasPower();
            }

            // -(a+b)
            if (!includeParenthesesBase)
            {
                includeParenthesesBase =
                    (SignIsNegative() 
                     && Count == 1
                     && _unitOperatorPair[0].Unit is SumDifferenceSet set
                     && set.Count > 1);
            }

            return label(baseLabel, includeParenthesesBase);
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
            if (IsEmpty()) return new ProductQuotientSet();
            return Count == 1 ? 
                new ProductQuotientSet(_unitOperatorPair[0].Unit.GetAbsolute(), _power) : 
                new ProductQuotientSet(getBase(getAbsolute: true), _power);
        }

        /// <summary>
        /// Combines the powers.
        /// </summary>
        /// <returns>IBase.</returns>
        public IBase CombinePowers()
        {
            if (IsEmpty()) return new ProductQuotientSet();
            ProductQuotientSet newSet = CloneSet();
            return combinePowers(newSet);
        }

        /// <summary>
        /// Simplifies the specified is recursive.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public override IBase Simplify(bool isRecursive = false)
        {
            if (IsEmpty()) return new ProductQuotientSet();
            ProductQuotientSet newSet = CloneSet();

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
            newSet = (ProductQuotientSet)newSet.ExtractSignFromPower();

            // Simplified Fractional
            //newSet = simplifyFractional(newSet);
            newSet = (ProductQuotientSet)newSet.SimplifyFractional();

            // Combine Numeric
            // TODO: Combine Numeric
            //newSet = CombineNumeric(isRecursive);

            // Combine Variables
            // TODO: Simplified Variables
            //newSet = CombineVariables(isRecursive);

            // Combine powers
            if (powersAlreadyCombined(newSet)) return newSet;
            newSet = combinePowers(newSet);

            return newSet;
        }

        /// <summary>
        /// Simplifies the fractional.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBase.</returns>
        public override IBase SimplifyFractional(bool isRecursive = false)
        {
            IBaseSet set = simplifyFractionalSet<ProductQuotientSet>(isRecursive);
            return set.SimplifyUnitsOfOne();
        }

        /// <summary>
        /// Simplifies the units of one.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        public override IBaseSet SimplifyUnitsOfOne(bool isRecursive = true)
        {
            if(IsEmpty()) return new ProductQuotientSet();
            ProductQuotientSet newSet = CloneSet();
            return simplifyUnitsOfOne(newSet, isRecursive);
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
        /// Gathers the variables.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="count">The count.</param>
        /// <returns>ProductQuotientSet.</returns>
        private static ProductQuotientSet gatherVariables(string label, int count)
        {
            if (count < 0)
            {
                label = "1/" + label;
            }

            return Math.Abs(count) == 1 ? 
                new ProductQuotientSet(label) : 
                new ProductQuotientSet(label, new PrimitiveUnit(Math.Abs(count)));
        }

        /// <summary>
        /// Simplifies the variables.
        /// </summary>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>IBaseSet.</returns>
        public override IBaseSet SimplifyVariables(bool isRecursive = false)
        {
            if (IsEmpty()) return new ProductQuotientSet();
            ProductQuotientSet newBaseSet = CloneSet();

            // Simplify 
            if (isRecursive)
            {
                for (int i = 0; i < newBaseSet.Count; i++)
                {
                    if (newBaseSet._unitOperatorPair[i].Unit is IBaseSet unitSet)
                    {
                        newBaseSet._unitOperatorPair[i] = newBaseSet._unitOperatorPair[i].UpdateUnit(unitSet.SimplifyVariables(isRecursive: true));
                    }
                }
            }

            Dictionary<string, int> variables = newBaseSet.extractVariableAndValue();

            ProductQuotientSet newSet = null;
            foreach (var variableSet in variables)
            {
                if (variables.Count == 1 && variableSet.Value == 0)
                {
                    bool variableIsNegative = (variableSet.Key.Length >= 1) &&
                                              (variableSet.Key.Substring(0,1) == Sign.NEGATIVE.ToString());
                    return new ProductQuotientSet(1, null, variableIsNegative);
                }
                if (variableSet.Value == 0) continue;
                
                ProductQuotientSet currentVariable = gatherVariables(variableSet.Key, variableSet.Value);

                if (newSet == null)
                {
                    newSet = new ProductQuotientSet(currentVariable);
                }
                else
                {
                    newSet.MultiplyItem(currentVariable);
                }
            }

            if (newSet == null) return CloneSet(); 

            newSet.mergeUnits();
            newSet = extractSign(newSet, isRecursive: true);
            newSet = simplifyUnitsOfOne(newSet, isRecursive: true);
            newSet = collectInverseProducts(newSet, isRecursive: true);
            return newSet;
        }

        /// <summary>
        /// Appends the item to group.
        /// </summary>
        /// <param name="lastOperand">The last operand.</param>
        /// <param name="newValuePrimitive">The new value primitive.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public override bool AppendItemToGroup(char lastOperand, IBase newValuePrimitive)
        {
            switch (lastOperand)
            {
                case Query.MULTIPLY:
                    return MultiplyItem(newValuePrimitive);
                case Query.DIVIDE:
                    return DivideItem(newValuePrimitive);
                case Query.POWER:
                    if (!HasPower())
                    {
                        addPower(newValuePrimitive);
                    }
                    else
                    {
                        ProductQuotientSet productQuotientSet = new ProductQuotientSet(CloneSet(), newValuePrimitive);
                        _unitOperatorPair.Clear();
                        addUnitOperatorPair(productQuotientSet, string.Empty);
                    }

                    return true;
                case Query.ADD:
                case Query.SUBTRACT:
                    SumDifferenceSet sumDifferenceSet = new SumDifferenceSet(CloneSet());
                    sumDifferenceSet.AppendItemToGroup(lastOperand, newValuePrimitive);
                    _unitOperatorPair.Clear();
                    addUnitOperatorPair(sumDifferenceSet, string.Empty);
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Factories the specified new value primitive.
        /// </summary>
        /// <param name="newValuePrimitive">The new value primitive.</param>
        /// <returns>IBaseSet.</returns>
        public override IBaseSet Factory(IBase newValuePrimitive)
        {
            return new ProductQuotientSet(newValuePrimitive);
        }
        #endregion


        #region Overrides     

        /// <summary>
        /// Clones the set.
        /// </summary>
        /// <returns>ProductQuotientSet.</returns>
        public ProductQuotientSet CloneSet()
        {
            return (ProductQuotientSet)Clone();
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            IBase unit = Count > 0 ? _unitOperatorPair[0].Unit : null;
            ProductQuotientSet newSet = new ProductQuotientSet(unit, _power, SignIsNegative());
            for (int i = 1; i < Count; i++)
            {
                if (_unitOperatorPair[i].OperatorEquals(Query.MULTIPLY)) 
                {
                    newSet.MultiplyItem(_unitOperatorPair[i].Unit);
                }
                if (_unitOperatorPair[i].OperatorEquals(Query.DIVIDE))
                {
                    newSet.DivideItem(_unitOperatorPair[i].Unit);
                }
            }

            return newSet;
        }
        #endregion

        #region Private
        /// <summary>
        /// Gets the base.
        /// </summary>
        /// <param name="getAbsolute">if set to <c>true</c> [get absolute].</param>
        /// <returns>IBase.</returns>
        protected IBase getBase(bool getAbsolute = false)
        {
            if (Count == 0) return new ProductQuotientSet();
            IBase unitBase = getUnit(0, getAbsolute);
            ProductQuotientSet newSet = new ProductQuotientSet(unitBase);
            for (int i = 1; i < Count; i++)
            {
                IBase unit = getUnit(i, getAbsolute);
                if (_unitOperatorPair[i].OperatorEquals(Query.MULTIPLY))
                {
                    newSet.MultiplyItem(unit);
                }
                if (_unitOperatorPair[i].OperatorEquals(Query.DIVIDE))
                {
                    newSet.DivideItem(unit);
                }
            }

            return newSet;
        }

        /// <summary>
        /// Extracts the sign.
        /// </summary>
        /// <param name="newSet">The new set.</param>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>ProductQuotientSet.</returns>
        protected ProductQuotientSet extractSign(ProductQuotientSet newSet, bool isRecursive = false)
        {
            if (newSet.Count == 1) return newSet;

            bool valueWasNegative = newSet.ValueIsNegative();

            if (isRecursive)
            {
                for (int i = 0; i < newSet.Count; i++)
                {
                    newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateUnit(newSet._unitOperatorPair[i].Unit.ExtractSign(isRecursive: true));
                }
            }

            // Make all values positive
            foreach (UnitOperatorPair item in newSet._unitOperatorPair)
            {
                IBase unit = item.Unit;
                if (unit.SignIsNegative())
                {
                    unit.FlipSign();
                }
            }

            if (valueWasNegative && !newSet.SignIsNegative())
            {
                newSet.FlipSign();
            }
            return newSet;
        }

        private static bool powersAlreadyCombined(ProductQuotientSet newSet)
        {
            foreach (UnitOperatorPair unitOperatorPair in newSet)
            {
                if (unitOperatorPair.Unit.HasPower()) return false;
            }

            return true;
        }

        /// <summary>
        /// Combines the powers.
        /// </summary>
        /// <param name="newSet">The new set.</param>
        /// <returns>ProductQuotientSet.</returns>
        protected ProductQuotientSet combinePowers(ProductQuotientSet newSet)
        {
            for (int i = 0; i < newSet.Count - 1; i++)
            {
                IBase baseA = newSet._unitOperatorPair[i].Unit.GetBase();
                IBase powerA = newSet._unitOperatorPair[i].Unit.GetPower() ?? new PrimitiveUnit(1);
                IBase baseB = newSet._unitOperatorPair[i + 1].Unit.GetBase();
                IBase powerB = newSet._unitOperatorPair[i + 1].Unit.GetPower() ?? new PrimitiveUnit(1);
                Sign baseSign = newSet._unitOperatorPair[i].Unit.GetSign() * newSet._unitOperatorPair[i + 1].Unit.GetSign();

                bool isProduct = (newSet._unitOperatorPair[i + 1].OperatorEquals(Query.MULTIPLY));
                bool baseIsSame = baseA.GetBase().Equals(baseB.GetBase());
                bool exponentIsSame = powerA.GetBase().Equals(powerB.GetBase());
                bool powerSignIsSame = powerA.GetSign().Equals(powerB.GetSign());

                // (1) Same Base, Same exponent (ignore sign)
                if (baseIsSame && exponentIsSame)
                {
                    // Multiply Exponent Same Sign
                    // Divide w/ Exponent Different Sign
                    if ((isProduct && powerSignIsSame) ||
                        (!isProduct && !powerSignIsSame))
                    {
                        newSet = new ProductQuotientSet(baseA, new PrimitiveUnit(2) * powerA, baseSign.IsNegative());
                    }
                    else
                    // Divide Exponent Same Sign
                    // Multiply Exponent Different Sign
                    {
                        newSet = new ProductQuotientSet(1, null, baseSign.IsNegative());
                    }
                }

                // (2) Same Base, Different exponent (ignore sign)
                if (baseIsSame && !exponentIsSame)
                {
                    SumDifferenceSet newPower = new SumDifferenceSet(powerA);
                    // Multiply Exponent Same Sign
                    // Divide w/ Exponent Different Sign
                    if (isProduct)
                    {
                        newPower.SumItem(powerB);
                    }
                    else
                    // Divide Exponent Same Sign
                    // Multiply Exponent Different Sign
                    {
                        newPower.SubtractItem(powerB);
                    }
                    newSet = new ProductQuotientSet(baseA, newPower, baseSign.IsNegative());
                }


                // (3) Different Base, Same exponent (ignore sign)
                if (!baseIsSame && exponentIsSame)
                {
                    newSet = new ProductQuotientSet(baseA, powerA, baseSign.IsNegative());

                    // Multiply Exponent Same Sign
                    // Divide w/ Exponent Different Sign
                    if ((isProduct && powerSignIsSame) ||
                        (!isProduct && !powerSignIsSame))
                    {
                        newSet.MultiplyItem(baseB);
                    }
                    else
                    // Divide Exponent Same Sign
                    // Multiply Exponent Different Sign
                    {
                        newSet.DivideItem(baseB);
                    }
                }


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
            newSet = new ProductQuotientSet(newSet.GetBase(), power, newSet.SignIsNegative());

            return newSet;
        }

        /// <summary>
        /// Simplifies the units of one.
        /// </summary>
        /// <param name="newSet">The new set.</param>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>ProductQuotientSet.</returns>
        protected ProductQuotientSet simplifyUnitsOfOne(ProductQuotientSet newSet, bool isRecursive = true)
        {
            // Simplify 
            if (isRecursive)
            {
                for (int i = 0; i < newSet.Count; i++)
                {
                    if (newSet._unitOperatorPair[i].Unit is IBaseSet unitSet)
                    {
                        newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateUnit(unitSet.SimplifyUnitsOfOne());
                    }
                }
            }

            if (Count < 2) return newSet;

            int firstIndex = unitIsRedundantSingular(0, newSet._unitOperatorPair) ? 1 : 0;
            ProductQuotientSet set = new ProductQuotientSet(newSet._unitOperatorPair[firstIndex].Unit, newSet.GetPower(), newSet.SignIsNegative());

            for (int j = firstIndex + 1; j < newSet.Count; j++)
            {
                if (unitIsRedundantSingular(j, newSet._unitOperatorPair)) continue;
                if (newSet._unitOperatorPair[j].OperatorEquals(Query.MULTIPLY))
                {
                    set.MultiplyItem(newSet._unitOperatorPair[j].Unit);
                }
                else if (newSet._unitOperatorPair[j].OperatorEquals(Query.DIVIDE))
                {
                    set.DivideItem(newSet._unitOperatorPair[j].Unit);
                }
            }

            return set;
        }

        /// <summary>
        /// Collects the inverse products.
        /// </summary>
        /// <param name="newSet">The new set.</param>
        /// <param name="isRecursive">if set to <c>true</c> [is recursive].</param>
        /// <returns>ProductQuotientSet.</returns>
        protected ProductQuotientSet collectInverseProducts(ProductQuotientSet newSet, bool isRecursive = true)
        {
            if (isRecursive)
            {
                for (int i = 0; i < newSet.Count; i++)
                {
                    if (newSet._unitOperatorPair[i].Unit is ProductQuotientSet unitSet)
                    {
                        newSet._unitOperatorPair[i] = newSet._unitOperatorPair[i].UpdateUnit(collectInverseProducts(unitSet, isRecursive: true));
                    }
                }
            }

            ProductQuotientSet collectedInverseProducts = newSet.CloneSet();
            if (collectedInverseProducts.Count == 0) return newSet;

            collectedInverseProducts._unitOperatorPair.Clear();
            collectedInverseProducts._unitOperatorPair.Add(new UnitOperatorPair(newSet._unitOperatorPair[0].Unit, Query.EMPTY));

            for (int j = 1; j < newSet.Count; j++)
            {
                if (newSet._unitOperatorPair[j].Unit is ProductQuotientSet productQuotientSet
                    && productQuotientSet.Count > 1
                    && productQuotientSet._unitOperatorPair[0].Unit.Label() == "1"
                    && productQuotientSet._unitOperatorPair[1].OperatorEquals(Query.DIVIDE))
                {
                    List<UnitOperatorPair> newUnitOperators = productQuotientSet._unitOperatorPair;
                    newUnitOperators.RemoveAt(0);

                    if (newUnitOperators.Count == 1)
                    {
                        newUnitOperators[0] = newUnitOperators[0].UpdateUnit(new ProductQuotientSet(newUnitOperators[0].Unit, productQuotientSet.GetPower()));
                    }
                    else
                    {
                        collectedInverseProducts.SetPower(productQuotientSet.GetPower());
                    }

                    foreach (var unitOperator in newUnitOperators)
                    {
                        // TODO: Deal with unit power
                        collectedInverseProducts._unitOperatorPair.Add(unitOperator);
                    }
                }
                else
                {
                    collectedInverseProducts._unitOperatorPair.Add(newSet._unitOperatorPair[j]);
                }
            }

            return collectedInverseProducts;
        }

        /// <summary>
        /// Simplifies the fractional.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="newSetGeneric">The new set generic.</param>
        /// <returns>ProductQuotientSet.</returns>
        protected override ProductQuotientSet simplifyFractional<T>(T newSetGeneric) 
        {
            if (!(newSetGeneric is ProductQuotientSet)) return new ProductQuotientSet(newSetGeneric);
            ProductQuotientSet newSet = newSetGeneric as ProductQuotientSet;
            if (newSet.Count < 2) return newSet;
            if (((newSet[0].Unit is PrimitiveUnit primitiveUnit0 && !primitiveUnit0.IsFraction())
                    || (newSet[0].Unit is ProductQuotientSet newSet0 && newSet0.Count == 1))
                && ((newSet[1].Unit is PrimitiveUnit primitiveUnit1 && !primitiveUnit1.IsFraction())
                    || (newSet[1].Unit is ProductQuotientSet newSet1 && newSet1.Count == 1))) return newSet;

            ProductQuotientSet leftSet = fractionSet(newSet, 0);
            for (int i = 1; i < newSet.Count; i++)
            {
                ProductQuotientSet rightSet = fractionSet(newSet, i);
                IBase unitA = leftSet._unitOperatorPair[0].Unit;
                IBase unitB = leftSet._unitOperatorPair[1].Unit;
                IBase unitC = rightSet._unitOperatorPair[0].Unit;
                IBase unitD = rightSet._unitOperatorPair[1].Unit;

                ProductQuotientSet numeratorSet = new ProductQuotientSet(unitA);
                ProductQuotientSet denominatorSet = new ProductQuotientSet(unitB);

                if (newSet._unitOperatorPair[i].OperatorEquals(Query.MULTIPLY))
                {
                    numeratorSet.MultiplyItem(unitC);
                    denominatorSet.MultiplyItem(unitD);
                }
                else if (newSet._unitOperatorPair[i].OperatorEquals(Query.DIVIDE))
                {
                    numeratorSet.MultiplyItem(unitD);
                    denominatorSet.MultiplyItem(unitC);
                }
                numeratorSet = simplifyNumeric<ProductQuotientSet>(numeratorSet);
                denominatorSet = simplifyNumeric<ProductQuotientSet>(denominatorSet);

                leftSet = new ProductQuotientSet(numeratorSet);
                leftSet.DivideItem(denominatorSet);
            }

            string simplifiedFraction = Query.SimplifiedFraction(leftSet.Label());
            return new ProductQuotientSet(simplifiedFraction);
        }

        /// <summary>
        /// Units the is redundant singular.
        /// </summary>
        /// <param name="unitIndex">Index of the unit.</param>
        /// <param name="units">The units.</param>
        /// <param name="operators">The operators.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        protected bool unitIsRedundantSingular(int unitIndex, List<UnitOperatorPair> unitOperatorPairs)
        {
            string unitsLabel = unitOperatorPairs[unitIndex].Unit.Label();
            string nextOperator = (unitOperatorPairs.Count > unitIndex + 1) ? unitOperatorPairs[unitIndex + 1].Operator : string.Empty;

            if (unitIndex == 0 && unitsLabel == "1")
            { // Case 1/a vs. 1*a
                return nextOperator != Query.DIVIDE.ToString();
            }

            // Case a/1, a*1, etc.
            return unitIndex != 0 && unitsLabel == "1";
        }

        protected Dictionary<string, int> extractVariableAndValue()
        {
            // Variables of the same text are treated as different if they have different powers. 
            List<string> variableLabels = new List<string>();
            List<int> variableCounts = new List<int>();
            List<bool> variableSigns = new List<bool>();

            // Populate variable lists with variable, count, and sign
            for (int i = 0; i < Count; i++)
            {
                string absoluteValue = _unitOperatorPair[i].Unit.GetAbsolute().Label();
                bool isCurrentUnitPositive = !_unitOperatorPair[i].Unit.SignIsNegative();

                // Get PowerCount
                string[] absoluteValueAndPower = absoluteValue.Split(Query.POWER);
                int power = 0;
                switch (absoluteValueAndPower.Length)
                {
                    case 1:
                        power = 1;
                        break;
                    case 2 when Query.IsNumeric(absoluteValueAndPower[1]):
                        absoluteValue = absoluteValueAndPower[0];
                        int.TryParse(absoluteValueAndPower[1], out power);
                        break;
                    default:
                        if (absoluteValueAndPower.Length > 2)
                        {
                            absoluteValue = absoluteValueAndPower[0];
                            // Recombine all segments except the last one
                            for (int j = 1; j < absoluteValueAndPower.Length - 1; j++)
                            {
                                absoluteValue += Query.POWER + absoluteValueAndPower[j];
                            }
                            int.TryParse(absoluteValueAndPower[absoluteValueAndPower.Length - 1], out power);
                        }

                        break;
                }

                if (!variableLabels.Contains(absoluteValue))
                {
                    variableLabels.Add(absoluteValue);
                    variableSigns.Add(isCurrentUnitPositive);
                    if (_unitOperatorPair[i].OperatorEquals(Query.MULTIPLY) || _unitOperatorPair[i].OperatorEquals(Query.EMPTY))
                    {
                        variableCounts.Add(power);
                    }
                    else if (_unitOperatorPair[i].OperatorEquals(Query.DIVIDE))
                    {
                        variableCounts.Add(-power);
                    }
                }
                else
                {
                    int variableIndex = variableLabels.IndexOf(absoluteValue);
                    variableSigns[variableIndex] = variableSigns[variableIndex] && isCurrentUnitPositive;

                    if (_unitOperatorPair[i].OperatorEquals(Query.MULTIPLY))
                    {
                        variableCounts[variableIndex] += power;
                    }
                    else if (_unitOperatorPair[i].OperatorEquals(Query.DIVIDE))
                    {
                        variableCounts[variableIndex] -= power;
                    }
                }
            }

            // Package lists into a dictionary with the resulting sign added to the label
            Dictionary<string, int> variables = new Dictionary<string, int>();
            for (int i = 0; i < variableLabels.Count; i++)
            {
                if (!variableSigns[i])
                {
                    variableLabels[i] = Sign.NEGATIVE + variableLabels[i];
                }
                variables.Add(variableLabels[i], variableCounts[i]);
            }

            return variables;
        }
        #endregion


        #region Operators

        public static SumDifferenceSet operator +(ProductQuotientSet set1, ProductQuotientSet set2)
        {
            checkNull(set1, set2);
            return bothAreNumeric(set1, set2) ? 
                new SumDifferenceSet(addNumericAsType(set1, set2)) : 
                addSymbolic(set1, set2);
        }

        public static SumDifferenceSet operator -(ProductQuotientSet set1, ProductQuotientSet set2)
        {
            checkNull(set1, set2);
            return bothAreNumeric(set1, set2) ?
                new SumDifferenceSet(subtractNumericAsType(set1, set2)) : 
                subtractSymbolic(set1, set2);
        }

        public static ProductQuotientSet operator *(ProductQuotientSet set1, ProductQuotientSet set2)
        {
            checkNull(set1, set2);
            return bothAreNumeric(set1, set2) ?
                new ProductQuotientSet(multiplyNumericAsType(set1, set2)) : 
                multiplySymbolic(set1, set2);
        }

        public static ProductQuotientSet operator /(ProductQuotientSet set1, ProductQuotientSet set2)
        {
            checkNull(set1, set2);
            return bothAreNumeric(set1, set2) ?
                new ProductQuotientSet(divideNumericAsType(set1, set2)) : 
                divideSymbolic(set1, set2);
        }
        #endregion

        #region Operators: Cross-Types IBase
        public static ProductQuotientSet operator *(ProductQuotientSet value1, IBase value)
        {
            checkNull(value1, value);
            value1 = value1.CloneSet();
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
        public static ProductQuotientSet operator *(IBase value, ProductQuotientSet value1)
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

        public static ProductQuotientSet operator /(ProductQuotientSet value1, IBase value)
        {
            checkNull(value1, value);
            value1 = value1.CloneSet();
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
        public static ProductQuotientSet operator /(IBase value, ProductQuotientSet value1)
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


        #region Operators: Cross-Types PrimitiveUnit
        public static SumDifferenceSet operator +(ProductQuotientSet set, PrimitiveUnit value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return new SumDifferenceSet(set) + value;
        }
        public static SumDifferenceSet operator +(PrimitiveUnit value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new SumDifferenceSet(value) + set;
        }


        public static SumDifferenceSet operator -(ProductQuotientSet set, PrimitiveUnit value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return new SumDifferenceSet(set) - value;
        }
        public static SumDifferenceSet operator -(PrimitiveUnit value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new SumDifferenceSet(value) - set;
        }

        public static ProductQuotientSet operator *(ProductQuotientSet set, PrimitiveUnit value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            set.MultiplyItem(value);
            return set;
        }
        public static ProductQuotientSet operator *(PrimitiveUnit value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new ProductQuotientSet(value) * set;
        }


        public static ProductQuotientSet operator /(ProductQuotientSet set, PrimitiveUnit value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            set.DivideItem(value);
            return set;
        }
        public static ProductQuotientSet operator /(PrimitiveUnit value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new ProductQuotientSet(value) / set;
        }

        #endregion

        #region Operators: Cross-Types SumDifferenceSet
        public static SumDifferenceSet operator +(ProductQuotientSet set, SumDifferenceSet value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return new SumDifferenceSet(set) + value;
        }
        public static SumDifferenceSet operator +(SumDifferenceSet value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return value + new SumDifferenceSet(set);
        }


        public static SumDifferenceSet operator -(ProductQuotientSet set, SumDifferenceSet value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return new SumDifferenceSet(set) - value;
        }
        public static SumDifferenceSet operator -(SumDifferenceSet value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return value - new SumDifferenceSet(set);
        }

        public static ProductQuotientSet operator *(ProductQuotientSet set, SumDifferenceSet value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set * new ProductQuotientSet(value);
        }
        public static ProductQuotientSet operator *(SumDifferenceSet value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new ProductQuotientSet(value) * set;
        }


        public static ProductQuotientSet operator /(ProductQuotientSet set, SumDifferenceSet value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set / new ProductQuotientSet(value);
        }
        public static ProductQuotientSet operator /(SumDifferenceSet value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new ProductQuotientSet(value) / set;
        }
        #endregion

        #region Operators: Cross-Types Integer
        public static SumDifferenceSet operator +(ProductQuotientSet set, int value)
        {
            checkNull(set);
            return set + new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator +(int value, ProductQuotientSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) + set;
        }


        public static SumDifferenceSet operator -(ProductQuotientSet set, int value)
        {
            checkNull(set);
            return set - new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator -(int value, ProductQuotientSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) - set;
        }

        public static ProductQuotientSet operator *(ProductQuotientSet set, int value)
        {
            checkNull(set);
            return set * new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator *(int value, ProductQuotientSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) * set;
        }


        public static ProductQuotientSet operator /(ProductQuotientSet set, int value)
        {
            checkNull(set);
            return set / new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator /(int value, ProductQuotientSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) / set;
        }
        #endregion
        
        #region Operators: Cross-Types Double
        public static SumDifferenceSet operator +(ProductQuotientSet set, double value)
        {
            checkNull(set);
            return set + new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator +(double value, ProductQuotientSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) + set;
        }


        public static SumDifferenceSet operator -(ProductQuotientSet set, double value)
        {
            checkNull(set);
            return set - new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator -(double value, ProductQuotientSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) - set;
        }

        public static ProductQuotientSet operator *(ProductQuotientSet set, double value)
        {
            checkNull(set);
            return set * new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator *(double value, ProductQuotientSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) * set;
        }


        public static ProductQuotientSet operator /(ProductQuotientSet set, double value)
        {
            checkNull(set);
            return set / new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator /(double value, ProductQuotientSet set)
        {
            checkNull(set);
            return new PrimitiveUnit(value) / set;
        }
        #endregion

        #region Operators: Cross-Types String
        public static SumDifferenceSet operator +(ProductQuotientSet set, string value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set + new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator +(string value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) + set;
        }


        public static SumDifferenceSet operator -(ProductQuotientSet set, string value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set - new PrimitiveUnit(value);
        }
        public static SumDifferenceSet operator -(string value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) - set;
        }

        public static ProductQuotientSet operator *(ProductQuotientSet set, string value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set * new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator *(string value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) * set;
        }


        public static ProductQuotientSet operator /(ProductQuotientSet set, string value)
        {
            checkNull(set, value);
            set = set.CloneSet();
            return set / new PrimitiveUnit(value);
        }
        public static ProductQuotientSet operator /(string value, ProductQuotientSet set)
        {
            checkNull(set, value);
            return new PrimitiveUnit(value) / set;
        }
        #endregion
    }
}