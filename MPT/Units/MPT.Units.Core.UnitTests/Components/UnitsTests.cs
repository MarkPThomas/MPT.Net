using System.Collections.Generic;
using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components
{
    [TestFixture]
    public class UnitsTests
    {
        #region Initialization, Properties, Overrides
        [Test]
        public void Initialization_Basic_Sets_Defaults()
        {
            cUnits units = new cUnits();

            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));
            Assert.That(units.UnitsAll.Count, Is.EqualTo(0));
        }

        [Test]
        public void Get_Set_Properties_Gets_Sets_Properties()
        {
            cUnits units = new cUnits { ShorthandLabel = "UnitsLabel" };

            cUnit unitNumerator11 = new cUnit();
            unitNumerator11.SetUnitName("FooBar");

            cUnit unitNumerator12 = new cUnit();
            unitNumerator12.SetUnitName("kN");

            units.AddUnit(unitNumerator11);
            units.AddUnit(unitNumerator12);

            cUnit unitDenominator11 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator11.SetUnitName("MooNar");

            cUnit unitDenominator12 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator12.SetUnitName("kg");

            units.AddUnit(unitDenominator11);
            units.AddUnit(unitDenominator12);

            Assert.That(units.ShorthandLabel, Is.EqualTo("UnitsLabel"));
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitNumerators[0].Equals(unitNumerator11));
            Assert.That(units.UnitNumerators[1].Equals(unitNumerator12));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators[0].Equals(unitDenominator11));
            Assert.That(units.UnitDenominators[1].Equals(unitDenominator12));
            Assert.That(units.UnitsAll.Count, Is.EqualTo(4));
            Assert.That(units.UnitsAll[0].Equals(unitNumerator11));
            Assert.That(units.UnitsAll[1].Equals(unitNumerator12));
            Assert.That(units.UnitsAll[2].Equals(unitDenominator11));
            Assert.That(units.UnitsAll[3].Equals(unitDenominator12));
        }

        [Test]
        public void Clone_Clones_Object()
        {
            cUnits unitsOriginal = new cUnits { ShorthandLabel = "UnitsLabel" };
            Assert.That(unitsOriginal.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(unitsOriginal.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit();
            unitNumerator2.SetUnitName("kN");

            unitsOriginal.AddUnit(unitNumerator);
            unitsOriginal.AddUnit(unitNumerator2);
            Assert.That(unitsOriginal.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(unitsOriginal.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");

            cUnit unitDenominator2 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator2.SetUnitName("kg");

            unitsOriginal.AddUnit(unitDenominator);
            unitsOriginal.AddUnit(unitDenominator2);
            Assert.That(unitsOriginal.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(unitsOriginal.UnitDenominators.Count, Is.EqualTo(2));

            object unitsClone = unitsOriginal.Clone();

            Assert.That(unitsClone is cUnits);
            cUnits unitsCloneCast = (cUnits)unitsClone;

            Assert.That(unitsCloneCast.ShorthandLabel, Is.EqualTo("UnitsLabel"));
            Assert.That(unitsCloneCast.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(unitsCloneCast.UnitNumerators[0].Equals(unitNumerator));
            Assert.That(unitsCloneCast.UnitNumerators[1].Equals(unitNumerator2));
            Assert.That(unitsCloneCast.UnitDenominators.Count, Is.EqualTo(2));
            Assert.That(unitsCloneCast.UnitDenominators[0].Equals(unitDenominator));
            Assert.That(unitsCloneCast.UnitDenominators[1].Equals(unitDenominator2));
        }

        [Test]
        public void Equals_Compares_Objects_By_Properties_That_Are_Equal_Returns_True()
        {
            cUnits units1 = new cUnits { ShorthandLabel = "UnitsLabel" };
            Assert.That(units1.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units1.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator11 = new cUnit();
            unitNumerator11.SetUnitName("FooBar");

            cUnit unitNumerator12 = new cUnit();
            unitNumerator12.SetUnitName("kN");

            units1.AddUnit(unitNumerator11);
            units1.AddUnit(unitNumerator12);
            Assert.That(units1.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units1.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator11 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator11.SetUnitName("MooNar");

            cUnit unitDenominator12 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator12.SetUnitName("kg");

            units1.AddUnit(unitDenominator11);
            units1.AddUnit(unitDenominator12);
            Assert.That(units1.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units1.UnitDenominators.Count, Is.EqualTo(2));

            cUnits units2 = new cUnits { ShorthandLabel = "UnitsLabel" };
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator21 = new cUnit();
            unitNumerator21.SetUnitName("FooBar");

            cUnit unitNumerator22 = new cUnit();
            unitNumerator22.SetUnitName("kN");

            units2.AddUnit(unitNumerator21);
            units2.AddUnit(unitNumerator22);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator21 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator21.SetUnitName("MooNar");

            cUnit unitDenominator22 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator22.SetUnitName("kg");

            units2.AddUnit(unitDenominator21);
            units2.AddUnit(unitDenominator22);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(2));

            // Method Under Test
            Assert.That(units1.Equals(units2));
        }

        [Test]
        public void Equals_Compares_Objects_By_Properties_That_Are_Not_Equal_Returns_False()
        {
            cUnits units1 = new cUnits();
            Assert.That(units1.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units1.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator11 = new cUnit();
            unitNumerator11.SetUnitName("FooBar");

            cUnit unitNumerator12 = new cUnit();
            unitNumerator12.SetUnitName("kN");

            units1.AddUnit(unitNumerator11);
            units1.AddUnit(unitNumerator12);
            Assert.That(units1.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units1.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator11 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator11.SetUnitName("MooNar");

            cUnit unitDenominator12 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator12.SetUnitName("kg");

            units1.AddUnit(unitDenominator11);
            units1.AddUnit(unitDenominator12);
            Assert.That(units1.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units1.UnitDenominators.Count, Is.EqualTo(2));

            cUnits units2 = new cUnits();
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator21 = new cUnit();
            unitNumerator21.SetUnitName("FooNar");

            cUnit unitNumerator22 = new cUnit();
            unitNumerator22.SetUnitName("kN");

            units2.AddUnit(unitNumerator21);
            units2.AddUnit(unitNumerator22);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator21 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator21.SetUnitName("MooFar");

            cUnit unitDenominator22 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator22.SetUnitName("kg");

            units2.AddUnit(unitDenominator21);
            units2.AddUnit(unitDenominator22);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(2));

            // Method Under Test
            Assert.That(!units1.Equals(units2));
        }

        [Test]
        public void UnitsAllList_Is_Immutable()
        {
            cUnits units = new cUnits();
            cUnit unit1 = new cUnit(eUnitType.Force);
            cUnit unit2 = new cUnit(eUnitType.Length, unitPower: "2");
            cUnit unit3 = new cUnit(eUnitType.Time, unitIsNumerator: false, unitPower: "1/2");
            cUnit unit4 = new cUnit(eUnitType.Mass, unitIsNumerator: false);

            Assert.That(units.UnitsAll.Count, Is.EqualTo(0));

            units.AddUnit(unit1);
            units.AddUnit(unit2);
            units.AddUnit(unit3);
            units.AddUnit(unit4);

            Assert.That(units.UnitsAll.Count, Is.EqualTo(4));

            // Method Under Test
            cUnit invalidUnit = new cUnit(eUnitType.Force);
            units.UnitsAll.Add(invalidUnit);
            Assert.That(units.UnitsAll.Count, Is.EqualTo(4));
            Assert.That(units.UnitsAll[0], Is.EqualTo(unit1));
            Assert.That(units.UnitsAll[1], Is.EqualTo(unit2));
            Assert.That(units.UnitsAll[2], Is.EqualTo(unit3));
            Assert.That(units.UnitsAll[3], Is.EqualTo(unit4));
        }

        [Test]
        public void UnitNumerators_Is_Immutable()
        {
            cUnits units = new cUnits();
            cUnit unit1 = new cUnit(eUnitType.Force);
            cUnit unit2 = new cUnit(eUnitType.Length, unitPower: "2");
            cUnit unit3 = new cUnit(eUnitType.Temperature, unitPower: "3");
            cUnit unit4 = new cUnit(eUnitType.Time, unitIsNumerator: false, unitPower: "1/2");
            cUnit unit5 = new cUnit(eUnitType.Mass, unitIsNumerator: false);

            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));

            units.AddUnit(unit1);
            units.AddUnit(unit2);
            units.AddUnit(unit3);
            units.AddUnit(unit4);
            units.AddUnit(unit5);

            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));

            // Method Under Test
            cUnit invalidUnit = new cUnit(eUnitType.Force);
            units.UnitNumerators.Add(invalidUnit);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitNumerators[0], Is.EqualTo(unit1));
            Assert.That(units.UnitNumerators[1], Is.EqualTo(unit2));
            Assert.That(units.UnitNumerators[2], Is.EqualTo(unit3));
        }

        [Test]
        public void UnitDenominators_Is_Immutable()
        {
            cUnits units = new cUnits();
            cUnit unit1 = new cUnit(eUnitType.Force);
            cUnit unit2 = new cUnit(eUnitType.Length, unitPower: "2");
            cUnit unit3 = new cUnit(eUnitType.Temperature, unitPower: "3");
            cUnit unit4 = new cUnit(eUnitType.Time, unitIsNumerator: false, unitPower: "1/2");
            cUnit unit5 = new cUnit(eUnitType.Mass, unitIsNumerator: false);

            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            units.AddUnit(unit1);
            units.AddUnit(unit2);
            units.AddUnit(unit3);
            units.AddUnit(unit4);
            units.AddUnit(unit5);

            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            // Method Under Test
            cUnit invalidUnit = new cUnit(eUnitType.Force);
            units.UnitDenominators.Add(invalidUnit);
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators[0], Is.EqualTo(unit4));
            Assert.That(units.UnitDenominators[1], Is.EqualTo(unit5));
        }
        #endregion
        
        #region Add/Remove Methods
        [Test]
        public void AddUnit_Adds_Unit()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            // Method Under Test
            units.AddUnit(unitNumerator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");

            units.AddUnit(unitDenominator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));
        }

        [Test]
        public void InsertUnit_Inserts_Unit()
        {
            cUnits units = new cUnits();

            cUnit unitNumerator11 = new cUnit();
            unitNumerator11.SetUnitName("FooBar");

            cUnit unitNumerator12 = new cUnit();
            unitNumerator12.SetUnitName("kN");

            cUnit unitDenominator11 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator11.SetUnitName("MooNar");

            cUnit unitDenominator12 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator12.SetUnitName("kg");

            units.AddUnit(unitNumerator11);
            units.AddUnit(unitNumerator12);

            units.AddUnit(unitDenominator11);
            units.AddUnit(unitDenominator12);

            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitNumerators[0].Equals(unitNumerator11));
            Assert.That(units.UnitNumerators[1].Equals(unitNumerator12));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators[0].Equals(unitDenominator11));
            Assert.That(units.UnitDenominators[1].Equals(unitDenominator12));

            // Method Under Test
            cUnit unitNumerator13 = new cUnit();
            unitNumerator13.SetUnitName("sec");

            cUnit unitDenominator13 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator12.SetUnitName("ft");

            units.AddUnit(unitNumerator13, 1);
            units.AddUnit(unitDenominator13, 2);

            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitNumerators[0].Equals(unitNumerator11));
            Assert.That(units.UnitNumerators[1].Equals(unitNumerator13));
            Assert.That(units.UnitNumerators[2].Equals(unitNumerator12));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators[0].Equals(unitDenominator11));
            Assert.That(units.UnitDenominators[1].Equals(unitDenominator12));
            Assert.That(units.UnitDenominators[2].Equals(unitDenominator13));
        }

        [Test]
        public void RemoveUnit_Removes_Unit()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            units.AddUnit(unitNumerator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");

            units.AddUnit(unitDenominator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));

            // Method Under Test
            units.RemoveUnit(unitNumerator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));

            units.RemoveUnit(unitDenominator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveUnit_Removes_Identical_Unit()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            units.AddUnit(unitNumerator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");

            units.AddUnit(unitDenominator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));

            // Method Under Test
            cUnit newNumerator = new cUnit();
            newNumerator.SetUnitName("FooBar");

            units.RemoveUnit(newNumerator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));

            cUnit newDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            newDenominator.SetUnitName("MooNar");

            units.RemoveUnit(newDenominator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveUnit_Does_Nothing_For_Nonexisting_Unit()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            units.AddUnit(unitNumerator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");

            units.AddUnit(unitDenominator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));

            // Method Under Test
            cUnit newNumerator = new cUnit();
            newNumerator.SetUnitName("FooBar2");

            units.RemoveUnit(newNumerator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));


            cUnit newDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            newDenominator.SetUnitName("MooNar2");

            units.RemoveUnit(newDenominator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));
        }

        [Test]
        public void RemoveUnit_By_Index_Removes_Unit()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit();
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            List<cUnit> numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            List<cUnit> denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));

            // Method Under Test
            units.RemoveUnit(1, isNumerator: true);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator3));

            units.RemoveUnit(0, isNumerator: false);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));

            denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator2));
        }

        [Test]
        public void RemoveUnit_By_Index_Does_Nothing_For_Nonexisting_Index()
        {

            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit();
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            List<cUnit> numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            List<cUnit> denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));

            // Method Under Test
            units.RemoveUnit(3, isNumerator: true);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            units.RemoveUnit(-1, isNumerator: false);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));
        }

        [Test]
        public void RemoveUnit_By_Type_Removes_Unit()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit(eUnitType.Force);
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Length);
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit(eUnitType.Mass);
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            List<cUnit> numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            List<cUnit> denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));

            // Method Under Test
            units.RemoveUnit(eUnitType.Force, isNumerator: true);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator3));

            units.RemoveUnit(eUnitType.Length, isNumerator: false);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));

            denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
        }

        [Test]
        public void RemoveUnit_By_Type_Does_Nothing_For_Nonexisting_Index()
        {

            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit(eUnitType.Force);
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Length);
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit(eUnitType.Mass);
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            List<cUnit> numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            List<cUnit> denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));

            // Method Under Test
            units.RemoveUnit(3, isNumerator: true);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            units.RemoveUnit(-1, isNumerator: false);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));
        }


        [Test]
        public void ReplaceUnitByType_Replaces_Unit_Names_of_Matching_Type()
        {
            string unitNumeratorName1 = "FooBar";
            string unitNumeratorName2 = "FooBar2";
            string unitNumeratorName3 = "FooBar3";
            string unitDenominatorName1 = "MooNar";
            string unitDenominatorName2 = "MooNar2";

            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();  // Type = None
            unitNumerator.SetUnitName(unitNumeratorName1);

            cUnit unitNumerator2 = new cUnit();
            unitNumerator2.SetUnitName(unitNumeratorName2);

            cUnit unitNumerator3 = new cUnit(eUnitType.Force);
            unitNumerator3.SetUnitName(unitNumeratorName3);

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            List<cUnit> numerators = units.UnitNumerators;
            Assert.That(numerators[0].Name, Is.EqualTo(unitNumeratorName1));
            Assert.That(numerators[1].Name, Is.EqualTo(unitNumeratorName2));
            Assert.That(numerators[2].Name, Is.EqualTo(unitNumeratorName3));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName(unitDenominatorName1);
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName(unitDenominatorName2);

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            List<cUnit> denominators = units.UnitDenominators;
            Assert.That(denominators[0].Name, Is.EqualTo(unitDenominatorName1));
            Assert.That(denominators[1].Name, Is.EqualTo(unitDenominatorName2));

            // Method Under Test
            string newNumeratorName = "Numerator";
            cUnit newNumerator = new cUnit();  // Type = None
            newNumerator.SetUnitName(newNumeratorName);

            units.ReplaceUnitByType(newNumerator);

            numerators = units.UnitNumerators;
            Assert.That(numerators[0].Name, Is.EqualTo(newNumeratorName));
            Assert.That(numerators[1].Name, Is.EqualTo(newNumeratorName));
            Assert.That(numerators[2].Name, Is.EqualTo(unitNumeratorName3));

            string newDenominatorName = "Denominator";
            cUnit newDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            newDenominator.SetUnitName(newDenominatorName);

            units.ReplaceUnitByType(newDenominator);

            denominators = units.UnitDenominators;
            Assert.That(denominators[0].Name, Is.EqualTo(newDenominatorName));
            Assert.That(denominators[1].Name, Is.EqualTo(unitDenominatorName2));
        }
        #endregion

        #region Units Methods
        [Test]
        public void GetUnitLabel_All_Empty_Returns_Label_With_Empty()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            Assert.That(units.GetUnitsLabel(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void GetUnitLabel_Numerator_Only_Returns_Label_With_Numerator()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            units.AddUnit(unitNumerator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            Assert.That(units.GetUnitsLabel(), Is.EqualTo("FooBar"));
        }

        [Test]
        public void GetUnitLabel_Denominator_Only_Returns_Label_With_1_Over_Denominator()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");

            units.AddUnit(unitDenominator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));

            Assert.That(units.GetUnitsLabel(), Is.EqualTo("1/MooNar"));
        }

        [Test]
        public void GetUnitLabel_Of_Numerator_And_Denominator_Returns_Label_With_Both()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            units.AddUnit(unitNumerator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");

            units.AddUnit(unitDenominator);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(1));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(1));

            Assert.That(units.GetUnitsLabel(), Is.EqualTo("FooBar/MooNar"));
        }

        [Test]
        public void GetUnitLabel_Of_Multiple_Numerators_And_Denominators_Returns_Label_With_All()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Force, unitPower: "2");
            unitNumerator2.SetUnitName("kN");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");

            cUnit unitDenominator2 = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator2.SetUnitName("kg");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            Assert.That(units.GetUnitsLabel(), Is.EqualTo("(FooBar*kN^2)/(MooNar*kg)"));
        }

        [Test]
        public void GetUnitsList_Returns_List_Of_Units_By_Name()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit(eUnitType.Unitless, unitPower: "2");
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Force, unitPower: "1/2");
            unitNumerator2.SetUnitName("kN");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Unitless, unitIsNumerator: false, unitPower: "2.2");
            unitDenominator.SetUnitName("MooNar");

            cUnit unitDenominator2 = new cUnit(eUnitType.Mass, unitIsNumerator: false, unitPower: "0.2");
            unitDenominator2.SetUnitName("kg");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            // Method Under Test
            List<string> unitsList = units.GetUnitsList();
            Assert.That(unitsList.Count, Is.EqualTo(4));
            Assert.That(unitsList[0], Is.EqualTo("FooBar"));
            Assert.That(unitsList[1], Is.EqualTo("kN"));
            Assert.That(unitsList[2], Is.EqualTo("MooNar"));
            Assert.That(unitsList[3], Is.EqualTo("kg"));
        }

        [Test]
        public void GetUnitsList_With_Powers_Returns_List_Of_Units_By_Name_With_Powers()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit(eUnitType.Unitless, unitPower: "2");
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Force, unitPower: "1/2");
            unitNumerator2.SetUnitName("kN");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Unitless, unitIsNumerator: false, unitPower: "2.2");
            unitDenominator.SetUnitName("MooNar");

            cUnit unitDenominator2 = new cUnit(eUnitType.Mass, unitIsNumerator: false, unitPower: "1");
            unitDenominator2.SetUnitName("kg");

            cUnit unitDenominator3 = new cUnit(eUnitType.Mass, unitIsNumerator: false, unitPower: "0");
            unitDenominator3.SetUnitName("lbm");

            cUnit unitDenominator4 = new cUnit(eUnitType.Time, unitIsNumerator: false, unitPower: "-2");
            unitDenominator4.SetUnitName("hr");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            units.AddUnit(unitDenominator3);
            units.AddUnit(unitDenominator4);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(4));

            // Method Under Test
            List<string> unitsList = units.GetUnitsList(withPowers: true);
            Assert.That(unitsList.Count, Is.EqualTo(6));
            Assert.That(unitsList[0], Is.EqualTo("FooBar^2"));
            Assert.That(unitsList[1], Is.EqualTo("kN^(1/2)"));
            Assert.That(unitsList[2], Is.EqualTo("1/MooNar^2.2"));
            Assert.That(unitsList[3], Is.EqualTo("1/kg"));
            Assert.That(unitsList[4], Is.EqualTo(""));   // 1/lbm^0       ^0 is not simplified
            Assert.That(unitsList[5], Is.EqualTo("hr^2"));      // 1/hr^2  sign of -2 power is being ignored
        }
        #endregion

        #region Schema Methods
        [Test]
        public void GetSchemaLabel()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit();
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            List<cUnit> numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            List<cUnit> denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));

            // Method Under Test
            Assert.That(units.GetSchemaLabel(), Is.EqualTo("()/(Force*Length)"));
        }

        [Test]
        public void GetSchemaList()
        {
            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Mass);
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            List<cUnit> numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            List<cUnit> denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));

            // Method Under Test
            List<string> schemaList = units.GetSchemaList();
            Assert.That(schemaList.Count, Is.EqualTo(5));
            Assert.That(schemaList[0], Is.EqualTo(""));
            Assert.That(schemaList[1], Is.EqualTo("Mass"));
            Assert.That(schemaList[2], Is.EqualTo(""));
            Assert.That(schemaList[3], Is.EqualTo("1/Force"));
            Assert.That(schemaList[4], Is.EqualTo("1/Length"));
        }

        [Test]
        public void SchemasMatch_Different_Numerator_Count_Returns_False()
        {

            cUnits units = new cUnits();
            cUnits units2 = new cUnits();

            cUnit unitNumerator = new cUnit(eUnitType.Unitless);
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Mass);
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            units2.AddUnit(unitNumerator);
            units2.AddUnit(unitNumerator2);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            units2.AddUnit(unitDenominator);
            units2.AddUnit(unitDenominator2);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(2));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(2));

            // Method Under Test
            Assert.IsFalse(units.SchemasMatch(units2));
        }

        [Test]
        public void SchemasMatch_Different_Numerator_Schema_Returns_False()
        {
            cUnits units = new cUnits();
            cUnits units2 = new cUnits();

            cUnit unitNumerator = new cUnit(eUnitType.Unitless);
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Mass);
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));
            
            cUnit unitNumerator2Different = new cUnit(eUnitType.Temperature);
            unitNumerator2.SetUnitName("FooBar2");

            units2.AddUnit(unitNumerator);
            units2.AddUnit(unitNumerator2Different);
            units2.AddUnit(unitNumerator3);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            units2.AddUnit(unitDenominator);
            units2.AddUnit(unitDenominator2);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(2));

            // Method Under Test
            Assert.IsFalse(units.SchemasMatch(units2));
        }

        [Test]
        public void SchemasMatch_Different_Denominator_Count_Returns_False()
        {

            cUnits units = new cUnits();
            cUnits units2 = new cUnits();

            cUnit unitNumerator = new cUnit(eUnitType.Unitless);
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Mass);
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            units2.AddUnit(unitNumerator);
            units2.AddUnit(unitNumerator2);
            units2.AddUnit(unitNumerator3);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            units2.AddUnit(unitDenominator);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(1));

            // Method Under Test
            Assert.IsFalse(units.SchemasMatch(units2));
        }

        [Test]
        public void SchemasMatch_Different_Denominator_Schema_Returns_False()
        {
            cUnits units = new cUnits();
            cUnits units2 = new cUnits();

            cUnit unitNumerator = new cUnit(eUnitType.Unitless);
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Mass);
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            units2.AddUnit(unitNumerator);
            units2.AddUnit(unitNumerator2);
            units2.AddUnit(unitNumerator3);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));
            
            cUnit unitDenominator2Different = new cUnit(eUnitType.Temperature, unitIsNumerator: false);
            unitDenominator2Different.SetUnitName("FooBar2");

            units2.AddUnit(unitDenominator);
            units2.AddUnit(unitDenominator2Different);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(2));

            // Method Under Test
            Assert.IsFalse(units.SchemasMatch(units2));
        }

        [Test]
        public void SchemasMatch_Same_Numerators_and_Denominators_Returns_True()
        {
            cUnits units = new cUnits();
            cUnits units2 = new cUnits();

            cUnit unitNumerator = new cUnit(eUnitType.Unitless);
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Mass);
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            units2.AddUnit(unitNumerator);
            units2.AddUnit(unitNumerator2);
            units2.AddUnit(unitNumerator3);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(0));

            List<cUnit> numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            units2.AddUnit(unitDenominator);
            units2.AddUnit(unitDenominator2);
            Assert.That(units2.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units2.UnitDenominators.Count, Is.EqualTo(2));

            List<cUnit> denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));

            // Method Under Test
            Assert.IsTrue(units.SchemasMatch(units2));
        }
        #endregion

        #region Query
        [Test]
        public void AreUnitsSet_of_No_Units_Returns_False()
        {
            cUnits units = new cUnits();

            Assert.IsFalse(units.AreUnitsSet());
        }

        [Test]
        public void AreUnitsSet_of_Numerator_Unit_Unnamed_Returns_False()
        {
            cUnits units = new cUnits();
            cUnit numerator = new cUnit();
            cUnit denominator = new cUnit(eUnitType.Length, unitIsNumerator: false);
            denominator.SetUnitName("Foo");

            units.AddUnit(numerator);
            units.AddUnit(denominator);

            Assert.IsFalse(units.AreUnitsSet());
        }

        [Test]
        public void AreUnitsSet_of_Denominator_Unit_Unnamed_Returns_False()
        {
            cUnits units = new cUnits();
            cUnit numerator = new cUnit(eUnitType.Force);
            numerator.SetUnitName("Foo");
            cUnit denominator = new cUnit(eUnitType.None, unitIsNumerator: false);

            units.AddUnit(numerator);
            units.AddUnit(denominator);

            Assert.IsFalse(units.AreUnitsSet());
        }

        [Test]
        public void AreUnitsSet_of_All_Units_Named_Returns_True()
        {
            cUnits units = new cUnits();
            cUnit numerator = new cUnit(eUnitType.Force);
            numerator.SetUnitName("Foo");
            cUnit denominator = new cUnit(eUnitType.Length, unitIsNumerator: false);
            numerator.SetUnitName("Bar");

            units.AddUnit(numerator);
            units.AddUnit(denominator);

            Assert.IsTrue(units.AreUnitsSet());
        }


        [Test]
        public void IsSchemaSet_of_No_Units_Returns_False()
        {
            cUnits units = new cUnits();

            Assert.IsFalse(units.IsSchemaSet());
        }

        [Test]
        public void IsSchemaSet_of_Numerator_Unit_Without_Type_Returns_False()
        {
            cUnits units = new cUnits();
            cUnit numerator = new cUnit();
            cUnit denominator = new cUnit(eUnitType.Length, unitIsNumerator: false);
            denominator.SetUnitName("Foo");

            units.AddUnit(numerator);
            units.AddUnit(denominator);

            Assert.IsFalse(units.IsSchemaSet());
        }

        [Test]
        public void IsSchemaSet_of_Denominator_Unit_Without_Type_Returns_False()
        {
            cUnits units = new cUnits();
            cUnit numerator = new cUnit(eUnitType.Force);
            numerator.SetUnitName("Foo");
            cUnit denominator = new cUnit(eUnitType.None, unitIsNumerator: false);

            units.AddUnit(numerator);
            units.AddUnit(denominator);

            Assert.IsFalse(units.IsSchemaSet());
        }

        [Test]
        public void IsSchemaSet_of_All_Units_With_Type_Returns_True()
        {
            cUnits units = new cUnits();
            cUnit numerator = new cUnit(eUnitType.Force);
            numerator.SetUnitName("Foo");
            cUnit denominator = new cUnit(eUnitType.Length, unitIsNumerator: false);
            numerator.SetUnitName("Bar");

            units.AddUnit(numerator);
            units.AddUnit(denominator);

            Assert.IsTrue(units.IsSchemaSet());
        }
        #endregion

        #region Manipulate/Convert Methods
        [Test]
        public void SwapNumeratorsDenominators()
        {

            cUnits units = new cUnits();
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(0));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            cUnit unitNumerator = new cUnit();
            unitNumerator.SetUnitName("FooBar");

            cUnit unitNumerator2 = new cUnit(eUnitType.Temperature);
            unitNumerator2.SetUnitName("FooBar2");

            cUnit unitNumerator3 = new cUnit();
            unitNumerator3.SetUnitName("FooBar3");

            units.AddUnit(unitNumerator);
            units.AddUnit(unitNumerator2);
            units.AddUnit(unitNumerator3);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(0));

            List<cUnit> numerators = units.UnitNumerators;
            Assert.That(numerators[0], Is.EqualTo(unitNumerator));
            Assert.That(numerators[1], Is.EqualTo(unitNumerator2));
            Assert.That(numerators[2], Is.EqualTo(unitNumerator3));

            cUnit unitDenominator = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unitDenominator.SetUnitName("MooNar");
            cUnit unitDenominator2 = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unitDenominator2.SetUnitName("MooNar2");

            units.AddUnit(unitDenominator);
            units.AddUnit(unitDenominator2);
            Assert.That(units.UnitNumerators.Count, Is.EqualTo(3));
            Assert.That(units.UnitDenominators.Count, Is.EqualTo(2));

            List<cUnit> denominators = units.UnitDenominators;
            Assert.That(denominators[0], Is.EqualTo(unitDenominator));
            Assert.That(denominators[1], Is.EqualTo(unitDenominator2));

            // Method Under Test
            List<cUnit> originalNumerators = units.UnitNumerators;
            List<cUnit> originalDenominators = units.UnitDenominators;

            units.SwapNumeratorsDenominators();

            List<cUnit> swappedNumerators = units.UnitNumerators;
            List<cUnit> swappedDenominators = units.UnitDenominators;

            Assert.That(originalNumerators.Count, Is.EqualTo(swappedDenominators.Count));
            Assert.That(originalNumerators[0], Is.EqualTo(swappedDenominators[0]));
            Assert.That(originalNumerators[1], Is.EqualTo(swappedDenominators[1]));
            Assert.That(originalNumerators[2], Is.EqualTo(swappedDenominators[2]));

            Assert.That(originalDenominators.Count, Is.EqualTo(swappedNumerators.Count));
            Assert.That(originalDenominators[0], Is.EqualTo(swappedNumerators[0]));
            Assert.That(originalDenominators[1], Is.EqualTo(swappedNumerators[1]));
        }

        [Test]
        public void CombineUnits_Default_Combines_Units_and_Returns_Factor_of_One()
        {
            cUnits unitsStress = new cUnits();
            unitsStress.AddUnit(new cUnit(eUnitType.Force));
            unitsStress.AddUnit(new cUnit(eUnitType.Length, unitPower: "2", unitIsNumerator:false));

            cUnits unitsAngularAcceleration = new cUnits();
            unitsAngularAcceleration.AddUnit(new cUnit(eUnitType.Rotation));
            unitsAngularAcceleration.AddUnit(new cUnit(eUnitType.Time, unitPower: "2", unitIsNumerator: false));

            // Method Under Test
            double conversionFactor = unitsStress.CombineUnits(unitsAngularAcceleration);

            Assert.That(conversionFactor, Is.EqualTo(1));
            Assert.That(unitsStress.GetSchemaLabel(), Is.EqualTo("(Force*Rotation)/(Length^2*Time^2)"));
            Assert.That(unitsStress.GetUnitsLabel(useDefaultsIfNotSet: true), Is.EqualTo("(lb*rad)/(in^2*sec^2)"));
        }

        [Test]
        public void CombineUnits_Divide_Units()
        {
            cUnits unitsStress = new cUnits();
            unitsStress.AddUnit(new cUnit(eUnitType.Force));
            unitsStress.AddUnit(new cUnit(eUnitType.Length, unitPower: "2", unitIsNumerator: false));

            cUnits unitsAngularAcceleration = new cUnits();
            unitsAngularAcceleration.AddUnit(new cUnit(eUnitType.Rotation));
            unitsAngularAcceleration.AddUnit(new cUnit(eUnitType.Time, unitPower: "2", unitIsNumerator: false));

            // Method Under Test
            double conversionFactor = unitsStress.CombineUnits(unitsAngularAcceleration, divideUnits: true);

            Assert.That(conversionFactor, Is.EqualTo(1));
            Assert.That(unitsStress.GetSchemaLabel(), Is.EqualTo("(Force*Time^2)/(Length^2*Rotation)"));
            Assert.That(unitsStress.GetUnitsLabel(useDefaultsIfNotSet: true), Is.EqualTo("(lb*sec^2)/(in^2*rad)"));
        }

        [Test]
        public void CombineUnits_Simplify_Units()
        {
            cUnits existingUnits = new cUnits();
            // Force * Force * Mass / Length^2
            existingUnits.AddUnit(new cUnit(eUnitType.Force));
            existingUnits.AddUnit(new cUnit(eUnitType.Length, unitPower: "2", unitIsNumerator: false));
            existingUnits.AddUnit(new cUnit(eUnitType.Force));
            existingUnits.AddUnit(new cUnit(eUnitType.Mass));

            cUnits addedUnits = new cUnits();
            // Rotation * Mass / Time^2
            addedUnits.AddUnit(new cUnit(eUnitType.Rotation));
            addedUnits.AddUnit(new cUnit(eUnitType.Time, unitPower: "2", unitIsNumerator: false));
            addedUnits.AddUnit(new cUnit(eUnitType.Mass));

            // Method Under Test
            double conversionFactor = existingUnits.CombineUnits(addedUnits, simplifyUnits: true);

            Assert.That(conversionFactor, Is.EqualTo(1));
            Assert.That(existingUnits.GetSchemaLabel(), Is.EqualTo("(Force^2*Mass^2*Rotation)/(Length^2*Time^2)"));
            Assert.That(existingUnits.GetUnitsLabel(useDefaultsIfNotSet: true), Is.EqualTo("(lb^2*lbm^2*rad)/(in^2*sec^2)"));
        }
        
        [Test]
        public void CombineUnits_Simplify_Unit_Numerators_with_List()
        {
            List<string> simplifiedUnits = new List<string> { "kN", "deg", "m", "lbm" };

            // Existing units
            cUnits existingUnits = new cUnits();

            // Names match
            cUnit unit1A = new cUnit(eUnitType.Force);
            unit1A.SetUnitName("lb");
            existingUnits.AddUnit(unit1A);

            // Added unit exists in list
            cUnit unit2A = new cUnit(eUnitType.Length);
            unit2A.SetUnitName("ft");
            existingUnits.AddUnit(unit2A);
            
            // Existing unit exists in list
            cUnit unit3A = new cUnit(eUnitType.Rotation);
            unit3A.SetUnitName("deg");
            existingUnits.AddUnit(unit3A);

            // Neither exists in list
            cUnit unit4A = new cUnit(eUnitType.Temperature);
            unit4A.SetUnitName("F");
            existingUnits.AddUnit(unit4A);
            

            // Added units
            cUnits addedUnits = new cUnits();

            // Names match
            cUnit unit1B = new cUnit(eUnitType.Force);
            unit1B.SetUnitName("lb");
            addedUnits.AddUnit(unit1B);
            
            // Added unit exists in list
            cUnit unit2B = new cUnit(eUnitType.Length);
            unit2B.SetUnitName("m");
            addedUnits.AddUnit(unit2B);

            // Existing unit exists in list
            cUnit unit3B = new cUnit(eUnitType.Rotation);
            unit3B.SetUnitName("rad");
            addedUnits.AddUnit(unit3B);

            // Neither exists in list
            cUnit unit4B = new cUnit(eUnitType.Temperature);
            unit4B.SetUnitName("C");
            addedUnits.AddUnit(unit4B);
            
            // Method Under Test
            double conversionFactor = existingUnits.CombineUnits(
                                        addedUnits, 
                                        simplifyUnits: true, 
                                        simplifiedUnitsList: simplifiedUnits);

            Assert.That(conversionFactor, Is.EqualTo(1));
            Assert.That(existingUnits.GetSchemaLabel(), Is.EqualTo("(Force^2*Length^2*Rotation^2*Temperature^2)"));
            Assert.That(existingUnits.GetUnitsLabel(useDefaultsIfNotSet: true), Is.EqualTo("(lb*rad)/(in^2*sec^2)"));
        }

        [Test]
        public void CombineUnits_Simplify_Unit_Denominators_with_List()
        {
            List<string> simplifiedUnits = new List<string> { "kN", "deg", "m", "lbm" };

            // Existing units
            cUnits existingUnits = new cUnits();

            // Names match
            cUnit unit1A = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unit1A.SetUnitName("lb");
            existingUnits.AddUnit(unit1A);

            // Added unit exists in list
            cUnit unit2A = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unit2A.SetUnitName("ft");
            existingUnits.AddUnit(unit2A);

            // Existing unit exists in list
            cUnit unit3A = new cUnit(eUnitType.Rotation, unitIsNumerator: false);
            unit3A.SetUnitName("deg");
            existingUnits.AddUnit(unit3A);

            // Neither exists in list
            cUnit unit4A = new cUnit(eUnitType.Temperature, unitIsNumerator: false);
            unit4A.SetUnitName("F");
            existingUnits.AddUnit(unit4A);

            // Names match, negative power
            cUnit unit5A = new cUnit(eUnitType.Time, unitIsNumerator: false, unitPower: "-3");
            unit5A.SetUnitName("sec");
            existingUnits.AddUnit(unit5A);


            // Added units
            cUnits addedUnits = new cUnits();

            // Names match
            cUnit unit1B = new cUnit(eUnitType.Force);
            unit1B.SetUnitName("lb");
            addedUnits.AddUnit(unit1B);

            // Added unit exists in list
            cUnit unit2B = new cUnit(eUnitType.Length);
            unit2B.SetUnitName("m");
            addedUnits.AddUnit(unit2B);

            // Existing unit exists in list
            cUnit unit3B = new cUnit(eUnitType.Rotation);
            unit3B.SetUnitName("rad");
            addedUnits.AddUnit(unit3B);

            // Neither exists in list
            cUnit unit4B = new cUnit(eUnitType.Temperature);
            unit4B.SetUnitName("C");
            addedUnits.AddUnit(unit4B);

            // Names match, negative power
            // TODO: Not tiggering 'IsPowerDenominator' in simplifyUnitNumeratorDenominator
            cUnit unit5B = new cUnit(eUnitType.Time, unitPower: "-2");
            unit5B.SetUnitName("sec");
            addedUnits.AddUnit(unit5B);

            // Method Under Test
            double conversionFactor = existingUnits.CombineUnits(
                                        addedUnits,
                                        simplifyUnits: true,
                                        simplifiedUnitsList: simplifiedUnits);

            Assert.That(conversionFactor, Is.EqualTo(1));
            Assert.That(existingUnits.GetSchemaLabel(), Is.EqualTo("Time/(Force^2*Length^2*Rotation^2*Temperature^2)"));
            Assert.That(existingUnits.GetUnitsLabel(useDefaultsIfNotSet: true), Is.EqualTo("(lb*rad)/(in^2*sec^2)"));
        }


        [Test]
        public void CombineUnits_Simplify_Unit_Numerators_without_List()
        {
            // Existing units
            cUnits existingUnits = new cUnits();

            // Names match
            cUnit unit1A = new cUnit(eUnitType.Force);
            unit1A.SetUnitName("lb");
            existingUnits.AddUnit(unit1A);

            // Added unit exists in list
            cUnit unit2A = new cUnit(eUnitType.Length);
            unit2A.SetUnitName("ft");
            existingUnits.AddUnit(unit2A);

            // Existing unit exists in list
            cUnit unit3A = new cUnit(eUnitType.Rotation);
            unit3A.SetUnitName("deg");
            existingUnits.AddUnit(unit3A);

            // Neither exists in list
            cUnit unit4A = new cUnit(eUnitType.Temperature);
            unit4A.SetUnitName("F");
            existingUnits.AddUnit(unit4A);


            // Added units
            cUnits addedUnits = new cUnits();

            // Names match
            cUnit unit1B = new cUnit(eUnitType.Force);
            unit1B.SetUnitName("lb");
            addedUnits.AddUnit(unit1B);

            // Added unit exists in list
            cUnit unit2B = new cUnit(eUnitType.Length);
            unit2B.SetUnitName("m");
            addedUnits.AddUnit(unit2B);

            // Existing unit exists in list
            cUnit unit3B = new cUnit(eUnitType.Rotation);
            unit3B.SetUnitName("rad");
            addedUnits.AddUnit(unit3B);

            // Neither exists in list
            cUnit unit4B = new cUnit(eUnitType.Temperature);
            unit4B.SetUnitName("C");
            addedUnits.AddUnit(unit4B);

            // Method Under Test
            double conversionFactor = existingUnits.CombineUnits(
                                        addedUnits,
                                        simplifyUnits: true);

            Assert.That(conversionFactor, Is.EqualTo(1));
            Assert.That(existingUnits.GetSchemaLabel(), Is.EqualTo("(Force^2*Length^2*Rotation^2*Temperature^2)"));
            Assert.That(existingUnits.GetUnitsLabel(useDefaultsIfNotSet: true), Is.EqualTo("(lb*rad)/(in^2*sec^2)"));
        }

        [Test]
        public void CombineUnits_Simplify_Unit_Denominators_without_List()
        {
            // Existing units
            cUnits existingUnits = new cUnits();

            // Names match
            cUnit unit1A = new cUnit(eUnitType.Force, unitIsNumerator: false);
            unit1A.SetUnitName("lb");
            existingUnits.AddUnit(unit1A);

            // Added unit exists in list
            cUnit unit2A = new cUnit(eUnitType.Length, unitIsNumerator: false);
            unit2A.SetUnitName("ft");
            existingUnits.AddUnit(unit2A);

            // Existing unit exists in list
            cUnit unit3A = new cUnit(eUnitType.Rotation, unitIsNumerator: false);
            unit3A.SetUnitName("deg");
            existingUnits.AddUnit(unit3A);

            // Neither exists in list
            cUnit unit4A = new cUnit(eUnitType.Temperature, unitIsNumerator: false);
            unit4A.SetUnitName("F");
            existingUnits.AddUnit(unit4A);

            // Names match, negative power
            cUnit unit5A = new cUnit(eUnitType.Time, unitIsNumerator: false, unitPower: "-3");
            unit5A.SetUnitName("sec");
            existingUnits.AddUnit(unit5A);


            // Added units
            cUnits addedUnits = new cUnits();

            // Names match
            cUnit unit1B = new cUnit(eUnitType.Force);
            unit1B.SetUnitName("lb");
            addedUnits.AddUnit(unit1B);

            // Added unit exists in list
            cUnit unit2B = new cUnit(eUnitType.Length);
            unit2B.SetUnitName("m");
            addedUnits.AddUnit(unit2B);

            // Existing unit exists in list
            cUnit unit3B = new cUnit(eUnitType.Rotation);
            unit3B.SetUnitName("rad");
            addedUnits.AddUnit(unit3B);

            // Neither exists in list
            cUnit unit4B = new cUnit(eUnitType.Temperature);
            unit4B.SetUnitName("C");
            addedUnits.AddUnit(unit4B);

            // Names match, negative power
            // TODO: Not tiggering 'IsPowerDenominator' in simplifyUnitNumeratorDenominator
            cUnit unit5B = new cUnit(eUnitType.Time, unitPower: "-2");
            unit5B.SetUnitName("sec");
            addedUnits.AddUnit(unit5B);

            // Method Under Test
            double conversionFactor = existingUnits.CombineUnits(
                                        addedUnits,
                                        simplifyUnits: true);

            Assert.That(conversionFactor, Is.EqualTo(1));
            Assert.That(existingUnits.GetSchemaLabel(), Is.EqualTo("Time/(Force^2*Length^2*Rotation^2*Temperature^2)"));
            Assert.That(existingUnits.GetUnitsLabel(useDefaultsIfNotSet: true), Is.EqualTo("(lb*rad)/(in^2*sec^2)"));
        }

        [Test]
        public void Convert_Nonmatching_Numerator_Type_Returns_Zero()
        {
            cUnits units1 = new cUnits();
            cUnits units2 = new cUnits();

            cUnit numerator1 = new cUnit(eUnitType.Force);
            cUnit numerator2 = new cUnit(eUnitType.Length);
            cUnit denominator1 = new cUnit(eUnitType.Time, unitIsNumerator: false);
            cUnit denominator2 = new cUnit(eUnitType.Temperature, unitIsNumerator: false);

            units1.AddUnit(numerator1);
            units1.AddUnit(numerator2);
            units1.AddUnit(denominator1);
            units1.AddUnit(denominator2);

            cUnit nonmatchingNumerator1 = new cUnit(eUnitType.Time);

            units2.AddUnit(nonmatchingNumerator1);
            units2.AddUnit(numerator2);
            units2.AddUnit(denominator1);
            units2.AddUnit(denominator2);

            // Method under test
            Assert.That(units1.Convert(units2), Is.EqualTo(0));
        }

        [Test]
        public void Convert_Nonmatching_Denominator_Type_Returns_Zero()
        {
            cUnits units1 = new cUnits();
            cUnits units2 = new cUnits();

            cUnit numerator1 = new cUnit(eUnitType.Force);
            cUnit numerator2 = new cUnit(eUnitType.Length);
            cUnit denominator1 = new cUnit(eUnitType.Time, unitIsNumerator: false);
            cUnit denominator2 = new cUnit(eUnitType.Temperature, unitIsNumerator: false);

            units1.AddUnit(numerator1);
            units1.AddUnit(numerator2);
            units1.AddUnit(denominator1);
            units1.AddUnit(denominator2);

            cUnit nonmatchingDenominator1 = new cUnit(eUnitType.Force, unitIsNumerator: false);

            units2.AddUnit(numerator1);
            units2.AddUnit(numerator2);
            units2.AddUnit(nonmatchingDenominator1);
            units2.AddUnit(denominator2);

            // Method under test
            Assert.That(units1.Convert(units2), Is.EqualTo(0));
        }

        [Test]
        public void Convert_Returns_Conversion_Factor()
        {
            cUnits units1 = new cUnits();
            cUnits units2 = new cUnits();

            cUnit numerator1a = new cUnit(eUnitType.Force);
            numerator1a.SetUnitName("lb");
            cUnit numerator2a = new cUnit(eUnitType.Length, unitPower: "2");
            numerator2a.SetUnitName("ft");
            cUnit denominator1a = new cUnit(eUnitType.Time, unitPower: "3", unitIsNumerator: false);
            denominator1a.SetUnitName("s");
            cUnit denominator2a = new cUnit(eUnitType.Temperature, unitIsNumerator: false);
            denominator2a.SetUnitName("F");

            units1.AddUnit(numerator1a);
            units1.AddUnit(numerator2a);
            units1.AddUnit(denominator1a);
            units1.AddUnit(denominator2a);

            cUnit numerator1b = (cUnit)numerator1a.Clone();
            numerator1b.SetUnitName("kN");
            cUnit numerator2b = (cUnit)numerator2a.Clone();
            numerator2b.SetUnitName("m");
            cUnit denominator1b = (cUnit)denominator1a.Clone();
            denominator1b.SetUnitName("hr");
            cUnit denominator2b = (cUnit)denominator2a.Clone();
            denominator2b.SetUnitName("C");

            units2.AddUnit(numerator1b);
            units2.AddUnit(numerator2b);
            units2.AddUnit(denominator1b);
            units2.AddUnit(denominator2b);

            // Converting from:
            Assert.That(units2.GetUnitsLabel(), Is.EqualTo("(kN*m^2)/(hr^3*C)"));
            // To:
            Assert.That(units1.GetUnitsLabel(), Is.EqualTo("(lb*ft^2)/(sec^3*F)"));
            
            // Method under test
            Assert.That(units1.Convert(units2), Is.EqualTo(1.534476E-09).Within(0.000001));
        }
        
        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase(" ", ExpectedResult = "")]
        [TestCase("lb", ExpectedResult = "lb")]
        [TestCase("lb*in", ExpectedResult = "lb*in")]
        [TestCase("1/lb", ExpectedResult = "1/lb")]
        [TestCase("lb/sec", ExpectedResult = "lb/sec")]
        [TestCase("lb*in/sec", ExpectedResult = "(lb*in)/sec")]
        [TestCase("lb^2*in/sec", ExpectedResult = "(lb^2*in)/sec")]
        [TestCase("lb^2*in^3/sec", ExpectedResult = "(lb^2*in^3)/sec")]
        [TestCase("lb^2*in^3/sec^4", ExpectedResult = "(lb^2*in^3)/sec^4")]
        [TestCase("lb^2*in^3/(sec^4*rad^2)", ExpectedResult = "(lb^2*in^3)/(sec^4*rad^2)")]
        [TestCase("lb^-2", ExpectedResult = "1/lb^2")]
        public string ParseStringToUnits_Parses_String_To_Units(string value)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(value);
            return units.GetUnitsLabel();
        }

        [TestCase(null, "lb", ExpectedResult = "lb")]
        [TestCase("", "lb", ExpectedResult = "lb")]
        [TestCase("lb", "kip", ExpectedResult = "kip")]
        [TestCase("lb", null, ExpectedResult = "lb")]
        [TestCase("lb", "", ExpectedResult = "lb")]
        [TestCase("lb", " ", ExpectedResult = "lb")]
        [TestCase("lb", "ft", ExpectedResult = "ft")]
        [TestCase("lb^2*in^3/sec^4", "ft^5/lbm", ExpectedResult = "ft^5/lbm")]
        public string ParseStringToUnits_On_Existing_Units_Replaces_Existing_Units_With_Parsed_Units_Unless_Null_Or_Empty(string value1, string value2)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(value1);
            units.ParseStringToUnits(value2);
            return units.GetUnitsLabel();
        }

        [TestCase(null, "lb", ExpectedResult = "lb")]
        [TestCase("", "lb", ExpectedResult = "lb")]
        [TestCase("lb", "kip", ExpectedResult = "lb*kip")]
        [TestCase("lb", null, ExpectedResult = "lb")]
        [TestCase("lb", "", ExpectedResult = "lb")]
        [TestCase("lb", " ", ExpectedResult = "lb")]
        [TestCase("lb", "ft", ExpectedResult = "lb*ft")]
        [TestCase("lb", "lb", ExpectedResult = "lb*lb")]
        [TestCase("lb", "in^-2", ExpectedResult = "lb/in^2")]
        [TestCase("lb", "lb^-1", ExpectedResult = "lb/lb")]
        [TestCase("lb^2*in^3/sec^4", "ft^5/lbm", ExpectedResult = "(lb^2*in^3*ft^5)/(sec^4*lbm)")]
        public string ParseStringToUnits_On_Existing_Units_With_AddToExisting_Adds_Parsed_Units_To_Existing_Units(string value1, string value2)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(value1);
            units.ParseStringToUnits(value2, addToExisting: true);
            return units.GetUnitsLabel();
        }
        #endregion
    }
}
