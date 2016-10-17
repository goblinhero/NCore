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

            public FakeEntity(params IRule<FakeEntity>[] extraRules)
            {
                _extraRules = extraRules;
            }

            public FakeEntity(long id)
            {
                Id = id;
            }
            protected override IRule<FakeEntity>[] GetBusinessRules()
            {
                return _extraRules;
            }
        }

        [Test]
        public void ShouldEqualOnSameId()
        {
            var entity1 = new FakeEntity(3);
            var entity2 = new FakeEntity(3);

            Assert.That(entity1, Is.EqualTo(entity2));
            Assert.That(entity1.GetHashCode(),Is.EqualTo(entity2.GetHashCode()));
        }
        [Test]
        public void ShouldHaveTransientIdBehaviour()
        {
            var entity1 = new FakeEntity();
            var entity2 = new FakeEntity();

            Assert.That(entity1, Is.Not.EqualTo(entity2));
            Assert.That(entity1.GetHashCode(),Is.Not.EqualTo(entity2.GetHashCode()));
        }
        [Test]
        public void ShouldBeValid()
        {
            IEntity entity = new FakeEntity();
            IEnumerable<string> errors;

            var isValid = entity.IsValid(out errors);

            Assert.IsTrue(isValid);
            Assert.IsEmpty(errors);
        }

        [Test]
        public void ShouldHaveHookForExtraValidationRules()
        {
            var errorMessage = "Some error message";
            IEntity entity = new FakeEntity(new RelayRule<FakeEntity>(t => true, errorMessage));
            IEnumerable<string> errors;

            var isValid = entity.IsValid(out errors);

            Assert.IsFalse(isValid);
            Assert.That(errors.Count(), Is.EqualTo(1));
            Assert.That(errors.First(), Is.EqualTo(errorMessage));
        }
    }
}