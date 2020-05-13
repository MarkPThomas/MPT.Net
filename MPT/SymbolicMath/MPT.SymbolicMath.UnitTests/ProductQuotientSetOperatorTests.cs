using System;
using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{
    [TestFixture]
    public class ProductQuotientSetOperatorTests
    {
        #region Equality
        [TestCase(null, null, true)]       // Null or empty
        [TestCase(null, "", true)]
        [TestCase(null, "1", false)]
        [TestCase("", "", true)]
        [TestCase("", "1", false)]
        [TestCase("2", "-2", false)]        // Different sign
        [TestCase("2", "2", true)]          // Integer
        [TestCase("-2", "-2", true)]
        [TestCase("0", "0", true)]
        [TestCase("2", "3", false)]
        [TestCase("2/5", "2/5", true)]      // Fraction
        [TestCase("-2/5", "-2/5", true)]
        [TestCase("0/0", "0/0", true)]
        [TestCase("-2/5", "-1/5", false)]
        [TestCase("2.3", "2.3", true)]      // Float
        [TestCase("-2.3", "-2.3", true)]
        [TestCase("0.0", "0.0", true)]
        [TestCase("2.3", "2.4", false)]
        [TestCase("Foo", "Foo", true)]      // Symbolic
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("Foo", "Bar", false)]
        [TestCase("a^b", "a^b", true)]      // Power
        [TestCase("a^b", "a^c", false)]
        [TestCase("a^b", "a", false)]
        [TestCase("2^3", "2^3", true)]
        [TestCase("-2^3", "-2^3", true)]
        [TestCase("2^-3", "2^-3", true)]
        [TestCase("2^3", "2^4", false)]
        [TestCase("2^3", "2", false)]
        [TestCase("1/2", "2/4", false)]     // Simplified
        [TestCase("2^1", "2", true)]
        [TestCase("2^0", "0", false)]
        [TestCase("1.0", "1", false)]       // Mixed Numeric Types
        [TestCase("0.5", "1/2", false)]
        [TestCase("2", "4/2", false)]
        [TestCase("4*2", "4/2", false)]     // Differing operator counts by type
        public void Equals(string value1, string value2, bool expectedEqual)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);

            bool result = set1.Equals(set2);

            Assert.That(result, Is.EqualTo(expectedEqual));
        }

        [TestCase(null, null, true)]       // Null or empty
        [TestCase(null, "", true)]
        [TestCase(null, "1", false)]
        [TestCase("", "", true)]
        [TestCase("", "1", false)]
        [TestCase("2", "-2", false)]        // Different sign
        [TestCase("2", "2", true)]          // Integer
        [TestCase("-2", "-2", true)]
        [TestCase("0", "0", true)]
        [TestCase("2", "3", false)]
        [TestCase("2/5", "2/5", true)]      // Fraction
        [TestCase("-2/5", "-2/5", true)]
        [TestCase("0/0", "0/0", true)]
        [TestCase("-2/5", "-1/5", false)]
        [TestCase("2.3", "2.3", true)]      // Float
        [TestCase("-2.3", "-2.3", true)]
        [TestCase("0.0", "0.0", true)]
        [TestCase("2.3", "2.4", false)]
        [TestCase("Foo", "Foo", true)]      // Symbolic
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("Foo", "Bar", false)]
        [TestCase("a^b", "a^b", true)]      // Power
        [TestCase("a^b", "a^c", false)]
        [TestCase("2^3", "2^3", true)]
        [TestCase("-2^3", "-2^3", true)]
        [TestCase("2^-3", "2^-3", true)]
        [TestCase("2^3", "2^4", false)]
        [TestCase("1/2", "2/4", false)]     // Simplified
        [TestCase("1.0", "1", false)]       // Mixed Numeric Types
        [TestCase("0.5", "1/2", false)]
        [TestCase("2", "4/2", false)]
        public void Equals_Object(string value1, string value2, bool expectedEqual)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            object set2 = new ProductQuotientSet(value2);

            bool result = set1.Equals(set2);

            Assert.That(result, Is.EqualTo(expectedEqual));
        }

        [TestCase(null, null, true)]       // Null or empty
        [TestCase(null, "", true)]
        [TestCase(null, "1", false)]
        [TestCase("", "", true)]
        [TestCase("", "1", false)]
        [TestCase("2", "-2", false)]        // Different sign
        [TestCase("2", "2", true)]          // Integer
        [TestCase("-2", "-2", true)]
        [TestCase("0", "0", true)]
        [TestCase("2", "3", false)]
        [TestCase("2/5", "2/5", true)]      // Fraction
        [TestCase("-2/5", "-2/5", true)]
        [TestCase("0/0", "0/0", true)]
        [TestCase("-2/5", "-1/5", false)]
        [TestCase("2.3", "2.3", true)]      // Float
        [TestCase("-2.3", "-2.3", true)]
        [TestCase("0.0", "0.0", true)]
        [TestCase("2.3", "2.4", false)]
        [TestCase("Foo", "Foo", true)]      // Symbolic
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("Foo", "Bar", false)]
        [TestCase("a^b", "a^b", true)]      // Power
        [TestCase("a^b", "a^c", false)]
        [TestCase("2^3", "2^3", true)]
        [TestCase("-2^3", "-2^3", true)]
        [TestCase("2^-3", "2^-3", true)]
        [TestCase("2^3", "2^4", false)]
        [TestCase("1/2", "2/4", false)]     // Simplified
        [TestCase("1.0", "1", false)]       // Mixed Numeric Types
        [TestCase("0.5", "1/2", false)]
        [TestCase("2", "4/2", false)]
        public void Operator_Equals(string value1, string value2, bool expectedEqual)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);

            bool result = set1 == set2;

            Assert.That(result, Is.EqualTo(expectedEqual));
        }

        [TestCase(null, null, true)]       // Null or empty
        [TestCase(null, "", true)]
        [TestCase(null, "1", false)]
        [TestCase("", "", true)]
        [TestCase("", "1", false)]
        [TestCase("2", "-2", false)]        // Different sign
        [TestCase("2", "2", true)]          // Integer
        [TestCase("-2", "-2", true)]
        [TestCase("0", "0", true)]
        [TestCase("2", "3", false)]
        [TestCase("2/5", "2/5", true)]      // Fraction
        [TestCase("-2/5", "-2/5", true)]
        [TestCase("0/0", "0/0", true)]
        [TestCase("-2/5", "-1/5", false)]
        [TestCase("2.3", "2.3", true)]      // Float
        [TestCase("-2.3", "-2.3", true)]
        [TestCase("0.0", "0.0", true)]
        [TestCase("2.3", "2.4", false)]
        [TestCase("Foo", "Foo", true)]      // Symbolic
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("Foo", "Bar", false)]
        [TestCase("a^b", "a^b", true)]      // Power
        [TestCase("a^b", "a^c", false)]
        [TestCase("2^3", "2^3", true)]
        [TestCase("-2^3", "-2^3", true)]
        [TestCase("2^-3", "2^-3", true)]
        [TestCase("2^3", "2^4", false)]
        [TestCase("1/2", "2/4", false)]     // Simplified
        [TestCase("1.0", "1", false)]       // Mixed Numeric Types
        [TestCase("0.5", "1/2", false)]
        [TestCase("2", "4/2", false)]
        public void Operator_Not_Equals(string value1, string value2, bool expectedEqual)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);

            bool result = set1 != set2;

            Assert.That(result, Is.EqualTo(!expectedEqual));
        }

        [TestCase(null, null, true)]       // Null or empty
        [TestCase(null, "", true)]
        [TestCase(null, "1", false)]
        [TestCase("", "", true)]
        [TestCase("", "1", false)]
        [TestCase("2", "-2", false)]        // Different sign
        [TestCase("2", "2", true)]          // Integer
        [TestCase("-2", "-2", true)]
        [TestCase("0", "0", true)]
        [TestCase("2", "3", false)]
        [TestCase("2/5", "2/5", true)]      // Fraction
        [TestCase("-2/5", "-2/5", true)]
        [TestCase("0/0", "0/0", true)]
        [TestCase("-2/5", "-1/5", false)]
        [TestCase("2.3", "2.3", true)]      // Float
        [TestCase("-2.3", "-2.3", true)]
        [TestCase("0.0", "0.0", true)]
        [TestCase("2.3", "2.4", false)]
        [TestCase("Foo", "Foo", true)]      // Symbolic
        [TestCase("-Foo", "-Foo", true)]
        [TestCase("Foo", "Bar", false)]
        [TestCase("a^b", "a^b", true)]      // Power
        [TestCase("a^b", "a^c", false)]
        [TestCase("2^3", "2^3", true)]
        [TestCase("-2^3", "-2^3", true)]
        [TestCase("2^-3", "2^-3", true)]
        [TestCase("2^3", "2^4", false)]
        [TestCase("1/2", "2/4", false)]     // Simplified
        [TestCase("1.0", "1", false)]       // Mixed Numeric Types
        [TestCase("0.5", "1/2", false)]
        [TestCase("2", "4/2", false)]
        public void GetHashCode_Gets_Hashcode_Of_Members(string value1, string value2, bool expectedEqual)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);

            int hashCode1 = set1.GetHashCode();
            int hashCode2 = set2.GetHashCode();
            bool result = hashCode1 == hashCode2;
            Assert.That(result, Is.EqualTo(expectedEqual));
        }
        #endregion

        #region Null Or Empty
        [Test]
        public void Operator_Plus_Null_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            ProductQuotientSet setNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 + setNull;
            }
                );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull + set1;
            }
            );
        }

        [Test]
        public void Operator_Plus_Empty_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 + setEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty + set1;
            }
            );

            setEmpty = new ProductQuotientSet(new PrimitiveUnit(string.Empty));

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 + setEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty + set1;
            }
            );
        }

        [Test]
        public void Operator_Minus_Null_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            ProductQuotientSet setNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 - setNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull - set1;
            }
            );
        }

        [Test]
        public void Operator_Minus_Empty_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 - setEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty - set1;
            }
            );

            setEmpty = new ProductQuotientSet(new PrimitiveUnit(string.Empty));

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 - setEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty - set1;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Null_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            ProductQuotientSet setNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * setNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull * set1;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Empty_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * setEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty * set1;
            }
            );

            setEmpty = new ProductQuotientSet(new PrimitiveUnit(string.Empty));

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * setEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty * set1;
            }
            );
        }

        [Test]
        public void Operator_Divide_Null_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            ProductQuotientSet setNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / setNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull / set1;
            }
            );
        }

        [Test]
        public void Operator_Divide_Empty_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / setEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty / set1;
            }
            );

            setEmpty = new ProductQuotientSet(new PrimitiveUnit(string.Empty));

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * setEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty * set1;
            }
            );
        }
        #endregion

        #region Numeric
        //[TestCase(10, 5, "15", "15")]
        //[TestCase(5, 5, "10", "10")]
        //[TestCase(-5, -5, "-10", "-10")]
        //[TestCase(5, -5, "0", "0")]
        //[TestCase(-5, 5, "0", "0")]
        //[TestCase(5, 0, "5", "5")]
        //[TestCase(0, 5, "5", "5")]
        //[TestCase(-5, 0, "-5", "-5")]
        //[TestCase(0, -5, "-5", "-5")]
        //[TestCase(0, 0, "0", "0")]
        //public void Operator_Plus_Integer(int value1, int value2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1);

        //    SumDifferenceSet symbolicSet = set1 + value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 + set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1));

        //    symbolicSet = set1 + value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 + set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(2, 3, 5, 2, "33", "33")]
        //[TestCase(2, 3, 25, 0.5, "13", "13")]
        //[TestCase(2, -2, 4, -0.5, "0.75", "0.75")]
        //public void Operator_Plus_Integer_With_Power(int value1, double power1, int value2, double power2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1, new PrimitiveUnit(power1));
        //    ProductQuotientSet set2 = new ProductQuotientSet(value2, new PrimitiveUnit(power2));

        //    SumDifferenceSet symbolicSet = set1 + set2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = set2 + set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1, new PrimitiveUnit(power1)));
        //    set2 = new ProductQuotientSet(new PrimitiveUnit(value2, new PrimitiveUnit(power2)));

        //    symbolicSet = set1 + set2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = set2 + set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(10, 5, "5", "-5")]
        //[TestCase(5, 4, "1", "-1")]
        //[TestCase(-5, -5, "0", "0")]
        //[TestCase(5, -5, "10", "-10")]
        //[TestCase(-5, 5, "-10", "10")]
        //[TestCase(5, 0, "5", "-5")]
        //[TestCase(0, 5, "-5", "5")]
        //[TestCase(-5, 0, "-5", "5")]
        //[TestCase(0, -5, "5", "-5")]
        //[TestCase(0, 0, "0", "0")]
        //public void Operator_Minus_Integer(int value1, int value2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1);

        //    SumDifferenceSet symbolicSet = set1 - value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 - set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1));

        //    symbolicSet = set1 + value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 + set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(2, 3, 5, 2, "-17", "17")]
        //[TestCase(2, 3, 25, 0.5, "3", "-3")]
        //[TestCase(2, -2, 4, -0.5, "-0.25", "0.25")]
        //public void Operator_Minus_Integer_With_Power(int value1, double power1, int value2, double power2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1, new PrimitiveUnit(power1));
        //    ProductQuotientSet set2 = new ProductQuotientSet(value2, new PrimitiveUnit(power2));

        //    SumDifferenceSet symbolicSet = set1 - set2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = set2 - set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1, new PrimitiveUnit(power1)));
        //    set2 = new ProductQuotientSet(new PrimitiveUnit(value2, new PrimitiveUnit(power2)));

        //    symbolicSet = set1 - set2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = set2 - set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(10, 5, "50", "50")]
        //[TestCase(5, 5, "25", "25")]
        //[TestCase(-5, -5, "25", "25")]
        //[TestCase(5, -5, "-25", "-25")]
        //[TestCase(-5, 5, "-25", "-25")]
        //[TestCase(5, 0, "0", "0")]
        //[TestCase(0, 5, "0", "0")]
        //[TestCase(-5, 0, "0", "0")]
        //[TestCase(0, -5, "0", "0")]
        //[TestCase(0, 0, "0", "0")]
        //public void Operator_Multiply_Integer(int value1, int value2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1);

        //    ProductQuotientSet symbolicSet = set1 * value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 * set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1));

        //    symbolicSet = set1 * value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 * set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(2, 3, 5, 2, "200", "200")]
        //[TestCase(2, 3, 25, 0.5, "40", "40")]
        //[TestCase(2, -2, 4, -0.5, "0.125", "0.125")]
        //public void Operator_Multiply_Integer_With_Power(int value1, double power1, int value2, double power2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1, new PrimitiveUnit(power1));
        //    ProductQuotientSet set2 = new ProductQuotientSet(value2, new PrimitiveUnit(power2));

        //    ProductQuotientSet symbolicSet = set1 * set2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = set2 * set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1, new PrimitiveUnit(power1)));
        //    set2 = new ProductQuotientSet(new PrimitiveUnit(value2, new PrimitiveUnit(power2)));

        //    symbolicSet = set1 * set2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = set2 * set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(10, 5, "2", "0.5")]
        //[TestCase(-5, -5, "1", "1")]
        //[TestCase(5, -5, "-1", "-1")]
        //[TestCase(-5, 5, "-1", "-1")]
        //[TestCase(5, 0, "Infinity", "0")]
        //[TestCase(0, 5, "0", "Infinity")]
        //[TestCase(-5, 0, "-Infinity", "0")]
        //[TestCase(0, -5, "0", "-Infinity")]
        //[TestCase(0, 0, "-NaN", "-NaN")]
        //public void Operator_Divide_Integer(int value1, int value2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1);

        //    ProductQuotientSet symbolicSet = set1 / value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 / set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1));

        //    symbolicSet = set1 / value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 / set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(2, 3, 5, 2, "0.32", "3.125")]
        //[TestCase(2, 3, 25, 0.5, "1.6", "0.625")]
        //[TestCase(2, -2, 4, -0.5, "0.5", "2")]
        //public void Operator_Divide_Integer_With_Power(int value1, double power1, int value2, double power2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1, new PrimitiveUnit(power1));
        //    ProductQuotientSet set2 = new ProductQuotientSet(value2, new PrimitiveUnit(power2));

        //    ProductQuotientSet symbolicSet = set1 / set2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = set2 / set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1, new PrimitiveUnit(power1)));
        //    set2 = new ProductQuotientSet(new PrimitiveUnit(value2, new PrimitiveUnit(power2)));

        //    symbolicSet = set1 / set2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = set2 / set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(10.1, 5.1, "15.2", "15.2")]
        //[TestCase(5.1, 5.1, "10.2", "10.2")]
        //[TestCase(-5.1, -5.1, "-10.2", "-10.2")]
        //[TestCase(5.1, -5.1, "0", "0")]
        //[TestCase(-5.1, 5.1, "0", "0")]
        //[TestCase(5.1, 0.0, "5.1", "5.1")]
        //[TestCase(0.0, 5.1, "5.1", "5.1")]
        //[TestCase(-5.1, 0.0, "-5.1", "-5.1")]
        //[TestCase(0.0, -5.1, "-5.1", "-5.1")]
        //[TestCase(0.0, 0.0, "0", "0")]
        //public void Operator_Plus_Double(double value1, double value2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1);

        //    SumDifferenceSet symbolicSet = set1 + value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 + set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1));

        //    symbolicSet = set1 + value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 + set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(10, 5.1, "4.9", "-4.9")]
        //[TestCase(5.1, 5.1, "0", "0")]
        //[TestCase(-5.1, -5.1, "0", "0")]
        //[TestCase(5.1, -5.1, "10.2", "-10.2")]
        //[TestCase(-5.1, 5.1, "-10.2", "10.2")]
        //[TestCase(5.1, 0.0, "5.1", "-5.1")]
        //[TestCase(0.0, 5.1, "-5.1", "5.1")]
        //[TestCase(-5.1, 0.0, "-5.1", "5.1")]
        //[TestCase(0.0, -5.1, "5.1", "-5.1")]
        //[TestCase(0.0, 0.0, "0", "0")]
        //public void Operator_Minus_Double(double value1, double value2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1);

        //    SumDifferenceSet symbolicSet = set1 - value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 - set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1));

        //    symbolicSet = set1 - value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 - set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(10, 5.1, "51", "51")]
        //[TestCase(5.1, 5.1, "26.01", "26.01")]
        //[TestCase(-5.1, -5.1, "26.01", "26.01")]
        //[TestCase(5.1, -5.1, "-26.01", "-26.01")]
        //[TestCase(-5.1, 5.1, "-26.01", "-26.01")]
        //[TestCase(5.1, 0.0, "0", "0")]
        //[TestCase(0.0, 5.1, "0", "0")]
        //[TestCase(-5.1, 0.0, "0", "0")]
        //[TestCase(0.0, -5.1, "0", "0")]
        //[TestCase(0.0, 0.0, "0", "0")]
        //public void Operator_Multiply_Double(double value1, double value2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1);

        //    ProductQuotientSet symbolicSet = set1 * value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 * set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1));

        //    symbolicSet = set1 * value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 * set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase(10, 5, "2", "0.5")]
        //[TestCase(5.1, 5.1, "1", "1")]
        //[TestCase(-5.1, -5.1, "1", "1")]
        //[TestCase(5.1, -5.1, "-1", "-1")]
        //[TestCase(-5.1, 5.1, "-1", "-1")]
        //[TestCase(5.1, 0.0, "Infinity", "0")]
        //[TestCase(0.0, 5.1, "0", "Infinity")]
        //[TestCase(-5.1, 0.0, "-Infinity", "0")]
        //[TestCase(0.0, -5.1, "0", "-Infinity")]
        //[TestCase(0.0, 0.0, "-NaN", "-NaN")]
        //public void Operator_Divide_Double(double value1, double value2, string expectedLabel1, string expectedLabel2)
        //{
        //    ProductQuotientSet set1 = new ProductQuotientSet(value1);

        //    ProductQuotientSet symbolicSet = set1 / value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 / set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));

        //    set1 = new ProductQuotientSet(new PrimitiveUnit(value1));

        //    symbolicSet = set1 / value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 / set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}
        #endregion

        #region Symbolic
        [TestCase("a^n", "a^n", "a^n+a^n")]           // Same bases, same exponents
        [TestCase("A", "A", "A+A")]
        [TestCase("A*B", "A*B", "(A*B)+(A*B)")]
        [TestCase("A/B", "A/B", "(A/B)+(A/B)")]
        [TestCase("a^n", "b^n", "a^n+b^n")]             // Different bases, same exponents
        [TestCase("A", "B", "A+B")]
        [TestCase("A+B", "C+D", "(A+B)+(C+D)")]     
        [TestCase("A-B", "C-D", "(A-B)+(C-D)")]     
        [TestCase("A*B", "C*D", "(A*B)+(C*D)")]
        [TestCase("A/B", "C/D", "(A/B)+(C/D)")]
        [TestCase("A/B", "B/A", "(A/B)+(B/A)")]     
        [TestCase("A/B", "C/(B*D)", "(A/B)+(C/(B*D))")] 
        [TestCase("a^n", "a^m", "a^n+a^m")]     // Same bases, different exponents
        [TestCase("a^n", "b^m", "a^n+b^m")]     // Different bases, different exponents
        public void Operator_Plus_Symbolic(string value1, string value2, string expectedLabel)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);

            SumDifferenceSet symbolicSet = set1 + set2;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase("a^n", "a^n", "a^n-a^n")]           // Same bases, same exponents
        [TestCase("A", "A", "A-A")]
        [TestCase("A*B", "A*B", "(A*B)-(A*B)")]
        [TestCase("A/B", "A/B", "(A/B)-(A/B)")]
        [TestCase("a^n", "b^n", "a^n-b^n")]             // Different bases, same exponents
        [TestCase("A", "B", "A-B")]
        [TestCase("A+B", "C+D", "(A+B)-(C+D)")] 
        [TestCase("A-B", "C-D", "(A-B)-(C-D)")] 
        [TestCase("A*B", "C*D", "(A*B)-(C*D)")]
        [TestCase("A/B", "C/D", "(A/B)-(C/D)")]
        [TestCase("A/B", "B/A", "(A/B)-(B/A)")]     
        [TestCase("A/B", "C/(B*D)", "(A/B)-(C/(B*D))")] 
        [TestCase("a^n", "a^m", "a^n-a^m")]     // Same bases, different exponents
        [TestCase("a^n", "b^m", "a^n-b^m")]     // Different bases, different exponents
        public void Operator_Minus_Symbolic(string value1, string value2, string expectedLabel)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);

            SumDifferenceSet symbolicSet = set1 - set2;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase("a^n", "a^n", "a^n*a^n")]           // Same bases, same exponents  
        [TestCase("A", "A", "A*A")]
        [TestCase("A*B", "A*B", "(A*B)*(A*B)")]
        [TestCase("A/B", "A/B", "(A/B)*(A/B)")]
        [TestCase("a^n", "b^n", "a^n*b^n")]           // Different bases, same exponents  
        [TestCase("A", "B", "A*B")]
        [TestCase("A+B", "C+D", "(A+B)*(C+D)")]     
        [TestCase("A-B", "C-D", "(A-B)*(C-D)")]     
        [TestCase("A*B", "C*D", "(A*B)*(C*D)")]     
        [TestCase("A/B", "C/D", "(A/B)*(C/D)")]     
        [TestCase("A/B", "B/A", "(A/B)*(B/A)")]     
        [TestCase("A/B", "C/(B*D)", "(A/B)*(C/(B*D))")] 
        [TestCase("a^n", "a^m", "a^n*a^m")]           // Same bases, different exponents  
        [TestCase("a^n", "b^m", "a^n*b^m")]           // Different bases, different exponents
        public void Operator_Multiply_Symbolic(string value1, string value2, string expectedLabel)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);

            ProductQuotientSet symbolicSet = set1 * set2;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase("a^n", "a^n", "a^n/a^n")]           // Same bases, same exponents
        [TestCase("A", "A", "A/A")]
        [TestCase("A*B", "A*B", "(A*B)/(A*B)")]
        [TestCase("A/B", "A/B", "(A/B)/(A/B)")]
        [TestCase("a^n", "b^n", "a^n/b^n")]     // Different bases, same exponents  
        [TestCase("A", "B", "A/B")]
        [TestCase("A+B", "C+D", "(A+B)/(C+D)")]
        [TestCase("A-B", "C-D", "(A-B)/(C-D)")]
        [TestCase("A*B", "C*D", "(A*B)/(C*D)")]
        [TestCase("A/B", "C/D", "(A/B)/(C/D)")]     
        [TestCase("A/B", "B/A", "(A/B)/(B/A)")]     
        [TestCase("A/B", "C/(B*D)", "(A/B)/(C/(B*D))")] 
        [TestCase("a^n", "a^m", "a^n/a^m")]     // Same bases, different exponents  
        [TestCase("a^n", "b^m", "a^n/b^m")]     // Different bases, different exponents
        public void Operator_Divide_Symbolic(string value1, string value2, string expectedLabel)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);

            ProductQuotientSet symbolicSet = set1 / set2;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }
        #endregion

        #region Cross-Types String
        [Test]
        public void Operator_Plus_Null_CrossString_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            string stringNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 + stringNull;
            }
                );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = stringNull + set1;
            }
            );

            ProductQuotientSet setNull = null;
            string stringNotNull = "Bar";

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull + stringNotNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = stringNotNull + setNull;
            }
            );
        }

        [Test]
        public void Operator_Plus_Empty_CrossString_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            string stringEmpty = String.Empty;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 + stringEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = stringEmpty + set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            string stringNotEmpty = "Bar";

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty + stringNotEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = stringNotEmpty + setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Minus_Null_CrossString_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            string stringNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 - stringNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = stringNull - set1;
            }
            );

            ProductQuotientSet setNull = null;
            string stringNotNull = "Bar";

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull - stringNotNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = stringNotNull - setNull;
            }
            );
        }

        [Test]
        public void Operator_Minus_Empty_CrossString_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            string stringEmpty = String.Empty;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 - stringEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = stringEmpty - set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            string stringNotEmpty = "Bar";

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty - stringNotEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = stringNotEmpty - setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Multiply_CrossString_Null_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            string stringNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * stringNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = stringNull * set1;
            }
            );

            ProductQuotientSet setNull = null;
            string stringNotNull = "Bar";

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull * stringNotNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = stringNotNull * setNull;
            }
            );
        }

        [Test]
        public void Operator_Multiply_CrossString_Empty_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            string stringEmpty = String.Empty;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * stringEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = stringEmpty * set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            string stringNotEmpty = "Bar";

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty * stringNotEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = stringNotEmpty * setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Divide_CrossString_Null_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            string stringNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / stringNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = stringNull / set1;
            }
            );

            ProductQuotientSet setNull = null;
            string stringNotNull = "Bar";

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull / stringNotNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = stringNotNull / setNull;
            }
            );
        }

        [Test]
        public void Operator_Divide_CrossString_Empty_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            string stringEmpty = String.Empty;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / stringEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = stringEmpty / set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            string stringNotEmpty = "Bar";

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty / stringNotEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = stringNotEmpty / setEmpty;
            }
            );
        }


        [TestCase("a^n", "Foo", "a^n+Foo", "Foo+a^n")]      // Symbolic
        [TestCase("5", "Foo", "5+Foo", "Foo+5")]            // Integer
        [TestCase("5.5", "Foo", "5.5+Foo", "Foo+5.5")]      // Float
        public void Operator_Plus_CrossString(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);

            SumDifferenceSet symbolicSet = set1 + value2;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = value2 + set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n-Foo", "Foo-a^n")]      // Symbolic
        [TestCase("5", "Foo", "5-Foo", "Foo-5")]            // Integer
        [TestCase("5.5", "Foo", "5.5-Foo", "Foo-5.5")]      // Float
        public void Operator_Minus_CrossString(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);

            SumDifferenceSet symbolicSet = set1 - value2;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = value2 - set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n*Foo", "Foo*a^n")]      // Symbolic
        [TestCase("5", "Foo", "5*Foo", "Foo*5")]            // Integer
        [TestCase("5.5", "Foo", "5.5*Foo", "Foo*5.5")]      // Float
        public void Operator_Multiply_CrossString(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);

            ProductQuotientSet symbolicSet = set1 * value2;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = value2 * set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n/Foo", "Foo/a^n")]      // Symbolic
        [TestCase("5", "Foo", "5/Foo", "Foo/5")]            // Integer
        [TestCase("5.5", "Foo", "5.5/Foo", "Foo/5.5")]      // Float
        public void Operator_Divide_CrossString(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);

            ProductQuotientSet symbolicSet = set1 / value2;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = value2 / set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }
        #endregion

        #region Cross-Types Double
        [Test]
        public void Operator_Plus_Null_CrossDouble_Throws_Exception()
        {
            ProductQuotientSet setNull = null;
            double number = 2.5;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull + number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = number + setNull;
            }
            );
        }

        [Test]
        public void Operator_Plus_Empty_CrossDouble_Throws_Exception()
        {
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            double number = 2.5;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty + number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = number + setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Minus_Null_CrossDouble_Throws_Exception()
        {
            ProductQuotientSet setNull = null;
            double number = 2.5;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull - number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = number - setNull;
            }
            );
        }

        [Test]
        public void Operator_Minus_Empty_CrossDouble_Throws_Exception()
        {
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            double number = 2.5;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty - number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = number - setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Null_CrossDouble_Throws_Exception()
        {
            ProductQuotientSet setNull = null;
            double number = 2.5;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull * number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = number * setNull;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Empty_CrossDouble_Throws_Exception()
        {
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            double number = 2.5;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty * number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = number * setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Divide_Null_CrossDouble_Throws_Exception()
        {
            ProductQuotientSet setNull = null;
            double number = 2.5;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull / number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = number / setNull;
            }
            );
        }

        [Test]
        public void Operator_Divide_Empty_CrossDouble_Throws_Exception()
        {
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            double number = 2.5;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty / number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = number / setEmpty;
            }
            );
        }

        //[TestCase("a^n", 5.4, "a^n+5.4", "5.4+a^n")]    // Symbolic
        //[TestCase("5", 5.4, "10.4", "10.4")]            // Integer
        //[TestCase("5.5", 5.4, "10.9", "10.9")]          // Float
        //public void Operator_Plus_CrossDouble(string value1, double value2, string expectedLabel1, string expectedLabel2)
        //{
        //    SumDifferenceSet set1 = new SumDifferenceSet(value1);

        //    SumDifferenceSet symbolicSet = set1 + value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 + set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase("a^n", 5.4, "a^n-5.4", "5.4-a^n")]      // Symbolic
        //[TestCase("5", 5.4, "-0.4", "0.4")]            // Integer
        //[TestCase("5.5", 5.4, "0.0999999999999996", "-0.0999999999999996")]      // Float
        //public void Operator_Minus_CrossDouble(string value1, double value2, string expectedLabel1, string expectedLabel2)
        //{
        //    SumDifferenceSet set1 = new SumDifferenceSet(value1);

        //    SumDifferenceSet symbolicSet = set1 - value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 - set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase("a^n", 5.4, "a^n*5.4", "5.4*a^n")]      // Symbolic
        //[TestCase("5", 5.4, "27", "27")]            // Integer
        //[TestCase("5.5", 5.4, "29.7", "29.7")]      // Float
        //public void Operator_Multiply_CrossDouble(string value1, double value2, string expectedLabel1, string expectedLabel2)
        //{
        //    SumDifferenceSet set1 = new SumDifferenceSet(value1);

        //    ProductQuotientSet symbolicSet = set1 * value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 * set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase("a^n", 5.4, "a^n/5.4", "5.4/a^n")]      // Symbolic
        //[TestCase("5", 5.4, "0.925925925925926", "1.08")]            // Integer
        //[TestCase("5.5", 5.4, "1.01851851851852", "0.981818181818182")]      // Float
        //public void Operator_Divide_CrossDouble(string value1, double value2, string expectedLabel1, string expectedLabel2)
        //{
        //    SumDifferenceSet set1 = new SumDifferenceSet(value1);

        //    ProductQuotientSet symbolicSet = set1 / value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 / set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}
        #endregion

        #region Cross-Types Integer
        [Test]
        public void Operator_Plus_Null_CrossInteger_Throws_Exception()
        {
            ProductQuotientSet setNull = null;
            int number = 2;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull + number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = number + setNull;
            }
            );
        }

        [Test]
        public void Operator_Plus_Empty_CrossInteger_Throws_Exception()
        {
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            int number = 2;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty + number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = number + setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Minus_Null_CrossInteger_Throws_Exception()
        {
            ProductQuotientSet setNull = null;
            int number = 2;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull - number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = number - setNull;
            }
            );
        }

        [Test]
        public void Operator_Minus_Empty_CrossInteger_Throws_Exception()
        {
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            int number = 2;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty - number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = number - setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Null_CrossInteger_Throws_Exception()
        {
            ProductQuotientSet setNull = null;
            int number = 2;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull * number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = number * setNull;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Empty_CrossInteger_Throws_Exception()
        {
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            int number = 2;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty * number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = number * setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Divide_Null_CrossInteger_Throws_Exception()
        {
            ProductQuotientSet setNull = null;
            int number = 2;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull / number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = number / setNull;
            }
            );
        }

        [Test]
        public void Operator_Divide_Empty_CrossInteger_Throws_Exception()
        {
            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            int number = 2;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty / number;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = number / setEmpty;
            }
            );
        }


        //[TestCase("a^n", 5, "a^n+5", "5+a^n")]    // Symbolic
        //[TestCase("5", 5, "10", "10")]            // Integer
        //[TestCase("-5", -5, "-10", "-10")]
        //[TestCase("5", -5, "0", "0")]
        //[TestCase("-5", 5, "0", "0")]
        //[TestCase("5", 0, "5", "5")]
        //[TestCase("0", 5, "5", "5")]
        //[TestCase("-5", 0, "-5", "-5")]
        //[TestCase("0", -5, "-5", "-5")]
        //[TestCase("0", 0, "0", "0")]
        //[TestCase("5.5", 5, "10.5", "10.5")]          // Float
        //public void Operator_Plus_CrossInteger(string value1, int value2, string expectedLabel1, string expectedLabel2)
        //{
        //    SumDifferenceSet set1 = new SumDifferenceSet(value1);

        //    SumDifferenceSet symbolicSet = set1 + value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 + set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase("a^n", 4, "a^n-4", "4-a^n")]      // Symbolic
        //[TestCase("5", 4, "1", "-1")]            // Integer
        //[TestCase("-5", -5, "0", "0")]
        //[TestCase("5", -5, "10", "-10")]
        //[TestCase("-5", 5, "-10", "10")]
        //[TestCase("5", 0, "5", "-5")]
        //[TestCase("0", 5, "-5", "5")]
        //[TestCase("-5", 0, "-5", "5")]
        //[TestCase("0", -5, "5", "-5")]
        //[TestCase("0", 0, "0", "0")]
        //[TestCase("5.5", 4, "1.5", "-1.5")]      // Float
        //public void Operator_Minus_CrossInteger(string value1, int value2, string expectedLabel1, string expectedLabel2)
        //{
        //    SumDifferenceSet set1 = new SumDifferenceSet(value1);

        //    SumDifferenceSet symbolicSet = set1 - value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 - set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase("a^n", 5, "a^n*5", "5*a^n")]      // Symbolic
        //[TestCase("-n", 2, "-2*n", "-2*n")]      
        //[TestCase("5", 5, "25", "25")]            // Integer
        //[TestCase("-5", -5, "25", "25")]
        //[TestCase("5", -5, "-25", "-25")]
        //[TestCase("-5", 5, "-25", "-25")]
        //[TestCase("5", 0, "0", "0")]
        //[TestCase("0", 5, "0", "0")]
        //[TestCase("-5", 0, "0", "0")]
        //[TestCase("0", -5, "0", "0")]
        //[TestCase("0", 0, "0", "0")]
        //[TestCase("5.5", 5, "27.5", "27.5")]      // Float
        //public void Operator_Multiply_CrossInteger(string value1, int value2, string expectedLabel1, string expectedLabel2)
        //{
        //    SumDifferenceSet set1 = new SumDifferenceSet(value1);

        //    ProductQuotientSet symbolicSet = set1 * value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 * set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}

        //[TestCase("a^n", 5, "a^n/5", "5/a^n")]      // Symbolic
        //[TestCase("10", 5, "2", "0.5")]            // Integer
        //[TestCase("-5", -5, "1", "1")]
        //[TestCase("5", -5, "-1", "-1")]
        //[TestCase("-5", 5, "-1", "-1")]
        //[TestCase("5", 0, "Infinity", "0")]
        //[TestCase("0", 5, "0", "Infinity")]
        //[TestCase("-5", 0, "-Infinity", "0")]
        //[TestCase("0", -5, "0", "-Infinity")]
        //[TestCase("0", 0, "-NaN", "-NaN")]
        //[TestCase("5.5", 5, "1.1", "0.909090909090909")]      // Float
        //public void Operator_Divide_CrossInteger(string value1, int value2, string expectedLabel1, string expectedLabel2)
        //{
        //    SumDifferenceSet set1 = new SumDifferenceSet(value1);

        //    ProductQuotientSet symbolicSet = set1 / value2;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

        //    symbolicSet = value2 / set1;

        //    Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
        //    Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        //}
        #endregion

        #region Symbolic: Cross-Types IBase
        [Test]
        public void Operator_Multiply_Null_CrossIBase_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            IBase iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * iBaseNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseNull * set1;
            }
            );

            ProductQuotientSet setNull = null;
            IBase iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull * iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase * setNull;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Empty_CrossIBase_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            IBase iBaseEmpty = new PrimitiveUnit(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseEmpty * set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            IBase iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty * iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase * setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Unsupported_IBase_CrossIBase_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            IBase iBaseUnsupported = new TestBaseUnit();

            Assert.Throws<NotImplementedException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * iBaseUnsupported;
            }
            );
            Assert.Throws<NotImplementedException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseUnsupported * set1;
            }
            );
        }

        [Test]
        public void Operator_Divide_Null_CrossIBase_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            IBase iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / iBaseNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseNull / set1;
            }
            );

            ProductQuotientSet setNull = null;
            IBase iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull / iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase / setNull;
            }
            );
        }

        [Test]
        public void Operator_Divide_Empty_CrossIBase_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            IBase iBaseEmpty = new PrimitiveUnit(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseEmpty / set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            IBase iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty / iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase / setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Divide_Unsupported_IBase_CrossIBase_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            IBase iBaseUnsupported = new TestBaseUnit();

            Assert.Throws<NotImplementedException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / iBaseUnsupported;
            }
            );
            Assert.Throws<NotImplementedException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseUnsupported / set1;
            }
            );
        }

        [TestCase("a^n", "Foo", "a^n*Foo", "Foo*a^n")]      // Symbolic
        [TestCase("5", "Foo", "5*Foo", "Foo*5")]            // Integer
        [TestCase("5.5", "Foo", "5.5*Foo", "Foo*5.5")]      // Float
        public void Operator_Multiply_CrossIBase(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {

            ProductQuotientSet set1 = new ProductQuotientSet(value1);


            IBase setPrimitive = new PrimitiveUnit(value2);

            ProductQuotientSet symbolicSet = set1 * setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive * set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));


            IBase setSumDifference = new SumDifferenceSet(value2);

            symbolicSet = set1 * setSumDifference;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setSumDifference * set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));


            IBase setProductQuotient = new ProductQuotientSet(value2);

            symbolicSet = set1 * setProductQuotient;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setProductQuotient * set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n/Foo", "Foo/a^n")]      // Symbolic
        [TestCase("5", "Foo", "5/Foo", "Foo/5")]            // Integer
        [TestCase("5.5", "Foo", "5.5/Foo", "Foo/5.5")]      // Float
        public void Operator_Divide_CrossIBase(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {

            ProductQuotientSet set1 = new ProductQuotientSet(value1);


            IBase setPrimitive = new PrimitiveUnit(value2);

            ProductQuotientSet symbolicSet = set1 / setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive / set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));


            IBase setSumDifference = new SumDifferenceSet(value2);

            symbolicSet = set1 / setSumDifference;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setSumDifference / set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));


            IBase setProductQuotient = new ProductQuotientSet(value2);

            symbolicSet = set1 / setProductQuotient;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setProductQuotient / set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }
        #endregion

        #region Symbolic: Cross-Types PrimitiveUnit

        [Test]
        public void Operator_Plus_Null_CrossPrimitiveUnit_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            PrimitiveUnit iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 + iBaseNull;
            }
                );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBaseNull + set1;
            }
            );

            ProductQuotientSet setNull = null;
            PrimitiveUnit iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull + iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBase + setNull;
            }
            );
        }

        [Test]
        public void Operator_Plus_Empty_CrossPrimitiveUnit_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            PrimitiveUnit iBaseEmpty = new PrimitiveUnit(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 + iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBaseEmpty + set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            PrimitiveUnit iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty + iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBase + setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Minus_Null_CrossPrimitiveUnit_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            PrimitiveUnit iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 - iBaseNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBaseNull - set1;
            }
            );

            ProductQuotientSet setNull = null;
            PrimitiveUnit iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull - iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBase - setNull;
            }
            );
        }

        [Test]
        public void Operator_Minus_Empty_CrossPrimitiveUnit_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            PrimitiveUnit iBaseEmpty = new PrimitiveUnit(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 - iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBaseEmpty - set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            PrimitiveUnit iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty - iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBase - setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Null_CrossPrimitiveUnit_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            PrimitiveUnit iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * iBaseNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseNull * set1;
            }
            );

            ProductQuotientSet setNull = null;
            PrimitiveUnit iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull * iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase * setNull;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Empty_CrossPrimitiveUnit_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            PrimitiveUnit iBaseEmpty = new PrimitiveUnit(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseEmpty * set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            PrimitiveUnit iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty * iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase * setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Divide_Null_CrossPrimitiveUnit_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            PrimitiveUnit iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / iBaseNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseNull / set1;
            }
            );

            ProductQuotientSet setNull = null;
            PrimitiveUnit iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull / iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase / setNull;
            }
            );
        }

        [Test]
        public void Operator_Divide_Empty_CrossPrimitiveUnit_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            PrimitiveUnit iBaseEmpty = new PrimitiveUnit(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseEmpty / set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            PrimitiveUnit iBase = new PrimitiveUnit("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty / iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase / setEmpty;
            }
            );
        }

        [TestCase("a^n", "Foo", "a^n+Foo", "Foo+a^n")]      // Symbolic
        [TestCase("5", "Foo", "5+Foo", "Foo+5")]            // Integer
        [TestCase("5.5", "Foo", "5.5+Foo", "Foo+5.5")]      // Float
        public void Operator_Plus_CrossPrimitiveUnit(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            
            PrimitiveUnit setPrimitive = new PrimitiveUnit(value2);

            SumDifferenceSet symbolicSet = set1 + setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive + set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n-Foo", "Foo-a^n")]      // Symbolic
        [TestCase("5", "Foo", "5-Foo", "Foo-5")]            // Integer
        [TestCase("5.5", "Foo", "5.5-Foo", "Foo-5.5")]      // Float
        public void Operator_Minus_CrossPrimitiveUnit(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            
            PrimitiveUnit setPrimitive = new PrimitiveUnit(value2);

            SumDifferenceSet symbolicSet = set1 - setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive - set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n*Foo", "Foo*a^n")]      // Symbolic
        [TestCase("5", "Foo", "5*Foo", "Foo*5")]            // Integer
        [TestCase("5.5", "Foo", "5.5*Foo", "Foo*5.5")]      // Float
        public void Operator_Multiply_CrossPrimitiveUnit(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            
            PrimitiveUnit setPrimitive = new PrimitiveUnit(value2);

            ProductQuotientSet symbolicSet = set1 * setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive * set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n/Foo", "Foo/a^n")]      // Symbolic
        [TestCase("5", "Foo", "5/Foo", "Foo/5")]            // Integer
        [TestCase("5.5", "Foo", "5.5/Foo", "Foo/5.5")]      // Float
        public void Operator_Divide_CrossPrimitiveUnit(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            
            PrimitiveUnit setPrimitive = new PrimitiveUnit(value2);

            ProductQuotientSet symbolicSet = set1 / setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive / set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }
        #endregion

        #region Symbolic: Cross-Types SumDifferenceSet

        [Test]
        public void Operator_Plus_Null_CrossSumDifferenceSet_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            SumDifferenceSet iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 + iBaseNull;
            }
                );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBaseNull + set1;
            }
            );

            SumDifferenceSet setNull = null;
            SumDifferenceSet iBase = new SumDifferenceSet("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull + iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBase + setNull;
            }
            );
        }

        [Test]
        public void Operator_Plus_Empty_CrossSumDifferenceSet_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            SumDifferenceSet iBaseEmpty = new SumDifferenceSet(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 + iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBaseEmpty + set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            SumDifferenceSet iBase = new SumDifferenceSet("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty + iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBase + setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Minus_Null_CrossSumDifferenceSet_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            SumDifferenceSet iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 - iBaseNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBaseNull - set1;
            }
            );

            ProductQuotientSet setNull = null;
            SumDifferenceSet iBase = new SumDifferenceSet("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setNull - iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBase - setNull;
            }
            );
        }

        [Test]
        public void Operator_Minus_Empty_CrossSumDifferenceSet_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            SumDifferenceSet iBaseEmpty = new SumDifferenceSet(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = set1 - iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBaseEmpty - set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            SumDifferenceSet iBase = new SumDifferenceSet("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = setEmpty - iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                SumDifferenceSet symbolicSet = iBase - setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Null_CrossSumDifferenceSet_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            SumDifferenceSet iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * iBaseNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseNull * set1;
            }
            );

            ProductQuotientSet setNull = null;
            SumDifferenceSet iBase = new SumDifferenceSet("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull * iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase * setNull;
            }
            );
        }

        [Test]
        public void Operator_Multiply_Empty_CrossSumDifferenceSet_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            SumDifferenceSet iBaseEmpty = new SumDifferenceSet(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 * iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseEmpty * set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            SumDifferenceSet iBase = new SumDifferenceSet("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty * iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase * setEmpty;
            }
            );
        }

        [Test]
        public void Operator_Divide_Null_CrossSumDifferenceSet_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            SumDifferenceSet iBaseNull = null;

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / iBaseNull;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseNull / set1;
            }
            );

            ProductQuotientSet setNull = null;
            SumDifferenceSet iBase = new SumDifferenceSet("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setNull / iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase / setNull;
            }
            );
        }

        [Test]
        public void Operator_Divide_Empty_CrossSumDifferenceSet_Throws_Exception()
        {
            ProductQuotientSet set1 = new ProductQuotientSet("Foo");
            SumDifferenceSet iBaseEmpty = new SumDifferenceSet(string.Empty);

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = set1 / iBaseEmpty;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBaseEmpty / set1;
            }
            );

            ProductQuotientSet setEmpty = new ProductQuotientSet(string.Empty);
            SumDifferenceSet iBase = new SumDifferenceSet("Foo");

            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = setEmpty / iBase;
            }
            );
            Assert.Throws<ArgumentNullException>(() =>
            {
                ProductQuotientSet symbolicSet = iBase / setEmpty;
            }
            );
        }

        [TestCase("a^n", "Foo", "a^n+Foo", "Foo+a^n")]      // Symbolic
        [TestCase("5", "Foo", "5+Foo", "Foo+5")]            // Integer
        [TestCase("5.5", "Foo", "5.5+Foo", "Foo+5.5")]      // Float
        public void Operator_Plus_CrossSumDifferenceSet(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);

            SumDifferenceSet setPrimitive = new SumDifferenceSet(value2);

            SumDifferenceSet symbolicSet = set1 + setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive + set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n-Foo", "Foo-a^n")]      // Symbolic
        [TestCase("5", "Foo", "5-Foo", "Foo-5")]            // Integer
        [TestCase("5.5", "Foo", "5.5-Foo", "Foo-5.5")]      // Float
        public void Operator_Minus_CrossSumDifferenceSet(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);

            SumDifferenceSet setPrimitive = new SumDifferenceSet(value2);

            SumDifferenceSet symbolicSet = set1 - setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive - set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n*Foo", "Foo*a^n")]      // Symbolic
        [TestCase("5", "Foo", "5*Foo", "Foo*5")]            // Integer
        [TestCase("5.5", "Foo", "5.5*Foo", "Foo*5.5")]      // Float
        public void Operator_Multiply_CrossSumDifferenceSet(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);

            SumDifferenceSet setPrimitive = new SumDifferenceSet(value2);

            ProductQuotientSet symbolicSet = set1 * setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive * set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }

        [TestCase("a^n", "Foo", "a^n/Foo", "Foo/a^n")]      // Symbolic
        [TestCase("5", "Foo", "5/Foo", "Foo/5")]            // Integer
        [TestCase("5.5", "Foo", "5.5/Foo", "Foo/5.5")]      // Float
        public void Operator_Divide_CrossSumDifferenceSet(string value1, string value2, string expectedLabel1, string expectedLabel2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);

            SumDifferenceSet setPrimitive = new SumDifferenceSet(value2);

            ProductQuotientSet symbolicSet = set1 / setPrimitive;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel1));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel1));

            symbolicSet = setPrimitive / set1;

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel2));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel2));
        }
        #endregion
    }
}
