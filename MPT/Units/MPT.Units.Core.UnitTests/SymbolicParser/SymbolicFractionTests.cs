using System;
using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.SymbolicParser
{
    // TODO: Handle integer and double values of 0

    [TestFixture]
    public class SymbolicFractionTests
    {
        //[Test]
        //public void Initialize_With_Null()
        //{
        //    cSymbolicValue value = null;
        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(value);

        //    Assert.That(symbolicFraction.Numerator, Is.EqualTo("1"));
        //    Assert.That(symbolicFraction.Denominator, Is.EqualTo("1"));
        //    Assert.That(symbolicFraction.Value, Is.EqualTo("1"));
        //}

        //[TestCase("", "1", "1")]
        //[TestCase("Foo", "Foo", "Foo")]
        //public void Initialize_Only_Numerator_As_String(string value1, string expectedNumerator, string expectedValue)
        //{
        //    cSymbolicValue symbolicValue1 = new cSymbolicValue(value1);

        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(symbolicValue1);

        //    Assert.That(symbolicFraction.Numerator, Is.EqualTo(expectedNumerator));
        //    Assert.That(symbolicFraction.Denominator, Is.EqualTo("1"));
        //    Assert.That(symbolicFraction.Value, Is.EqualTo(expectedValue));
        //}

        //[TestCase(5, "5", "5")]
        //[TestCase(-5, "5", "-5")]
        //[TestCase(0, "0", "0")]
        //public void Initialize_Only_Numerator_As_Integer(int value1, string expectedNumerator, string expectedValue)
        //{
        //    cSymbolicValue symbolicValue1 = new cSymbolicValue(value1);

        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(symbolicValue1);

        //    Assert.That(symbolicFraction.Numerator, Is.EqualTo(expectedNumerator));
        //    Assert.That(symbolicFraction.Denominator, Is.EqualTo("1"));
        //    Assert.That(symbolicFraction.Value, Is.EqualTo(expectedValue));
        //}

        //[TestCase(5.2, "5.2", "5.2")]
        //[TestCase(-5.2, "5.2", "-5.2")]
        //[TestCase(0.0, "0", "0")]
        //public void Initialize_Only_Numerator_As_Double(double value1, string expectedNumerator, string expectedValue)
        //{
        //    cSymbolicValue symbolicValue1 = new cSymbolicValue(value1);

        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(symbolicValue1);

        //    Assert.That(symbolicFraction.Numerator, Is.EqualTo(expectedNumerator));
        //    Assert.That(symbolicFraction.Denominator, Is.EqualTo("1"));
        //    Assert.That(symbolicFraction.Value, Is.EqualTo(expectedValue));
        //}

        //[TestCase("Foo", "Bar", "Foo", "Bar", "Foo/Bar")]
        //[TestCase("-Foo", "Bar", "Foo", "Bar", "-Foo/Bar")]
        //[TestCase("Foo", "-Bar", "Foo", "Bar", "-Foo/Bar")]
        //[TestCase("Foo", "Foo", "Foo", "Foo", "Foo/Foo")]
        //[TestCase("-Foo", "-Foo", "Foo", "Foo", "Foo/Foo")]
        //[TestCase("-Foo", "-Bar", "Foo", "Bar", "Foo/Bar")]
        //public void Initialize_As_String(string value1, string value2, string expectedNumerator, string expectedDenominator, string expectedValue)
        //{
        //    cSymbolicValue symbolicValue1 = new cSymbolicValue(value1);
        //    cSymbolicValue symbolicValue2 = new cSymbolicValue(value2);

        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(symbolicValue1, symbolicValue2);

        //    Assert.That(symbolicFraction.Numerator, Is.EqualTo(expectedNumerator));
        //    Assert.That(symbolicFraction.Denominator, Is.EqualTo(expectedDenominator));
        //    Assert.That(symbolicFraction.Value, Is.EqualTo(expectedValue));
        //}


        //[TestCase(5, 2, "5", "2", "5/2")]
        //[TestCase(-5, 2, "5", "2", "-5/2")]
        //[TestCase(-5, -2, "5", "2", "5/2")]
        //[TestCase(5, -2, "5", "2", "-5/2")]
        //[TestCase(5, 5, "5", "5", "5/5")]
        //[TestCase(0, 0, "0", "0", "0/0")]
        //public void Initialize_As_Integer(int value1, int value2, string expectedNumerator, string expectedDenominator, string expectedValue)
        //{
        //    cSymbolicValue symbolicValue1 = new cSymbolicValue(value1);
        //    cSymbolicValue symbolicValue2 = new cSymbolicValue(value2);

        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(symbolicValue1, symbolicValue2);

        //    Assert.That(symbolicFraction.Numerator, Is.EqualTo(expectedNumerator));
        //    Assert.That(symbolicFraction.Denominator, Is.EqualTo(expectedDenominator));
        //    Assert.That(symbolicFraction.Value, Is.EqualTo(expectedValue));
        //}

        //[TestCase(5.2, 2.3, "5.2", "2.3", "5.2/2.3")]
        //[TestCase(-5.2, 2.3, "5.2", "2.3", "-5.2/2.3")]
        //[TestCase(5.2, -2.3, "5.2", "2.3", "-5.2/2.3")]
        //[TestCase(-5.2, -2.3, "5.2", "2.3", "5.2/2.3")]
        //[TestCase(5.2, 5.2, "5.2", "5.2", "5.2/5.2")]
        //[TestCase(0.0, 0.0, "0", "0", "0/0")]
        //public void Initialize_As_Float(double value1, double value2, string expectedNumerator, string expectedDenominator, string expectedValue)
        //{
        //    cSymbolicValue symbolicValue1 = new cSymbolicValue(value1);
        //    cSymbolicValue symbolicValue2 = new cSymbolicValue(value2);

        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(symbolicValue1, symbolicValue2);

        //    Assert.That(symbolicFraction.Numerator, Is.EqualTo(expectedNumerator));
        //    Assert.That(symbolicFraction.Denominator, Is.EqualTo(expectedDenominator));
        //    Assert.That(symbolicFraction.Value, Is.EqualTo(expectedValue));
        //}

        //[TestCase("-Foo", "Bar", "Foo", "Bar", "-Foo/Bar")]
        //[TestCase("5", "3", "5", "3", "5/3")]
        //[TestCase("-5.2", "-2.3", "5.2", "2.3", "5.2/2.3")]
        //public void Initialize_From_String(string value1, string value2, string expectedNumerator, string expectedDenominator, string expectedValue)
        //{
        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(value1, value2);

        //    Assert.That(symbolicFraction.Numerator, Is.EqualTo(expectedNumerator));
        //    Assert.That(symbolicFraction.Denominator, Is.EqualTo(expectedDenominator));
        //    Assert.That(symbolicFraction.Value, Is.EqualTo(expectedValue));
        //}

        //[TestCase("", "", ExpectedResult = "1")]
        //[TestCase("Foo", "Bar", ExpectedResult = "Foo/Bar")]
        //[TestCase("Foo", "Foo", ExpectedResult = "Foo/Foo")] // TODO: Improve string handling behavior
        //[TestCase("2", "3", ExpectedResult = "0.666666666666667")]
        //[TestCase("2", "4", ExpectedResult = "0.5")]
        //[TestCase("2.2", "4.4", ExpectedResult = "0.5")]
        //public string ConsolidateAsString(string value1, string value2)
        //{
        //    cSymbolicValue symbolicValue1 = new cSymbolicValue(value1);
        //    cSymbolicValue symbolicValue2 = new cSymbolicValue(value2);

        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(symbolicValue1, symbolicValue2);

        //    return symbolicFraction.ConsolidateAsString();
        //}

        //[TestCase("", "", ExpectedResult = 1)]
        //[TestCase("Foo", "Bar", ExpectedResult = 0)]
        //[TestCase("Foo", "Foo", ExpectedResult = 0)] 
        //[TestCase("2", "3", ExpectedResult = 1)]
        //[TestCase("2", "4", ExpectedResult = 0)]
        //[TestCase("4", "2", ExpectedResult = 2)]
        //[TestCase("4.4", "2.2", ExpectedResult = 2)]
        //public int ConsolidateAsInteger(string value1, string value2)
        //{
        //    cSymbolicValue symbolicValue1 = new cSymbolicValue(value1);
        //    cSymbolicValue symbolicValue2 = new cSymbolicValue(value2);

        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(symbolicValue1, symbolicValue2);

        //    return symbolicFraction.ConsolidateAsInteger();
        //}

        //[TestCase("", "", ExpectedResult = 1.0)]
        //[TestCase("Foo", "Bar", ExpectedResult = 0.0)]
        //[TestCase("Foo", "Foo", ExpectedResult = 0.0)] 
        //[TestCase("3", "2", ExpectedResult = 1.5)]
        //[TestCase("2", "4", ExpectedResult = 0.5)]
        //[TestCase("2.2", "4.4", ExpectedResult = 0.5)]
        //public double ConsolidateAsFloat(string value1, string value2)
        //{
        //    cSymbolicValue symbolicValue1 = new cSymbolicValue(value1);
        //    cSymbolicValue symbolicValue2 = new cSymbolicValue(value2);

        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(symbolicValue1, symbolicValue2);

        //    return symbolicFraction.ConsolidateAsFloat();
        //}

        //[Test]
        //public void Override_ToString()
        //{
        //    cSymbolicFraction symbolicFraction = new cSymbolicFraction(new cSymbolicValue("2"), new cSymbolicValue("5"));
        //    Assert.That(symbolicFraction.ToString(), Is.EqualTo("MPT.Units.Core.cSymbolicFraction (2/5)"));
        //}

        //[Test]
        //public void Operator_Add_Null()
        //{
        //    Assert.Throws<ArgumentNullException>(() =>
        //        {
        //            cSymbolicValue value1 = null;
        //            cSymbolicValue value2 = null;
        //            cSymbolicFraction operand1 = new cSymbolicFraction(value1, value2);

        //            cSymbolicFraction result = operand1 + null;
        //        },
        //    "Operand was null.");

        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        cSymbolicValue value1 = null;
        //        cSymbolicValue value2 = null;
        //        cSymbolicFraction operand2 = new cSymbolicFraction(value1, value2);

        //            cSymbolicFraction result = null + operand2;
        //        },
        //        "Operand was null.");

        //    Assert.Throws<ArgumentNullException>(() =>
        //        {
        //            cSymbolicFraction operand1 = null;
        //            cSymbolicFraction operand2 = null;

        //            cSymbolicFraction result = operand1 + operand2;
        //        },
        //        "Operand was null.");
        //}

        //[Test]
        //public void Operator_Subtract_Null()
        //{
        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        cSymbolicValue value1 = null;
        //        cSymbolicValue value2 = null;
        //        cSymbolicFraction operand1 = new cSymbolicFraction(value1, value2);

        //            cSymbolicFraction result = operand1 - null;
        //        },
        //        "Operand was null.");

        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        cSymbolicValue value1 = null;
        //        cSymbolicValue value2 = null;
        //        cSymbolicFraction operand2 = new cSymbolicFraction(value1, value2);

        //            cSymbolicFraction result = null - operand2;
        //        },
        //        "Operand was null.");

        //    Assert.Throws<ArgumentNullException>(() =>
        //        {
        //            cSymbolicFraction operand1 = null;
        //            cSymbolicFraction operand2 = null;

        //            cSymbolicFraction result = operand1 - operand2;
        //        },
        //        "Operand was null.");
        //}

        //[Test]
        //public void Operator_Multiply_Null()
        //{
        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        cSymbolicValue value1 = null;
        //        cSymbolicValue value2 = null;
        //        cSymbolicFraction operand1 = new cSymbolicFraction(value1, value2);

        //            cSymbolicFraction result = operand1 * null;
        //        },
        //        "Operand was null.");

        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        cSymbolicValue value1 = null;
        //        cSymbolicValue value2 = null;
        //        cSymbolicFraction operand2 = new cSymbolicFraction(value1, value2);

        //            cSymbolicFraction result = null * operand2;
        //        },
        //        "Operand was null.");

        //    Assert.Throws<ArgumentNullException>(() =>
        //        {
        //            cSymbolicFraction operand1 = null;
        //            cSymbolicFraction operand2 = null;

        //            cSymbolicFraction result = operand1 * operand2;
        //        },
        //        "Operand was null.");
        //}

        //[Test]
        //public void Operator_Divide_Null()
        //{
        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        cSymbolicValue value1 = null;
        //        cSymbolicValue value2 = null;
        //        cSymbolicFraction operand1 = new cSymbolicFraction(value1, value2);

        //            cSymbolicFraction result = operand1 / null;
        //        },
        //        "Operand was null.");

        //    Assert.Throws<ArgumentNullException>(() =>
        //    {
        //        cSymbolicValue value1 = null;
        //        cSymbolicValue value2 = null;
        //        cSymbolicFraction operand2 = new cSymbolicFraction(value1, value2);

        //            cSymbolicFraction result = null / operand2;
        //        },
        //        "Operand was null.");

        //    Assert.Throws<ArgumentNullException>(() =>
        //        {
        //            cSymbolicFraction operand1 = null;
        //            cSymbolicFraction operand2 = null;

        //            cSymbolicFraction result = operand1 / operand2;
        //        },
        //        "Operand was null.");
        //}

        //[TestCase("", "", "", "", "2", "2")]
        //[TestCase("Foo", "Bar", "Foo", "Bar", "(Foo+Foo)/Bar", "(Foo+Foo)/Bar")]
        //[TestCase("Foo", "Bar", "Bar", "Foo", "(Foo*Foo+Bar*Bar)/(Bar*Foo)", "(Bar*Bar+Foo*Foo)/(Foo*Bar)")]
        //[TestCase("1", "2", "1", "2", "2/2", "2/2")]
        //[TestCase("2", "3", "1", "2", "7/6", "7/6")]
        //[TestCase("-2", "3", "1", "2", "-1/6", "-1/6")]
        //[TestCase("-2", "3", "-1", "2", "-7/6", "-7/6")]
        //[TestCase("-2", "3", "1", "-2", "-7/6", "-7/6")]
        //[TestCase("2.2", "3.4", "1.2", "2", "8.48/6.8", "8.48/6.8")]
        //public void Operator_Add(string numerator1, string denominator1, string numerator2, string denominator2, string expectedResult1, string expectedResult2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    cSymbolicFraction value = operand1 + operand2;

        //    Assert.That(value.Value, Is.EqualTo(expectedResult1));

        //    value = operand2 + operand1;

        //    Assert.That(value.Value, Is.EqualTo(expectedResult2));
        //}

        //[TestCase("", "", "", "", "0", "0")]
        //[TestCase("Foo", "Bar", "Foo", "Bar", "(Foo-Foo)/Bar", "(Foo-Foo)/Bar")]
        //[TestCase("Foo", "Bar", "Bar", "Foo", "(Foo*Foo-Bar*Bar)/(Bar*Foo)", "(Bar*Bar-Foo*Foo)/(Foo*Bar)")]
        //[TestCase("1", "2", "1", "2", "0/2", "0/2")]
        //[TestCase("2", "3", "1", "2", "1/6", "-1/6")]
        //[TestCase("-2", "3", "1", "2", "-7/6", "7/6")]
        //[TestCase("-2", "3", "-1", "2", "-1/6", "1/6")]
        //[TestCase("-2", "3", "1", "-2", "-1/6", "1/6")]
        //[TestCase("2.2", "3.4", "1.2", "2", "0.32/6.8", "-0.32/6.8")]
        //public void Operator_Subtract(string numerator1, string denominator1, string numerator2, string denominator2, string expectedResult1, string expectedResult2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    cSymbolicFraction value = operand1 - operand2;

        //    Assert.That(value.Value, Is.EqualTo(expectedResult1));

        //    value = operand2 - operand1;

        //    Assert.That(value.Value, Is.EqualTo(expectedResult2));
        //}

        //[TestCase("", "", "", "", "1", "1")]
        //[TestCase("Foo", "Bar", "Foo", "Bar", "(Foo*Foo)/(Bar*Bar)")]
        //[TestCase("Foo", "Bar", "Bar", "Foo", "(Bar*Foo)/(Foo*Bar)")]
        //[TestCase("1", "2", "1", "2", "1/4", "1/4")]
        //[TestCase("2", "3", "1", "2", "2/6", "2/6")]
        //[TestCase("-2", "3", "1", "2", "-2/6", "-2/6")]
        //[TestCase("-2", "3", "-1", "2", "2/6", "2/6")]
        //[TestCase("-2", "3", "1", "-2", "2/6", "2/6")]
        //[TestCase("2.2", "3.4", "1.2", "2", "2.64/6.8", "2.64/6.8")]
        //public void Operator_Multiply(string numerator1, string denominator1, string numerator2, string denominator2, string expectedResult1, string expectedResult2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    cSymbolicFraction value = operand1 * operand2;

        //    Assert.That(value.Value, Is.EqualTo(expectedResult1));

        //    value = operand2 * operand1;

        //    Assert.That(value.Value, Is.EqualTo(expectedResult2));
        //}

        //[TestCase("", "", "", "", "1", "1")]
        //[TestCase("Foo", "Bar", "Foo", "Bar", "(Foo*Bar)/(Bar*Foo)", "(Foo*Bar)/(Bar*Foo)")]
        //[TestCase("Foo", "Bar", "Bar", "Foo", "(Foo*Foo)/(Bar*Bar)", "(Bar*Bar)/(Foo*Foo)")]
        //[TestCase("1", "2", "1", "2", "2/2", "2/2")]
        //[TestCase("2", "3", "1", "2", "4/3", "3/4")]
        //[TestCase("-2", "3", "1", "2", "-4/3", "-3/4")]
        //[TestCase("-2", "3", "-1", "2", "4/3", "3/4")]
        //[TestCase("-2", "3", "1", "-2", "4/3", "3/4")]
        //[TestCase("2.2", "3.4", "1.2", "2", "4.4/4.08", "4.08/4.4")]
        //public void Operator_Divide(string numerator1, string denominator1, string numerator2, string denominator2, string expectedResult1, string expectedResult2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    cSymbolicFraction value = operand1 / operand2;

        //    Assert.That(value.Value, Is.EqualTo(expectedResult1));

        //    value = operand2 / operand1;

        //    Assert.That(value.Value, Is.EqualTo(expectedResult2));
        //}

        //[Test]
        //public void Operator_Equals_with_Null()
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction("1", "2");
        //    cSymbolicFraction operandNull = null;

        //    bool result = (operand1 == operandNull);

        //    Assert.IsFalse(result);

        //    result = (operandNull == operand1);

        //    Assert.IsFalse(result);

        //    result = (operandNull == operandNull);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("", "", "", "", true)]
        //[TestCase("Foo", "Bar", "Foo", "Bar", true)]
        //[TestCase("Foo", "Bar", "Bar", "Foo", false)]
        //[TestCase("1", "2", "1", "2", true)]
        //[TestCase("2", "3", "1", "2", false)]
        //[TestCase("-1", "2", "-1", "2", true)]
        //[TestCase("-1", "2", "1", "2", false)]
        //[TestCase("2.2", "3.4", "2.2", "3.4", true)]
        //[TestCase("2.2", "3.4", "1.2", "2", false)]
        //public void Operator_Equals(string numerator1, string denominator1, string numerator2, string denominator2, bool isEqual)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 == operand2);

        //    Assert.That(result, Is.EqualTo(isEqual));

        //    result = (operand2 == operand1);

        //    Assert.That(result, Is.EqualTo(isEqual));
        //}

        //[Test]
        //public void Operator_Not_Equals_with_Null()
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction("1", "2");
        //    cSymbolicFraction operandNull = null;

        //    bool result = (operand1 != operandNull);

        //    Assert.IsTrue(result);

        //    result = (operandNull != operand1);

        //    Assert.IsTrue(result);

        //    result = (operandNull != operandNull);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("", "", "", "", false)]
        //[TestCase("Foo", "Bar", "Foo", "Bar", false)]
        //[TestCase("Foo", "Bar", "Bar", "Foo", true)]
        //[TestCase("1", "2", "1", "2", false)]
        //[TestCase("2", "3", "1", "2", true)]
        //[TestCase("-1", "2", "-1", "2", false)]
        //[TestCase("-1", "2", "1", "2", true)]
        //[TestCase("2.2", "3.4", "2.2", "3.4", false)]
        //[TestCase("2.2", "3.4", "1.2", "2", true)]
        //public void Operator_Not_Equals(string numerator1, string denominator1, string numerator2, string denominator2, bool isEqual)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 != operand2);

        //    Assert.That(result, Is.EqualTo(isEqual));

        //    result = (operand2 != operand1);

        //    Assert.That(result, Is.EqualTo(isEqual));
        //}

        //[Test]
        //public void Operator_Greater_with_Null()
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction("1", "2");
        //    cSymbolicFraction operandNull = null;

        //    bool result = (operand1 > operandNull);

        //    Assert.IsTrue(result);

        //    result = (operandNull > operand1);

        //    Assert.IsFalse(result);

        //    result = (operandNull > operandNull);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("", "", "", "")]
        //[TestCase("Foo", "Bar", "Foo", "Bar")]
        //[TestCase("1", "2", "1", "2")]
        //[TestCase("-1", "2", "-1", "2")]
        //[TestCase("2.2", "3.4", "2.2", "3.4")]
        //public void Operator_Greater_with_Equal_Values(string numerator1, string denominator1, string numerator2, string denominator2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("C", "D", "A", "B")]
        //[TestCase("3", "4", "1", "2")]
        //[TestCase("1", "2", "-1", "2")]
        //[TestCase("2.2", "3.4", "1.2", "2")]
        //public void Operator_Greater(string numerator1, string denominator1, string numerator2, string denominator2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 > operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 > operand1);

        //    Assert.IsFalse(result);
        //}

        //[Test]
        //public void Operator_Lesser_with_Null()
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction("1", "2");
        //    cSymbolicFraction operandNull = null;

        //    bool result = (operand1 < operandNull);

        //    Assert.IsFalse(result);

        //    result = (operandNull < operand1);

        //    Assert.IsTrue(result);

        //    result = (operandNull < operandNull);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("", "", "", "")]
        //[TestCase("Foo", "Bar", "Foo", "Bar")]
        //[TestCase("1", "2", "1", "2")]
        //[TestCase("-1", "2", "-1", "2")]
        //[TestCase("2.2", "3.4", "2.2", "3.4")]
        //public void Operator_Lesser_with_Equal_Values(string numerator1, string denominator1, string numerator2, string denominator2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsFalse(result);
        //}

        //[TestCase("C", "D", "A", "B")]
        //[TestCase("3", "4", "1", "2")]
        //[TestCase("1", "2", "-1", "2")]
        //[TestCase("2.2", "3.4", "1.2", "2")]
        //public void Operator_Lesser(string numerator1, string denominator1, string numerator2, string denominator2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 < operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 < operand1);

        //    Assert.IsTrue(result);
        //}

        //[Test]
        //public void Operator_Greater_or_Equals_with_Null()
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction("1", "2");
        //    cSymbolicFraction operandNull = null;

        //    bool result = (operand1 >= operandNull);

        //    Assert.IsTrue(result);

        //    result = (operandNull >= operand1);

        //    Assert.IsFalse(result);

        //    result = (operandNull >= operandNull);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("", "", "", "")]
        //[TestCase("Foo", "Bar", "Foo", "Bar")]
        //[TestCase("1", "2", "1", "2")]
        //[TestCase("-1", "2", "-1", "2")]
        //[TestCase("2.2", "3.4", "2.2", "3.4")]
        //public void Operator_Greater_or_Equals_with_Equal_Values(string numerator1, string denominator1, string numerator2, string denominator2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("C", "D", "A", "B")]
        //[TestCase("3", "4", "1", "2")]
        //[TestCase("1", "2", "-1", "2")]
        //[TestCase("2.2", "3.4", "1.2", "2")]
        //public void Operator_Greater_or_Equals(string numerator1, string denominator1, string numerator2, string denominator2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 >= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 >= operand1);

        //    Assert.IsFalse(result);
        //}

        //[Test]
        //public void Operator_Lesser_or_Equals_with_Null()
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction("1", "2");
        //    cSymbolicFraction operandNull = null;

        //    bool result = (operand1 <= operandNull);

        //    Assert.IsFalse(result);

        //    result = (operandNull <= operand1);

        //    Assert.IsTrue(result);

        //    result = (operandNull <= operandNull);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("", "", "", "")]
        //[TestCase("Foo", "Bar", "Foo", "Bar")]
        //[TestCase("1", "2", "1", "2")]
        //[TestCase("-1", "2", "-1", "2")]
        //[TestCase("2.2", "3.4", "2.2", "3.4")]
        //public void Operator_Lesser_or_Equals_with_Equal_Values(string numerator1, string denominator1, string numerator2, string denominator2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsTrue(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}

        //[TestCase("C", "D", "A", "B")]
        //[TestCase("3", "4", "1", "2")]
        //[TestCase("1", "2", "-1", "2")]
        //[TestCase("2.2", "3.4", "1.2", "2")]
        //public void Operator_Lesser_or_Equals(string numerator1, string denominator1, string numerator2, string denominator2)
        //{
        //    cSymbolicFraction operand1 = new cSymbolicFraction(numerator1, denominator1);
        //    cSymbolicFraction operand2 = new cSymbolicFraction(numerator2, denominator2);

        //    bool result = (operand1 <= operand2);

        //    Assert.IsFalse(result);

        //    result = (operand2 <= operand1);

        //    Assert.IsTrue(result);
        //}
    }
}
