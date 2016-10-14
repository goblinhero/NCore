using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCore.Extensions;
using NCore.Nancy.Updaters;
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

        [Test]
        public void ShouldCheckValidity()
        {
            var entity = MockRepository.GenerateMock<FakeEntity>();
            var updater = new FakeUpdater();
            updater.SetEntity(entity);

            updater.TryUpdate(MockRepository.GenerateStub<ISession>(), out _errors);
            
            entity.AssertWasCalled(fe => fe.IsValid(out _errors));
        }
        public class FakeUpdater : BaseUpdater<FakeEntity>
        {
            public FakeUpdater() 
                : base(4)
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
