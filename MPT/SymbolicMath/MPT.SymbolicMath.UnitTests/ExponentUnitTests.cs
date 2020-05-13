using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{
    [TestFixture]
    public class ExponentUnitTests
    {
        [TestCase(5, "5", 5, 5.0)]
        [TestCase(-5, "-5", -5, -5.0)]
        [TestCase(0, "0", 0, 0.0)]
        public void Initialize_As_Integer_No_Exponent(int value, string expectedString, int expectedInt, double expectedFloat)
        {
            ExponentUnit symbolicValue = new ExponentUnit(value);

            Assert.That(symbolicValue.BaseAsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.BaseAsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.BaseAsFloat(), Is.EqualTo(expectedFloat));
            Assert.IsTrue(symbolicValue.IsNumber());
            Assert.IsTrue(symbolicValue.IsInteger());
        }

        [TestCase(5, "2", "5", 5, 5.0, true, true, "5^2")]
        [TestCase(-5, "2", "-5", -5, -5.0, true, true, "-5^2")]
        [TestCase(0, "2", "0", 0, 0.0, true, true, "0^2")]
        [TestCase(5, "Foo", "5", 5, 5.0, false, false, "5^Foo")]
        [TestCase(5, "", "5", 5, 5.0, true, true, "5")]
        public void Initialize_As_Integer(int value, string exponent, 
            string expectedString, int expectedInt, double expectedFloat, bool expectedAsNumber, bool expectedAsInteger, string expectedLabel)
        {
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);

            Assert.That(symbolicValue.BaseAsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.BaseAsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.BaseAsFloat(), Is.EqualTo(expectedFloat));
            Assert.That(symbolicValue.IsNumber(), Is.EqualTo(expectedAsNumber));
            Assert.That(symbolicValue.IsInteger(), Is.EqualTo(expectedAsInteger));
            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
        }


        [TestCase(5.6, "5.6", 6, 5.6)]
        [TestCase(5.5, "5.5", 6, 5.5)]
        [TestCase(5.4, "5.4", 5, 5.4)]
        [TestCase(-5.6, "-5.6", -6, -5.6)]
        [TestCase(-5.5, "-5.5", -6, -5.5)]
        [TestCase(-5.4, "-5.4", -5, -5.4)]
        [TestCase(0.0, "0", 0, 0.0)]
        [TestCase(-0.0, "0", 0, 0.0)]
        public void Initialize_As_Float_No_Exponent(double value, string expectedString, int expectedInt, double expectedFloat)
        {
            ExponentUnit symbolicValue = new ExponentUnit(value);

            Assert.That(symbolicValue.BaseAsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.BaseAsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.BaseAsFloat(), Is.EqualTo(expectedFloat));
            Assert.IsTrue(symbolicValue.IsNumber());
            Assert.IsFalse(symbolicValue.IsInteger());
        }

        [TestCase(5.6, "0.5", "5.6", 6, 5.6, true, "5.6^0.5")]
        [TestCase(-5.6, "2.2", "-5.6", -6, -5.6, true, "-5.6^2.2")]
        [TestCase(0.0, "0.5", "0", 0, 0.0, true, "0^0.5")]
        [TestCase(-0.0, "0.5", "0", 0, 0.0, true, "0^0.5")]
        [TestCase(5.6, "Foo", "5.6", 6, 5.6, false, "5.6^Foo")]
        [TestCase(5.6, "", "5.6", 6, 5.6, true, "5.6")]
        public void Initialize_As_Float(double value, string exponent,
            string expectedString, int expectedInt, double expectedFloat, bool expectedAsNumber, string expectedLabel)
        {
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);

            Assert.That(symbolicValue.BaseAsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.BaseAsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.BaseAsFloat(), Is.EqualTo(expectedFloat));
            Assert.That(symbolicValue.IsNumber(), Is.EqualTo(expectedAsNumber));
            Assert.IsFalse(symbolicValue.IsInteger());
            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
        }

        [TestCase("1", "1", 1, 1.0, true)]
        [TestCase("-1", "-1", -1, -1.0, true)]
        [TestCase("25.3", "25.3", 25, 25.3, false)]
        [TestCase("-25.3", "-25.3", -25, -25.3, false)]
        [TestCase("-25.", "-25", -25, -25.0, true)]
        [TestCase("1/2", "1/2", 0, 0.5, false)]
        public void Initialize_Numeric_As_String_No_Exponent(string value, string expectedString, int expectedInt, double expectedFloat, bool isInteger)
        {
            ExponentUnit symbolicValue = new ExponentUnit(value);

            Assert.That(symbolicValue.BaseAsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.BaseAsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.BaseAsFloat(), Is.EqualTo(expectedFloat));
            Assert.IsTrue(symbolicValue.IsNumber());
            Assert.That(symbolicValue.IsInteger(), Is.EqualTo(isInteger));
        }

        [TestCase(null, "", 0, 0.0)]
        [TestCase("", "", 0, 0.0)]
        [TestCase("1.1.3", "1.1.3", 0, 0.0)]
        [TestCase("Foo", "Foo", 0, 0.0)]
        public void Initialize_NonNumeric_As_String_No_Exponent(string value, string expectedString, int expectedInt, double expectedFloat)
        {
            ExponentUnit symbolicValue = new ExponentUnit(value);

            Assert.That(symbolicValue.BaseAsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.BaseAsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.BaseAsFloat(), Is.EqualTo(expectedFloat));
            Assert.IsFalse(symbolicValue.IsNumber());
            Assert.IsFalse(symbolicValue.IsInteger());
        }

        [TestCase(null, "", "", 0, 0.0, false, false, "")]
        [TestCase("", "", "", 0, 0.0, false, false, "")]
        [TestCase("1.1.3", "5.4.2", "1.1.3", 0, 0.0, false, false, "1.1.3^5.4.2")]
        [TestCase("Foo", "2", "Foo", 0, 0.0, false, false, "Foo^2")]
        [TestCase("Foo", "Bar", "Foo", 0, 0.0, false, false, "Foo^Bar")]
        [TestCase("Foo", "-Bar", "Foo", 0, 0.0, false, false, "Foo^-Bar")]
        [TestCase("1", "2", "1", 1, 1.0, true, true, "1^2")]
        [TestCase("-1", "2", "-1", -1, -1.0, true, true, "-1^2")]
        [TestCase("25.3", "2.5", "25.3", 25, 25.3, true, false, "25.3^2.5")]
        [TestCase("-25.3", "2.5", "-25.3", -25, -25.3, true, false, "-25.3^2.5")]
        [TestCase("-25.", "5", "-25", -25, -25.0, true, true, "-25^5")]
        [TestCase("1/2", "3/4", "1/2", 0, 0.5, true, false, "(1/2)^(3/4)")]
        [TestCase("1/2", "", 0, 0.5, true, true, "1/2")]
        [TestCase("1/2", "Foo", 0, 0.5, false, false, "(1/2)^Foo")]
        public void Initialize_As_String(string value, string exponent,
            string expectedString, int expectedInt, double expectedFloat, bool expectedAsNumber, bool expectedAsInteger, string expectedLabel)
        {
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);

            Assert.That(symbolicValue.BaseAsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.BaseAsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.BaseAsFloat(), Is.EqualTo(expectedFloat));
            Assert.That(symbolicValue.IsNumber(), Is.EqualTo(expectedAsNumber));
            Assert.That(symbolicValue.IsInteger(), Is.EqualTo(expectedAsInteger));
            Assert.That(symbolicValue.Label(), Is.EqualTo(expectedLabel));
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = 0)]
        [TestCase("2X", ExpectedResult = 0)]
        [TestCase("4", ExpectedResult = 4)]
        [TestCase("-4", ExpectedResult = -4)]
        [TestCase("5.7", ExpectedResult = 5.7)]
        [TestCase("-5.7", ExpectedResult = -5.7)]
        public double Calculate_No_Exponent(string value)
        {
            ExponentUnit symbolicValue = new ExponentUnit(value);
            return symbolicValue.Calculate();
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = 1)]
        [TestCase("2X", ExpectedResult = 1)]
        [TestCase("4", ExpectedResult = 1)]
        [TestCase("-4", ExpectedResult = 1)]
        [TestCase("5.7", ExpectedResult = 1)]
        [TestCase("-5.7", ExpectedResult = 1)]
        public double Calculate_Power_Zero(string value)
        {
            string exponent = "0";
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);

            return symbolicValue.Calculate();
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = 0)]
        [TestCase("2X", ExpectedResult = 0)]
        [TestCase("4", ExpectedResult = 4)]
        [TestCase("-4", ExpectedResult = -4)]
        [TestCase("5.7", ExpectedResult = 5.7)]
        [TestCase("-5.7", ExpectedResult = -5.7)]
        public double Calculate_Power_One(string value)
        {
            string exponent = "1";
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);

            return symbolicValue.Calculate();
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = 0)]
        [TestCase("2X", ExpectedResult = 0)]
        [TestCase("4", ExpectedResult = 16)]
        [TestCase("-4", ExpectedResult = 16)]
        [TestCase("5.7", ExpectedResult = 32.49)]
        [TestCase("-5.7", ExpectedResult = 32.49)]
        public double Calculate_Power_Integer(string value)
        {
            string exponent = "2";
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);

            return symbolicValue.Calculate();
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = 0)]
        [TestCase("2X", ExpectedResult = 0)]
        [TestCase("4", ExpectedResult = 16)]
        [TestCase("-4", ExpectedResult = 16)]
        [TestCase("5.7", ExpectedResult = 32.49)]
        [TestCase("-5.7", ExpectedResult = 32.49)]
        public double Calculate_Power_Fraction(string value)
        {
            string exponent = "4/2";
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);
            return symbolicValue.Calculate();
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = 0)]
        [TestCase("2X", ExpectedResult = 0)]
        [TestCase("4", ExpectedResult = 32)]
        [TestCase("-4", ExpectedResult = double.NaN)]
        [TestCase("5.7", ExpectedResult = 77.568811838263969)]
        [TestCase("-5.7", ExpectedResult = double.NaN)]
        public double Calculate_Power_Decimal(string value)
        {
            string exponent = "2.5";
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);
            return symbolicValue.Calculate();
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = double.PositiveInfinity)]
        [TestCase("2X", ExpectedResult = double.PositiveInfinity)]
        [TestCase("4", ExpectedResult = 0.0625)]
        [TestCase("-4", ExpectedResult = 0.0625)]
        [TestCase("5.7", ExpectedResult = 0.03077870113881194)]
        [TestCase("-5.7", ExpectedResult = 0.03077870113881194)]
        public double Calculate_Power_Negative(string value)
        {
            string exponent = "-2";
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);
            return symbolicValue.Calculate();
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = 0)]
        [TestCase("2X", ExpectedResult = 0)]
        [TestCase("4", ExpectedResult = 2)]
        [TestCase("-4", ExpectedResult = double.NaN)]
        [TestCase("5.7", ExpectedResult = 2.3874672772626644)]
        [TestCase("-5.7", ExpectedResult = double.NaN)]
        public double Calculate_Power_Root(string value)
        {
            string exponent = "0.5";
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);
            return symbolicValue.Calculate();
        }

        [TestCase(null, ExpectedResult = 0)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("Foo", ExpectedResult = 1)]
        [TestCase("2X", ExpectedResult = 1)]
        [TestCase("4", ExpectedResult = 1)]
        [TestCase("-4", ExpectedResult = 1)]
        [TestCase("5.7", ExpectedResult = 1)]
        [TestCase("-5.7", ExpectedResult = 1)]
        public double Calculate_Power_Symbolic(string value)
        {
            string exponent = "Bar";
            ExponentUnit power = new ExponentUnit(exponent);
            ExponentUnit symbolicValue = new ExponentUnit(value, power);
            return symbolicValue.Calculate();
        }

        [TestCase(null, "MPT.SymbolicMath.ExponentUnit")]
        [TestCase("", "MPT.SymbolicMath.ExponentUnit")]
        [TestCase("1.1.3", "1.1.3")]
        [TestCase("Foo", "Foo")]
        [TestCase("0", "0")]
        [TestCase("5", "5")]
        [TestCase("-5", "-5")]
        [TestCase("-300.2", "-300.2")]
        public void Override_ToString(string value, string expectedString)
        {
            ExponentUnit symbolicValue = new ExponentUnit(value);
            Assert.That(symbolicValue.ToString(), Is.EqualTo(expectedString));
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Add_Null_Or_Empty(string value1, string value2)
        {
            ExponentUnit operand1 = new ExponentUnit(value1);
            ExponentUnit operand2 = new ExponentUnit(value2);
            ExponentUnit value3 = operand1 + operand2;

            Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber());
            Assert.IsFalse(value3.IsInteger());
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Add_Partial_Null_Or_Empty(int value1, string value2)
        {
            ExponentUnit operand1 = new ExponentUnit(value1);
            ExponentUnit operand2 = new ExponentUnit(value2);
            ExponentUnit value3 = operand1 + operand2;

            Assert.That(value3.BaseAsString(), Is.EqualTo("2"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(2));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(2.0));
            Assert.IsTrue(value3.IsNumber());
            Assert.IsTrue(value3.IsInteger());

            value3 = operand2 + operand1;

            Assert.That(value3.BaseAsString(), Is.EqualTo("2"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(2));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(2.0));
            Assert.IsTrue(value3.IsNumber());
            Assert.IsTrue(value3.IsInteger());
        }

        [Test]
        public void Operator_Add_Integer_and_Integer()
        {
            ExponentUnit operand1 = new ExponentUnit(3);
            ExponentUnit operand2 = new ExponentUnit(2);

            ExponentUnit value3 = operand1 + operand2;

            Assert.That(value3.BaseAsString(), Is.EqualTo("5"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(5));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(5.0));
            Assert.IsTrue(value3.IsNumber());
            Assert.IsTrue(value3.IsInteger());

            value3 = operand2 + operand1;

            Assert.That(value3.BaseAsString(), Is.EqualTo("5"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(5));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(5.0));
            Assert.IsTrue(value3.IsNumber());
            Assert.IsTrue(value3.IsInteger());
        }

        [Test]
        public void Operator_Add_Double_and_Double()
        {
            ExponentUnit operand1 = new ExponentUnit(3.2);
            ExponentUnit operand2 = new ExponentUnit(2.74);

            ExponentUnit value3 = operand1 + operand2;

            Assert.That(value3.BaseAsString(), Is.EqualTo("5.94"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(6));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(5.94));
            Assert.IsTrue(value3.IsNumber());
            Assert.IsFalse(value3.IsInteger());

            value3 = operand2 + operand1;

            Assert.That(value3.BaseAsString(), Is.EqualTo("5.94"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(6));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(5.94));
            Assert.IsTrue(value3.IsNumber());
            Assert.IsFalse(value3.IsInteger());
        }

        [Test]
        public void Operator_Add_String_and_String()
        {
            ExponentUnit operand1 = new ExponentUnit("Foo");
            ExponentUnit operand2 = new ExponentUnit("Bar");

            ExponentUnit value3 = operand1 + operand2;

            Assert.That(value3.BaseAsString(), Is.EqualTo("Foo+Bar"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(0));

            value3 = operand2 + operand1;

            Assert.That(value3.BaseAsString(), Is.EqualTo("Bar+Foo"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(0));
        }

        [Test]
        public void Operator_Add_Integer_and_Double()
        {
            ExponentUnit operand1 = new ExponentUnit(3.2);
            ExponentUnit operand2 = new ExponentUnit(2);

            ExponentUnit value3 = operand1 + operand2;

            Assert.That(value3.BaseAsString(), Is.EqualTo("5.2"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(5));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(5.2));
            Assert.IsTrue(value3.IsNumber());
            Assert.IsFalse(value3.IsInteger());

            value3 = operand2 + operand1;

            Assert.That(value3.BaseAsString(), Is.EqualTo("5.2"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(5));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(5.2));
            Assert.IsTrue(value3.IsNumber());
            Assert.IsFalse(value3.IsInteger());
        }


        [Test]
        public void Operator_Add_String_and_Integer()
        {
            ExponentUnit operand1 = new ExponentUnit("Foo");
            ExponentUnit operand2 = new ExponentUnit(3);

            ExponentUnit value3 = operand1 + operand2;

            Assert.That(value3.BaseAsString(), Is.EqualTo("Foo+3"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber());
            Assert.IsFalse(value3.IsInteger());

            value3 = operand2 + operand1;

            Assert.That(value3.BaseAsString(), Is.EqualTo("3+Foo"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber());
            Assert.IsFalse(value3.IsInteger());
        }

        [Test]
        public void Operator_Add_String_and_Double()
        {
            ExponentUnit operand1 = new ExponentUnit("Foo");
            ExponentUnit operand2 = new ExponentUnit(2.2);

            ExponentUnit value3 = operand1 + operand2;

            Assert.That(value3.BaseAsString(), Is.EqualTo("Foo+2.2"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber());
            Assert.IsFalse(value3.IsInteger());

            value3 = operand2 + operand1;

            Assert.That(value3.BaseAsString(), Is.EqualTo("2.2+Foo"));
            Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
            Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber());
            Assert.IsFalse(value3.IsInteger());
        }

        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase("", null)]
        //public void Operator_Subtract_Null_Or_Empty(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);
        //    ExponentUnit value3 = operand1 - operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[TestCase(2, "")]
        //[TestCase(2, null)]
        //public void Operator_Subtract_Partial_Null_Or_Empty(int value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    ExponentUnit value3 = operand1 - operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 - operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Subtract_Integer_and_Integer()
        //{
        //    ExponentUnit operand1 = new ExponentUnit(3);
        //    ExponentUnit operand2 = new ExponentUnit(2);

        //    ExponentUnit value3 = operand1 - operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("1"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(1));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(1.0));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsTrue(value3.IsInteger());

        //    value3 = operand2 - operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("-1"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(-1));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(-1.0));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsTrue(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Subtract_Double_and_Double()
        //{
        //    ExponentUnit operand1 = new ExponentUnit(3.2);
        //    ExponentUnit operand2 = new ExponentUnit(2.74);

        //    ExponentUnit value3 = operand1 - operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("0.46"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.46));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 - operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("-0.46"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(-0.46));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Subtract_String_and_String()
        //{
        //    ExponentUnit operand1 = new ExponentUnit("Foo");
        //    ExponentUnit operand2 = new ExponentUnit("Bar");
        //    ExponentUnit value3 = operand1 - operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("Foo-Bar"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Subtract_Integer_and_Double()
        //{
        //    ExponentUnit operand1 = new ExponentUnit(3.2);
        //    ExponentUnit operand2 = new ExponentUnit(2);

        //    ExponentUnit value3 = operand1 - operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("1.2"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(1));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(1.2));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 - operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("-1.2"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(-1));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(-1.2));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}


        //[Test]
        //public void Operator_Subtract_String_and_Integer()
        //{
        //    ExponentUnit operand1 = new ExponentUnit("Foo");
        //    ExponentUnit operand2 = new ExponentUnit(3);

        //    ExponentUnit value3 = operand1 - operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("Foo-3"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 - operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("3-Foo"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Subtract_String_and_Double()
        //{
        //    ExponentUnit operand1 = new ExponentUnit("Foo");
        //    ExponentUnit operand2 = new ExponentUnit(2.2);
        //    ExponentUnit value3 = operand1 - operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("Foo-2.2"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 - operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("2.2-Foo"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase("", null)]
        //public void Operator_Multiply_Null_Or_Empty(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);
        //    ExponentUnit value3 = operand1 * operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[TestCase(2, "")]
        //[TestCase(2, null)]
        //public void Operator_Multiply_Partial_Null_Or_Empty(int value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);
        //    ExponentUnit value3 = operand1 * operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 * operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Multiply_Integer_and_Integer()
        //{
        //    ExponentUnit operand1 = new ExponentUnit(3);
        //    ExponentUnit operand2 = new ExponentUnit(2);

        //    ExponentUnit value3 = operand1 * operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("6"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(6));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(6.0));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsTrue(value3.IsInteger());

        //    value3 = operand2 * operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("6"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(6));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(6.0));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsTrue(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Multiply_Double_and_Double()
        //{
        //    ExponentUnit operand1 = new ExponentUnit(3.2);
        //    ExponentUnit operand2 = new ExponentUnit(2.74);

        //    ExponentUnit value3 = operand1 * operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("8.768"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(9));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(8.768));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 * operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("8.768"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(9));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(8.768));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Multiply_String_and_String()
        //{
        //    ExponentUnit operand1 = new ExponentUnit("Foo");
        //    ExponentUnit operand2 = new ExponentUnit("Bar");
        //    ExponentUnit value3 = operand1 * operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("Foo*Bar"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Multiply_Integer_and_Double()
        //{
        //    ExponentUnit operand1 = new ExponentUnit(3.2);
        //    ExponentUnit operand2 = new ExponentUnit(2);

        //    ExponentUnit value3 = operand1 * operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("6.4"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(6));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(6.4));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 * operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("6.4"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(6));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(6.4));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}


        //[Test]
        //public void Operator_Multiply_String_and_Integer()
        //{
        //    ExponentUnit operand1 = new ExponentUnit("Foo");
        //    ExponentUnit operand2 = new ExponentUnit(3);

        //    ExponentUnit value3 = operand1 * operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("Foo*3"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 * operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("3*Foo"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Multiply_String_and_Double()
        //{
        //    ExponentUnit operand1 = new ExponentUnit("Foo");
        //    ExponentUnit operand2 = new ExponentUnit(2.2);
        //    ExponentUnit value3 = operand1 * operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("Foo*2.2"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 * operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("2.2*Foo"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}


        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase("", null)]
        //public void Operator_Divide_Null_Or_Empty(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);
        //    ExponentUnit value3 = operand1 / operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[TestCase(2, "")]
        //[TestCase(2, null)]
        //public void Operator_Divide_Partial_Null_Or_Empty(int value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);
        //    ExponentUnit value3 = operand1 / operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 / operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo(string.Empty));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Divide_Integer_and_Integer()
        //{
        //    ExponentUnit operand1 = new ExponentUnit(3);
        //    ExponentUnit operand2 = new ExponentUnit(2);

        //    ExponentUnit value3 = operand1 / operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("1.5"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(2));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(1.5));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 / operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("0.666666666666667"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(1));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.666666666666667));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    operand1 = new ExponentUnit(4);
        //    operand2 = new ExponentUnit(2);

        //    value3 = operand1 / operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("2"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(2));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(2.0));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsTrue(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Divide_Double_and_Double()
        //{
        //    ExponentUnit operand1 = new ExponentUnit(3.2);
        //    ExponentUnit operand2 = new ExponentUnit(1.6);

        //    ExponentUnit value3 = operand1 / operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("2"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(2));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(2.0));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 / operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("0.5"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.5));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Divide_String_and_String()
        //{
        //    ExponentUnit operand1 = new ExponentUnit("Foo");
        //    ExponentUnit operand2 = new ExponentUnit("Bar");
        //    ExponentUnit value3 = operand1 / operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("Foo/Bar"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Divide_Integer_and_Double()
        //{
        //    ExponentUnit operand1 = new ExponentUnit(3.2);
        //    ExponentUnit operand2 = new ExponentUnit(2);

        //    ExponentUnit value3 = operand1 / operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("1.6"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(2));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(1.6));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 / operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("0.625"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(1));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.625));
        //    Assert.IsTrue(value3.IsNumber());
        //    Assert.IsTrue(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}


        //[Test]
        //public void Operator_Divide_String_and_Integer()
        //{
        //    ExponentUnit operand1 = new ExponentUnit("Foo");
        //    ExponentUnit operand2 = new ExponentUnit(3);

        //    ExponentUnit value3 = operand1 / operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("Foo/3"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 / operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("3/Foo"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[Test]
        //public void Operator_Divide_String_and_Double()
        //{
        //    ExponentUnit operand1 = new ExponentUnit("Foo");
        //    ExponentUnit operand2 = new ExponentUnit(2.2);
        //    ExponentUnit value3 = operand1 / operand2;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("Foo/2.2"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());

        //    value3 = operand2 / operand1;

        //    Assert.That(value3.BaseAsString(), Is.EqualTo("2.2/Foo"));
        //    Assert.That(value3.BaseAsInteger(), Is.EqualTo(0));
        //    Assert.That(value3.BaseAsFloat(), Is.EqualTo(0.0));
        //    Assert.IsFalse(value3.IsNumber());
        //    Assert.IsFalse(value3.IsFloat);
        //    Assert.IsFalse(value3.IsInteger());
        //}

        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase("", null)]
        //public void Operator_Equals_Null_Or_Empty(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 == operand2);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(2, "")]
        //[TestCase(2, null)]
        //public void Operator_Equals_Partial_Null_Or_Empty(int value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 == operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 == operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(3, 2, false)]
        //[TestCase(2, 2, true)]
        //[TestCase(0, 0, true)]
        //[TestCase(-2, -2, true)]
        //[TestCase(-2, -1, false)]
        //public void Operator_Equals_Integer_and_Integer(int value1, int value2, bool isEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 == operand2);

        //    Assert.That(result, Is.EqualTo(isEqual));

        //    result = (operand2 == operand1);

        //    Assert.That(result, Is.EqualTo(isEqual));
        //}

        //[TestCase(3.2, 2.2, false)]
        //[TestCase(2.2, 2.2, true)]
        //[TestCase(0.0, 0.0, true)]
        //[TestCase(-2.4, -2.4, true)]
        //[TestCase(-2.1, -1.1, false)]
        //public void Operator_Equals_Double_and_Double(double value1, double value2, bool isEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 == operand2);

        //    Assert.That(result, Is.EqualTo(isEqual));

        //    result = (operand2 == operand1);

        //    Assert.That(result, Is.EqualTo(isEqual));
        //}

        //[TestCase("Foo", "Bar", false)]
        //[TestCase("Foo", "Foo", true)]
        //[TestCase("-2", "-2", true)]
        //[TestCase("-2", "-3", false)]
        //public void Operator_Equals_String_and_String(string value1, string value2, bool isEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 == operand2);

        //    Assert.That(result, Is.EqualTo(isEqual));

        //    result = (operand2 == operand1);

        //    Assert.That(result, Is.EqualTo(isEqual));
        //}

        //[TestCase(2, 2.2, false)]
        //[TestCase(0, 0.0, true)]
        //[TestCase(2, 2.0, true)]
        //[TestCase(-2, -2.0, true)]
        //[TestCase(-2, -2.2, false)]
        //public void Operator_Equals_Integer_and_Double(int value1, double value2, bool isEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 == operand2);

        //    Assert.That(result, Is.EqualTo(isEqual));

        //    result = (operand2 == operand1);

        //    Assert.That(result, Is.EqualTo(isEqual));
        //}


        //[TestCase("2", 1, false)]
        //[TestCase("0", 0, true)]
        //[TestCase("2", 2, true)]
        //[TestCase("-2", -2, true)]
        //[TestCase("-2", -1, false)]
        //public void Operator_Equals_String_and_Integer(string value1, int value2, bool isEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 == operand2);

        //    Assert.That(result, Is.EqualTo(isEqual));

        //    result = (operand2 == operand1);

        //    Assert.That(result, Is.EqualTo(isEqual));
        //}

        //[TestCase("2.1", 2.2, false)]
        //[TestCase("0", 0.0, true)]
        //[TestCase("2.2", 2.2, true)]
        //[TestCase("-2.4", -2.4, true)]
        //[TestCase("-2.5", -2.2, false)]
        //public void Operator_Equals_String_and_Double(string value1, double value2, bool isEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 == operand2);

        //    Assert.That(result, Is.EqualTo(isEqual));

        //    result = (operand2 == operand1);

        //    Assert.That(result, Is.EqualTo(isEqual));
        //}


        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase("", null)]
        //public void Operator_Not_Equals_Null_Or_Empty(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 != operand2);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(2, "")]
        //[TestCase(2, null)]
        //public void Operator_Not_Equals_Partial_Null_Or_Empty(int value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 != operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 != operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(3, 2, true)]
        //[TestCase(2, 2, false)]
        //[TestCase(0, 0, false)]
        //[TestCase(-2, -2, false)]
        //[TestCase(-2, -1, true)]
        //public void Operator_Not_Equals_Integer_and_Integer(int value1, int value2, bool isNotEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 != operand2);

        //    Assert.That(result, Is.EqualTo(isNotEqual));

        //    result = (operand2 != operand1);

        //    Assert.That(result, Is.EqualTo(isNotEqual));
        //}

        //[TestCase(3.2, 2.2, true)]
        //[TestCase(2.2, 2.2, false)]
        //[TestCase(0.0, 0.0, false)]
        //[TestCase(-2.4, -2.4, false)]
        //[TestCase(-2.1, -1.1, true)]
        //public void Operator_Not_Equals_Double_and_Double(double value1, double value2, bool isNotEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 != operand2);

        //    Assert.That(result, Is.EqualTo(isNotEqual));

        //    result = (operand2 != operand1);

        //    Assert.That(result, Is.EqualTo(isNotEqual));
        //}

        //[TestCase("Foo", "Bar", true)]
        //[TestCase("Foo", "Foo", false)]
        //[TestCase("-2", "-2", false)]
        //[TestCase("-2", "-3", true)]
        //public void Operator_Not_Equals_String_and_String(string value1, string value2, bool isNotEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 != operand2);

        //    Assert.That(result, Is.EqualTo(isNotEqual));

        //    result = (operand2 != operand1);

        //    Assert.That(result, Is.EqualTo(isNotEqual));
        //}

        //[TestCase(2, 2.2, true)]
        //[TestCase(0, 0.0, false)]
        //[TestCase(2, 2.0, false)]
        //[TestCase(-2, -2.0, false)]
        //[TestCase(-2, -2.2, true)]
        //public void Operator_Not_Equals_Integer_and_Double(int value1, double value2, bool isNotEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 != operand2);

        //    Assert.That(result, Is.EqualTo(isNotEqual));

        //    result = (operand2 != operand1);

        //    Assert.That(result, Is.EqualTo(isNotEqual));
        //}


        //[TestCase("2", 1, true)]
        //[TestCase("0", 0, false)]
        //[TestCase("2", 2, false)]
        //[TestCase("-2", -2, false)]
        //[TestCase("-2", -1, true)]
        //public void Operator_Not_Equals_String_and_Integer(string value1, int value2, bool isNotEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 != operand2);

        //    Assert.That(result, Is.EqualTo(isNotEqual));

        //    result = (operand2 != operand1);

        //    Assert.That(result, Is.EqualTo(isNotEqual));
        //}

        //[TestCase("2.1", 2.2, true)]
        //[TestCase("0", 0.0, false)]
        //[TestCase("2.2", 2.2, false)]
        //[TestCase("-2.4", -2.4, false)]
        //[TestCase("-2.5", -2.2, true)]
        //public void Operator_Not_Equals_String_and_Double(string value1, double value2, bool isNotEqual)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 != operand2);

        //    Assert.That(result, Is.EqualTo(isNotEqual));

        //    result = (operand2 != operand1);

        //    Assert.That(result, Is.EqualTo(isNotEqual));
        //}

        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase("", null)]
        //public void Operator_Greater_Null_Or_Empty(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(2, "")]
        //[TestCase(2, null)]
        //public void Operator_Greater_Partial_Null_Or_Empty(int value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(3, 2)]
        //[TestCase(3, 0)]
        //[TestCase(-1, -2)]
        //[TestCase(0, -2)]
        //public void Operator_Greater_Integer_and_Integer(int value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(2, 2)]
        //[TestCase(0, 0)]
        //[TestCase(-2, -2)]
        //public void Operator_Greater_Integer_and_Integer_of_Identical_Values(int value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(3.2, 2.2)]
        //[TestCase(3.2, 0.0)]
        //[TestCase(-2.4, -2.5)]
        //[TestCase(0.0, -1.1)]
        //public void Operator_Greater_Double_and_Double(double value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(2.2, 2.2)]
        //[TestCase(0.0, 0.0)]
        //[TestCase(-2.4, -2.4)]
        //public void Operator_Greater_Double_and_Double_of_Identical_Values(double value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("Foo", "Bar")]
        //[TestCase("Fo", "Bara")]
        //[TestCase("-2", "-3")]
        //[TestCase("0", "-3")]
        //[TestCase("3", "2")]
        //[TestCase("3", "0")]
        //public void Operator_Greater_String_and_String(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("Foo", "Foo")]
        //[TestCase("-2", "-2")]
        //public void Operator_Greater_String_and_String_of_Identical_Values(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(3, 2.2)]
        //[TestCase(3, 0)]
        //[TestCase(0, -2.2)]
        //[TestCase(-2, -2.2)]
        //public void Operator_Greater_Integer_and_Double(int value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(0, 0.0)]
        //[TestCase(2, 2.0)]
        //[TestCase(-2, -2.0)]
        //public void Operator_Greater_Integer_and_Double_of_Identical_Values(int value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}


        //[TestCase("2", 1)]
        //[TestCase("2", 0)]
        //[TestCase("0", -3)]
        //[TestCase("-2", -3)]
        //public void Operator_Greater_String_and_Integer(string value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("0", 0)]
        //[TestCase("2", 2)]
        //[TestCase("-2", -2)]
        //public void Operator_Greater_String_and_Integer_of_Identical_Values(string value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("2.3", 2.2)]
        //[TestCase("2.3", 0)]
        //[TestCase("0", -2.2)]
        //[TestCase("-2.1", -2.2)]
        //public void Operator_Greater_String_and_Double(string value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("0", 0.0)]
        //[TestCase("2.2", 2.2)]
        //[TestCase("-2.4", -2.4)]
        //public void Operator_Greater_String_and_Double_of_Identical_Values(string value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase("", null)]
        //public void Operator_Lesser_Null_Or_Empty(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(2, "")]
        //[TestCase(2, null)]
        //public void Operator_Lesser_Partial_Null_Or_Empty(int value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(3, 2)]
        //[TestCase(3, 0)]
        //[TestCase(-1, -2)]
        //[TestCase(0, -2)]
        //public void Operator_Lesser_Integer_and_Integer(int value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(2, 2)]
        //[TestCase(0, 0)]
        //[TestCase(-2, -2)]
        //public void Operator_Lesser_Integer_and_Integer_of_Identical_Values(int value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(3.2, 2.2)]
        //[TestCase(3.2, 0.0)]
        //[TestCase(-2.4, -2.5)]
        //[TestCase(0.0, -1.1)]
        //public void Operator_Lesser_Double_and_Double(double value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(2.2, 2.2)]
        //[TestCase(0.0, 0.0)]
        //[TestCase(-2.4, -2.4)]
        //public void Operator_Lesser_Double_and_Double_of_Identical_Values(double value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("Foo", "Bar")]
        //[TestCase("Fo", "Bara")]
        //[TestCase("-2", "-3")]
        //[TestCase("0", "-3")]
        //[TestCase("3", "2")]
        //[TestCase("3", "0")]
        //public void Operator_Lesser_String_and_String(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("Foo", "Foo")]
        //[TestCase("-2", "-2")]
        //public void Operator_Lesser_String_and_String_of_Identical_Values(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(3, 2.2)]
        //[TestCase(3, 0)]
        //[TestCase(0, -2.2)]
        //[TestCase(-2, -2.2)]
        //public void Operator_Lesser_Integer_and_Double(int value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(0, 0.0)]
        //[TestCase(2, 2.0)]
        //[TestCase(-2, -2.0)]
        //public void Operator_Lesser_Integer_and_Double_of_Identical_Values(int value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsFalse(result);
        //}


        //[TestCase("2", 1)]
        //[TestCase("2", 0)]
        //[TestCase("0", -3)]
        //[TestCase("-2", -3)]
        //public void Operator_Lesser_String_and_Integer(string value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("0", 0)]
        //[TestCase("2", 2)]
        //[TestCase("-2", -2)]
        //public void Operator_Lesser_String_and_Integer_of_Identical_Values(string value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("2.3", 2.2)]
        //[TestCase("2.3", 0)]
        //[TestCase("0", -2.2)]
        //[TestCase("-2.1", -2.2)]
        //public void Operator_Lesser_String_and_Double(string value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("0", 0.0)]
        //[TestCase("2.2", 2.2)]
        //[TestCase("-2.4", -2.4)]
        //public void Operator_Lesser_String_and_Double_of_Identical_Values(string value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase("", null)]
        //public void Operator_Greater_or_Equal_Null_Or_Empty(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(2, "")]
        //[TestCase(2, null)]
        //public void Operator_Greater_or_Equal_Partial_Null_Or_Empty(int value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(3, 2)]
        //[TestCase(3, 0)]
        //[TestCase(-1, -2)]
        //[TestCase(0, -2)]
        //public void Operator_Greater_or_Equal_Integer_and_Integer(int value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(2, 2)]
        //[TestCase(0, 0)]
        //[TestCase(-2, -2)]
        //public void Operator_Greater_or_Equal_Integer_and_Integer_of_Identical_Values(int value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(3.2, 2.2)]
        //[TestCase(3.2, 0.0)]
        //[TestCase(-2.4, -2.5)]
        //[TestCase(0.0, -1.1)]
        //public void Operator_Greater_or_Equal_Double_and_Double(double value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(2.2, 2.2)]
        //[TestCase(0.0, 0.0)]
        //[TestCase(-2.4, -2.4)]
        //public void Operator_Greater_or_Equal_Double_and_Double_of_Identical_Values(double value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("Foo", "Bar")]
        //[TestCase("Fo", "Bara")]
        //[TestCase("-2", "-3")]
        //[TestCase("0", "-3")]
        //[TestCase("3", "2")]
        //[TestCase("3", "0")]
        //public void Operator_Greater_or_Equal_String_and_String(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("Foo", "Foo")]
        //[TestCase("-2", "-2")]
        //public void Operator_Greater_or_Equal_String_and_String_of_Identical_Values(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(3, 2.2)]
        //[TestCase(3, 0)]
        //[TestCase(0, -2.2)]
        //[TestCase(-2, -2.2)]
        //public void Operator_Greater_or_Equal_Integer_and_Double(int value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase(0, 0.0)]
        //[TestCase(2, 2.0)]
        //[TestCase(-2, -2.0)]
        //public void Operator_Greater_or_Equal_Integer_and_Double_of_Identical_Values(int value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsTrue(result);
        //}


        //[TestCase("2", 1)]
        //[TestCase("2", 0)]
        //[TestCase("0", -3)]
        //[TestCase("-2", -3)]
        //public void Operator_Greater_or_Equal_String_and_Integer(string value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("0", 0)]
        //[TestCase("2", 2)]
        //[TestCase("-2", -2)]
        //public void Operator_Greater_or_Equal_String_and_Integer_of_Identical_Values(string value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("2.3", 2.2)]
        //[TestCase("2.3", 0)]
        //[TestCase("0", -2.2)]
        //[TestCase("-2.1", -2.2)]
        //public void Operator_Greater_or_Equal_String_and_Double(string value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("0", 0.0)]
        //[TestCase("2.2", 2.2)]
        //[TestCase("-2.4", -2.4)]
        //public void Operator_Greater_or_Equal_String_and_Double_of_Identical_Values(string value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(null, null)]
        //[TestCase("", "")]
        //[TestCase(null, "")]
        //[TestCase("", null)]
        //public void Operator_Lesser_or_Equal_Null_Or_Empty(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(2, "")]
        //[TestCase(2, null)]
        //public void Operator_Lesser_or_Equal_Partial_Null_Or_Empty(int value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(3, 2)]
        //[TestCase(3, 0)]
        //[TestCase(-1, -2)]
        //[TestCase(0, -2)]
        //public void Operator_Lesser_or_Equal_Integer_and_Integer(int value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(2, 2)]
        //[TestCase(0, 0)]
        //[TestCase(-2, -2)]
        //public void Operator_Lesser_or_Equal_Integer_and_Integer_of_Identical_Values(int value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(3.2, 2.2)]
        //[TestCase(3.2, 0.0)]
        //[TestCase(-2.4, -2.5)]
        //[TestCase(0.0, -1.1)]
        //public void Operator_Lesser_or_Equal_Double_and_Double(double value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(2.2, 2.2)]
        //[TestCase(0.0, 0.0)]
        //[TestCase(-2.4, -2.4)]
        //public void Operator_Lesser_or_Equal_Double_and_Double_of_Identical_Values(double value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("Foo", "Bar")]
        //[TestCase("Fo", "Bara")]
        //[TestCase("-2", "-3")]
        //[TestCase("0", "-3")]
        //[TestCase("3", "2")]
        //[TestCase("3", "0")]
        //public void Operator_Lesser_or_Equal_String_and_String(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("Foo", "Foo")]
        //[TestCase("-2", "-2")]
        //public void Operator_Lesser_or_Equal_String_and_String_of_Identical_Values(string value1, string value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(3, 2.2)]
        //[TestCase(3, 0)]
        //[TestCase(0, -2.2)]
        //[TestCase(-2, -2.2)]
        //public void Operator_Lesser_or_Equal_Integer_and_Double(int value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase(0, 0.0)]
        //[TestCase(2, 2.0)]
        //[TestCase(-2, -2.0)]
        //public void Operator_Lesser_or_Equal_Integer_and_Double_of_Identical_Values(int value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}


        //[TestCase("2", 1)]
        //[TestCase("2", 0)]
        //[TestCase("0", -3)]
        //[TestCase("-2", -3)]
        //public void Operator_Lesser_or_Equal_String_and_Integer(string value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("0", 0)]
        //[TestCase("2", 2)]
        //[TestCase("-2", -2)]
        //public void Operator_Lesser_or_Equal_String_and_Integer_of_Identical_Values(string value1, int value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("2.3", 2.2)]
        //[TestCase("2.3", 0)]
        //[TestCase("0", -2.2)]
        //[TestCase("-2.1", -2.2)]
        //public void Operator_Lesser_or_Equal_String_and_Double(string value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("0", 0.0)]
        //[TestCase("2.2", 2.2)]
        //[TestCase("-2.4", -2.4)]
        //public void Operator_Lesser_or_Equal_String_and_Double_of_Identical_Values(string value1, double value2)
        //{
        //    ExponentUnit operand1 = new ExponentUnit(value1);
        //    ExponentUnit operand2 = new ExponentUnit(value2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}
    }
}
