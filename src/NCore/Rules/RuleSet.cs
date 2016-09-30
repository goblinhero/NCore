using System.Collections.Generic;
using System.Linq;

namespace NCore.Rules
{
    public class RuleSet<T>
        where T : class
    {
        private readonly IRule<T>[] _rules;

        public RuleSet(params IRule<T>[] rules)
        {
            _rules = rules;
        }

        public bool UpholdsRules(T underTest, out IEnumerable<string> errors)
        {
            errors = _rules.Where(r => r.IsBroken(underTest)).Select(r => r.BrokenMessage).ToArray();
            return !errors.Any();
        }
    }
}