using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.TestSupport;
using Moq;
using CompanyName.Atlas.Contracts.Presentation.Services;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Base class of the bindable base deriver classes (UI view models).
    /// </summary>
    /// <typeparam name="T">The type of the bindable class (must implement <see cref="INotifyPropertyChanged"/>.</typeparam>
    public abstract class BindableTestsBase<T, TValidatable> : ValidatedTestsBase<T, TValidatable>
        where T : class, INotifyPropertyChanged, IInteractionRequest
    {
        private EventHandler<InteractionRequestedEventArgs> _raised;


        /// <summary>
        /// Initializes the test scenario.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            CreateStatusBarServicesMock();
            CreateStatusBarServices();
            ServiceLocatorMock.Setup(x => x.GetInstance<IStatusBarServices>()).Returns(StatusBarServices);
        }


        /// <summary>
        /// Gets the property change tracker associated to the TestObject.
        /// </summary>
        public PropertyChangeTracker ChangeTracker { get; protected set; }

        /// <summary>
        /// Gets the mock of the <see cref="IStatusBarServices"/>.
        /// </summary>
        public Mock<IStatusBarServices> StatusBarServicesMock { get; protected set; }

        /// <summary>
        /// Gets the mocked instance of <see cref="IStatusBarServices"/>.
        /// </summary>
        public IStatusBarServices StatusBarServices { get; protected set; }


        /// <summary>
        /// Tests that the bindable when given a new value to a property it sets that value and the property return such value when requested.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="propertyExpression">The expression defining the property to test.</param>
        /// <param name="getNewValue">A function to retrieve the value to set to the property.</param>
        public virtual void TestGetsSetValue<TProperty>(Expression<Func<T, TProperty>> propertyExpression, Func<TProperty> getNewValue)
        {
            // Arrange
            TProperty propertyValue = getNewValue();

            // Act
            string propertyName = ExtractPropertyName<TProperty>(propertyExpression);
            SetProperty(TestObject, propertyName, propertyValue);

            // Assert
            var value = GetProperty<TProperty>(TestObject, propertyName);
            Assert.AreEqual(propertyValue, value);
        }

        /// <summary>
        /// Tests that a property notifies a change when a new value is given to it.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="propertyExpression">The expression defining the property to test.</param>
        /// <param name="getOldValue">A function to retrieve the value a value that was on the property before the test started.</param>
        /// <param name="getNewValue">A function to retrieve the value to set to the property.</param>
        public virtual void TestNotifiesPropertyChangesIfSetValueIsDifferent<TProperty>(Expression<Func<T, TProperty>> propertyExpression, Func<TProperty> getOldValue, Func<TProperty> getNewValue)
        {
            // Arrange
            string propertyName = ExtractPropertyName<TProperty>(propertyExpression);
            TProperty oldPropertyValue = getOldValue();
            TProperty newPropertyValue = getNewValue();

            SetProperty(TestObject, propertyName, oldPropertyValue);

            var changeTracker = new PropertyChangeTracker(TestObject);

            // Act
            SetProperty(TestObject, propertyName, newPropertyValue);

            // Assert
            CollectionAssert.Contains(changeTracker.ChangedProperties, propertyName);
        }

        /// <summary>
        /// Tests that a property notifies a change when the same value is given to it.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="propertyExpression">The expression defining the property to test.</param>
        /// <param name="getValue">A function to retrieve the value to set to the property.</param>
        public virtual void TestDoesNotNotifPropertyChangesIfSetValueIsSame<TProperty>(Expression<Func<T, TProperty>> propertyExpression, Func<TProperty> getValue)
        {
            // Arrange
            string propertyName = ExtractPropertyName<TProperty>(propertyExpression);
            TProperty oldPropertyValue = getValue();

            SetProperty(TestObject, propertyName, oldPropertyValue);

            var changeTracker = new PropertyChangeTracker(TestObject);

            // Act
            SetProperty(TestObject, propertyName, oldPropertyValue);

            // Assert
            CollectionAssert.DoesNotContain(changeTracker.ChangedProperties, propertyName);
        }

        /// <summary>
        /// Determines whether there has been a number of notifications of a certain property changes.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="propertyExpression">The expression denoting the property to check its changes notifications.</param>
        /// <param name="times">A integer saying the expected number of notifications of the specified property.</param>
        public virtual void AssertNotified<TProperty>(Expression<Func<T, TProperty>> propertyExpression, int times = 1)
        {
            string propertyName = ExtractPropertyName<T, TProperty>(propertyExpression);

            AssertNotified(propertyName, times);
        }

        /// <summary>
        /// Determines whether there has been a number of notifications of a certain property changes.
        /// </summary>
        /// <param name="propertyName">The name of the property to check its changes notifications.</param>
        /// <param name="times">A integer saying the expected number of notifications of the specified property.</param>
        public virtual void AssertNotified(string propertyName, int times = 1)
        {
            Assert.AreEqual(times, ChangeTracker.ChangedProperties.Count(property => property == propertyName));
        }

        /// <summary>
        /// Invokes the PropertyChanged event notifying that the property defined by the given expression has changed.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property notified as changed.</typeparam>
        /// <param name="propertyExpression">The expression defining the changed property.</param>
        public void RaisePropertyChanged<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            string propertyName = ExtractPropertyName<TProperty>(propertyExpression);
            TestObjectInternals.Invoke("OnPropertyHasChanged", TestObject, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Creates an assigns to the StatusBarServicesMock property a mock of the <see cref="IStatusBarServices"/>.
        /// </summary>
        protected virtual void CreateStatusBarServicesMock()
        {
            StatusBarServicesMock = new Mock<IStatusBarServices>();
        }

        /// <summary>
        /// Creates an assigns to the StatusBarServices property a mocked instance of the <see cref="IStatusBarServices"/>.
        /// </summary>
        protected virtual void CreateStatusBarServices()
        {
            StatusBarServices = StatusBarServicesMock.Object;
        }
        
        /// <summary>
        /// Constructs an instance of the mocked test object and assigns it to the TestObject property.
        /// </summary>
        protected override void CreateInstance()
        {
            base.CreateInstance();

            ChangeTracker = new PropertyChangeTracker(TestObject);

            CreateStatusBarServicesMock();
            CreateStatusBarServices();
        }


        #region Utilities

        /// <summary>
        /// Setups the response that the user gives when performed an interaction to the given response.
        /// </summary>
        /// <param name="response">The response to return by the user in the interaction.</param>
        protected void SetupConfirmationResponse(bool response)
        {
            // Drop the all method
            if (_raised != null)
                TestObject.Raised -= _raised;

            // Create the new simulating the expected response of the user
            _raised = (sender, e) =>
            {
                var confirmation = e.Context as IConfirmation;
                if (confirmation != null)
                    confirmation.Confirmed = response;
            };

            // Rewire the new method into the Raised event
            TestObject.Raised += _raised;
        }

        #endregion
    }
}
