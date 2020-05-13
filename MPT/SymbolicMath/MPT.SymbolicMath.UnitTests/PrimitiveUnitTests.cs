using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{
    [TestFixture]
    public class PrimitiveUnitTests
    {
        #region Initialize
        [TestCase("A", "A")]
        [TestCase("-A", "-A")]
        [TestCase("A^n", "A^n")]
        [TestCase("-A^n", "-A^n")]
        [TestCase("A^-n", "A^-n")]
        [TestCase("-A^-n", "-A^-n")]
        [TestCase("A^(n^m)", "A^(n^m)")]
        [TestCase("-A^(-n^-m)", "-A^(-n^-m)")]
        [TestCase("-A^(n^-m)", "-A^(n^-m)")]
        [TestCase("A^(-n^m)", "A^(-n^m)")]
        public void Initialize_As_String_Primitives(string value, string expectedLabel)
        {
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value);

            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicValue.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase(5, "5", "", "5")]
        [TestCase(-5, "-5", "", "-5")]
        [TestCase(0, "0", "", "0")]
        public void Initialize_As_Integer_No_Exponent(int value, string expectedBaseLabel, string expectedPowerLabel, string expectedLabel)
        {
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value);

            Assert.IsTrue(symbolicValue.IsNumber());
            Assert.IsFalse(symbolicValue.IsFloat());
            Assert.IsTrue(symbolicValue.IsInteger());
            Assert.IsFalse(symbolicValue.IsFraction());
            Assert.IsFalse(symbolicValue.IsSymbolic());

            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicValue.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicValue.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase(5.6, "5.6", "", "5.6")]
        [TestCase(5.5, "5.5", "", "5.5")]
        [TestCase(5.4, "5.4", "", "5.4")]
        [TestCase(-5.6, "-5.6", "", "-5.6")]
        [TestCase(-5.5, "-5.5", "", "-5.5")]
        [TestCase(-5.4, "-5.4", "", "-5.4")]
        [TestCase(0.0, "0", "", "0")]
        public void Initialize_As_Float_No_Exponent(double value, string expectedBaseLabel, string expectedPowerLabel, string expectedLabel)
        {
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value);

            Assert.IsTrue(symbolicValue.IsNumber());
            Assert.IsTrue(symbolicValue.IsFloat());
            Assert.IsFalse(symbolicValue.IsInteger());
            Assert.IsFalse(symbolicValue.IsFraction());
            Assert.IsFalse(symbolicValue.IsSymbolic());

            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicValue.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicValue.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase("1", "1", "", "1", true, false)]
        [TestCase("-1", "-1", "", "-1", true, false)]
        [TestCase("25.3", "25.3", "", "25.3", false, false)]
        [TestCase("-25.3", "-25.3", "", "-25.3", false, false)]
        [TestCase("-25.", "-25", "", "-25", true, false)]
        [TestCase("1/2", "1/2", "", "1/2", false, true)]
        public void Initialize_Numeric_As_String_No_Exponent(string value, string expectedBaseLabel, string expectedPowerLabel, string expectedLabel, bool isInteger, bool isFraction)
        {
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value);

            Assert.IsTrue(symbolicValue.IsNumber());
            Assert.That(symbolicValue.IsFloat, Is.Not.EqualTo(isInteger));
            Assert.That(symbolicValue.IsInteger, Is.EqualTo(isInteger));
            Assert.That(symbolicValue.IsFraction, Is.EqualTo(isFraction));
            Assert.IsFalse(symbolicValue.IsSymbolic());

            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicValue.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicValue.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase(null, "", "", "")]
        [TestCase("", "", "", "")]
        [TestCase("1.1.3", "", "", "")]
        [TestCase("()", "", "", "")]
        [TestCase("Foo", "Foo", "", "Foo")]
        public void Initialize_NonNumeric_As_String_No_Exponent(string value, string expectedBaseLabel, string expectedPowerLabel, string expectedLabel)
        {
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value);

            Assert.IsFalse(symbolicValue.IsNumber());
            Assert.IsFalse(symbolicValue.IsFloat());
            Assert.IsFalse(symbolicValue.IsInteger());
            Assert.IsFalse(symbolicValue.IsFraction());
            Assert.IsTrue(symbolicValue.IsSymbolic());

            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicValue.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicValue.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }



        [TestCase(5, "2", "5", "2", "5^2", true, true)]
        [TestCase(-5, "2", "-5", "2", "-5^2", true, true)]
        [TestCase(0, "2", "0", "2", "0^2", true, true)]
        [TestCase(5, "Foo", "5", "Foo", "5^Foo", false, false)]
        [TestCase(5, "", "5", "", "5", true, true)]
        public void Initialize_As_Integer(int value, string exponent,
            string expectedBaseLabel, string expectedPowerLabel, string expectedLabel, bool isNumber, bool isInteger)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.IsNumber(), Is.EqualTo(isNumber));
            Assert.IsFalse(symbolicValue.IsFloat());
            Assert.That(symbolicValue.IsInteger(), Is.EqualTo(isInteger));
            Assert.IsFalse(symbolicValue.IsFraction());
            Assert.That(symbolicValue.IsSymbolic(), Is.EqualTo(!isNumber));

            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicValue.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicValue.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase(5.6, "0.5", "5.6", "0.5", "5.6^0.5", true)]
        [TestCase(-5.6, "2.2", "-5.6", "2.2", "-5.6^2.2", true, true)]
        [TestCase(0.0, "0.5", "0", "0.5", "0^0.5", true, true)]
        [TestCase(-0.0, "-0.5", "0", "-0.5", "0^-0.5", true, true)]
        [TestCase(5.6, "Foo", "5.6", "Foo", "5.6^Foo", false, false)]
        [TestCase(5.6, "", "5.6", "", "5.6", true, true)]
        public void Initialize_As_Float(double value, string exponent,
            string expectedBaseLabel, string expectedPowerLabel, string expectedLabel, bool isNumber, bool isFloat)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.IsNumber(), Is.EqualTo(isNumber));
            Assert.That(symbolicValue.IsFloat(), Is.EqualTo(isFloat));
            Assert.IsFalse(symbolicValue.IsInteger());
            Assert.IsFalse(symbolicValue.IsFraction());
            Assert.That(symbolicValue.IsSymbolic(), Is.EqualTo(!isNumber));

            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicValue.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicValue.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase(null, "", "", "", "", false, false, false, false)]
        [TestCase("", "", "", "", "", false, false, false, false)]
        [TestCase("1.1.3", "5.4.2", "", "", "", false, false, false, false)]
        [TestCase("-25^5^3", "", "", "", "", false, false, false, false)]
        [TestCase("Foo", "2", "Foo", "2", "Foo^2", false, false, false, false)]
        [TestCase("Foo", "Bar", "Foo", "Bar", "Foo^Bar", false, false, false, false)]
        [TestCase("Foo", "-Bar", "Foo", "-Bar", "Foo^-Bar", false, false, false, false)]
        [TestCase("1/2", "Foo", "1/2", "Foo", "(1/2)^Foo", false, false, false, false)]
        [TestCase("1", "2", "1", "2", "1^2", true, false, true, false)]
        [TestCase("-1", "2", "-1", "2", "-1^2", true, false, true, false)]
        [TestCase("-25.", "5", "-25", "5", "-25^5", true, false, true, false)]
        [TestCase("5", "1/2", "5", "1/2", "5^(1/2)", true, true, false, false)]
        [TestCase("5", "-5", "5", "-5", "5^-5", true, true, false, false)]
        [TestCase("25.3", "2.5", "25.3", "2.5", "25.3^2.5", true, true, false, false)]
        [TestCase("-25.3", "2.5", "-25.3", "2.5", "-25.3^2.5", true, true, false, false)]
        [TestCase("1/2", "3/4", "1/2", "3/4", "(1/2)^(3/4)", true, true, false, true)]
        [TestCase("1/2", "", "1/2", "", "1/2", true, true, false, true)]
        [TestCase("-25^5", "", "-25", "5", "-25^5", true, false, true, false)]
        public void Initialize_As_String(string value, string exponent,
            string expectedBaseLabel, string expectedPowerLabel, string expectedLabel,
            bool isNumber, bool isFloat, bool isInteger, bool isFraction)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.IsNumber(), Is.EqualTo(isNumber));
            Assert.That(symbolicValue.IsFloat(), Is.EqualTo(isFloat));
            Assert.That(symbolicValue.IsInteger(), Is.EqualTo(isInteger));
            Assert.That(symbolicValue.IsFraction(), Is.EqualTo(isFraction));
            Assert.That(symbolicValue.IsSymbolic(), Is.EqualTo(!isNumber));

            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicValue.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicValue.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase("2X")]
        [TestCase("2.2X")]
        [TestCase("-5X")]
        [TestCase("3/4X")]
        [TestCase("(3/4)X")]
        [TestCase("(3/4)*X")]
        [TestCase("(3/4/5)*X")]
        [TestCase("4*X")]
        [TestCase("Y*4")]
        [TestCase("4*5*X")]
        [TestCase("Y*X")]
        [TestCase("4*4")]
        [TestCase("5+5")]
        [TestCase("5-5")]
        [TestCase("5*5")]
        [TestCase("5/4/3")]
        [TestCase("(5")]
        [TestCase("5)")]
        [TestCase("[5")]
        [TestCase("5]")]
        [TestCase("{5")]
        [TestCase("5}")]
        public void Initialize_Numeric_Variable_As_String_No_Exponent(string value)
        {
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value);

            Assert.IsFalse(symbolicValue.IsNumber());
            Assert.IsFalse(symbolicValue.IsFloat());
            Assert.IsFalse(symbolicValue.IsInteger());
            Assert.IsFalse(symbolicValue.IsFraction());
            Assert.IsTrue(symbolicValue.IsSymbolic());

            Assert.That(symbolicValue.Label(), Is.EqualTo(string.Empty));
            Assert.That(symbolicValue.BaseLabel(), Is.EqualTo(string.Empty));
            Assert.That(symbolicValue.PowerLabel(), Is.EqualTo(string.Empty));
        }
        #endregion
        
        #region Methods: Public

        [TestCase("", ExpectedResult = "")]
        [TestCase("a", ExpectedResult = "a")]
        [TestCase("-a", ExpectedResult = "-a")]
        [TestCase("a/b", ExpectedResult = "a/b")]
        [TestCase("-a/b", ExpectedResult = "-a/b")]
        [TestCase("a^b", ExpectedResult = "a^b")]
        [TestCase("-a^b", ExpectedResult = "-a^b")]
        [TestCase("(a/b)^c", ExpectedResult = "(a/b)^c")]
        [TestCase("-(a/b)^c", ExpectedResult = "-(a/b)^c")]
        [TestCase("a^-b", ExpectedResult = "a^-b")]
        [TestCase("-a^-b", ExpectedResult = "-a^-b")]
        [TestCase("a^(b/c)", ExpectedResult = "a^(b/c)")]
        [TestCase("a^-(b/c)", ExpectedResult = "a^-(b/c)")]
        public string Label_with_Primitive_Power(string value)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            return unit.Label();
        }


        [TestCase("a", "b^c", ExpectedResult = "a^b^c")]
        [TestCase("a", "-b^c", ExpectedResult = "a^-b^c")]
        [TestCase("a", "-b^-c", ExpectedResult = "a^-b^-c")]
        [TestCase("a", "(-b^c)", ExpectedResult = "a^-b^c")]
        [TestCase("a", "-(b^c)", ExpectedResult = "a^-(b^c)")]
        [TestCase("a", "-(b^-c)", ExpectedResult = "a^-(b^-c)")]
        [TestCase("a", "(b/c)", ExpectedResult = "a^(b/c)")]
        [TestCase("a", "-(b/c)", ExpectedResult = "a^-(b/c)")]
        [TestCase("a", "(b*c)", ExpectedResult = "a^(b*c)")]
        [TestCase("a", "-(b*c)", ExpectedResult = "a^-(b*c)")]
        public string Label_with_ProductQuotient_Power(string value, string powerValue)
        {
            ProductQuotientSet power = new ProductQuotientSet(powerValue);
            PrimitiveUnit unit = new PrimitiveUnit(value, power);
            return unit.Label();
        }
        
        [TestCase("a", "(b+c)", ExpectedResult = "a^(b+c)")]
        [TestCase("a", "-(b+c)", ExpectedResult = "a^-(b+c)")]
        [TestCase("a", "(b-c)", ExpectedResult = "a^(b-c)")]
        [TestCase("a","-(b-c)", ExpectedResult = "a^-(b-c)")]
        public string Label_with_SumDifference_Power(string value, string powerValue)
        {
            SumDifferenceSet power = new SumDifferenceSet(powerValue);
            PrimitiveUnit unit = new PrimitiveUnit(value, power);
            return unit.Label();
        }

        // TODO: Static_HasValidPowers

        // TODO: GetBase
        // TODO: GetPower
        // TODO: GetSign

        // TODO: PowerLabel
        // TODO: BaseLabel

        // TODO: IsInteger
        // TODO: IsFloat
        // TODO: IsFraction
        // TODO: IsNumber
        // TODO: IsSymbolic
        // TODO: IsEmpty
        // TODO: HasPower
        // TODO: SignIsNegative
        // TODO: FlipSign

        // TODO: AsInteger
        // TODO: AsFloat

        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase("a", ExpectedResult = "a")]
        [TestCase("-a", ExpectedResult = "a")]
        [TestCase("a^-b", ExpectedResult = "a^-b")]
        [TestCase("-a^-b", ExpectedResult = "a^-b")]
        public string GetAbsolute(string value)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            IBase absoluteUnit = unit.GetAbsolute();
            return absoluteUnit.Label();
        }

        // TODO: SimplifyBase
        // TODO: SimplifyPower
        // TODO: SimplifySign
        // TODO: DistributeSign
        #endregion

        #region Methods: Calculate
        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("0", double.NaN)]
        [TestCase("4", 1)]
        [TestCase("-4", 1)]
        [TestCase("5.7", 1)]
        [TestCase("-5.7", 1)]
        public void Calculate_With_Power_Zero(string value, double expectedResult)
        {
            string exponent = "0";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("0", 0)]
        [TestCase("4", 4)]
        [TestCase("-4", -4)]
        [TestCase("5.7", 5.7)]
        [TestCase("-5.7", -5.7)]
        public void Calculate_With_No_Exponent(string value, double expectedResult)
        {
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("0", 0)]
        [TestCase("4", 4)]
        [TestCase("-4", -4)]
        [TestCase("5.7", 5.7)]
        [TestCase("-5.7", -5.7)]
        [TestCase("1/2", 0.5)]
        [TestCase("1/0", double.PositiveInfinity)]
        [TestCase("-1/0", double.NegativeInfinity)]
        [TestCase("0/0", double.NaN)]
        public void Calculate_With_Power_One(string value, double expectedResult)
        {
            string exponent = "1";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("4", 16)]
        [TestCase("-4", 16)]
        [TestCase("5.7", 32.49)]
        [TestCase("-5.7", 32.49)]
        public void Calculate_With_Power_Integer(string value, double expectedResult)
        {
            string exponent = "2";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("4", 16)]
        [TestCase("-4", 16)]
        [TestCase("5.7", 32.49)]
        [TestCase("-5.7", 32.49)]
        public void Calculate_With_Power_Fraction_Integer(string value, double expectedResult)
        {
            string exponent = "4/2";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("4", 6.3496042078727974)]
        [TestCase("-4", double.NaN)]
        [TestCase("5.7", 10.182001129935921)]
        [TestCase("-5.7", double.NaN)]
        public void Calculate_With_Power_Fraction_Decimal(string value, double expectedResult)
        {
            string exponent = "4/3";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase("2X", 0)]
        [TestCase("4", double.NaN)]
        [TestCase("-4", double.NaN)]
        [TestCase("5.7", double.NaN)]
        [TestCase("-5.7", double.NaN)]
        public void Calculate_With_Power_Fraction_NaN(string value, double expectedResult)
        {
            string exponent = "0/0";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase("2X", 0)]
        [TestCase("4", 0)] // double.NegativeInfinity = 1/double.PositiveInfinity = 0
        [TestCase("-4", 0)]
        [TestCase("5.7", 0)]
        [TestCase("-5.7", 0)]
        public void Calculate_With_Power_Fraction_NegativeInfinity(string value, double expectedResult)
        {
            string exponent = "-1/0";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase("2X", 0)]
        [TestCase("4", double.PositiveInfinity)]
        [TestCase("-4", double.PositiveInfinity)]
        [TestCase("5.7", double.PositiveInfinity)]
        [TestCase("-5.7", double.PositiveInfinity)]
        public void Calculate_With_Power_Fraction_PositiveInfinity(string value, double expectedResult)
        {
            string exponent = "1/0";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("4", 32)]
        [TestCase("-4", double.NaN)]
        [TestCase("5.7", 77.568811838263969)]
        [TestCase("-5.7", double.NaN)]
        public void Calculate_With_Power_Decimal(string value, double expectedResult)
        {
            string exponent = "2.5";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("4", 0.0625)]
        [TestCase("-4", 0.0625)]
        [TestCase("5.7", 0.03077870113881194)]
        [TestCase("-5.7", 0.03077870113881194)]
        public void Calculate_With_Power_Negative(string value, double expectedResult)
        {
            string exponent = "-2";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("4", 2)]
        [TestCase("-4", double.NaN)]
        [TestCase("5.7", 2.3874672772626644)]
        [TestCase("-5.7", double.NaN)]
        public void Calculate_With_Power_Root(string value, double expectedResult)
        {
            string exponent = "0.5";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase("-2^(-3^-2)", double.NaN)]//(41) Math.Pow documentation says that -x^y(decimal) = NaN. This is because it results in imaginary number answers.
        [TestCase("-2^(3^-2)", double.NaN)]//(42) Math.Pow documentation says that -x^y(decimal) = NaN. This is because it results in imaginary number answers.       
        [TestCase("2^(-3^-2)", 1.08006)]//(41a)
        [TestCase("2^(3^-2)", 1.08006)]//(42a)
        public void Calculate_With_Power_Various(string value, double expectedResult)
        {
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult).Within(0.000001));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Foo")]
        [TestCase("2X")]
        [TestCase("4")]
        [TestCase("-4")]
        [TestCase("5.7")]
        [TestCase("-5.7")]
        public void Calculate_With_Power_Symbolic(string value)
        {
            string exponent = "Bar";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value, power);
            Assert.That(symbolicValue.Calculate(), Is.EqualTo(0));

            symbolicValue = new PrimitiveUnit(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(0));
        }
        #endregion

        #region Methods: Simplify
        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        public string Simplify_Nothing_Or_Empty_Returns_Empty(string value)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            IBase simplifiedUnit = unit.Simplify();
            return simplifiedUnit.Label();
        }


        [TestCase("a", ExpectedResult = "a")]
        [TestCase("1", ExpectedResult = "1")]
        [TestCase("-1", ExpectedResult = "-1")]
        [TestCase("1/2", ExpectedResult = "1/2")]
        [TestCase("2/4", ExpectedResult = "1/2")]
        [TestCase("4/2", ExpectedResult = "2")]
        [TestCase("2.2/2.2", ExpectedResult = "1")]
        [TestCase("4.4/2.2", ExpectedResult = "2")]
        public string Simplify_Fractional_Base_Returns_Reduced_Fraction(string value)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            IBase simplifiedUnit = unit.Simplify();
            return simplifiedUnit.Label();
        }

        [TestCase("a^0", ExpectedResult = "1")]
        [TestCase("5^0", ExpectedResult = "1")]
        public string Simplify_Zero_Power_Returns_One(string value)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            IBase simplifiedUnit = unit.Simplify();
            return simplifiedUnit.Label();
        }

        [TestCase("a^1", ExpectedResult = "a")]
        [TestCase("5^1", ExpectedResult = "5")]
        public string Simplify_One_Power_Returns_Base(string value)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            IBase simplifiedUnit = unit.Simplify();
            return simplifiedUnit.Label();
        }

        [TestCase("a", ExpectedResult = "a")]
        [TestCase("5", ExpectedResult = "5")]
        public string Simplify_Empty_Power_Returns_Base(string value)
        {
            PrimitiveUnit emptyPower = new PrimitiveUnit(string.Empty);
            PrimitiveUnit unit = new PrimitiveUnit(value, emptyPower);
            IBase simplifiedUnit = unit.Simplify();
            return simplifiedUnit.Label();
        }

        [TestCase("a", ExpectedResult = "a")]
        [TestCase("5", ExpectedResult = "5")]
        public string Simplify_Null_Power_Returns_Base(string value)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value, null);
            IBase simplifiedUnit = unit.Simplify();
            return simplifiedUnit.Label();
        }

        [TestCase("a^1", ExpectedResult = "a")]
        [TestCase("a^(1/2)", ExpectedResult = "a^(1/2)")]
        [TestCase("a^(2/4)", ExpectedResult = "a^(1/2)")]
        [TestCase("a^(4/2)", ExpectedResult = "a^2")]
        public string Simplify_Fractional_Power_Returns_Reduced_Fraction(string value)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            IBase simplifiedUnit = unit.Simplify();
            return simplifiedUnit.Label();
        }

        [TestCase("a^-0", ExpectedResult = "1")]
        [TestCase("a^-1", ExpectedResult = "1/a")]
        [TestCase("a^-2", ExpectedResult = "1/a^2")]
        [TestCase("a^(-1/2)", ExpectedResult = "1/(a^(1/2))")]  
        [TestCase("a^(-2/4)", ExpectedResult = "1/(a^(1/2))")]
        public string Simplify_Negative_Power_Returns_Inverse_Fraction(string value)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            IBase simplifiedUnit = unit.Simplify();
            return simplifiedUnit.Label();
        }
        #endregion

        #region Overrides
        [TestCase(null, "MPT.SymbolicMath.PrimitiveUnit")]
        [TestCase("", "MPT.SymbolicMath.PrimitiveUnit")]
        [TestCase("1.1.3", "MPT.SymbolicMath.PrimitiveUnit")]
        [TestCase("Foo", "Foo")]
        [TestCase("0", "0")]
        [TestCase("5", "5")]
        [TestCase("-5", "-5")]
        [TestCase("-300.2", "-300.2")]
        public void Override_ToString(string value, string expectedString)
        {
            PrimitiveUnit symbolicValue = new PrimitiveUnit(value);
            Assert.That(symbolicValue.ToString(), Is.EqualTo(expectedString));
        }

        [Test]
        public void Override_Clone()
        {
            PrimitiveUnit unitOriginal = new PrimitiveUnit("-2^3");
            object unitClone = unitOriginal.Clone();

            Assert.IsTrue(unitOriginal.Equals(unitClone));
        }

        [Test]
        public void Override_CloneUnit()
        {
            PrimitiveUnit unitOriginal = new PrimitiveUnit("-2^3");
            PrimitiveUnit unitClone = unitOriginal.CloneUnit();

            Assert.IsTrue(unitOriginal.Equals(unitClone));
        }
        #endregion
    }
}
