using System;
using System.Collections.Generic;
using System.Linq;
using NCore.Rules;
using NUnit.Framework;

namespace NCore.Tests.UnitTest
{
    [TestFixture]
    public class EntityTests
    {
        public class FakeEntity : Entity<FakeEntity>
        {
            private readonly IRule<FakeEntity>[] _extraRules;

            public FakeEntity(DateTime? creationDate = null, params IRule<FakeEntity>[] extraRules)
            {
                _extraRules = extraRules;
                CreationDate = creationDate;
            }

            protected override IRule<FakeEntity>[] GetBusinessRules()
            {
                return _extraRules;
            }
        }

        [Test]
        public void ShouldBeValidWithCreationDate()
        {
            IEntity entity = new FakeEntity(DateTime.Now);
            IEnumerable<string> errors;

            var isValid = entity.IsValid(out errors);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(errors);
        }

        [Test]
        public void ShouldHaveHookForExtraValidationRules()
        {
            var errorMessage = "Some error message";
            IEntity entity = new FakeEntity(DateTime.Now, new RelayRule<FakeEntity>(t => true, errorMessage));
            IEnumerable<string> errors;

            var isValid = entity.IsValid(out errors);

            Assert.IsFalse(isValid);
            Assert.That(errors.Count(), Is.EqualTo(1));
            Assert.That(errors.First(), Is.EqualTo(errorMessage));
        }
    }
}