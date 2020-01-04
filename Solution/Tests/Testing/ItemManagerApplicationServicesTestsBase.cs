using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Moq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Base class of the test classes testing item manager application services classes.
    /// </summary>
    /// <typeparam name="TServices">The type of the item manager application services class to test.</typeparam>
    /// <typeparam name="T">The type of the items manage by the tested class.</typeparam>
    /// <typeparam name="TRepository">The type of the repository used by the tested class.</typeparam>
    /// <typeparam name="TDomainServices">The type of the domain services aiding the tested class.</typeparam>
    /// <typeparam name="TUnitOfWork">The unit or work ensuring true transactioning processing used by the tested class.</typeparam>
    public abstract class ItemManagerApplicationServicesTestsBase<TServices, T, TRepository, TDomainServices, TUnitOfWork> :
        MockedTestBase<TServices>
        where T : class, IEntity
        where TServices : class, IItemManagerApplicationServices<T>
        where TRepository : class, IRepository<T>
        where TDomainServices : class, IDomainServices<T>
        where TUnitOfWork : class, IUnitOfWork
    {
        /// <summary>
        /// Initializes the test scenario.
        /// </summary>
        public override void Initialize()
        {
            ServiceLocatorMock = new Mock<IServiceLocator>();
            ServiceLocatorObject = ServiceLocatorMock.Object;
            ServiceLocator.SetLocatorProvider(() => ServiceLocatorObject);

            CreateEventAggregatorMock();
            CreateEventAggregator();
            ServiceLocatorMock.Setup(x => x.GetInstance<IEventAggregator>()).Returns(EventAggregatorObject);

            CreateUnitOfWorkMock();
            CreateUnitOfWork();
            CreateRepositoryMock();
            CreateRepository();
            CreateDomainServicesMock();
            CreateDomainServices();
            CreateLoggerMock();
            CreateLogger();

            ServiceLocatorMock.Setup(x => x.GetInstance<IUnitOfWork>()).Returns(UnitOfWorkObject);
            ServiceLocatorMock.Setup(x => x.GetInstance<TRepository>()).Returns(RepositoryObject);
            ServiceLocatorMock.Setup(x => x.GetInstance<TDomainServices>()).Returns(DomainServicesObject);
            ServiceLocatorMock.Setup(x => x.GetInstance<ILoggerFacade>()).Returns(LoggerObject);

            CreateMock();
            CreateInstance();
        }


        /// <summary>
        /// Gets the mock of the unit of work.
        /// </summary>
        public Mock<TUnitOfWork> UnitOfWorkMock { get; protected set; }

        /// <summary>
        /// Gets the instance of the mocked unit of work.
        /// </summary>
        public TUnitOfWork UnitOfWorkObject { get; protected set; }

        /// <summary>
        /// Gets the mock of the repository mock.
        /// </summary>
        public Mock<TRepository> RepositoryMock { get; protected set; }

        /// <summary>
        /// Gets the mocked instance of the repository.
        /// </summary>
        public TRepository RepositoryObject { get; protected set; }

        /// <summary>
        /// Gets the mock of the Domain Services mock.
        /// </summary>
        public Mock<TDomainServices> DomainServicesMock { get; protected set; }

        /// <summary>
        /// Gets the mocked instance of the Domain Services.
        /// </summary>
        public TDomainServices DomainServicesObject { get; protected set; }

        /// <summary>
        /// Gets the mock of the logger.
        /// </summary>
        public Mock<ILoggerFacade> LoggerMock { get; protected set; }

        /// <summary>
        /// Gets the instance of the mocked logger.
        /// </summary>
        public ILoggerFacade LoggerObject { get; protected set; }


        /// <summary>
        /// Constructs an instance of Unit of Work mock and assigns it to the UnitOfWorkMock property.
        /// </summary>
        protected virtual void CreateUnitOfWorkMock()
        {
            UnitOfWorkMock = new Mock<TUnitOfWork>();
        }

        /// <summary>
        /// Constructs an instance of Unit of Work using the mock and assigns it to the UnitOfWorkObject property.
        /// </summary>
        protected virtual void CreateUnitOfWork()
        {
            UnitOfWorkObject = UnitOfWorkMock.Object;
        }

        /// <summary>
        /// Constructs an instance of the Repository mock and assigns it to the RepositoryMock property.
        /// </summary>
        protected virtual void CreateRepositoryMock()
        {
            RepositoryMock = new Mock<TRepository>();
        }

        /// <summary>
        /// Constructs an instance of the mocked Repository and assigns it to the Repository property.
        /// </summary>
        protected virtual void CreateRepository()
        {
            RepositoryObject = RepositoryMock.Object;
        }

        /// <summary>
        /// Constructs an instance of the DomainServices mock and assigns it to the DomainServicesMock property.
        /// </summary>
        protected virtual void CreateDomainServicesMock()
        {
            DomainServicesMock = new Mock<TDomainServices>();
        }

        /// <summary>
        /// Constructs an instance of the mocked DomainServices and assigns it to the DomainServices property.
        /// </summary>
        protected virtual void CreateDomainServices()
        {
            DomainServicesObject = DomainServicesMock.Object;
        }

        /// <summary>
        /// Constructs an instance of Logger mock and assigns it to the LoggerMock property.
        /// </summary>
        protected virtual void CreateLoggerMock()
        {
            LoggerMock = new Mock<ILoggerFacade>();
        }

        /// <summary>
        /// Constructs an instance of Logger using the mock and assigns it to the LoggerObject property.
        /// </summary>
        protected virtual void CreateLogger()
        {
            LoggerObject = LoggerMock.Object;
        }

        /// <summary>
        /// Constructs an instance of the mock of the Application Services and assigns it to the ApplicationServicesMock property.
        /// </summary>
        protected override void CreateMock()
        {
            TestMock = new Mock<TServices> { CallBase = true };
        }

        /// <summary>
        /// Constructs an instance of the mocked Application Services and assigns it to the ApplicationServices property.
        /// </summary>
        protected override void CreateInstance()
        {
            TestObject = TestMock.Object;
            TestObjectInternals = new PrivateObject(TestObject);
        }
    }
}
