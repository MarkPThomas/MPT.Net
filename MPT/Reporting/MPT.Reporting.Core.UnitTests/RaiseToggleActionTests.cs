using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MPT.Reporting.Core.UnitTests
{
    // TODO: Unsure if this class is still neeed.
    [TestFixture]
    public class RaiseToggleActionTests
    {
        [Test]
        public void StartAction_Reports_Method_Started()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMethodName = "Test method";

                RaiseToggleAction.StartAction(testMethodName);

                string expected = string.Format(testMethodName + " started");
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void EndAction_Reports_Method_Completed()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMethodName = "Test method";

                RaiseToggleAction.EndAction(testMethodName);

                string expected = string.Format(testMethodName + " completed");
                Assert.AreEqual(expected, sw.ToString());
            }
        }
    }
}
