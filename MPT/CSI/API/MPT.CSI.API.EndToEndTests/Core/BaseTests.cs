using System.Threading;
using NUnit.Framework;

namespace MPT.CSI.API.EndToEndTests.Core
{
    [TestFixture]
    public class BaseTests
    {
        protected void delayTestStart(bool until,
            int attempts,
            int wait)
        {
            int numberOfAttempts = 0;
            while (!until &&
                   (numberOfAttempts < attempts))
            {
                // Wait to execute test...
                numberOfAttempts++;
                Thread.Sleep(wait);
            }
        }
    }
}
