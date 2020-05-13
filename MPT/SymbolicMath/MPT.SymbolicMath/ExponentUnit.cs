using System;

namespace MPT.SymbolicMath
{

    //public class ExponentUnit : BaseBaseUnit,
    //    IExponentUnit
    //{
    //    #region Fields
    //    protected IBase _base = new BaseUnit(0);
    //    #endregion

    //    #region Properties
    //    public static double TOLERANCE = 0.0001;
    //    #endregion


    //    #region Public
    //    public ExponentUnit(IExponentUnit value)
    //    {
    //        if (value == null)
    //        {
    //            return;

    //        }
    //        initialize(value.BaseAsString(), value.Power());
    //    }

    //    public ExponentUnit(IBase value)
    //    {
    //        if (value == null)
    //        {
    //            return;

    //        }
    //        initialize(value.Label());
    //    }

    //    public ExponentUnit(string value, IBase power = null)
    //    {
    //        initialize(value, power);
    //    }

    //    public ExponentUnit(int value, IBase power = null)
    //    {
    //        _base = new BaseUnit(value);
    //        addPower(power);
    //    }

    //    public ExponentUnit(double value, IBase power = null)
    //    {
    //        _base = new BaseUnit(value);
    //        addPower(power);
    //    }
        
    //    public IBase GetBase()
    //    {
    //        return (IBase)_base.Clone();
    //    }
       
    //    public bool IsEmpty()
    //    {
    //       return _base.IsEmpty();
    //    }

    //    public int AsInteger()
    //    {
    //        return (int)Math.Round(Calculate());
    //    }

    //    public double AsFloat()
    //    {
    //        return Calculate();
    //    }


    //    public string Label()
    //    {
    //        string baseLabel = _base.IsFraction() ? addParentheses(_base.Label()) : _base.Label();

    //        return baseLabel + PowerLabel();
    //    }
        

    //    public string BaseAsString()
    //    {
    //        return _base.Label();
    //    }

    //    public int BaseAsInteger()
    //    {
    //        return (!_base.IsNumber()) ? 0 : _base.AsInteger();
    //    }

    //    public double BaseAsFloat()
    //    {
    //        return (!_base.IsNumber()) ? 0 : _base.AsFloat();
    //    }

    //    public double Calculate()
    //    {
    //        if (STRICT && 
    //            (!_base.IsNumber() || !_power.IsNumber()))
    //        {
    //            throw new Exception($"GetBase or GetPower is non-numeric. Cannot Calculate a numeric value with {Label()}");
    //        }

    //        return _power == null ? BaseAsFloat() : Math.Pow(BaseAsFloat(), _power.Calculate());
    //    }

    //    public IBase Simplify(bool isRecursive = false)
    //    {
    //        //ExponentUnit newPower = isRecursive ? _power.Simplify(true) : new ExponentUnit(_power);

    //        //// TODO: Complete Simplify
    //        //return new ExponentUnit(_base.AsString(), newPower);
    //        throw new NotImplementedException();
    //    }


    //    public ExponentUnit ConsolidateType(bool isRecursive = false)
    //    {
    //        return (ExponentUnit) Consolidate(isRecursive);
    //    }

    //    public IBase Consolidate(bool isRecursive = false)
    //    {
    //        switch (_power.Label())
    //        {
    //            case "0":
    //                return new ExponentUnit(0);
    //            case "1":
    //                return new ExponentUnit(_base.Label());
    //            default:
    //                IBase newPower = isRecursive ? _power.Consolidate(true) : new ExponentUnit(_power);
    //                return consolidateComplex(newPower);
    //        }
    //    }
    //    #endregion


    //    #region Private
    //    private static bool basesAreSame(ExponentUnit value1, ExponentUnit value2)
    //    {
    //        return (value1.BaseAsString() == value2.BaseAsString());
    //    }

    //    private static bool powersAreSame(ExponentUnit value1, ExponentUnit value2)
    //    {
    //        return (value1.PowerLabel() == value2.PowerLabel());
    //    }


    //    private void initialize(string value, IBase power = null)
    //    {
    //        if (string.IsNullOrEmpty(value)) return;
            
    //        _base = new BaseUnit(value);
    //        addPower(power);
    //    }



    //    private ExponentUnit consolidateComplex(IBase newPower)
    //    {
    //        if (!IsNumber() || !newPower.IsNumber())
    //        {
    //            return new ExponentUnit(_base.Label(), newPower);
    //        }

