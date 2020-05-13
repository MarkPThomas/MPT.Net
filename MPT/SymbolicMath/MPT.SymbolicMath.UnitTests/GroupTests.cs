using System;
using NUnit.Framework;

namespace MPT.SymbolicMath.UnitTests
{

    [TestFixture]
    public class GroupTests
    {
        [TestCase(null, ExpectedResult = -1)]
        [TestCase("", ExpectedResult = -1)]
        [TestCase("A", ExpectedResult = -1)]
        [TestCase("A+B", ExpectedResult = -1)]
        [TestCase("A+B+C+D)", ExpectedResult = -1)]
        [TestCase("A+B+(C+D)", ExpectedResult = 4)]
        [TestCase("A+B+((C+D)*E)", ExpectedResult = 4)]
        public int IndexOfStartOfBracketGroup(string value)
        {
            return Group.IndexOfStartOfBracketGroup(value);
        }

        [TestCase(null, ExpectedResult = -1)]
        [TestCase("", ExpectedResult = -1)]
        [TestCase("A", ExpectedResult = -1)]
        [TestCase("A+B", ExpectedResult = -1)]
        [TestCase("A+B+C+D)", ExpectedResult = -1)]
        [TestCase("A+B+(C+D)*(E-F)", ExpectedResult = 8)]
        [TestCase("A+B+((C+D)*E)*(E-F)", ExpectedResult = 12)]
        public int IndexOfEndOfBracketGroup(string value)
        {
            return Group.IndexOfEndOfBracketGroup(value);
        }

        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("()", ExpectedResult = true)]
        [TestCase("([])", ExpectedResult = true)]
        [TestCase("([{}])", ExpectedResult = true)]
        [TestCase("(", ExpectedResult = false)]
        [TestCase("([)", ExpectedResult = false)]
        [TestCase("([{])", ExpectedResult = false)]
        [TestCase("A+B", ExpectedResult = true)]
        [TestCase("(A+B)", ExpectedResult = true)]
        [TestCase("(A)+(B)", ExpectedResult = true)]
        [TestCase("(A)+B", ExpectedResult = true)]
        [TestCase("(A)+[B", ExpectedResult = false)]
        [TestCase("(A+B))", ExpectedResult = false)]
        [TestCase("((A+B)", ExpectedResult = false)]
        [TestCase(")(A+B)", ExpectedResult = false)]
        [TestCase("(A+}B)", ExpectedResult = false)]
        [TestCase("(A+B)^(C+D))", ExpectedResult = false)]
        [TestCase("(A+B)^((C+D)", ExpectedResult = false)]
        [TestCase("(A+B)^)(C+D)", ExpectedResult = false)]
        [TestCase("(A+B)+(A-B)", ExpectedResult = true)]
        [TestCase("(A+B)-(A-B)", ExpectedResult = true)]
        [TestCase("(A+B)*(A-B)", ExpectedResult = true)]
        [TestCase("(A+B)(A-B)", ExpectedResult = true)]
        [TestCase("(A+B)/(A-B)", ExpectedResult = true)]
        public bool BracketsAreBalanced(string value)
        {
            return Group.BracketsAreBalanced(value);
        }

        [TestCase("A+B", ExpectedResult = true)]
        [TestCase("(A+B)", ExpectedResult = true)]
        [TestCase("(A)+(B)", ExpectedResult = false)]
        [TestCase("(A+B)^(C+D))", ExpectedResult = false)]
        [TestCase("(A+B)^((C+D)", ExpectedResult = false)]
        [TestCase("(A+B)^)(C+D)", ExpectedResult = false)]
        [TestCase("(A+B)+(A-B)", ExpectedResult = false)]
        [TestCase("(A+B)-(A-B)", ExpectedResult = false)]
        [TestCase("(A+B)*(A-B)", ExpectedResult = false)]
        [TestCase("(A+B)(A-B)", ExpectedResult = false)]
        [TestCase("(A+B)/(A-B)", ExpectedResult = false)]
        public bool BracketsAreBalanced_Single_Bracket_Set_Allowed(string value)
        {
            return Group.BracketsAreBalanced(value, limitToSingleBrackets: true);
        }


        [TestCase(null, ExpectedResult = '\0')]
        [TestCase('\0', ExpectedResult = '\0')]
        [TestCase('a', ExpectedResult = '\0')]
        [TestCase('(', ExpectedResult = ')')]
        [TestCase('[', ExpectedResult = ']')]
        [TestCase('{', ExpectedResult = '}')]
        [TestCase('<', ExpectedResult = '>')]
        [TestCase(')', ExpectedResult = '\0')]
        [TestCase(']', ExpectedResult = '\0')]
        [TestCase('}', ExpectedResult = '\0')]
        [TestCase('>', ExpectedResult = '\0')]
        public char ClosingBracket(char openingBracket)
        {
            return Group.ClosingBracket(openingBracket);
        }


