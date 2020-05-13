using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Base
{
    [TestFixture]
    public class UnitTemperatureTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitTemperature unitTemperature = new cUnitTemperature();

            Assert.That(cUnitTemperature.UnitDefault, Is.EqualTo(cUnitTemperature.eUnit.Fahrenheit));
            Assert.IsTrue(cUnitTemperature.UnitsList.Contains(""));
            Assert.IsTrue(cUnitTemperature.UnitsList.Contains("F"));
            Assert.IsTrue(cUnitTemperature.UnitsList.Contains("C"));
            Assert.That(unitTemperature.Unit, Is.EqualTo(cUnitTemperature.UnitDefault));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitTemperature.UnitsList.Count, Is.EqualTo(3));

            string invalidUnits = "invalid";
            cUnitTemperature.UnitsList.Add(invalidUnits);
            Assert.That(cUnitTemperature.UnitsList.Count, Is.EqualTo(3));
        }

        [TestCase(null, ExpectedResult = (double)cUnitTemperature.eUnit.None)]
        [TestCase("", ExpectedResult = (double)cUnitTemperature.eUnit.None)]
        [TestCase("FooBar", ExpectedResult = (double)cUnitTemperature.eUnit.None)] // Out of range
        [TestCase("F", ExpectedResult = (double)cUnitTemperature.eUnit.Fahrenheit)]
        public double ToEnum_Converts_Unit_Name_To_Enum(string name)
        {
            return (double)cUnitTemperature.ToEnum(name);
        }

        [TestCase(1, null, null, 0, 0.1)]
        [TestCase(1, "", "", 0, 0.1)]
        [TestCase(1, "Foo", "", 0, 0.1)]
        [TestCase(1, "Foo", "Bar", 0, 0.1)]
        [TestCase(1, "C", "", 33.8, 0.1)]
        [TestCase(1, "C", "F", 33.8, 0.1)]
        [TestCase(1, "F", "C", -17.2222, 0.0001)]
        public void Convert_As_String_Converts(
            double fromUnitValue,
            string fromUnitType,
            string toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitTemperature.Convert(fromUnitValue, fromUnitType, toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitTemperature unitTemperature = new cUnitTemperature();
            cUnitTemperature.eUnit defaultUnit = cUnitTemperature.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitTemperature.eUnit.Fahrenheit));
            unitTemperature.Unit = cUnitTemperature.eUnit.Celcius;
            Assert.That(unitTemperature.Unit, Is.EqualTo(cUnitTemperature.eUnit.Celcius));

            unitTemperature.SetToDefault();
            Assert.That(unitTemperature.Unit, Is.EqualTo(cUnitTemperature.eUnit.Fahrenheit));
        }

        [TestCase(1, cUnitTemperature.eUnit.None, cUnitTemperature.eUnit.None, ExpectedResult = 0)]
        [TestCase(1, cUnitTemperature.eUnit.None, cUnitTemperature.eUnit.Fahrenheit, ExpectedResult = 0)]
        [TestCase(1, cUnitTemperature.eUnit.Fahrenheit, cUnitTemperature.eUnit.None, ExpectedResult = 1)] // Ignoring conversion to None
        [TestCase(1, cUnitTemperature.eUnit.Fahrenheit, cUnitTemperature.eUnit.Fahrenheit, ExpectedResult = 1)]
        public double Convert(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType)
        {
            return cUnitTemperature.Convert(fromUnitValue, (cUnitTemperature.eUnit)fromUnitType, (cUnitTemperature.eUnit)toUnitType);
        }

        // Convert to default: Fahrenheit
        [TestCase(1, cUnitTemperature.eUnit.None, 0, 0.0001)]
        [TestCase(1, cUnitTemperature.eUnit.Fahrenheit, 1, 0.0001)]
        [TestCase(1, cUnitTemperature.eUnit.Celcius, 33.8000, 0.0001)]
        [TestCase(1, 100, 0)] // Out of range
        public void Convert_To_Base(
            double fromUnitValue,
            int fromUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitTemperature.Convert(fromUnitValue, (cUnitTemperature.eUnit)fromUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitTemperature.eUnit.None, cUnitTemperature.eUnit.Celcius, 0, 0.0001)]
        [TestCase(1, cUnitTemperature.eUnit.Fahrenheit, cUnitTemperature.eUnit.Celcius, -17.2222, 0.0001)]
        [TestCase(1, cUnitTemperature.eUnit.Fahrenheit, 100, 0)] // Out of range
        public void Convert_From_Base(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitTemperature.Convert(fromUnitValue, (cUnitTemperature.eUnit)fromUnitType, (cUnitTemperature.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(12, cUnitTemperature.eUnit.None, cUnitTemperature.eUnit.Celcius, 0, 0.0001)]
        [TestCase(12, cUnitTemperature.eUnit.Fahrenheit, cUnitTemperature.eUnit.Celcius, -11.1111, 0.0001)]
        [TestCase(12, cUnitTemperature.eUnit.Fahrenheit, 100, 0, 0.0001)] // Out of range
        public void Convert_From_Base_And_Non_Unit_Value(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitTemperature.Convert(fromUnitValue, (cUnitTemperature.eUnit)fromUnitType, (cUnitTemperature.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }
    }
}