    //        if (IsFraction())
    //        {
    //            return new ExponentUnit(BaseAsString());
    //        }
    //        if (IsInteger() && newPower.IsInteger())
    //        {
    //            return new ExponentUnit((int)Math.Round(Math.Pow(BaseAsInteger(), newPower.AsInteger())));
    //        }
    //        return new ExponentUnit(Math.Pow(BaseAsFloat(), newPower.AsFloat()));
    //    }
    //    #endregion

    //    #region Overrides        
    //    public override string ToString()
    //    {
    //        string label = Label();
    //        return !string.IsNullOrEmpty(label) ? label : base.ToString();
    //    }


    //    public ExponentUnit CloneUnit()
    //    {
    //        return (ExponentUnit)Clone();
    //    }

    //    public object Clone()
    //    {
    //        return new ExponentUnit(this);
    //    }
    //    #endregion

    //    #region Operators

    //    // 1. Null

    //    // 2. Exponent = 0
        
    //    // 3. No exponent, exponent = 1
        
    //    // 4. Exponent



    //    // Math
    //    public static ExponentUnit operator +(ExponentUnit value1, ExponentUnit value2)
    //    {
    //        // 1. Null
    //        if (value1 == null || string.IsNullOrEmpty(value1.BaseAsString()))
    //        {
    //            return value2;
    //        }
    //        if (value2 == null || string.IsNullOrEmpty(value2.BaseAsString()))
    //        {
    //            return value1;
    //        }

    //        // 2. No exponent, Exponent = 0
    //        if (exponentIsZero(value1) && exponentIsZero(value2))
    //        {
    //            return new ExponentUnit(0);
    //        }
    //        if (exponentIsZero(value1))
    //        {
    //            return value2;
    //        }
    //        if (exponentIsZero(value2))
    //        {
    //            return value1;
    //        }
            
    //        // 3. Exponent = 1
    //        if (exponentIsOne(value1) && exponentIsOne(value2))
    //        {
    //            //return new ExponentUnit((value1.GetBase() + value2.GetBase()).Label());
    //        }


    //        // 4. Exponent


    //        // Exponent
    //        if (basesAreSame(value1, value2) && powersAreSame(value1, value2))
    //        {
    //            return new ExponentUnit(value1.BaseAsString() + value2.BaseAsString(), value1._power);
    //        }
    //        if (STRICT && value1.hasPower())
    //        {
    //            throw new ArgumentException($"Exponent is not compatible for operand {value1.Label()}. Consolidate operand before operation");
    //        }
    //        if (STRICT && value2.hasPower())
    //        {
    //            throw new ArgumentException($"Exponent is not compatible for operand {value2.Label()}. Consolidate operand before operation");
    //        }
    //        if (value1.hasPower() || value2.hasPower())
    //        {
    //            return new ExponentUnit((ExponentUnit)value1.Consolidate() + (ExponentUnit)value2.Consolidate());
    //        }

    //        // No Exponent
    //        if (value1._base.IsInteger() && value2._base.IsInteger())
    //        {
    //            return new ExponentUnit(value1.BaseAsInteger() + value2.BaseAsInteger());
    //        }
    //        if (value1._base.IsNumber() && value2._base.IsNumber())
    //        {
    //            return new ExponentUnit(value1.BaseAsFloat() + value2.BaseAsFloat());
    //        }
    //        return new ExponentUnit(value1.BaseAsString() + ADD + value2.BaseAsString());
    //    }

    //    public static ExponentUnit operator -(ExponentUnit value1, ExponentUnit value2)
    //    {
    //        // Null
    //        if (string.IsNullOrEmpty(value1.BaseAsString()) || string.IsNullOrEmpty(value2.BaseAsString()))
    //        {
    //            return new ExponentUnit(string.Empty);
    //        }

    //        // Exponent
    //        if (basesAreSame(value1, value2) && powersAreSame(value1, value2))
    //        {
    //            return new ExponentUnit(0);
    //        }
    //        if (STRICT && value1.hasPower())
    //        {
    //            throw new ArgumentException($"Exponent is not compatible for operand {value1.Label()}. Consolidate operand before operation");
    //        }
    //        if (STRICT && value2.hasPower())
    //        {
    //            throw new ArgumentException($"Exponent is not compatible for operand {value2.Label()}. Consolidate operand before operation");
    //        }
    //        if (value1.hasPower() || value2.hasPower())
    //        {
    //            return new ExponentUnit((ExponentUnit)value1.Consolidate() - (ExponentUnit)value2.Consolidate());
    //        }

