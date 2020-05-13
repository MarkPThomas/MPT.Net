using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Derived
{
    [TestFixture]
    public class UnitForceTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitForce unitForce = new cUnitForce();

            Assert.That(cUnitForce.UnitDefault, Is.EqualTo(cUnitForce.eUnit.PoundForce));
            Assert.IsTrue(cUnitForce.UnitsList.Contains(""));
            Assert.IsTrue(cUnitForce.UnitsList.Contains("lb"));
            Assert.IsTrue(cUnitForce.UnitsList.Contains("Kip"));
            Assert.IsTrue(cUnitForce.UnitsList.Contains("N"));
            Assert.IsTrue(cUnitForce.UnitsList.Contains("kN"));
            Assert.IsTrue(cUnitForce.UnitsList.Contains("MN"));
            Assert.IsTrue(cUnitForce.UnitsList.Contains("GN"));
            Assert.IsTrue(cUnitForce.UnitsList.Contains("kgf"));
            Assert.IsTrue(cUnitForce.UnitsList.Contains("tf"));
            Assert.That(unitForce.Unit, Is.EqualTo(cUnitForce.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitForce unitForce = new cUnitForce();
            cUnitForce.eUnit defaultUnit = cUnitForce.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitForce.eUnit.PoundForce));
            unitForce.Unit = cUnitForce.eUnit.KiloNewton;
            Assert.That(unitForce.Unit, Is.EqualTo(cUnitForce.eUnit.KiloNewton));

            unitForce.SetToDefault();
            Assert.That(unitForce.Unit, Is.EqualTo(cUnitForce.eUnit.PoundForce));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitForce.UnitsList.Count, Is.EqualTo(9));

            string invalidUnits = "invalid";
            cUnitForce.UnitsList.Add(invalidUnits);
            Assert.That(cUnitForce.UnitsList.Count, Is.EqualTo(9));
        }

        [TestCase(null, ExpectedResult = (double)cUnitForce.eUnit.None)]
        [TestCase("", ExpectedResult = (double)cUnitForce.eUnit.None)]
        [TestCase("FooBar", ExpectedResult = (double)cUnitForce.eUnit.None)] // Out of range
        [TestCase("kN", ExpectedResult = (double)cUnitForce.eUnit.KiloNewton)]
        public double ToEnum_Converts_Unit_Name_To_Enum(string name)
        {
            return (double) cUnitForce.ToEnum(name);
        }

        [TestCase(1, null, null, 0, 0.1)]
        [TestCase(1, "", "", 0, 0.1)]
        [TestCase(1, "Foo", "", 0, 0.1)]
        [TestCase(1, "Foo", "Bar", 0, 0.1)]
        [TestCase(1, "kip", "", 1000, 0.1)]
        [TestCase(1, "kip", "lb", 1000, 0.1)]
        [TestCase(1, "kip", "kN", 4.4482, 0.0001)]
        public void Convert_As_String_Converts(
            double fromUnitValue,
            string fromUnitType,
            string toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitForce.Convert(fromUnitValue, fromUnitType, toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitForce.eUnit.None, cUnitForce.eUnit.None, ExpectedResult = 0)]
        [TestCase(1, cUnitForce.eUnit.None, cUnitForce.eUnit.PoundForce, ExpectedResult = 0)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.None, ExpectedResult = 1)] // Ignoring conversion to None
        [TestCase(1, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.PoundForce, ExpectedResult = 1)]
        public double Convert(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType)
        {
            return cUnitForce.Convert(fromUnitValue, (cUnitForce.eUnit)fromUnitType, (cUnitForce.eUnit)toUnitType);
        }

        // Convert to default: PoundForce
        [TestCase(1, cUnitForce.eUnit.None, 0, 0.0001)]
        [TestCase(1, cUnitForce.eUnit.Kip, 1000, 0.0001)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, 1, 0.0001)]
        [TestCase(1, cUnitForce.eUnit.Newton, 0.2248, 0.0001)]
        [TestCase(1, cUnitForce.eUnit.KiloNewton, 224.8092, 0.0001)]
        [TestCase(1, cUnitForce.eUnit.MegaNewton, 224809.282, 0.001)]
        [TestCase(1, cUnitForce.eUnit.GigaNewton, 224809282.38, 0.01)]
        [TestCase(1, cUnitForce.eUnit.KilogramForce, 2.2046, 0.0001)]
        [TestCase(1, cUnitForce.eUnit.TonForce, 2204.6226, 0.0001)]
        [TestCase(1, 100, 0)] // Out of range
        public void Convert_To_Base(
            double fromUnitValue,
            int fromUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitForce.Convert(fromUnitValue, (cUnitForce.eUnit)fromUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(1, cUnitForce.eUnit.None, cUnitForce.eUnit.KiloNewton, 0, 0.0001)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.Kip, 0.0010, 0.0001)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.Newton, 4.448, 0.001)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.KiloNewton, 0.004448, 0.000001)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.MegaNewton, 0.000004448, 0.000000001)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.GigaNewton, 0.000000004448, 0.000000000001)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.KilogramForce, 0.45359, 0.00001)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.TonForce, 0.00045359, 0.00000001)]
        [TestCase(1, cUnitForce.eUnit.PoundForce, 100, 0)] // Out of range
        public void Convert_From_Base(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitForce.Convert(fromUnitValue, (cUnitForce.eUnit)fromUnitType, (cUnitForce.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }

        [TestCase(12, cUnitForce.eUnit.None, cUnitForce.eUnit.KiloNewton, 0, 0.0001)]
        [TestCase(12, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.Kip, 0.0120, 0.0001)]
        [TestCase(12, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.Newton, 53.3786, 0.0001)]
        [TestCase(12, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.KiloNewton, 0.0533786, 0.0000001)]
        [TestCase(12, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.MegaNewton, 0.0000533786, 0.0000000001)]
        [TestCase(12, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.GigaNewton, 0.0000000533786, 0.0000000000001)]
        [TestCase(12, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.KilogramForce, 5.4431, 0.0001)]
        [TestCase(12, cUnitForce.eUnit.PoundForce, cUnitForce.eUnit.TonForce, 0.0054431, 0.0000001)]
        [TestCase(12, cUnitForce.eUnit.PoundForce, 100, 0, 0.0001)] // Out of range
        public void Convert_From_Base_And_Non_Unit_Value(
            double fromUnitValue,
            int fromUnitType,
            int toUnitType,
            double expected,
            double tolerance)
        {
            double result = cUnitForce.Convert(fromUnitValue, (cUnitForce.eUnit)fromUnitType, (cUnitForce.eUnit)toUnitType);
            Assert.AreEqual(expected, result, tolerance);
        }
    }
}
