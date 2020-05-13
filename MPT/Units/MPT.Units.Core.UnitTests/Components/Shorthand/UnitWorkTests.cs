using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Shorthand
{
    [TestFixture]
    public class UnitWorkTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitWork unitWork = new cUnitWork();

            Assert.That(cUnitWork.UnitDefault, Is.EqualTo(cUnitWork.eUnit.Joule));
            Assert.IsTrue(cUnitWork.UnitsList.Contains(""));
            Assert.IsTrue(cUnitWork.UnitsList.Contains("J"));
            Assert.IsTrue(cUnitWork.UnitsList.Contains("kJ"));
            Assert.IsTrue(cUnitWork.UnitsList.Contains("MJ"));
            Assert.IsTrue(cUnitWork.UnitsList.Contains("GJ"));
            Assert.That(unitWork.Unit, Is.EqualTo(cUnitWork.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitWork unitWork = new cUnitWork();
            cUnitWork.eUnit defaultUnit = cUnitWork.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitWork.eUnit.Joule));
            unitWork.Unit = cUnitWork.eUnit.KiloJoule;
            Assert.That(unitWork.Unit, Is.EqualTo(cUnitWork.eUnit.KiloJoule));

            unitWork.SetToDefault();
            Assert.That(unitWork.Unit, Is.EqualTo(cUnitWork.eUnit.Joule));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitWork.UnitsList.Count, Is.EqualTo(5));

            string invalidUnits = "invalid";
            cUnitWork.UnitsList.Add(invalidUnits);
            Assert.That(cUnitWork.UnitsList.Count, Is.EqualTo(5));
        }

        [Test]
        public void GetUnits_Of_None_Returns_NA()
        {
            cUnitWork unitWork =
                new cUnitWork { Unit = cUnitWork.eUnit.None };
            cUnits units = unitWork.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("NA*NA"));
        }

        [Test]
        public void GetUnits_Returns_Units()
        {
            cUnitWork unitWork = new cUnitWork();
            cUnits units = unitWork.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("N*m"));
        }

        [TestCase(null, ExpectedResult = "NA*NA")]
        [TestCase(100, ExpectedResult = "NA*NA")] // Out of range
        [TestCase(cUnitWork.eUnit.None, ExpectedResult = "NA*NA")]
        [TestCase(cUnitWork.eUnit.Joule, ExpectedResult = "N*m")]
        [TestCase(cUnitWork.eUnit.KiloJoule, ExpectedResult = "kN*m")]
        [TestCase(cUnitWork.eUnit.MegaJoule, ExpectedResult = "MN*m")]
        [TestCase(cUnitWork.eUnit.GigaJoule, ExpectedResult = "GN*m")]
        public string GetUnits_By_Enum_Name_Returns_Units_Of_Name(double shorthandUnitName)
        {
            cUnits units = cUnitWork.GetUnits((cUnitWork.eUnit)shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, ExpectedResult = "N*m")]
        [TestCase("", ExpectedResult = "N*m")]
        [TestCase("J", ExpectedResult = "N*m")]
        [TestCase("kJ", ExpectedResult = "kN*m")]
        public string GetUnits_By_String_Name_Returns_Units_Of_Name(string shorthandUnitName)
        {
            cUnits units = cUnitWork.GetUnits(shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, cUnitWork.eUnit.None)]
        [TestCase("", cUnitWork.eUnit.None)]
        [TestCase("J", cUnitWork.eUnit.Joule)]
        [TestCase("kJ", cUnitWork.eUnit.KiloJoule)]
        public void GetUnitsEnum(string shorthandUnitName, double expectedResult)
        {
            cUnitWork.eUnit unit = cUnitWork.GetUnitsEnum(shorthandUnitName);

            Assert.That(unit, Is.EqualTo((cUnitWork.eUnit)expectedResult));
        }


        [TestCase(null, cUnitWork.eUnit.None)]
        [TestCase("", cUnitWork.eUnit.None)]
        [TestCase("J", cUnitWork.eUnit.Joule)]
        [TestCase("kJ", cUnitWork.eUnit.KiloJoule)]
        public void SetUnitByName_By_String_Name_Sets_Units(string shorthandUnitName, double expectedResult)
        {
            cUnitWork unitWork = new cUnitWork();
            Assert.That(unitWork.Unit, Is.EqualTo(cUnitWork.eUnit.Joule));

            // Method Under Test
            unitWork.SetUnitByName(shorthandUnitName);
            Assert.That(unitWork.Unit, Is.EqualTo((cUnitWork.eUnit)expectedResult));
        }
    }
}
