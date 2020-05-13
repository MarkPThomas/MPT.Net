using System.Collections.Generic;
using NUnit.Framework;

namespace MPT.Units.Core.UnitTests.SymbolicParser
{
    [TestFixture]
    public class SymbolicParserTests
    {
        [TestCase()]
        public void ParseStringToUnits(string value)
        {
            List<cUnit> units = cSymbolicParser.ParseStringToUnits(value);
        }
    }
}
