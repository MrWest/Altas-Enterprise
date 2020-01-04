using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.Data.Db4O
{
    [TestClass, ExcludeFromCodeCoverage]
    public class Db4OUnitOfWorkTests
    {
        private Mock<IDb4ODatabaseContext> _databaseContextMock;
        private IDb4ODatabaseContext _databaseContext;

        private Mock<IServiceLocator> _serviceMock;
        private IServiceLocator _service;


        [TestInitialize]
        public void Initialize()
        {
            _serviceMock = new Mock<IServiceLocator>();
            _service = _serviceMock.Object;
            ServiceLocator.SetLocatorProvider(() => _service);
          
            _databaseContextMock = new Mock<IDb4ODatabaseContext>();
            _databaseContext = _databaseContextMock.Object;
            _serviceMock.Setup(x => x.GetInstance<IDatabaseContext>()).Returns(_databaseContext);
            _serviceMock.Setup(x => x.GetInstance<IDb4ODatabaseContext>()).Returns(_databaseContext);

            var internals = new PrivateType(typeof(Db4OUnitOfWork));
            internals.SetStaticField("Context", _databaseContext);
            internals.SetStaticField("_stack", 0);
            internals.SetStaticField("_rollbackProgrammed", false);
        }


        private static Db4OUnitOfWork CreateUnitOfWork()
        {
            var mock = new Mock<Db4OUnitOfWork> { CallBase = true };
            return mock.Object;
        }


        [TestMethod]
        public void Commit_CommitsOnlyAtLastCommitCall()
        {
            IUnitOfWork u1 = CreateUnitOfWork(), u2 = CreateUnitOfWork(), u3 = CreateUnitOfWork();

            // Cannot actually commit yet
            u3.Commit();
            u3.Dispose();
            _databaseContextMock.Verify(x => x.Save(), Times.Never);

            // Not yet
            u2.Commit();
            u2.Dispose();
            _databaseContextMock.Verify(x => x.Save(), Times.Never);

            // Now is when it should be done
            u1.Commit();
            u1.Dispose();
            _databaseContextMock.Verify(x => x.Save());
        }


        [TestMethod]
        public void Rollback_IfAtAnyPointARollbackWasSent_ThenTheRollbackMustBeExecutedAtLastRollbackCall()
        {
            IUnitOfWork u1 = CreateUnitOfWork(), u2 = CreateUnitOfWork(), u3 = CreateUnitOfWork();

            // Cannot actually commit yet
            u3.Commit();
            u3.Dispose();
            _databaseContextMock.Verify(x => x.Save(), Times.Never);

            // Not yet
            u2.Rollback();
            u2.Dispose();
            _databaseContextMock.Verify(x => x.Save(), Times.Never);
            _databaseContextMock.Verify(x => x.Rollback(), Times.Never);

            // Now is when it should be done
            u1.Commit();
            u1.Dispose();
            _databaseContextMock.Verify(x => x.Save(), Times.Never);
            _databaseContextMock.Verify(x => x.DropChanges());
        }
    }
}
