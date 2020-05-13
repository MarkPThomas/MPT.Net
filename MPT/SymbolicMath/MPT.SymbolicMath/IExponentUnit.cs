namespace MPT.SymbolicMath
{
    public interface IExponentUnit : IBase
    {
        string BaseAsString();
        int BaseAsInteger();
        double BaseAsFloat();

        IBase Power();
        IBase Base();
    }
}
