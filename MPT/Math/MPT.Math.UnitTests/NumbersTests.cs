﻿using System;

using NUnit.Framework;

namespace MPT.Math.UnitTests
{
    [TestFixture]
    public class NumbersTests
    {
        [TestCase(0, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(6, ExpectedResult = true)]
        [TestCase(54, ExpectedResult = true)]
        [TestCase(-2, ExpectedResult = true)]
        [TestCase(-6, ExpectedResult = true)]
        [TestCase(-54, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(-1, ExpectedResult = false)]
        [TestCase(-3, ExpectedResult = false)]
        [TestCase(-5, ExpectedResult = false)]
        public bool IsEven_Int(int number)
        {
            return number.IsEven();
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(6, ExpectedResult = false)]
        [TestCase(54, ExpectedResult = false)]
        [TestCase(-2, ExpectedResult = false)]
        [TestCase(-6, ExpectedResult = false)]
        [TestCase(-54, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(-1, ExpectedResult = true)]
        [TestCase(-3, ExpectedResult = true)]
        [TestCase(-5, ExpectedResult = true)]
        public bool IsOdd_Int(int number)
        {
            return number.IsOdd();
        }

        [TestCase(-1, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = true)]
        public bool IsPositive_Int(int number)
        {
            return number.IsPositive();
        }

        [TestCase(-1, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        public bool IsNegative_Int(int number)
        {
            return number.IsNegative();
        }

        [TestCase(-1, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(-5.31256712, ExpectedResult = false)]
        [TestCase(5.31256712, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, ExpectedResult = false)]
        public bool IsPositive_Double_Default_Tolerance(double number)
        {
            return number.IsPositive();
        }

        [TestCase(-0.001, 0.1, ExpectedResult = false)]
        [TestCase(0, 0.1, ExpectedResult = false)]
        [TestCase(0.001, 0.1, ExpectedResult = false)]
        [TestCase(0.001, 0.0001, ExpectedResult = true)]
        [TestCase(0.001, -0.0001, ExpectedResult = true)]
        public bool IsPositive_Double_Custom_Tolerance(double number, double tolerance)
        {
            return number.IsPositive(tolerance);
        }

        [TestCase(-1, ExpectedResult = true)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(-5.31256712, ExpectedResult = true)]
        [TestCase(5.31256712, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, ExpectedResult = true)]
        public bool IsNegative_Double_Default_Tolerance(double number)
        {
            return number.IsNegative();
        }

        [TestCase(-0.001, 0.1, ExpectedResult = false)]
        [TestCase(0, 0.1, ExpectedResult = false)]
        [TestCase(0.001, 0.1, ExpectedResult = false)]
        [TestCase(-0.001, 0.0001, ExpectedResult = true)]
        [TestCase(-0.001, -0.0001, ExpectedResult = true)]
        public bool IsNegative_Double_Custom_Tolerance(double number, double tolerance)
        {
            return number.IsNegative(tolerance);
        }

        [TestCase(0, ExpectedResult = true)]
        [TestCase(0.0001, ExpectedResult = false)]
        [TestCase(-0.0001, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(double.NegativeInfinity, ExpectedResult = false)]
        public bool IsZero_Default_Tolerance(double value)
        {
            return value.IsZero();
        }

        [TestCase(0, ExpectedResult = true)]
        [TestCase(0.0001, 0.001, ExpectedResult = true)]
        [TestCase(-0.0001, 0.001, ExpectedResult = true)]
        [TestCase(0.0001, -0.001, ExpectedResult = true)]
        [TestCase(-0.0001, -0.001, ExpectedResult = true)]
        public bool IsZero_Custom_Tolerance(double value, double tolerance)
        {
            return value.IsZero(tolerance);
        }

        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(-1, -1, ExpectedResult = true)]
        [TestCase(-1, 1, ExpectedResult = false)]
        [TestCase(5.6882, 5.6882, ExpectedResult = true)]
        [TestCase(5.6882, 5.6880, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, double.NegativeInfinity, ExpectedResult = false)]
        public bool AreEqual_Default_Tolerance(double value1, double value2)
        {
            return Numbers.AreEqual(value1, value2);
        }

        [TestCase(5.555, 5.554, 0.001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, -0.001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, 0.001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, -0.001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, 0.0001, ExpectedResult = false)]
        [TestCase(-5.555, -5.554, 0.0001, ExpectedResult = false)]
        public bool AreEqual_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return Numbers.AreEqual(value1, value2, tolerance);
        }

        [TestCase(0, 0, ExpectedResult = true)]
        [TestCase(1, 1, ExpectedResult = true)]
        [TestCase(-1, -1, ExpectedResult = true)]
        [TestCase(-1, 1, ExpectedResult = false)]
        [TestCase(5.6882, 5.6882, ExpectedResult = true)]
        [TestCase(5.6882, 5.6880, ExpectedResult = false)]
        [TestCase(double.PositiveInfinity, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(double.NegativeInfinity, double.NegativeInfinity, ExpectedResult = true)]
        [TestCase(double.PositiveInfinity, double.NegativeInfinity, ExpectedResult = false)]
        public bool IsEqualTo_Default_Tolerance(double value1, double value2)
        {
            return value1.IsEqualTo(value2);
        }

        [TestCase(5.555, 5.554, 0.001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, -0.001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, 0.001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, -0.001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, 0.0001, ExpectedResult = false)]
        [TestCase(-5.555, -5.554, 0.0001, ExpectedResult = false)]
        public bool IsEqualTo_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return value1.IsEqualTo(value2, tolerance);
        }

        [TestCase(0, 0, ExpectedResult = false)]
        [TestCase(1, 1, ExpectedResult = false)]
        [TestCase(-1, -1, ExpectedResult = false)]
        [TestCase(-1, -2, ExpectedResult = true)]
        [TestCase(-2, -1, ExpectedResult = false)]
        [TestCase(1, -1, ExpectedResult = true)]
        [TestCase(5.6882, 0, ExpectedResult = true)]
        [TestCase(5.6882, 5.68, ExpectedResult = true)]
        [TestCase(-5.6882, 0, ExpectedResult = false)]
        [TestCase(-5.6882, -5.68, ExpectedResult = false)]
        [TestCase(5.6882, double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(5.6882, double.NegativeInfinity, ExpectedResult = true)]
        [TestCase(-5.6882, double.PositiveInfinity, ExpectedResult = false)]
        [TestCase(-5.6882, double.NegativeInfinity, ExpectedResult = true)]
        public bool IsGreaterThan_Default_Tolerance(double value1, double value2)
        {
            return value1.IsGreaterThan(value2);
        }

        [TestCase(5.555, 5.554, 0.001, ExpectedResult = false)]
        [TestCase(5.555, 5.554, 0.0001, ExpectedResult = true)]
        [TestCase(5.555, 5.554, -0.0001, ExpectedResult = true)]
        [TestCase(-5.554, -5.555, 0.001, ExpectedResult = false)]
        [TestCase(-5.554, -5.555, 0.0001, ExpectedResult = true)]
        [TestCase(-5.554, -5.555, -0.0001, ExpectedResult = true)]
        public bool IsGreaterThan_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return value1.IsGreaterThan(value2, tolerance);
        }

        [TestCase(0, 0, ExpectedResult = false)]
        [TestCase(1, 1, ExpectedResult = false)]
        [TestCase(-1, -1, ExpectedResult = false)]
        [TestCase(-1, -2, ExpectedResult = false)]
        [TestCase(-2, -1, ExpectedResult = true)]
        [TestCase(1, -1, ExpectedResult = false)]
        [TestCase(5.6882, 0, ExpectedResult = false)]
        [TestCase(5.68, 5.6882, ExpectedResult = true)]
        [TestCase(-5.6882, 0, ExpectedResult = true)]
        [TestCase(-5.6882, -5.68, ExpectedResult = true)]
        [TestCase(5.6882, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(5.6882, double.NegativeInfinity, ExpectedResult = false)]
        [TestCase(-5.6882, double.PositiveInfinity, ExpectedResult = true)]
        [TestCase(-5.6882, double.NegativeInfinity, ExpectedResult = false)]
        public bool IsLessThan_Default_Tolerance(double value1, double value2)
        {
            return value1.IsLessThan(value2);
        }

        [TestCase(5.554, 5.555, 0.0001, ExpectedResult = true)]
        [TestCase(5.554, 5.555, -0.0001, ExpectedResult = true)]
        [TestCase(5.554, 5.555, 0.01, ExpectedResult = false)]
        [TestCase(-5.555, -5.554, 0.0001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, -0.0001, ExpectedResult = true)]
        [TestCase(-5.555, -5.554, 0.01, ExpectedResult = false)]
        public bool IsLessThan_Custom_Tolerance(double value1, double value2, double tolerance)
        {
            return value1.IsLessThan(value2, tolerance);
        }

        [TestCase(-1, ExpectedResult = 1)]
        [TestCase(-97, ExpectedResult = 7)]
        [TestCase(3, ExpectedResult = 3)]
        [TestCase(45, ExpectedResult = 5)]
        [TestCase(63, ExpectedResult = 3)]
        public int LastDigit(int number)
        {
            return number.LastDigit();
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 1 * 2)]
        [TestCase(3, ExpectedResult = 1 * 2 * 3)]
        [TestCase(4, ExpectedResult = 1 * 2 *3 * 4)]
        public int Factorial(int number)
        {
            return number.Factorial();
        }

        [TestCase(5, 2, ExpectedResult = 25)]
        [TestCase(5, -2, ExpectedResult = (1D/25))]
        [TestCase(0, 2, ExpectedResult = 0)]
        [TestCase(2, 0, ExpectedResult = 1)]
        [TestCase(0, 0, ExpectedResult = 1)]
        [TestCase(-1, 0, ExpectedResult = 1)]
        public double Pow_Integer(int number, int power)
        {
            return number.Pow(power);
        }

        [TestCase(0, -1)]
        public void Pow_Integer_Throws_Exception(int number, int power)
        {
            Assert.Throws<DivideByZeroException>(() => number.Pow(power));
        }

        [TestCase(5, 2, ExpectedResult = 25)]
        [TestCase(5, -2, ExpectedResult = (1D / 25))]
        [TestCase(0, 2, ExpectedResult = 0)]
        [TestCase(2, 0, ExpectedResult = 1)]
        [TestCase(0, 0, ExpectedResult = 1)]
        [TestCase(-1, 0, ExpectedResult = 1)]
        [TestCase(double.PositiveInfinity, 0, ExpectedResult = 1)]
        [TestCase(double.NegativeInfinity, 0, ExpectedResult = 1)]
        [TestCase(double.PositiveInfinity, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, 2, ExpectedResult = double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, 3, ExpectedResult = double.NegativeInfinity)]
        [TestCase(2, double.PositiveInfinity, ExpectedResult = double.PositiveInfinity)]
        [TestCase(2, double.NegativeInfinity, ExpectedResult = 0)]
        public double Pow_Double(double number, double power)
        {
            return number.Pow(power);
        }

        [TestCase(0, -1)]
        public void Pow_Double_Throws_Exception(double number, double power)
        {
            Assert.Throws<DivideByZeroException>(() => number.Pow(power));
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 4)]
        [TestCase(3, ExpectedResult = 9)]
        public int Squared_Integer(int value)
        {
            return value.Squared();
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 8)]
        [TestCase(3, ExpectedResult = 27)]
        public int Cubed_Integer(int value)
        {
            return value.Cubed();
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 4)]
        [TestCase(3, ExpectedResult = 9)]
        [TestCase(4.4, ExpectedResult = 4.4 * 4.4)]
        [TestCase(double.PositiveInfinity, ExpectedResult = double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, ExpectedResult = double.PositiveInfinity)]
        public double Squared_Double(double value)
        {
            return value.Squared();
        }

        [TestCase(0, ExpectedResult = 0)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 8)]
        [TestCase(3, ExpectedResult = 27)]
        [TestCase(4.4, ExpectedResult = 4.4 * 4.4 * 4.4)]
        [TestCase(double.PositiveInfinity, ExpectedResult = double.PositiveInfinity)]
        [TestCase(double.NegativeInfinity, ExpectedResult = double.NegativeInfinity)]
        public double Cubed_Double(double value)
        {
            return value.Cubed();
        }
    }

