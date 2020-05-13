using System;
using System.IO;
using System.Text;
using NUnit.Framework;

// See: https://stackoverflow.com/questions/2139274/grabbing-the-output-sent-to-console-out-from-within-a-unit-test
// See: https://blogs.msdn.microsoft.com/ploeh/2006/10/21/console-unit-testing/
namespace MPT.Reporting.Core.UnitTests
{
    [TestFixture]
    public class MessengerListenerTests
    {
        [SetUp]
        public void SetUp()
        {
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
        }

        [TearDown]
        public void TearDown()
        {
            Console.SetOut(new StreamWriter(Console.OpenStandardError()));
        }

        [Test]
        public void SubscribeListener_Subsribes_Listener()
        {
           using (StringWriter sw = new StringWriter())
           {
                Console.SetOut(sw);
                string testTitle = "Test title";
                string testMessage = "Test message";

                MessengerReporter reporter = new MessengerReporter();
                MessengerListener.SubscribeListener(reporter);
                
                //Check for event
                MessengerEventArgs arg = new MessengerEventArgs(testTitle, testMessage);
                reporter.ThrowMessengerEvent(arg);

                string expected = string.Format(testTitle + Environment.NewLine + testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
           }
        }

        [Test]
        public void UnsubscribeListener_Unsubscribes_Listener()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testTitle = "Test title";
                string testMessage = "Test message";

                MessengerReporter reporter = new MessengerReporter();
                MessengerListener.SubscribeListener(reporter);

                //Check for event
                MessengerEventArgs arg = new MessengerEventArgs(testTitle, testMessage);
                reporter.ThrowMessengerEvent(arg);

                string expected = string.Format(testTitle + Environment.NewLine + testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());

                // Clear Console output
                StringBuilder sb = sw.GetStringBuilder();
                sb.Remove(0, sb.Length);

                // Check that event does not trigger again
                MessengerListener.UnsubscribeListener(reporter);
                reporter.ThrowMessengerEvent(arg);

                expected = string.Empty;
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ReportMessageToConsole_Does_Nothing_If_Logging_Event_Handled()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testTitle = "Test title";
                string testMessage = "Test message";

                MessengerReporter reporter = new MessengerReporter();
                MessengerListener.SubscribeListener(reporter);

                //Check for event
                MessengerEventArgs arg = new MessengerEventArgs(testTitle, testMessage);
                reporter.ThrowMessengerEvent(arg);

                string expected = string.Format(testTitle + Environment.NewLine + testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());

                // Clear Console output
                StringBuilder sb = sw.GetStringBuilder();
                sb.Remove(0, sb.Length);

                // Check that event does not trigger again
                arg.Handled = true;
                reporter.ThrowMessengerEvent(arg);

                expected = string.Empty;
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ReportMessageToConsole_Messages_If_Logging_Event_Not_Handled()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testTitle = "Test title";
                string testMessage = "Test message";

                MessengerReporter reporter = new MessengerReporter();
                MessengerListener.SubscribeListener(reporter);

                //Check for event
                MessengerEventArgs arg = new MessengerEventArgs(testTitle, testMessage);
                reporter.ThrowMessengerEvent(arg);

                string expected = string.Format(testTitle + Environment.NewLine + testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());

                // Clear Console output
                StringBuilder sb = sw.GetStringBuilder();
                sb.Remove(0, sb.Length);

                // Check that event triggers again
                reporter.ThrowMessengerEvent(arg);
                
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        private class MessengerReporter : IMessengerEvent
        {
            public event EventHandler<MessengerEventArgs> MessengerEvent = delegate { };

            public void ThrowMessengerEvent(MessengerEventArgs e)
            {
                MessengerEvent(this, e);
            }
        }
    }
}
