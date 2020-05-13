using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{
    [TestFixture]
    public class BaseSetTests
    {
        [Test]
        public void Index_Accesses_Valid_Index_Returns_Item()
        {
            ProductQuotientSet productQuotientSet = new ProductQuotientSet("a");
            PrimitiveUnit unit1 = new PrimitiveUnit("b", new PrimitiveUnit("2"));
            productQuotientSet.MultiplyItem(unit1);
            PrimitiveUnit unit2 = new PrimitiveUnit("c");
            productQuotientSet.DivideItem(unit2);

            UnitOperatorPair unitOperatorPair = productQuotientSet[1];
            Assert.IsTrue(unitOperatorPair.Unit.Equals(unit1));
            Assert.That(unitOperatorPair.Operator, Is.EqualTo("*"));
        }

        [Test]
        public void Index_Accesses_Invalid_Index_Throws_Exception()
        {

            ProductQuotientSet productQuotientSet = new ProductQuotientSet("a");
            PrimitiveUnit unit1 = new PrimitiveUnit("b", new PrimitiveUnit("2"));
            productQuotientSet.MultiplyItem(unit1);
            PrimitiveUnit unit2 = new PrimitiveUnit("c");
            productQuotientSet.DivideItem(unit2);

            Assert.Throws<ArgumentOutOfRangeException>(() => { UnitOperatorPair unitOperatorPair = productQuotientSet[-1]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { UnitOperatorPair unitOperatorPair = productQuotientSet[3]; });
        }

        [Test]
        public void ForEach_Iterates_Items()
        {
            PrimitiveUnit unit0 = new PrimitiveUnit("a");
            ProductQuotientSet productQuotientSet = new ProductQuotientSet(unit0);
            PrimitiveUnit unit1 = new PrimitiveUnit("b", new PrimitiveUnit("2"));
            productQuotientSet.MultiplyItem(unit1);
            PrimitiveUnit unit2 = new PrimitiveUnit("c");
            productQuotientSet.DivideItem(unit2);

            int counter = 0;
            foreach (UnitOperatorPair item in productQuotientSet)
            {
                switch (counter)
                {
                    case 0:
                        Assert.IsTrue(item.Unit.Equals(unit0));
                        break;
                    case 1:
                        Assert.IsTrue(item.Unit.Equals(unit1));
                        break;
                    case 2:
                        Assert.IsTrue(item.Unit.Equals(unit2));
                        break;
                }

                counter++;
            }
        }

        [Test]
        public void ForEach_Resets()
        {
            PrimitiveUnit unit0 = new PrimitiveUnit("a");
            ProductQuotientSet productQuotientSet = new ProductQuotientSet(unit0);
            PrimitiveUnit unit1 = new PrimitiveUnit("b", new PrimitiveUnit("2"));
            productQuotientSet.MultiplyItem(unit1);
            PrimitiveUnit unit2 = new PrimitiveUnit("c");
            productQuotientSet.DivideItem(unit2);

            IEnumerator enumerator = productQuotientSet.GetEnumerator();
            enumerator.MoveNext();
            UnitOperatorPair unitOperatorPair = (UnitOperatorPair)enumerator.Current;
            Assert.IsTrue(unitOperatorPair.Unit.Equals(unit0));
            enumerator.MoveNext();
            unitOperatorPair = (UnitOperatorPair)enumerator.Current;
            Assert.IsTrue(unitOperatorPair.Unit.Equals(unit1));

            enumerator.Reset();
            unitOperatorPair = (UnitOperatorPair)enumerator.Current;
            Assert.IsTrue(unitOperatorPair.Unit.Equals(unit0));
        }

        [Test]
        public void ForEach_Throws_Exception()
        {
            PrimitiveUnit unit0 = new PrimitiveUnit("a");
            ProductQuotientSet productQuotientSet = new ProductQuotientSet(unit0);
            PrimitiveUnit unit1 = new PrimitiveUnit("b", new PrimitiveUnit("2"));
            productQuotientSet.MultiplyItem(unit1);
            PrimitiveUnit unit2 = new PrimitiveUnit("c");
            productQuotientSet.DivideItem(unit2);

            // Not incremented yet
            IEnumerator enumerator = productQuotientSet.GetEnumerator();
            Assert.Throws<ArgumentOutOfRangeException>(() => { IBase badUnit = (IBase)enumerator.Current; });

            enumerator.MoveNext();
            UnitOperatorPair unitOperatorPair = (UnitOperatorPair)enumerator.Current;
            Assert.IsTrue(unitOperatorPair.Unit.Equals(unit0));
            enumerator.MoveNext();
            unitOperatorPair = (UnitOperatorPair)enumerator.Current;
            Assert.IsTrue(unitOperatorPair.Unit.Equals(unit1));
            enumerator.MoveNext();
            unitOperatorPair = (UnitOperatorPair)enumerator.Current;
            Assert.IsTrue(unitOperatorPair.Unit.Equals(unit2));

            // Too many increments
            enumerator.MoveNext();
            Assert.Throws<ArgumentOutOfRangeException>(() => { IBase badUnit = (IBase)enumerator.Current; });
        }
    }
}
