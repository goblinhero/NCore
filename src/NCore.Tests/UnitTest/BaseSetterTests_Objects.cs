using System;
using NCore.Web.Utilities;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCore.Tests.UnitTest
{
    [TestFixture]
    public class BaseSetterTests_Objects
    {
        private readonly string _initialDescription = "Initial";
        private readonly string _newDescription = "Newly set description";
        private readonly decimal _oldTotal = 34.8m;
        private readonly decimal _newTotal = 49.8m;
        private readonly long? _childId = 654;

        public class FakeEntity : Entity<FakeEntity>
        {
            public string Description { get; set; }
            public decimal Total { get; set; }
            public FakeEntity Child { get; set; }
        }

        public class FakeEntitySetter : EntitySetter<FakeEntity>
        {
            public FakeEntitySetter(object dto, FakeEntity entity)
                : base(new PropertyHelper(dto), entity)
            {
            }

            public void SetDescription(Action<string, string> postUpdate = null)
            {
                UpdateSimpleProperty(e => e.Description, postUpdate);
            }

            public void SetTotal(Action<decimal, decimal> postUpdate = null)
            {
                UpdateSimpleProperty(fe => fe.Total, postUpdate);
            }

            public void SetChild(ISession session, Action<FakeEntity, FakeEntity> postUpdate = null)
            {
                UpdateComplexProperty(fe => fe.Child, session);
            }
        }

        [Test]
        public void ShouldHandleNullForNullableProperties()
        {
            var entity = new FakeEntity {Description = _initialDescription};
            var updater = new FakeEntitySetter(new {Description = (string) null}, entity);

            updater.SetDescription();

            Assert.That(entity.Description, Is.Null);
        }

        [Test]
        public void ShouldIgnoreCasing()
        {
            var entity = new FakeEntity {Total = _oldTotal};
            var updater = new FakeEntitySetter(new {toTal = _newTotal}, entity);

            updater.SetTotal();

            Assert.That(entity.Total, Is.EqualTo(_newTotal));
        }

        [Test]
        public void ShouldIgnoreMismatchedPropertyTypes()
        {
            var entity = new FakeEntity {Description = _initialDescription};
            var updater = new FakeEntitySetter(new {Description = 35}, entity);

            updater.SetDescription();

            Assert.That(entity.Description, Is.EqualTo(_initialDescription));
        }

        [Test]
        public void ShouldIgnorePropertiesNotExistingOnEntity()
        {
            var entity = new FakeEntity {Description = _initialDescription};
            var updater = new FakeEntitySetter(new {OtherProperty = 34}, entity);

            updater.SetDescription();

            Assert.That(entity.Description, Is.EqualTo(_initialDescription));
        }

        [Test]
        public void ShouldNotSetPropertyIfNotSetInDto()
        {
            var entity = new FakeEntity {Description = _initialDescription};
            var updater = new FakeEntitySetter(new {}, entity);

            updater.SetDescription();

            Assert.That(entity.Description, Is.EqualTo(_initialDescription));
        }

        [Test]
        public void ShouldNullComplexPropertyIfIdSetToNull()
        {
            var child = MockRepository.GenerateStub<FakeEntity>();
            child.Expect(c => c.Id).Return(_childId).Repeat.Any();
            var entity = new FakeEntity {Child = child};
            var session = MockRepository.GenerateStub<ISession>();
            var updater = new FakeEntitySetter(new {ChildId = (long?) null}, entity);

            updater.SetChild(session);

            Assert.That(entity.Child, Is.Null);
        }

        [Test]
        public void ShouldSetComplexProperty()
        {
            var child = MockRepository.GenerateStub<FakeEntity>();
            child.Expect(c => c.Id).Return(_childId).Repeat.Any();
            var entity = new FakeEntity();
            var session = MockRepository.GenerateMock<ISession>();
            session.Expect(s => s.Get<FakeEntity>(_childId)).Return(child);
            var updater = new FakeEntitySetter(new {ChildId = _childId}, entity);

            updater.SetChild(session);

            Assert.That(entity.Child, Is.SameAs(child));
            session.VerifyAllExpectations();
        }

        [Test]
        public void ShouldSetPropertyIfSetInDto()
        {
            var entity = new FakeEntity {Description = _initialDescription};
            var updater = new FakeEntitySetter(new {Description = _newDescription}, entity);

            updater.SetDescription();

            Assert.That(entity.Description, Is.EqualTo(_newDescription));
        }

        [Test]
        public void ShouldSetValueType()
        {
            var entity = new FakeEntity {Total = _oldTotal};
            var updater = new FakeEntitySetter(new {Total = _newTotal}, entity);

            updater.SetTotal();

            Assert.That(entity.Total, Is.EqualTo(_newTotal));
        }
    }
}