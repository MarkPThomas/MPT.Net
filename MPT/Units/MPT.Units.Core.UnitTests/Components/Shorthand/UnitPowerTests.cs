using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Shorthand
{
    [TestFixture]
    public class UnitPowerTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitPower unitPower = new cUnitPower();

            Assert.That(cUnitPower.UnitDefault, Is.EqualTo(cUnitPower.eUnit.Watt));
            Assert.IsTrue(cUnitPower.UnitsList.Contains(""));
            Assert.IsTrue(cUnitPower.UnitsList.Contains("W"));
            Assert.IsTrue(cUnitPower.UnitsList.Contains("kW"));
            Assert.IsTrue(cUnitPower.UnitsList.Contains("MW"));
            Assert.IsTrue(cUnitPower.UnitsList.Contains("GW"));
            Assert.That(unitPower.Unit, Is.EqualTo(cUnitPower.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitPower unitPower = new cUnitPower();
            cUnitPower.eUnit defaultUnit = cUnitPower.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitPower.eUnit.Watt));
            unitPower.Unit = cUnitPower.eUnit.KiloWatt;
            Assert.That(unitPower.Unit, Is.EqualTo(cUnitPower.eUnit.KiloWatt));

            unitPower.SetToDefault();
            Assert.That(unitPower.Unit, Is.EqualTo(cUnitPower.eUnit.Watt));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitPower.UnitsList.Count, Is.EqualTo(5));

            string invalidUnits = "invalid";
            cUnitPower.UnitsList.Add(invalidUnits);
            Assert.That(cUnitPower.UnitsList.Count, Is.EqualTo(5));
        }

        [Test]
        public void GetUnits_Of_None_Returns_NA()
        {
            cUnitPower unitPower =
                new cUnitPower { Unit = cUnitPower.eUnit.None };
            cUnits units = unitPower.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("(NA*NA)/NA"));
        }

        [Test]
        public void GetUnits_Returns_Units()
        {
            cUnitPower unitPower = new cUnitPower();
            cUnits units = unitPower.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("(N*m)/sec"));
        }

        [TestCase(null, ExpectedResult = "(NA*NA)/NA")]
        [TestCase(100, ExpectedResult = "(NA*NA)/NA")] // Out of range
        [TestCase(cUnitPower.eUnit.None, ExpectedResult = "(NA*NA)/NA")]
        [TestCase(cUnitPower.eUnit.Watt, ExpectedResult = "(N*m)/sec")]
        [TestCase(cUnitPower.eUnit.KiloWatt, ExpectedResult = "(kN*m)/sec")]
        [TestCase(cUnitPower.eUnit.MegaWatt, ExpectedResult = "(MN*m)/sec")]
        [TestCase(cUnitPower.eUnit.GigaWatt, ExpectedResult = "(GN*m)/sec")]
        public string GetUnits_By_Enum_Name_Returns_Units_Of_Name(double shorthandUnitName)
        {
            cUnits units = cUnitPower.GetUnits((cUnitPower.eUnit)shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, ExpectedResult = "(N*m)/sec")]
        [TestCase("", ExpectedResult = "(N*m)/sec")]
        [TestCase("W", ExpectedResult = "(N*m)/sec")]
        [TestCase("kW", ExpectedResult = "(kN*m)/sec")]
        public string GetUnits_By_String_Name_Returns_Units_Of_Name(string shorthandUnitName)
        {
            cUnits units = cUnitPower.GetUnits(shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, cUnitPower.eUnit.None)]
        [TestCase("", cUnitPower.eUnit.None)]
        [TestCase("W", cUnitPower.eUnit.Watt)]
        [TestCase("kW", cUnitPower.eUnit.KiloWatt)]
        public void GetUnitsEnum(string shorthandUnitName, double expectedResult)
        {
            cUnitPower.eUnit unit = cUnitPower.GetUnitsEnum(shorthandUnitName);

            Assert.That(unit, Is.EqualTo((cUnitPower.eUnit)expectedResult));
        }


        [TestCase(null, cUnitPower.eUnit.None)]
        [TestCase("", cUnitPower.eUnit.None)]
        [TestCase("W", cUnitPower.eUnit.Watt)]
        [TestCase("kW", cUnitPower.eUnit.KiloWatt)]
        public void SetUnitByName_By_String_Name_Sets_Units(string shorthandUnitName, double expectedResult)
        {
            cUnitPower unitPower = new cUnitPower();
            Assert.That(unitPower.Unit, Is.EqualTo(cUnitPower.eUnit.Watt));

            // Method Under Test
            unitPower.SetUnitByName(shorthandUnitName);
            Assert.That(unitPower.Unit, Is.EqualTo((cUnitPower.eUnit)expectedResult));
        }
    }
}
