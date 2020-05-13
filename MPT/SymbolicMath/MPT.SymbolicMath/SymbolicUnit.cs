using System;
using System.Globalization;
using System.Linq;

namespace MPT.SymbolicMath
{
    public class SymbolicUnit
    {
        #region Constants
        private const char NEGATIVE = '-';

        // Operators
        private const char DIVISOR = '/';
        private const char ADD = '+';
        private const char SUBTRACT = '-';
        private const char MULTIPLY = '*';
        private const char DIVIDE = '/';
        private const char POWER = '^';

        // Other
        private const char OPEN_GROUP = '(';
        private const char CLOSE_GROUP = ')';
        private const string INFINITY = "Infinity";
        #endregion

        #region Fields
        private int _sign = 1;
        private string _value = string.Empty;
        private SymbolicUnit _power;
        #endregion

        #region Properties
        public static double TOLERANCE = 0.0001;                  
        public static bool STRICT = false;

        public bool IsInteger { get; private set; }
        public bool IsFloat { get; private set; }
        public bool IsNumber { get; private set; }
        #endregion


        #region Public

        public SymbolicUnit(SymbolicUnit value)
        {
            if (value == null)
            {
                return;

            }
            initialize(value.AsString(), value._power);
        }

        public SymbolicUnit(string value, SymbolicUnit power = null)
        {
            initialize(value, power);
        }

        public SymbolicUnit(int value, SymbolicUnit power = null)
        {
            _value = value.ToString();
            IsInteger = true;
            IsNumber = true;
            setSign(value >= 0);

            if (power != null)
            {
                _power = power;
            }
        }

        public SymbolicUnit(double value, SymbolicUnit power = null)
        {
            _value = value.ToString(CultureInfo.InvariantCulture);
            IsFloat = true;
            IsNumber = true;
            setSign(value >= 0);
            
            if (power != null)
            {
                _power = power;
            }
        }

        public SymbolicUnit Consolidate(bool isRecursive = false)
        {
            switch (_power.Label)
            {
                case "0":
                    return new SymbolicUnit(0);
                case "1":
                    return new SymbolicUnit(_value);
                default:
                    SymbolicUnit newPower = isRecursive ? _power.Consolidate(true) : new SymbolicUnit(_power);
                    return consolidateComplex(newPower);
            }
        }

        private SymbolicUnit consolidateComplex(SymbolicUnit newPower)
        {
            if (!IsNumber || !newPower.IsNumber)
            {
                return new SymbolicUnit(_value, newPower);
            }

            if (IsInteger && newPower.IsInteger)
            {
                return new SymbolicUnit((int)Math.Round(Math.Pow(AsInteger(), newPower.AsInteger())));
            }
            return new SymbolicUnit(Math.Pow(AsFloat(), newPower.AsFloat()));
        }

        public SymbolicUnit Simplify(bool isRecursive = false)
        {
            SymbolicUnit newPower = isRecursive ? _power.Simplify(true) : new SymbolicUnit(_power);

            // TODO: Complete Simplify
            return new SymbolicUnit(_value, newPower);
        }



        public string Label => signCharacter() + _value + powerLabel();

        // TODO: Value 'As' vs. Label 'As' vs. Power? 'As'
        public string AsString()
        {
            return _value;
        }

        public int AsInteger()
        {
            return (string.IsNullOrEmpty(_value) || !IsNumber) ? 0 : (int) Math.Round(double.Parse(_value));
        }

        public double AsFloat()
        {
            return (string.IsNullOrEmpty(_value) || !IsNumber) ? 0 : double.Parse(_value);
        }
        #endregion


        #region Private

        private static bool isNegative(string symbolicValue)
        {
            return symbolicValue[0] == NEGATIVE &&
                   (symbolicValue.Count(c => c == NEGATIVE) == 1);
        }

        private static bool basesAreSame(SymbolicUnit value1, SymbolicUnit value2)
        {
            return (value1.AsString() == value2.AsString());
        }

        private static bool powersAreSame(SymbolicUnit value1, SymbolicUnit value2)
        {
            return (value1.powerLabel() == value2.powerLabel());
        }

        
        private void initialize(string value, SymbolicUnit power = null)
        {
            if (string.IsNullOrEmpty(value)) return;

            IsNumber = double.TryParse(value, out var valueDouble);
            if (!IsNumber) return;

            IsInteger = int.TryParse(value, out var valueInt);
            IsFloat = !IsInteger;

            // TODO: Handle parsing here?
            _value = value;
            setSign(!isNegative(_value));
            
            if (power != null)
            {
                _power = power;
            }
        }

        private void setSign(bool isPositive)
        {
            _sign = isPositive ? 1 : -1;
        }

        private bool isNegative()
        {
            return _sign == -1;
        }

        private string signCharacter()
        {
            return isNegative() ? NEGATIVE.ToString() : string.Empty;
        }


        private string powerLabel()
        {
            return hasPower() ? POWER + _power.Label : string.Empty;
        }
        
