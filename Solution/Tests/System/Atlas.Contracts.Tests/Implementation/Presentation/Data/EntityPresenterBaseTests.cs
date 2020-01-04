using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Controls;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Validation;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Presentation.Data
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EntityPresenterBaseTests : PresenterTestsBase<IEntity, EntityPresenterBase<IEntity, IItemManagerApplicationServices<IEntity>>, IItemManagerApplicationServices<IEntity>>
    {
        private Mock<IValidatorFactory> _localValidatorFactory;
        private Mock<IValidator> _localValidator;


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            _localValidatorFactory = new Mock<IValidatorFactory>();
            ServiceLocatorMock.Setup(x => x.GetInstance<IValidatorFactory>()).Returns(_localValidatorFactory.Object);

            _localValidator = new Mock<IValidator>();
            _localValidatorFactory.Setup(x => x.CreateValidator(TestObject.GetType())).Returns(_localValidator.Object);

            ApplicationServicesMock.Setup(x => x.CanUpdate(Entity)).Returns(true);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNull_ThrowsException()
        {
            try
            {
                var r = new Mock<EntityPresenterBase<IEntity, IItemManagerApplicationServices<IEntity>>>((IEntity)null).Object;
            }
            catch (Exception e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void Constructor_GivenObject_InitializesTheObjectProperty()
        {
            // Assert
            Assert.AreSame(Entity, TestObject.Object);
            Assert.AreSame(Entity, ((IPresenter)TestObject).Object);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Object_GivenNullValue_ThrowsException()
        {
            TestObject.Object = null;
        }

        [TestMethod]
        public void Object_GivenValue_SetsIt()
        {
            // Arrange
            var value = Mock.Of<IEntity>();

            // Act
            TestObject.Object = value;

            // Assert
            Assert.AreSame(value, TestObject.Object);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Object_IfValueIsNotDefinedAndIsRequested_ThrowsException()
        {
            // Arrange
            TestMock = new Mock<EntityPresenterBase<IEntity, IItemManagerApplicationServices<IEntity>>>() { CallBase = true };
            TestObject = TestMock.Object;

            // Act
            var o = TestObject.Object;
        }


        [TestMethod]
        public void ToString_ReturnsUnderlyingObjectString()
        {
            // Act
            string actualString = TestObject.ToString();

            // Assert
            Assert.AreEqual(Entity.ToString(), actualString);
        }


        [TestMethod]
        public void PropertyChangedEvent_Raised_OnPropertyHasChangedMethodIsCalled()
        {
            // Act
            TestObject.Id = "890";

            // Assert
            CollectionAssert.Contains(ChangeTracker.ChangedProperties, "Id");
        }


        [TestMethod]
        public void Id_GivenDifferentValue_SetsValue()
        {
            TestGetsSetValue(x => x.Id, () => Guid.NewGuid().ToString().ToString());
        }

        [TestMethod]
        public void Id_GivenDifferentValue_NotifiesPropertyChanged()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Id, () => Guid.NewGuid().ToString(), () => Guid.NewGuid().ToString());
        }

        [TestMethod]
        public void Id_GivenSameValue_DoesNotNotifyPropertyChanged()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Id, () => Guid.NewGuid().ToString());
        }


        [TestMethod]
        public void FullName_ReturnsToStringMethodValue()
        {
            // Arrange
            string fullName = TestObject.FullName;

            // Assert
            Assert.AreEqual(TestObject.ToString(), fullName);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SetProperty_GivenNullSetter_Throw()
        {
            SetProperty(null,  90.ToString(), x => x.Id);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SetProperty_GivenNullPropertyName_Throw()
        {
            SetProperty(null,  90.ToString(), x => x.Id, true);
        }

        [TestMethod]
        public void SetProperty_GivenSameValue_EntityPropertyIsNotSet()
        {
            // Arrange
            Entity.Id = 90.ToString();
            EntityMock.ResetCalls();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            EntityMock.VerifySet(x => x.Id =  90.ToString(), Times.Never);
        }

        [TestMethod]
        public void SetProperty_GivenSameValue_DoesNotCommandsToValidate()
        {
            // Arrange
            Entity.Id = 90.ToString();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            _localValidator.Verify(x => x.Validate(TestObject), Times.Never);
        }

        [TestMethod]
        public void SetProperty_GivenSameValue_DoesNotNotifyChanges()
        {
            // Arrange
            Entity.Id = 90.ToString();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsFalse(ChangeTracker.ChangedProperties.Any());
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_SetsTheValueToTheEntity()
        {
            // Arrange
            Entity.Id = 80.ToString();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            EntityMock.VerifySet(x => x.Id =  90.ToString());
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_CommandsToValidate()
        {
            // Arrange
            Entity.Id = 80.ToString();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            _localValidator.Verify(x => x.Validate(TestObject));
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationFails_SetsTheEntityPropertyValueToTheFormer()
        {
            // Arrange
            Entity.Id = 80.ToString();
            EntityMock.ResetCalls();

            _localValidator.Setup(x => x.Validate(TestObject)).Returns(new[] { "Error" });

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            EntityMock.VerifySet(x => x.Id = 80.ToString());
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationFails_NotifiesPropertyChange()
        {
            // Arrange
            Entity.Id = 80.ToString();
            EntityMock.ResetCalls();

            _localValidator.Setup(x => x.Validate(TestObject)).Returns(new[] { "Error" });

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            CollectionAssert.Contains(ChangeTracker.ChangedProperties, ExtractPropertyName(x => x.Id));
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationFails_RaisesErrorsChangedEvent()
        {
            // Arrange
            Entity.Id = 80.ToString();

            bool notified = false;
            TestObject.ErrorsChanged += (sender, e) => notified |= e.PropertyName == ExtractPropertyName(x => x.Id);

            _localValidator.Setup(x => x.Validate(TestObject)).Returns(new[] { "Error" });

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationFails_HasError()
        {
            // Arrange
            Entity.Id = 80.ToString();

            _localValidator.Setup(x => x.Validate(TestObject)).Returns(new[] { "Error" });

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsTrue(TestObject.HasErrors);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationFails_NotifiesHasErrorPropertyChanged()
        {
            // Arrange
            Entity.Id = 80.ToString();

            _localValidator.Setup(x => x.Validate(TestObject)).Returns(new[] { "Error" });

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            CollectionAssert.Contains(ChangeTracker.ChangedProperties, ExtractPropertyName(x => x.HasErrors));
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationFails_ReturnsFalse()
        {
            // Arrange
            Entity.Id = 80.ToString();

            _localValidator.Setup(x => x.Validate(TestObject)).Returns(new[] { "Error" });

            // Act
            bool wasPropertySet = SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsFalse(wasPropertySet);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationFails_ValidationErrorsAreProperlyRegistered()
        {
            // Arrange
            Entity.Id = 80.ToString();

            _localValidator.Setup(x => x.Validate(TestObject)).Returns(new[] { "Error" });

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            CollectionAssert.Contains(TestObject.GetErrors(ExtractPropertyName(x => x.Id)).Cast<ValidationResult>().Select(x => x.ErrorContent).ToArray(), "Error");
            CollectionAssert.Contains(TestObject.GetErrors(ExtractPropertyName(x => x.Id)).Cast<ValidationResult>().Select(x => x.IsValid).ToArray(), false);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationFails_NotifiesInTheStatusBarTheValidationFailure()
        {
            // Arrange
            Entity.Id = 80.ToString();

            _localValidator.Setup(x => x.Validate(TestObject)).Returns(new[] { "Error" });

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            StatusBarServicesMock.Verify(x => x.SignalText(Resources.ItemNotModifiedDueToValidationErrors.EasyFormat(Entity)));
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationFails_SetsTheEntityPropertyValueToTheFormer()
        {
            // Arrange
            Entity.Id = 80.ToString();
            EntityMock.ResetCalls();

            ApplicationServicesMock.Setup(x => x.Update(Entity)).Throws(new DataMisalignedException("Failed"));

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            EntityMock.VerifySet(x => x.Id = 80.ToString());
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationFails_NotifiesPropertyChange()
        {
            // Arrange
            Entity.Id = 80.ToString();
            EntityMock.ResetCalls();

            ApplicationServicesMock.Setup(x => x.Update(Entity)).Throws(new DataMisalignedException("Failed"));

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            CollectionAssert.Contains(ChangeTracker.ChangedProperties, ExtractPropertyName(x => x.Id));
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationFails_RaisesErrorsChangedEvent()
        {
            // Arrange
            Entity.Id = 80.ToString();

            ApplicationServicesMock.Setup(x => x.Update(Entity)).Throws(new DataMisalignedException("Failed"));

            bool notified = false;
            TestObject.ErrorsChanged += (sender, e) => notified |= e.PropertyName == ExtractPropertyName(x => x.Id);

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationFails_HasNoError()
        {
            // Arrange
            Entity.Id = 80.ToString();

            ApplicationServicesMock.Setup(x => x.Update(Entity)).Throws(new DataMisalignedException("Failed"));

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsFalse(TestObject.HasErrors);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationFails_NotifiesHasErrorPropertyChanged()
        {
            // Arrange
            Entity.Id = 80.ToString();

            ApplicationServicesMock.Setup(x => x.Update(Entity)).Throws(new DataMisalignedException("Failed"));

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            CollectionAssert.Contains(ChangeTracker.ChangedProperties, ExtractPropertyName(x => x.HasErrors));
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationFails_ReturnsFalse()
        {
            // Arrange
            Entity.Id = 80.ToString();

            ApplicationServicesMock.Setup(x => x.Update(Entity)).Throws(new DataMisalignedException("Failed"));

            // Act
            bool wasPropertySet = SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsFalse(wasPropertySet);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationFails_ThereAreNoErrorsRegistered()
        {
            // Arrange
            Entity.Id = 80.ToString();

            ApplicationServicesMock.Setup(x => x.Update(Entity)).Throws(new DataMisalignedException("Failed"));

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsFalse(TestObject.GetErrors(ExtractPropertyName(x => x.Id)).Cast<ValidationResult>().Select(x => x.ErrorContent).Any());
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationFails_NotifiesInTheStatusBarTheValidationFailure()
        {
            // Arrange
            Entity.Id = 80.ToString();

            ApplicationServicesMock.Setup(x => x.Update(Entity)).Throws(new DataMisalignedException("Failed"));

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            StatusBarServicesMock.Verify(x => x.SignalText(Resources.ItemNotModified.EasyFormat(Entity)));
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationSucceed_DoesNotSetTheEntityPropertyValueToTheFormer()
        {
            // Arrange
            Entity.Id = 80.ToString();
            EntityMock.ResetCalls();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            EntityMock.VerifySet(x => x.Id = 80.ToString(), Times.Never);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationSucceed_NotifiesPropertyChange()
        {
            // Arrange
            Entity.Id = 80.ToString();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            CollectionAssert.Contains(ChangeTracker.ChangedProperties, ExtractPropertyName(x => x.Id));
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationSucceed_RaisesErrorsChangedEvent()
        {
            // Arrange
            Entity.Id = 80.ToString();

            bool notified = false;
            TestObject.ErrorsChanged += (sender, e) => notified |= e.PropertyName == ExtractPropertyName(x => x.Id);

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationSucceed_HasNoError()
        {
            // Arrange
            Entity.Id = 80.ToString();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsFalse(TestObject.HasErrors);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationSucceed_NotifiesHasErrorPropertyChanged()
        {
            // Arrange
            Entity.Id = 80.ToString();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            CollectionAssert.Contains(ChangeTracker.ChangedProperties, ExtractPropertyName(x => x.HasErrors));
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationSucceed_ReturnsTrue()
        {
            // Arrange
            Entity.Id = 80.ToString();

            // Act
            bool wasPropertySet = SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsTrue(wasPropertySet);
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationSucceed_ThereAreNoErrorsRegistered()
        {
            // Arrange
            Entity.Id = 80.ToString();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            Assert.IsFalse(TestObject.GetErrors(ExtractPropertyName(x => x.Id)).Cast<ValidationResult>().Select(x => x.ErrorContent).Any());
        }

        [TestMethod]
        public void SetProperty_GivenDifferentValue_ValidationSucceed_UpdateOperationSucceed_NotifiesInTheStatusBarTheValidationFailure()
        {
            // Arrange
            Entity.Id = 80.ToString();

            // Act
            SetProperty(v => Entity.Id = v,  90.ToString(), x => x.Id);

            // Assert
            StatusBarServicesMock.Verify(x => x.SignalText(Resources.ItemModified.EasyFormat(Entity)));
        }


        private bool SetProperty<TValue>(Action<TValue> setter, TValue value, Expression<Func<IEntity, TValue>> propertyExpression, bool passNullToPropertyName = false)
        {
            try
            {
                string propertyName = ExtractPropertyName(propertyExpression);
                MethodInfo method = TestObject.GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Single(x => x.Name == "SetProperty" && x.GetParameters().First().ParameterType.Name == "Action`1");
                method = method.MakeGenericMethod(typeof(TValue));
                return (bool)method.Invoke(TestObject, new object[] { setter, value, passNullToPropertyName ? null : propertyName });
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }
    }
}
