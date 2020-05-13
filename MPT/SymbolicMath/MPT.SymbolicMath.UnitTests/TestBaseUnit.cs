using System;

namespace MPT.SymbolicMath.UnitTests
{
    public class TestBaseUnit : IBase
    {
        public object Clone()
        {
            throw new NotImplementedException();
        }

        public bool IsInteger()
        {
            throw new NotImplementedException();
        }

        public int AsInteger()
        {
            throw new NotImplementedException();
        }

        public bool IsFloat()
        {
            throw new NotImplementedException();
        }

        public double AsFloat()
        {
            throw new NotImplementedException();
        }

        public bool IsFraction()
        {
            throw new NotImplementedException();
        }

        public bool IsNumber()
        {
            throw new NotImplementedException();
        }

        public bool IsSymbolic()
        {
            throw new NotImplementedException();
        }

        public bool SignIsNegative()
        {
            throw new NotImplementedException();
        }

        public bool ValueIsNegative()
        {
            throw new NotImplementedException();
        }

        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public bool HasPower()
        {
            throw new NotImplementedException();
        }

        public string Label()
        {
            throw new NotImplementedException();
        }

        public string BaseLabel()
        {
            throw new NotImplementedException();
        }

        public string PowerLabel()
        {
            throw new NotImplementedException();
        }

        public IBase GetPower()
        {
            throw new NotImplementedException();
        }

        public IBase GetBase()
        {
            throw new NotImplementedException();
        }

        public IBase GetAbsolute()
        {
            throw new NotImplementedException();
        }

        public Sign GetSign()
        {
            throw new NotImplementedException();
        }

        public void FlipSign()
        {
            throw new NotImplementedException();
        }

        public double Calculate()
        {
            throw new NotImplementedException();
        }

        public IBase Simplify(bool isRecursive)
        {
            throw new NotImplementedException();
        }

        public IBase ExtractSign(bool isRecursive)
        {
            throw new NotImplementedException();
        }

        public IBase DistributeSign()
        {
            throw new NotImplementedException();
        }

        public IBase DistributeSignFromPower()
        {
            throw new NotImplementedException();
        }

        public IBase ExtractSignFromPower()
        {
            throw new NotImplementedException();
        }

        public IBase Consolidate(bool isRecursive)
        {
            throw new NotImplementedException();
        }

        public SumDifferenceSet Sum(IBase value)
        {
            throw new NotImplementedException();
        }

        public SumDifferenceSet Subtract(IBase value)
        {
            throw new NotImplementedException();
        }

        public ProductQuotientSet Multiply(IBase value)
        {
            throw new NotImplementedException();
        }

        public ProductQuotientSet Divide(IBase value)
        {
            throw new NotImplementedException();
        }
    }

}
