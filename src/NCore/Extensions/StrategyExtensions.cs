using System.Collections.Generic;
using System.Linq;
using NCore.Strategies;

namespace NCore.Extensions
{
    public static class StrategyExtensions
    {
        public static void SafeExecute<TCriteria>(this IStrategy<TCriteria> strategy, TCriteria criteria)
        {
            strategy?.Execute(criteria);
        }

        public static void SafeExecute<TCriteria>(this IEnumerable<IStrategy<TCriteria>> strategies, TCriteria criteria)
        {
            strategies.FirstOrDefault(s => s.IsApplicable(criteria)).SafeExecute(criteria);
        }

        public static TResult SafeExecute<TCriteria, TResult>(
            this IEnumerable<IStrategy<TCriteria, TResult>> strategies, TCriteria criteria)
            where TResult : class
        {
            return strategies.FirstOrDefault(s => s.IsApplicable(criteria))?.Execute(criteria);
        }

        public static TResult ExecuteFirstOrDefault<TCriteria, TResult>(
            this IEnumerable<IStrategy<TCriteria, TResult>> strategies, TCriteria criteria)
        {
            var strategy = strategies.FirstOrDefault(s => s.IsApplicable(criteria));
            return strategy == null ? default(TResult) : strategy.Execute(criteria);
        }

        public static IEnumerable<TResult> Execute<TCriteria, TResult>(
            this IEnumerable<IStrategy<TCriteria, TResult>> strategies, TCriteria criteria)
        {
            return strategies.Where(s => s.IsApplicable(criteria)).Select(s => s.Execute(criteria)).ToList();
        }

        public static void Execute<TCriteria>(this IEnumerable<IStrategy<TCriteria>> strategies, TCriteria criteria)
        {
            foreach (var strategy in strategies.Where(s => s.IsApplicable(criteria)))
            {
                strategy.Execute(criteria);
            }
        }
    }
}