        private bool hasPower()
        {
            return !(_power == null || 
                     (_power.IsInteger && _power.AsInteger() == 1));
        }
        
        #endregion

        #region Overrides

        public override string ToString()
        {
            return !string.IsNullOrEmpty(_value) ? Label : base.ToString();
        }

        #endregion

        #region Operators

        // Math
        public static SymbolicUnit operator+ (SymbolicUnit value1, SymbolicUnit value2)
        {
            // Null
            if (string.IsNullOrEmpty(value1.AsString()))
            {
                return value2;
            }
            if (string.IsNullOrEmpty(value2.AsString()))
            {
                return value1;
            }

            // Exponent
            if (basesAreSame(value1, value2) && powersAreSame(value1, value2))
            {
                return new SymbolicUnit(value1.AsString() + value2.AsString(), value1._power);
            }
            if (STRICT && value1.hasPower())
            {
                throw new ArgumentException($"Exponent is not compatible for operand {value1.Label}. Consolidate operand before operation");
            }
            if (STRICT && value2.hasPower())
            {
                throw new ArgumentException($"Exponent is not compatible for operand {value2.Label}. Consolidate operand before operation");
            }
            if (value1.hasPower() || value2.hasPower())
            {
                return new SymbolicUnit(value1.Consolidate() + value2.Consolidate());
            }

            // No Exponent
            if (value1.IsInteger && value2.IsInteger)
            {
                return new SymbolicUnit(value1.AsInteger() + value2.AsInteger());
            }
            if (value1.IsNumber && value2.IsNumber)
            {
                return new SymbolicUnit(value1.AsFloat() + value2.AsFloat());
            }
            return new SymbolicUnit(value1.AsString() + ADD + value2.AsString());
        }

        public static SymbolicUnit operator -(SymbolicUnit value1, SymbolicUnit value2)
        {
            // Null
            if (string.IsNullOrEmpty(value1.AsString()) || string.IsNullOrEmpty(value2.AsString()))
            {
                return new SymbolicUnit(string.Empty);
            }

            // Exponent
            if (basesAreSame(value1, value2) && powersAreSame(value1, value2))
            {
                return new SymbolicUnit(0);
            }
            if (STRICT && value1.hasPower())
            {
                throw new ArgumentException($"Exponent is not compatible for operand {value1.Label}. Consolidate operand before operation");
            }
            if (STRICT && value2.hasPower())
            {
                throw new ArgumentException($"Exponent is not compatible for operand {value2.Label}. Consolidate operand before operation");
            }
            if (value1.hasPower() || value2.hasPower())
            {
                return new SymbolicUnit(value1.Consolidate() - value2.Consolidate());
            }

            // No Exponent
            if (value1.IsInteger && value2.IsInteger)
            {
                return new SymbolicUnit(value1.AsInteger() - value2.AsInteger());
            }
            if (value1.IsNumber && value2.IsNumber)
            {
                return new SymbolicUnit(value1.AsFloat() - value2.AsFloat());
            }
            return new SymbolicUnit(value1.AsString() + SUBTRACT + value2.AsString());
        }

        public static SymbolicUnit operator *(SymbolicUnit value1, SymbolicUnit value2)
        {
            // Null
            if (string.IsNullOrEmpty(value1.AsString()) || string.IsNullOrEmpty(value2.AsString()))
            {
                return new SymbolicUnit(string.Empty);
            }

            // Exponent
            if (basesAreSame(value1, value2))
            {
                return new SymbolicUnit(value1.AsString(), value1._power + value2._power);
            }
            if (STRICT && !powersAreSame(value1, value2))
            {
                throw new ArgumentException($"Exponents are not identical for operands {value1.Label} & {value2.Label}. Consolidate operand before operation");
            }
            if (!powersAreSame(value1, value2))
            {
                return new SymbolicUnit(value1.Consolidate() * value2.Consolidate());
            }

            // Identical Exponent
            if (value1.IsInteger && value2.IsInteger)
            {
                return new SymbolicUnit(value1.AsInteger() * value2.AsInteger(), value1._power);
            }
            if (value1.IsNumber && value2.IsNumber)
            {
                return new SymbolicUnit(value1.AsFloat() * value2.AsFloat(), value1._power);
            }
            return new SymbolicUnit(value1.AsString() + MULTIPLY + value2.AsString(), value1._power);
        }

        public static SymbolicUnit operator /(SymbolicUnit value1, SymbolicUnit value2)
        {
            // Null
            if (string.IsNullOrEmpty(value1.AsString()) || string.IsNullOrEmpty(value2.AsString()))
            {
                return new SymbolicUnit(string.Empty);
            }

            // Exponent
            if (basesAreSame(value1, value2))
            {
                return new SymbolicUnit(value1.AsString(), value1._power - value2._power);
            }
            if (STRICT && !powersAreSame(value1, value2))
            {
                throw new ArgumentException($"Exponents are not identical for operands {value1.Label} & {value2.Label}. Consolidate operand before operation");
            }
            if (!powersAreSame(value1, value2))
            {
                return new SymbolicUnit(value1.Consolidate() / value2.Consolidate());
            }

            // Identical Exponent
            if (value1.IsInteger && value2.IsInteger)
            {
                return new SymbolicUnit(value1.AsInteger() / value2.AsInteger(), value1._power);
            }
            if (value1.IsNumber && value2.IsNumber)
            {
                return new SymbolicUnit(value1.AsFloat() / value2.AsFloat(), value1._power);
            }
            return new SymbolicUnit(value1.AsString() + DIVIDE + value2.AsString(), value1._power);
        }

