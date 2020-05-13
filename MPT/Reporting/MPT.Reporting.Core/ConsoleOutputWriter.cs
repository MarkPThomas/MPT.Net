using System;

namespace MPT.Reporting.Core
{
    public class ConsoleOutputWriter : IOutputWriter
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
