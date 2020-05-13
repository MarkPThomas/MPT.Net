using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{
    // TODO: Handle integer and double values of 0

    [TestFixture]
    public class SymbolicValueTests
    {
        [TestCase(5, "5", 5, 5.0)]
        [TestCase(-5, "-5", -5, -5.0)]
        [TestCase(0, "0", 0, 0.0)]
        public void Initialize_As_Integer(int value, string expectedString, int expectedInt, double expectedFloat)
        {
            SymbolicUnit symbolicValue = new SymbolicUnit(value);

            Assert.That(symbolicValue.AsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.AsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.AsFloat(), Is.EqualTo(expectedFloat));
            Assert.IsTrue(symbolicValue.IsNumber);
            Assert.IsFalse(symbolicValue.IsFloat);
            Assert.IsTrue(symbolicValue.IsInteger);
        }


        [TestCase(5.6, "5.6", 6, 5.6)]
        [TestCase(5.5, "5.5", 6, 5.5)]
        [TestCase(5.4, "5.4", 5, 5.4)]
        [TestCase(-5.6, "-5.6", -6, -5.6)]
        [TestCase(-5.5, "-5.5", -6, -5.5)]
        [TestCase(-5.4, "-5.4", -5, -5.4)]
        [TestCase(0.0, "0", 0, 0.0)]
        [TestCase(-0.0, "0", 0, 0.0)]
        public void Initialize_As_Float(double value, string expectedString, int expectedInt, double expectedFloat)
        {
            SymbolicUnit symbolicValue = new SymbolicUnit(value);

            Assert.That(symbolicValue.AsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.AsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.AsFloat(), Is.EqualTo(expectedFloat));
            Assert.IsTrue(symbolicValue.IsNumber);
            Assert.IsTrue(symbolicValue.IsFloat);
            Assert.IsFalse(symbolicValue.IsInteger);
        }

        [TestCase("1", "1", 1, 1.0, true)]
        [TestCase("-1", "-1", -1, -1.0, true)]
        [TestCase("25.3", "25.3", 25, 25.3, false)]
        [TestCase("-25.3", "-25.3", -25, -25.3, false)]
        [TestCase("-25.", "-25.", -25, -25.0, true)]
        [TestCase("1/2", "1/2", 0, 0.5, false)]
        public void Initialize_Numeric_As_String(string value, string expectedString, int expectedInt, double expectedFloat, bool isInteger)
        {
            SymbolicUnit symbolicValue = new SymbolicUnit(value);

            Assert.That(symbolicValue.AsString(), Is.EqualTo(expectedString));
            Assert.That(symbolicValue.AsInteger(), Is.EqualTo(expectedInt));
            Assert.That(symbolicValue.AsFloat(), Is.EqualTo(expectedFloat));
            Assert.IsTrue(symbolicValue.IsNumber);
            Assert.That(symbolicValue.IsFloat, Is.Not.EqualTo(isInteger));
            Assert.That(symbolicValue.IsInteger, Is.EqualTo(isInteger));
        }

        [TestCase(null, "", 0, 0.0)]
        [TestCase("", "", 0, 0.0)]
        [TestCase("1.1.3", "1.1.3", 0, 0.0)]
        [TestCase("Foo", "Foo", 0, 0.0)]
        public void Initialize_NonNumeric_As_String(string value, string expectedString, int expectedInt, double expectedFloat)
        {
            SymbolicUnit symbolicValue = new SymbolicUnit(null);

            Assert.That(symbolicValue.AsString(), Is.EqualTo(string.Empty));
            Assert.That(symbolicValue.AsInteger(), Is.EqualTo(0));
            Assert.That(symbolicValue.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(symbolicValue.IsNumber);
            Assert.IsFalse(symbolicValue.IsFloat);
            Assert.IsFalse(symbolicValue.IsInteger);
        }

        [TestCase(null, "MPT.SymbolicMath.SymbolicUnit")]
        [TestCase("", "MPT.SymbolicMath.SymbolicUnit")]
        [TestCase("1.1.3", "1.1.3")]
        [TestCase("Foo", "Foo")]
        [TestCase("0", "0")]
        [TestCase("5", "5")]
        [TestCase("-5", "-5")]
        [TestCase("-300.2", "-300.2")]
        public void Override_ToString(string value, string expectedString)
        {
            SymbolicUnit symbolicValue = new SymbolicUnit(value);
            Assert.That(symbolicValue.ToString(), Is.EqualTo(expectedString));
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Add_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);
            SymbolicUnit value3 = operand1 + operand2;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Add_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);
            SymbolicUnit value3 = operand1 + operand2;

            Assert.That(value3.AsString(), Is.EqualTo("2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(2));
            Assert.That(value3.AsFloat(), Is.EqualTo(2.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsTrue(value3.IsInteger);
            
            value3 = operand2 + operand1;

            Assert.That(value3.AsString(), Is.EqualTo("2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(2));
            Assert.That(value3.AsFloat(), Is.EqualTo(2.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsTrue(value3.IsInteger);
        }

        [Test]
        public void Operator_Add_Integer_and_Integer()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3);
            SymbolicUnit operand2 = new SymbolicUnit(2);

            SymbolicUnit value3 = operand1 + operand2;

            Assert.That(value3.AsString(), Is.EqualTo("5"));
            Assert.That(value3.AsInteger(), Is.EqualTo(5));
            Assert.That(value3.AsFloat(), Is.EqualTo(5.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsTrue(value3.IsInteger);
            
            value3 = operand2 + operand1;

            Assert.That(value3.AsString(), Is.EqualTo("5"));
            Assert.That(value3.AsInteger(), Is.EqualTo(5));
            Assert.That(value3.AsFloat(), Is.EqualTo(5.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsTrue(value3.IsInteger);
        }

        [Test]
        public void Operator_Add_Double_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3.2);
            SymbolicUnit operand2 = new SymbolicUnit(2.74);

            SymbolicUnit value3 = operand1 + operand2;

            Assert.That(value3.AsString(), Is.EqualTo("5.94"));
            Assert.That(value3.AsInteger(), Is.EqualTo(6));
            Assert.That(value3.AsFloat(), Is.EqualTo(5.94));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
            
            value3 = operand2 + operand1;

            Assert.That(value3.AsString(), Is.EqualTo("5.94"));
            Assert.That(value3.AsInteger(), Is.EqualTo(6));
            Assert.That(value3.AsFloat(), Is.EqualTo(5.94));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Add_String_and_String()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit("Bar");

            SymbolicUnit value3 = operand1 + operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo+Bar"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0));
            
            value3 = operand2 + operand1;

            Assert.That(value3.AsString(), Is.EqualTo("Bar+Foo"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0));
        }

        [Test]
        public void Operator_Add_Integer_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3.2);
            SymbolicUnit operand2 = new SymbolicUnit(2);

            SymbolicUnit value3 = operand1 + operand2;

            Assert.That(value3.AsString(), Is.EqualTo("5.2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(5));
            Assert.That(value3.AsFloat(), Is.EqualTo(5.2));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
            
            value3 = operand2 + operand1;

            Assert.That(value3.AsString(), Is.EqualTo("5.2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(5));
            Assert.That(value3.AsFloat(), Is.EqualTo(5.2));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }


        [Test]
        public void Operator_Add_String_and_Integer()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit(3);

            SymbolicUnit value3 = operand1 + operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo+3"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
            
            value3 = operand2 + operand1;

            Assert.That(value3.AsString(), Is.EqualTo("3+Foo"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Add_String_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit(2.2);

            SymbolicUnit value3 = operand1 + operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo+2.2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
            
            value3 = operand2 + operand1;

            Assert.That(value3.AsString(), Is.EqualTo("2.2+Foo"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Subtract_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);
            SymbolicUnit value3 = operand1 - operand2;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Subtract_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            SymbolicUnit value3 = operand1 - operand2;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
            
            value3 = operand2 - operand1;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Subtract_Integer_and_Integer()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3);
            SymbolicUnit operand2 = new SymbolicUnit(2);

            SymbolicUnit value3 = operand1 - operand2;

            Assert.That(value3.AsString(), Is.EqualTo("1"));
            Assert.That(value3.AsInteger(), Is.EqualTo(1));
            Assert.That(value3.AsFloat(), Is.EqualTo(1.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsTrue(value3.IsInteger);

            value3 = operand2 - operand1;

            Assert.That(value3.AsString(), Is.EqualTo("-1"));
            Assert.That(value3.AsInteger(), Is.EqualTo(-1));
            Assert.That(value3.AsFloat(), Is.EqualTo(-1.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsTrue(value3.IsInteger);
        }

        [Test]
        public void Operator_Subtract_Double_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3.2);
            SymbolicUnit operand2 = new SymbolicUnit(2.74);

            SymbolicUnit value3 = operand1 - operand2;

            Assert.That(value3.AsString(), Is.EqualTo("0.46"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.46));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 - operand1;

            Assert.That(value3.AsString(), Is.EqualTo("-0.46"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(-0.46));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Subtract_String_and_String()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit("Bar");
            SymbolicUnit value3 = operand1 - operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo-Bar"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Subtract_Integer_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3.2);
            SymbolicUnit operand2 = new SymbolicUnit(2);

            SymbolicUnit value3 = operand1 - operand2;

            Assert.That(value3.AsString(), Is.EqualTo("1.2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(1));
            Assert.That(value3.AsFloat(), Is.EqualTo(1.2));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 - operand1;

            Assert.That(value3.AsString(), Is.EqualTo("-1.2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(-1));
            Assert.That(value3.AsFloat(), Is.EqualTo(-1.2));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }


        [Test]
        public void Operator_Subtract_String_and_Integer()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit(3);

            SymbolicUnit value3 = operand1 - operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo-3"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 - operand1;

            Assert.That(value3.AsString(), Is.EqualTo("3-Foo"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Subtract_String_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit(2.2);
            SymbolicUnit value3 = operand1 - operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo-2.2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 - operand1;

            Assert.That(value3.AsString(), Is.EqualTo("2.2-Foo"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }
        
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Multiply_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);
            SymbolicUnit value3 = operand1 * operand2;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Multiply_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);
            SymbolicUnit value3 = operand1 * operand2;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
            
            value3 = operand2 * operand1;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Multiply_Integer_and_Integer()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3);
            SymbolicUnit operand2 = new SymbolicUnit(2);

            SymbolicUnit value3 = operand1 * operand2;

            Assert.That(value3.AsString(), Is.EqualTo("6"));
            Assert.That(value3.AsInteger(), Is.EqualTo(6));
            Assert.That(value3.AsFloat(), Is.EqualTo(6.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsTrue(value3.IsInteger);

            value3 = operand2 * operand1;

            Assert.That(value3.AsString(), Is.EqualTo("6"));
            Assert.That(value3.AsInteger(), Is.EqualTo(6));
            Assert.That(value3.AsFloat(), Is.EqualTo(6.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsTrue(value3.IsInteger);
        }

        [Test]
        public void Operator_Multiply_Double_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3.2);
            SymbolicUnit operand2 = new SymbolicUnit(2.74);

            SymbolicUnit value3 = operand1 * operand2;

            Assert.That(value3.AsString(), Is.EqualTo("8.768"));
            Assert.That(value3.AsInteger(), Is.EqualTo(9));
            Assert.That(value3.AsFloat(), Is.EqualTo(8.768));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 * operand1;

            Assert.That(value3.AsString(), Is.EqualTo("8.768"));
            Assert.That(value3.AsInteger(), Is.EqualTo(9));
            Assert.That(value3.AsFloat(), Is.EqualTo(8.768));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Multiply_String_and_String()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit("Bar");
            SymbolicUnit value3 = operand1 * operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo*Bar"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Multiply_Integer_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3.2);
            SymbolicUnit operand2 = new SymbolicUnit(2);

            SymbolicUnit value3 = operand1 * operand2;

            Assert.That(value3.AsString(), Is.EqualTo("6.4"));
            Assert.That(value3.AsInteger(), Is.EqualTo(6));
            Assert.That(value3.AsFloat(), Is.EqualTo(6.4));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 * operand1;

            Assert.That(value3.AsString(), Is.EqualTo("6.4"));
            Assert.That(value3.AsInteger(), Is.EqualTo(6));
            Assert.That(value3.AsFloat(), Is.EqualTo(6.4));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }


        [Test]
        public void Operator_Multiply_String_and_Integer()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit(3);

            SymbolicUnit value3 = operand1 * operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo*3"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 * operand1;

            Assert.That(value3.AsString(), Is.EqualTo("3*Foo"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Multiply_String_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit(2.2);
            SymbolicUnit value3 = operand1 * operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo*2.2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 * operand1;

            Assert.That(value3.AsString(), Is.EqualTo("2.2*Foo"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }


        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Divide_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);
            SymbolicUnit value3 = operand1 / operand2;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Divide_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);
            SymbolicUnit value3 = operand1 / operand2;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 / operand1;

            Assert.That(value3.AsString(), Is.EqualTo(string.Empty));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Divide_Integer_and_Integer()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3);
            SymbolicUnit operand2 = new SymbolicUnit(2);

            SymbolicUnit value3 = operand1 / operand2;

            Assert.That(value3.AsString(), Is.EqualTo("1.5"));
            Assert.That(value3.AsInteger(), Is.EqualTo(2));
            Assert.That(value3.AsFloat(), Is.EqualTo(1.5));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 / operand1;

            Assert.That(value3.AsString(), Is.EqualTo("0.666666666666667"));
            Assert.That(value3.AsInteger(), Is.EqualTo(1));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.666666666666667));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            operand1 = new SymbolicUnit(4);
            operand2 = new SymbolicUnit(2);

            value3 = operand1 / operand2;

            Assert.That(value3.AsString(), Is.EqualTo("2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(2));
            Assert.That(value3.AsFloat(), Is.EqualTo(2.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsTrue(value3.IsInteger);
        }

        [Test]
        public void Operator_Divide_Double_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3.2);
            SymbolicUnit operand2 = new SymbolicUnit(1.6);

            SymbolicUnit value3 = operand1 / operand2;

            Assert.That(value3.AsString(), Is.EqualTo("2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(2));
            Assert.That(value3.AsFloat(), Is.EqualTo(2.0));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 / operand1;

            Assert.That(value3.AsString(), Is.EqualTo("0.5"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.5));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Divide_String_and_String()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit("Bar");
            SymbolicUnit value3 = operand1 / operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo/Bar"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Divide_Integer_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit(3.2);
            SymbolicUnit operand2 = new SymbolicUnit(2);

            SymbolicUnit value3 = operand1 / operand2;

            Assert.That(value3.AsString(), Is.EqualTo("1.6"));
            Assert.That(value3.AsInteger(), Is.EqualTo(2));
            Assert.That(value3.AsFloat(), Is.EqualTo(1.6));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 / operand1;

            Assert.That(value3.AsString(), Is.EqualTo("0.625"));
            Assert.That(value3.AsInteger(), Is.EqualTo(1));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.625));
            Assert.IsTrue(value3.IsNumber);
            Assert.IsTrue(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }


        [Test]
        public void Operator_Divide_String_and_Integer()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit(3);

            SymbolicUnit value3 = operand1 / operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo/3"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 / operand1;

            Assert.That(value3.AsString(), Is.EqualTo("3/Foo"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [Test]
        public void Operator_Divide_String_and_Double()
        {
            SymbolicUnit operand1 = new SymbolicUnit("Foo");
            SymbolicUnit operand2 = new SymbolicUnit(2.2);
            SymbolicUnit value3 = operand1 / operand2;

            Assert.That(value3.AsString(), Is.EqualTo("Foo/2.2"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);

            value3 = operand2 / operand1;

            Assert.That(value3.AsString(), Is.EqualTo("2.2/Foo"));
            Assert.That(value3.AsInteger(), Is.EqualTo(0));
            Assert.That(value3.AsFloat(), Is.EqualTo(0.0));
            Assert.IsFalse(value3.IsNumber);
            Assert.IsFalse(value3.IsFloat);
            Assert.IsFalse(value3.IsInteger);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Equals_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 == operand2);

            Assert.IsTrue(result);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Equals_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 == operand2);

            Assert.IsFalse(result);

            result = (operand2 == operand1);

            Assert.IsFalse(result);
        }

        [TestCase(3, 2, false)]
        [TestCase(2, 2, true)]
        [TestCase(0, 0, true)]
        [TestCase(-2, -2, true)]
        [TestCase(-2, -1, false)]
        public void Operator_Equals_Integer_and_Integer(int value1, int value2, bool isEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 == operand2);

            Assert.That(result, Is.EqualTo(isEqual));

            result = (operand2 == operand1);

            Assert.That(result, Is.EqualTo(isEqual));
        }

        [TestCase(3.2, 2.2, false)]
        [TestCase(2.2, 2.2, true)]
        [TestCase(0.0, 0.0, true)]
        [TestCase(-2.4, -2.4, true)]
        [TestCase(-2.1, -1.1, false)]
        public void Operator_Equals_Double_and_Double(double value1, double value2, bool isEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 == operand2);

            Assert.That(result, Is.EqualTo(isEqual));

            result = (operand2 == operand1);

            Assert.That(result, Is.EqualTo(isEqual));
        }

        [TestCase("Foo", "Bar", false)]
        [TestCase("Foo", "Foo", true)]
        [TestCase("-2", "-2", true)]
        [TestCase("-2", "-3", false)]
        public void Operator_Equals_String_and_String(string value1, string value2, bool isEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 == operand2);

            Assert.That(result, Is.EqualTo(isEqual));

            result = (operand2 == operand1);

            Assert.That(result, Is.EqualTo(isEqual));
        }

        [TestCase(2, 2.2, false)]
        [TestCase(0, 0.0, true)]
        [TestCase(2, 2.0, true)]
        [TestCase(-2, -2.0, true)]
        [TestCase(-2, -2.2, false)]
        public void Operator_Equals_Integer_and_Double(int value1, double value2, bool isEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 == operand2);

            Assert.That(result, Is.EqualTo(isEqual));

            result = (operand2 == operand1);

            Assert.That(result, Is.EqualTo(isEqual));
        }


        [TestCase("2", 1, false)]
        [TestCase("0", 0, true)]
        [TestCase("2", 2, true)]
        [TestCase("-2", -2, true)]
        [TestCase("-2", -1, false)]
        public void Operator_Equals_String_and_Integer(string value1, int value2, bool isEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 == operand2);

            Assert.That(result, Is.EqualTo(isEqual));

            result = (operand2 == operand1);

            Assert.That(result, Is.EqualTo(isEqual));
        }

        [TestCase("2.1", 2.2, false)]
        [TestCase("0", 0.0, true)]
        [TestCase("2.2", 2.2, true)]
        [TestCase("-2.4", -2.4, true)]
        [TestCase("-2.5", -2.2, false)]
        public void Operator_Equals_String_and_Double(string value1, double value2, bool isEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 == operand2);

            Assert.That(result, Is.EqualTo(isEqual));

            result = (operand2 == operand1);

            Assert.That(result, Is.EqualTo(isEqual));
        }


        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Not_Equals_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 != operand2);

            Assert.IsFalse(result);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Not_Equals_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 != operand2);

            Assert.IsTrue(result);

            result = (operand2 != operand1);

            Assert.IsTrue(result);
        }

        [TestCase(3, 2, true)]
        [TestCase(2, 2, false)]
        [TestCase(0, 0, false)]
        [TestCase(-2, -2, false)]
        [TestCase(-2, -1, true)]
        public void Operator_Not_Equals_Integer_and_Integer(int value1, int value2, bool isNotEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 != operand2);

            Assert.That(result, Is.EqualTo(isNotEqual));

            result = (operand2 != operand1);

            Assert.That(result, Is.EqualTo(isNotEqual));
        }

        [TestCase(3.2, 2.2, true)]
        [TestCase(2.2, 2.2, false)]
        [TestCase(0.0, 0.0, false)]
        [TestCase(-2.4, -2.4, false)]
        [TestCase(-2.1, -1.1, true)]
        public void Operator_Not_Equals_Double_and_Double(double value1, double value2, bool isNotEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 != operand2);

            Assert.That(result, Is.EqualTo(isNotEqual));

            result = (operand2 != operand1);

            Assert.That(result, Is.EqualTo(isNotEqual));
        }

        [TestCase("Foo", "Bar", true)]
        [TestCase("Foo", "Foo", false)]
        [TestCase("-2", "-2", false)]
        [TestCase("-2", "-3", true)]
        public void Operator_Not_Equals_String_and_String(string value1, string value2, bool isNotEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 != operand2);

            Assert.That(result, Is.EqualTo(isNotEqual));

            result = (operand2 != operand1);

            Assert.That(result, Is.EqualTo(isNotEqual));
        }

        [TestCase(2, 2.2, true)]
        [TestCase(0, 0.0, false)]
        [TestCase(2, 2.0, false)]
        [TestCase(-2, -2.0, false)]
        [TestCase(-2, -2.2, true)]
        public void Operator_Not_Equals_Integer_and_Double(int value1, double value2, bool isNotEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 != operand2);

            Assert.That(result, Is.EqualTo(isNotEqual));

            result = (operand2 != operand1);

            Assert.That(result, Is.EqualTo(isNotEqual));
        }


        [TestCase("2", 1, true)]
        [TestCase("0", 0, false)]
        [TestCase("2", 2, false)]
        [TestCase("-2", -2, false)]
        [TestCase("-2", -1, true)]
        public void Operator_Not_Equals_String_and_Integer(string value1, int value2, bool isNotEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 != operand2);

            Assert.That(result, Is.EqualTo(isNotEqual));

            result = (operand2 != operand1);

            Assert.That(result, Is.EqualTo(isNotEqual));
        }

        [TestCase("2.1", 2.2, true)]
        [TestCase("0", 0.0, false)]
        [TestCase("2.2", 2.2, false)]
        [TestCase("-2.4", -2.4, false)]
        [TestCase("-2.5", -2.2, true)]
        public void Operator_Not_Equals_String_and_Double(string value1, double value2, bool isNotEqual)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 != operand2);

            Assert.That(result, Is.EqualTo(isNotEqual));

            result = (operand2 != operand1);

            Assert.That(result, Is.EqualTo(isNotEqual));
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Greater_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsFalse(result);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Greater_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsTrue(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }

        [TestCase(3, 2)]
        [TestCase(3, 0)]
        [TestCase(-1, -2)]
        [TestCase(0, -2)]
        public void Operator_Greater_Integer_and_Integer(int value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsTrue(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }
        
        [TestCase(2, 2)]
        [TestCase(0, 0)]
        [TestCase(-2, -2)]
        public void Operator_Greater_Integer_and_Integer_of_Identical_Values(int value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsFalse(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }

        [TestCase(3.2, 2.2)]
        [TestCase(3.2, 0.0)]
        [TestCase(-2.4, -2.5)]
        [TestCase(0.0, -1.1)]
        public void Operator_Greater_Double_and_Double(double value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsTrue(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }
        
        [TestCase(2.2, 2.2)]
        [TestCase(0.0, 0.0)]
        [TestCase(-2.4, -2.4)]
        public void Operator_Greater_Double_and_Double_of_Identical_Values(double value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsFalse(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }

        [TestCase("Foo", "Bar")]
        [TestCase("Fo", "Bara")]
        [TestCase("-2", "-3")]
        [TestCase("0", "-3")]
        [TestCase("3", "2")]
        [TestCase("3", "0")]
        public void Operator_Greater_String_and_String(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsTrue(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }
        
        [TestCase("Foo", "Foo")]
        [TestCase("-2", "-2")]
        public void Operator_Greater_String_and_String_of_Identical_Values(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsFalse(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }

        [TestCase(3, 2.2)]
        [TestCase(3, 0)]
        [TestCase(0, -2.2)]
        [TestCase(-2, -2.2)]
        public void Operator_Greater_Integer_and_Double(int value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsTrue(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }
        
        [TestCase(0, 0.0)]
        [TestCase(2, 2.0)]
        [TestCase(-2, -2.0)]
        public void Operator_Greater_Integer_and_Double_of_Identical_Values(int value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsFalse(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }


        [TestCase("2", 1)]
        [TestCase("2", 0)]
        [TestCase("0", -3)]
        [TestCase("-2", -3)]
        public void Operator_Greater_String_and_Integer(string value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsTrue(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }
        
        [TestCase("0", 0)]
        [TestCase("2", 2)]
        [TestCase("-2", -2)]
        public void Operator_Greater_String_and_Integer_of_Identical_Values(string value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsFalse(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }

        [TestCase("2.3", 2.2)]
        [TestCase("2.3", 0)]
        [TestCase("0", -2.2)]
        [TestCase("-2.1", -2.2)]
        public void Operator_Greater_String_and_Double(string value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsTrue(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }
        
        [TestCase("0", 0.0)]
        [TestCase("2.2", 2.2)]
        [TestCase("-2.4", -2.4)]
        public void Operator_Greater_String_and_Double_of_Identical_Values(string value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 > operand2);

            Assert.IsFalse(result);

            result = (operand2 > operand1);

            Assert.IsFalse(result);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Lesser_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Lesser_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsTrue(result);
        }

        [TestCase(3, 2)]
        [TestCase(3, 0)]
        [TestCase(-1, -2)]
        [TestCase(0, -2)]
        public void Operator_Lesser_Integer_and_Integer(int value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsTrue(result);
        }

        [TestCase(2, 2)]
        [TestCase(0, 0)]
        [TestCase(-2, -2)]
        public void Operator_Lesser_Integer_and_Integer_of_Identical_Values(int value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsFalse(result);
        }

        [TestCase(3.2, 2.2)]
        [TestCase(3.2, 0.0)]
        [TestCase(-2.4, -2.5)]
        [TestCase(0.0, -1.1)]
        public void Operator_Lesser_Double_and_Double(double value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsTrue(result);
        }

        [TestCase(2.2, 2.2)]
        [TestCase(0.0, 0.0)]
        [TestCase(-2.4, -2.4)]
        public void Operator_Lesser_Double_and_Double_of_Identical_Values(double value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsFalse(result);
        }

        [TestCase("Foo", "Bar")]
        [TestCase("Fo", "Bara")]
        [TestCase("-2", "-3")]
        [TestCase("0", "-3")]
        [TestCase("3", "2")]
        [TestCase("3", "0")]
        public void Operator_Lesser_String_and_String(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsTrue(result);
        }

        [TestCase("Foo", "Foo")]
        [TestCase("-2", "-2")]
        public void Operator_Lesser_String_and_String_of_Identical_Values(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsFalse(result);
        }

        [TestCase(3, 2.2)]
        [TestCase(3, 0)]
        [TestCase(0, -2.2)]
        [TestCase(-2, -2.2)]
        public void Operator_Lesser_Integer_and_Double(int value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsTrue(result);
        }

        [TestCase(0, 0.0)]
        [TestCase(2, 2.0)]
        [TestCase(-2, -2.0)]
        public void Operator_Lesser_Integer_and_Double_of_Identical_Values(int value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsFalse(result);
        }


        [TestCase("2", 1)]
        [TestCase("2", 0)]
        [TestCase("0", -3)]
        [TestCase("-2", -3)]
        public void Operator_Lesser_String_and_Integer(string value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsTrue(result);
        }

        [TestCase("0", 0)]
        [TestCase("2", 2)]
        [TestCase("-2", -2)]
        public void Operator_Lesser_String_and_Integer_of_Identical_Values(string value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsFalse(result);
        }

        [TestCase("2.3", 2.2)]
        [TestCase("2.3", 0)]
        [TestCase("0", -2.2)]
        [TestCase("-2.1", -2.2)]
        public void Operator_Lesser_String_and_Double(string value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsTrue(result);
        }

        [TestCase("0", 0.0)]
        [TestCase("2.2", 2.2)]
        [TestCase("-2.4", -2.4)]
        public void Operator_Lesser_String_and_Double_of_Identical_Values(string value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 < operand2);

            Assert.IsFalse(result);

            result = (operand2 < operand1);

            Assert.IsFalse(result);
        }
        
        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Greater_or_Equal_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Greater_or_Equal_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsFalse(result);
        }

        [TestCase(3, 2)]
        [TestCase(3, 0)]
        [TestCase(-1, -2)]
        [TestCase(0, -2)]
        public void Operator_Greater_or_Equal_Integer_and_Integer(int value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsFalse(result);
        }

        [TestCase(2, 2)]
        [TestCase(0, 0)]
        [TestCase(-2, -2)]
        public void Operator_Greater_or_Equal_Integer_and_Integer_of_Identical_Values(int value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsTrue(result);
        }

        [TestCase(3.2, 2.2)]
        [TestCase(3.2, 0.0)]
        [TestCase(-2.4, -2.5)]
        [TestCase(0.0, -1.1)]
        public void Operator_Greater_or_Equal_Double_and_Double(double value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsFalse(result);
        }

        [TestCase(2.2, 2.2)]
        [TestCase(0.0, 0.0)]
        [TestCase(-2.4, -2.4)]
        public void Operator_Greater_or_Equal_Double_and_Double_of_Identical_Values(double value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsTrue(result);
        }

        [TestCase("Foo", "Bar")]
        [TestCase("Fo", "Bara")]
        [TestCase("-2", "-3")]
        [TestCase("0", "-3")]
        [TestCase("3", "2")]
        [TestCase("3", "0")]
        public void Operator_Greater_or_Equal_String_and_String(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsFalse(result);
        }

        [TestCase("Foo", "Foo")]
        [TestCase("-2", "-2")]
        public void Operator_Greater_or_Equal_String_and_String_of_Identical_Values(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsTrue(result);
        }

        [TestCase(3, 2.2)]
        [TestCase(3, 0)]
        [TestCase(0, -2.2)]
        [TestCase(-2, -2.2)]
        public void Operator_Greater_or_Equal_Integer_and_Double(int value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsFalse(result);
        }

        [TestCase(0, 0.0)]
        [TestCase(2, 2.0)]
        [TestCase(-2, -2.0)]
        public void Operator_Greater_or_Equal_Integer_and_Double_of_Identical_Values(int value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsTrue(result);
        }


        [TestCase("2", 1)]
        [TestCase("2", 0)]
        [TestCase("0", -3)]
        [TestCase("-2", -3)]
        public void Operator_Greater_or_Equal_String_and_Integer(string value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsFalse(result);
        }

        [TestCase("0", 0)]
        [TestCase("2", 2)]
        [TestCase("-2", -2)]
        public void Operator_Greater_or_Equal_String_and_Integer_of_Identical_Values(string value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsTrue(result);
        }

        [TestCase("2.3", 2.2)]
        [TestCase("2.3", 0)]
        [TestCase("0", -2.2)]
        [TestCase("-2.1", -2.2)]
        public void Operator_Greater_or_Equal_String_and_Double(string value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsFalse(result);
        }

        [TestCase("0", 0.0)]
        [TestCase("2.2", 2.2)]
        [TestCase("-2.4", -2.4)]
        public void Operator_Greater_or_Equal_String_and_Double_of_Identical_Values(string value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 >= operand2);

            Assert.IsTrue(result);

            result = (operand2 >= operand1);

            Assert.IsTrue(result);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        [TestCase(null, "")]
        [TestCase("", null)]
        public void Operator_Lesser_or_Equal_Null_Or_Empty(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsTrue(result);
        }

        [TestCase(2, "")]
        [TestCase(2, null)]
        public void Operator_Lesser_or_Equal_Partial_Null_Or_Empty(int value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsFalse(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase(3, 2)]
        [TestCase(3, 0)]
        [TestCase(-1, -2)]
        [TestCase(0, -2)]
        public void Operator_Lesser_or_Equal_Integer_and_Integer(int value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsFalse(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase(2, 2)]
        [TestCase(0, 0)]
        [TestCase(-2, -2)]
        public void Operator_Lesser_or_Equal_Integer_and_Integer_of_Identical_Values(int value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsTrue(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase(3.2, 2.2)]
        [TestCase(3.2, 0.0)]
        [TestCase(-2.4, -2.5)]
        [TestCase(0.0, -1.1)]
        public void Operator_Lesser_or_Equal_Double_and_Double(double value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsFalse(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase(2.2, 2.2)]
        [TestCase(0.0, 0.0)]
        [TestCase(-2.4, -2.4)]
        public void Operator_Lesser_or_Equal_Double_and_Double_of_Identical_Values(double value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsTrue(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase("Foo", "Bar")]
        [TestCase("Fo", "Bara")]
        [TestCase("-2", "-3")]
        [TestCase("0", "-3")]
        [TestCase("3", "2")]
        [TestCase("3", "0")]
        public void Operator_Lesser_or_Equal_String_and_String(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsFalse(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase("Foo", "Foo")]
        [TestCase("-2", "-2")]
        public void Operator_Lesser_or_Equal_String_and_String_of_Identical_Values(string value1, string value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsTrue(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase(3, 2.2)]
        [TestCase(3, 0)]
        [TestCase(0, -2.2)]
        [TestCase(-2, -2.2)]
        public void Operator_Lesser_or_Equal_Integer_and_Double(int value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsFalse(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase(0, 0.0)]
        [TestCase(2, 2.0)]
        [TestCase(-2, -2.0)]
        public void Operator_Lesser_or_Equal_Integer_and_Double_of_Identical_Values(int value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsTrue(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }


        [TestCase("2", 1)]
        [TestCase("2", 0)]
        [TestCase("0", -3)]
        [TestCase("-2", -3)]
        public void Operator_Lesser_or_Equal_String_and_Integer(string value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsFalse(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase("0", 0)]
        [TestCase("2", 2)]
        [TestCase("-2", -2)]
        public void Operator_Lesser_or_Equal_String_and_Integer_of_Identical_Values(string value1, int value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsTrue(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase("2.3", 2.2)]
        [TestCase("2.3", 0)]
        [TestCase("0", -2.2)]
        [TestCase("-2.1", -2.2)]
        public void Operator_Lesser_or_Equal_String_and_Double(string value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsFalse(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }

        [TestCase("0", 0.0)]
        [TestCase("2.2", 2.2)]
        [TestCase("-2.4", -2.4)]
        public void Operator_Lesser_or_Equal_String_and_Double_of_Identical_Values(string value1, double value2)
        {
            SymbolicUnit operand1 = new SymbolicUnit(value1);
            SymbolicUnit operand2 = new SymbolicUnit(value2);

            bool result = (operand1 <= operand2);

            Assert.IsTrue(result);

            result = (operand2 <= operand1);

            Assert.IsTrue(result);
        }
    }
}
