using System;
using NCore.Nancy.Updaters;
using NUnit.Framework;

namespace NCore.Tests.UnitTest
{
    [TestFixture]
    public class BaseSetterTests
    {
        private string _initialDescription = "Initial";
        private string _newDescription = "Newly set description";

        [Test]
        public void ShouldSetPropertyIfSetInDto()
        {
            var entity = new FakeEntity { Description = _initialDescription };
            var updater = new FakeEntitySetter(entity);

            updater.SetDescription(new { Description = _newDescription });

            Assert.That(entity.Description, Is.EqualTo(_newDescription));
        }

        [Test]
        public void ShouldNotSetPropertyIfNotSetInDto()
        {
            var entity = new FakeEntity { Description = _initialDescription };
            var updater = new FakeEntitySetter(entity);

            updater.SetDescription(new { });

            Assert.That(entity.Description, Is.EqualTo(_initialDescription));
        }

        [Test]
        public void ShouldIgnorePropertiesNotExistingOnEntity()
        {
            var entity = new FakeEntity { Description = _initialDescription };
            var updater = new FakeEntitySetter(entity);

            updater.SetDescription(new { OtherProperty = 34 });

            Assert.That(entity.Description, Is.EqualTo(_initialDescription));
        }

        [Test]
        public void ShouldIgnoreMismatchedPropertyTypes()
        {
            var entity = new FakeEntity { Description = _initialDescription };
            var updater = new FakeEntitySetter(entity);

            updater.SetDescription(new { Description = 35 });

            Assert.That(entity.Description, Is.EqualTo(_initialDescription));
        }

        [Test]
        public void ShouldHandleNullForNullableProperties()
        {
            var entity = new FakeEntity { Description = _initialDescription };
            var updater = new FakeEntitySetter(entity);

            updater.SetDescription(new { Description = (string)null });

            Assert.That(entity.Description, Is.Null);
        }
        public class FakeEntity : Entity<FakeEntity>
        {
            public string Description { get; set; }
        }

        public class FakeEntitySetter : EntitySetter<FakeEntity>
        {
            public FakeEntitySetter(FakeEntity entity)
            {
                _entity = entity;
            }

            public void SetDescription(object dto, Action<string, string> postUpdate = null)
            {
                UpdateSimpleProperty(e => e.Description, dto, postUpdate);
            }
        }
    }
}