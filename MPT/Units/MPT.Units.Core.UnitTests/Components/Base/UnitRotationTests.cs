using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Base
{
    [TestFixture]
    public class UnitRotationTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitRotation unitRotation = new cUnitRotation();

            Assert.That(cUnitRotation.UnitDefault, Is.EqualTo(cUnitRotation.eUnit.Radian));
            Assert.IsTrue(cUnitRotation.UnitsList.Contains(""));
            Assert.IsTrue(cUnitRotation.UnitsList.Contains("rad"));
            Assert.IsTrue(cUnitRotation.UnitsList.Contains("deg"));
            Assert.IsTrue(cUnitRotation.UnitsList.Contains("cyc"));
            Assert.IsTrue(cUnitRotation.UnitsList.Contains("10^3 cyc"));
            Assert.IsTrue(cUnitRotation.UnitsList.Contains("10^6 cyc"));
            Assert.IsTrue(cUnitRotation.UnitsList.Contains("10^9 cyc"));
            Assert.That(unitRotation.Unit, Is.EqualTo(cUnitRotation.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitRotation unitRotation = new cUnitRotation();
            cUnitRotation.eUnit defaultUnit = cUnitRotation.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitRotation.eUnit.Radian));
            unitRotation.Unit = cUnitRotation.eUnit.Degree;
            Assert.That(unitRotation.Unit, Is.EqualTo(cUnitRotation.eUnit.Degree));

            unitRotation.SetToDefault();
            Assert.That(unitRotation.Unit, Is.EqualTo(cUnitRotation.eUnit.Radian));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitRotation.UnitsList.Count, Is.EqualTo(7));

            string invalidUnits = "invalid";
            cUnitRotation.UnitsList.Add(invalidUnits);
            Assert.That(cUnitRotation.UnitsList.Count, Is.EqualTo(7));
        }

        [TestCase(null, ExpectedResult = (double)cUnitRotation.eUnit.None)]
        [TestCase("", ExpectedResult = (double)cUnitRotation.eUnit.None)]
        [TestCase("FooBar", ExpectedResult = (double)cUnitRotation.eUnit.None)] // Out of range
        [TestCase("rad", ExpectedResult = (double)cUnitRotation.eUnit.Radian)]
        public double ToEnum_Converts_Unit_Name_To_Enum(string name)
        {
            return (double)cUnitRotation.ToEnum(name);
        }

        [TestCase(1, null, null, 0, 0.1)]
        [TestCase(1, "", "", 0, 0.1)]
        [TestCase(1, "Foo", "", 0, 0.1)]
        [TestCase(1, "Foo", "Bar", 0, 0.1)]
        [TestCase(1, "deg", "", 0.01745, 0.00001)]
        [TestCase(1, "deg", "rad", 0.01745, 0.00001)]
        [TestCase(1, "cyc", "kN", 6.2832, 0.0001)]
        public void Convert_As_String_Converts(
            double fromUnitValue,
            string fromUnitType,
            string toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitRotation.Convert(fromUnitValue, fromUnitType, toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitRotation.eUnit.None, cUnitRotation.eUnit.None, ExpectedResult = 0)]
        [TestCase(1, cUnitRotation.eUnit.None, cUnitRotation.eUnit.Radian, ExpectedResult = 0)]
        [TestCase(1, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.None, ExpectedResult = 1)] // Ignoring conversion to None
        [TestCase(1, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.Radian, ExpectedResult = 1)]
        public double Convert(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType)
        {
            return cUnitRotation.Convert(fromUnitValue, (cUnitRotation.eUnit)fromUnitType, (cUnitRotation.eUnit)toUnitType);
        }

        // Convert to default: radians
        [TestCase(1, cUnitRotation.eUnit.None, 0, 0.0001)]
        [TestCase(1, cUnitRotation.eUnit.Radian, 1, 0.0001)]
        [TestCase(1, cUnitRotation.eUnit.Degree, 0.01745, 0.0001)]
        [TestCase(1, cUnitRotation.eUnit.Cycle, 6.2832, 0.0001)]
        [TestCase(1, cUnitRotation.eUnit.KiloCycle, 6283.1853, 0.0001)]
        [TestCase(1, cUnitRotation.eUnit.MegaCycle, 6283185.3071, 0.0001)]
        [TestCase(1, cUnitRotation.eUnit.GigaCycle, 6283185307.1796, 0.0001)]
        [TestCase(1, 100, 0)] // Out of range
        public void Convert_To_Base(
            double fromUnitValue,
            int fromUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitRotation.Convert(fromUnitValue, (cUnitRotation.eUnit)fromUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitRotation.eUnit.None, cUnitRotation.eUnit.Cycle, 0, 0.0001)]
        [TestCase(1, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.Degree, 57.29578, 0.00001)]
        [TestCase(1, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.Cycle, 0.15915, 0.00001)]
        [TestCase(1, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.KiloCycle, 0.00015915, 0.00000001)]
        [TestCase(1, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.MegaCycle, 0.00000015915, 0.00000000001)]
        [TestCase(1, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.GigaCycle, 0.00000000015915, 0.00000000000001)]
        [TestCase(1, cUnitRotation.eUnit.Radian, 100, 0)] // Out of range
        public void Convert_From_Base(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitRotation.Convert(fromUnitValue, (cUnitRotation.eUnit)fromUnitType, (cUnitRotation.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(12, cUnitRotation.eUnit.None, cUnitRotation.eUnit.Cycle, 0, 0.0001)]
        [TestCase(12, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.Degree, 687.54935, 0.0001)]
        [TestCase(12, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.Cycle, 1.90985, 0.0001)]
        [TestCase(12, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.KiloCycle, 0.00190985, 0.0000001)]
        [TestCase(12, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.MegaCycle, 0.00000190985, 0.0000000001)]
        [TestCase(12, cUnitRotation.eUnit.Radian, cUnitRotation.eUnit.GigaCycle, 0.00000000190985, 0.0000000000001)]
        [TestCase(12, cUnitRotation.eUnit.Radian, 100, 0, 0.0001)] // Out of range
        public void Convert_From_Base_And_Non_Unit_Value(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitRotation.Convert(fromUnitValue, (cUnitRotation.eUnit)fromUnitType, (cUnitRotation.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }
    }
}
