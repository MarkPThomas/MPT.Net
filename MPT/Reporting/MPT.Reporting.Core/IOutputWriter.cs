namespace MPT.Reporting.Core
{
    public interface IOutputWriter
    {
        void WriteLine(string message = "/n");
    }
}