        [TestCase(null, ExpectedResult = false)]
        [TestCase("", ExpectedResult = false)]
        [TestCase("(A*B)", ExpectedResult = true)]
        [TestCase("-(A*B)", ExpectedResult = false)]    // Currently literal. Does not consider outside exceptions.
        [TestCase("(A*B)^C", ExpectedResult = false)]   // Currently literal. Does not consider outside exceptions.
        [TestCase("(A*B*C*D)", ExpectedResult = true)]
        [TestCase("[A*B*C*D]", ExpectedResult = true)]
        [TestCase("{A*B*C*D}", ExpectedResult = true)]
        [TestCase("<A*B*C*D>", ExpectedResult = true)]
        [TestCase("(A*B*C*D>", ExpectedResult = false)]     // Imbalanced bracket types. 
        [TestCase("[A*B*C*D)", ExpectedResult = false)]
        [TestCase("{A*B*C*D]", ExpectedResult = false)]
        [TestCase("<A*B*C*D}", ExpectedResult = false)]
        [TestCase("(A*(B*C)*D)", ExpectedResult = true)]    // Inner brackets
        [TestCase("(A*B)*(C*D)", ExpectedResult = false)]    // Divided brackets. This is a limitation of the method being simple. TODO: Consider handling this better
        public bool HasOuterParentheses(string value)
        {
            return Group.HasOuterParentheses(value);
        }

        [TestCase(null, false, "", "", -1, '\0', '\0')]
        [TestCase("", false, "", "", -1, '\0', '\0')]
        [TestCase("()", false, "", "", 2, '(', '\0')]
        [TestCase("(A)", false, "A", "", 3, '(', '\0')]
        [TestCase("[A]", false, "A", "", 3, '[', '\0')]
        [TestCase("{A}", false, "A", "", 3, '{', '\0')]
        [TestCase("<A>", false, "A", "", 3, '<', '\0')]
        [TestCase("A", false, "A", "", 1, '\0', '\0')]
        [TestCase("-(A)", true, "A", "", 4, '(', '\0')]
        [TestCase("-A", false, "-A", "", 2, '\0', '\0')]
        [TestCase("(A)^B", false, "A", "B", 5, '(', '\0')]
        [TestCase("A^B", false, "A^B", "", 3, '\0', '\0')]
        [TestCase("(A^B)+C", false, "A^B", "", 5, '(', '\0')]
        [TestCase("-(A)^B", true, "A", "B", 6, '(', '\0')]
        [TestCase("-(A)^B+C", true, "A", "B", 6, '(', '\0')]
        [TestCase("(-(A)^B)+C", false, "-(A)^B", "", 8, '(', '\0')]
        [TestCase("(A+B)", false, "A+B", "", 5, '(', '\0')]
        [TestCase("(A+B)+C", false, "A+B", "", 5, '(', '\0')]
        [TestCase("(A+B)-C", false, "A+B", "", 5, '(', '\0')]
        [TestCase("(A+B)*C", false, "A+B", "", 5, '(', '\0')]
        [TestCase("(A+B)/C", false, "A+B", "", 5, '(', '\0')]
        [TestCase("(A+B)^C", false, "A+B", "C", 7, '(', '\0')]
        [TestCase("-(A+B)", true, "A+B", "", 6, '(', '\0')]
        [TestCase("(A+B)^(C+D)", false, "A+B", "C+D", 11, '(', '(')]
        [TestCase("(A+B)^[C+D]", false, "A+B", "C+D", 11, '(', '[')]
        [TestCase("-(A+B)^[C+D]", true, "A+B", "C+D", 12, '(', '[')]
        [TestCase("(A+B)^-[C+D]", false, "A+B", "-[C+D]", 12, '(', '[')]
        [TestCase("-(A+B)^-[C+D]", true, "A+B", "-[C+D]", 13, '(', '[')]
        [TestCase("(A+B*{D/E})", false, "A+B*{D/E}", "", 11, '(', '\0')]    
        [TestCase("(A+B^C*{D/E})", false, "A+B^C*{D/E}", "", 13, '(', '\0')]
        [TestCase("(A+B^(C+G)*{D/E})", false, "A+B^(C+G)*{D/E}", "", 17, '(', '\0')]
        [TestCase("-(A+B^(C+G)*{D/E})^[H/K+(L-M)]", true, "A+B^(C+G)*{D/E}", "H/K+(L-M)", 30, '(', '[')]
        public void Initialize(string value, bool expectedIsNegative, string expectedBase, string expectedPower, int expectedContinueIndex, char expectedBracket, char expectedPowerChar)
        {
            Group group = new Group(value);

            Assert.That(group.IsNegative, Is.EqualTo(expectedIsNegative));
            Assert.That(group.Base, Is.EqualTo(expectedBase));
            Assert.That(group.Power, Is.EqualTo(expectedPower));
            Assert.That(group.ContinueIndex, Is.EqualTo(expectedContinueIndex));
            Assert.That(group.BracketCharacter, Is.EqualTo(expectedBracket));
            Assert.That(group.PowerBracketCharacter, Is.EqualTo(expectedPowerChar));
        }
        
        [TestCase("(A+B))")]
        [TestCase("((A+B)")]
        [TestCase(")(A+B)")]
        [TestCase("(A+}B)")]
        [TestCase("(A+B)^(C+D))")]
        [TestCase("(A+B)^((C+D)")]
        [TestCase("(A+B)^)(C+D)")]
        public void Initialize_Unbalanced_Brackets_Is_Empty(string value)
        {
            Group group = new Group(value);

            Assert.IsFalse(group.IsNegative);
            Assert.That(group.Base, Is.EqualTo(string.Empty));
            Assert.That(group.Power, Is.EqualTo(string.Empty));
            Assert.That(group.ContinueIndex, Is.EqualTo(-1));
            Assert.That(group.BracketCharacter, Is.EqualTo('\0'));
            Assert.That(group.PowerBracketCharacter, Is.EqualTo('\0'));
        }
    }
}
