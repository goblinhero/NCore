using System;

namespace NCore.Strategies
{
    public class RelayStrategy<TCriteria> : IStrategy<TCriteria>
    {
        private readonly Action<TCriteria> _execute;
        private readonly Predicate<TCriteria> _isApplicable;

        public RelayStrategy(Action<TCriteria> execute, Predicate<TCriteria> isApplicable = null)
        {
            _isApplicable = isApplicable ?? (t => true);
            _execute = execute;
        }

        public bool IsApplicable(TCriteria criteria)
        {
            return _isApplicable(criteria);
        }

        public void Execute(TCriteria criteria)
        {
            _execute(criteria);
        }
    }

    public class RelayStrategy<TCriteria, TResult> : IStrategy<TCriteria, TResult>
    {
        private readonly Func<TCriteria, TResult> _execute;
        private readonly Predicate<TCriteria> _isSatisfiedBy;

        public RelayStrategy(Func<TCriteria, TResult> execute, Predicate<TCriteria> isSatisfiedBy = null)
        {
            _isSatisfiedBy = isSatisfiedBy ?? (t => true);
            _execute = execute;
        }

        public bool IsApplicable(TCriteria criteria)
        {
            return _isSatisfiedBy(criteria);
        }

        public TResult Execute(TCriteria criteria)
        {
            return _execute(criteria);
        }
    }
}