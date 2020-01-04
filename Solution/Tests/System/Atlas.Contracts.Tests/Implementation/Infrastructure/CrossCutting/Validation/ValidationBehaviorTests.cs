using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Validation.Stubs;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Infrastructure.CrossCutting.Validation
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ValidationBehaviorTests : TestBase
    {
        private UnityContainer _container;

        private Mock<IDomainServices<IEntity>> _domainServicesMock;
        private IDomainServices<IEntity> _domainServices;

        private ITargetStub _target;


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            _container = new UnityContainer();
            _container.AddNewExtension<Interception>();

            _domainServicesMock = new Mock<IDomainServices<IEntity>>();
            _domainServices = _domainServicesMock.Object;

            InterceptionMember[] behaviors =
            {
                new Interceptor<InterfaceInterceptor>(),
                new InterceptionBehavior<ValidationBehavior>(),
            };

            _container.RegisterType<ITargetStub, TargetStub>(behaviors);

            ServiceLocatorMock.Setup(x => x.GetInstance<IDomainServices<IEntity>>()).Returns(_domainServices);

            _target = _container.Resolve<ITargetStub>();
        }


        [TestMethod]
        public void Validate_MethodHasNoEntityArguments_DoesNotValidate()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            Mock<IEntity> entityMock = Mock.Get(entity);

            // Act
            _target.Do();

            // Assert
            _domainServicesMock.Verify(x => x.Validate(entity), Times.Never);
        }

        [TestMethod]
        public void Validate_ValidationIsCorrect_MethodGetsCalledCorrectly()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            Mock<IEntity> entityMock = Mock.Get(entity);

            _domainServicesMock.Setup(x => x.Validate(entity)).Returns(new string[0]);

            // Act
            _target.Add(entity);

            // Assert
            entityMock.VerifySet(x => x.Id = 100.ToString());
        }

        [TestMethod]
        public void Validate_ValidationIsIncorrect_MethodDoesNotGetCalled()
        {
            // Arrange
            var entity = Mock.Of<IEntity>();
            Mock<IEntity> entityMock = Mock.Get(entity);

            _domainServicesMock.Setup(x => x.Validate(entity)).Returns(new[] { "Error" });

            try
            {
                // Act
                _target.Add(entity);
            }
            catch (ValidationException e)
            {
                CollectionAssert.Contains(e.Errors, "Error");
            }

            // Assert
            entityMock.VerifySet(x => x.Id = 100.ToString(), Times.Never);
        }
    }
}
