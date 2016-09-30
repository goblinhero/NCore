using System.Collections.Generic;
using System.Linq;
using NCore.Rules;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCore.Tests
{
    [TestFixture]
    public class RuleSetTests
    {
        [Test]
        public void ShouldReturnFalseIfRuleIsBroken()
        {
            var message = "Rule is broken";
            IEnumerable<string> errors;
            var rule = MockRepository.GenerateStub<IRule<string>>();
            rule.Expect(r => r.IsBroken(Arg<string>.Is.Anything)).Return(true);
            rule.Expect(r => r.BrokenMessage).Return(message);

            var ruleset = new RuleSet<string>(rule);

            Assert.That(!ruleset.UpholdsRules(string.Empty,out errors));
            Assert.That(errors, Is.Not.Empty);
            Assert.That(errors.First(), Is.EqualTo(message));
        }
        [Test]
        public void ShouldReturnFalseIfAnyRuleIsBroken()
        {
            var message = "Rule is broken";
            IEnumerable<string> errors;
            var rule1 = MockRepository.GenerateStub<IRule<string>>();
            rule1.Expect(r => r.IsBroken(Arg<string>.Is.Anything)).Return(true);
            rule1.Expect(r => r.BrokenMessage).Return(message);

            var rule2 = MockRepository.GenerateStub<IRule<string>>();
            rule2.Expect(r => r.IsBroken(Arg<string>.Is.Anything)).Return(false);

            var ruleset = new RuleSet<string>(rule1, rule2);

            Assert.That(!ruleset.UpholdsRules(string.Empty,out errors));
            Assert.That(errors, Is.Not.Empty);
            Assert.That(errors.First(), Is.EqualTo(message));
        }
        [Test]
        public void ShouldReturnTrueIfRuleIsNotBroken()
        {
            IEnumerable<string> errors;
            var rule = MockRepository.GenerateStub<IRule<string>>();
            rule.Expect(r => r.IsBroken(Arg<string>.Is.Anything)).Return(false);

            var ruleset = new RuleSet<string>(rule);

            Assert.That(ruleset.UpholdsRules(string.Empty,out errors));
            Assert.That(errors, Is.Empty);
        }
    }
}