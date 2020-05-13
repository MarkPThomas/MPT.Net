using System.Collections.Generic;
using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.Components
{
    [TestFixture]
    public class UnitsControllerTests
    {
        #region Initialization, Properties, Overrides
        [Test]
        public void Initialization_Basic_Sets_Defaults()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.That(unitsController.QuickUnitTypes.Count, Is.EqualTo(11));
            Assert.That(unitsController.AllUnitTypes.Count, Is.EqualTo(31));
            Assert.That(unitsController.ShorthandUnitsAvailable.Count, Is.EqualTo(0));
            Assert.That(unitsController.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.None));
            Assert.That(unitsController.TypeShorthand, Is.EqualTo(cUnitsController.eUnitTypeShorthand.None));
            Assert.That(unitsController.Units, Is.EqualTo(new cUnits()));
        }

        [Test]
        public void Clone_Clones_Object()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.That(unitsController.QuickUnitTypes.Count, Is.EqualTo(11));
            Assert.That(unitsController.AllUnitTypes.Count, Is.EqualTo(31));
            Assert.That(unitsController.ShorthandUnitsAvailable.Count, Is.EqualTo(0));
            Assert.That(unitsController.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.None));
            Assert.That(unitsController.TypeShorthand, Is.EqualTo(cUnitsController.eUnitTypeShorthand.None));
            Assert.That(unitsController.Units, Is.EqualTo(new cUnits()));

            unitsController.SetTypeByShorthand(cUnitsController.eUnitTypeShorthand.Power);

            Assert.That(unitsController.QuickUnitTypes.Count, Is.EqualTo(11));
            Assert.That(unitsController.AllUnitTypes.Count, Is.EqualTo(31));
            Assert.That(unitsController.ShorthandUnitsAvailable.Count, Is.EqualTo(5));
            Assert.That(unitsController.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.Power));
            Assert.That(unitsController.TypeShorthand, Is.EqualTo(cUnitsController.eUnitTypeShorthand.None));
            Assert.That(unitsController.Units, Is.EqualTo(new cUnits()));

            object unitsControllerClone = unitsController.Clone();

            Assert.That(unitsControllerClone is cUnitsController);
            cUnitsController unitsControllerCloneCast = (cUnitsController)unitsControllerClone;
            
            Assert.That(unitsControllerCloneCast.QuickUnitTypes.Count, Is.EqualTo(11));
            Assert.That(unitsControllerCloneCast.AllUnitTypes.Count, Is.EqualTo(31));
            Assert.That(unitsControllerCloneCast.ShorthandUnitsAvailable.Count, Is.EqualTo(5));
            Assert.That(unitsControllerCloneCast.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.Power));
            Assert.That(unitsControllerCloneCast.TypeShorthand, Is.EqualTo(cUnitsController.eUnitTypeShorthand.None));
            Assert.That(unitsControllerCloneCast.Units, Is.EqualTo(new cUnits()));
        }

        [Test]
        public void Equals_Compares_Objects_By_Properties_That_Are_Equal_Returns_True()
        {
            cUnitsController unitsController1 = new cUnitsController();

            unitsController1.SetTypeByShorthand(cUnitsController.eUnitTypeShorthand.Power);

            Assert.That(unitsController1.QuickUnitTypes.Count, Is.EqualTo(11));
            Assert.That(unitsController1.AllUnitTypes.Count, Is.EqualTo(31));
            Assert.That(unitsController1.ShorthandUnitsAvailable.Count, Is.EqualTo(5));
            Assert.That(unitsController1.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.Power));
            Assert.That(unitsController1.TypeShorthand, Is.EqualTo(cUnitsController.eUnitTypeShorthand.None));
            Assert.That(unitsController1.Units, Is.EqualTo(new cUnits()));

            cUnitsController unitsController2 = new cUnitsController();

            unitsController2.SetTypeByShorthand(cUnitsController.eUnitTypeShorthand.Power);

            Assert.That(unitsController2.QuickUnitTypes.Count, Is.EqualTo(11));
            Assert.That(unitsController2.AllUnitTypes.Count, Is.EqualTo(31));
            Assert.That(unitsController2.ShorthandUnitsAvailable.Count, Is.EqualTo(5));
            Assert.That(unitsController2.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.Power));
            Assert.That(unitsController2.TypeShorthand, Is.EqualTo(cUnitsController.eUnitTypeShorthand.None));
            Assert.That(unitsController2.Units, Is.EqualTo(new cUnits()));
            
            // Method Under Test
            Assert.That(unitsController1.Equals(unitsController2));
        }

        [Test]
        public void Equals_Compares_Objects_By_Properties_That_Are_Not_Equal_Returns_False()
        {
            cUnitsController unitsController1 = new cUnitsController();

            unitsController1.SetTypeByShorthand(cUnitsController.eUnitTypeShorthand.Power);

            Assert.That(unitsController1.QuickUnitTypes.Count, Is.EqualTo(11));
            Assert.That(unitsController1.AllUnitTypes.Count, Is.EqualTo(31));
            Assert.That(unitsController1.ShorthandUnitsAvailable.Count, Is.EqualTo(5));
            Assert.That(unitsController1.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.Power));
            Assert.That(unitsController1.TypeShorthand, Is.EqualTo(cUnitsController.eUnitTypeShorthand.None));
            Assert.That(unitsController1.Units, Is.EqualTo(new cUnits()));

            cUnitsController unitsController2 = new cUnitsController();

            unitsController2.SetTypeByShorthand(cUnitsController.eUnitTypeShorthand.Work);

            Assert.That(unitsController2.QuickUnitTypes.Count, Is.EqualTo(11));
            Assert.That(unitsController2.AllUnitTypes.Count, Is.EqualTo(31));
            Assert.That(unitsController2.ShorthandUnitsAvailable.Count, Is.EqualTo(5));
            Assert.That(unitsController2.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.Work));
            Assert.That(unitsController2.TypeShorthand, Is.EqualTo(cUnitsController.eUnitTypeShorthand.None));
            Assert.That(unitsController2.Units, Is.EqualTo(new cUnits()));

            // Method Under Test
            Assert.That(!unitsController1.Equals(unitsController2));
        }

        [Test]
        public void QuickUnitTypes_Is_Immutable()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.That(unitsController.QuickUnitTypes.Count, Is.EqualTo(11));

            unitsController.QuickUnitTypes.Add("FooBar");

            Assert.That(unitsController.QuickUnitTypes.Count, Is.EqualTo(11));
            Assert.IsFalse(unitsController.QuickUnitTypes.Contains("FooBar"));
        }

        [Test]
        public void AllUnitTypes_Is_Immutable()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.That(unitsController.AllUnitTypes.Count, Is.EqualTo(31));

            unitsController.AllUnitTypes.Add("FooBar");

            Assert.That(unitsController.AllUnitTypes.Count, Is.EqualTo(31));
            Assert.IsFalse(unitsController.AllUnitTypes.Contains("FooBar"));
        }

        [Test]
        public void ShorthandUnitsAvailable_Is_Immutable()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.That(unitsController.ShorthandUnitsAvailable.Count, Is.EqualTo(0));

            unitsController.ShorthandUnitsAvailable.Add("FooBar");

            Assert.That(unitsController.ShorthandUnitsAvailable.Count, Is.EqualTo(0));
            Assert.IsFalse(unitsController.ShorthandUnitsAvailable.Contains("FooBar"));
        }

        [Test]
        public void Units_Is_Immutable()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.That(unitsController.Units, Is.EqualTo(new cUnits()));
            
            cUnit illegalUnit = new cUnit(eUnitType.Force);
            illegalUnit.SetUnitName("lb");

            cUnits immutableUnits = unitsController.Units;
            immutableUnits.AddUnit(illegalUnit);

            Assert.IsTrue(unitsController.Units.Equals(new cUnits()));
        }
        #endregion

        #region Conversion

        [Test]
        public void ConvertFrom()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.IsTrue(false);
        }

        [Test]
        public void ConvertTo()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.IsTrue(false);
        }
        #endregion
        
        #region Set Methods
        
        [TestCase(null, cUnitsController.eUnitTypeStandard.None)]
        [TestCase("", cUnitsController.eUnitTypeStandard.None)]
        [TestCase(" ", cUnitsController.eUnitTypeStandard.None)]
        [TestCase("Custom", cUnitsController.eUnitTypeStandard.Custom)]
        [TestCase("D/C", cUnitsController.eUnitTypeStandard.D_C)]
        [TestCase("Strain", cUnitsController.eUnitTypeStandard.Strain)]
        [TestCase("Length", cUnitsController.eUnitTypeStandard.Length)]
        [TestCase("Area", cUnitsController.eUnitTypeStandard.Area)]
        [TestCase("Volume", cUnitsController.eUnitTypeStandard.Volume)]
        [TestCase("Displacement", cUnitsController.eUnitTypeStandard.Displacement)]
        [TestCase("Displacement (Rotation)", cUnitsController.eUnitTypeStandard.DisplacementRotational)]
        [TestCase("Velocity", cUnitsController.eUnitTypeStandard.Velocity)]
        [TestCase("Acceleration", cUnitsController.eUnitTypeStandard.Acceleration)]
        [TestCase("Rotation", cUnitsController.eUnitTypeStandard.Rotation)]
        [TestCase("Angular Velocity", cUnitsController.eUnitTypeStandard.AngularVelocity)]
        [TestCase("Angular Acceleration", cUnitsController.eUnitTypeStandard.AngularAcceleration)]
        [TestCase("Mass", cUnitsController.eUnitTypeStandard.Mass)]
        [TestCase("Weight", cUnitsController.eUnitTypeStandard.Weight)]
        [TestCase("Density (Mass)", cUnitsController.eUnitTypeStandard.Density_Mass)]
        [TestCase("Density (Weight)", cUnitsController.eUnitTypeStandard.Density_Weight)]
        [TestCase("Temperature", cUnitsController.eUnitTypeStandard.Temperature)]
        [TestCase("Time", cUnitsController.eUnitTypeStandard.Time)]
        [TestCase("Period", cUnitsController.eUnitTypeStandard.Period)]
        [TestCase("Frequency", cUnitsController.eUnitTypeStandard.Frequency)]
        [TestCase("Force", cUnitsController.eUnitTypeStandard.Force)]
        [TestCase("Moment", cUnitsController.eUnitTypeStandard.Moment)]
        [TestCase("Stress", cUnitsController.eUnitTypeStandard.Stress)]
        [TestCase("Pressure", cUnitsController.eUnitTypeStandard.Pressure)]
        [TestCase("Pressure (Line)", cUnitsController.eUnitTypeStandard.Pressure_Line)]
        [TestCase("Work", cUnitsController.eUnitTypeStandard.Work)]
        [TestCase("Power", cUnitsController.eUnitTypeStandard.Power)]
        [TestCase("Rotational Inertia", cUnitsController.eUnitTypeStandard.RotationalInertia)]
        [TestCase("Section Modulus", cUnitsController.eUnitTypeStandard.SectionModulus)]
        [TestCase("Radius of Gyration", cUnitsController.eUnitTypeStandard.RadiusOfGyration)]
        [TestCase("Unitless", cUnitsController.eUnitTypeStandard.Unitless)]
        public void SetTypeByDescription_Of_Existing_Description_Changes_Type(
            string value, 
            cUnitsController.eUnitTypeStandard result)
        {
            cUnitsController unitsController = new cUnitsController();
            Assert.That(unitsController.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.None));

            unitsController.SetTypeByDescription(value);
            Assert.That(unitsController.Type, Is.EqualTo(result));
        }

        [Test]
        public void SetTypeByDescription_Of_NonExisting_Description_Changes_Type_To_None()
        {
            cUnitsController unitsController = new cUnitsController();
            Assert.That(unitsController.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.None));

            unitsController.SetTypeByDescription("FooBar");

            Assert.That(unitsController.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.None));

            unitsController.SetTypeByDescription("Displacement (Rotation)");
            Assert.That(unitsController.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.DisplacementRotational));

            unitsController.SetTypeByDescription("FooBar");

            Assert.That(unitsController.Type, Is.EqualTo(cUnitsController.eUnitTypeStandard.None));
        }


        [TestCase(cUnitsController.eUnitTypeShorthand.None, cUnitsController.eUnitTypeStandard.None)]
        [TestCase(cUnitsController.eUnitTypeShorthand.PressureOrStress, cUnitsController.eUnitTypeStandard.Stress)]
        [TestCase(cUnitsController.eUnitTypeShorthand.SpeedAngular, cUnitsController.eUnitTypeStandard.Frequency)]
        [TestCase(cUnitsController.eUnitTypeShorthand.ForceLineDistribution, cUnitsController.eUnitTypeStandard.Pressure_Line)]
        [TestCase(cUnitsController.eUnitTypeShorthand.Work, cUnitsController.eUnitTypeStandard.Work)]
        [TestCase(cUnitsController.eUnitTypeShorthand.Power, cUnitsController.eUnitTypeStandard.Power)]
        [TestCase(cUnitsController.eUnitTypeShorthand.Speed, cUnitsController.eUnitTypeStandard.Velocity)]
        public void SetTypeByShorthand_Without_Schema_Types_Defined(
            cUnitsController.eUnitTypeShorthand value, 
            cUnitsController.eUnitTypeStandard result)
        {
            cUnitsController unitsController = new cUnitsController();
            unitsController.SetTypeByShorthand(value);
            Assert.That(unitsController.Type, Is.EqualTo(result));
        }

        [TestCase(cUnitsController.eUnitTypeShorthand.None, "Displacement (Rotation)", cUnitsController.eUnitTypeStandard.DisplacementRotational)]
        [TestCase(cUnitsController.eUnitTypeShorthand.PressureOrStress, "", cUnitsController.eUnitTypeStandard.Stress)]
        [TestCase(cUnitsController.eUnitTypeShorthand.PressureOrStress, "Stress", cUnitsController.eUnitTypeStandard.Stress)]
        [TestCase(cUnitsController.eUnitTypeShorthand.PressureOrStress, "Pressure", cUnitsController.eUnitTypeStandard.Pressure)]
        [TestCase(cUnitsController.eUnitTypeShorthand.PressureOrStress, "Displacement (Rotation)", cUnitsController.eUnitTypeStandard.DisplacementRotational)] // TODO: This should not be allowed
        [TestCase(cUnitsController.eUnitTypeShorthand.SpeedAngular, "", cUnitsController.eUnitTypeStandard.Frequency)]
        [TestCase(cUnitsController.eUnitTypeShorthand.SpeedAngular, "Frequency", cUnitsController.eUnitTypeStandard.Frequency)]
        [TestCase(cUnitsController.eUnitTypeShorthand.SpeedAngular, "Angular Velocity", cUnitsController.eUnitTypeStandard.AngularVelocity)]
        [TestCase(cUnitsController.eUnitTypeShorthand.SpeedAngular, "Displacement (Rotation)", cUnitsController.eUnitTypeStandard.DisplacementRotational)] // TODO: This should not be allowed
        public void SetTypeByShorthand_With_Schema_Types_Defined(
            cUnitsController.eUnitTypeShorthand value, 
            string schema,
            cUnitsController.eUnitTypeStandard result)
        {
            // TODO: Limit schema types to appropriate sub-categories.
            cUnitsController unitsController = new cUnitsController();
            unitsController.SetTypeByShorthand(value, schema);
            Assert.That(unitsController.Type, Is.EqualTo(result));
        }

        [TestCase("lb*ft", ExpectedResult = "lb*ft")]
        [TestCase("ksi", ExpectedResult = "Kip/in^2")]
        public string ParseStringToUnits(string unitsToParse)
        {
            cUnitsController unitsController = new cUnitsController();
            unitsController.ParseStringToUnits(unitsToParse);
            return unitsController.Units.GetUnitsLabel();
        }
        #endregion

        #region Shorthand Units
        [TestCase("lb*ft", ExpectedResult = "")]
        [TestCase("ksi", ExpectedResult = "Kip/in^2")]
        public string ParseStringToShorthandUnits(string unitsToParse)
        {
            cUnitsController unitsController = new cUnitsController();
            unitsController.ParseStringToShorthandUnits(unitsToParse);
            return unitsController.Units.GetUnitsLabel();
        }


        [TestCase("psf", ExpectedResult = "lb/ft^2")]
        public string AssignShorthandUnits(string shorthandName)
        {
            cUnitsController unitsController = new cUnitsController();
            unitsController.AssignShorthandUnits(shorthandName);
            return unitsController.Units.GetUnitsLabel();
        }


        [TestCase("lb*ft", "", ExpectedResult = "")]
        [TestCase("plf", "", ExpectedResult = "lb/ft")]
        [TestCase("ksi", "", ExpectedResult = "Kip/in^2")]
        [TestCase("J", "", ExpectedResult = "N*m")]
        [TestCase("W", "", ExpectedResult = "(N*m)/sec")]
        [TestCase("mph", "", ExpectedResult = "Mile/hr")]
        [TestCase("Hz", "", ExpectedResult = "cyc/sec")]
        public string AssignShorthandUnits_With_Schema_Type(string shorthandName, string schemaType)
        {
            cUnitsController unitsController = new cUnitsController();
            unitsController.AssignShorthandUnits(shorthandName, schemaType);
            return unitsController.Units.GetUnitsLabel();
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void AssignShorthandUnits_With_Empty_Shorthand_Name_Removes_Shorthand_Units(string shorthandName)
        {
            cUnitsController unitsController = new cUnitsController();
            unitsController.AssignShorthandUnits("lb*ft");
            //Assert.That(unitsController.Units.GetUnitsLabel(), Is.EqualTo("lb*ft"));
            
            unitsController.AssignShorthandUnits(shorthandName);
            Assert.That(unitsController.Units.GetUnitsLabel(), Is.EqualTo(""));
        }


        [Test]
        public void RemoveShorthandUnits()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.IsTrue(false);
        }

        #endregion

        #region Query
        [Test]
        public void GetShorthandNamesList_Static_No_Shorthand_Returns_Empty_List()
        {
            List<string> shorthandNamesList = cUnitsController.GetShorthandNamesList(cUnitsController.eUnitTypeShorthand.None);

            Assert.That(shorthandNamesList.Count,Is.EqualTo(0));
        }

        [Test]
        public void GetShorthandNamesList_Static_ForceLineDistribution_Returns_ForceLineDistribution_List()
        {
            List<string> shorthandNamesList = cUnitsController.GetShorthandNamesList(cUnitsController.eUnitTypeShorthand.ForceLineDistribution);

            Assert.That(shorthandNamesList[0], Is.EqualTo(""));
            Assert.That(shorthandNamesList[1], Is.EqualTo("plf"));
            Assert.That(shorthandNamesList[2], Is.EqualTo("klf"));
        }


        [Test]
        public void GetShorthandNamesList_Static_PressureOrStress_Returns_PressureOrStress_List()
        {
            List<string> shorthandNamesList = cUnitsController.GetShorthandNamesList(cUnitsController.eUnitTypeShorthand.PressureOrStress);

            Assert.That(shorthandNamesList[0], Is.EqualTo(""));
            Assert.That(shorthandNamesList[1], Is.EqualTo("psi"));
            Assert.That(shorthandNamesList[2], Is.EqualTo("psf"));
            Assert.That(shorthandNamesList[3], Is.EqualTo("ksi"));
            Assert.That(shorthandNamesList[4], Is.EqualTo("ksf"));
            Assert.That(shorthandNamesList[5], Is.EqualTo("Pa"));
            Assert.That(shorthandNamesList[6], Is.EqualTo("kPa"));
            Assert.That(shorthandNamesList[7], Is.EqualTo("MPa"));
            Assert.That(shorthandNamesList[8], Is.EqualTo("GPa"));
        }


        [Test]
        public void GetShorthandNamesList_Static_Work_Returns_Work()
        {
            List<string> shorthandNamesList = cUnitsController.GetShorthandNamesList(cUnitsController.eUnitTypeShorthand.Work);

            Assert.That(shorthandNamesList[0], Is.EqualTo(""));
            Assert.That(shorthandNamesList[1], Is.EqualTo("J"));
            Assert.That(shorthandNamesList[2], Is.EqualTo("kJ"));
            Assert.That(shorthandNamesList[3], Is.EqualTo("MJ"));
            Assert.That(shorthandNamesList[4], Is.EqualTo("GJ"));
        }


        [Test]
        public void GetShorthandNamesList_Static_Power_Returns_Power_List()
        {
            List<string> shorthandNamesList = cUnitsController.GetShorthandNamesList(cUnitsController.eUnitTypeShorthand.Power);

            Assert.That(shorthandNamesList[0], Is.EqualTo(""));
            Assert.That(shorthandNamesList[1], Is.EqualTo("W"));
            Assert.That(shorthandNamesList[2], Is.EqualTo("kW"));
            Assert.That(shorthandNamesList[3], Is.EqualTo("MW"));
            Assert.That(shorthandNamesList[4], Is.EqualTo("GW"));
        }


        [Test]
        public void GetShorthandNamesList_Static_Speed_Returns_Speed_List()
        {
            List<string> shorthandNamesList = cUnitsController.GetShorthandNamesList(cUnitsController.eUnitTypeShorthand.Speed);

            Assert.That(shorthandNamesList[0], Is.EqualTo(""));
            Assert.That(shorthandNamesList[1], Is.EqualTo("fps"));
            Assert.That(shorthandNamesList[2], Is.EqualTo("mph"));
            Assert.That(shorthandNamesList[3], Is.EqualTo("kph"));
        }


        [Test]
        public void GetShorthandNamesList_Static_SpeedAngular_Returns_SpeedAngular_List()
        {
            List<string> shorthandNamesList = cUnitsController.GetShorthandNamesList(cUnitsController.eUnitTypeShorthand.SpeedAngular);

            Assert.That(shorthandNamesList[0], Is.EqualTo(""));
            Assert.That(shorthandNamesList[1], Is.EqualTo("1/sec"));
            Assert.That(shorthandNamesList[2], Is.EqualTo("s^-1"));
            Assert.That(shorthandNamesList[3], Is.EqualTo("rpm"));
            Assert.That(shorthandNamesList[4], Is.EqualTo("Hz"));
            Assert.That(shorthandNamesList[5], Is.EqualTo("kHz"));
            Assert.That(shorthandNamesList[6], Is.EqualTo("MHz"));
            Assert.That(shorthandNamesList[7], Is.EqualTo("GHz"));
        }

        [Test]
        public void GetShorthandNamesList()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.IsTrue(false);
        }


        [TestCase(null, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase("", cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(" ", cUnitsController.eUnitTypeShorthand.None)]
        [TestCase("Foobar", cUnitsController.eUnitTypeShorthand.None)]
        [TestCase("plf", cUnitsController.eUnitTypeShorthand.ForceLineDistribution)]
        [TestCase("klf", cUnitsController.eUnitTypeShorthand.ForceLineDistribution)]
        [TestCase("psi", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("psf", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("ksi", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("ksf", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("Pa", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("kPa", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("MPa", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("GPa", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("J", cUnitsController.eUnitTypeShorthand.Work)]
        [TestCase("kJ", cUnitsController.eUnitTypeShorthand.Work)]
        [TestCase("MJ", cUnitsController.eUnitTypeShorthand.Work)]
        [TestCase("GJ", cUnitsController.eUnitTypeShorthand.Work)]
        [TestCase("W", cUnitsController.eUnitTypeShorthand.Power)]
        [TestCase("kW", cUnitsController.eUnitTypeShorthand.Power)]
        [TestCase("MW", cUnitsController.eUnitTypeShorthand.Power)]
        [TestCase("GW", cUnitsController.eUnitTypeShorthand.Power)]
        [TestCase("fps", cUnitsController.eUnitTypeShorthand.Speed)]
        [TestCase("mph", cUnitsController.eUnitTypeShorthand.Speed)]
        [TestCase("kph", cUnitsController.eUnitTypeShorthand.Speed)]
        [TestCase("1/sec", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("s^-1", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("rpm", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("Hz", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("kHz", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("MHz", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("GHz", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        public void GetShorthandTypeByName(string shorthandName,
            cUnitsController.eUnitTypeShorthand expectedResult)
        {
            cUnitsController.eUnitTypeShorthand result = cUnitsController.GetShorthandTypeByName(shorthandName);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase(100, cUnitsController.eUnitTypeShorthand.None)]     // Out of range
        [TestCase(cUnitsController.eUnitTypeStandard.None, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Custom, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.D_C, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Strain, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Length, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Area, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Volume, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Displacement, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.DisplacementRotational, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Acceleration, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Rotation, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.AngularAcceleration, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Mass, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Weight, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Density_Mass, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Density_Weight, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Temperature, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Time, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Period, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Force, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Moment, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.RotationalInertia, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.SectionModulus, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.RadiusOfGyration, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Unitless, cUnitsController.eUnitTypeShorthand.None)]
        [TestCase(cUnitsController.eUnitTypeStandard.Stress, cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase(cUnitsController.eUnitTypeStandard.Pressure, cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase(cUnitsController.eUnitTypeStandard.Pressure_Line, cUnitsController.eUnitTypeShorthand.ForceLineDistribution)]
        [TestCase(cUnitsController.eUnitTypeStandard.Work, cUnitsController.eUnitTypeShorthand.Work)]
        [TestCase(cUnitsController.eUnitTypeStandard.Power, cUnitsController.eUnitTypeShorthand.Power)]
        [TestCase(cUnitsController.eUnitTypeStandard.Frequency, cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase(cUnitsController.eUnitTypeStandard.AngularVelocity, cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase(cUnitsController.eUnitTypeStandard.Velocity, cUnitsController.eUnitTypeShorthand.Speed)]
        public void GetShorthandTypeByUnitsType_Static(
            cUnitsController.eUnitTypeStandard value,
            cUnitsController.eUnitTypeShorthand expectedResult)
        {
            cUnitsController.eUnitTypeShorthand result = cUnitsController.GetShorthandTypeByUnitsType(value);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetShorthandTypeByUnitsType()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.IsTrue(false);
        }

        [TestCase(null)]     
        [TestCase("")]
        [TestCase(" ")]
        [TestCase("custom")]
        [TestCase("kip/kip")]
        [TestCase("in/in")]
        [TestCase("ft")]
        [TestCase("ft^2")]
        [TestCase("ft^3")]
        [TestCase("in^3")]
        [TestCase("in^4")]
        [TestCase("in")]
        [TestCase("ft/sec^2")]
        [TestCase("rad")]
        [TestCase("rad/sec^2")]
        [TestCase("kg")]
        [TestCase("kg*m/sec^2")]
        [TestCase("kg/mm^3")]
        [TestCase("kg*m/sec^2/mm^3")]
        [TestCase("F")]
        [TestCase("sec")]
        [TestCase("kip")]
        public void GetShorthandTypeByUnits_Static_None(string unitsAsString)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(unitsAsString);
            cUnitsController.eUnitTypeShorthand result = cUnitsController.GetShorthandTypeByUnits(units);

            Assert.That(result, Is.EqualTo(cUnitsController.eUnitTypeShorthand.None));
        }

        [TestCase("lb/in^2", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("lb/ft^2", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("kip/in^2", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("kip/ft^2", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("N/m^2", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("kN/m^2", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("MN/m^2", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        [TestCase("GN/m^2", cUnitsController.eUnitTypeShorthand.PressureOrStress)]
        public void GetShorthandTypeByUnits_Static_PressureOrStress(
            string unitsAsString,
            cUnitsController.eUnitTypeShorthand expectedResult)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(unitsAsString);
            cUnitsController.eUnitTypeShorthand result = cUnitsController.GetShorthandTypeByUnits(units);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("lb/ft", cUnitsController.eUnitTypeShorthand.ForceLineDistribution)]
        [TestCase("kip/ft", cUnitsController.eUnitTypeShorthand.ForceLineDistribution)]
        public void GetShorthandTypeByUnits_Static_ForceLineDistribution(
            string unitsAsString,
            cUnitsController.eUnitTypeShorthand expectedResult)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(unitsAsString);
            cUnitsController.eUnitTypeShorthand result = cUnitsController.GetShorthandTypeByUnits(units);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("N*m", cUnitsController.eUnitTypeShorthand.Work)]
        [TestCase("kN*m", cUnitsController.eUnitTypeShorthand.Work)]
        [TestCase("MN*m", cUnitsController.eUnitTypeShorthand.Work)]
        [TestCase("GN*m", cUnitsController.eUnitTypeShorthand.Work)]
        public void GetShorthandTypeByUnits_Static_Work(
            string unitsAsString,
            cUnitsController.eUnitTypeShorthand expectedResult)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(unitsAsString);
            cUnitsController.eUnitTypeShorthand result = cUnitsController.GetShorthandTypeByUnits(units);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("N*m/sec", cUnitsController.eUnitTypeShorthand.Power)]
        [TestCase("kN*m/sec", cUnitsController.eUnitTypeShorthand.Power)]
        [TestCase("MN*m/sec", cUnitsController.eUnitTypeShorthand.Power)]
        [TestCase("GN*m/sec", cUnitsController.eUnitTypeShorthand.Power)]
        public void GetShorthandTypeByUnits_Static_Power(
            string unitsAsString,
            cUnitsController.eUnitTypeShorthand expectedResult)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(unitsAsString);
            cUnitsController.eUnitTypeShorthand result = cUnitsController.GetShorthandTypeByUnits(units);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("cyc/sec", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("cyc/min", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("10^3 cyc/sec", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("10^6 cyc/sec", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        [TestCase("10^9 cyc/sec", cUnitsController.eUnitTypeShorthand.SpeedAngular)]
        public void GetShorthandTypeByUnits_Static_SpeedAngular(
            string unitsAsString,
            cUnitsController.eUnitTypeShorthand expectedResult)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(unitsAsString);
            cUnitsController.eUnitTypeShorthand result = cUnitsController.GetShorthandTypeByUnits(units);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [TestCase("ft/sec", cUnitsController.eUnitTypeShorthand.Speed)]
        [TestCase("mile/hr", cUnitsController.eUnitTypeShorthand.Speed)]
        [TestCase("km/hr", cUnitsController.eUnitTypeShorthand.Speed)]
        public void GetShorthandTypeByUnits_Static_Speed(
            string unitsAsString,
            cUnitsController.eUnitTypeShorthand expectedResult)
        {
            cUnits units = new cUnits();
            units.ParseStringToUnits(unitsAsString);
            cUnitsController.eUnitTypeShorthand result = cUnitsController.GetShorthandTypeByUnits(units);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetShorthandTypeByUnits()
        {
            cUnitsController unitsController = new cUnitsController();

            Assert.IsTrue(false);
        }

        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase(" ", false)]
        [TestCase("Custom", false)]
        [TestCase("D/C", false)]
        [TestCase("Strain", false)]
        [TestCase("Length", false)]
        [TestCase("Area", false)]
        [TestCase("Volume", false)]
        [TestCase("Displacement", false)]
        [TestCase("Displacement (Rotation)", false)]
        [TestCase("Velocity", true)]
        [TestCase("Acceleration", false)]
        [TestCase("Rotation", false)]
        [TestCase("Angular Velocity", true)]
        [TestCase("Angular Acceleration", false)]
        [TestCase("Mass", false)]
        [TestCase("Weight", false)]
        [TestCase("Density (Mass)", false)]
        [TestCase("Density (Weight)", false)]
        [TestCase("Temperature", false)]
        [TestCase("Time", false)]
        [TestCase("Period", false)]
        [TestCase("Frequency", true)]
        [TestCase("Force", false)]
        [TestCase("Moment", false)]
        [TestCase("Stress", true)]
        [TestCase("Pressure", true)]
        [TestCase("Pressure (Line)", true)]
        [TestCase("Work", true)]
        [TestCase("Power", true)]
        [TestCase("Rotational Inertia", false)]
        [TestCase("Section Modulus", false)]
        [TestCase("Radius of Gyration", false)]
        [TestCase("Unitless", false)]
        public void IsShorthandTypesAvailable(string value, bool isShorthandTypesAvailable)
        {
            cUnitsController unitsController = new cUnitsController();
            unitsController.SetTypeByDescription(value);

            Assert.That(unitsController.IsShorthandTypesAvailable(), Is.EqualTo(isShorthandTypesAvailable));
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase(" ", ExpectedResult = false)]
        [TestCase("lb", ExpectedResult = false)]
        [TestCase("lb ft", ExpectedResult = false)]
        [TestCase("lb, ft", ExpectedResult = true)]
        public bool IsConsistent(string unitsToCheck)
        {
            return cUnitsController.IsConsistent(unitsToCheck);
        }

        #endregion

        #region Consistent Units
        [Test]
        public void MakeUnitsConsistent()
        {
            cUnitsController unitsController = new cUnitsController();
            unitsController.ParseStringToUnits("lb*kN/in");

            Assert.That(unitsController.Units.GetUnitsLabel(), Is.EqualTo("(lb*kN)/in"));

            string unitsToApply = "lb, ft";
            unitsController.MakeUnitsConsistent(unitsToApply);

            Assert.That(unitsController.Units.GetUnitsLabel(), Is.EqualTo("lb/ft"));
        }

        [Test]
        public void MakeUnitsConsistent_From_List()
        {
            cUnitsController unitsController = new cUnitsController();
            unitsController.ParseStringToUnits("lb*kN/in");

            Assert.That(unitsController.Units.GetUnitsLabel(), Is.EqualTo("(lb*kN)/in"));

            List<string> unitsToApply = new List<string>{"lb", "ft"};
            unitsController.MakeUnitsConsistent(unitsToApply);

            Assert.That(unitsController.Units.GetUnitsLabel(), Is.EqualTo("lb/ft"));
        }

        #endregion
    }
}
