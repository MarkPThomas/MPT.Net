using System;
using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.SymbolicParser
{
    [TestFixture]
    public class SymbolicBlockPowerTests
    {
        ////[Test]
        ////public void Initialize_With_Defaults()
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();

        ////    Assert.IsFalse(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsFalse(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(string.Empty));
        ////}

        ////[TestCase(null)]
        ////[TestCase("")]
        ////[TestCase("-1")]
        ////[TestCase("0")]
        ////[TestCase("1")]
        ////[TestCase("5")]
        ////[TestCase("15")]
        ////[TestCase("1.5")]
        ////[TestCase("-15")]
        ////[TestCase("-1.5")]
        ////[TestCase("1/5")]
        ////[TestCase("-1/5")]
        ////public void Initialize_With_New_Power(string newPower)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower(newPower);

        ////    if (newPower == null)
        ////    {
        ////        newPower = string.Empty;
        ////    }
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(newPower));

        ////}

        ////[Test]
        ////public void Initialize_With_New_Power_Throws_Exception_for_NonNumeric()
        ////{
        ////    Assert.Throws<InvalidCastException>(() => Initialize_With_New_Power_for_NonNumeric("Apple"));
        ////}

        ////private void Initialize_With_New_Power_for_NonNumeric(string newPower)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower(newPower);
        ////}

        ////// TODO: Handle parentheses
        ////// TODO: Handle non-numeric
        ////// TODO: Consider FOIL class for numeric values?

        ////[TestCase(null)]
        ////[TestCase("")]
        ////public void ParsePowerNumeratorDenominator_Empty(string newPower)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsFalse(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsFalse(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(string.Empty));
        ////}
        
        ////[Test]
        ////public void ParsePowerNumeratorDenominator_Throws_Exception_for_NonNumeric()
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    Assert.Throws<InvalidCastException>(() => symbolicBlockPower.ParsePowerNumeratorDenominator("Apple"));
        ////}

        ////[TestCase("-7", -7)]
        ////[TestCase("-27", -27)]
        ////[TestCase("0", 0)]
        ////[TestCase("5", 5)]
        ////[TestCase("35", 35)]
        ////[TestCase("8/1", 8, 1)]
        ////public void ParsePowerNumeratorDenominator_Integer_Numerator(string newPower, int expectedNumerator)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsFalse(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsFalse(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(newPower));
        ////}

        ////[TestCase("1/3", 1, 3, "1/3")]
        ////[TestCase("3/4", 3, 4, "3/4")]
        ////[TestCase("23/4", 23, 4, "23/4")]
        ////[TestCase("3/24", 3, 24, "3/24")]
        ////[TestCase("33/54", 33, 54, "33/54")]
        ////[TestCase("-3/4", -3, 4, "-3/4")]
        ////[TestCase("-23/4", -23, 4, "-23/4")]
        ////[TestCase("-3/24", -3, 24, "-3/24")]
        ////[TestCase("-33/54", -33, 54, "-33/54")]
        ////[TestCase("3/0", 3, 0, "Infinity")]
        ////public void ParsePowerNumeratorDenominator_Integer_Fraction(string newPower, int expectedNumerator, int expectedDenominator, string expectedString)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsTrue(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsFalse(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(expectedDenominator));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(expectedString));
        ////}

        
        ////[TestCase("-1.5", -1.5)]
        ////[TestCase("0.05", 0.05)]
        ////[TestCase("1.5", 1.5)]
        ////public void ParsePowerNumeratorDenominator_Decimal_Numerator(string newPower, double expectedNumerator)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsFalse(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsTrue(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(newPower));
        ////}

        ////[TestCase("1/1.5", 1, 1.5, "0.6666")]
        ////[TestCase("1.7/1.6", 1.7, 1.6, "1.0625")]
        ////[TestCase("1.0/1.6", 1, 1.6, "0.625")]
        ////[TestCase("3.0/4.0", 3, 4, "0.75")]
        ////[TestCase("3.5/0.0", 3.5, 0, "Infinity")]
        ////[TestCase("3/0.0", 3, 0, "Infinity")]
        ////[TestCase("3.5/0", 3.5, 0, "Infinity")]
        ////public void ParsePowerNumeratorDenominator_Decimal_Fraction(string newPower, double expectedNumerator, double expectedDenominator, string expectedString)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsTrue(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsTrue(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(expectedDenominator));
        ////    Assert.That(symbolicBlockPower.Power.Substring(0, expectedString.Length), Is.EqualTo(expectedString));
        ////}

        ////[TestCase("2/4/5", 2, 20, "2/20")]
        ////public void ParsePowerNumeratorDenominator_Integer_Multiple_Fraction(string newPower, int expectedNumerator, int expectedDenominator, string expectedString)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsTrue(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsFalse(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(expectedDenominator));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(expectedString));
        ////}

        ////[TestCase("1*2*3", 6, "6")]
        ////[TestCase("1*2*3/1", 6, "6")]
        ////[TestCase("2*4*5", 40, "40")]
        ////public void ParsePowerNumeratorDenominator_Integer_Multipliers(string newPower, int expectedNumerator, string expectedString)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsFalse(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsFalse(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(expectedString));
        ////}

        ////[TestCase("1*2*3/4", 6, 4, "6/4")]
        ////[TestCase("3/2*2*4", 24, 2, "24/2")]
        ////public void ParsePowerNumeratorDenominator_Integer_Multipliers_Fraction(string newPower, int expectedNumerator, int expectedDenominator, string expectedString)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsTrue(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsFalse(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(expectedDenominator));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(expectedString));
        ////}

        ////[TestCase("2.2/4.4/5.2", 2.2, 22.88, "0.09615")]
        ////public void ParsePowerNumeratorDenominator_Double_Multiple_Fraction(string newPower, double expectedNumerator, double expectedDenominator, string expectedString)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsTrue(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsTrue(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(expectedDenominator).Within(0.01));
        ////    Assert.That(symbolicBlockPower.Power.Substring(0, expectedString.Length), Is.EqualTo(expectedString));
        ////}

        ////[TestCase("1*2.2*3.3", 7.26, "7.26")]
        ////[TestCase("1.2*2.2*3/1.0", 7.92, "7.92")]
        ////[TestCase("2.3*4*5", 46, "46")]
        ////public void ParsePowerNumeratorDenominator_Double_Multipliers(string newPower, double expectedNumerator, string expectedString)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsFalse(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsTrue(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.Power, Is.EqualTo(expectedString));
        ////}

        ////[TestCase("1*2.4*3.2/4.2", 7.68, 4.2, "1.8285")]
        ////[TestCase("3/2.1*2*4.2", 25.2, 2.1, "12")]
        ////public void ParsePowerNumeratorDenominator_Double_Multipliers_Fraction(string newPower, double expectedNumerator, double expectedDenominator, string expectedString)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower();
        ////    symbolicBlockPower.ParsePowerNumeratorDenominator(newPower);

        ////    Assert.IsTrue(symbolicBlockPower.IsFractionFormat);
        ////    Assert.IsTrue(symbolicBlockPower.IsDecimalFormat);
        ////    Assert.That(symbolicBlockPower.NumeratorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.NumeratorAsDouble, Is.EqualTo(expectedNumerator));
        ////    Assert.That(symbolicBlockPower.DenominatorAsInteger, Is.EqualTo(1));
        ////    Assert.That(symbolicBlockPower.DenominatorAsDouble, Is.EqualTo(expectedDenominator));
        ////    Assert.That(symbolicBlockPower.Power.Substring(0, expectedString.Length), Is.EqualTo(expectedString));
        ////}

        //[TestCase(null, null, ExpectedResult = "2")]
        //[TestCase("", "", ExpectedResult = "2")]
        //[TestCase("1", "1", ExpectedResult = "2")]
        //[TestCase("2", "3", ExpectedResult = "5")]
        //[TestCase("1/2", "1/2", ExpectedResult = "1")]
        //[TestCase("1/2", "1/3", ExpectedResult = "5/6")]
        //[TestCase("1/2", "2/3", ExpectedResult = "7/6")]
        //[TestCase("2", "1/2", ExpectedResult = "5/2")]
        //[TestCase("0.5", "0.5", ExpectedResult = "1")]
        //[TestCase("0.5", "0.25", ExpectedResult = "0.75")]
        //[TestCase("2", "0.5", ExpectedResult = "2.5")]
        //public string CombinePowersBaseMultiply_Static(string power1, string power2)
        //{
        //    cSymbolicBlockPower symbolicBlockPower1 = new cSymbolicBlockPower(power1);
        //    cSymbolicBlockPower symbolicBlockPower2 = new cSymbolicBlockPower(power2);

        //    cSymbolicBlockPower symbolicBlockPowerMultiply =
        //        cSymbolicBlockPower.CombinePowersBaseMultiply(symbolicBlockPower1, symbolicBlockPower2);

        //    return symbolicBlockPowerMultiply.Power;
        //}

        ////[TestCase(null, null, ExpectedResult = "1")]
        ////public string CombinePowersBaseMultiply(string power1, string power2)
        ////{
        ////    cSymbolicBlockPower symbolicBlockPower1 = new cSymbolicBlockPower(power1);
        ////    cSymbolicBlockPower symbolicBlockPower2 = new cSymbolicBlockPower(power2);

        ////    cSymbolicBlockPower symbolicBlockPowerMultiply =
        ////        symbolicBlockPower1.CombinePowersBaseMultiply(symbolicBlockPower2);

        ////    return symbolicBlockPowerMultiply.Power;
        ////}

        //[TestCase(null, null, ExpectedResult = null)]
        //public string CombinePowersBaseDivide_Static(string power1, string power2)
        //{
        //    cSymbolicBlockPower symbolicBlockPower1 = new cSymbolicBlockPower(power1);
        //    cSymbolicBlockPower symbolicBlockPower2 = new cSymbolicBlockPower(power2);

        //    cSymbolicBlockPower symbolicBlockPowerDivide =
        //        cSymbolicBlockPower.CombinePowersBaseDivide(symbolicBlockPower1, symbolicBlockPower2);

        //    return symbolicBlockPowerDivide.Power;
        //}

        //[TestCase(null, null, ExpectedResult = null)]
        //public string CombinePowersBaseDivide(string power1, string power2)
        //{
        //    cSymbolicBlockPower symbolicBlockPower1 = new cSymbolicBlockPower(power1);
        //    cSymbolicBlockPower symbolicBlockPower2 = new cSymbolicBlockPower(power2);

        //    cSymbolicBlockPower symbolicBlockPowerDivide =
        //        symbolicBlockPower1.CombinePowersBaseDivide(symbolicBlockPower2);

        //    return symbolicBlockPowerDivide.Power;
        //}

        //[TestCase(null, ExpectedResult = false)]
        //[TestCase("", ExpectedResult = false)]
        //[TestCase("-2", ExpectedResult = true)]
        //[TestCase("2", ExpectedResult = false)]
        //[TestCase("-FooBar", ExpectedResult = true)]
        //[TestCase("FooBar", ExpectedResult = false)]
        //public bool IsPowerDenominator_Static_Returns_Whether_Provided_Power_Is_Denominator(string blockPower)
        //{
        //   return cSymbolicBlockPower.IsPowerDenominator(blockPower);
        //}

        //[TestCase(null, false)]
        //[TestCase("", false)]
        //[TestCase("-2", true)]
        //[TestCase("2", false)]
        //public void IsPowerDenominator_Returns_Whether_Class_Power_Is_Denominator(string blockPower, bool expectedResult)
        //{
        //    cSymbolicBlockPower symbolicBlockPower = new cSymbolicBlockPower(blockPower);

        //    Assert.That(symbolicBlockPower.IsPowerDenominator(), Is.EqualTo(expectedResult));
        //}
    }
}
