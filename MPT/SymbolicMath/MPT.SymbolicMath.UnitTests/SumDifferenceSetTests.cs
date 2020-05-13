using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{
    [TestFixture]
    public class SumDifferenceSetTests
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
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value, power);

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
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value, power);

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
        [TestCase("Foo", "", "Foo", "", "Foo", false, false, false, false)] // TODO: Add strings of numbers w/ exponents
        public void Initialize_As_String(string value, string exponent,
            string expectedBaseLabel, string expectedPowerLabel, string expectedLabel,
            bool isNumber, bool isFloat, bool isInteger, bool isFraction)
        {
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value, power);

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
            SumDifferenceSet symbolicSet = new SumDifferenceSet(unit, power);

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
        public bool Initialize_As_Integer_With_IBase(string value, string exponent, bool isNegative)
        {
            PrimitiveUnit unit = new PrimitiveUnit(value);
            PrimitiveUnit power = new PrimitiveUnit(exponent);
            SumDifferenceSet symbolicSet = new SumDifferenceSet(unit, power, isNegative);
            return symbolicSet.SignIsNegative();
        }

        // TODO: Factory
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
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value);

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
        [TestCase("A^n*B^m", "A^n*B^m")]
        [TestCase(" A*B ", "A*B")]
        [TestCase("A * B", "A*B")]
        [TestCase(" A * B ", "A*B")]
        [TestCase(" A * B E ", "A*BE")]
        [TestCase(" A / B E ", "A/BE")]
        public void Initialize_As_String_ProductQuotients(string value, string expectedLabel)
        {
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value);

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
        [TestCase("A+-B", "A+-B")]
        [TestCase("A--B", "A--B")]
        [TestCase("A*-B", "A*-B")]
        [TestCase("A/-B", "A/-B")]
        public void Initialize_As_String_SumDifference(string value, string expectedLabel)
        {
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }

        [TestCase("A*B-C", "(A*B)-C")]
        [TestCase("A/B-C", "(A/B)-C")]
        [TestCase("A+B*C", "A+(B*C)")]
        [TestCase("A-B/C", "A-(B/C)")]
        [TestCase("A+B-C*D/E^F", "A+B-(C*D/E^F)")]
        [TestCase("a/b+c/d", "(a/b)+(c/d)")]
        [TestCase("a/b-c/d", "(a/b)-(c/d)")]
        public void Initialize_As_String_Mixed_SumDifference_ProductQuotient(string value, string expectedLabel)
        {
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }
        #endregion

        #region Initialize: Check Group Parsing
        [TestCase("(A*B)", "A*B")]
        [TestCase("(A * B)", "A*B")]
        [TestCase("-(A*B)", "-(A*B)")]
        [TestCase("-(A*B)^n", "-(A*B)^n")]
        [TestCase("-(A/B)", "-(A/B)")]
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
        [TestCase("-(A/B/C)^(n*m)", "-(A/B/C)^(n*m)")]
        [TestCase("-(A/B/C)^(n*m/p)", "-(A/B/C)^(n*m/p)")]
        [TestCase("-(A/B)^(n*m)*o", "(-(A/B)^(n*m))*o")]
        public void Initialize_As_String_With_Parentheses_ProductQuotients(string value, string expectedLabel)
        {
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value);

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
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value);

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
        [TestCase("(A*B*C)+(D*E*F)", "(A*B*C)+(D*E*F)")]
        [TestCase("(A+B+C)*(D+E+F)", "(A+B+C)*(D+E+F)")]
        [TestCase("(A/B/C)-(D/E/F)", "(A/B/C)-(D/E/F)")]
        [TestCase("(A-B-C)/(D-E-F)", "(A-B-C)/(D-E-F)")]
        [TestCase("A^n+(B^m*C^x)^y", "A^n+((B^m*C^x)^y)")]
        [TestCase("(X/Y)+A^n-(B^m*C^x)^y", "(X/Y)+A^n-((B^m*C^x)^y)")]
        [TestCase("-(X/Y)+A^n-(B^m*C^x)^y", "-(X/Y)+A^n-((B^m*C^x)^y)")]
        [TestCase("A^n-(X/Y)+(B^m*C^x)^y", "A^n-(X/Y)+((B^m*C^x)^y)")]
        [TestCase("A^n+(B^m*C^x*(E+F*G)+H^(1/2)/I^0.5)^y", "A^n+(((B^m*C^x*(E+(F*G)))+((H^(1/2))/I^0.5))^y)")]
        public void Initialize_As_String_With_Parentheses_Mixed_SumDifference_ProductQuotient(string value, string expectedLabel)
        {
            SumDifferenceSet symbolicSet = new SumDifferenceSet(value);

            Assert.That(symbolicSet.Label(), Is.EqualTo(expectedLabel));
            Assert.That(symbolicSet.ToString(), Is.EqualTo(expectedLabel));
        }
        #endregion

        #region Methods: Public
        [TestCase("a+b", ExpectedResult = "a+b")]
        [TestCase("a-b", ExpectedResult = "a-b")]
        [TestCase("-a+b", ExpectedResult = "-a+b")]
        [TestCase("-a-b", ExpectedResult = "-a-b")]
        [TestCase("(a+b)^m", ExpectedResult = "(a+b)^m")]
        [TestCase("(a-b)^m", ExpectedResult = "(a-b)^m")]
        [TestCase("-(a+b)^m", ExpectedResult = "-(a+b)^m")]
        [TestCase("-(a-b)^m", ExpectedResult = "-(a-b)^m")]
        [TestCase("-b+a^m", ExpectedResult = "-b+a^m")]
        [TestCase("-b-a^m", ExpectedResult = "-b-a^m")]
        [TestCase("-a^m+b", ExpectedResult = "-a^m+b")]
        [TestCase("-a^m-b", ExpectedResult = "-a^m-b")]
        [TestCase("a^m+b^n", ExpectedResult = "a^m+b^n")]
        [TestCase("a^m-b^n", ExpectedResult = "a^m-b^n")]
        [TestCase("(a^-m)+(b^-n)", ExpectedResult = "(a^-m)+(b^-n)")]
        [TestCase("(a^-m)-(b^-n)", ExpectedResult = "(a^-m)-(b^-n)")]
        [TestCase("a^m+(b^-n)", ExpectedResult = "a^m+(b^-n)")]
        [TestCase("a^m-(b^-n)", ExpectedResult = "a^m-(b^-n)")]
        [TestCase("-((a^-m)+(b^-n))", ExpectedResult = "-((a^-m)+(b^-n))")]
        [TestCase("-((a^-m)-(b^-n))", ExpectedResult = "-((a^-m)-(b^-n))")]
        [TestCase("(a^-m)+(b^-n)+(c^-o)", ExpectedResult = "(a^-m)+(b^-n)+(c^-o)")]
        [TestCase("(a^-m)-(b^-n)-(c^-o)", ExpectedResult = "(a^-m)-(b^-n)-(c^-o)")]
        [TestCase("-(a^-m)+(b^-n)+(c^-o)", ExpectedResult = "-(a^-m)+(b^-n)+(c^-o)")]
        [TestCase("-(a^-m)-(b^-n)-(c^-o)", ExpectedResult = "-(a^-m)-(b^-n)-(c^-o)")]
        [TestCase("(a^-m)+(b^-n)-(c^-o)", ExpectedResult = "(a^-m)+(b^-n)-(c^-o)")]
        [TestCase("(a^-m)-(b^-n)+(c^-o)", ExpectedResult = "(a^-m)-(b^-n)+(c^-o)")]
        [TestCase("-(a^-m)+(b^-n)-(c^-o)", ExpectedResult = "-(a^-m)+(b^-n)-(c^-o)")]
        [TestCase("-(a^-m)-(b^-n)+(c^-o)", ExpectedResult = "-(a^-m)-(b^-n)+(c^-o)")]
        [TestCase("-((a^-m)+(b^-n))^o", ExpectedResult = "-((a^-m)+(b^-n))^o")]
        [TestCase("-((a^-m)-(b^-n))^o", ExpectedResult = "-((a^-m)-(b^-n))^o")]
        [TestCase("(-(a^-m)+(b^-n))^o", ExpectedResult = "(-(a^-m)+(b^-n))^o")]
        [TestCase("(-(a^-m)-(b^-n))^o", ExpectedResult = "(-(a^-m)-(b^-n))^o")]
        [TestCase("(a+b)-(c+d)", ExpectedResult = "(a+b)-(c+d)")]
        [TestCase("(a+b+c)-(d+e+f)", ExpectedResult = "(a+b+c)-(d+e+f)")]
        [TestCase("((a+b+c)-d)+e+f", ExpectedResult = "((a+b+c)-d)+e+f")]
        public string Label_with_Primitive_Power(string value)
        {
            SumDifferenceSet unit = new SumDifferenceSet(value);
            return unit.Label();
        }

        // TODO: GetBase
        // TODO: GetPower
        // TODO: GetSign

        // TODO: PowerLabel
        // TODO: BaseLabel

        // TODO: IsInteger
        // TODO: IsFloat
        // TODO: IsFraction
        // TODO: IsNumber
        // TODO: IsSymbolic
        // TODO: IsEmpty
        // TODO: HasPower

        // TODO: SignIsNegative

        // TODO: FlipSign

        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        [TestCase("a+b", ExpectedResult = "a+b")]
        [TestCase("-a+-b", ExpectedResult = "-(a+b)")]
        [TestCase("-a+b", ExpectedResult = "-(a-b)")]
        [TestCase("a+-b", ExpectedResult = "a-b")]
        [TestCase("a-b", ExpectedResult = "a-b")]
        [TestCase("-a--b", ExpectedResult = "-(a-b)")]
        [TestCase("-a-b", ExpectedResult = "-(a+b)")]
        [TestCase("a--b", ExpectedResult = "a+b")]
        public string ExtractSign(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
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
        [TestCase("a+b^c", ExpectedResult = "a+b^c")]   // Multi-Unit
        [TestCase("-(a+b^c)", ExpectedResult = "a+b^c")]
        [TestCase("-a+b^c", ExpectedResult = "-a+b^c")]
        [TestCase("a+-b^c", ExpectedResult = "a+-b^c")]
        [TestCase("a-b^c", ExpectedResult = "a-b^c")]
        [TestCase("-(a-b^c)", ExpectedResult = "a-b^c")]
        [TestCase("-a-b^c", ExpectedResult = "-a-b^c")]
        [TestCase("a--b^c", ExpectedResult = "a--b^c")]
        public string GetAbsolute(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase absoluteSet = set.GetAbsolute();
            return absoluteSet.Label();
        }

        // TODO: AsInteger
        // TODO: AsFloat
        #endregion

        #region Methods: Aggregate Items
        [Test]
        public void SumItem_Empty_Item_Returns_False()
        {
            SumDifferenceSet sumDifferenceSet = new SumDifferenceSet(5);
            Assert.That(sumDifferenceSet.Label(), Is.EqualTo("5"));

            IBase unit = new PrimitiveUnit(string.Empty);
            Assert.IsFalse(sumDifferenceSet.SumItem(unit));
            Assert.That(sumDifferenceSet.Label(), Is.EqualTo("5"));
        }

        [Test]
        public void SumItem_NonEmpty_Item_Returns_True()
        {
            SumDifferenceSet sumDifferenceSet = new SumDifferenceSet(5);
            Assert.That(sumDifferenceSet.Label(), Is.EqualTo("5"));

            IBase unit = new PrimitiveUnit("8");
            Assert.IsTrue(sumDifferenceSet.SumItem(unit));
            Assert.That(sumDifferenceSet.Label(), Is.EqualTo("5+8"));
        }

        [Test]
        public void SubtractItem_Empty_Item_Returns_False()
        {
            SumDifferenceSet sumDifferenceSet = new SumDifferenceSet(5);
            Assert.That(sumDifferenceSet.Label(), Is.EqualTo("5"));

            IBase unit = new PrimitiveUnit(string.Empty);
            Assert.IsFalse(sumDifferenceSet.SubtractItem(unit));
            Assert.That(sumDifferenceSet.Label(), Is.EqualTo("5"));
        }

        [Test]
        public void SubtractItem_NonEmpty_Item_Returns_True()
        {
            SumDifferenceSet sumDifferenceSet = new SumDifferenceSet(5);
            Assert.That(sumDifferenceSet.Label(), Is.EqualTo("5"));

            IBase unit = new PrimitiveUnit("8");
            Assert.IsTrue(sumDifferenceSet.SubtractItem(unit));
            Assert.That(sumDifferenceSet.Label(), Is.EqualTo("5-8"));
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
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new SumDifferenceSet(value, null, isNegative: true);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(-1*expectedResult));

            symbolicValue = new SumDifferenceSet(value, null, isNegative: false);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));
        }

        [TestCase("-(5.7)", -5.7)]
        [TestCase("-(-5.7)", 5.7)]
        public void Calculate_With_Group_Sign_Redundant_For_Single_Unit(string value, double expectedResult)
        {
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new SumDifferenceSet(value, null, isNegative: true);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new SumDifferenceSet(value, null, isNegative: false);

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
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new SumDifferenceSet(value + Query.POWER + exponent);

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
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

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
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new SumDifferenceSet(value + Query.POWER + exponent);

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
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value, power);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult));

            symbolicValue = new SumDifferenceSet(value + Query.POWER + exponent);

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
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value, power);
            Assert.That(symbolicValue.Calculate(), Is.EqualTo(0));
            
            symbolicValue = new SumDifferenceSet(value + Query.POWER + exponent);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(0));
        }
        #endregion

        #region Methods: Calculate
        [TestCase("2", 2)]//(1)                            
        [TestCase("-2", -2)]//(2)
        [TestCase("2^3", 8)]//(3)
        [TestCase("-2^3", -8)]//(4)
        [TestCase("2^-3", 0.125)]//(5)
        [TestCase("-2^-3", -0.125)]//(6)
        [TestCase("2^3^2", 64)]//(7)
        [TestCase("-2^-3^-2", 64)]//(8)
        [TestCase("-2^3^-2", 0.015625)]//(9)
        [TestCase("2^-3^2", 0.015625)]//(10)
        [TestCase("2^3^2^5", 1073741824)]//(11)
        [TestCase("-2^-3^-2^-5", 9.31323E-10)]//(12)
        public void Calculate_Primitive(string value, double expectedResult)
        {
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult).Within(0.0000000001));
        }


        [TestCase("2*3", 6)]//(13)                        
        [TestCase("2/3", 0.66666666666667)]//(14)
        [TestCase("2*3/4", 1.5)]//(15)
        [TestCase("2*3*4/5", 4.8)]//(16)
        [TestCase("2/3*4", 2.66666666666667)]//(17)
        [TestCase("2/3*4*5", 13.33333333333330)]//(18)
        [TestCase("2*3/4*5", 7.5)]//(19)
        [TestCase("2/3*4/5", 0.53333333333333)]//(20)
        [TestCase("2*3*4/5*6*7", 201.6)]//(21)
        [TestCase("2/3/4*5/6/7", 0.01984126984127)]//(22)
        public void Calculate_Product_Quotient(string value, double expectedResult)
        {
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult).Within(0.0000000001));
        }

        [TestCase("2^3*3^2", 72)]//(23)                
        [TestCase("2+3", 5)]//(24)
        [TestCase("2-3", -1)]//(25)
        [TestCase("2+3-4", 1)]//(26)
        [TestCase("2-3+4", 3)]//(27)
        [TestCase("2+3+4-5-6-7+8", -1)]//(28)
        [TestCase("2-3-4+5+6+7-8", 5)]//(29)
        [TestCase("2^3+3^2", 17)]//(30)
        [TestCase("2+-3", -1)]//(31)
        [TestCase("2--3", 5)]//(32)
        [TestCase("2*-3", -6)]//(33)
        [TestCase("2/-3", -0.66666666666666663)]//(34)
        public void Calculate_Sum_Difference(string value, double expectedResult)
        {
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult).Within(0.0000000001));
        }


        [TestCase("2*3-4", 2)]//(35)                  
        [TestCase("2/3-4", -3.33333333333333)]//(36)
        [TestCase("2+3*4", 14)]//(37)
        [TestCase("2-3/4", 1.25)]//(38)
        [TestCase("2+3-4*5/6^7", 4.99992855509831)]//(39)
        public void Calculate_Mixed(string value, double expectedResult)
        {
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult).Within(0.0000000001));
        }

       
        [TestCase("2^(3^2)", 512)]//(40)                    
        [TestCase("-2^(-3^-2)", double.NaN)]//(41)
        [TestCase("-2^(3^-2)", double.NaN)]//(42)         
        [TestCase("2^(-3^-2)", 1.08006)]//(41a)
        [TestCase("2^(3^-2)", 1.08006)]//(42a)
        [TestCase("2^(-3^2)", 512)]//(43)
        public void Calculate_Groups_Primitive(string value, double expectedResult)
        {
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult).Within(0.000001));
        }

        [TestCase("(2*3)", 6)]//(44)                     
        [TestCase("-(2*3)", -6)]//(45)
        [TestCase("-(2*3)^3", -216)]//(46)
        [TestCase("-(2/3)", -0.6666666667)]//(47)
        [TestCase("-(2/3)^3", -0.2962962963)]//(48)
        [TestCase("(2*3)*(4*5)", 120)]//(49)
        [TestCase("(2/3)/(4/5)", 0.8333333333)]//(50)
        [TestCase("(2*3*4)*(5*6*7)", 5040)]//(51)
        [TestCase("(2/3/4)/(5/6/7)", 1.4)]//(52)
        [TestCase("(2/3)^3*(3*4)^2", 42.6666666667)]//(53)
        [TestCase("-(2/3)^3*(3*4)^2", -42.66666666670)]//(54)
        [TestCase("-(2/3/4)^3*(4*5*6)^2", -66.6666666667)]//(55)
        [TestCase("-((2/3)^3*(3*4)^2)^4", 3314017.975308639)]//(56)
        [TestCase("-((2/3)^3*(3*4)^2)^4*5", 16570089.8765432)]//(57)
        [TestCase("-(2/3)^(3*2)", 0.0877914952)]//(58)
        [TestCase("-(2/3/4)^(3*2)", 0.0000214335)]//(59)
        [TestCase("-(2/3/4)^(3*2/5)", double.NaN)]//(60)
        public void Calculate_Groups_Product_Quotient(string value, double expectedResult)
        {
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult).Within(0.00000001));
        }

        [TestCase("-(2/3)^(3*2)*4", 0.3511659808)]//(61)   
        [TestCase("(2+3)", 5)]//(62)
        [TestCase("-(2+3)", -5)]//(63)
        [TestCase("-(2+3)^3", -125)]//(64)
        [TestCase("(2+3)+(4+5)", 14)]//(65)
        [TestCase("(2-3)-(4-5)", 0.0)]//(66)
        [TestCase("(2-3)+(4-5)", -2)]//(67)
        [TestCase("(2+3)-(4+5)", -4)]//(68)
        [TestCase("(2+3+4)+(5+6+7)", 27)]//(69)
        [TestCase("(2-3-4)-(5-6-7)", 3)]//(70)
        [TestCase("(2-3-4)+(5-6-7)", -13)]//(71)
        [TestCase("(2+3+4)-(5+6+7)", -9)]//(72)
        public void Calculate_Groups_Sum_Difference(string value, double expectedResult)
        {
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult).Within(0.0000000001));
        }

        [TestCase("2*(3+4)", 14)]//(73)                    
        [TestCase("2+(3+4)", 9)]//(74)
        [TestCase("2+(3*4)", 14)]//(75)
        [TestCase("(2*3)+(4*5)", 26)]//(76)
        [TestCase("(2+3)*(4+5)", 45)]//(77)
        [TestCase("(2/3)-(4/5)", -0.1333333333)]//(78)
        [TestCase("(2-3)/(4-5)", 1.0)]//(79)
        [TestCase("(2*3*4)+(5*6*7)", 234)]//(80)
        [TestCase("(2+3+4)*(5+6+7)", 162)]//(81)
        [TestCase("(2/3/4)-(5/6/7)", 0.0476190476)]//(82)
        [TestCase("(2-3-4)/(5-6-7)", 0.625)]//(83)
        [TestCase("2^3+(3^2*4^0.5)^0.25", 10.059767144)]//(84)
        [TestCase("(0.5/0.25)+2^3-(3^2*4^0.5)^0.25", 7.940232856)]//(85)
        [TestCase("-(0.5/0.25)+2^3-(3^2*4^0.5)^0.25", 3.940232856)]//(86)
        [TestCase("2^3-(0.5/0.25)+(3^2*4^0.5)^0.25", 8.059767144)]//(87)
        [TestCase("2^3+(3^2*4^0.5*(6+7*8)+9^(1/2)/10^0.5)^0.25", 13.78107103)]//(88)
        public void Calculate_Groups_Mixed(string value, double expectedResult)
        {
            SumDifferenceSet symbolicValue = new SumDifferenceSet(value);

            Assert.That(symbolicValue.Calculate(), Is.EqualTo(expectedResult).Within(0.00000001));
        }
        #endregion

        #region Methods: Simplify
        // TODO: For SumDifference & ProductQuotient, factor out combining power & other relevant tests from this
        [TestCase(null, ExpectedResult = "")]
        [TestCase("", ExpectedResult = "")]
        public string Simplify_Nothing_Or_Empty_Returns_Empty(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n+a^n", ExpectedResult = "2*a^n")]       // Base Case
        [TestCase("-a^n+-a^n", ExpectedResult = "-(2*a^n)")]     // Signs at base
        [TestCase("-a^n+a^n", ExpectedResult = "0")]         
        [TestCase("a^n+-a^n", ExpectedResult = "0")]            
        [TestCase("a^-n+a^-n", ExpectedResult = "2*(a^-n)")]    // Signs at power   
        [TestCase("a^n+a^-n", ExpectedResult = "a^n+(a^-n)")]   
        [TestCase("a^-n+a^n", ExpectedResult = "(a^-n)+a^n")]            
        [TestCase("a+a", ExpectedResult = "2*a")]               // Numeric Power
        [TestCase("a^3+a^3", ExpectedResult = "2*a^3")]
        [TestCase("2^a+2^a", ExpectedResult = "2*2^a")]       // Numeric Base
        [TestCase("2+2", ExpectedResult = "2*2")]
        [TestCase("3+3", ExpectedResult = "2*3")]
        public string Simplify_Sum_Same_Base_Same_Exponent(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n+a^m", ExpectedResult = "a^n+a^m")]       // Base Case
        [TestCase("-a^n+-a^m", ExpectedResult = "-(a^n+a^m)")]     // Signs at base
        [TestCase("-a^n+a^m", ExpectedResult = "-(a^n-a^m)")]         
        [TestCase("a^n+-a^m", ExpectedResult = "a^n-a^m")]         
        [TestCase("a^-n+a^-m", ExpectedResult = "(a^-n)+(a^-m)")]    // Signs at power    
        [TestCase("a^n+a^-m", ExpectedResult = "a^n+(a^-m)")]      
        [TestCase("a^-n+a^m", ExpectedResult = "(a^-n)+a^m")]      
        [TestCase("a^2+a^-3", ExpectedResult = "a^2+(a^-3)")]           // Numeric Power      
        [TestCase("a^2+a^3", ExpectedResult = "a^2+a^3")]
        public string Simplify_Sum_Same_Base_Different_Exponent(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n+b^n", ExpectedResult = "a^n+b^n")]       // Base Case
        [TestCase("-a^n+-b^n", ExpectedResult = "-(a^n+b^n)")]     // Signs at base
        [TestCase("-a^n+b^n", ExpectedResult = "-(a^n-b^n)")]
        [TestCase("a^n+-b^n", ExpectedResult = "a^n-b^n")]
        [TestCase("a^-n+b^-n", ExpectedResult = "(a^-n)+(b^-n)")]    // Signs at power   
        [TestCase("a^-n+b^n", ExpectedResult = "(a^-n)+b^n")]   
        [TestCase("a^n+b^-n", ExpectedResult = "a^n+(b^-n)")]   
        public string Simplify_Sum_Different_Base_Same_Exponent(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n+b^m", ExpectedResult = "a^n+b^m")]       // Base Case
        [TestCase("-a^n+-b^m", ExpectedResult = "-(a^n+b^m)")]       // Signs at base
        [TestCase("-a^n+b^m", ExpectedResult = "-(a^n-b^m)")]
        [TestCase("a^n+-b^m", ExpectedResult = "a^n-b^m")]       
        [TestCase("a^-n+b^-m", ExpectedResult = "(a^-n)+(b^-m)")]   // Signs at power   
        [TestCase("a^-n+b^m", ExpectedResult = "(a^-n)+b^m")]   
        [TestCase("a^n+b^-m", ExpectedResult = "a^n+(b^-m)")]   
        [TestCase("a^2+b^3", ExpectedResult = "a^2+b^3")]          // Numeric Power
        [TestCase("a^5+b^3", ExpectedResult = "a^5+b^3")]
        [TestCase("2^a+3^b", ExpectedResult = "2^a+3^b")]       // Numeric Base
        public string Simplify_Sum_Different_Base_Different_Exponent(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n-a^n", ExpectedResult = "0")]       // Base Case
        [TestCase("-a^n--a^n", ExpectedResult = "0")]       // Signs at base
        [TestCase("-a^n-a^n", ExpectedResult = "-(2*a^n)")]         
        [TestCase("a^n--a^n", ExpectedResult = "2*a^n")]       
        [TestCase("a^-n-a^-n", ExpectedResult = "0")]     // Signs at power
        [TestCase("a^n-a^-n", ExpectedResult = "a^n-(a^-n)")]          
        [TestCase("a^-n-a^n", ExpectedResult = "(a^-n)-a^n")]     
        [TestCase("a-a", ExpectedResult = "0")]               // Numeric Power
        [TestCase("a^3-a^3", ExpectedResult = "0")]
        [TestCase("2^a-2^a", ExpectedResult = "0")]       // Numeric Base
        [TestCase("2-2", ExpectedResult = "0")]
        public string Simplify_Subtract_Same_Base_Same_Exponent(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n-a^m", ExpectedResult = "a^n-a^m")]       // Base Case
        [TestCase("-a^n--a^m", ExpectedResult = "-(a^n-a^m)")]       // Signs at base
        [TestCase("-a^n-a^m", ExpectedResult = "-(a^n+a^m)")]      
        [TestCase("a^n--a^m", ExpectedResult = "a^n+a^m")]      
        [TestCase("a^-n-a^-m", ExpectedResult = "(a^-n)-(a^-m)")]     // Signs at power       
        [TestCase("a^n-a^-m", ExpectedResult = "a^n-(a^-m)")]  
        [TestCase("a^-n-a^m", ExpectedResult = "(a^-n)-a^m")]           
        [TestCase("a^2-a^3", ExpectedResult = "a^2-a^3")]          // Numeric Power
        [TestCase("a^5-a^3", ExpectedResult = "a^5-a^3")]
        [TestCase("a^2-b^3", ExpectedResult = "a^2-b^3")]       // Numeric Base
        public string Simplify_Subtract_Same_Base_Different_Exponent(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n-b^n", ExpectedResult = "a^n-b^n")]       // Base Case
        [TestCase("-a^n--b^n", ExpectedResult = "-(a^n-b^n)")]     // Signs at base
        [TestCase("-a^n-b^n", ExpectedResult = "-(a^n+b^n)")]
        [TestCase("a^n--b^n", ExpectedResult = "a^n+b^n")]
        [TestCase("a^-n-b^-n", ExpectedResult = "(a^-n)-(b^-n)")]    // Signs at power    
        [TestCase("a^-n-b^n", ExpectedResult = "(a^-n)-b^n")]    
        [TestCase("a^n-b^-n", ExpectedResult = "a^n-(b^-n)")]     
        public string Simplify_Subtract_Different_Base_Same_Exponent(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }

        [TestCase("a^n-b^m", ExpectedResult = "a^n-b^m")]      // Base Case
        [TestCase("-a^n--b^m", ExpectedResult = "-(a^n-b^m)")]    // Signs at base
        [TestCase("-a^n-b^m", ExpectedResult = "-(a^n+b^m)")]
        [TestCase("a^n--b^m", ExpectedResult = "a^n+b^m")]
        [TestCase("a^-n-b^-m", ExpectedResult = "(a^-n)-(b^-m)")]   // Signs at power  
        [TestCase("a^-n-b^m", ExpectedResult = "(a^-n)-b^m")]         
        [TestCase("a^n-b^-m", ExpectedResult = "a^n-(b^-m)")]     
        public string Simplify_Subtract_Different_Base_Different_Exponent(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.Simplify();
            return simplifiedSet.Label();
        }
        
        [TestCase("a+a", ExpectedResult = "2*a")]
        [TestCase("a+b+a", ExpectedResult = "(2*a)+b")]   // TODO: Fix label to return 2*a+b?
        [TestCase("a+2*a", ExpectedResult = "3*a")]
        [TestCase("-a+a", ExpectedResult = "0")]
        [TestCase("-a+b+a", ExpectedResult = "b")]
        [TestCase("a+-2*a", ExpectedResult = "-a")]   
        [TestCase("a+-2*a+b", ExpectedResult = "-a+b")]   
        [TestCase("a+-2*a+b+b^2", ExpectedResult = "-a+b+b^2")]  
        public string SimplifyVariables_Duplicate_Add_Returns_Greater_Multiple(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.SimplifyVariables();
            return simplifiedSet.Label();
        }

        [TestCase("a-a", ExpectedResult = "0")]
        [TestCase("a-b-a", ExpectedResult = "-b")] 
        [TestCase("a-2*a", ExpectedResult = "-a")] 
        [TestCase("-a-a", ExpectedResult = "-2*a")]   
        [TestCase("-a-b-a", ExpectedResult = "-(2*a)-b")]  // TODO: Fix label to return -2*a-b?
        [TestCase("a--2*a", ExpectedResult = "3*a")]
        [TestCase("a--2*a-b", ExpectedResult = "(3*a)-b")]  // TODO: Fix label to return 3*a-b?
        [TestCase("a--2*a-b-b^2", ExpectedResult = "(3*a)-b-b^2")]  // TODO: Fix label to return 3*a-b-b^2?
        public string SimplifyVariables_Duplicate_Subtract_Returns_Lesser_Multiple(string value)
        {
            SumDifferenceSet set = new SumDifferenceSet(value);
            IBase simplifiedSet = set.SimplifyVariables();
            return simplifiedSet.Label();
        }

        [TestCase("a/b", "c/d", ExpectedResult = "((a*d)+(c*b))/(b*d)")]   // TODO: Fix label to return (a*d+c*b)/(b*d)?
        [TestCase("1/2", "1/2", ExpectedResult = "1")]
        [TestCase("1/2", "2/3", ExpectedResult = "7/6")]
        [TestCase("2/3", "1/2", ExpectedResult = "7/6")]
        [TestCase("1", "1/2", ExpectedResult = "3/2")]
        [TestCase("1/2", "1", ExpectedResult = "3/2")]
        [TestCase("a^n", "b^m", ExpectedResult = "a^n+b^m")]
        [TestCase("-a^n", "b^m", ExpectedResult = "-a^n+b^m")]
        public string Simplify_Fractional_Add_Returns_Fraction_Or_Integer(string value1, string value2)
        {
            ProductQuotientSet setLeft = new ProductQuotientSet(value1);
            ProductQuotientSet setRight = new ProductQuotientSet(value2);
            SumDifferenceSet set = new SumDifferenceSet(setLeft);
            set.SumItem(setRight);

            IBase simplifiedSet = set.SimplifyFractional();
            return simplifiedSet.Label();
        }

        [TestCase("a/b", "c/d", ExpectedResult = "((a*d)-(c*b))/(b*d)")]     // TODO: Fix label to return (a*d-c*b)/(b*d)?
        [TestCase("1/2", "1/2", ExpectedResult = "0")]
        [TestCase("1/2", "2/3", ExpectedResult = "-1/6")]
        [TestCase("2/3", "1/2", ExpectedResult = "1/6")]
        [TestCase("1", "1/2", ExpectedResult = "1/2")]
        [TestCase("1/2", "1", ExpectedResult = "-1/2")]
        [TestCase("a^n", "b^m", ExpectedResult = "a^n-b^m")]
        [TestCase("-a^n", "b^m", ExpectedResult = "-a^n-b^m")]
        public string Simplify_Fractional_Subtract_Returns_Fraction_Or_Integer(string value1, string value2)
        {
            ProductQuotientSet setLeft = new ProductQuotientSet(value1);
            ProductQuotientSet setRight = new ProductQuotientSet(value2);
            SumDifferenceSet set = new SumDifferenceSet(setLeft);
            set.SubtractItem(setRight);

            IBase simplifiedSet = set.SimplifyFractional();
            return simplifiedSet.Label();
        }

        //[TestCase("A+B", "B+C", ExpectedResult = "A+B+C+D")]
        //[TestCase("(A+B)", "(B+C)", ExpectedResult = "A+B+C+D")]
        //[TestCase("A-B", "B-C", ExpectedResult = "A-B+C-D")]
        //[TestCase("(A-B)", "(B-C)", ExpectedResult = "A-B+C-D")]
        //public string Simplify_Sum_Difference_Operand_Groups(string value1, string value2)
        //{
        //    return "";
        //}
        #endregion

        #region Overrides

        [Test]
        public void Override_Clone()
        {
            SumDifferenceSet setOriginal = new SumDifferenceSet("-2^3");
            object setClone = setOriginal.Clone();

            Assert.IsTrue(setOriginal.Equals(setClone));
        }

        [Test]
        public void Override_CloneUnit()
        {
            SumDifferenceSet setOriginal = new SumDifferenceSet("-2^3");
            SumDifferenceSet setClone = setOriginal.CloneSet();

            Assert.IsTrue(setOriginal.Equals(setClone));
        }
        #endregion
    }
}
