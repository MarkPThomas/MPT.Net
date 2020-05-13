using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT.Reporting.Core
{
    public abstract class ReporterWriter
    {
        public static IOutputWriter Writer = new ConsoleOutputWriter();
    }
}
