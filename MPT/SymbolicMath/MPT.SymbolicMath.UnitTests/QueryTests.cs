using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{

    [TestFixture]
    public class QueryTests
    {
        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("-1", ExpectedResult = false)]
        [TestCase("0", ExpectedResult = false)]
        [TestCase("1", ExpectedResult = false)]
        [TestCase("-1.5", ExpectedResult = false)]
        [TestCase("0.0", ExpectedResult = false)]
        [TestCase("1.5", ExpectedResult = false)]
        [TestCase("2/4", ExpectedResult = true)]
        [TestCase("-2/4", ExpectedResult = true)]
        [TestCase("2/-4", ExpectedResult = true)]
        [TestCase("(2/4)", ExpectedResult = true)]     
        [TestCase("(-2/4)", ExpectedResult = true)]
        [TestCase("1.1.1", ExpectedResult = false)]
        [TestCase("3/4X", ExpectedResult = false)]
        [TestCase("3X/4X", ExpectedResult = false)]
        [TestCase("3X/4", ExpectedResult = false)]
        [TestCase("a/b", ExpectedResult = true)]       // Symbolic
        [TestCase("-a/-b", ExpectedResult = true)]
        [TestCase("-a/b", ExpectedResult = true)]
        [TestCase("a/-b", ExpectedResult = true)]
        [TestCase("(a/b)", ExpectedResult = true)]
        [TestCase("(a/b)*c", ExpectedResult = false)]
        [TestCase("(a/b)c", ExpectedResult = false)]
        [TestCase("a/bc", ExpectedResult = true)]
        [TestCase("(a+a/b)", ExpectedResult = false)]
        [TestCase("(a-a/b)", ExpectedResult = false)]
        [TestCase("(a*a/b)", ExpectedResult = false)]
        [TestCase("(a/a/b)", ExpectedResult = false)]
        [TestCase("(a/b+b)", ExpectedResult = false)]
        [TestCase("(a/b-b)", ExpectedResult = false)]
        [TestCase("(a/b*b)", ExpectedResult = false)]
        [TestCase("(a/b/b)", ExpectedResult = false)]
        public bool IsSymbolicFraction(string value)
        {
            return Query.IsSymbolicFraction(value);
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("-1", ExpectedResult = false)]
        [TestCase("0", ExpectedResult = false)]
        [TestCase("1", ExpectedResult = false)]
        [TestCase("-1.5", ExpectedResult = false)]
        [TestCase("0.0", ExpectedResult = false)]
        [TestCase("1.5", ExpectedResult = false)]
        [TestCase("2/4", ExpectedResult = true)]   
        [TestCase("-2/4", ExpectedResult = true)]
        [TestCase("2/-4", ExpectedResult = true)]
        [TestCase("(2/4)", ExpectedResult = true)]     
        [TestCase("(-2/4)", ExpectedResult = true)]
        [TestCase("1.1.1", ExpectedResult = false)]
        [TestCase("a/b", ExpectedResult = false)]       // Symbolic
        [TestCase("-a/-b", ExpectedResult = false)]       
        [TestCase("-a/b", ExpectedResult = false)]       
        [TestCase("a/-b", ExpectedResult = false)]
        [TestCase("(3/4)*X", ExpectedResult = false)]
        [TestCase("(3/4)X", ExpectedResult = false)]
        [TestCase("3/4X", ExpectedResult = false)]
        public bool IsNumericFraction(string value)
        {
            return Query.IsNumericFraction(value);
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("-1", ExpectedResult = true)]
        [TestCase("0", ExpectedResult = true)]
        [TestCase("1", ExpectedResult = true)]
        [TestCase("-1.5", ExpectedResult = false)]
        [TestCase("0.0", ExpectedResult = false)]
        [TestCase("1.5", ExpectedResult = false)]
        [TestCase("2/4", ExpectedResult = false)]   // Fractions are not taken to be integers
        [TestCase("4/2", ExpectedResult = false)]   // Fractions are not taken to be integers, even if they reduce down to one
        [TestCase("-2/4", ExpectedResult = false)]
        [TestCase("2/-4", ExpectedResult = false)]
        [TestCase("(2/4)", ExpectedResult = false)]
        [TestCase("(-2/4)", ExpectedResult = false)]
        [TestCase("1.1.1", ExpectedResult = false)]
        public bool IsInteger(string value)
        {
            return Query.IsInteger(value);
        }

        [TestCase(null,ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("-1", ExpectedResult = true)]
        [TestCase("0", ExpectedResult = true)]
        [TestCase("1", ExpectedResult = true)]
        [TestCase("-1.5", ExpectedResult = true)]
        [TestCase("0.0", ExpectedResult = true)]
        [TestCase("1.5", ExpectedResult = true)]
        [TestCase("2/4", ExpectedResult = false)]   // Fractions are not taken to be numeric
        [TestCase("-2/4", ExpectedResult = false)]
        [TestCase("2/-4", ExpectedResult = false)]
        [TestCase("(2/4)", ExpectedResult = false)]
        [TestCase("(-2/4)", ExpectedResult = false)]
        [TestCase("1.1.1", ExpectedResult = false)]
        public bool IsNumeric(string value)
        {
            return Query.IsNumeric(value);
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("123", ExpectedResult = false)]
        [TestCase("A1B2C3", ExpectedResult = false)]
        [TestCase("A+A", ExpectedResult = false)]
        [TestCase("A-A", ExpectedResult = false)]
        [TestCase("A*A", ExpectedResult = false)]
        [TestCase("A/A", ExpectedResult = false)]
        [TestCase("A^A", ExpectedResult = false)]
        [TestCase("A.A", ExpectedResult = false)]
        [TestCase("A(A", ExpectedResult = false)]
        [TestCase("A)A", ExpectedResult = false)]
        [TestCase("A[A", ExpectedResult = false)]
        [TestCase("A]A", ExpectedResult = false)]
        [TestCase("A{A", ExpectedResult = false)]
        [TestCase("A}A", ExpectedResult = false)]
        [TestCase("A A", ExpectedResult = false)]
        [TestCase("ABC", ExpectedResult = true)]       // English (random examples)
        [TestCase("αβγδΔθΣσς", ExpectedResult = true)]  // Greek symbols (random examples)
        [TestCase("äöüñû", ExpectedResult = true)]      // Unusual letters from other languages (random examples)
        public bool IsAllLetters(string value)  
        {
            return Query.IsAllLetters(value);
        }

        [TestCase(null, 0, ExpectedResult = false)]
        [TestCase("", 0, ExpectedResult = false)]
        [TestCase("-A", ExpectedResult = true)]
        [TestCase("-(A)", ExpectedResult = true)]
        [TestCase("-(-A)", ExpectedResult = true)]
        [TestCase("(-A)", ExpectedResult = false)]      // Base is considered to be demarcated by the parentheses/brackets
        public bool ValueBaseIsNegative(string value)
        {
            return Query.BaseIsNegative(value);
        }

        [TestCase(null, 0, ExpectedResult = false)]
        [TestCase("", 0, ExpectedResult = false)]
        [TestCase("-A", 0, ExpectedResult = true)]      // Single values
        [TestCase("-(A)", 0, ExpectedResult = true)]
        [TestCase("-(-A)", 0, ExpectedResult = true)]
        [TestCase("-(-A)", 2, ExpectedResult = true)]
        [TestCase("(-A)", 1, ExpectedResult = true)]
        [TestCase("B-A", 1, ExpectedResult = false)]    // Operations
        [TestCase("B+A", 1, ExpectedResult = false)]
        [TestCase("B*A", 1, ExpectedResult = false)]
        [TestCase("B/A", 1, ExpectedResult = false)]
        [TestCase("B^A", 1, ExpectedResult = false)]
        [TestCase("B--A", 1, ExpectedResult = false)]   // Operations with negatives
        [TestCase("B+-A", 1, ExpectedResult = false)]
        [TestCase("B*-A", 1, ExpectedResult = false)]
        [TestCase("B/-A", 1, ExpectedResult = false)]
        [TestCase("B^-A", 1, ExpectedResult = false)]
        [TestCase("B--A", 2, ExpectedResult = true)]
        [TestCase("B+-A", 2, ExpectedResult = true)]
        [TestCase("B*-A", 2, ExpectedResult = true)]
        [TestCase("B/-A", 2, ExpectedResult = true)]
        [TestCase("B^-A", 2, ExpectedResult = true)]    // Powers with negatives
        [TestCase("B^-(A)", 2, ExpectedResult = true)]
        [TestCase("B^(-A)", 3, ExpectedResult = true)]
        public bool IsNegative(string value, int characterIndex)
        {
            return Query.IsNegative(value, characterIndex);
        }

        [TestCase(null, 0, ExpectedResult = false)]
        [TestCase("", 0, ExpectedResult = false)]
        [TestCase("-A", 0, ExpectedResult = false)]      // Single values
        [TestCase("-(A)", 0, ExpectedResult = false)]
        [TestCase("-(-A)", 0, ExpectedResult = false)]
        [TestCase("-(-A)", 2, ExpectedResult = false)]
        [TestCase("(-A)", 1, ExpectedResult = false)]
        [TestCase("B-A", 1, ExpectedResult = true)]    // Operations
        [TestCase("B+A", 1, ExpectedResult = false)]
        [TestCase("B*A", 1, ExpectedResult = false)]
        [TestCase("B/A", 1, ExpectedResult = false)]
        [TestCase("B^A", 1, ExpectedResult = false)]
        [TestCase("B--A", 1, ExpectedResult = true)]   // Operations with negatives
        [TestCase("B+-A", 1, ExpectedResult = false)]
        [TestCase("B*-A", 1, ExpectedResult = false)]
        [TestCase("B/-A", 1, ExpectedResult = false)]
        [TestCase("B^-A", 1, ExpectedResult = false)]
        [TestCase("B--A", 2, ExpectedResult = false)]
        [TestCase("B+-A", 2, ExpectedResult = false)]
        [TestCase("B*-A", 2, ExpectedResult = false)]
        [TestCase("B/-A", 2, ExpectedResult = false)]
        [TestCase("B^-A", 2, ExpectedResult = false)]    // Powers with negatives
        [TestCase("B^-(A)", 2, ExpectedResult = false)]
        [TestCase("B^(-A)", 3, ExpectedResult = false)]
        public bool IsSubtraction(string value, int characterIndex)
        {
            return Query.IsSubtraction(value, characterIndex);
        }

        [TestCase(null, null, ExpectedResult = -1)]
        [TestCase("", '+', ExpectedResult = -1)]
        [TestCase("A+B", '\0', ExpectedResult = -1)]
        [TestCase("A+B", '-', ExpectedResult = -1)]
        [TestCase("A+B", '+', ExpectedResult = 1)]
        [TestCase("A-B*C+D", '+', ExpectedResult = 5)]
        [TestCase("A-(B+E)*C+D", '+', ExpectedResult = 4)]
        [TestCase("A-B^(E+F)+G*C+D", '+', ExpectedResult = 6)]
        [TestCase("A-((B+E)*C+D", '+', ExpectedResult = 5)]
        [TestCase("A-(B+E))*C+D", '+', ExpectedResult = 4)]
        [TestCase("-A-B", '-', ExpectedResult = 2)]
        [TestCase("A+-(B+-E--F)*C--D", '-', ExpectedResult = 8)]
        [TestCase("-A^n", '-', ExpectedResult = -1)]
        [TestCase("A^-n", '-', ExpectedResult = -1)]
        [TestCase("-A^-n", '-', ExpectedResult = -1)]
        [TestCase("-A^-n-B", '-', ExpectedResult = 5)]
        [TestCase("-A^-n--B", '-', ExpectedResult = 5)]
        [TestCase("A^(-n^m)", '-', ExpectedResult = -1)]
        [TestCase("-A^(-n^-m)", '-', ExpectedResult = -1)]
        public int IndexOfNextOperator_NotIgnoreInGroups(string value, char targetOperator)
        {
            return Query.IndexOfNextOperator(value, targetOperator, ignoreInGroups: false);
        }

        [TestCase(null, null, ExpectedResult = -1)]
        [TestCase("", '+', ExpectedResult = -1)]
        [TestCase("A+B", '\0', ExpectedResult = -1)]
        [TestCase("A+B", '-', ExpectedResult = -1)]
        [TestCase("A+B", '+', ExpectedResult = 1)]
        [TestCase("A-B*C+D", '+', ExpectedResult = 5)]
        [TestCase("A-(B+E)*C+D", '+', ExpectedResult = 9)]
        [TestCase("A-B^(E+F)+G*C+D", '+', ExpectedResult = 9)]
        [TestCase("A-((B+E)*C+D", '+', ExpectedResult = 10)]
        [TestCase("A-(B+E))*C+D", '+', ExpectedResult = 10)]
        [TestCase("-A-B", '-', ExpectedResult = 2)]
        [TestCase("A+-(B+-E--F)*C--D", '-', ExpectedResult = 14)]
        [TestCase("-A^n", '-', ExpectedResult = -1)]
        [TestCase("A^-n", '-', ExpectedResult = -1)]
        [TestCase("-A^-n", '-', ExpectedResult = -1)]
        [TestCase("-A^-n-B", '-', ExpectedResult = 5)]
        [TestCase("-A^-n--B", '-', ExpectedResult = 5)]
        [TestCase("A^(-n^m)", '-', ExpectedResult = -1)]
        [TestCase("-A^(-n^-m)", '-', ExpectedResult = -1)]
        public int IndexOfNextOperator_IgnoreInGroups(string value, char targetOperator)
        {
            return Query.IndexOfNextOperator(value, targetOperator, ignoreInGroups: true);
        }

        [Test]
        public void IndexOfNextOperator_Of_Null_Or_Empty_Returns_Negative_One()
        {
            char[] operators = null;
            Assert.That(Query.IndexOfNextOperator("FooBar", operators), Is.EqualTo(-1));

            operators = new char[0];
            Assert.That(Query.IndexOfNextOperator("FooBar", operators), Is.EqualTo(-1));
        }

        [TestCase(null, ExpectedResult = -1)]
        [TestCase("", ExpectedResult = -1)]
        [TestCase("A+B", ExpectedResult = 1)]
        [TestCase("A-B*C+D", ExpectedResult = 3)]
        [TestCase("A-(B+E)*C+D", ExpectedResult = 4)]
        [TestCase("A-B^(E+F)+G*C+D", ExpectedResult = 6)]
        [TestCase("A-((B*E)*C+D", ExpectedResult = 5)]
        [TestCase("A-(B*E))*C+D", ExpectedResult = 4)]
        public int IndexOfNextOperator_Of_Any_Operator_NotIgnoreInGroups(string value)
        {
            char[] operators = {'*', '+'};
            return Query.IndexOfNextOperator(value, operators, ignoreInGroups: false);
        }

        [TestCase(null, ExpectedResult = -1)]
        [TestCase("", ExpectedResult = -1)]
        [TestCase("A+B", ExpectedResult = 1)]
        [TestCase("A-B*C+D", ExpectedResult = 3)]
        [TestCase("A-(B+E)*C+D", ExpectedResult = 7)]
        [TestCase("A-B^(E+F)+G*C+D", ExpectedResult = 9)]
        [TestCase("A-((B*E)*C+D", ExpectedResult = 10)]
        [TestCase("A-(B*E))*C+D", ExpectedResult = 8)]
        public int IndexOfNextOperator_Of_Any_Operator_IgnoreInGroups(string value)
        {
            char[] operators = { '*', '+' };
            return Query.IndexOfNextOperator(value, operators, ignoreInGroups: true);
        }

        [TestCase("-A-B", ExpectedResult = 2)]
        [TestCase("A+-(B+-E--F)*C--D", ExpectedResult = 8)]
        [TestCase("-A^n", ExpectedResult = -1)]
        [TestCase("A^-n", ExpectedResult = -1)]
        [TestCase("-A^-n", ExpectedResult = -1)]
        [TestCase("-A^-n-B", ExpectedResult = 5)]
        [TestCase("-A^-n--B", ExpectedResult = 5)]
        [TestCase("A^(-n^m)", ExpectedResult = -1)]
        [TestCase("-A^(-n^-m)", ExpectedResult = -1)]
        public int IndexOfNextOperator_Of_Any_Operator_With_Subtraction_NotIgnoreInGroups(string value)
        {
            char[] operators = { '/', '-' };
            return Query.IndexOfNextOperator(value, operators, ignoreInGroups: false);
        }

        [TestCase("-A-B", ExpectedResult = 2)]
        [TestCase("A+-(B+-E--F)*C--D", ExpectedResult = 14)]
        [TestCase("-A^n", ExpectedResult = -1)]
        [TestCase("A^-n", ExpectedResult = -1)]
        [TestCase("-A^-n", ExpectedResult = -1)]
        [TestCase("-A^-n-B", ExpectedResult = 5)]
        [TestCase("-A^-n--B", ExpectedResult = 5)]
        [TestCase("A^(-n^m)", ExpectedResult = -1)]
        [TestCase("-A^(-n^-m)", ExpectedResult = -1)]
        public int IndexOfNextOperator_Of_Any_Operator_With_Subtraction_IgnoreInGroups(string value)
        {
            char[] operators = { '/', '-' };
            return Query.IndexOfNextOperator(value, operators, ignoreInGroups: true);
        }

        [TestCase(null, 0, ExpectedResult = false)]
        [TestCase("", 0, ExpectedResult = false)]
        [TestCase("-A", 0, ExpectedResult = false)]      // Single values
        [TestCase("-(A)", 0, ExpectedResult = false)]
        [TestCase("-(-A)", 0, ExpectedResult = false)]
        [TestCase("-(-A)", 2, ExpectedResult = false)]
        [TestCase("(-A)", 1, ExpectedResult = false)]
        [TestCase("B-A", 1, ExpectedResult = true)]    // Operations
        [TestCase("B+A", 1, ExpectedResult = true)]
        [TestCase("B*A", 1, ExpectedResult = false)]
        [TestCase("B/A", 1, ExpectedResult = false)]
        [TestCase("B^A", 1, ExpectedResult = false)]
        [TestCase("B--A", 1, ExpectedResult = true)]   // Operations with negatives
        [TestCase("B+-A", 1, ExpectedResult = true)]
        [TestCase("B*-A", 1, ExpectedResult = false)]
        [TestCase("B/-A", 1, ExpectedResult = false)]
        [TestCase("B^-A", 1, ExpectedResult = false)]
        [TestCase("B--A", 2, ExpectedResult = false)]
        [TestCase("B+-A", 2, ExpectedResult = false)]
        [TestCase("B*-A", 2, ExpectedResult = false)]
        [TestCase("B/-A", 2, ExpectedResult = false)]
        [TestCase("B^-A", 2, ExpectedResult = false)]    // Powers with negatives
        [TestCase("B^-(A)", 2, ExpectedResult = false)]
        [TestCase("B^(-A)", 3, ExpectedResult = false)]
        public bool IsSumDifference(string value, int characterIndex)
        {
            return Query.IsSumDifference(value, characterIndex);
        }

        [TestCase(null, 0, ExpectedResult = false)]
        [TestCase("", 0, ExpectedResult = false)]
        [TestCase("-A", 0, ExpectedResult = false)]      // Single values
        [TestCase("-(A)", 0, ExpectedResult = false)]
        [TestCase("-(-A)", 0, ExpectedResult = false)]
        [TestCase("-(-A)", 2, ExpectedResult = false)]
        [TestCase("(-A)", 1, ExpectedResult = false)]
        [TestCase("B-A", 1, ExpectedResult = false)]    // Operations
        [TestCase("B+A", 1, ExpectedResult = false)]
        [TestCase("B*A", 1, ExpectedResult = true)]
        [TestCase("B/A", 1, ExpectedResult = true)]
        [TestCase("B^A", 1, ExpectedResult = false)]
        [TestCase("B--A", 1, ExpectedResult = false)]   // Operations with negatives
        [TestCase("B+-A", 1, ExpectedResult = false)]
        [TestCase("B*-A", 1, ExpectedResult = true)]
        [TestCase("B/-A", 1, ExpectedResult = true)]
        [TestCase("B^-A", 1, ExpectedResult = false)]
        [TestCase("B--A", 2, ExpectedResult = false)]
        [TestCase("B+-A", 2, ExpectedResult = false)]
        [TestCase("B*-A", 2, ExpectedResult = false)]
        [TestCase("B/-A", 2, ExpectedResult = false)]
        [TestCase("B^-A", 2, ExpectedResult = false)]    // Powers with negatives
        [TestCase("B^-(A)", 2, ExpectedResult = false)]
        [TestCase("B^(-A)", 3, ExpectedResult = false)]
        public bool IsProductQuotient(string value, int characterIndex)
        {
            return Query.IsProductQuotient(value, characterIndex);
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("A", ExpectedResult = false)]
        [TestCase("-A", ExpectedResult = false)]
        [TestCase("A+B", ExpectedResult = false)]
        [TestCase("A-B", ExpectedResult = true)]
        [TestCase("A*B", ExpectedResult = false)]
        [TestCase("A/B", ExpectedResult = false)]
        [TestCase("-A+B", ExpectedResult = false)]
        [TestCase("-A-B", ExpectedResult = true)]
        [TestCase("-A*B", ExpectedResult = false)]
        [TestCase("-A/B", ExpectedResult = false)]
        [TestCase("A+-B", ExpectedResult = false)]
        [TestCase("A--B", ExpectedResult = true)]
        [TestCase("A*-B", ExpectedResult = false)]
        [TestCase("A/-B", ExpectedResult = false)]
        [TestCase("A^B", ExpectedResult = false)]       // Powers
        [TestCase("-A^B", ExpectedResult = false)]
        [TestCase("A^-B", ExpectedResult = false)]
        [TestCase("-A^-B", ExpectedResult = false)]
        [TestCase("-A^(-B)", ExpectedResult = false)]
        [TestCase("-A^-(B-C)", ExpectedResult = true)]      // TODO: Should this include within powers?
        [TestCase("-A^(-B-C)", ExpectedResult = true)]      // TODO: Should this include within powers?
        [TestCase("-A^(-B--C)", ExpectedResult = true)]      // TODO: Should this include within powers?
        [TestCase("-A^-(B+C)", ExpectedResult = false)]     
        [TestCase("-A^(-B+C)", ExpectedResult = false)]
        [TestCase("-A^(-B+-C)", ExpectedResult = false)]
        public bool ValueContainsSubtraction(string value)
        {
            return Query.ContainsSubtraction(value);
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("A", ExpectedResult = false)]
        [TestCase("-A", ExpectedResult = false)]
        [TestCase("(-A)", ExpectedResult = false)]
        [TestCase("-(-A)", ExpectedResult = false)]
        [TestCase("A*B/C*D", ExpectedResult = false)]
        [TestCase("A*(B/C)/D", ExpectedResult = false)]
        [TestCase("A*B+C/D", ExpectedResult = true)]
        [TestCase("A*(B+C)/D", ExpectedResult = true)]
        [TestCase("A*B-C/D", ExpectedResult = true)]
        [TestCase("A*(B-C)/D", ExpectedResult = true)]
        [TestCase("A^B", ExpectedResult = false)]               // Testing powers
        [TestCase("A^(B+C)", ExpectedResult = true)]           // TODO: Should this include within powers?
        [TestCase("A^(B*C)", ExpectedResult = false)]
        [TestCase("A^(B+C)+D^(E-F)", ExpectedResult = true)]
        [TestCase("A^(B*C)+D^(E-F)", ExpectedResult = true)]
        [TestCase("A^(B*C)+D^(E/F)", ExpectedResult = true)] 
        [TestCase("A^(B*C)*D^(E-F)", ExpectedResult = true)]   // TODO: Should this include within powers?
        [TestCase("A^(B*C)*D^(E/F)", ExpectedResult = false)]   
        [TestCase("A^(B*C)/D^(E/F)", ExpectedResult = false)]
        public bool ValueContainsAddSubtract(string value)
        {
            return Query.ContainsAddSubtract(value);
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("A", ExpectedResult = false)]
        [TestCase("-A", ExpectedResult = false)]
        [TestCase("(-A)", ExpectedResult = false)]
        [TestCase("-(-A)", ExpectedResult = false)]
        [TestCase("A+B-C+D", ExpectedResult = false)]
        [TestCase("A+(B-C)-D", ExpectedResult = false)]
        [TestCase("A+B*C-D", ExpectedResult = true)]
        [TestCase("A+(B*C)-D", ExpectedResult = true)]
        [TestCase("A+B/C-D", ExpectedResult = true)]
        [TestCase("A+(B/C)-D", ExpectedResult = true)]
        [TestCase("A^B", ExpectedResult = false)]               // Testing powers
        [TestCase("A^(B+C)", ExpectedResult = false)]              
        [TestCase("A^(B*C)", ExpectedResult = true)]           // TODO: Should this include within powers?
        [TestCase("A^(B+C)+D^(E-F)", ExpectedResult = false)]
        [TestCase("A^(B*C)+D^(E-F)", ExpectedResult = true)]   // TODO: Should this include within powers?
        [TestCase("A^(B*C)+D^(E/F)", ExpectedResult = true)]   // TODO: Should this include within powers?
        [TestCase("A^(B*C)*D^(E-F)", ExpectedResult = true)]   
        [TestCase("A^(B*C)/D^(E/F)", ExpectedResult = true)]   
        public bool ValueContainsMultiplyDivide(string value)
        {
            return Query.ContainsMultiplyDivide(value);
        }

        [TestCase(0, 4, ExpectedResult = 0)]
        [TestCase(2, 0, ExpectedResult = 0)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(-2, 4, ExpectedResult = 2)]
        [TestCase(2, -4, ExpectedResult = 2)]
        [TestCase(2, 4, ExpectedResult = 2)]
        [TestCase(4, 2, ExpectedResult = 2)]
        [TestCase(3, 6, ExpectedResult = 3)]
        [TestCase(6, 3, ExpectedResult = 3)]
        [TestCase(300, 3, ExpectedResult = 3)]
        [TestCase(3, 300, ExpectedResult = 3)]
        public int GreatestCommonDenominator(int value1, int value2)
        {
            return Query.GreatestCommonDenominator(value1, value2, 1);
        }

        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase("0", ExpectedResult = "0")]
        [TestCase("0/1", ExpectedResult = "0")]
        [TestCase("0/0", ExpectedResult = "NaN")]
        [TestCase("1/0", ExpectedResult = "Infinity")]
        [TestCase("-1/0", ExpectedResult = "-Infinity")]
        [TestCase("1/2", ExpectedResult = "1/2")]
        [TestCase("-1/2", ExpectedResult = "-1/2")]
        [TestCase("1/-2", ExpectedResult = "-1/2")]
        [TestCase("2/4", ExpectedResult = "1/2")]
        [TestCase("4/2", ExpectedResult = "2")]
        [TestCase("3/9", ExpectedResult = "1/3")]
        [TestCase("9/3", ExpectedResult = "3")]
        [TestCase("900/3", ExpectedResult = "300")]
        [TestCase("3/900", ExpectedResult = "1/300")]
        [TestCase("5/6", ExpectedResult = "5/6")]
        public string SimplifiedFraction(string value)
        {
            return Query.SimplifiedFraction(value);
        }
    }
}
