using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Validation;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data
{
    /// <summary>
    /// Represents the base class of the presenter used to decorate a domain entity.
    /// </summary>
    /// <typeparam name="T">The type of the entity to present.</typeparam>
    /// <typeparam name="TServices">The type of the application services this presenter view model uses to update the changes.</typeparam>
    public abstract class EntityPresenterBase<T, TServices> : ServiceBackedViewModel<TServices>, IPresenter<T>, INotifyDataErrorInfo
        where T : class, IEntity
        where TServices : IItemManagerApplicationServices<T>
    {
        private T _object;
        private IStatusBarServices _statusBarServices;
        private readonly ErrorsContainer<ValidationResult> _errors;
        private EventHandlerManager<DataErrorsChangedEventArgs> _errorsChangedEvent = new EventHandlerManager<DataErrorsChangedEventArgs>();


        /// <summary>
        /// Initializes a new instance of a presenter view model decorating a domain entity but without such entity.
        /// </summary>
        protected EntityPresenterBase()
        {
            Update = true;
            PropertyChanged += OnPropertyHasChanged;
            _errors = new ErrorsContainer<ValidationResult>(RaiseErrorsChanged);
        }

        /// <summary>
        /// Initializes a new instance of a presenter view model decorating a domain entity.
        /// </summary>
        /// <param name="entity">The domain entity to present.</param>
        /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
        protected EntityPresenterBase(T entity)
            : this()
        {
            if (Equals(entity, null))
                throw new ArgumentNullException("entity");

            Object = entity;
        }


        /// <summary>
        /// Gets the object being represented in the user interface by this presenter view model.
        /// </summary>
        object IPresenter.Object
        {
            get { return Object; }
            set { Object = (T)value; }
        }

        /// <summary>
        /// Gets the object being represented in the user interface by this presenter view model.
        /// </summary>
        public T Object
        {
            get
            {
                var type = this.GetType();
                if (Equals(_object, null))
                    throw new InvalidOperationException(Resources.APresenterMustHaveAnUnderlyingObject);

                return _object;
            }
            set
            {
                if (Equals(value, null))
                    throw new ArgumentNullException("value");

                _object = value;
            }
        }

        /// <summary>
        /// Gets or sets the unique identifier of the current entity.
        /// </summary>
        public virtual string Id
        {
            get { return Object.Id; }
            set { SetProperty(v => Object.Id = v, value); }
        }

        /// <summary>
        /// Gets the ToString method's value.
        /// </summary>
        public string FullName
        {
            get { return ToString(); }
            set { }
        }

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        public bool HasErrors
        {
            get { return _errors.HasErrors; }
        }

        /// <summary>
        /// Gets or sets whether to update the changes made to the current presenter.
        /// </summary>
        protected bool Update { get; set; }

        private IValidator Validator
        {
            get
            {
                var validatorFactory = ServiceLocator.Current.GetInstance<IValidatorFactory>();
                return validatorFactory.CreateValidator(GetType());
            }
        }

        private IStatusBarServices StatusBarServices
        {
            get
            {
                return _statusBarServices ?? (_statusBarServices = ServiceLocator.Current.GetInstance<IStatusBarServices>());
            }
        }


        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged
        {
            add { _errorsChangedEvent += value; }
            remove { _errorsChangedEvent -= value; }
        }

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property to retrieve validation errors for; or null or System.String.Empty, to retrieve entity-level errors.
        /// </param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.GetErrors(propertyName);
        }

        /// <summary>
        /// Returns a string that represents the current presenter view model. This string is the string representation
        /// of the decorated object.
        /// </summary>
        /// <returns>
        /// A string that represents the current presenter view model (or better the udnerlying object).
        /// </returns>
        public override string ToString()
        {
            return Object.ToString();
        }

        /// <summary>
        /// Gets the error message that must be displayed to the user when there were validation errors found.
        /// </summary>
        /// <returns>A string representing the validation error message.</returns>
        protected virtual string GetValidationErrorMessage()
        {
            return Resources.ItemNotModifiedDueToValidationErrors.EasyFormat(Object);
        }

        /// <summary>
        /// Gets the error message that must be displayed to the user when there were errors found.
        /// </summary>
        /// <returns>A string representing the error message.</returns>
        protected virtual string GetErrorMessage()
        {
            return Resources.ItemNotModified.EasyFormat(Object);
        }

        /// <summary>
        /// Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected virtual string SucessfullyUpdatedMessage
        {
            get { return Resources.ItemModified; }
        }

        /// <summary>
        /// Invoked when a property in the current entity presenter view model has been notified as changed.
        /// </summary>
        /// <param name="sender">The object sending the event.</param>
        /// <param name="e">Arguments containing the details of the event.</param>
        protected virtual void OnPropertyHasChanged(object sender, PropertyChangedEventArgs e)
        {
        }

        /// <summary>
        /// Commands to save the changes made to the underlying entity, sending an Update call to the services.
        /// </summary>
        /// <returns>An enumerable of strings being the errors there were found in the Update process (if any); or empty one in no errors where present.</returns>
        protected virtual bool UpdateEntity(List<string> errors)
        {
           
            return ExecuteUsingServices(services =>
            {
                // Do nothing if the entity cannot be updated or there cannot be executed the update altogether
                if (!Update || !services.CanUpdate(Object))
                    return false;

                try
                {
                    // Update the entity
                    services.Update(Object);
                    return true;
                }
                catch (ValidationException exception)
                {
                    // In case of validation errors, register them
                    errors.AddRange(exception.Errors);
                }

                return false;
            });
        }

        /// <summary>
        /// Executes the given method, not letting any update to be sent to the system's core. That is, when property changes or there is done something else that would
        /// incure in an entity update, such update command is ignored and not sent.
        /// </summary>
        /// <param name="method">The method to execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="method"/> is null.</exception>
        protected void DoWithoutUpdating(Action method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            try
            {
                Update = false;
                method();
            }
            finally
            {
                Update = true;
            }
        }

        /// <summary>
        /// Raises the ErrorsChanged event notifying that the errors situation for a property was changed.
        /// </summary>
        /// <param name="propertyName">The name of the property which errors has changed.</param>
        protected void RaiseErrorsChanged(string propertyName)
        {
            _errorsChangedEvent.CallEventHandlers(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the errors for the defined property.
        /// </summary>
        /// <typeparam name="TProperty">The type of the property's value.</typeparam>
        /// <param name="propertyExpression">An expression outlining the property which errors will be set.</param>
        /// <param name="errors">
        /// An <see cref="IEnumerable{T}"/> of <see cref="ValidationResult"/> containing the errors for the property specified
        /// by <paramref name="propertyExpression"/>.
        /// </param>
        protected void SetErrors<TProperty>(Expression<Func<TProperty>> propertyExpression, IEnumerable<ValidationResult> errors)
        {
            var member = propertyExpression.Body as MemberExpression;

            if (member == null)
                return;

            string propertyName = member.Member.Name;
            SetErrors(propertyName, errors);
        }

        /// <summary>
        /// Sets the errors for the defined property.
        /// </summary>
        /// <param name="propertyName">An name of the property which errors will be set.</param>
        /// <param name="errors">
        /// An <see cref="IEnumerable{T}"/> of <see cref="ValidationResult"/> containing the errors for the property specified
        /// by <paramref name="propertyName"/>.
        /// </param>
        protected void SetErrors(string propertyName, IEnumerable<ValidationResult> errors)
        {
            _errors.SetErrors(propertyName, errors);
            RaiseErrorsChanged(propertyName);
            OnPropertyChanged(() => HasErrors);
        }

        /// <summary>
        /// Validates the current presenter.
        /// </summary>
        /// <returns>An enumerable of strings representing the errors messages of the validation errors found (if any).</returns>
        protected IEnumerable<string> Validate()
        {
            return Validator.Validate(this);
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="TValue">Type of the property.</typeparam>
        /// <param name="setter">A setter method used to set the value of the property when it's considered OK to be done.</param>
        /// <param name="value">The new value to set to the property.</param>
        /// <param name="propertyName">
        /// This is the name of the property being set, used to notify later that it has changed. Recommended to be left null,
        /// if this method is called inside the property's setter, in order to leave the compiler supply the name of such property.
        /// </param>
        /// <exception cref="ArgumentNullException">Either <paramref name="setter"/> or <paramref name="propertyName"/> is null.</exception>
        /// <returns>True if the property was set; false otherwise.</returns>
        protected virtual bool SetProperty<TValue>(Action<TValue> setter, TValue value, [CallerMemberName] string propertyName = null)
        {
            if (setter == null)
                throw new ArgumentNullException("setter");
            if (propertyName == null)
                throw new ArgumentNullException("propertyName");

            bool isPropertySet = true;

            // Check whether the actual and the new values are equal, in which case, there cannot be set the property, no need to
            TValue actualValue = GetValue<TValue>(propertyName);
            if (Equals(actualValue, value) || ReferenceEquals(actualValue, value))
                return false;

            // Otherwise, set the property's new value
            setter(value);

            // Before continuing, the entity must be validated
            string statusBarMessage = SucessfullyUpdatedMessage.EasyFormat(Object);
            List<string> errors = Validate().ToList();
            IEnumerable<ValidationResult> validationErrors = new ValidationResult[0];

            // If validation goes well, command the services to save the property new value, and record any error that may come up in the process
            if (!errors.Any())
                isPropertySet = UpdateEntity(errors);

            // If then there are errors, then aggregate them all in a format that the validation engine understands
            if (errors.Any())
            {
                validationErrors = errors.Aggregate(new List<ValidationResult>(), (list, error) =>
                {
                    list.Add(new ValidationResult(false, error));
                    return list;
                });

                // Change the notification message to a one notifying about the validation errors
                statusBarMessage = GetValidationErrorMessage();
                isPropertySet = false;
            }

            // Returns the property to its former value if there were errors
            if (!isPropertySet)
            {
                setter(actualValue);
                if (!errors.Any())
                    statusBarMessage = GetErrorMessage();
            }

            // Now the set the found errors (empty is the entity is value, not empty when it has errors) for the property
            SetErrors(propertyName, validationErrors);

            // Send the notification to the user
            if(AllowUdpateNotifications)
            StatusBarServices.SignalText(statusBarMessage);

            // Notify WPF that the property has changed its value
            OnPropertyChanged(propertyName);

            return isPropertySet;
        }

        protected bool AllowUdpateNotifications = true;

        private TValue GetValue<TValue>(string propertyName)
        {
            Type type = GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            return (TValue)property.GetValue(this);
        }

        /// <summary>
        /// Gets the message that is used to notify about an update made to the current presenter view model.
        /// </summary>
        protected virtual string SuccessfullyUpdatedMessage
        {
            get { return Resources.ItemModified; }
        }

        /// <summary>
        /// Notify properties changes
        /// </summary>
        public virtual void Notify()
        {

            

        }
    }
}
