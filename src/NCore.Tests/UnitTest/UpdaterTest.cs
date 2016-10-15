using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCore.Extensions;
using NCore.Nancy.Commands;
using NHibernate;
using NHibernate.Tuple.Entity;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCore.Tests.UnitTest
{
    [TestFixture]
    public class UpdaterTest
    {
        private IEnumerable<string> _errors;
        private long _id = 5;
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
        public class FakeUpdater : BaseUpdater<FakeEntity>
        {
            public FakeUpdater(long id) 
                : base(id)
            {
            }

            protected override bool TrySetProperties(ISession session, out IEnumerable<string> errors)
            {
                return this.Success(out errors);
            }
        }

        public class FakeEntity : Entity<FakeEntity>
        {
        }
    }
}
