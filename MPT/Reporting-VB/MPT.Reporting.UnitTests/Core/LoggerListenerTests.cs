using NUnit.Framework;

namespace MPT.Reporting.UnitTests
{
    [TestFixture]
    public class LoggerListenerTests
    {
        [Test]
        public void SubscribeListener_Subsribes_Listener()
        {

        }

        [Test]
        public void UnsubscribeListener_Unsubscribes_Listener()
        {

        }

        [Test]
        public void ReportLogToConsole_Does_Nothing_If_Logging_Event_Handled()
        {

        }


        [Test]
        public void ReportLogToConsole_Logs_If_Logging_Event_Unhandled()
        {

        }

        [Test]
        public void ReportLogToConsole_Logs_Without_Title_If_Logging_Event_Unhandled_And_Title_Empty()
        {

        }

        private class LoggerReporter
        {
            public void OnLoggerMessage(object sender, ILoggerEvent e)
            {

            }
        }
    }
}
