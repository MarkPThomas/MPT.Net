using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Base
{
    [TestFixture]
    public class UnitMassTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitMass unitMass = new cUnitMass();

            Assert.That(cUnitMass.UnitDefault, Is.EqualTo(cUnitMass.eUnit.PoundMass));
            Assert.IsTrue(cUnitMass.UnitsList.Contains(""));
            Assert.IsTrue(cUnitMass.UnitsList.Contains("kg"));
            Assert.IsTrue(cUnitMass.UnitsList.Contains("lbm"));
            Assert.That(unitMass.Unit, Is.EqualTo(cUnitMass.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitMass unitMass = new cUnitMass();
            cUnitMass.eUnit defaultUnit = cUnitMass.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitMass.eUnit.PoundMass));
            unitMass.Unit = cUnitMass.eUnit.Kilogram;
            Assert.That(unitMass.Unit, Is.EqualTo(cUnitMass.eUnit.Kilogram));

            unitMass.SetToDefault();
            Assert.That(unitMass.Unit, Is.EqualTo(cUnitMass.eUnit.PoundMass));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitMass.UnitsList.Count, Is.EqualTo(3));

            string invalidUnits = "invalid";
            cUnitMass.UnitsList.Add(invalidUnits);
            Assert.That(cUnitMass.UnitsList.Count, Is.EqualTo(3));
        }

        [TestCase(null, ExpectedResult = (double)cUnitMass.eUnit.None)]
        [TestCase("", ExpectedResult = (double)cUnitMass.eUnit.None)]
        [TestCase("FooBar", ExpectedResult = (double)cUnitMass.eUnit.None)] // Out of range
        [TestCase("kg", ExpectedResult = (double)cUnitMass.eUnit.Kilogram)]
        public double ToEnum_Converts_Unit_Name_To_Enum(string name)
        {
            return (double)cUnitMass.ToEnum(name);
        }

        [TestCase(1, null, null, 0, 0.1)]
        [TestCase(1, "", "", 0, 0.1)]
        [TestCase(1, "Foo", "", 0, 0.1)]
        [TestCase(1, "Foo", "Bar", 0, 0.1)]
        [TestCase(1, "kg", "", 2.2046, 0.0001)]
        [TestCase(1, "kg", "lbm", 2.2046, 0.0001)]
        [TestCase(1, "lbm", "kg", 0.4536, 0.0001)]
        public void Convert_As_String_Converts(
            double fromUnitValue,
            string fromUnitType,
            string toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitMass.Convert(fromUnitValue, fromUnitType, toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitMass.eUnit.None, cUnitMass.eUnit.None, ExpectedResult = 0)]
        [TestCase(1, cUnitMass.eUnit.None, cUnitMass.eUnit.PoundMass, ExpectedResult = 0)]
        [TestCase(1, cUnitMass.eUnit.PoundMass, cUnitMass.eUnit.None, ExpectedResult = 1)] // Ignoring conversion to None
        [TestCase(1, cUnitMass.eUnit.PoundMass, cUnitMass.eUnit.PoundMass, ExpectedResult = 1)]
        public double Convert(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType)
        {
            return cUnitMass.Convert(fromUnitValue, (cUnitMass.eUnit)fromUnitType, (cUnitMass.eUnit)toUnitType);
        }

        // Convert to default: poundmasses
        [TestCase(1, cUnitMass.eUnit.None, 0, 0.0001)]
        [TestCase(1, cUnitMass.eUnit.PoundMass, 1, 0.0001)]
        [TestCase(1, cUnitMass.eUnit.Kilogram, 2.2046, 0.0001)]
        [TestCase(1, 100, 0)] // Out of range
        public void Convert_To_Base(
            double fromUnitValue,
            int fromUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitMass.Convert(fromUnitValue, (cUnitMass.eUnit)fromUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitMass.eUnit.None, cUnitMass.eUnit.Kilogram, 0, 0.0001)]
        [TestCase(1, cUnitMass.eUnit.PoundMass, cUnitMass.eUnit.Kilogram, 0.45359, 0.00001)]
        [TestCase(1, cUnitMass.eUnit.PoundMass, 100, 0)] // Out of range
        public void Convert_From_Base(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitMass.Convert(fromUnitValue, (cUnitMass.eUnit)fromUnitType, (cUnitMass.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(12, cUnitMass.eUnit.None, cUnitMass.eUnit.Kilogram, 0, 0.0001)]
        [TestCase(12, cUnitMass.eUnit.PoundMass, cUnitMass.eUnit.Kilogram, 5.4431, 0.0001)]
        [TestCase(12, cUnitMass.eUnit.PoundMass, 100, 0, 0.0001)] // Out of range
        public void Convert_From_Base_And_Non_Unit_Value(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitMass.Convert(fromUnitValue, (cUnitMass.eUnit)fromUnitType, (cUnitMass.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }
    }
}
