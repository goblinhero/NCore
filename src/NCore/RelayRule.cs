using System;

namespace NCore
{
    public class RelayRule<T> : IRule<T>
    {
        private readonly Predicate<T> _isBroken;

        public RelayRule(Predicate<T> isBroken, string brokenMessage)
        {
            BrokenMessage = brokenMessage;
            _isBroken = isBroken;
        }

        public bool IsBroken(T underTest)
        {
            return _isBroken(underTest);
        }

        public string BrokenMessage { get; set; }
    }
}