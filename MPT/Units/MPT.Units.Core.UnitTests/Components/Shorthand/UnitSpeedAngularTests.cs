using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Shorthand
{
    [TestFixture]
    public class UnitSpeedAngularTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitSpeedAngular unitSpeedAngular = new cUnitSpeedAngular();

            Assert.That(cUnitSpeedAngular.UnitDefault, Is.EqualTo(cUnitSpeedAngular.eUnit.OneCyclePerSecond));
            Assert.IsTrue(cUnitSpeedAngular.UnitsList.Contains(""));
            Assert.IsTrue(cUnitSpeedAngular.UnitsList.Contains("1/sec"));
            Assert.IsTrue(cUnitSpeedAngular.UnitsList.Contains("s^-1"));
            Assert.IsTrue(cUnitSpeedAngular.UnitsList.Contains("rpm"));
            Assert.IsTrue(cUnitSpeedAngular.UnitsList.Contains("Hz"));
            Assert.IsTrue(cUnitSpeedAngular.UnitsList.Contains("kHz"));
            Assert.IsTrue(cUnitSpeedAngular.UnitsList.Contains("MHz"));
            Assert.IsTrue(cUnitSpeedAngular.UnitsList.Contains("GHz"));
            Assert.That(unitSpeedAngular.Unit, Is.EqualTo(cUnitSpeedAngular.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitSpeedAngular unitSpeedAngular = new cUnitSpeedAngular();
            cUnitSpeedAngular.eUnit defaultUnit = cUnitSpeedAngular.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitSpeedAngular.eUnit.OneCyclePerSecond));
            unitSpeedAngular.Unit = cUnitSpeedAngular.eUnit.Hertz;
            Assert.That(unitSpeedAngular.Unit, Is.EqualTo(cUnitSpeedAngular.eUnit.Hertz));

            unitSpeedAngular.SetToDefault();
            Assert.That(unitSpeedAngular.Unit, Is.EqualTo(cUnitSpeedAngular.eUnit.OneCyclePerSecond));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitSpeedAngular.UnitsList.Count, Is.EqualTo(8));

            string invalidUnits = "invalid";
            cUnitSpeedAngular.UnitsList.Add(invalidUnits);
            Assert.That(cUnitSpeedAngular.UnitsList.Count, Is.EqualTo(8));
        }

        [Test]
        public void GetUnits_Of_None_Returns_NA()
        {
            cUnitSpeedAngular unitSpeedAngular =
                new cUnitSpeedAngular { Unit = cUnitSpeedAngular.eUnit.None };
            cUnits units = unitSpeedAngular.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("NA/NA"));
        }

        [Test]
        public void GetUnits_Returns_Units()
        {
            cUnitSpeedAngular unitSpeedAngular = new cUnitSpeedAngular();
            cUnits units = unitSpeedAngular.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("cyc/sec"));
        }

        [TestCase(null, ExpectedResult = "NA/NA")]
        [TestCase(100, ExpectedResult = "NA/NA")] // Out of range
        [TestCase(cUnitSpeedAngular.eUnit.None, ExpectedResult = "NA/NA")]
        [TestCase(cUnitSpeedAngular.eUnit.OneCyclePerSecond, ExpectedResult = "cyc/sec")]
        [TestCase(cUnitSpeedAngular.eUnit.OneCyclePerSecondAlt, ExpectedResult = "cyc/sec")]
        [TestCase(cUnitSpeedAngular.eUnit.RotationsPerMinute, ExpectedResult = "cyc/min")]
        [TestCase(cUnitSpeedAngular.eUnit.Hertz, ExpectedResult = "cyc/sec")]
        [TestCase(cUnitSpeedAngular.eUnit.KiloHertz, ExpectedResult = "10^3 cyc/sec")]
        [TestCase(cUnitSpeedAngular.eUnit.MegaHertz, ExpectedResult = "10^6 cyc/sec")]
        [TestCase(cUnitSpeedAngular.eUnit.GigaHertz, ExpectedResult = "10^9 cyc/sec")]
        public string GetUnits_By_Enum_Name_Returns_Units_Of_Name(double shorthandUnitName)
        {
            cUnits units = cUnitSpeedAngular.GetUnits((cUnitSpeedAngular.eUnit)shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, ExpectedResult = "cyc/sec")]
        [TestCase("", ExpectedResult = "cyc/sec")]
        [TestCase("1/sec", ExpectedResult = "cyc/sec")]
        [TestCase("Hz", ExpectedResult = "cyc/sec")]
        public string GetUnits_By_String_Name_Returns_Units_Of_Name(string shorthandUnitName)
        {
            cUnits units = cUnitSpeedAngular.GetUnits(shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, cUnitSpeedAngular.eUnit.None)]
        [TestCase("", cUnitSpeedAngular.eUnit.None)]
        [TestCase("1/sec", cUnitSpeedAngular.eUnit.OneCyclePerSecond)]
        [TestCase("Hz", cUnitSpeedAngular.eUnit.Hertz)]
        public void GetUnitsEnum(string shorthandUnitName, double expectedResult)
        {
            cUnitSpeedAngular.eUnit unit = cUnitSpeedAngular.GetUnitsEnum(shorthandUnitName);

            Assert.That(unit, Is.EqualTo((cUnitSpeedAngular.eUnit)expectedResult));
        }


        [TestCase(null, cUnitSpeedAngular.eUnit.None)]
        [TestCase("", cUnitSpeedAngular.eUnit.None)]
        [TestCase("1/sec", cUnitSpeedAngular.eUnit.OneCyclePerSecond)]
        [TestCase("Hz", cUnitSpeedAngular.eUnit.Hertz)]
        public void SetUnitByName_By_String_Name_Sets_Units(string shorthandUnitName, double expectedResult)
        {
            cUnitSpeedAngular unitSpeedAngular = new cUnitSpeedAngular();
            Assert.That(unitSpeedAngular.Unit, Is.EqualTo(cUnitSpeedAngular.eUnit.OneCyclePerSecond));

            // Method Under Test
            unitSpeedAngular.SetUnitByName(shorthandUnitName);
            Assert.That(unitSpeedAngular.Unit, Is.EqualTo((cUnitSpeedAngular.eUnit)expectedResult));
        }
    }
}
