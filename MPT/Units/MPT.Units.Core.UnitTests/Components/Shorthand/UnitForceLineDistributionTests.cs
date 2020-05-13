using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Shorthand
{
    [TestFixture]
    public class UnitForceLineDistributionTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitForceLineDistribution unitForceLineDistribution = new cUnitForceLineDistribution();

            Assert.That(cUnitForceLineDistribution.UnitDefault, Is.EqualTo(cUnitForceLineDistribution.eUnit.PoundForcePerLinearFoot));
            Assert.IsTrue(cUnitForceLineDistribution.UnitsList.Contains(""));
            Assert.IsTrue(cUnitForceLineDistribution.UnitsList.Contains("plf"));
            Assert.IsTrue(cUnitForceLineDistribution.UnitsList.Contains("klf"));
            Assert.That(unitForceLineDistribution.Unit, Is.EqualTo(cUnitForceLineDistribution.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitForceLineDistribution unitForceLineDistribution = new cUnitForceLineDistribution();
            cUnitForceLineDistribution.eUnit defaultUnit = cUnitForceLineDistribution.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitForceLineDistribution.eUnit.PoundForcePerLinearFoot));
            unitForceLineDistribution.Unit = cUnitForceLineDistribution.eUnit.KipPerLinearFoot;
            Assert.That(unitForceLineDistribution.Unit, Is.EqualTo(cUnitForceLineDistribution.eUnit.KipPerLinearFoot));

            unitForceLineDistribution.SetToDefault();
            Assert.That(unitForceLineDistribution.Unit, Is.EqualTo(cUnitForceLineDistribution.eUnit.PoundForcePerLinearFoot));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitForceLineDistribution.UnitsList.Count, Is.EqualTo(3));

            string invalidUnits = "invalid";
            cUnitForceLineDistribution.UnitsList.Add(invalidUnits);
            Assert.That(cUnitForceLineDistribution.UnitsList.Count, Is.EqualTo(3));
        }

        [Test]
        public void GetUnits_Of_None_Returns_NA()
        {
            cUnitForceLineDistribution unitForceLineDistribution =
                new cUnitForceLineDistribution {Unit = cUnitForceLineDistribution.eUnit.None};
            cUnits units = unitForceLineDistribution.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("NA/NA"));
        }

        [Test]
        public void GetUnits_Returns_Units()
        {
            cUnitForceLineDistribution unitForceLineDistribution = new cUnitForceLineDistribution();
            cUnits units = unitForceLineDistribution.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("lb/ft"));
        }

        [TestCase(null, ExpectedResult = "NA/NA")]
        [TestCase(100, ExpectedResult = "NA/NA")] // Out of range
        [TestCase(cUnitForceLineDistribution.eUnit.None, ExpectedResult = "NA/NA")]
        [TestCase(cUnitForceLineDistribution.eUnit.PoundForcePerLinearFoot, ExpectedResult = "lb/ft")]
        [TestCase(cUnitForceLineDistribution.eUnit.KipPerLinearFoot, ExpectedResult = "Kip/ft")]
        public string GetUnits_By_Enum_Name_Returns_Units_Of_Name(double shorthandUnitName)
        {
            cUnits units = cUnitForceLineDistribution.GetUnits((cUnitForceLineDistribution.eUnit)shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, ExpectedResult = "lb/ft")]
        [TestCase("", ExpectedResult = "lb/ft")]
        [TestCase("plf", ExpectedResult = "lb/ft")]
        [TestCase("klf", ExpectedResult = "Kip/ft")]
        public string GetUnits_By_String_Name_Returns_Units_Of_Name(string shorthandUnitName)
        {
            cUnits units = cUnitForceLineDistribution.GetUnits(shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, cUnitForceLineDistribution.eUnit.None)]
        [TestCase("", cUnitForceLineDistribution.eUnit.None)]
        [TestCase("plf", cUnitForceLineDistribution.eUnit.PoundForcePerLinearFoot)]
        [TestCase("klf", cUnitForceLineDistribution.eUnit.KipPerLinearFoot)]
        public void GetUnitsEnum(string shorthandUnitName, double expectedResult)
        {
            cUnitForceLineDistribution.eUnit unit = cUnitForceLineDistribution.GetUnitsEnum(shorthandUnitName);

            Assert.That(unit, Is.EqualTo((cUnitForceLineDistribution.eUnit)expectedResult));
        }


        [TestCase(null, cUnitForceLineDistribution.eUnit.None)]
        [TestCase("", cUnitForceLineDistribution.eUnit.None)]
        [TestCase("plf", cUnitForceLineDistribution.eUnit.PoundForcePerLinearFoot)]
        [TestCase("klf", cUnitForceLineDistribution.eUnit.KipPerLinearFoot)]
        public void SetUnitByName_By_String_Name_Sets_Units(string shorthandUnitName, double expectedResult)
        {
            cUnitForceLineDistribution unitForceLineDistribution = new cUnitForceLineDistribution();
            Assert.That(unitForceLineDistribution.Unit, Is.EqualTo(cUnitForceLineDistribution.eUnit.PoundForcePerLinearFoot));

            // Method Under Test
            unitForceLineDistribution.SetUnitByName(shorthandUnitName);
            Assert.That(unitForceLineDistribution.Unit, Is.EqualTo((cUnitForceLineDistribution.eUnit)expectedResult));
        }
    }
}
