using System;
using System.IO;
using System.Text;
using NUnit.Framework;

// See: https://stackoverflow.com/questions/2139274/grabbing-the-output-sent-to-console-out-from-within-a-unit-test
// See: https://blogs.msdn.microsoft.com/ploeh/2006/10/21/console-unit-testing/
namespace MPT.Reporting.Core.UnitTests
{
    [TestFixture]
    public class LoggerListenerTests
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
                string testMessage = "Test message";

                LoggerReporter reporter = new LoggerReporter();
                LoggerListener.SubscribeListener(reporter);

                //Check for event
                LoggerEventArgs arg = new LoggerEventArgs(new MessengerEventArgs(testMessage));
                reporter.ThrowLoggerEvent(arg);

                string expected = string.Format(testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void UnsubscribeListener_Unsubscribes_Listener()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMessage = "Test message";

                LoggerReporter reporter = new LoggerReporter();
                LoggerListener.SubscribeListener(reporter);

                //Check for event
                LoggerEventArgs arg = new LoggerEventArgs(new MessengerEventArgs(testMessage));
                reporter.ThrowLoggerEvent(arg);

                string expected = string.Format(testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());

                // Clear Console output
                StringBuilder sb = sw.GetStringBuilder();
                sb.Remove(0, sb.Length);

                // Check that event does not trigger again
                LoggerListener.UnsubscribeListener(reporter);
                reporter.ThrowLoggerEvent(arg);

                expected = string.Empty;
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ReportLogToConsole_Does_Nothing_If_Logging_Event_Handled()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMessage = "Test message";

                LoggerReporter reporter = new LoggerReporter();
                LoggerListener.SubscribeListener(reporter);

                //Check for event
                LoggerEventArgs arg = new LoggerEventArgs(new MessengerEventArgs(testMessage));
                reporter.ThrowLoggerEvent(arg);

                string expected = string.Format(testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());

                // Clear Console output
                StringBuilder sb = sw.GetStringBuilder();
                sb.Remove(0, sb.Length);

                // Check that event does not trigger again
                arg.Handled = true;
                reporter.ThrowLoggerEvent(arg);

                expected = string.Empty;
                Assert.AreEqual(expected, sw.ToString());
            }
        }


        [Test]
        public void ReportLogToConsole_Logs_If_Logging_Event_Not_Handled()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMessage = "Test message";

                LoggerReporter reporter = new LoggerReporter();
                LoggerListener.SubscribeListener(reporter);

                //Check for event
                LoggerEventArgs arg = new LoggerEventArgs(new MessengerEventArgs(testMessage));
                reporter.ThrowLoggerEvent(arg);

                string expected = string.Format(testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());

                // Clear Console output
                StringBuilder sb = sw.GetStringBuilder();
                sb.Remove(0, sb.Length);

                // Check that event does not trigger again
                reporter.ThrowLoggerEvent(arg);
                
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ReportLogToConsole_Logs_Message()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMessage = "Test message";

                LoggerReporter reporter = new LoggerReporter();
                LoggerListener.SubscribeListener(reporter);

                //Check for event
                LoggerEventArgs arg = new LoggerEventArgs(new MessengerEventArgs(testMessage));
                reporter.ThrowLoggerEvent(arg);

                string expected = string.Format(testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }


        [Test]
        public void ReportLogToConsole_Logs_Exception_With_No_Stacktrace()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMessage = "Test message";
                string exceptionMessage = "Exception Message";

                LoggerReporter reporter = new LoggerReporter();
                LoggerListener.SubscribeListener(reporter);

                //Check for event
                Exception expectedException = new Exception(exceptionMessage);

                LoggerEventArgs arg = new LoggerEventArgs(expectedException, new MessengerEventArgs(testMessage));
                reporter.ThrowLoggerEvent(arg);

                string expected = string.Format(exceptionMessage + Environment.NewLine + testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [Test]
        public void ReportLogToConsole_Logs_Exception_With_Stacktrace()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMessage = "Test message";
                string exceptionMessage = "Exception Message";

                LoggerReporter reporter = new LoggerReporter();
                LoggerListener.SubscribeListener(reporter);

                //Check for event
                try
                {
                    throwException(exceptionMessage);
                }
                catch (Exception expectedException)
                {
                    LoggerEventArgs arg = new LoggerEventArgs(expectedException, new MessengerEventArgs(testMessage));
                    reporter.ThrowLoggerEvent(arg);
                }
                
                string expectedWithoutStackTrace = string.Format(exceptionMessage + Environment.NewLine + testMessage + Environment.NewLine);
                Assert.IsTrue(sw.ToString().Contains(exceptionMessage));
                Assert.IsTrue(sw.ToString().Contains(testMessage));
                Assert.IsTrue(sw.ToString().Length > expectedWithoutStackTrace.Length);
            }
        }

        [Test]
        public void ReportLogToConsole_Logs_Parameters()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMessage = "Test message";

                string key1 = "Key1:";
                string value1 = "Value1";
                string key2 = "Key2:";
                string value2 = "Value2";

                LoggerReporter reporter = new LoggerReporter();
                LoggerListener.SubscribeListener(reporter);

                //Check for event
                LoggerEventArgs arg = new LoggerEventArgs(new MessengerEventArgs(testMessage),
                    key1, value1,
                    key2, value2);
                reporter.ThrowLoggerEvent(arg);

                string expected = string.Format(testMessage + Environment.NewLine +
                                                key1 + " " + value1 + Environment.NewLine +
                                                key2 + " " + value2 + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }


        [Test]
        public void ReportLogToConsole_Logs_Title()
        {
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                string testMessage = "Test message";
                string testTitle = "Test title";

                LoggerReporter reporter = new LoggerReporter();
                LoggerListener.SubscribeListener(reporter);

                //Check for event
                LoggerEventArgs arg = new LoggerEventArgs(new MessengerEventArgs(testTitle, testMessage));
                reporter.ThrowLoggerEvent(arg);

                string expected = string.Format(testTitle + Environment.NewLine  + testMessage + Environment.NewLine);
                Assert.AreEqual(expected, sw.ToString());
            }
        }


        [Test]
        public void ReportLogToConsole_Logs_All()
        {

        }

        private class LoggerReporter : ILoggerEvent
        {
            public event EventHandler<LoggerEventArgs> LoggerEvent = delegate { };

            public void ThrowLoggerEvent(LoggerEventArgs e)
            {
                LoggerEvent(this, e);
            }
        }

        private void throwException(string exceptionMessage)
        {
            throw new Exception(exceptionMessage);
        }
    }
}
