using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Moq;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Base class of the test classes testing a certain instance of <typeparamref name="T"/> type. Note that the instance is a mock.
    /// </summary>
    /// <typeparam name="T">The type of the type to mock and then test.</typeparam>
    public abstract class MockedTestBase<T> : TestBase<T> where T : class
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

            CreateMock();
            CreateInstance();
            CreateInstanceInternals();
        }


        /// <summary>
        /// Gets the mock of the test object.
        /// </summary>
        public Mock<T> TestMock { get; protected set; }


        /// <summary>
        /// Constructs an instance of the test object mock and assigns it to the TestMock property.
        /// </summary>
        protected virtual void CreateMock()
        {
            TestMock = new Mock<T> { CallBase = true };
        }

        /// <summary>
        /// Constructs an instance of the mocked test object and assigns it to the TestObject property.
        /// </summary>
        protected override void CreateInstance()
        {
            TestObject = TestMock.Object;
        }
    }
}