        // TODO: Add consolidation to these
        // Comparison
        public static bool operator ==(SymbolicUnit value1, SymbolicUnit value2)
        {
            if (ReferenceEquals(value1, null))
            {
                return ReferenceEquals(value2, null);
            }
            
            if (string.IsNullOrEmpty(value1.AsString()) && string.IsNullOrEmpty(value2.AsString()))
                return true;
            if (string.IsNullOrEmpty(value1.AsString()) || string.IsNullOrEmpty(value2.AsString()))
                return false;

            if (value1.IsInteger && value2.IsInteger)
                return (value1.AsInteger() == value2.AsInteger());
            if (value1.IsNumber && value2.IsNumber)
                return (Math.Abs(value1.AsFloat() - value2.AsFloat()) < TOLERANCE);

            return (string.CompareOrdinal(value1.AsString(), value2.AsString()) == 0);
        }

        public static bool operator !=(SymbolicUnit value1, SymbolicUnit value2)
        {
            if (ReferenceEquals(value1, null))
            {
                return !ReferenceEquals(value2, null);
            }

            if (string.IsNullOrEmpty(value1.AsString()) && string.IsNullOrEmpty(value2.AsString()))
                return false;
            if (string.IsNullOrEmpty(value1.AsString()) || string.IsNullOrEmpty(value2.AsString()))
                return true;

            if (value1.IsInteger && value2.IsInteger)
                return (value1.AsInteger() != value2.AsInteger());
            if (value1.IsNumber && value2.IsNumber)
                return !(Math.Abs(value1.AsFloat() - value2.AsFloat()) < SymbolicUnit.TOLERANCE);

            return (string.CompareOrdinal(value1.AsString(), value2.AsString()) != 0);
        }

        public static bool operator >(SymbolicUnit value1, SymbolicUnit value2)
        {
            if (string.IsNullOrEmpty(value1.AsString()) && string.IsNullOrEmpty(value2.AsString()))
                return false;
            if (string.IsNullOrEmpty(value2.AsString()))
                return true;

            if (value1.IsInteger && value2.IsInteger)
                return (value1.AsInteger() > value2.AsInteger());
            if (value1.IsNumber && value2.IsNumber)
                return (value1.AsFloat() > value2.AsFloat());

            return (string.CompareOrdinal(value1.AsString(), value2.AsString()) > 0);
        }

        public static bool operator <(SymbolicUnit value1, SymbolicUnit value2)
        {
            if (string.IsNullOrEmpty(value1.AsString()) && string.IsNullOrEmpty(value2.AsString()))
                return false;
            if (string.IsNullOrEmpty(value1.AsString()))
                return true;

            if (value1.IsInteger && value2.IsInteger)
                return (value1.AsInteger() < value2.AsInteger());
            if (value1.IsNumber && value2.IsNumber)
                return (value1.AsFloat() < value2.AsFloat());

            return (string.CompareOrdinal(value1.AsString(), value2.AsString()) < 0);
        }

        public static bool operator >=(SymbolicUnit value1, SymbolicUnit value2)
        {
            if ((string.IsNullOrEmpty(value1.AsString()) && string.IsNullOrEmpty(value2.AsString())) || 
                string.IsNullOrEmpty(value2.AsString()))
                return true;
            if (string.IsNullOrEmpty(value1.AsString()))
                return false;

            if (value1.IsInteger && value2.IsInteger)
                return (value1.AsInteger() >= value2.AsInteger());
            if (value1.IsNumber && value2.IsNumber)
                return (value1.AsFloat() >= value2.AsFloat());

            return (string.CompareOrdinal(value1.AsString(), value2.AsString()) >= 0);
        }

        public static bool operator <=(SymbolicUnit value1, SymbolicUnit value2)
        {
            if ((string.IsNullOrEmpty(value1.AsString()) && string.IsNullOrEmpty(value2.AsString())) || 
                 (string.IsNullOrEmpty(value1.AsString())))
                return true;
            if (string.IsNullOrEmpty(value2.AsString()))
                return false;

            if (value1.IsInteger && value2.IsInteger)
                return (value1.AsInteger() <= value2.AsInteger());
            if (value1.IsNumber && value2.IsNumber)
                return (value1.AsFloat() <= value2.AsFloat());

            return (string.CompareOrdinal(value1.AsString(), value2.AsString()) <= 0);
        }

        #endregion


    }
}
