using Moq;
using CompanyName.Atlas.Contracts.Domain.Validation;
using System.Collections.Generic;

namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    /// <summary>
    /// Base class of the test classes used to test other classes involving validation.
    /// </summary>
    /// <typeparam name="T">The type of the class being tested.</typeparam>
    /// <typeparam name="TValidatable">The type of the class supporting validation.</typeparam>
    public abstract class ValidatedTestsBase<T, TValidatable> : MockedTestBase<T> where T : class
    {
        /// <summary>
        /// Initializes the scena
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            CreateValidatorFactoryMock();
            CreateValidatorFactory();
            ServiceLocatorMock.Setup(x => x.GetInstance<IValidatorFactory>()).Returns(ValidatorFactory);

            CreateValidatorMock();
            CreateValidator();
            ValidatorFactoryMock.Setup(x => x.CreateValidator<TValidatable>()).Returns(Validator);
            ValidatorFactoryMock.Setup(x => x.CreateValidator(TestObject.GetType())).Returns(Validator);
        }


        /// <summary>
        /// Gets the mock of the validator factory used by the tested domain services object.
        /// </summary>
        public Mock<IValidatorFactory> ValidatorFactoryMock { get; private set; }

        /// <summary>
        /// Gets the mocked instance of the validator factory used by the tested domain services object.
        /// </summary>
        public IValidatorFactory ValidatorFactory { get; private set; }

        /// <summary>
        /// Gets the mock of the validator used by the tested domain services object.
        /// </summary>
        public Mock<IValidator<TValidatable>> ValidatorMock { get; private set; }

        /// <summary>
        /// Gets the mocked instance of the validator used by the tested domain services object.
        /// </summary>
        public IValidator<TValidatable> Validator { get; private set; }

        /// <summary>
        /// Creates an assigns to the ValidatorFactoryMock property the mock of the validator factory used by the tested domain services.
        /// </summary>
        protected virtual void CreateValidatorFactoryMock()
        {
            ValidatorFactoryMock = new Mock<IValidatorFactory>();
        }

        /// <summary>
        /// Creates an assigns to the ValidatorFactory property the mocked instance of the validator factory used by the tested domain services.
        /// </summary>
        protected virtual void CreateValidatorFactory()
        {
            ValidatorFactory = ValidatorFactoryMock.Object;
        }

        /// <summary>
        /// Creates an assigns to the ValidatorMock property the mock of the validator used by the tested domain services.
        /// </summary>
        protected virtual void CreateValidatorMock()
        {
            ValidatorMock = new Mock<IValidator<TValidatable>>();
        }

        /// <summary>
        /// Creates an assigns to the Validator property the mocked instance of the validator used by the tested domain services.
        /// </summary>
        protected virtual void CreateValidator()
        {
            Validator = ValidatorMock.Object;
        }


        /// <summary>
        /// Sets the validation to make its result be like a valid object state.
        /// </summary>
        /// <param name="errors">The set of errors to be returned by the validation.</param>
        protected void SetValidTheValidationResults(TValidatable validatable, params string[] errors)
        {
            ValidatorMock.Setup(x => x.Validate(validatable)).Returns(errors);
        }

        /// <summary>
        /// Commands to validate the presenter.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> with strings representing validation error messages; or an empty one if no errors has
        /// been found in the validation.
        /// </returns>
        protected IEnumerable<string> Validate()
        {
            return (IEnumerable<string>)TestObjectInternals.Invoke("Validate");
        }
    }
}