using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Base
{
    [TestFixture]
    public class UnitTimeTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitTime unitTime = new cUnitTime();

            Assert.That(cUnitTime.UnitDefault, Is.EqualTo(cUnitTime.eUnit.Second));
            Assert.IsTrue(cUnitTime.UnitsList.Contains(""));
            Assert.IsTrue(cUnitTime.UnitsList.Contains("sec"));
            Assert.IsTrue(cUnitTime.UnitsList.Contains("min"));
            Assert.IsTrue(cUnitTime.UnitsList.Contains("hr"));
            Assert.IsTrue(cUnitTime.UnitsList.Contains("Day"));
            Assert.IsTrue(cUnitTime.UnitsList.Contains("Week"));
            Assert.IsTrue(cUnitTime.UnitsList.Contains("Month"));
            Assert.IsTrue(cUnitTime.UnitsList.Contains("Year"));
            Assert.That(unitTime.Unit, Is.EqualTo(cUnitTime.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitTime unitTime = new cUnitTime();
            cUnitTime.eUnit defaultUnit = cUnitTime.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitTime.eUnit.Second));
            unitTime.Unit = cUnitTime.eUnit.Hour;
            Assert.That(unitTime.Unit, Is.EqualTo(cUnitTime.eUnit.Hour));

            unitTime.SetToDefault();
            Assert.That(unitTime.Unit, Is.EqualTo(cUnitTime.eUnit.Second));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitTime.UnitsList.Count, Is.EqualTo(8));

            string invalidUnits = "invalid";
            cUnitTime.UnitsList.Add(invalidUnits);
            Assert.That(cUnitTime.UnitsList.Count, Is.EqualTo(8));
        }

        [TestCase(null, ExpectedResult = (double)cUnitTime.eUnit.None)]
        [TestCase("", ExpectedResult = (double)cUnitTime.eUnit.None)]
        [TestCase("FooBar", ExpectedResult = (double)cUnitTime.eUnit.None)] // Out of range
        [TestCase("day", ExpectedResult = (double)cUnitTime.eUnit.Day)]
        public double ToEnum_Converts_Unit_Name_To_Enum(string name)
        {
            return (double)cUnitTime.ToEnum(name);
        }

        [TestCase(1, null, null, 0, 0.1)]
        [TestCase(1, "", "", 0, 0.1)]
        [TestCase(1, "Foo", "", 0, 0.1)]
        [TestCase(1, "Foo", "Bar", 0, 0.1)]
        [TestCase(1, "min", "", 60, 0.1)]
        [TestCase(1, "min", "sec", 60, 0.1)]
        [TestCase(1, "hr", "sec", 3600, 0.0001)]
        public void Convert_As_String_Converts(
            double fromUnitValue,
            string fromUnitType,
            string toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitTime.Convert(fromUnitValue, fromUnitType, toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitTime.eUnit.None, cUnitTime.eUnit.None, ExpectedResult = 0)]
        [TestCase(1, cUnitTime.eUnit.None, cUnitTime.eUnit.Second, ExpectedResult = 0)]
        [TestCase(1, cUnitTime.eUnit.Second, cUnitTime.eUnit.None, ExpectedResult = 1)] // Ignoring conversion to None
        [TestCase(1, cUnitTime.eUnit.Second, cUnitTime.eUnit.Second, ExpectedResult = 1)]
        public double Convert(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType)
        {
            return cUnitTime.Convert(fromUnitValue, (cUnitTime.eUnit)fromUnitType, (cUnitTime.eUnit)toUnitType);
        }

        // Convert to default: seconds
        [TestCase(1, cUnitTime.eUnit.None, 0, 0.0001)]
        [TestCase(1, cUnitTime.eUnit.Second, 1, 0.0001)]
        [TestCase(1, cUnitTime.eUnit.Minute, 60, 0.0001)]
        [TestCase(1, cUnitTime.eUnit.Hour, 3600, 0.0001)]
        [TestCase(1, cUnitTime.eUnit.Day, 86400, 0.0001)]
        [TestCase(1, cUnitTime.eUnit.Week, 604800, 0.0001)]
        [TestCase(1, cUnitTime.eUnit.Month, 2629746, 0.0001)]
        [TestCase(1, cUnitTime.eUnit.Year, 31556952, 0.0001)]
        [TestCase(1, 100, 0)] // Out of range
        public void Convert_To_Base(
            double fromUnitValue,
            int fromUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitTime.Convert(fromUnitValue, (cUnitTime.eUnit)fromUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitTime.eUnit.None, cUnitTime.eUnit.Hour, 0, 0.0001)]
        [TestCase(1, cUnitTime.eUnit.Second, cUnitTime.eUnit.Minute, 0.01666, 0.00001)]
        [TestCase(1, cUnitTime.eUnit.Second, cUnitTime.eUnit.Hour, 0.0002777, 0.00001)]
        [TestCase(1, cUnitTime.eUnit.Second, cUnitTime.eUnit.Day, 0.000011574, 0.000000001)]
        [TestCase(1, cUnitTime.eUnit.Second, cUnitTime.eUnit.Week, 0.000001653, 0.000000001)]
        [TestCase(1, cUnitTime.eUnit.Second, cUnitTime.eUnit.Month, 0.00000038026, 0.00000000001)]
        [TestCase(1, cUnitTime.eUnit.Second, cUnitTime.eUnit.Year, 0.0000000316887, 0.0000000000001)]
        [TestCase(1, cUnitTime.eUnit.Second, 100, 0)] // Out of range
        public void Convert_From_Base(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitTime.Convert(fromUnitValue, (cUnitTime.eUnit)fromUnitType, (cUnitTime.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(12, cUnitTime.eUnit.None, cUnitTime.eUnit.Hour, 0, 0.001)]
        [TestCase(12, cUnitTime.eUnit.Second, cUnitTime.eUnit.Minute, 0.2000, 0.0001)]
        [TestCase(12, cUnitTime.eUnit.Second, cUnitTime.eUnit.Hour, 0.00333, 0.00001)]
        [TestCase(12, cUnitTime.eUnit.Second, cUnitTime.eUnit.Day, 0.0001388, 0.0000001)]
        [TestCase(12, cUnitTime.eUnit.Second, cUnitTime.eUnit.Week, 0.00001984, 0.0000001)]
        [TestCase(12, cUnitTime.eUnit.Second, cUnitTime.eUnit.Month, 0.000004563, 0.000000001)]
        [TestCase(12, cUnitTime.eUnit.Second, cUnitTime.eUnit.Year, 0.0000003803, 0.0000000001)]
        [TestCase(12, cUnitTime.eUnit.Second, 100, 0, 0.0001)] // Out of range
        public void Convert_From_Base_And_Non_Unit_Value(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitTime.Convert(fromUnitValue, (cUnitTime.eUnit)fromUnitType, (cUnitTime.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }
    }
}
