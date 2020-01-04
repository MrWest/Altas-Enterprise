using System;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Moq;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Provides a base class for test classes.
    /// </summary>
    public abstract class TestBase
    {
        /// <summary>
        /// Initializes the tests scenario.
        /// </summary>
        public virtual void Initialize()
        {
            CreateServiceLocatorMock();
            CreateServiceLocator();

            CreateEventAggregatorMock();
            CreateEventAggregator();

            CreateLoggerMock();
            CreateLogger();

            ServiceLocatorMock.Setup(x => x.GetInstance<IEventAggregator>()).Returns(EventAggregatorObject);
            ServiceLocatorMock.Setup(x => x.GetInstance<ILoggerFacade>()).Returns(LoggerObject);
        }


        /// <summary>
        /// This is the mock of the <see cref="IServiceLocator"/> to make configurations to it.
        /// </summary>
        public Mock<IServiceLocator> ServiceLocatorMock { get; protected set; }

        /// <summary>
        /// This is the mocked object of the <see cref="IServiceLocator"/> with the configuration already in place.
        /// </summary>
        public IServiceLocator ServiceLocatorObject { get; protected set; }

        /// <summary>
        /// Gets the reference to the mock of the <see cref="IEventAggregator"/> service.
        /// </summary>
        public Mock<IEventAggregator> EventAggregatorMock { get; protected set; }

        /// <summary>
        /// Gets the reference to the mocked instance of the <see cref="IEventAggregator"/> service.
        /// </summary>
        public IEventAggregator EventAggregatorObject { get; protected set; }

        /// <summary>
        /// Gets the mock of the logger (<see cref="ILoggerFacade"/>).
        /// </summary>
        public Mock<ILoggerFacade> LoggerMock { get; protected set; }

        /// <summary>
        /// Gets the mocked instance of the logger (<see cref="ILoggerFacade"/>).
        /// </summary>
        public ILoggerFacade LoggerObject { get; protected set; }


        /// <summary>
        /// Constructs the mock of the <see cref="IServiceLocator"/> and assigns it to the ServiceLocatorMock property.
        /// </summary>
        protected virtual void CreateServiceLocatorMock()
        {
            ServiceLocatorMock = new Mock<IServiceLocator>();
        }

        /// <summary>
        /// Constructs the mocked instance of the <see cref="IServiceLocator"/> and assigns it to the ServiceLocatorObject property.
        /// </summary>
        protected virtual void CreateServiceLocator()
        {
            ServiceLocatorObject = ServiceLocatorMock.Object;
            ServiceLocator.SetLocatorProvider(() => ServiceLocatorObject);
        }

        /// <summary>
        /// Constructs the mock of the <see cref="IEventAggregator"/> and assigns it to the EventAggregatorMock property.
        /// </summary>
        protected virtual void CreateEventAggregatorMock()
        {
            EventAggregatorMock = new Mock<IEventAggregator>();
        }

        /// <summary>
        /// Constructs the mocked instance of the <see cref="IEventAggregator"/> and assigns it to the EventAggregatorObject property.
        /// </summary>
        protected virtual void CreateEventAggregator()
        {
            EventAggregatorObject = EventAggregatorMock.Object;
        }

        /// <summary>
        /// Creates and assigns to the LoggerMock property the mock of the logger (<see cref="ILoggerFacade"/>).
        /// </summary>
        protected virtual void CreateLoggerMock()
        {
            LoggerMock = new Mock<ILoggerFacade>();
        }

        /// <summary>
        /// Creates and assigns to the LoggerObject property the mocked instance of the logger (<see cref="ILoggerFacade"/>).
        /// </summary>
        protected virtual void CreateLogger()
        {
            LoggerObject = LoggerMock.Object;
        }
    }


    /// <summary>
    /// Provides a base class for the test classes which tests target instances of a certain class.
    /// </summary>
    /// <typeparam name="T">The type of the class to test.</typeparam>
    public abstract class TestBase<T> : TestBase
    {
        /// <summary>
        /// Initializes the tests scenario.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            CreateInstance();
            CreateInstanceInternals();
        }


        /// <summary>
        /// Gets the instance of the class to be tested in the tests found in current class.
        /// </summary>
        public virtual T TestObject { get; protected set; }

        /// <summary>
        /// Gets the internals accessor of the TestObject object.
        /// </summary>
        public PrivateObject TestObjectInternals { get; protected set; }


        /// <summary>
        /// Creates and assigns to the TestObject property a new instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <returns>A new instance of type <typeparamref name="T"/>.</returns>
        protected virtual void CreateInstance()
        {
            TestObject = Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Constructs and assigns to the TestObjectInternals property an object internals accessor.
        /// </summary>
        protected virtual void CreateInstanceInternals()
        {
            TestObjectInternals = new PrivateObject(TestObject);
        }

        #region Utilities

        // TODO: Comment every single method below

        /// <summary>
        /// Sets the given value to the given property of the given object.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="testObject">The object which property will be set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="propertyValue">The value to set to the property.</param>
        protected void SetProperty<TProperty>(T testObject, string propertyName, TProperty propertyValue)
        {
            PropertyInfo property = GetPropertyInfo(propertyName);
            property.SetValue(testObject, propertyValue);
        }

        /// <summary>
        /// Sets the given value to the given property of the given object.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="testObject">The object which property will be set.</param>
        /// <param name="propertyExpression">The expression defining the property to set.</param>
        /// <param name="propertyValue">The value to set to the property.</param>
        protected void SetProperty<TProperty>(T testObject, Expression<Func<T, TProperty>> propertyExpression, TProperty propertyValue)
        {
            string propertyName = ExtractPropertyName<TProperty>(propertyExpression);
            PropertyInfo property = GetPropertyInfo(propertyName);
            property.SetValue(testObject, propertyValue);
        }

        /// <summary>
        /// Gets the value of the given property of the given object.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="testObject">The object which property's value will be get.</param>
        /// <param name="propertyName">The name of the property to get its value.</param>
        /// <returns>The property's value.</returns>
        protected TProperty GetProperty<TProperty>(T testObject, string propertyName)
        {
            return GetProperty<T, TProperty>(testObject, propertyName);
        }

        /// <summary>
        /// Gets the value of the given property of the given object.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="testObject">The object which property's value will be get.</param>
        /// <param name="propertyExpression">The expression defining the property to get its value.</param>
        /// <returns>The property's value.</returns>
        protected TProperty GetProperty<TProperty>(T testObject, Expression<Func<T, TProperty>> propertyExpression)
        {
            return GetProperty<T, TProperty>(testObject, propertyExpression);
        }

        /// <summary>
        /// Gets the value of the given property of the given object.
        /// </summary>
        /// <typeparam name="TType">The type of the object which property's value will be gotten.</typeparam>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="testObject">The object which property's value will be get.</param>
        /// <param name="propertyExpression">The expression defining the property to get its value.</param>
        /// <returns>The property's value.</returns>
        protected TProperty GetProperty<TType, TProperty>(TType testObject, Expression<Func<TType, TProperty>> propertyExpression)
        {
            return GetProperty<TType, TProperty>(testObject, ExtractPropertyName(propertyExpression));
        }

        /// <summary>
        /// Gets the value of the given property of the given object.
        /// </summary>
        /// <typeparam name="TType">The type of the object which property's value will be gotten.</typeparam>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="testObject">The object which property's value will be get.</param>
        /// <param name="propertyName">The name of the property to get its value.</param>
        /// <returns>The property's value.</returns>
        protected TProperty GetProperty<TType, TProperty>(TType testObject, string propertyName)
        {
            PropertyInfo property = GetPropertyInfo<TType>(propertyName);
            return (TProperty)property.GetValue(testObject);
        }

        /// <summary>
        /// Gets the name of a property.
        /// </summary>
        /// <typeparam name="TProperty">The type of property's value.</typeparam>
        /// <param name="propertyExpression">The expression of the property which name must be gotten.</param>
        /// <returns>The property's name.</returns>
        protected string ExtractPropertyName<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            return ExtractPropertyName<T, TProperty>(propertyExpression);
        }

        /// <summary>
        /// Gets the name of a property.
        /// </summary>
        /// <typeparam name="TType">The type of the object of the property to get its name.</typeparam>
        /// <typeparam name="TProperty">The type of property's value.</typeparam>
        /// <param name="propertyExpression">The expression of the property which name must be gotten.</param>
        /// <returns>The property's name.</returns>
        protected string ExtractPropertyName<TType, TProperty>(Expression<Func<TType, TProperty>> propertyExpression)
        {
            var member = propertyExpression.Body as MemberExpression;
            return member.Member.Name;
        }

        /// <summary>
        /// Gets the method information of a property.
        /// </summary>
        /// <param name="propertyName">The name of the property which information will be returned.</param>
        /// <returns>The <see cref="MethodInfo"/> of the property which name is <paramref name="propertyName"/>.</returns>
        private PropertyInfo GetPropertyInfo(string propertyName)
        {
            return GetPropertyInfo<T>(propertyName);
        }

        /// <summary>
        /// Gets the method information of a property.
        /// </summary>
        /// <typeparam name="TType">The type of the object of the property which information will be returned.</typeparam>
        /// <param name="propertyName">The name of the property which information will be returned.</param>
        /// <returns>The <see cref="MethodInfo"/> of the property which name is <paramref name="propertyName"/>.</returns>
        private PropertyInfo GetPropertyInfo<TType>(string propertyName)
        {
            Type typeT = typeof(TType);
            return typeT.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        /// Invokes a generic protected method.
        /// </summary>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="genericArguments">The types of the generic arguments respectively.</param>
        /// <param name="methodArguments">The arguments of the method respectively.</param>
        protected void InvokeGeneric(string methodName, Type[] genericArguments, params object[] methodArguments)
        {
            Type objectType = TestObject.GetType();
            MethodInfo method = objectType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method = method.MakeGenericMethod(genericArguments);

            method.Invoke(TestObject, methodArguments);
        }

        /// <summary>
        /// Invokes a generic protected method.
        /// </summary>
        /// <typeparam name="TResult">The type of the expected result of the method.</typeparam>
        /// <param name="methodName">The name of the method to invoke.</param>
        /// <param name="genericArguments">The types of the generic arguments respectively.</param>
        /// <param name="methodArguments">The arguments of the method respectively.</param>
        /// <returns>An object of type <typeparamref name="TResult"/> being the result of the method.</returns>
        protected TResult InvokeGeneric<TResult>(string methodName, Type[] genericArguments, params object[] methodArguments)
        {
            Type objectType = TestObject.GetType();
            MethodInfo method = objectType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            method = method.MakeGenericMethod(genericArguments);

            return (TResult)method.Invoke(TestObject, methodArguments);
        }

        #endregion
    }
}
