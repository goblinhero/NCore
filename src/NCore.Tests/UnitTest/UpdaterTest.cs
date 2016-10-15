using System.Collections.Generic;
using NCore.Extensions;
using NCore.Nancy.Commands;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCore.Tests.UnitTest
{
    [TestFixture]
    public class UpdaterTest
    {
        private IEnumerable<string> _errors;
        private readonly long _id = 5;

        public class FakeUpdater : BaseUpdater<FakeEntity>
        {
            public FakeUpdater(long id)
                : base(id)
            {
            }

            protected override bool TrySetProperties(ISession session, FakeEntity entity, out IEnumerable<string> errors)
            {
                return this.Success(out errors);
            }
        }

        public class FakeEntity : Entity<FakeEntity>
        {
        }

        [Test]
        public void ShouldCheckValidity()
        {
            var entity = MockRepository.GenerateMock<FakeEntity>();
            var updater = new FakeUpdater(_id);
            var session = MockRepository.GenerateMock<ISession>();
            session.Expect(s => s.Get<FakeEntity>(_id)).Return(entity);

            updater.TryExecute(session, out _errors);

            entity.AssertWasCalled(fe => fe.IsValid(out _errors));
            session.VerifyAllExpectations();
        }
    }
}