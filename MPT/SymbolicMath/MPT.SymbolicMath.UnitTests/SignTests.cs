using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{
    [TestFixture]
    public class SignTests
    {
        [Test]
        public void Initialize()
        {
            Sign sign = new Sign();

            Assert.IsTrue(sign.IsPositive());
            Assert.IsFalse(sign.IsNegative());
            Assert.That(sign.Label(), Is.EqualTo(string.Empty));
        }

        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase("Foo", true)]
        [TestCase("-Foo", false)]
        [TestCase("5", true)]
        [TestCase("-5", false)]
        public void Initialize_String(string value, bool isPositive)
        {
            Sign sign = new Sign(value);

            string expectedLabel = isPositive ? string.Empty : "-";

            Assert.That(sign.IsPositive(), Is.EqualTo(isPositive));
            Assert.That(sign.IsNegative(), Is.Not.EqualTo(isPositive));
            Assert.That(sign.Label(), Is.EqualTo(expectedLabel));
        }

        [TestCase(5, true)]
        [TestCase(-5, false)]
        [TestCase(123, true)]
        [TestCase(-123, false)]
        public void Initialize_Integer(int value, bool isPositive)
        {
            Sign sign = new Sign(value);

            string expectedLabel = isPositive ? string.Empty : "-";

            Assert.That(sign.IsPositive(), Is.EqualTo(isPositive));
            Assert.That(sign.IsNegative(), Is.Not.EqualTo(isPositive));
            Assert.That(sign.Label(), Is.EqualTo(expectedLabel));
        }

        [TestCase(5.23, true)]
        [TestCase(-5.23, false)]
        [TestCase(123.23, true)]
        [TestCase(-123.23, false)]
        public void Initialize_Float(double value, bool isPositive)
        {
            Sign sign = new Sign(value);

            string expectedLabel = isPositive ? string.Empty : "-";

            Assert.That(sign.IsPositive(), Is.EqualTo(isPositive));
            Assert.That(sign.IsNegative(), Is.Not.EqualTo(isPositive));
            Assert.That(sign.Label(), Is.EqualTo(expectedLabel));
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("Foo", false)]
        [TestCase("-Foo", true)]
        [TestCase("5", false)]
        [TestCase("-5", true)]
        public void FlipSign(string value, bool isPositive)
        {
            Sign sign = new Sign(value);

            string expectedLabel = isPositive ? string.Empty : "-";

            Assert.That(sign.IsPositive(), Is.EqualTo(!isPositive));
            Assert.That(sign.IsNegative(), Is.Not.EqualTo(!isPositive));

            sign.FlipSign();

            Assert.That(sign.IsPositive(), Is.EqualTo(isPositive));
            Assert.That(sign.IsNegative(), Is.Not.EqualTo(isPositive));
            Assert.That(sign.Label(), Is.EqualTo(expectedLabel));
        }

        [TestCase(null, ExpectedResult = true)]
        [TestCase("", ExpectedResult = true)]
        [TestCase("Foo", ExpectedResult = true)]
        [TestCase("-Foo", ExpectedResult = false)]
        [TestCase("5", ExpectedResult = true)]
        [TestCase("-5", ExpectedResult = false)]
        public bool Static_IsPositive(string value)
        {
            return Sign.IsPositive(value);
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("Foo", ExpectedResult = false)]
        [TestCase("-Foo", ExpectedResult = true)]
        [TestCase("5", ExpectedResult = false)]
        [TestCase("-5", ExpectedResult = true)]
        public bool Static_IsNegative(string value)
        {
            return Sign.IsNegative(value);
        }

        [TestCase(null, ExpectedResult = null)]
        [TestCase("", ExpectedResult = "")]
        [TestCase("Foo", ExpectedResult = "Foo")]
        [TestCase("-Foo", ExpectedResult = "Foo")]
        [TestCase("5", ExpectedResult = "5")]
        [TestCase("-5", ExpectedResult = "5")]
        [TestCase("123.45", ExpectedResult = "123.45")]
        [TestCase("-123.45", ExpectedResult = "123.45")]
        public string Static_RemoveNegativeSign(string value)
        {
            return Sign.RemoveNegativeSign(value);
        }

        [Test]
        public void Sign_ToString()
        {
            Sign signDefault = new Sign();
            Assert.That(signDefault.ToString(), Is.EqualTo("MPT.SymbolicMath.Sign (1)"));

            Sign signPositive = new Sign("5");
            Assert.That(signPositive.ToString(), Is.EqualTo("MPT.SymbolicMath.Sign (1)"));

            Sign signNegative = new Sign("-5");
            Assert.That(signNegative.ToString(), Is.EqualTo("MPT.SymbolicMath.Sign (-1)"));
        }

        [Test]
        public void Operator_Multiply_String()
        {
            // Default
            Sign sign = new Sign();
            string result = sign * "Foo";
            Assert.That(result, Is.EqualTo("Foo"));

            // Positive
            sign = new Sign(1);
            result = sign * "Foo";
            Assert.That(result, Is.EqualTo("Foo"));

            // Negative
            sign = new Sign(-1);
            result = sign * "Foo";
            Assert.That(result, Is.EqualTo("-Foo"));
        }

        [Test]
        public void Operator_Multiply_Integer()
        {
            // Default
            Sign sign = new Sign(0);
            int result = sign * 2;
            Assert.That(result, Is.EqualTo(2));

            // Positive-Positive
            sign = new Sign(1);
            result = sign * 2;
            Assert.That(result, Is.EqualTo(2));

            // Negative-Positive
            sign = new Sign(-1);
            result = sign * 2;
            Assert.That(result, Is.EqualTo(-2));

            // Negative-Negative
            sign = new Sign(-1);
            result = sign * -2;
            Assert.That(result, Is.EqualTo(2));

            // Positive-Negative
            sign = new Sign(1);
            result = sign * -2;
            Assert.That(result, Is.EqualTo(-2));
        }

        [Test]
        public void Operator_Multiply_Float()
        {
            // Default
            Sign sign = new Sign(0.0);
            double result = sign * 2;
            Assert.That(result, Is.EqualTo(2));

            // Positive-Positive
            sign = new Sign(1.1);
            result = sign * 2.1;
            Assert.That(result, Is.EqualTo(2.1));

            // Negative-Positive
            sign = new Sign(-1.1);
            result = sign * 2.1;
            Assert.That(result, Is.EqualTo(-2.1));

            // Negative-Negative
            sign = new Sign(-1.1);
            result = sign * -2.1;
            Assert.That(result, Is.EqualTo(2.1));

            // Positive-Negative
            sign = new Sign(1.1);
            result = sign * -2.1;
            Assert.That(result, Is.EqualTo(-2.1));
        }

        [Test]
        public void Operator_Multiply_Sign()
        {
            // Default
            Sign sign1 = new Sign(0.0);
            Sign sign2 = new Sign(2);
            Sign sign = sign1 * sign2;
            Assert.IsTrue(sign.IsPositive());

            // Positive-Positive
            sign1 = new Sign(1.1);
            sign2 = new Sign(2.1);
            sign = sign1 * sign2;
            Assert.IsTrue(sign.IsPositive());

            // Negative-Positive
            sign1 = new Sign(-1.1);
            sign2 = new Sign(2.1);
            sign = sign1 * sign2;
            Assert.IsTrue(sign.IsNegative());

            // Negative-Negative
            sign1 = new Sign(-1.1);
            sign2 = new Sign(-2.1);
            sign = sign1 * sign2;
            Assert.IsTrue(sign.IsPositive());

            // Positive-Negative
            sign1 = new Sign(1.1);
            sign2 = new Sign(-2.1);
            sign = sign1 * sign2;
            Assert.IsTrue(sign.IsNegative());
        }

        [TestCase(null, null, true)]
        [TestCase("", "", true)]
        [TestCase("2", "2", true)]
        [TestCase("-2", "-2", true)]
        [TestCase("Foo", "Foo", true)]
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("2", "-2", false)]
        [TestCase("-2", "2", false)]
        [TestCase("Foo", "-Foo", false)]
        [TestCase("-Foo", "Foo", false)]
        public void Equals(string sign1Value, string sign2Value, bool expectedEqual)
        {
            Sign sign1 = new Sign(sign1Value);
            Sign sign2 = new Sign(sign2Value);

            bool result = sign1.Equals(sign2);

            Assert.That(result, Is.EqualTo(expectedEqual));
        }

        [TestCase(null, null, true)]
        [TestCase("", "", true)]
        [TestCase("2", "2", true)]
        [TestCase("-2", "-2", true)]
        [TestCase("Foo", "Foo", true)]
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("2", "-2", false)]
        [TestCase("-2", "2", false)]
        [TestCase("Foo", "-Foo", false)]
        [TestCase("-Foo", "Foo", false)]
        public void Equals_Object(string sign1Value, string sign2Value, bool expectedEqual)
        {
            Sign sign1 = new Sign(sign1Value);
            object sign2 = new Sign(sign2Value);

            bool result = sign1.Equals(sign2);

            Assert.That(result, Is.EqualTo(expectedEqual));
        }

        [TestCase(null, null, true)]
        [TestCase("", "", true)]
        [TestCase("2", "2", true)]
        [TestCase("-2", "-2", true)]
        [TestCase("Foo", "Foo", true)]
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("2", "-2", false)]
        [TestCase("-2", "2", false)]
        [TestCase("Foo", "-Foo", false)]
        [TestCase("-Foo", "Foo", false)]
        public void Operator_Equals(string sign1Value, string sign2Value, bool expectedEqual)
        {
            Sign sign1 = new Sign(sign1Value);
            Sign sign2 = new Sign(sign2Value);

            bool result = sign1 == sign2;

            Assert.That(result, Is.EqualTo(expectedEqual));
        }
        
        [TestCase(null, null, true)]
        [TestCase("", "", true)]
        [TestCase("2", "2", true)]
        [TestCase("-2", "-2", true)]
        [TestCase("Foo", "Foo", true)]
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("2", "-2", false)]
        [TestCase("-2", "2", false)]
        [TestCase("Foo", "-Foo", false)]
        [TestCase("-Foo", "Foo", false)]
        public void Operator_Not_Equals(string sign1Value, string sign2Value, bool expectedEqual)
        {
            Sign sign1 = new Sign(sign1Value);
            Sign sign2 = new Sign(sign2Value);

            bool result = sign1 != sign2;

            Assert.That(result, Is.EqualTo(!expectedEqual));
        }

        [TestCase(null, null, true)]
        [TestCase("", "", true)]
        [TestCase("2", "2", true)]
        [TestCase("-2", "-2", true)]
        [TestCase("Foo", "Foo", true)]
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("2", "-2", false)]
        [TestCase("-2", "2", false)]
        [TestCase("Foo", "-Foo", false)]
        [TestCase("-Foo", "Foo", false)]
        public void GetHashCode_Gets_Hashcode_Of_Members(string sign1Value, string sign2Value, bool expectedEqual)
        {
            Sign sign1 = new Sign(sign1Value);
            Sign sign2 = new Sign(sign2Value);

            int hashCode1 = sign1.GetHashCode();
            int hashCode2 = sign2.GetHashCode();
            bool result = hashCode1 == hashCode2;
            Assert.That(result, Is.EqualTo(expectedEqual));
        }
    }
}