    [TestFixture]
    public class NumbersTests_IsPrime
    {

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = true)]
        [TestCase(5, ExpectedResult = true)]
        [TestCase(7, ExpectedResult = true)]
        [TestCase(11, ExpectedResult = true)]
        [TestCase(13, ExpectedResult = true)]
        [TestCase(17, ExpectedResult = true)]
        [TestCase(19, ExpectedResult = true)]
        [TestCase(23, ExpectedResult = true)]
        [TestCase(29, ExpectedResult = true)]
        [TestCase(71, ExpectedResult = true)]
        [TestCase(113, ExpectedResult = true)]
        [TestCase(601, ExpectedResult = true)]
        [TestCase(733, ExpectedResult = true)]
        [TestCase(809, ExpectedResult = true)]
        [TestCase(863, ExpectedResult = true)]
        [TestCase(941, ExpectedResult = true)]
        [TestCase(967, ExpectedResult = true)]
        [TestCase(997, ExpectedResult = true)]
        [TestCase(-2, ExpectedResult = true)]
        [TestCase(-3, ExpectedResult = true)]
        [TestCase(-5, ExpectedResult = true)]
        [TestCase(-7, ExpectedResult = true)]
        [TestCase(-11, ExpectedResult = true)]
        [TestCase(-13, ExpectedResult = true)]
        [TestCase(-17, ExpectedResult = true)]
        [TestCase(-19, ExpectedResult = true)]
        [TestCase(-23, ExpectedResult = true)]
        [TestCase(-29, ExpectedResult = true)]
        [TestCase(-71, ExpectedResult = true)]
        [TestCase(-113, ExpectedResult = true)]
        [TestCase(-601, ExpectedResult = true)]
        [TestCase(-733, ExpectedResult = true)]
        [TestCase(-809, ExpectedResult = true)]
        [TestCase(-863, ExpectedResult = true)]
        [TestCase(-941, ExpectedResult = true)]
        [TestCase(-967, ExpectedResult = true)]
        [TestCase(-997, ExpectedResult = true)]
        public bool IsPrime_Returns_True_for_Prime_Numbers(int number)
        {
            return number.IsPrime();
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(4, ExpectedResult = false)]
        [TestCase(6, ExpectedResult = false)]
        [TestCase(8, ExpectedResult = false)]
        [TestCase(9, ExpectedResult = false)]
        [TestCase(10, ExpectedResult = false)]
        [TestCase(25, ExpectedResult = false)]
        [TestCase(841, ExpectedResult = false)]
        [TestCase(-1, ExpectedResult = false)]
        [TestCase(-4, ExpectedResult = false)]
        [TestCase(-6, ExpectedResult = false)]
        [TestCase(-8, ExpectedResult = false)]
        [TestCase(-9, ExpectedResult = false)]
        [TestCase(-10, ExpectedResult = false)]
        [TestCase(-841, ExpectedResult = false)]
        public bool IsPrime_Returns_False_for_Not_Prime_Numbers(int number)
        {
            return number.IsPrime();
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(3, ExpectedResult = false)]
        [TestCase(5, ExpectedResult = false)]
        [TestCase(7, ExpectedResult = false)]
        [TestCase(11, ExpectedResult = false)]
        [TestCase(13, ExpectedResult = false)]
        [TestCase(17, ExpectedResult = false)]
        [TestCase(19, ExpectedResult = false)]
        [TestCase(23, ExpectedResult = false)]
        [TestCase(29, ExpectedResult = false)]
        [TestCase(71, ExpectedResult = false)]
        [TestCase(113, ExpectedResult = false)]
        [TestCase(601, ExpectedResult = false)]
        [TestCase(733, ExpectedResult = false)]
        [TestCase(809, ExpectedResult = false)]
        [TestCase(863, ExpectedResult = false)]
        [TestCase(941, ExpectedResult = false)]
        [TestCase(967, ExpectedResult = false)]
        [TestCase(997, ExpectedResult = false)]
        [TestCase(-2, ExpectedResult = false)]
        [TestCase(-3, ExpectedResult = false)]
        [TestCase(-5, ExpectedResult = false)]
        [TestCase(-7, ExpectedResult = false)]
        [TestCase(-11, ExpectedResult = false)]
        [TestCase(-13, ExpectedResult = false)]
        [TestCase(-17, ExpectedResult = false)]
        [TestCase(-19, ExpectedResult = false)]
        [TestCase(-23, ExpectedResult = false)]
        [TestCase(-29, ExpectedResult = false)]
        [TestCase(-71, ExpectedResult = false)]
        [TestCase(-113, ExpectedResult = false)]
        [TestCase(-601, ExpectedResult = false)]
        [TestCase(-733, ExpectedResult = false)]
        [TestCase(-809, ExpectedResult = false)]
        [TestCase(-863, ExpectedResult = false)]
        [TestCase(-941, ExpectedResult = false)]
        [TestCase(-967, ExpectedResult = false)]
        [TestCase(-997, ExpectedResult = false)]
        public bool IsComposite_Returns_True_for_Prime_Numbers(int number)
        {
            return number.IsComposite();
        }

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(4, ExpectedResult = true)]
        [TestCase(6, ExpectedResult = true)]
        [TestCase(8, ExpectedResult = true)]
        [TestCase(9, ExpectedResult = true)]
        [TestCase(10, ExpectedResult = true)]
        [TestCase(841, ExpectedResult = true)]
        [TestCase(-1, ExpectedResult = true)]
        [TestCase(-4, ExpectedResult = true)]
        [TestCase(-6, ExpectedResult = true)]
        [TestCase(-8, ExpectedResult = true)]
        [TestCase(-9, ExpectedResult = true)]
        [TestCase(-10, ExpectedResult = true)]
        [TestCase(-841, ExpectedResult = true)]
        public bool IsComposite_Returns_False_for_Not_Prime_Numbers(int number)
        {
            return number.IsComposite();
        }
    }
}