    //        // No Exponent
    //        if (value1._base.IsInteger() && value2._base.IsInteger())
    //        {
    //            return new ExponentUnit(value1.BaseAsInteger() - value2.BaseAsInteger());
    //        }
    //        if (value1._base.IsNumber() && value2._base.IsNumber())
    //        {
    //            return new ExponentUnit(value1.BaseAsFloat() - value2.BaseAsFloat());
    //        }
    //        return new ExponentUnit(value1.BaseAsString() + SUBTRACT + value2.BaseAsString());
    //    }

    //    //public static ExponentUnit operator *(ExponentUnit value1, ExponentUnit value2)
    //    //{
    //    //    // Null
    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()) || string.IsNullOrEmpty(value2.BaseAsString()))
    //    //    {
    //    //        return new ExponentUnit(string.Empty);
    //    //    }

    //    //    // Exponent
    //    //    if (basesAreSame(value1, value2))
    //    //    {
    //    //        return new ExponentUnit(value1.BaseAsString(), value1._power + value2._power);
    //    //    }
    //    //    if (_STRICT && !powersAreSame(value1, value2))
    //    //    {
    //    //        throw new ArgumentException($"Exponents are not identical for operands {value1.Label()} & {value2.Label()}. Consolidate operand before operation");
    //    //    }
    //    //    if (!powersAreSame(value1, value2))
    //    //    {
    //    //        return new ExponentUnit(value1.Consolidate() * value2.Consolidate());
    //    //    }

    //    //    // Identical Exponent
    //    //    if (value1._base.IsInteger && value2._base.IsInteger)
    //    //    {
    //    //        return new ExponentUnit(value1.BaseAsInteger() * value2.BaseAsInteger(), value1._power);
    //    //    }
    //    //    if (value1._base.IsNumber && value2._base.IsNumber)
    //    //    {
    //    //        return new ExponentUnit(value1.BaseAsFloat() * value2.BaseAsFloat(), value1._power);
    //    //    }
    //    //    return new ExponentUnit(value1.BaseAsString() + MULTIPLY + value2.BaseAsString(), value1._power);
    //    //}

    //    //public static ExponentUnit operator /(ExponentUnit value1, ExponentUnit value2)
    //    //{
    //    //    // Null
    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()) || string.IsNullOrEmpty(value2.BaseAsString()))
    //    //    {
    //    //        return new ExponentUnit(string.Empty);
    //    //    }

    //    //    // Exponent
    //    //    if (basesAreSame(value1, value2))
    //    //    {
    //    //        return new ExponentUnit(value1.BaseAsString(), value1._power - value2._power);
    //    //    }
    //    //    if (_STRICT && !powersAreSame(value1, value2))
    //    //    {
    //    //        throw new ArgumentException($"Exponents are not identical for operands {value1.Label()} & {value2.Label()}. Consolidate operand before operation");
    //    //    }
    //    //    if (!powersAreSame(value1, value2))
    //    //    {
    //    //        return new ExponentUnit(value1.Consolidate() / value2.Consolidate());
    //    //    }

    //    //    // Identical Exponent
    //    //    if (value1._base.IsInteger && value2._base.IsInteger)
    //    //    {
    //    //        return new ExponentUnit(value1.BaseAsInteger() / value2.BaseAsInteger(), value1._power);
    //    //    }
    //    //    if (value1._base.IsNumber && value2._base.IsNumber)
    //    //    {
    //    //        return new ExponentUnit(value1.BaseAsFloat() / value2.BaseAsFloat(), value1._power);
    //    //    }
    //    //    return new ExponentUnit(value1.BaseAsString() + DIVIDE + value2.BaseAsString(), value1._power);
    //    //}

    //    //// TODO: Add consolidation to these
    //    //// Comparison
    //    //public static bool operator ==(ExponentUnit value1, ExponentUnit value2)
    //    //{
    //    //    if (ReferenceEquals(value1, null))
    //    //    {
    //    //        return ReferenceEquals(value2, null);
    //    //    }

    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()) && string.IsNullOrEmpty(value2.BaseAsString()))
    //    //        return true;
    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()) || string.IsNullOrEmpty(value2.BaseAsString()))
    //    //        return false;

    //    //    if (value1._base.IsInteger && value2._base.IsInteger)
    //    //        return (value1.BaseAsInteger() == value2.BaseAsInteger());
    //    //    if (value1._base.IsNumber && value2._base.IsNumber)
    //    //        return (Math.Abs(value1.BaseAsFloat() - value2.BaseAsFloat()) < TOLERANCE);

    //    //    return (string.CompareOrdinal(value1.BaseAsString(), value2.BaseAsString()) == 0);
    //    //}

    //    //public static bool operator !=(ExponentUnit value1, ExponentUnit value2)
    //    //{
    //    //    if (ReferenceEquals(value1, null))
    //    //    {
    //    //        return !ReferenceEquals(value2, null);
    //    //    }

    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()) && string.IsNullOrEmpty(value2.BaseAsString()))
    //    //        return false;
    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()) || string.IsNullOrEmpty(value2.BaseAsString()))
    //    //        return true;

    //    //    if (value1._base.IsInteger && value2._base.IsInteger)
    //    //        return (value1.BaseAsInteger() != value2.BaseAsInteger());
    //    //    if (value1._base.IsNumber && value2._base.IsNumber)
    //    //        return !(Math.Abs(value1.BaseAsFloat() - value2.BaseAsFloat()) < ExponentUnit.TOLERANCE);

    //    //    return (string.CompareOrdinal(value1.BaseAsString(), value2.BaseAsString()) != 0);
    //    //}

    //    //public static bool operator >(ExponentUnit value1, ExponentUnit value2)
    //    //{
    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()) && string.IsNullOrEmpty(value2.BaseAsString()))
    //    //        return false;
    //    //    if (string.IsNullOrEmpty(value2.BaseAsString()))
    //    //        return true;

    //    //    if (value1._base.IsInteger && value2._base.IsInteger)
    //    //        return (value1.BaseAsInteger() > value2.BaseAsInteger());
    //    //    if (value1._base.IsNumber && value2._base.IsNumber)
    //    //        return (value1.BaseAsFloat() > value2.BaseAsFloat());

    //    //    return (string.CompareOrdinal(value1.BaseAsString(), value2.BaseAsString()) > 0);
    //    //}

    //    //public static bool operator <(ExponentUnit value1, ExponentUnit value2)
    //    //{
    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()) && string.IsNullOrEmpty(value2.BaseAsString()))
    //    //        return false;
    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()))
    //    //        return true;

    //    //    if (value1._base.IsInteger && value2._base.IsInteger)
    //    //        return (value1.BaseAsInteger() < value2.BaseAsInteger());
    //    //    if (value1._base.IsNumber && value2._base.IsNumber)
    //    //        return (value1.BaseAsFloat() < value2.BaseAsFloat());

    //    //    return (string.CompareOrdinal(value1.BaseAsString(), value2.BaseAsString()) < 0);
    //    //}

    //    //public static bool operator >=(ExponentUnit value1, ExponentUnit value2)
    //    //{
    //    //    if ((string.IsNullOrEmpty(value1.BaseAsString()) && string.IsNullOrEmpty(value2.BaseAsString())) ||
    //    //        string.IsNullOrEmpty(value2.BaseAsString()))
    //    //        return true;
    //    //    if (string.IsNullOrEmpty(value1.BaseAsString()))
    //    //        return false;

    //    //    if (value1._base.IsInteger && value2._base.IsInteger)
    //    //        return (value1.BaseAsInteger() >= value2.BaseAsInteger());
    //    //    if (value1._base.IsNumber && value2._base.IsNumber)
    //    //        return (value1.BaseAsFloat() >= value2.BaseAsFloat());

    //    //    return (string.CompareOrdinal(value1.BaseAsString(), value2.BaseAsString()) >= 0);
    //    //}

    //    //public static bool operator <=(ExponentUnit value1, ExponentUnit value2)
    //    //{
    //    //    if ((string.IsNullOrEmpty(value1.BaseAsString()) && string.IsNullOrEmpty(value2.BaseAsString())) ||
    //    //         (string.IsNullOrEmpty(value1.BaseAsString())))
    //    //        return true;
    //    //    if (string.IsNullOrEmpty(value2.BaseAsString()))
    //    //        return false;

    //    //    if (value1._base.IsInteger && value2._base.IsInteger)
    //    //        return (value1.BaseAsInteger() <= value2.BaseAsInteger());
    //    //    if (value1._base.IsNumber && value2._base.IsNumber)
    //    //        return (value1.BaseAsFloat() <= value2.BaseAsFloat());

    //    //    return (string.CompareOrdinal(value1.BaseAsString(), value2.BaseAsString()) <= 0);
    //    //}

    //    #endregion


    //}
}
