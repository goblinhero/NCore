namespace NCore.Rules
{
    public interface IRule<T>
    {
        string BrokenMessage { get; }
        bool IsBroken(T underTest);
    }
}