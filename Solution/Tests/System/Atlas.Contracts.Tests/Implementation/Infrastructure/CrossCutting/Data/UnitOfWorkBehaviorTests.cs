using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Data.Stubs;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Data
{
    [TestClass, ExcludeFromCodeCoverage]
    public class UnitOfWorkBehaviorTests : TestBase
    {
        private UnityContainer _container;

        private Mock<IUnitOfWork> _unitOfWorkMock;
        private IUnitOfWork _unitOfWork;

        private ITargetStub _target;


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            _container = new UnityContainer();
            _container.AddNewExtension<Interception>();

            InjectionMember[] behaviors =
            {
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<UnitOfWorkBehavior>()
            };
            _container.RegisterType<ITargetStub, TargetStub>(behaviors);

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWork = _unitOfWorkMock.Object;

            ServiceLocatorMock.Setup(x => x.GetInstance<IUnitOfWork>()).Returns(_unitOfWork);

            _target = _container.Resolve<ITargetStub>();
        }


        [TestMethod]
        public void Methods_ByDefaultOpensAndClosesUnitOfWork()
        {
            // Act
            string unitOfWorkString = _target.ImplicitUseUnitOfWork();
            _target.CommitUnitOfWork();
            _target.CommitUnitOfWork1();

            // Assert
            _unitOfWorkMock.Verify(x => x.Dispose(), Times.Exactly(3));
        }

        [TestMethod]
        public void Methods_CommitsOnlyWhenSpecified()
        {
            // Act
            _target.ImplicitUseUnitOfWork();
            _target.CommitUnitOfWork();
            _target.CommitUnitOfWork1();
            _target.DoNotCommitUnitOfWork();

            // Assert
            _unitOfWorkMock.Verify(x => x.Commit(), Times.Exactly(2));
        }

        [TestMethod]
        public void Methods_IfIgnoringUnitOfWork_DoNotUseIt()
        {
            // Assert
            _target.DoNotUseUnitOfWork();

            // Assert
            _unitOfWorkMock.Verify(x => x.Dispose(), Times.Never);
        }

        [TestMethod, ExpectedException(typeof(DllNotFoundException))]
        public void Methods_ThrowException_UnitOfWorkIsRolledBackAndExceptionRethrown()
        {
            // Act
            try
            {
                _target.Throw();
            }
            catch
            {
                _unitOfWorkMock.Verify(x => x.Rollback());
                throw;
            }
        }


        [TestMethod]
        public void Properties_ByDefaultOpensAndClosesUnitOfWork()
        {
            // Act
            int methodResult = _target.UsingUnitOfWork;

            // Assert
            _unitOfWorkMock.Verify(x => x.Dispose(), Times.Once);
        }

        [TestMethod]
        public void Properties_IfIgnoringUnitOfWork_DoNotOpensAndClosesUnitOfWork()
        {
            // Assert
            _target.DoNotUseUnitOfWork();

            // Assert
            _unitOfWorkMock.Verify(x => x.Dispose(), Times.Never);
        }
    }
}
