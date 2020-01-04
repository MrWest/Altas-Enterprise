using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Presentation.Data
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CrudViewModelBaseTests :
        CrudViewModelTestsBase<
            CrudViewModelBase<IEntity, IPresenter<IEntity>, IItemManagerApplicationServices<IEntity>>,
            IEntity, IPresenter<IEntity>, IItemManagerApplicationServices<IEntity>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Constructor_InitializesItemsCollection()
        {
            Assert.IsNotNull(TestObject.Items);
        }

        [TestMethod]
        public void Constructor_InitializesTheAddCommand()
        {
            Assert.IsNotNull(TestObject.AddCommand);
        }

        [TestMethod]
        public void Constructor_InitializesTheDeleteCommand()
        {
            Assert.IsNotNull(TestObject.DeleteCommand);
        }


        [TestMethod]
        public void SelectedItem_GivenNewValue_NotifiesPropertyChanges()
        {
            Func<IPresenter<IEntity>> func = Mock.Of<IPresenter<IEntity>>;
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.SelectedItem, func, func);
        }

        [TestMethod]
        public void SelectedItem_IsInterfaceExplicitImplementationAndGivenNewValue_NotifiesPropertyChanges()
        {
            // Arrange
            bool notified = false;
            TestObject.PropertyChanged += (sender, e) => notified |= e.PropertyName == "SelectedItem";

            // Act
            ((ICrudViewModel)TestObject).SelectedItem = Mock.Of<IPresenter<IEntity>>();

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void SelectedItem_GivenNewValue_SetsTheNewValue()
        {
            Func<IPresenter<IEntity>> func = Mock.Of<IPresenter<IEntity>>;
            TestGetsSetValue(x => x.SelectedItem, func);
        }

        [TestMethod]
        public void SelectedItem_IsInterfaceExplicitImplementationAndGivenNewValue_SetsTheNewValue()
        {
            // Arrange
            var presenter = Mock.Of<IPresenter<IEntity>>();

            // Act
            ((ICrudViewModel)TestObject).SelectedItem = presenter;

            // Assert
            Assert.AreEqual(presenter, TestObject.SelectedItem);
        }

        [TestMethod]
        public void SelectedItem_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.SelectedItem, Mock.Of<IPresenter<IEntity>>);
        }

        [TestMethod]
        public void SelectedItem_IsInterfaceExplicitImplementationAndGivenSameValue_DoesNotNotifyPropertyChanges()
        {
            // Arrange
            bool notified = false;
            TestObject.PropertyChanged += (sender, e) => notified |= e.PropertyName == "SelectedItem";

            // Act
            ((ICrudViewModel)TestObject).SelectedItem = null;

            // Assert
            Assert.IsFalse(notified);
        }


        [TestMethod]
        public void Items_ReturnsObservableCollection()
        {
            Assert.IsInstanceOfType(TestObject.Items, typeof(ObservableCollection<IPresenter<IEntity>>));
        }

        [TestMethod]
        public void Items_IsInterfaceExplicitImplementation_ReturnsSameCollectionAsTheImplicitImplementation()
        {
            Assert.AreSame(TestObject.Items, ((ICrudViewModel)TestObject).Items);
        }


        [TestMethod]
        public void Add_GivenAnItem_AddsItToTheItemsCollection()
        {
            // Arrange
            var presenter = Mock.Of<IPresenter<IEntity>>();

            // Act
            TestObject.Add(presenter);

            // Assert
            CollectionAssert.Contains(TestObject.Items.ToArray(), presenter);
        }

        [TestMethod]
        public void Add_GivenAnItem_AddsItToTheService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            // Act
            TestObject.Add(presenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Add(item));
        }

        [TestMethod]
        public void Add_GivenAnItem_SignalsOperationStatus()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            // Act
            TestObject.Add(presenter);

            // Assert
            string text = Resources.SuccessfullyAddedItemStatusMessage.EasyFormat(presenter);
            StatusBarServicesMock.Verify(x => x.SignalText(text));
        }

        [TestMethod]
        public void Add_GivenAnItem_DisposesService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            // Act
            TestObject.Add(presenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Dispose());
        }

        [TestMethod]
        public void Add_GivenNullItem_AddsAnNewItemDecoratedInItsCorrespondingPresenter()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            ApplicationServicesMock.Setup(x => x.Create()).Returns(item);
            SetupCreatePresenterFor(item, presenter);

            // Act
            TestObject.Add(null);

            // Assert
            CollectionAssert.Contains(TestObject.ToArray(), presenter);
        }

        [TestMethod]
        public void Add_GivenAnItem_RaisesAddedItemEvent()
        {
            // Arrange
            bool raised = false;
            TestObject.AddedItem += (sender, e) => raised = true;

            // Act
            TestObject.Add(Mock.Of<IPresenter<IEntity>>());

            // Assert
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void Add_GivenItem_UnexpectedErrorInTheProcess_NotificationIsDisplayedToTheUser()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            var exception = new InvalidOperationException();

            ApplicationServicesMock.Setup(x => x.Add(item)).Throws(exception);

            INotification notification = null;
            TestObject.Raised += (sender, e) => notification = e.Context;

            // Act
            TestObject.Add(presenter);

            // Assert
            Assert.IsNotNull(notification);
            Assert.AreEqual(exception.Message, notification.Content);
            Assert.AreEqual(Resources.UnexpectedExceptionTitle, notification.Title);
        }

        [TestMethod]
        public void Add_GivenItem_ValidationErrorInTheProcess_NotificationIsDisplayedToTheUser()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            var exception = new ValidationException();

            ApplicationServicesMock.Setup(x => x.Add(item)).Throws(exception);

            INotification notification = null;
            TestObject.Raised += (sender, e) => notification = e.Context;

            // Act
            TestObject.Add(presenter);

            // Assert
            Assert.IsNotNull(notification);
            Assert.AreNotEqual(exception.Message, notification.Content);
            Assert.AreNotEqual(Resources.UnexpectedExceptionTitle, notification.Title);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Delete_GivenNull_ThrowsException()
        {
            TestObject.Delete(null);
        }

        [TestMethod]
        public void Delete_GivenAnItemAndUserDoesNotAgree_DoesNothing()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.Items.Add(presenter);
            SetupConfirmationResponse(false);

            // Act
            TestObject.Delete(presenter);

            // Assert
            CollectionAssert.Contains(TestObject.Items.ToArray(), presenter);
        }

        [TestMethod]
        public void Delete_GivenAnItemAndUserAgrees_DeletesTheItem()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.Items.Add(presenter);
            SetupConfirmationResponse(true);

            // Act
            TestObject.Delete(presenter);

            // Assert
            CollectionAssert.DoesNotContain(TestObject.Items.ToArray(), presenter);
        }

        [TestMethod]
        public void Delete_GivenAnItemAndUserAgrees_SignalsOperationStatus()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.Items.Add(presenter);
            SetupConfirmationResponse(true);

            // Act
            TestObject.Delete(presenter);

            // Assert
            StatusBarServicesMock.Verify(x => x.SignalText(Resources.SuccessfullyDeletedItemStatusMessage.EasyFormat(presenter)));
        }

        [TestMethod]
        public void Delete_GivenAnItemAndUserAgrees_PassesTheCallToTheService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.Items.Add(presenter);
            SetupConfirmationResponse(true);

            // Act
            TestObject.Delete(presenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Delete(item));
        }

        [TestMethod]
        public void Delete_GivenAnItemAndUserAgrees_DisposesTheService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.Items.Add(presenter);
            SetupConfirmationResponse(true);

            // Act
            TestObject.Delete(presenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Dispose());
        }

        [TestMethod]
        public void Delete_GivenAnItemAndUserAgrees_RaisesDeleteedItemEvent()
        {
            // Arrange
            bool raised = false;
            TestObject.DeletedItem += (sender, e) => raised = true;

            SetupConfirmationResponse(true);

            // Act
            TestObject.Delete(Mock.Of<IPresenter<IEntity>>());

            // Assert
            Assert.IsTrue(raised);
        }

        [TestMethod]
        public void Delete_GivenItem_ErrorInTheProcess_NotificationIsDisplayedToTheUser()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            var exception = new InvalidOperationException();

            SetupConfirmationResponse(true);

            ApplicationServicesMock.Setup(x => x.Delete(item)).Throws(exception);

            INotification notification = null;
            TestObject.Raised += (sender, e) => notification = e.Context;

            // Act
            TestObject.Delete(presenter);

            // Assert
            Assert.IsNotNull(notification);
            Assert.AreEqual(exception.Message, notification.Content);
            Assert.AreEqual(Resources.UnexpectedExceptionTitle, notification.Title);
        }

        [TestMethod]
        public void Delete_GivenItem_ValidationErrorInTheProcess_NotificationIsDisplayedToTheUser()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            var exception = new ValidationException();

            SetupConfirmationResponse(true);

            ApplicationServicesMock.Setup(x => x.Delete(item)).Throws(exception);

            INotification notification = null;
            TestObject.Raised += (sender, e) => notification = e.Context;

            // Act
            TestObject.Delete(presenter);

            // Assert
            Assert.IsNotNull(notification);
            Assert.AreNotEqual(exception.Message, notification.Content);
            Assert.AreNotEqual(Resources.UnexpectedExceptionTitle, notification.Title);
        }


        [TestMethod]
        public void AddCommand_CanExecute_ReturnsCorrectResponse()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            ApplicationServicesMock.Setup(x => x.CanAdd(item)).Returns(true);

            // Act
            bool canAdd = TestObject.AddCommand.CanExecute(presenter);

            // Assert
            Assert.IsTrue(canAdd);
        }

        [TestMethod]
        public void AddCommand_Execute_GivenAnItem_CallsCorrespondingAddMethod()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestMock.Setup(x => x.Add(presenter));

            // Act
            TestObject.AddCommand.Execute(presenter);

            // Assert
            TestMock.Verify(x => x.Add(presenter));
        }

        [TestMethod]
        public void AddCommand_Execute_GivenNoItem_CallsCorrespondingAddMethodWithNewSelfCreatedItem()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestMock.Setup(x => x.Add(presenter));
            ApplicationServicesMock.Setup(x => x.Create()).Returns(item);
            SetupCreatePresenterFor(item, presenter);

            // Act
            TestObject.AddCommand.Execute(null);

            // Assert
            CollectionAssert.Contains(TestObject.Items.ToArray(), presenter);
        }


        [TestMethod]
        public void DeleteCommand_CanExecute_ReturnsCorrectResponse()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestMock.Setup(x => x.CanDelete(presenter)).Returns(true);

            // Act
            bool canDelete = TestObject.DeleteCommand.CanExecute(presenter);

            // Assert
            Assert.IsTrue(canDelete);
        }

        [TestMethod]
        public void DeleteCommand_Execute_GivenAnItem_CallsCorrespondingDeleteMethod()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestMock.Setup(x => x.Delete(presenter));

            // Act
            TestObject.DeleteCommand.Execute(presenter);

            // Assert
            TestMock.Verify(x => x.Delete(presenter));
        }


        [TestMethod]
        public void GetEnumerator_ReturnsProperIteratorAllowingIterationOverTheItems()
        {
            // Arrange
            IPresenter<IEntity> presenter1 = Mock.Of<IPresenter<IEntity>>(), presenter2 = Mock.Of<IPresenter<IEntity>>();
            TestObject.Items.Add(presenter1);
            TestObject.Items.Add(presenter2);

            // Act
            IPresenter<IEntity>[] actual = TestObject.ToArray();

            // Assert
            IPresenter<IEntity>[] expected = { presenter2, presenter1 };
            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void GetEnumerator_IsInterfaceExplicitImplementation_ReturnsProperIteratorAllowingIterationOverTheItems()
        {
            // Arrange
            IPresenter<IEntity> presenter1 = Mock.Of<IPresenter<IEntity>>(), presenter2 = Mock.Of<IPresenter<IEntity>>();
            TestObject.Items.Add(presenter1);
            TestObject.Items.Add(presenter2);

            // Act
            IPresenter<IEntity>[] actual = ((IEnumerable)TestObject).OfType<IPresenter<IEntity>>().ToArray();

            // Assert
            IPresenter<IEntity>[] expected = { presenter2, presenter1 };
            CollectionAssert.AreEquivalent(expected, actual);
        }


        [TestMethod]
        public void Load_PopulatesTheViewModelWithTheItemsInTheService()
        {
            // Arrange
            IEntity[] items = { Mock.Of<IEntity>(), Mock.Of<IEntity>() };
            ApplicationServicesMock.Setup(x => x.Items).Returns(items);
            IPresenter<IEntity>[] presenters =
            {
                Mock.Of<IPresenter<IEntity>>(x => x.Object == items[0]),
                Mock.Of<IPresenter<IEntity>>(x => x.Object == items[1])
            };
            SetupCreatePresenterFor(items[0], presenters[0]);
            SetupCreatePresenterFor(items[1], presenters[1]);

            // Act
            TestObject.Load();

            // Assert
            IPresenter<IEntity>[] actualItems = TestObject.Items.ToArray();
            CollectionAssert.AreEquivalent(presenters, actualItems);
        }

        [TestMethod]
        public void Load_ThrowsException_NotifiesAboutTheErrorGracefully()
        {
            // Arrange
            Exception exception = new InvalidCastException();
            ApplicationServicesMock.Setup(x => x.Items).Throws(exception);

            INotification notification = null;
            TestObject.Raised += (sender, e) => notification = e.Context;

            // Act
            TestObject.Load();

            // Assert
            Assert.IsNotNull(notification);
            Assert.AreEqual(exception.Message, notification.Content);
            Assert.AreEqual(Resources.UnexpectedExceptionTitle, notification.Title);
        }


        [TestMethod]
        public void CanAdd_GivenNull_PassesCallToServiceToCorrectMethodOverload()
        {
            // Arrange
            ApplicationServicesMock.Setup(x => x.CanAddNew()).Returns(true);

            // Act
            bool canAdd = TestObject.CanAdd(null);

            // Assert
            Assert.IsTrue(canAdd);
        }

        [TestMethod]
        public void CanAdd_GivenItem_PassesCallToService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            // Act
            TestObject.CanAdd(presenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.CanAdd(item));
        }

        [TestMethod]
        public void CanAdd_GivenItem_DisposesService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            // Act
            TestObject.CanAdd(presenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Dispose());
        }

        [TestMethod]
        public void CanAdd_ThrowsException_NotifiesAboutTheErrorGracefully()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            Exception exception = new InvalidCastException();
            ApplicationServicesMock.Setup(x => x.CanAdd(item)).Throws(exception);

            INotification notification = null;
            TestObject.Raised += (sender, e) => notification = e.Context;

            // Act
            TestObject.CanAdd(presenter);

            // Assert
            Assert.IsNotNull(notification);
            Assert.AreEqual(exception.Message, notification.Content);
            Assert.AreEqual(Resources.UnexpectedExceptionTitle, notification.Title);
        }


        [TestMethod]
        public void CanDelete_GivenNullItem_ReturnsFalse()
        {
            // Act
            bool canDelete = TestObject.CanDelete(null);

            // Assert
            Assert.IsFalse(canDelete);
        }

        [TestMethod]
        public void CanDelete_GivenItem_PassesCallToServices()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            // Act
            TestObject.CanDelete(presenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.CanDelete(item));
        }

        [TestMethod]
        public void CanDelete_GivenItem_DisposesServices()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            // Act
            TestObject.CanDelete(presenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Dispose());
        }

        [TestMethod]
        public void CanDelete_ThrowsException_NotifiesAboutTheErrorGracefully()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            Exception exception = new InvalidCastException();
            ApplicationServicesMock.Setup(x => x.CanDelete(item)).Throws(exception);

            INotification notification = null;
            TestObject.Raised += (sender, e) => notification = e.Context;

            // Act
            TestObject.CanDelete(presenter);

            // Assert
            Assert.IsNotNull(notification);
            Assert.AreEqual(exception.Message, notification.Content);
            Assert.AreEqual(Resources.UnexpectedExceptionTitle, notification.Title);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void CreatePresenterFor_GivenNullItem_ThrowsException()
        {
            try
            {
                // Act
                GetPresenterFor(null);
            }
            catch (TargetInvocationException e)
            {
                throw e.InnerException;
            }
        }

        [TestMethod]
        public void CreatePresenterFor_GivenItem_ReturnsInstanceOfPresenterWithItsObjectReferenceInitialized()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var presenterMock = new Mock<IPresenter<IEntity>>();
            IPresenter<IEntity> presenter = presenterMock.Object;
            CreateMock();
            CreateInstance();
            ServiceLocatorMock.Setup(x => x.GetInstance<IPresenter<IEntity>>()).Returns(presenter);

            // Act
            IPresenter<IEntity> actualPresenter = GetPresenterFor(item);
            
            // Assert
            Assert.AreSame(presenter, actualPresenter);
            presenterMock.VerifySet(x => x.Object = item);
        }


        [TestMethod]
        public void GetItems_GetsServicesItems()
        {
            // Arrange
            IEntity[] items = { Mock.Of<IEntity>(), Mock.Of<IEntity>() };
            ApplicationServicesMock.SetupGet(x => x.Items).Returns(items);

            // Act
            var actualItems = (IEnumerable<IEntity>)TestObjectInternals.Invoke("GetItems", ApplicationServices);

            // Assert
            CollectionAssert.AreEquivalent(items, actualItems.ToArray());
        }


        [TestMethod]
        public void Find_IsInterfaceExplicitImplementation_ReturnsProperMethodOverload()
        {
            // Act
            ((ICrudViewModel)TestObject).Find(90);

            // Assert
            TestMock.Verify(x => x.Find(90));
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Find_FindsMoreThanOneResult_ThrowsException()
        {
            // Arrange
            var entity1 = Mock.Of<IEntity>(x => x.Id == 1.ToString());
            var entity2 = Mock.Of<IEntity>(x => x.Id == 1.ToString());
            var presenter1 = Mock.Of<IPresenter<IEntity>>(x => x.Object == entity1);
            var presenter2 = Mock.Of<IPresenter<IEntity>>(x => x.Object == entity2);
            TestMock.SetupGet(x => x.Items).Returns(new[] { presenter1, presenter2 });

            // Act
            TestObject.Find(1);
        }

        [TestMethod]
        public void Find_ThereIsAMatch_ReturnsIt()
        {
            // Arrange
            var entity1 = Mock.Of<IEntity>(x => x.Id == 1.ToString());
            var entity2 = Mock.Of<IEntity>(x => x.Id == 2.ToString());
            var presenter1 = Mock.Of<IPresenter<IEntity>>(x => x.Object == entity1);
            var presenter2 = Mock.Of<IPresenter<IEntity>>(x => x.Object == entity2);
            TestMock.SetupGet(x => x.Items).Returns(new[] { presenter1, presenter2 });

            // Act
            IPresenter<IEntity> presenter = TestObject.Find(2);

            // Assert
            Assert.AreSame(presenter2, presenter);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void SignalStatus_GivenNullText_Throws()
        {
            TestObjectInternals.Invoke("SignalStatus", (string)null);
        }

        [TestMethod]
        public void SignalStatus_GivenText_SignalsItInTheStatusBarServices()
        {
            // Act
            TestObjectInternals.Invoke("SignalStatus", "A");

            // Assert
            StatusBarServicesMock.Verify(x => x.SignalText("A"));
        }
    }
}
