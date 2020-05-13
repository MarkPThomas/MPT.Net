using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Shorthand
{
    [TestFixture]
    public class UnitSpeedTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitSpeed unitSpeed = new cUnitSpeed();

            Assert.That(cUnitSpeed.UnitDefault, Is.EqualTo(cUnitSpeed.eUnit.FeetPerSecond));
            Assert.IsTrue(cUnitSpeed.UnitsList.Contains(""));
            Assert.IsTrue(cUnitSpeed.UnitsList.Contains("fps"));
            Assert.IsTrue(cUnitSpeed.UnitsList.Contains("mph"));
            Assert.IsTrue(cUnitSpeed.UnitsList.Contains("kph"));
            Assert.That(unitSpeed.Unit, Is.EqualTo(cUnitSpeed.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitSpeed unitSpeed = new cUnitSpeed();
            cUnitSpeed.eUnit defaultUnit = cUnitSpeed.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitSpeed.eUnit.FeetPerSecond));
            unitSpeed.Unit = cUnitSpeed.eUnit.MilesPerHour;
            Assert.That(unitSpeed.Unit, Is.EqualTo(cUnitSpeed.eUnit.MilesPerHour));

            unitSpeed.SetToDefault();
            Assert.That(unitSpeed.Unit, Is.EqualTo(cUnitSpeed.eUnit.FeetPerSecond));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitSpeed.UnitsList.Count, Is.EqualTo(4));

            string invalidUnits = "invalid";
            cUnitSpeed.UnitsList.Add(invalidUnits);
            Assert.That(cUnitSpeed.UnitsList.Count, Is.EqualTo(4));
        }

        [Test]
        public void GetUnits_Of_None_Returns_NA()
        {
            cUnitSpeed unitSpeed =
                new cUnitSpeed { Unit = cUnitSpeed.eUnit.None };
            cUnits units = unitSpeed.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("NA/NA"));
        }

        [Test]
        public void GetUnits_Returns_Units()
        {
            cUnitSpeed unitSpeed = new cUnitSpeed();
            cUnits units = unitSpeed.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("ft/sec"));
        }

        [TestCase(null, ExpectedResult = "NA/NA")]
        [TestCase(100, ExpectedResult = "NA/NA")] // Out of range
        [TestCase(cUnitSpeed.eUnit.None, ExpectedResult = "NA/NA")]
        [TestCase(cUnitSpeed.eUnit.FeetPerSecond, ExpectedResult = "ft/sec")]
        [TestCase(cUnitSpeed.eUnit.MilesPerHour, ExpectedResult = "Mile/hr")]
        [TestCase(cUnitSpeed.eUnit.KilometersPerHour, ExpectedResult = "km/hr")]
        public string GetUnits_By_Enum_Name_Returns_Units_Of_Name(double shorthandUnitName)
        {
            cUnits units = cUnitSpeed.GetUnits((cUnitSpeed.eUnit)shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, ExpectedResult = "ft/sec")]
        [TestCase("", ExpectedResult = "ft/sec")]
        [TestCase("fps", ExpectedResult = "ft/sec")]
        [TestCase("mph", ExpectedResult = "Mile/hr")]
        public string GetUnits_By_String_Name_Returns_Units_Of_Name(string shorthandUnitName)
        {
            cUnits units = cUnitSpeed.GetUnits(shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, cUnitSpeed.eUnit.None)]
        [TestCase("", cUnitSpeed.eUnit.None)]
        [TestCase("fps", cUnitSpeed.eUnit.FeetPerSecond)]
        [TestCase("mph", cUnitSpeed.eUnit.MilesPerHour)]
        public void GetUnitsEnum(string shorthandUnitName, double expectedResult)
        {
            cUnitSpeed.eUnit unit = cUnitSpeed.GetUnitsEnum(shorthandUnitName);

            Assert.That(unit, Is.EqualTo((cUnitSpeed.eUnit)expectedResult));
        }


        [TestCase(null, cUnitSpeed.eUnit.None)]
        [TestCase("", cUnitSpeed.eUnit.None)]
        [TestCase("fps", cUnitSpeed.eUnit.FeetPerSecond)]
        [TestCase("mph", cUnitSpeed.eUnit.MilesPerHour)]
        public void SetUnitByName_By_String_Name_Sets_Units(string shorthandUnitName, double expectedResult)
        {
            cUnitSpeed unitSpeed = new cUnitSpeed();
            Assert.That(unitSpeed.Unit, Is.EqualTo(cUnitSpeed.eUnit.FeetPerSecond));

            // Method Under Test
            unitSpeed.SetUnitByName(shorthandUnitName);
            Assert.That(unitSpeed.Unit, Is.EqualTo((cUnitSpeed.eUnit)expectedResult));
        }
    }
}
