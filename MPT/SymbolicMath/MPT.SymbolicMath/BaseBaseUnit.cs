using System.Linq;

namespace MPT.SymbolicMath
{
    public abstract class BaseBaseUnit
    {
        #region Constants
        // Operators
        protected const char ADD = '+';
        protected const char SUBTRACT = '-';
        protected const char MULTIPLY = '*';
        protected const char DIVIDE = '/';
        protected const char POWER = '^';

        // Other
        protected const char OPEN_GROUP = '(';
        protected const char CLOSE_GROUP = ')';
        protected const string INFINITY = "Infinity";
        #endregion

        #region Properties
        public static bool STRICT = false;

        protected bool _baseIsNumber;
        protected bool _baseIsInteger;
        protected bool _baseIsFloat;
        protected bool _baseIsFraction;

        protected Sign _sign = new Sign();
        protected IBase _power;
        #endregion

        #region Public
        
        public IBase GetPower()
        {
            return (IBase)_power.Clone();
        }

        public Sign GetSign()
        {
            return new Sign(_sign.Label());
        }

        public void FlipSign()
        {
            _sign.FlipSign();
        }

        public string powerLabel()
        {
            return hasPower() ? POWER + _power.Label() : string.Empty;
        }

        protected string label(string baseItem, bool addParenthesesToBase = false)
        {
            string baseLabel = addParenthesesToBase ? addParentheses(baseItem) : baseItem;

            return _sign * baseLabel + powerLabel();
        }


        public bool IsInteger()
        {
            return _baseIsInteger &&
                   (!hasPower() || _power.IsInteger());
        }

        public bool IsFloat()
        {
            return _baseIsFloat &&
                   (!hasPower() || _power.IsFloat());
        }

        public bool IsFraction()
        {
            return _baseIsFraction &&
                   (!hasPower() || _power.IsFraction());
        }

        public bool IsNumber()
        {
            return _baseIsNumber &&
                   (!hasPower() || _power.IsNumber());
        }

        public bool IsSymbolic()
        {
            return !IsNumber();
        }

        #endregion

        #region Private

        protected void addPower(IBase power)
        {
            if (!power.IsEmpty())
            {
                _power = power;
            }
        }

        protected bool hasPower()
        {
            return !(_power == null ||
                     (_power.Label() == "1"));
        }

        private static bool exponentIsZero(IBase value)
        {
            IBase power = value.GetPower();
            return (string.IsNullOrEmpty(power.Label()) || power.Label() == "0");
        }

        private static bool exponentIsOne(IBase value)
        {
            return (value.GetPower().Label() == "1");
        }

        private static bool powersAreSame(IBase value1, IBase value2)
        {
            return (value1.powerLabel() == value2.powerLabel());
        }
        #endregion

        #region Public: Static
        public static bool IsEmpty(IBase value)
        {
            return (value == null || value.IsEmpty());
        }
        #endregion

        #region Private: Static


        protected static bool isFraction(string value)
        {
            string[] values = value.Split('/');
            return ((values.Length == 2) && (isNumeric(values[0]) && isNumeric(values[1])));
        }

        protected static bool isNumeric(string value)
        {
            return (double.TryParse(value, out var valueDouble) && value.Count(x => x == '.') <= 1);
        }

        protected static bool isInteger(string value)
        {
            return int.TryParse(value, out var valueInt);
        }


        protected static string addParentheses(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return OPEN_GROUP + value + CLOSE_GROUP;
        }


        protected static string removeParentheses(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < 3) return value;
            if (value[0] == OPEN_GROUP && value[value.Length - 1] == CLOSE_GROUP)
            {
                return value.Substring(1, value.Length - 2);
            }

            return value;
        }
        #endregion
    }
}
