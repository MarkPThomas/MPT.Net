using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Base
{
    [TestFixture]
    public class UnitLengthTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitLength unitLength = new cUnitLength();

            Assert.That(cUnitLength.UnitDefault, Is.EqualTo(cUnitLength.eUnit.Inch));
            Assert.IsTrue(cUnitLength.UnitsList.Contains(""));
            Assert.IsTrue(cUnitLength.UnitsList.Contains("in"));
            Assert.IsTrue(cUnitLength.UnitsList.Contains("ft"));
            Assert.IsTrue(cUnitLength.UnitsList.Contains("yd"));
            Assert.IsTrue(cUnitLength.UnitsList.Contains("Mile"));
            Assert.IsTrue(cUnitLength.UnitsList.Contains("Micron"));
            Assert.IsTrue(cUnitLength.UnitsList.Contains("mm"));
            Assert.IsTrue(cUnitLength.UnitsList.Contains("cm"));
            Assert.IsTrue(cUnitLength.UnitsList.Contains("m"));
            Assert.IsTrue(cUnitLength.UnitsList.Contains("km"));
            Assert.That(unitLength.Unit, Is.EqualTo(cUnitLength.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitLength unitLength = new cUnitLength();
            cUnitLength.eUnit defaultUnit = cUnitLength.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitLength.eUnit.Inch));
            unitLength.Unit = cUnitLength.eUnit.Meter;
            Assert.That(unitLength.Unit, Is.EqualTo(cUnitLength.eUnit.Meter));

            unitLength.SetToDefault();
            Assert.That(unitLength.Unit, Is.EqualTo(cUnitLength.eUnit.Inch));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitLength.UnitsList.Count, Is.EqualTo(10));

            string invalidUnits = "invalid";
            cUnitLength.UnitsList.Add(invalidUnits);
            Assert.That(cUnitLength.UnitsList.Count, Is.EqualTo(10));
        }

        [TestCase(null, ExpectedResult = (double)cUnitLength.eUnit.None)]
        [TestCase("", ExpectedResult = (double)cUnitLength.eUnit.None)]
        [TestCase("FooBar", ExpectedResult = (double)cUnitLength.eUnit.None)] // Out of range
        [TestCase("ft", ExpectedResult = (double)cUnitLength.eUnit.Foot)]
        public double ToEnum_Converts_Unit_Name_To_Enum(string name)
        {
            return (double)cUnitLength.ToEnum(name);
        }

        [TestCase(1, null, null, 0, 0.1)]
        [TestCase(1, "", "", 0, 0.1)]
        [TestCase(1, "Foo", "", 0, 0.1)]
        [TestCase(1, "Foo", "Bar", 0, 0.1)]
        [TestCase(1, "ft", "", 12, 0.1)]
        [TestCase(1, "ft", "in", 12, 0.1)]
        [TestCase(1, "mile", "ft", 5280, 0.1)]
        public void Convert_As_String_Converts(
            double fromUnitValue,
            string fromUnitType,
            string toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitLength.Convert(fromUnitValue, fromUnitType, toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitLength.eUnit.None, cUnitLength.eUnit.None, ExpectedResult = 0)]
        [TestCase(1, cUnitLength.eUnit.None, cUnitLength.eUnit.Inch, ExpectedResult = 0)]
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.None, ExpectedResult = 1)] // Ignoring conversion to None
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Inch, ExpectedResult = 1)]
        public double Convert(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType)
        {
            return cUnitLength.Convert(fromUnitValue, (cUnitLength.eUnit) fromUnitType, (cUnitLength.eUnit) toUnitType);
        }

        // Convert to default: inches
        [TestCase(1, cUnitLength.eUnit.None, 0, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Inch, 1, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Foot, 12, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Yard, 36, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Mile, 63360, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Micron, 0.00003937, 0.00000001)]
        [TestCase(1, cUnitLength.eUnit.Millimeter, 0.03937, 0.000001)]
        [TestCase(1, cUnitLength.eUnit.Centimeter, 0.3937, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Meter, 39.37, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Kilometer, 39370, 0.1)]
        [TestCase(1, 100, 0)] // Out of range
        public void Convert_To_Base(
            double fromUnitValue,
            int fromUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitLength.Convert(fromUnitValue, (cUnitLength.eUnit)fromUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitLength.eUnit.None, cUnitLength.eUnit.Yard, 0, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Foot, 0.08333, 0.00001)]
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Yard, 0.02777, 0.00001)]
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Mile, 0.000015783, 0.000000001)]
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Micron, 25400, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Millimeter, 25.4, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Centimeter, 2.54, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Meter, 0.0254, 0.0001)]
        [TestCase(1, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Kilometer, 0.0000254, 0.0000001)]
        [TestCase(1, cUnitLength.eUnit.Inch, 100,  0)] // Out of range
        public void Convert_From_Base(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitLength.Convert(fromUnitValue, (cUnitLength.eUnit)fromUnitType, (cUnitLength.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(12, cUnitLength.eUnit.None, cUnitLength.eUnit.Yard, 0, 0.0001)]
        [TestCase(12, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Foot, 1, 0.0001)]
        [TestCase(12, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Yard, 0.3333, 0.0001)]
        [TestCase(12, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Mile, 0.000015783 * 12, 0.0001)]
        [TestCase(12, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Micron, 25400 * 12, 0.0001)]
        [TestCase(12, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Millimeter, 25.4 * 12, 0.0001)]
        [TestCase(12, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Centimeter, 2.54 * 12, 0.0001)]
        [TestCase(12, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Meter, 0.0254 * 12, 0.0001)]
        [TestCase(12, cUnitLength.eUnit.Inch, cUnitLength.eUnit.Kilometer, 0.0000254 * 12, 0.0001)]
        [TestCase(12, cUnitLength.eUnit.Inch, 100, 0, 0.0001)] // Out of range
        public void Convert_From_Base_And_Non_Unit_Value(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType, 
            double expected, 
            double tolerance)
        {
            double result = cUnitLength.Convert(fromUnitValue, (cUnitLength.eUnit)fromUnitType, (cUnitLength.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }
    }
}
