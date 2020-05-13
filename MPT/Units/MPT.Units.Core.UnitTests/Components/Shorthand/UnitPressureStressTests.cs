using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components.Shorthand
{
    [TestFixture]
    public class UnitPressureStressTests
    {
        [Test]
        public void Initialize_Sets_Default_Type_And_Empty_List()
        {
            cUnitPressureStress unitPressureStress = new cUnitPressureStress();

            Assert.That(cUnitPressureStress.UnitDefault, Is.EqualTo(cUnitPressureStress.eUnit.PoundPerSquareInch));
            Assert.IsTrue(cUnitPressureStress.UnitsList.Contains(""));
            Assert.IsTrue(cUnitPressureStress.UnitsList.Contains("psi"));
            Assert.IsTrue(cUnitPressureStress.UnitsList.Contains("psf"));
            Assert.IsTrue(cUnitPressureStress.UnitsList.Contains("ksi"));
            Assert.IsTrue(cUnitPressureStress.UnitsList.Contains("ksf"));
            Assert.IsTrue(cUnitPressureStress.UnitsList.Contains("Pa"));
            Assert.IsTrue(cUnitPressureStress.UnitsList.Contains("kPa"));
            Assert.IsTrue(cUnitPressureStress.UnitsList.Contains("MPa"));
            Assert.IsTrue(cUnitPressureStress.UnitsList.Contains("GPa"));
            Assert.That(unitPressureStress.Unit, Is.EqualTo(cUnitPressureStress.UnitDefault));
        }

        [Test]
        public void SetToDefault_Resets_Unit_To_Default()
        {
            cUnitPressureStress unitPressureStress = new cUnitPressureStress();
            cUnitPressureStress.eUnit defaultUnit = cUnitPressureStress.UnitDefault;

            Assert.That(defaultUnit, Is.EqualTo(cUnitPressureStress.eUnit.PoundPerSquareInch));
            unitPressureStress.Unit = cUnitPressureStress.eUnit.Pascal;
            Assert.That(unitPressureStress.Unit, Is.EqualTo(cUnitPressureStress.eUnit.Pascal));

            unitPressureStress.SetToDefault();
            Assert.That(unitPressureStress.Unit, Is.EqualTo(cUnitPressureStress.eUnit.PoundPerSquareInch));
        }

        [Test]
        public void UnitsList_Is_Immutable()
        {
            Assert.That(cUnitPressureStress.UnitsList.Count, Is.EqualTo(9));

            string invalidUnits = "invalid";
            cUnitPressureStress.UnitsList.Add(invalidUnits);
            Assert.That(cUnitPressureStress.UnitsList.Count, Is.EqualTo(9));
        }

        [Test]
        public void GetUnits_Of_None_Returns_NA()
        {
            cUnitPressureStress unitPressureStress =
                new cUnitPressureStress { Unit = cUnitPressureStress.eUnit.None };
            cUnits units = unitPressureStress.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("NA/NA^2"));
        }

        [Test]
        public void GetUnits_Returns_Units()
        {
            cUnitPressureStress unitPressureStress = new cUnitPressureStress();
            cUnits units = unitPressureStress.GetUnits();
            Assert.That(units.GetUnitsLabel(), Is.EqualTo("lb/in^2"));
        }

        [TestCase(null, ExpectedResult = "NA/NA^2")]
        [TestCase(100, ExpectedResult = "NA/NA^2")] // Out of range
        [TestCase(cUnitPressureStress.eUnit.None, ExpectedResult = "NA/NA^2")]
        [TestCase(cUnitPressureStress.eUnit.PoundPerSquareInch, ExpectedResult = "lb/in^2")]
        [TestCase(cUnitPressureStress.eUnit.PoundPerSquareFoot, ExpectedResult = "lb/ft^2")]
        [TestCase(cUnitPressureStress.eUnit.KipPerSquareInch, ExpectedResult = "Kip/in^2")]
        [TestCase(cUnitPressureStress.eUnit.KipPerSquareFoot, ExpectedResult = "Kip/ft^2")]
        [TestCase(cUnitPressureStress.eUnit.Pascal, ExpectedResult = "N/m^2")]
        [TestCase(cUnitPressureStress.eUnit.KiloPascal, ExpectedResult = "kN/m^2")]
        [TestCase(cUnitPressureStress.eUnit.MegaPascal, ExpectedResult = "MN/m^2")]
        [TestCase(cUnitPressureStress.eUnit.GigaPascal, ExpectedResult = "GN/m^2")]
        public string GetUnits_By_Enum_Name_Returns_Units_Of_Name(double shorthandUnitName)
        {
            cUnits units = cUnitPressureStress.GetUnits((cUnitPressureStress.eUnit)shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, ExpectedResult = "lb/in^2")]
        [TestCase("", ExpectedResult = "lb/in^2")]
        [TestCase("psi", ExpectedResult = "lb/in^2")]
        [TestCase("ksi", ExpectedResult = "Kip/in^2")]
        public string GetUnits_By_String_Name_Returns_Units_Of_Name(string shorthandUnitName)
        {
            cUnits units = cUnitPressureStress.GetUnits(shorthandUnitName);
            return units.GetUnitsLabel();
        }

        [TestCase(null, cUnitPressureStress.eUnit.None)]
        [TestCase("", cUnitPressureStress.eUnit.None)]
        [TestCase("psi", cUnitPressureStress.eUnit.PoundPerSquareInch)]
        [TestCase("Pa", cUnitPressureStress.eUnit.Pascal)]
        public void GetUnitsEnum(string shorthandUnitName, double expectedResult)
        {
            cUnitPressureStress.eUnit unit = cUnitPressureStress.GetUnitsEnum(shorthandUnitName);

            Assert.That(unit, Is.EqualTo((cUnitPressureStress.eUnit)expectedResult));
        }


        [TestCase(null, cUnitPressureStress.eUnit.None)]
        [TestCase("", cUnitPressureStress.eUnit.None)]
        [TestCase("psi", cUnitPressureStress.eUnit.PoundPerSquareInch)]
        [TestCase("Pa", cUnitPressureStress.eUnit.Pascal)]
        public void SetUnitByName_By_String_Name_Sets_Units(string shorthandUnitName, double expectedResult)
        {
            cUnitPressureStress unitPressureStress = new cUnitPressureStress();
            Assert.That(unitPressureStress.Unit, Is.EqualTo(cUnitPressureStress.eUnit.PoundPerSquareInch));

            // Method Under Test
            unitPressureStress.SetUnitByName(shorthandUnitName);
            Assert.That(unitPressureStress.Unit, Is.EqualTo((cUnitPressureStress.eUnit)expectedResult));
        }
    }
}
