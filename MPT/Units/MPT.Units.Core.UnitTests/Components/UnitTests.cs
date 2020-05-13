using System.Collections.Generic;
using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components
{
    [TestFixture]
    public class UnitTests
    {

        [Test]
        public void Initialization_Basic_Sets_Defaults()
        {
            cUnit unit = new cUnit();

            Assert.That(unit.Type, Is.EqualTo(eUnitType.None));
            Assert.That(unit.Name, Is.EqualTo(string.Empty));
            Assert.That(unit.Numerator, Is.EqualTo(true));
            Assert.That(unit.Power, Is.EqualTo("1"));
            Assert.That(unit.UnitsList.Count, Is.EqualTo(0));
        }

        [Test]
        public void Initialization_With_Parameters_Sets_Properties_By_Parameters()
        {
            eUnitType unitTypeBasic = eUnitType.Force;
            cUnit unitBasic = new cUnit(unitTypeBasic);

            Assert.That(unitBasic.Type, Is.EqualTo(unitTypeBasic));
            Assert.That(unitBasic.Name, Is.EqualTo("lb"));
            Assert.That(unitBasic.Numerator, Is.EqualTo(true));
            Assert.That(unitBasic.UnitsList.Count, Is.EqualTo(9)); 

            eUnitType unitType = eUnitType.Length;
            string unitPower = "2";
            bool isNumerator = false;
            cUnit unitFull = new cUnit(unitType, unitPower, isNumerator);

            Assert.That(unitFull.Type, Is.EqualTo(unitType));
            Assert.That(unitFull.Name, Is.EqualTo("in"));
            Assert.That(unitFull.Power, Is.EqualTo(unitPower));
            Assert.That(unitFull.Numerator, Is.EqualTo(isNumerator));
            Assert.That(unitFull.UnitsList.Count, Is.EqualTo(10));
        }


        [TestCase(100, "")]  // Out of range
        [TestCase(eUnitType.None, "")]
        [TestCase(eUnitType.Unitless, "")]
        [TestCase(eUnitType.Length, "in")]
        [TestCase(eUnitType.Mass, "lbm")]
        [TestCase(eUnitType.Rotation, "rad")]
        [TestCase(eUnitType.Temperature, "F")]
        [TestCase(eUnitType.Time, "sec")]
        [TestCase(eUnitType.Force, "lb")]
        public void Initialization_by_Unit_Type(eUnitType type, string expectedName)
        {
            cUnit unit = new cUnit(type);

            Assert.That(unit.Type, Is.EqualTo(type));
            Assert.That(unit.Name, Is.EqualTo(expectedName));
        }

        [Test]
        public void Get_Set_Properties_Gets_Sets_Properties()
        {
            cUnit unit = new cUnit();

            Assert.That(unit.Type, Is.EqualTo(eUnitType.None));
            Assert.That(unit.Name, Is.EqualTo(string.Empty));
            Assert.That(unit.Numerator, Is.EqualTo(true));
            Assert.That(unit.UnitsList.Count, Is.EqualTo(0));
            
            unit.Numerator = false;
            unit.Type = eUnitType.Force;

            Assert.That(unit.Type, Is.EqualTo(eUnitType.Force));
            Assert.That(unit.Name, Is.EqualTo("lb"));
            Assert.That(unit.Numerator, Is.EqualTo(false));
            Assert.That(unit.UnitsList.Count, Is.EqualTo(9));
            Assert.IsTrue(unit.UnitsList.Contains(""));
            Assert.IsTrue(unit.UnitsList.Contains("lb"));
            Assert.IsTrue(unit.UnitsList.Contains("Kip"));
            Assert.IsTrue(unit.UnitsList.Contains("N"));
            Assert.IsTrue(unit.UnitsList.Contains("kN"));
            Assert.IsTrue(unit.UnitsList.Contains("MN"));
            Assert.IsTrue(unit.UnitsList.Contains("GN"));
            Assert.IsTrue(unit.UnitsList.Contains("kgf"));
            Assert.IsTrue(unit.UnitsList.Contains("tf"));
        }

        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("Inch", ExpectedResult = "in")]
        [TestCase("inch", ExpectedResult = "in")]
        [TestCase("TonF", ExpectedResult = "tf")]
        [TestCase("tonf", ExpectedResult = "tf")]
        [TestCase("kn", ExpectedResult = "kN")]
        [TestCase("KN", ExpectedResult = "kN")]
        [TestCase("Kgf", ExpectedResult = "kgf")]
        [TestCase("kgF", ExpectedResult = "kgf")]
        [TestCase("s", ExpectedResult = "sec")]
        [TestCase("S", ExpectedResult = "sec")]
        [TestCase("mile", ExpectedResult = "mile")]
        public string SetUnitName_Sets_Unit_Name(string name)
        {
            cUnit unit = new cUnit();
            
            Assert.That(unit.Name, Is.EqualTo(string.Empty));

            unit.SetUnitName(name);

            return unit.Name;
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SetUnitName_Maintains_Default_Unit_Name_For_Empty_Names_and_Known_Types(string name)
        {
            cUnit unit = new cUnit(eUnitType.Force);

            Assert.That(unit.Name, Is.EqualTo("lb"));

            unit.SetUnitName(name);

            Assert.That(unit.Name, Is.EqualTo("lb"));
        }

        [TestCase(null, eUnitType.Unitless, ExpectedResult = "")]
        [TestCase("", eUnitType.Unitless, ExpectedResult = "")]
        [TestCase("in", eUnitType.Length, ExpectedResult = "in")]
        public string SetUnitName_Of_Unit_Type_Sets_Unit_Name(string name, double type)
        {
            cUnit unit = new cUnit((eUnitType)type);

            unit.SetUnitName(name);

            return unit.Name;
        }

        [TestCase(null, ExpectedResult = eUnitType.None)]
        [TestCase("", ExpectedResult = eUnitType.None)]
        [TestCase("FooBar", ExpectedResult = eUnitType.None)]
        [TestCase("kN", ExpectedResult = eUnitType.Force)]
        [TestCase("sec", ExpectedResult = eUnitType.Time)]
        [TestCase("kg", ExpectedResult = eUnitType.Mass)]
        [TestCase("rad", ExpectedResult = eUnitType.Rotation)]
        [TestCase("F", ExpectedResult = eUnitType.Temperature)]
        [TestCase("in", ExpectedResult = eUnitType.Length)]
        [TestCase("in/in", ExpectedResult = eUnitType.Unitless)]
        [TestCase("Inch", ExpectedResult = eUnitType.Length)]
        [TestCase("MN", ExpectedResult = eUnitType.Force)]
        [TestCase("mN", ExpectedResult = eUnitType.Force)]
        [TestCase("mm", ExpectedResult = eUnitType.Length)]
        [TestCase("MM", ExpectedResult = eUnitType.Length)]
        public eUnitType SetUnitTypeFromName_Sets_Type_From_Name_If_Matching(string name)
        {
            cUnit unit = new cUnit();

            Assert.That(unit.Type, Is.EqualTo(eUnitType.None));
            Assert.That(unit.Name, Is.EqualTo(string.Empty));

            unit.SetUnitName(name);
            string expectedName = name;
            if (string.IsNullOrWhiteSpace(expectedName))
            {
                expectedName = string.Empty;
            }
            else if (expectedName == "Inch")
            {
                expectedName = "in";
            }
            Assert.That(unit.Name, Is.EqualTo(expectedName));
            return unit.SetUnitTypeFromName();
        }
        
        [Test]
        public void UnitsList_Is_Immutable()
        {
            cUnit unit = new cUnit();

            Assert.That(unit.Type, Is.EqualTo(eUnitType.None));
            Assert.That(unit.Name, Is.EqualTo(string.Empty));
            Assert.That(unit.UnitsList.Count, Is.EqualTo(0));

            unit.Type = eUnitType.Force;
            Assert.That(unit.UnitsList.Count, Is.EqualTo(9));

            string invalidUnits = "invalid";
            unit.UnitsList.Add(invalidUnits);
            Assert.That(unit.UnitsList.Count, Is.EqualTo(9));
        }

        [TestCase(null, ExpectedResult = "1")]
        [TestCase("", ExpectedResult = "1")]
        [TestCase("power", ExpectedResult = "1")]
        [TestCase("2", ExpectedResult = "2")]
        [TestCase("95", ExpectedResult = "95")]
        [TestCase("1/2", ExpectedResult = "1/2")]
        [TestCase("0.25", ExpectedResult = "0.25")]
        public string Set_Power_Limited_To_Numerical(string power)
        {
            cUnit unit = new cUnit();
            
            Assert.That(unit.Power, Is.EqualTo("1"));

            unit.SetUnitPower(power);

            return unit.Power;
        }
        
        [TestCase("-", "1", true)]
        [TestCase("-power", "1", false)]
        [TestCase("-1", "1", false)]
        [TestCase("1/2", "1/2", true)]
        [TestCase("-1/2", "1/2", false)]
        [TestCase("0.25", "0.25", true)]
        [TestCase("-0.25", "0.25", false)]
        public void Set_Power_With_Negative_Number_Changes_Unit_To_Denominator(string power, string expectedPower, bool expectedAsNumerator)
        {
            cUnit unit = new cUnit();

            Assert.That(unit.Power, Is.EqualTo("1"));

            unit.SetUnitPower(power);
            Assert.That(unit.Power, Is.EqualTo(expectedPower));
            Assert.That(unit.Numerator, Is.EqualTo(expectedAsNumerator));
        }

        [Test]
        public void Clone_Clones_Object()
        {
            eUnitType unitType = eUnitType.Length;
            string unitPower = "2";
            bool isNumerator = false;
            cUnit unitOriginal = new cUnit(unitType, unitPower, isNumerator);

            Assert.That(unitOriginal.Type, Is.EqualTo(unitType));
            Assert.That(unitOriginal.Name, Is.EqualTo("in"));
            Assert.That(unitOriginal.Power, Is.EqualTo(unitPower));
            Assert.That(unitOriginal.Numerator, Is.EqualTo(isNumerator));
            Assert.That(unitOriginal.UnitsList.Count, Is.EqualTo(10));
            Assert.That(unitOriginal.UnitsList.Contains("ft"));

            object unitClone = unitOriginal.Clone();

            Assert.That(unitClone is cUnit);
            cUnit unitCloneCast = (cUnit) unitClone;

            Assert.That(unitCloneCast.Type, Is.EqualTo(unitType));
            Assert.That(unitCloneCast.Name, Is.EqualTo("in"));
            Assert.That(unitCloneCast.Power, Is.EqualTo(unitPower));
            Assert.That(unitCloneCast.Numerator, Is.EqualTo(isNumerator));
            Assert.That(unitCloneCast.UnitsList.Count, Is.EqualTo(10));
            Assert.That(unitCloneCast.UnitsList.Contains("ft"));
        }



        [Test]
        public void Equals_Compares_Objects_By_Properties()
        {
            eUnitType unitType = eUnitType.Length;
            string unitPower = "2";
            bool isNumerator = false;
            cUnit unit1 = new cUnit(unitType, unitPower, isNumerator);

            Assert.That(unit1.Type, Is.EqualTo(unitType));
            Assert.That(unit1.Name, Is.EqualTo("in"));
            Assert.That(unit1.Power, Is.EqualTo(unitPower));
            Assert.That(unit1.Numerator, Is.EqualTo(isNumerator));
            Assert.That(unit1.UnitsList.Count, Is.EqualTo(10));
            Assert.That(unit1.UnitsList.Contains("ft"));
            
            cUnit unit2 = new cUnit(unitType, unitPower, isNumerator);

            Assert.That(unit2.Type, Is.EqualTo(unitType));
            Assert.That(unit2.Name, Is.EqualTo("in"));
            Assert.That(unit2.Power, Is.EqualTo(unitPower));
            Assert.That(unit2.Numerator, Is.EqualTo(isNumerator));
            Assert.That(unit2.UnitsList.Count, Is.EqualTo(10));
            Assert.That(unit2.UnitsList.Contains("ft"));

            // Method Under Test
            Assert.That(unit1.Equals(unit2));

            unit2.SetUnitPower("3"); 
            Assert.That(!unit1.Equals(unit2));

            unit2.SetUnitPower("2"); 
            Assert.That(unit1.Equals(unit2));

            unit2.Numerator = true;
            Assert.That(!unit1.Equals(unit2));

            unit2.Numerator = false;
            Assert.That(unit1.Equals(unit2));

            unit2.Type = eUnitType.Force;
            Assert.That(!unit1.Equals(unit2));

            unit2.Type = eUnitType.Length;
            Assert.That(unit1.Equals(unit2));


            Assert.That(!unit1.Equals(null));
        }

        [TestCase(eUnitType.None, "", 0)]
        [TestCase(eUnitType.Unitless, "rad", 40)]
        [TestCase(eUnitType.Length, "Mile", 10)]
        [TestCase(eUnitType.Mass, "kg", 3)]
        [TestCase(eUnitType.Rotation, "rad", 7)]
        [TestCase(eUnitType.Temperature, "F", 3)]
        [TestCase(eUnitType.Time, "hr", 8)]
        [TestCase(eUnitType.Force, "Kip", 9)]
        [TestCase(100, "", 0)] // Out of range
        public void GetUnitsList_Returns_List_Of_Allowed_Units_For_Specified_Unit_Type(eUnitType unitType, string expectedUnit, int expectedListCount)
        {
            List<string> unitsList = cUnit.GetUnitsList(unitType);

            Assert.That(unitsList.Count, Is.EqualTo(expectedListCount));

            if (unitsList.Count > 0)
            {
                Assert.That(unitsList.Contains(expectedUnit));
            }
        }

        [TestCase(null, null, true, false, false, false, ExpectedResult = "NA")]
        [TestCase("", "", true, false, false, false, ExpectedResult = "NA")]
        [TestCase("", "", true, true, false, false, ExpectedResult = "Force")]
        [TestCase("kN", "", true, true, false, false, ExpectedResult = "Force")]
        [TestCase("kN", "", true, false, false, false, ExpectedResult = "kN")]
        [TestCase("kN", "2", true, true, false, false, ExpectedResult = "Force")]
        [TestCase("kN", "", true, true, true, false, ExpectedResult = "Force")]
        [TestCase("kN", "1", true, true, true, false, ExpectedResult = "Force")]
        [TestCase("kN", "2", true, true, true, false, ExpectedResult = "Force^2")]
        [TestCase("kN", "1/2", true, true, true, false, ExpectedResult = "Force^(1/2)")]
        [TestCase("kN", "1/2", true, true, true, true, ExpectedResult = "Force^(1/2)")]
        [TestCase("kN", "1/2", false, true, true, true, ExpectedResult = "1/Force^(1/2)")]
        [TestCase("kN", "5", false, true, true, true, ExpectedResult = "1/Force^5")]
        [TestCase("kN", "5", false, false, true, true, ExpectedResult = "1/kN^5")]
        public string GetUnitLabel(string name, string power, bool unitIsNumerator, bool parseSchema, bool withPowers, bool asList)
        {
            cUnit unit = new cUnit(eUnitType.Force, unitPower: power, unitIsNumerator: unitIsNumerator);
            unit.SetUnitName(name);
            return unit.GetUnitLabel(parseSchema, withPowers, asList);
        }

        [Test]
        public void GetUnitLabel_without_Setting_Name_Returns_NA()
        {
            cUnit unit = new cUnit(eUnitType.Force, unitPower: "5", unitIsNumerator: false);
            string unitLabel = unit.GetUnitLabel(parseSchema: false, withPowers: true, asList: true);

            Assert.That(unitLabel, Is.EqualTo("1/NA^5"));
        }

        [Test]
        public void GetUnitLabel_without_Setting_Name_Returns_Default_If_Specified()
        {
            cUnit unit = new cUnit(eUnitType.Force, unitPower: "5", unitIsNumerator: false);
            string unitLabel = unit.GetUnitLabel(parseSchema: false, withPowers: true, asList: true, useDefaultsIfNA: true);
            Assert.That(unitLabel, Is.EqualTo("1/lb^5"));
        }

        [Test]
        public void MultiplyUnitPowers()
        {
            cUnit unit1 = new cUnit(eUnitType.Force, unitPower: "6", unitIsNumerator: true);
            cUnit unit2 = new cUnit(eUnitType.Force, unitPower: "4", unitIsNumerator: true);
            string newPowerLabel = unit1.MultiplyUnitPowers(unit2);
            Assert.That(newPowerLabel, Is.EqualTo("10"));
        }

        [Test]
        public void MultiplyUnitPowers2a()
        {
            cUnit unit1 = new cUnit(eUnitType.Force, unitPower: "6", unitIsNumerator: true);
            cUnit unit2 = new cUnit(eUnitType.Force, unitPower: "-4", unitIsNumerator: false);
            string newPowerLabel = unit1.MultiplyUnitPowers(unit2);
            Assert.That(newPowerLabel, Is.EqualTo("10"));
        }

        [Test]
        public void MultiplyUnitPowers2b()
        {
            cUnit unit1 = new cUnit(eUnitType.Force, unitPower: "6", unitIsNumerator: true);
            cUnit unit2 = new cUnit(eUnitType.Force, unitPower: "-4", unitIsNumerator: true);
            string newPowerLabel = unit1.MultiplyUnitPowers(unit2);
            Assert.That(newPowerLabel, Is.EqualTo("2"));
        }

        [Test]
        public void MultiplyUnitPowers3()
        {
            cUnit unit1 = new cUnit(eUnitType.Force, unitPower: "6", unitIsNumerator: true);
            cUnit unit2 = new cUnit(eUnitType.Force, unitPower: "4", unitIsNumerator: false);
            string newPowerLabel = unit1.MultiplyUnitPowers(unit2);
            Assert.That(newPowerLabel, Is.EqualTo("2"));
        }



        [Test]
        public void DivideUnitPowers()
        {
            cUnit unit1 = new cUnit(eUnitType.Force, unitPower: "6", unitIsNumerator: true);
            cUnit unit2 = new cUnit(eUnitType.Force, unitPower: "4", unitIsNumerator: true);
            string newPowerLabel = unit1.DivideUnitPowers(unit2);
            Assert.That(newPowerLabel, Is.EqualTo("2"));
        }

        [Test]
        public void DivideUnitPowers2a()
        {
            cUnit unit1 = new cUnit(eUnitType.Force, unitPower: "6", unitIsNumerator: true);
            cUnit unit2 = new cUnit(eUnitType.Force, unitPower: "-4", unitIsNumerator: false);
            string newPowerLabel = unit1.DivideUnitPowers(unit2);
            Assert.That(newPowerLabel, Is.EqualTo("2"));
        }

        [Test]
        public void DivideUnitPowers2b()
        {
            cUnit unit1 = new cUnit(eUnitType.Force, unitPower: "6", unitIsNumerator: true);
            cUnit unit2 = new cUnit(eUnitType.Force, unitPower: "-4", unitIsNumerator: true);
            string newPowerLabel = unit1.DivideUnitPowers(unit2);
            Assert.That(newPowerLabel, Is.EqualTo("10"));
        }

        [Test]
        public void DivideUnitPowers3()
        {
            cUnit unit1 = new cUnit(eUnitType.Force, unitPower: "6", unitIsNumerator: true);
            cUnit unit2 = new cUnit(eUnitType.Force, unitPower: "4", unitIsNumerator: false);
            string newPowerLabel = unit1.DivideUnitPowers(unit2);
            Assert.That(newPowerLabel, Is.EqualTo("10"));
        }
        
        [TestCase(eUnitType.None, null, null, 1, 0.1)]
        [TestCase(eUnitType.Unitless, null, null, 1, 0.1)]
        [TestCase(eUnitType.Force, null, null, 0, 0.1)]
        [TestCase(eUnitType.Force, "", "", 0, 0.1)]
        [TestCase(eUnitType.Force, "kN", "", 224.8, 0.1)]
        [TestCase(eUnitType.Force, "kN", "lb", 224.8, 0.1)]
        [TestCase(eUnitType.Force, "kN", "kip", 0.2248, 0.0001)]
        [TestCase(eUnitType.Length, "Mile", "ft", 5280, 0.1)]
        [TestCase(eUnitType.Mass, "kg", "lbm", 2.205, 0.001)]
        [TestCase(eUnitType.Rotation, "deg", "rad", 0.01745, 0.00001)]
        [TestCase(eUnitType.Temperature, "F", "C", -17.2, 0.1)]
        [TestCase(eUnitType.Time, "min", "sec", 60, 0.1)]
        [TestCase(100, null, null, 1, 0.1)] // Out of range
        public void Convert_Units_As_Strings_Converts_Units(
            eUnitType unitType, 
            string unitToConvertFrom, 
            string unitToConvertTo,
            double expected,
            double tolerance)
        {
            cUnit unit = new cUnit(unitType);
            double result = unit.Convert(unitToConvertFrom, unitToConvertTo);
            Assert.AreEqual(expected, result, tolerance);
        }

        [Test]
        public void Convert_Units_As_Null_Objects_Returns_Zero()
        {
            cUnit unit = new cUnit(eUnitType.Force);
            cUnit unitFrom = null;
            cUnit unitTo = null;
            double result = unit.Convert(unitFrom, unitTo);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Convert_Units_From_Object_To_Null_Converts_To_Callee_Unit()
        {
            cUnit unitFrom = new cUnit(eUnitType.Force);
            unitFrom.SetUnitName("kN");

            cUnit unitTo = new cUnit(eUnitType.Force);
            unitTo.SetUnitName("kip");

            double result = unitTo.Convert(unitFrom, null);

            Assert.That(result, Is.EqualTo(0.2248).Within(0.0001));
        }

        [Test]
        public void Convert_Units_From_Null_Object_Returns_Zero()
        {
            cUnit unitFrom = new cUnit(eUnitType.Force);
            unitFrom.SetUnitName("kN");

            cUnit unitTo = new cUnit(eUnitType.Force);
            unitTo.SetUnitName("kip");

            double result = unitTo.Convert(null, unitTo);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Convert_Units_As_Incompatible_Objects_Returns_Zero()
        {
            cUnit unitFrom = new cUnit(eUnitType.Force);
            unitFrom.SetUnitName("kN");

            cUnit unitTo = new cUnit(eUnitType.Length);
            unitTo.SetUnitName("ft");

            double result = unitTo.Convert(unitFrom, unitTo);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Convert_Units_As_Objects_Converts_Units()
        {
            cUnit unitFrom = new cUnit(eUnitType.Force);
            unitFrom.SetUnitName("kN");

            cUnit unitTo = new cUnit(eUnitType.Force);
            unitTo.SetUnitName("kip");

            double result = unitTo.Convert(unitFrom, unitTo);

            Assert.That(result, Is.EqualTo(0.2248).Within(0.0001));
        }
    }
}
