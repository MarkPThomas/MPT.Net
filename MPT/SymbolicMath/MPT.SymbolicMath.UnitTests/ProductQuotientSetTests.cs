using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{
    [TestFixture]
    public class ProductQuotientSetTests
    {
        #region Initialize: Basic
        [TestCase(5, "", "5", "", "5", true, false, true)]
        [TestCase(5, "2", "5", "2", "5^2", true, false, true)]
        [TestCase(5, "1/2", "5", "1/2", "5^(1/2)", true, true, false)]
        [TestCase(5, "-1/2", "5", "-1/2", "5^-(1/2)", true, true, false)]
        [TestCase(-5, "", "-5", "", "-5", true, false, true)]
        [TestCase(-5, "2", "-5", "2", "-5^2", true, false, true)]
        [TestCase(-5, "1/2", "-5", "1/2", "-5^(1/2)", true, false)]
        [TestCase(-5, "-1/2", "-5", "-1/2", "-5^-(1/2)", true, false)]
        [TestCase(-5, "Foo", "-5", "Foo", "-5^Foo", false, false, false)]
        public void Initialize_As_Integer(int value, string exponent,
            string expectedBaseLabel, string expectedPowerLabel, string expectedLabel,
            bool isNumber, bool isFloat, bool isInteger)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value, power);

            Assert.That(symbolicSet.IsNumber(), Is.EqualTo(isNumber));
            Assert.That(symbolicSet.IsFloat(), Is.EqualTo(isFloat));
            Assert.That(symbolicSet.IsInteger(), Is.EqualTo(isInteger));
            Assert.IsFalse(symbolicSet.IsFraction());
            Assert.That(symbolicSet.IsSymbolic(), Is.EqualTo(!isNumber));

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicSet.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase(5.2, "", "5.2", "", "5.2", true, true)]
        [TestCase(5.2, "2", "5.2", "2", "5.2^2", true, true)]
        [TestCase(5.2, "1/2", "5.2", "1/2", "5.2^(1/2)", true, true)]
        [TestCase(5.2, "-1/2", "5.2", "-1/2", "5.2^-(1/2)", true, true)]
        [TestCase(-5.2, "", "-5.2", "", "-5.2", true, true)]
        [TestCase(-5.2, "2", "-5.2", "2", "-5.2^2", true, true)]
        [TestCase(-5.2, "1/2", "-5.2", "1/2", "-5.2^(1/2)", true, true)]
        [TestCase(-5.2, "-1/2", "-5.2", "-1/2", "-5.2^-(1/2)", true, true)]
        [TestCase(-5.2, "Foo", "-5.2", "Foo", "-5.2^Foo", false, false)]
        public void Initialize_As_Double(double value, string exponent,
            string expectedBaseLabel, string expectedPowerLabel, string expectedLabel, bool isNumber, bool isFloat)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value, power);

            Assert.That(symbolicSet.IsNumber(), Is.EqualTo(isNumber));
            Assert.That(symbolicSet.IsFloat(), Is.EqualTo(isFloat));
            Assert.IsFalse(symbolicSet.IsInteger());
            Assert.IsFalse(symbolicSet.IsFraction());
            Assert.That(symbolicSet.IsSymbolic(), Is.EqualTo(!isNumber));

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicSet.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase("5", "", "5", "", "5", true, false, true, false)]
        [TestCase("5", "2", "5", "2", "5^2", true, false, true, false)]
        [TestCase("5", "1/2", "5", "1/2", "5^(1/2)", true, true, false, false)]
        [TestCase("5", "-1/2", "5", "-1/2", "5^-(1/2)", true, true, false, false)]
        [TestCase("-5", "", "-5", "", "-5", true, false, true, false)]
        [TestCase("-5", "2", "-5", "2", "-5^2", true, false, true, false)]
        [TestCase("-5", "1/2", "-5", "1/2", "-5^(1/2)", true, true, false, false)]
        [TestCase("-5", "-1/2", "-5", "-1/2", "-5^-(1/2)", true, true, false, false)]
        [TestCase("5.2", "", "5.2", "", "5.2", true, true, false, false)]
        [TestCase("5/6", "", "5/6", "", "5/6", true, true, false, true)]
        [TestCase("-5", "Foo", "-5", "Foo", "-5^Foo", false, false, false, false)]
        [TestCase("Foo", "", "Foo", "", "Foo", false, false, false, false)]
        public void Initialize_As_String(string value, string exponent,
            string expectedBaseLabel, string expectedPowerLabel, string expectedLabel,
            bool isNumber, bool isFloat, bool isInteger, bool isFraction)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value, power);

            Assert.That(symbolicSet.IsNumber(), Is.EqualTo(isNumber));
            Assert.That(symbolicSet.IsFloat(), Is.EqualTo(isFloat));
            Assert.That(symbolicSet.IsInteger(), Is.EqualTo(isInteger));
            Assert.That(symbolicSet.IsFraction(), Is.EqualTo(isFraction));
            Assert.That(symbolicSet.IsSymbolic(), Is.EqualTo(!isNumber));

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicSet.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase("5", "", "", "5", "", "5", true, false, true, false)]
        [TestCase("5", "2", "", "5^2", "", "5^2", true, false, true, false)]
        [TestCase("5", "", "2", "5", "2", "5^2", true, false, true, false)]
        [TestCase("5", "3", "2", "5^3", "2", "(5^3)^2", true, false, true, false)]
        [TestCase("5", "1/2", "", "5^(1/2)", "", "5^(1/2)", true, true, false, false)]
        [TestCase("5", "-1/2", "", "5^-(1/2)", "", "5^-(1/2)", true, true, false, false)]
        [TestCase("Foo", "-1/2", "", "Foo^-(1/2)", "", "Foo^-(1/2)", false, false, false, false)]
        [TestCase("Foo", "-1/2", "Bar", "Foo^-(1/2)", "Bar", "(Foo^-(1/2))^Bar", false, false, false, false)]
        [TestCase("5", "-Foo", "", "5^-Foo", "", "5^-Foo", false, false, false, false)]
        [TestCase("5", "-Foo", "Bar", "5^-Foo", "Bar", "(5^-Foo)^Bar", false, false, false, false)]
        public void Initialize_As_PrimitiveUnit(string value, string valueExponent, string exponent,
            string expectedBaseLabel, string expectedPowerLabel, string expectedLabel,
            bool isNumber, bool isFloat, bool isInteger, bool isFraction)
        {
            PrimitiveUnit valuepower = new PrimitiveUnit(valueExponent);
            PrimitiveUnit unit = new PrimitiveUnit(value, valuepower);

            PrimitiveUnit power = new PrimitiveUnit(exponent);
            ProductQuotientSet symbolicSet = new ProductQuotientSet(unit, power);

            Assert.That(symbolicSet.IsNumber(), Is.EqualTo(isNumber));
            Assert.That(symbolicSet.IsFloat(), Is.EqualTo(isFloat));
            Assert.That(symbolicSet.IsInteger(), Is.EqualTo(isInteger));
            Assert.That(symbolicSet.IsFraction(), Is.EqualTo(isFraction));
            Assert.That(symbolicSet.IsSymbolic(), Is.EqualTo(!isNumber));

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.BaseLabel(), Is.EqualTo(expectedBaseLabel));
            Assert.That(symbolicSet.PowerLabel(), Is.EqualTo(expectedPowerLabel));
        }

        [TestCase(5, "", true, ExpectedResult = true)]
        [TestCase(5, "2", true, ExpectedResult = true)]
        [TestCase(5, "-2", true, ExpectedResult = true)]
        [TestCase(-5, "", true, ExpectedResult = true)]
        [TestCase(-5, "2", true, ExpectedResult = true)]
        [TestCase(-5, "-2", true, ExpectedResult = true)]
        [TestCase(5, "", false, ExpectedResult = false)]
        [TestCase(5, "2", false, ExpectedResult = false)]
        [TestCase(5, "-2", false, ExpectedResult = false)]
        [TestCase(-5, "", false, ExpectedResult = false)]
        [TestCase(-5, "2", false, ExpectedResult = false)]
        [TestCase(-5, "-2", false, ExpectedResult = false)]
        public bool Initialize_As_Integer_With_Sign(int value, string exponent, bool isNegative)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value, power, isNegative);
            return symbolicSet.SignIsNegative();
        }

        [TestCase(5.3, "", true, ExpectedResult = true)]
        [TestCase(5.3, "2", true, ExpectedResult = true)]
        [TestCase(5.3, "-2", true, ExpectedResult = true)]
        [TestCase(-5.3, "", true, ExpectedResult = true)]
        [TestCase(-5.3, "2", true, ExpectedResult = true)]
        [TestCase(-5.3, "-2", true, ExpectedResult = true)]
        [TestCase(5.3, "", false, ExpectedResult = false)]
        [TestCase(5.3, "2", false, ExpectedResult = false)]
        [TestCase(5.3, "-2", false, ExpectedResult = false)]
        [TestCase(-5.3, "", false, ExpectedResult = false)]
        [TestCase(-5.3, "2", false, ExpectedResult = false)]
        [TestCase(-5.3, "-2", false, ExpectedResult = false)]
        public bool Initialize_As_Double_With_Sign(double value, string exponent, bool isNegative)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value, power, isNegative);
            return symbolicSet.SignIsNegative();
        }

        [TestCase("5", "", true, ExpectedResult = true)]
        [TestCase("5", "2", true, ExpectedResult = true)]
        [TestCase("5", "-2", true, ExpectedResult = true)]
        [TestCase("-5", "", true, ExpectedResult = true)]
        [TestCase("-5", "2", true, ExpectedResult = true)]
        [TestCase("-5", "-2", true, ExpectedResult = true)]
        [TestCase("5", "", false, ExpectedResult = false)]
        [TestCase("5", "2", false, ExpectedResult = false)]
        [TestCase("5", "-2", false, ExpectedResult = false)]
        [TestCase("-5", "", false, ExpectedResult = false)]
        [TestCase("-5", "2", false, ExpectedResult = false)]
        [TestCase("-5", "-2", false, ExpectedResult = false)]
        public bool Initialize_As_Integer_With_Sign(string value, string exponent, bool isNegative)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value, power, isNegative);
            return symbolicSet.SignIsNegative();
        }

        [TestCase("5", "", true, ExpectedResult = true)]
        [TestCase("5", "2", true, ExpectedResult = true)]
        [TestCase("5", "-2", true, ExpectedResult = true)]
        [TestCase("-5", "", true, ExpectedResult = true)]
        [TestCase("-5", "2", true, ExpectedResult = true)]
        [TestCase("-5", "-2", true, ExpectedResult = true)]
        [TestCase("5", "", false, ExpectedResult = false)]
        [TestCase("5", "2", false, ExpectedResult = false)]
        [TestCase("5", "-2", false, ExpectedResult = false)]
        [TestCase("-5", "", false, ExpectedResult = false)]
        [TestCase("-5", "2", false, ExpectedResult = false)]
        [TestCase("-5", "-2", false, ExpectedResult = false)]
        public bool Initialize_As_IBase_With_Sign(string value, string exponent, bool isNegative)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            SumDifferenceSet symbolicSet = new SumDifferenceSet(unit, power, isNegative);
            return symbolicSet.SignIsNegative();
        }
        #endregion

        #region Initialize: Check Parsing & Labels
        [TestCase("A", "A")]
        [TestCase("-A", "-A")]
        [TestCase("A^n", "A^n")]
        [TestCase("-A^n", "-A^n")]
        [TestCase("A^-n", "A^-n")]
        [TestCase("-A^-n", "-A^-n")]
        [TestCase("A^(n^m)", "A^(n^m)")]
        [TestCase("-A^(-n^-m)", "-A^(-n^-m)")]
        [TestCase("-A^(n^-m)", "-A^(n^-m)")]
        [TestCase("A^(-n^m)", "A^(-n^m)")]
        [TestCase("A^n^m", "(A^n)^m")]
        [TestCase("-A^-n^-m", "(-A^-n)^-m")]
        [TestCase("-A^n^-m", "(-A^n)^-m")]
        [TestCase("A^-n^m", "(A^-n)^m")]
        [TestCase("A^n^m^p", "((A^n)^m)^p")]
        [TestCase("-A^-n^-m^-p", "((-A^-n)^-m)^-p")]
        public void Initialize_As_String_Primitives(string value, string expectedLabel)
        {
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase("A*B", "A*B")]
        [TestCase("A/B", "A/B")]
        [TestCase("A*B/C", "A*B/C")]
        [TestCase("A*B*C/D", "A*B*C/D")]
        [TestCase("A/B*C", "A/B*C")]
        [TestCase("A/B*C*D", "A/B*C*D")]
        [TestCase("A*B/C*D", "A*B/C*D")]
        [TestCase("A/B*C/D", "A/B*C/D")]
        [TestCase("A*B*C/D*E*F", "A*B*C/D*E*F")]
        [TestCase("A/B/C*D/E/F", "A/B/C*D/E/F")]
        [TestCase("(A*B)/C", "(A*B)/C")]
        [TestCase("(A*B*C)/D", "(A*B*C)/D")]
        [TestCase("(A/B)*C", "(A/B)*C")]
        [TestCase("(A/B)*C*D", "(A/B)*C*D")]
        [TestCase("((A*B)/C)*D", "((A*B)/C)*D")]
        [TestCase("((A/B)*C)/D", "((A/B)*C)/D")]
        [TestCase("((A*B*C)/D)*E*F", "((A*B*C)/D)*E*F")]
        [TestCase("((((A/B)/C)*D)/E)/F", "((((A/B)/C)*D)/E)/F")]
        [TestCase("A^n*B^m", "A^n*B^m")]
        [TestCase(" A*B ", "A*B")]
        [TestCase("A * B", "A*B")]
        [TestCase(" A * B ", "A*B")]
        [TestCase(" A * B E ", "A*BE")]
        [TestCase(" A / B E ", "A/BE")]
        public void Initialize_As_String_ProductQuotients(string value, string expectedLabel)
        {
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase("A+B", "A+B")]
        [TestCase("A-B", "A-B")]
        [TestCase("A+B-C", "A+B-C")]
        [TestCase("A-B+C", "A-B+C")]
        [TestCase("A+B+C-D-E-F+G", "A+B+C-D-E-F+G")]
        [TestCase("A-B-C+D+E+F-G", "A-B-C+D+E+F-G")]
        [TestCase("A^n+B^m", "A^n+B^m")]
        [TestCase(" A+B ", "A+B")]
        [TestCase("A + B", "A+B")]
        [TestCase(" A + B ", "A+B")]
        [TestCase(" A + B E ", "A+BE")]
        [TestCase(" A - B E ", "A-BE")] 
        public void Initialize_As_String_SumDifference(string value, string expectedLabel)
        {
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase("A*B-C", "(A*B)-C")]
        [TestCase("A/B-C", "(A/B)-C")]
        [TestCase("A+B*C", "A+(B*C)")]
        [TestCase("A-B/C", "A-(B/C)")]
        [TestCase("A+B-C*D/E^F", "A+B-(C*D/E^F)")]
        public void Initialize_As_String_Mixed_SumDifference_ProductQuotient(string value, string expectedLabel)
        {
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }
        #endregion

        #region Initialize: Check Group Parsing
        [TestCase("(A*B)", "A*B")]
        [TestCase("(A * B)", "A*B")]
        [TestCase("-(A*B)", "-A*B")]
        [TestCase("-(A*B)^n", "-(A*B)^n")]
        [TestCase("-(A/B)", "-A/B")]
        [TestCase("-(A/B)^n", "-(A/B)^n")]
        [TestCase("(A*B)*(C*D)", "(A*B)*(C*D)")]
        [TestCase("(A/B)/(C/D)", "(A/B)/(C/D)")]
        [TestCase("(A*B*C)*(D*E*F)", "(A*B*C)*(D*E*F)")]
        [TestCase("(A/B/C)/(D/E/F)", "(A/B/C)/(D/E/F)")]
        [TestCase("(A/B)^n*(B*C)^m", "((A/B)^n)*((B*C)^m)")]
        [TestCase("-(A/B)^n*(B*C)^m", "(-(A/B)^n)*((B*C)^m)")]
        [TestCase("-(A/B/C)^n*(C*D*E)^m", "(-(A/B/C)^n)*((C*D*E)^m)")]
        [TestCase("-((A/B)^n*(B*C)^m)^o", "-(((A/B)^n)*((B*C)^m))^o")]
        [TestCase("-((A/B)^n*(B*C)^m)^o*D", "(-(((A/B)^n)*((B*C)^m))^o)*D")]
        [TestCase("-(A/B)^(n*m)", "-(A/B)^(n*m)")]
        [TestCase("H^(1/2)/I^0.5", "(H^(1/2))/I^0.5")]
        [TestCase("(E+F*G)+H^(1/2)", "(E+(F*G))+(H^(1/2))")]
        [TestCase("-(A/B/C)^(n*m)", "-(A/B/C)^(n*m)")]
        [TestCase("-(A/B/C)^(n*m/n)", "-(A/B/C)^(n*m/n)")]
        [TestCase("-(A/B)^(n*m)*o", "(-(A/B)^(n*m))*o")]
        public void Initialize_As_String_With_Parentheses_ProductQuotients(string value, string expectedLabel)
        {
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase("(A+B)", "A+B")]
        [TestCase("(A + B)", "A+B")]
        [TestCase("-(A+B)", "-(A+B)")]
        [TestCase("-(A+B)^n", "-(A+B)^n")]
        [TestCase("(A+B)+(C+D)", "(A+B)+(C+D)")]
        [TestCase("(A-B)-(C-D)", "(A-B)-(C-D)")]
        [TestCase("(A-B)+(C-D)", "(A-B)+(C-D)")]
        [TestCase("(A+B)-(C+D)", "(A+B)-(C+D)")]
        [TestCase("(A+B+C)+(D+E+F)", "(A+B+C)+(D+E+F)")]
        [TestCase("(A-B-C)-(D-E-F)", "(A-B-C)-(D-E-F)")]
        [TestCase("(A-B-C)+(D-E-F)", "(A-B-C)+(D-E-F)")]
        [TestCase("(A+B+C)-(D+E+F)", "(A+B+C)-(D+E+F)")]
        public void Initialize_As_String_With_Parentheses_SumDifference(string value, string expectedLabel)
        {
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase("A*(B+C)", "A*(B+C)")]
        [TestCase("A+(B+C)", "A+(B+C)")]
        [TestCase("A+(B*C)", "A+(B*C)")]
        [TestCase("(A*B)+(C*D)", "(A*B)+(C*D)")]
        [TestCase("(A+B)*(C+D)", "(A+B)*(C+D)")]
        [TestCase("(A/B)-(C/D)", "(A/B)-(C/D)")]
        [TestCase("(A-B)/(C-D)", "(A-B)/(C-D)")]
        [TestCase("(A*B*C)+(C*D*E)", "(A*B*C)+(C*D*E)")]
        [TestCase("(A+B+C)*(C+D+E)", "(A+B+C)*(C+D+E)")]
        [TestCase("(A/B/C)-(C/D/E)", "(A/B/C)-(C/D/E)")]
        [TestCase("(A-B-C)/(C-D-E)", "(A-B-C)/(C-D-E)")]
        [TestCase("A^n+(B^m*C^x)^y", "A^n+((B^m*C^x)^y)")]
        [TestCase("(X/Y)+A^n-(B^m*C^x)^y", "(X/Y)+A^n-((B^m*C^x)^y)")]
        [TestCase("-(X/Y)+A^n-(B^m*C^x)^y", "-(X/Y)+A^n-((B^m*C^x)^y)")]
        [TestCase("A^n-(X/Y)+(B^m*C^x)^y", "A^n-(X/Y)+((B^m*C^x)^y)")]
        [TestCase("A^n+(B^m*C^x*(E+F*G)+H^(1/2)/I^0.5)^y", "A^n+(((B^m*C^x*(E+(F*G)))+((H^(1/2))/I^0.5))^y)")]
        [TestCase("a/b+c/d+e/f", "((a/b)+(c/d))+(e/f)")]
        public void Initialize_As_String_With_Parentheses_Mixed_SumDifference_ProductQuotient(string value, string expectedLabel)
        {
            ProductQuotientSet symbolicSet = new ProductQuotientSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }
        #endregion

        #region Methods: Public
        [TestCase("a*b", ExpectedResult = "a*b")]
        [TestCase("a/b", ExpectedResult = "a/b")]
        [TestCase("-a*b", ExpectedResult = "-a*b")]
        [TestCase("-a/b", ExpectedResult = "-a/b")]
        [TestCase("(a*b)^m", ExpectedResult = "(a*b)^m")]
        [TestCase("(a/b)^m", ExpectedResult = "(a/b)^m")]
        [TestCase("-(a*b)^m", ExpectedResult = "-(a*b)^m")]
        [TestCase("-(a/b)^m", ExpectedResult = "-(a/b)^m")]
        [TestCase("-b*a^m", ExpectedResult = "-b*a^m")]
        [TestCase("-b/a^m", ExpectedResult = "-b/a^m")]
        [TestCase("-a^m*b", ExpectedResult = "-a^m*b")]
        [TestCase("-a^m/b", ExpectedResult = "-a^m/b")]
        [TestCase("-a^m*b^n", ExpectedResult = "-a^m*b^n")]
        [TestCase("-a^m/b^n", ExpectedResult = "-a^m/b^n")]
        [TestCase("(a^-m)*(b^-n)", ExpectedResult = "(a^-m)*(b^-n)")]
        [TestCase("(a^-m)/(b^-n)", ExpectedResult = "(a^-m)/(b^-n)")]
        [TestCase("a^m*(b^-n)", ExpectedResult = "a^m*(b^-n)")]
        [TestCase("a^m/(b^-n)", ExpectedResult = "a^m/(b^-n)")]
        [TestCase("-(a^-m)*(b^-n)", ExpectedResult = "-(a^-m)*(b^-n)")]
        [TestCase("-(a^-m)/(b^-n)", ExpectedResult = "-(a^-m)/(b^-n)")]
        [TestCase("(a^-m)*(b^-n)*(c^-o)", ExpectedResult = "(a^-m)*(b^-n)*(c^-o)")]
        [TestCase("(a^-m)/(b^-n)/(c^-o)", ExpectedResult = "(a^-m)/(b^-n)/(c^-o)")]
        [TestCase("-(a^-m)*(b^-n)*(c^-o)", ExpectedResult = "-(a^-m)*(b^-n)*(c^-o)")]
        [TestCase("-(a^-m)/(b^-n)/(c^-o)", ExpectedResult = "-(a^-m)/(b^-n)/(c^-o)")]
        [TestCase("(a^-m)*(b^-n)/(c^-o)", ExpectedResult = "(a^-m)*(b^-n)/(c^-o)")]
        [TestCase("(a^-m)/(b^-n)*(c^-o)", ExpectedResult = "(a^-m)/(b^-n)*(c^-o)")]
        [TestCase("-(a^-m)*(b^-n)/(c^-o)", ExpectedResult = "-(a^-m)*(b^-n)/(c^-o)")]
        [TestCase("-(a^-m)/(b^-n)*(c^-o)", ExpectedResult = "-(a^-m)/(b^-n)*(c^-o)")]
        [TestCase("-a^m*(b^-n)/c^o", ExpectedResult = "-a^m*(b^-n)/c^o")]
        [TestCase("-a^m/(b^-n)*c^o", ExpectedResult = "-a^m/(b^-n)*c^o")]
        [TestCase("-(a^-m)*b^n/(c^-o)", ExpectedResult = "-(a^-m)*b^n/(c^-o)")]
        [TestCase("-(a^-m)/b^n*(c^-o)", ExpectedResult = "-(a^-m)/b^n*(c^-o)")]
        [TestCase("-((a^-m)*(b^-n))^o", ExpectedResult = "-((a^-m)*(b^-n))^o")]
        [TestCase("-((a^-m)/(b^-n))^o", ExpectedResult = "-((a^-m)/(b^-n))^o")]
        [TestCase("(-(a^-m)*(b^-n))^o", ExpectedResult = "(-(a^-m)*(b^-n))^o")]
        [TestCase("(-(a^-m)/(b^-n))^o", ExpectedResult = "(-(a^-m)/(b^-n))^o")]
        [TestCase("(a*b)/(c*d)", ExpectedResult = "(a*b)/(c*d)")]
        [TestCase("a*b/(c*d)", ExpectedResult = "a*b/(c*d)")]
        [TestCase("(a*b*c)/(d*e*f)", ExpectedResult = "(a*b*c)/(d*e*f)")]
        [TestCase("a*b*c/(d*e*f)", ExpectedResult = "a*b*c/(d*e*f)")]
        [TestCase("((a*b*c)/d)*e*f", ExpectedResult = "((a*b*c)/d)*e*f")]
        [TestCase("a*b*c/d*e*f", ExpectedResult = "a*b*c/d*e*f")]
        public string Label_with_Primitive_Power(string value)
        {
            ProductQuotientSet unit = new ProductQuotientSet(value);
            return unit.Label();
        }

        [TestCase("(c*a)+b", ExpectedResult = "(c*a)+b")]
        [TestCase("(c/a)+b", ExpectedResult = "(c/a)+b")]
        [TestCase("(c*a)-b", ExpectedResult = "(c*a)-b")]
        [TestCase("(c/a)-b", ExpectedResult = "(c/a)-b")]
        [TestCase("b+(c*a)", ExpectedResult = "b+(c*a)")]
        [TestCase("b+(c/a)", ExpectedResult = "b+(c/a)")]
        [TestCase("b-(c*a)", ExpectedResult = "b-(c*a)")]
        [TestCase("b-(c/a)", ExpectedResult = "b-(c/a)")]
        public string Label_with_Mixed_Base_and_Primitive_Power(string value)
        {
            ProductQuotientSet unit = new ProductQuotientSet(value);
            return unit.Label();
        }

        // TODO: GetBase
        // TODO: GetPower

        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("a", true)]
        [TestCase("-a", true)]
        [TestCase("a*b", true)]
        [TestCase("-a*b", true)]
        [TestCase("a*-b", true)]
        [TestCase("-(a)", false)]
        [TestCase("(a*b)", true)]
        [TestCase("-(a*b)", false)]
        [TestCase("(a*-b)", true)]
        public void GetSign(string value, bool expectedPositive)
        {
            ProductQuotientSet unit = new ProductQuotientSet(value);
            Sign sign = unit.GetSign();
            Assert.That(sign.IsPositive(), Is.EqualTo(expectedPositive));
        }

        // TODO: PowerLabel
        // TODO: BaseLabel

        // TODO: IsInteger
        // TODO: IsFloat
        // TODO: IsFraction
        // TODO: IsNumber
        // TODO: IsSymbolic
        // TODO: IsEmpty
        // TODO: HasPower

        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("a", true)]
        [TestCase("-a", true)]
        [TestCase("a*b", true)]
        [TestCase("-a*b", true)]
        [TestCase("a*-b", true)]
        [TestCase("-(a)", false)]
        [TestCase("(a*b)", true)]
        [TestCase("-(a*b)", false)]
        [TestCase("(a*-b)", true)]
        public void SignIsNegative(string value, bool expectedPositive)
        {
            ProductQuotientSet unit = new ProductQuotientSet(value);
            Assert.That(unit.SignIsNegative(), Is.EqualTo(!expectedPositive));
        }
        
        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase(" ", true)]
        [TestCase("a", true)]
        [TestCase("-a", false)]
        [TestCase("a*b", true)]
        [TestCase("-a*b", false)]
        [TestCase("a*-b", false)]
        public void ValueIsNegative(string value, bool expectedPositive)
        {
            ProductQuotientSet unit = new ProductQuotientSet(value);
            Assert.That(unit.ValueIsNegative(), Is.EqualTo(!expectedPositive));
        }

        // TODO: FlipSign

        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase("a*b", ExpectedResult = "a*b")]
        [TestCase("-a*-b", ExpectedResult = "a*b")]
        [TestCase("-a*b", ExpectedResult = "-a*b")]
        [TestCase("a*-b", ExpectedResult = "-a*b")]
        [TestCase("a/b", ExpectedResult = "a/b")]
        [TestCase("-a/-b", ExpectedResult = "a/b")]
        [TestCase("-a/b", ExpectedResult = "-a/b")]
        [TestCase("a/-b", ExpectedResult = "-a/b")]
        [TestCase("-a^2", ExpectedResult = "-a^2")]
        public string ExtractSign(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.ExtractSign();
            return simplifiedSet.Label();
        }

        // TODO: DistributeSign
        
        [TestCase(null, ExpectedResult = "")]           // Null or Empty
        [TestCase("", ExpectedResult = "")]
        [TestCase("a", ExpectedResult = "a")]           // Single Unit
        [TestCase("-a", ExpectedResult = "a")]
        [TestCase("a^-b", ExpectedResult = "a^-b")]
        [TestCase("-a^-b", ExpectedResult = "a^-b")]
        [TestCase("a*b^c", ExpectedResult = "a*b^c")]   // Multi-Unit
        [TestCase("-(a*b^c)", ExpectedResult = "a*b^c")]
        [TestCase("-a*b^c", ExpectedResult = "a*b^c")]
        [TestCase("a*-b^c", ExpectedResult = "a*b^c")]
        [TestCase("a/b^c", ExpectedResult = "a/b^c")]
        [TestCase("-(a/b^c)", ExpectedResult = "a/b^c")]
        [TestCase("-a/b^c", ExpectedResult = "a/b^c")]
        [TestCase("a/-b^c", ExpectedResult = "a/b^c")]
        public string GetAbsolute(string value)
        {
            ProductQuotientSet unit = new ProductQuotientSet(value);
            IBase absoluteUnit = unit.GetAbsolute();
            return absoluteUnit.Label();
        }



        // TODO: AsInteger
        // TODO: AsFloat
        #endregion

        #region Methods: Aggregate Items
        [Test]
        public void MultiplyItem_Empty_Item_Returns_False()
        {
            ProductQuotientSet productQuotientSet = new ProductQuotientSet(5);
            Assert.That(productQuotientSet.Label(), Is.EqualTo("5"));

            IBase unit = new PrimitiveUnit(string.Empty);
            Assert.IsFalse(productQuotientSet.MultiplyItem(unit));
            Assert.That(productQuotientSet.Label(), Is.EqualTo("5"));
        }

        [Test]
        public void MultiplyItem_NonEmpty_Item_Returns_True()
        {
            ProductQuotientSet productQuotientSet = new ProductQuotientSet(5);
            Assert.That(productQuotientSet.Label(), Is.EqualTo("5"));

            IBase unit = new PrimitiveUnit("8");
            Assert.IsTrue(productQuotientSet.MultiplyItem(unit));
            Assert.That(productQuotientSet.Label(), Is.EqualTo("5*8"));
        }

        [Test]
        public void DivideItem_Empty_Item_Returns_False()
        {
            ProductQuotientSet productQuotientSet = new ProductQuotientSet(5);
            Assert.That(productQuotientSet.Label(), Is.EqualTo("5"));

            IBase unit = new PrimitiveUnit(string.Empty);
            Assert.IsFalse(productQuotientSet.DivideItem(unit));
            Assert.That(productQuotientSet.Label(), Is.EqualTo("5"));
        }

        [Test]
        public void DivideItem_NonEmpty_Item_Returns_True()
        {
            ProductQuotientSet productQuotientSet = new ProductQuotientSet(5);
            Assert.That(productQuotientSet.Label(), Is.EqualTo("5"));

            IBase unit = new PrimitiveUnit("8");
            Assert.IsTrue(productQuotientSet.DivideItem(unit));
            Assert.That(productQuotientSet.Label(), Is.EqualTo("5/8"));
        }

        // TODO: AppendItemToGroup
        #endregion

        #region Methods: Calculate - Single Value

        [TestCase("5.7", 5.7)]
        [TestCase("-5.7", -5.7)]
        [TestCase("(5.7)", 5.7)]
        [TestCase("(-5.7)", -5.7)]
        public void Calculate_With_Group_Sign_For_Single_Unit(string value, double expectedResult)
        {
            ProductQuotientSet symbolicValue = new ProductQuotientSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new ProductQuotientSet(value, null, isNegative: true);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(-1 * expectedResult));

            symbolicValue = new ProductQuotientSet(value, null, isNegative: false);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase("-(5.7)", -5.7)]
        [TestCase("-(-5.7)", 5.7)]
        public void Calculate_With_Group_Sign_Redundant_For_Single_Unit(string value, double expectedResult)
        {
            ProductQuotientSet symbolicValue = new ProductQuotientSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new ProductQuotientSet(value, null, isNegative: true);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new ProductQuotientSet(value, null, isNegative: false);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("0", double.NaN)]
        [TestCase("4", 1)]
        [TestCase("-4", 1)]
        [TestCase("5.7", 1)]
        [TestCase("-5.7", 1)]
        public void Calculate_With_Power_Zero_For_Single_Unit(string value, double expectedResult)
        {
            string exponent = "0";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            ProductQuotientSet symbolicValue = new ProductQuotientSet(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new ProductQuotientSet(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("0", 0)]
        [TestCase("4", 4)]
        [TestCase("-4", -4)]
        [TestCase("5.7", 5.7)]
        [TestCase("-5.7", -5.7)]
        public void Calculate_With_No_Exponent_For_Single_Unit(string value, double expectedResult)
        {
            ProductQuotientSet symbolicValue = new ProductQuotientSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("0", 0)]
        [TestCase("4", 4)]
        [TestCase("-4", -4)]
        [TestCase("5.7", 5.7)]
        [TestCase("-5.7", -5.7)]
        [TestCase("1/2", 0.5)]
        [TestCase("1/0", double.PositiveInfinity)]
        [TestCase("-1/0", double.NegativeInfinity)]
        [TestCase("0/0", double.NaN)]
        public void Calculate_With_Power_One_For_Single_Unit(string value, double expectedResult)
        {
            string exponent = "1";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            ProductQuotientSet symbolicValue = new ProductQuotientSet(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new ProductQuotientSet(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null, 0)]
        [TestCase("", 0)]
        [TestCase("Foo", 0)]
        [TestCase("2X", 0)]
        [TestCase("4", 16)]
        [TestCase("-4", 16)]
        [TestCase("5.7", 32.49)]
        [TestCase("-5.7", 32.49)]
        public void Calculate_With_Power_For_Single_Unit(string value, double expectedResult)
        {
            string exponent = "2";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            ProductQuotientSet symbolicValue = new ProductQuotientSet(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new ProductQuotientSet(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("Foo")]
        [TestCase("2X")]
        [TestCase("4")]
        [TestCase("-4")]
        [TestCase("5.7")]
        [TestCase("-5.7")]
        public void Calculate_With_Power_Symbolic_For_Single_Unit(string value)
        {
            string exponent = "Bar";
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            ProductQuotientSet symbolicValue = new ProductQuotientSet(value, power);
            Assert.That(symbolicValue.Calculate(), Is.EqualTo(0));

            symbolicValue = new ProductQuotientSet(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(0));
        }
        #endregion

        #region Methods: Calculate

        #endregion

        #region Methods: Combine Powers

        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase("a^(2*n)", ExpectedResult = "a^(2*n)")]
        [TestCase("-a^(2*n)", ExpectedResult = "-a^(2*n)")]
        [TestCase("a^(-2*n)", ExpectedResult = "a^-(2*n)")]
        [TestCase("a^-(2*n)", ExpectedResult = "a^-(2*n)")]
        [TestCase("1", ExpectedResult = "1")]
        [TestCase("a^2", ExpectedResult = "a^2")]
        [TestCase("2^(2*a)", ExpectedResult = "2^(2*a)")]
        [TestCase("2^2", ExpectedResult = "2^2")]
        [TestCase("a^(n+m)", ExpectedResult = "a^(n+m)")]
        [TestCase("-a^(n+m)", ExpectedResult = "-a^(n+m)")]
        [TestCase("a^(-n+-m)", ExpectedResult = "a^(-n+-m)")]
        [TestCase("a^(n+-m)", ExpectedResult = "a^(n+-m)")]
        [TestCase("a^(-n+m)", ExpectedResult = "a^(-n+m)")]
        [TestCase("a^-1", ExpectedResult = "a^-1")]
        [TestCase("(a*b)^n", ExpectedResult = "(a*b)^n")]
        [TestCase("-(a*b)^n", ExpectedResult = "-(a*b)^n")]
        [TestCase("(a*b)^-n", ExpectedResult = "(a*b)^-n")]
        [TestCase("(a/b)^n", ExpectedResult = "(a/b)^n")]
        [TestCase("-(a/b)^n", ExpectedResult = "-(a/b)^n")]
        [TestCase("(a/b)^-n", ExpectedResult = "(a/b)^-n")]
        [TestCase("a^n/b^m", ExpectedResult = "a^n/b^m")]
        [TestCase("a^n/-b^m", ExpectedResult = "a^n/-b^m")]
        [TestCase("a^n/(b^-m)", ExpectedResult = "a^n/(b^-m)")]
        public string CombinePowers_Does_Nothing_If_No_Powers_To_Combine(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.CombinePowers();
            return simplifiedSet.Label();
        }

        [TestCase("a^n*a^n", ExpectedResult = "a^(2*n)")]       // Base Case
        [TestCase("-a^n*-a^n", ExpectedResult = "a^(2*n)")]     // Signs at base
        [TestCase("-a^n*a^n", ExpectedResult = "-a^(2*n)")]
        [TestCase("a^n*-a^n", ExpectedResult = "-a^(2*n)")]
        [TestCase("a^-n*a^-n", ExpectedResult = "a^-(2*n)")]    // Signs at power   
        [TestCase("a^n*a^-n", ExpectedResult = "1")]
        [TestCase("a^-n*a^n", ExpectedResult = "1")]
        [TestCase("a*a", ExpectedResult = "a^2")]               // Numeric Power
        [TestCase("a^3*a^3", ExpectedResult = "a^6")]
        [TestCase("2^a*2^a", ExpectedResult = "2^(2*a)")]       // Numeric Base
        [TestCase("2*2", ExpectedResult = "2^2")]
        public string CombinePowers_Multiply_Same_Base_Same_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.CombinePowers();
            return simplifiedSet.Label();
        }

        [TestCase("a^n*a^m", ExpectedResult = "a^(n+m)")]       // Base Case
        [TestCase("-a^n*-a^m", ExpectedResult = "a^(n+m)")]     // Signs at base
        [TestCase("-a^n*a^m", ExpectedResult = "-a^(n+m)")]
        [TestCase("a^n*-a^m", ExpectedResult = "-a^(n+m)")]
        [TestCase("a^-n*a^-m", ExpectedResult = "a^(-n+-m)")]    // Signs at power   
        [TestCase("a^n*a^-m", ExpectedResult = "a^(n+-m)")]
        [TestCase("a^-n*a^m", ExpectedResult = "a^(-n+m)")]
        [TestCase("a^2*a^-3", ExpectedResult = "a^-1")]           // Numeric Power
        [TestCase("a^2*a^3", ExpectedResult = "a^5")]
        public string CombinePowers_Multiply_Same_Base_Different_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.CombinePowers();
            return simplifiedSet.Label();
        }

        [TestCase("a^n*b^n", ExpectedResult = "(a*b)^n")]       // Base Case
        [TestCase("-a^n*-b^n", ExpectedResult = "(a*b)^n")]     // Signs at base
        [TestCase("-a^n*b^n", ExpectedResult = "-(a*b)^n")]
        [TestCase("a^n*-b^n", ExpectedResult = "-(a*b)^n")]
        [TestCase("a^-n*b^-n", ExpectedResult = "(a*b)^-n")]    // Signs at power
        [TestCase("a^-n*b^n", ExpectedResult = "(a/b)^-n")]
        [TestCase("a^n*b^-n", ExpectedResult = "(a/b)^n")]
        public string CombinePowers_Multiply_Different_Base_Same_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.CombinePowers();
            return simplifiedSet.Label();
        }

        [TestCase("a^n/a^n", ExpectedResult = "1")]       // Base Case
        [TestCase("-a^n/-a^n", ExpectedResult = "1")]       // Signs at base
        [TestCase("-a^n/a^n", ExpectedResult = "-1")]
        [TestCase("a^n/-a^n", ExpectedResult = "-1")]
        [TestCase("a^-n/a^-n", ExpectedResult = "1")]     // Signs at power
        [TestCase("a^n/a^-n", ExpectedResult = "a^(2*n)")]
        [TestCase("a^-n/a^n", ExpectedResult = "a^-(2*n)")]
        [TestCase("a/a", ExpectedResult = "1")]               // Numeric Power
        [TestCase("a^3/a^3", ExpectedResult = "1")]
        [TestCase("2^a/2^a", ExpectedResult = "1")]       // Numeric Base
        [TestCase("2/2", ExpectedResult = "1")]
        public string CombinePowers_Divide_Same_Base_Same_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.CombinePowers();
            return simplifiedSet.Label();
        }

        [TestCase("a^n/a^m", ExpectedResult = "a^(n-m)")]       // Base Case
        [TestCase("-a^n/-a^m", ExpectedResult = "a^(n-m)")]       // Signs at base
        [TestCase("-a^n/a^m", ExpectedResult = "-a^(n-m)")]
        [TestCase("a^n/-a^m", ExpectedResult = "-a^(n-m)")]
        [TestCase("a^-n/a^-m", ExpectedResult = "a^(-n--m)")]     // Signs at power        
        [TestCase("a^n/a^-m", ExpectedResult = "a^(n--m)")]
        [TestCase("a^-n/a^m", ExpectedResult = "a^(-n-m)")]
        [TestCase("a^2/a^3", ExpectedResult = "a^-1")]          // Numeric Power
        [TestCase("a^5/a^3", ExpectedResult = "a^2")]
        [TestCase("a^2/b^3", ExpectedResult = "a^2/b^3")]       // Numeric Base
        public string CombinePowers_Divide_Same_Base_Different_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.CombinePowers();
            return simplifiedSet.Label();
        }

        [TestCase("a^n/b^n", ExpectedResult = "(a/b)^n")]       // Base Case
        [TestCase("-a^n/-b^n", ExpectedResult = "(a/b)^n")]     // Signs at base
        [TestCase("-a^n/b^n", ExpectedResult = "-(a/b)^n")]
        [TestCase("a^n/-b^n", ExpectedResult = "-(a/b)^n")]
        [TestCase("a^-n/b^-n", ExpectedResult = "(a/b)^-n")]    // Signs at power
        [TestCase("a^-n/b^n", ExpectedResult = "(a*b)^-n")]
        [TestCase("a^n/b^-n", ExpectedResult = "(a*b)^n")]
        public string CombinePowers_Divide_Different_Base_Same_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.CombinePowers();
            return simplifiedSet.Label();
        }

        [TestCase("a^n/b^m", ExpectedResult = "a^n/b^m")]      // Base Case
        [TestCase("-a^n/-b^m", ExpectedResult = "-a^n/-b^m")]    // Signs at base
        [TestCase("-a^n/b^m", ExpectedResult = "-a^n/b^m")]
        [TestCase("a^n/-b^m", ExpectedResult = "a^n/-b^m")]
        [TestCase("a^-n/b^-m", ExpectedResult = "(a^-n)/(b^-m)")]   // Signs at power
        [TestCase("a^-n/b^m", ExpectedResult = "(a^-n)/b^m")]
        [TestCase("a^n/b^-m", ExpectedResult = "a^n/(b^-m)")]
        public string CombinePowers_Divide_Different_Base_Different_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.CombinePowers();
            return simplifiedSet.Label();
        }
        #endregion

        #region Methods: Simplify

        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        public string Simplify_Nothing_Or_Empty_Returns_Empty(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n*a^n", ExpectedResult = "a^(2*n)")]       // Base Case
        [TestCase("-a^n*-a^n", ExpectedResult = "a^(2*n)")]     // Signs at base
        [TestCase("-a^n*a^n", ExpectedResult = "-a^(2*n)")]         
        [TestCase("a^n*-a^n", ExpectedResult = "-a^(2*n)")]             
        [TestCase("a^-n*a^-n", ExpectedResult = "a^-(2*n)")]    // Signs at power   
        [TestCase("a^n*a^-n", ExpectedResult = "1")]
        [TestCase("a^-n*a^n", ExpectedResult = "1")]
        [TestCase("a*a", ExpectedResult = "a^2")]               // Numeric Power
        [TestCase("a^3*a^3", ExpectedResult = "a^6")]
        [TestCase("2^a*2^a", ExpectedResult = "2^(2*a)")]       // Numeric Base
        [TestCase("2*2", ExpectedResult = "2^2")]
        public string Simplify_Multiply_Same_Base_Same_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n*a^m", ExpectedResult = "a^(n+m)")]       // Base Case
        [TestCase("-a^n*-a^m", ExpectedResult = "a^(n+m)")]     // Signs at base
        [TestCase("-a^n*a^m", ExpectedResult = "-a^(n+m)")]         
        [TestCase("a^n*-a^m", ExpectedResult = "-a^(n+m)")]         
        [TestCase("a^-n*a^-m", ExpectedResult = "a^-(n+m)")]    // Signs at power   
        [TestCase("a^n*a^-m", ExpectedResult = "a^(n-m)")]   
        [TestCase("a^-n*a^m", ExpectedResult = "a^-(n-m)")]
        [TestCase("a^2*a^-3", ExpectedResult = "a^-1")]           // Numeric Power
        [TestCase("a^2*a^3", ExpectedResult = "a^5")]           
        public string Simplify_Multiply_Same_Base_Different_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n*b^n", ExpectedResult = "(a*b)^n")]       // Base Case
        [TestCase("-a^n*-b^n", ExpectedResult = "(a*b)^n")]     // Signs at base
        [TestCase("-a^n*b^n", ExpectedResult = "-(a*b)^n")]
        [TestCase("a^n*-b^n", ExpectedResult = "-(a*b)^n")]
        [TestCase("a^-n*b^-n", ExpectedResult = "(a*b)^-n")]    // Signs at power
        [TestCase("a^-n*b^n", ExpectedResult = "(a/b)^-n")]
        [TestCase("a^n*b^-n", ExpectedResult = "(a/b)^n")]
        public string Simplify_Multiply_Different_Base_Same_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n*b^m", ExpectedResult = "a^n*b^m")]       // Base Case
        [TestCase("-a^n*-b^m", ExpectedResult = "a^n*b^m")]       // Signs at base
        [TestCase("-a^n*b^m", ExpectedResult = "-(a^n*b^m)")]
        [TestCase("a^n*-b^m", ExpectedResult = "-(a^n*b^m)")]       
        [TestCase("a^-n*b^-m", ExpectedResult = "(a^-n)*(b^-m)")]   // Signs at power
        [TestCase("a^-n*b^m", ExpectedResult = "(a^-n)*b^m")]
        [TestCase("a^n*b^-m", ExpectedResult = "a^n*(b^-m)")]
        [TestCase("a^2*b^3", ExpectedResult = "a^2*b^3")]          // Numeric Power
        [TestCase("a^5*b^3", ExpectedResult = "a^5*b^3")]
        [TestCase("2^a*3^b", ExpectedResult = "2^a*3^b")]       // Numeric Base
        public string Simplify_Multiply_Different_Base_Different_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n/a^n", ExpectedResult = "1")]       // Base Case
        [TestCase("-a^n/-a^n", ExpectedResult = "1")]       // Signs at base
        [TestCase("-a^n/a^n", ExpectedResult = "-1")]         
        [TestCase("a^n/-a^n", ExpectedResult = "-1")]       
        [TestCase("a^-n/a^-n", ExpectedResult = "1")]     // Signs at power
        [TestCase("a^n/a^-n", ExpectedResult = "a^(2*n)")]
        [TestCase("a^-n/a^n", ExpectedResult = "a^-(2*n)")]     
        [TestCase("a/a", ExpectedResult = "1")]               // Numeric Power
        [TestCase("a^3/a^3", ExpectedResult = "1")]
        [TestCase("2^a/2^a", ExpectedResult = "1")]       // Numeric Base
        [TestCase("2/2", ExpectedResult = "1")]
        public string Simplify_Divide_Same_Base_Same_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n/a^m", ExpectedResult = "a^(n-m)")]       // Base Case
        [TestCase("-a^n/-a^m", ExpectedResult = "a^(n-m)")]       // Signs at base
        [TestCase("-a^n/a^m", ExpectedResult = "-a^(n-m)")]      
        [TestCase("a^n/-a^m", ExpectedResult = "-a^(n-m)")]      
        [TestCase("a^-n/a^-m", ExpectedResult = "a^-(n-m)")]     // Signs at power        
        [TestCase("a^n/a^-m", ExpectedResult = "a^(n+m)")]
        [TestCase("a^-n/a^m", ExpectedResult = "a^-(n+m)")]        
        [TestCase("a^2/a^3", ExpectedResult = "a^-1")]          // Numeric Power
        [TestCase("a^5/a^3", ExpectedResult = "a^2")]
        [TestCase("a^2/b^3", ExpectedResult = "a^2/b^3")]       // Numeric Base
        public string Simplify_Divide_Same_Base_Different_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n/b^n", ExpectedResult = "(a/b)^n")]       // Base Case
        [TestCase("-a^n/-b^n", ExpectedResult = "(a/b)^n")]     // Signs at base
        [TestCase("-a^n/b^n", ExpectedResult = "-(a/b)^n")]
        [TestCase("a^n/-b^n", ExpectedResult = "-(a/b)^n")]
        [TestCase("a^-n/b^-n", ExpectedResult = "(a/b)^-n")]    // Signs at power
        [TestCase("a^-n/b^n", ExpectedResult = "(a*b)^-n")]
        [TestCase("a^n/b^-n", ExpectedResult = "(a*b)^n")]
        public string Simplify_Divide_Different_Base_Same_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n/b^m", ExpectedResult = "a^n/b^m")]      // Base Case
        [TestCase("-a^n/-b^m", ExpectedResult = "a^n/b^m")]    // Signs at base
        [TestCase("-a^n/b^m", ExpectedResult = "-(a^n/b^m)")]
        [TestCase("a^n/-b^m", ExpectedResult = "-(a^n/b^m)")]
        [TestCase("a^-n/b^-m", ExpectedResult = "(a^-n)/(b^-m)")]   // Signs at power
        [TestCase("a^-n/b^m", ExpectedResult = "(a^-n)/b^m")]
        [TestCase("a^n/b^-m", ExpectedResult = "a^n/(b^-m)")]
        public string Simplify_Divide_Different_Base_Different_Exponent(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a*a", ExpectedResult = "a^2")]
        [TestCase("2*a*a", ExpectedResult = "2*a^2")]
        [TestCase("a*2*a", ExpectedResult = "a^2*2")]
        [TestCase("-a*a", ExpectedResult = "-a^2")]       
        [TestCase("-2*a*a", ExpectedResult = "-2*a^2")]       
        [TestCase("a*-2*a", ExpectedResult = "-(a^2*2)")]       
        [TestCase("a*-2*a*b", ExpectedResult = "-(a^2*2*b)")]       
        [TestCase("a*-2*a*b*b^2", ExpectedResult = "-(a^2*2*b^3)")]       
        [TestCase("a^6*(1/a^4)", ExpectedResult = "a^2")]       
        public string SimplifyVariables_Duplicate_Multiply_Returns_Greater_Power(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.SimplifyVariables(true);
            return simplifiedSet.Label();
        }

        [TestCase("a/a", ExpectedResult = "1")]
        [TestCase("a/2/a", ExpectedResult = "1/2")]
        [TestCase("-a/a", ExpectedResult = "-1")]        
        [TestCase("a/-2/a", ExpectedResult = "-1/2")]        
        [TestCase("a/-2/a/b", ExpectedResult = "-(1/2)/b")]       
        [TestCase("a/-2/a/b/b^2", ExpectedResult = "-(1/2)/b^3")]      
        [TestCase("a/-2/a/(b^3/b^2)", ExpectedResult = "-(1/2)/b")]       
        [TestCase("a/-2/a/(b^3/b^2)", ExpectedResult = "-(1/2)/b")]       
        [TestCase("a^6/(1/a^4)", ExpectedResult = "a^10")]       
        public string SimplifyVariables_Duplicate_Divide_Returns_Lesser_Power(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.SimplifyVariables(true);
            return simplifiedSet.Label();
        }

        [TestCase("(a/b)*(c/d)", ExpectedResult = "(a*c)/(b*d)")]
        [TestCase("(1/2)*(1/2)", ExpectedResult = "1/4")]
        [TestCase("(1/2)*(2/3)", ExpectedResult = "1/3")]
        [TestCase("3*(1/2)", ExpectedResult = "3/2")]
        [TestCase("(1/2)*3", ExpectedResult = "3/2")]
        [TestCase("a^m*b^n", ExpectedResult = "a^m*b^n")]
        [TestCase("(a*b)^n", ExpectedResult = "(a*b)^n")]
        public string SimplifyFractional_Multiply_Returns_Fraction_Or_Integer(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.SimplifyFractional();
            return simplifiedSet.Label();
        }

        [TestCase("(a/b)/(c/d)", ExpectedResult = "(a*d)/(b*c)")]
        [TestCase("(1/2)/(1/2)", ExpectedResult = "1")]
        [TestCase("(1/2)/(2/3)", ExpectedResult = "3/4")]
        [TestCase("3/(1/2)", ExpectedResult = "6")]
        [TestCase("(1/2)/3", ExpectedResult = "1/6")]
        [TestCase("a^m/b^n", ExpectedResult = "a^m/b^n")]
        [TestCase("(a/b)^n", ExpectedResult = "(a/b)^n")]
        public string SimplifyFractional_Divide_Returns_Fraction_Or_Integer(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.SimplifyFractional();
            return simplifiedSet.Label();
        }

        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase("a*1", ExpectedResult = "a")]
        [TestCase("1*a", ExpectedResult = "a")]
        [TestCase("a/1", ExpectedResult = "a")]
        [TestCase("1/a", ExpectedResult = "1/a")]
        [TestCase("1*a*1", ExpectedResult = "a")]
        [TestCase("1/a/1", ExpectedResult = "1/a")]
        [TestCase("1*a*1*a", ExpectedResult = "a*a")]
        [TestCase("1/a/1/a", ExpectedResult = "1/a/a")]
        [TestCase("-(a*1)", ExpectedResult = "-a")]        
        [TestCase("-(1*a)", ExpectedResult = "-a")]        
        [TestCase("(a*1)^n", ExpectedResult = "a^n")]
        [TestCase("(1*a)^n", ExpectedResult = "a^n")]
        [TestCase("-(a*1)^n", ExpectedResult = "-a^n")]        
        [TestCase("-(1*a)^n", ExpectedResult = "-a^n")]        
        [TestCase("a^n*1", ExpectedResult = "a^n")]
        [TestCase("((a^n*1)+(b^m*1))/1", ExpectedResult = "a^n+b^m")]
        [TestCase("((a^n*1)-(b^m*1))/1", ExpectedResult = "a^n-b^m")]
        [TestCase("((1/1)/b)", ExpectedResult = "1/b")]
        public string SimplifyUnitsOfOne(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBaseSet simplifiedSet = set.SimplifyUnitsOfOne();
            return simplifiedSet.Label();
        }

        //[TestCase("A*B", "B*C", ExpectedResult = "A*B*C*D")]
        //[TestCase("(A*B)", "(B*C)", ExpectedResult = "A*B*C*D")]
        //[TestCase("A/B", "B/C", ExpectedResult = "A/B*C/D")]
        //[TestCase("(A/B)", "(B/C)", ExpectedResult = "A/B*C/D")]
        //public string Simplify_Sum_Difference_Operand_Groups(string value1, string value2)
        //{
        //    return "";
        //}

        [TestCase("a^2*a^3", ExpectedResult = "a^5")]
        [TestCase("a^6/(1/a^4)", ExpectedResult = "a^10")]
        [TestCase("a^6*(1/a^4)", ExpectedResult = "a^2")]
        public string Simplify_Symbolic_Powers(string value)
        {
            ProductQuotientSet set = new ProductQuotientSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^2", "a^3", ExpectedResult = "a^5")]
        [TestCase("a^6", "(1/a^4)", ExpectedResult = "a^2")]
        public string Simplify_Multiplied_Symbolic_Powers(string value1, string value2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);
            ProductQuotientSet set = set1 * set2;
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^5", "a^3", ExpectedResult = "a^2")]
        [TestCase("a^6", "(1/a^4)", ExpectedResult = "a^10")]
        public string Simplify_Divided_Symbolic_Powers(string value1, string value2)
        {
            ProductQuotientSet set1 = new ProductQuotientSet(value1);
            ProductQuotientSet set2 = new ProductQuotientSet(value2);
            ProductQuotientSet set = set1 / set2;
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }
        #endregion

        #region Overrides

        [Test]
        public void Override_Clone()
        {
            ProductQuotientSet setOriginal = new ProductQuotientSet("-2^3");
            object setClone = setOriginal.Clone();

            Assert.IsTrue(setOriginal.Equals(setClone));
        }

        [Test]
        public void Override_CloneUnit()
        {
            ProductQuotientSet setOriginal = new ProductQuotientSet("-2^3");
            ProductQuotientSet setClone = setOriginal.CloneSet();

            Assert.IsTrue(setOriginal.Equals(setClone));
        }
        #endregion
    }
}
