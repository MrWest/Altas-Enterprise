using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Presentation.Data
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EnableableCrudViewModelBaseTests :
        CrudViewModelTestsBase<EnableableCrudViewModelBase<IEntity, IPresenter<IEntity>, IItemManagerApplicationServices<IEntity>>, IEntity, IPresenter<IEntity>, IItemManagerApplicationServices<IEntity>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Constructor_InitializesIsEnabledToTrue()
        {
            Assert.IsTrue(TestObject.IsEnabled);
        }


        [TestMethod]
        public void IsEnabled_GivenDifferentValue_SetsTheNewValue()
        {
            TestGetsSetValue(x => x.IsEnabled, () => false);
        }

        [TestMethod]
        public void IsEnabled_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.IsEnabled, () => false, () => true);
        }

        [TestMethod]
        public void IsEnabled_GivenSameValue_DoesNotNotifyPropertyChange()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.IsEnabled, () => false);
        }


        [TestMethod]
        public void Add_GivenItemPresenter_ViewModelIsDisabled_NoCallPassesToTheService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = false;

            // Act
            TestObject.Add(itemPresenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Add(item), Times.Never);
        }

        [TestMethod]
        public void Add_GivenItemPresenter_ViewModelIsDisabled_ServicesDoesNotGetDisposed()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = false;

            // Act
            TestObject.Add(itemPresenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Dispose(), Times.Never);
        }

        [TestMethod]
        public void Add_GivenItemPresenter_ViewModelIsDisabled_GivenItemPresenterGetsNotAddedToTheViewModel()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = false;

            // Act
            TestObject.Add(itemPresenter);

            // Assert
            CollectionAssert.DoesNotContain(TestObject.ToArray(), itemPresenter);
        }

        [TestMethod]
        public void Add_GivenItemPresenter_ViewModelIsDisabled_CallIsPassedToTheService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = true;

            // Act
            TestObject.Add(itemPresenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Add(item));
        }

        [TestMethod]
        public void Add_GivenItemPresenter_ViewModelIsEnabled_ServicesDoesNotGetDisposed()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = true;

            // Act
            TestObject.Add(itemPresenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Dispose());
        }

        [TestMethod]
        public void Add_GivenItemPresenter_ViewModelIsEnabled_GivenItemPresenterGetsNotAddedToTheViewModel()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = true;

            // Act
            TestObject.Add(itemPresenter);

            // Assert
            CollectionAssert.Contains(TestObject.ToArray(), itemPresenter);
        }


        [TestMethod]
        public void Delete_GivenItemPresenter_ViewModelIsDisabled_NoCallPassesToTheService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = false;

            TestObject.Raised += (sender, e) =>
            {
                ((Confirmation)e.Context).Confirmed = true;
            };

            // Act
            TestObject.Delete(itemPresenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Delete(item), Times.Never);
        }

        [TestMethod]
        public void Delete_GivenItemPresenter_ViewModelIsDisabled_ServicesDoesNotGetDisposed()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = false;

            TestObject.Raised += (sender, e) =>
            {
                ((Confirmation)e.Context).Confirmed = true;
            };

            // Act
            TestObject.Delete(itemPresenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Dispose(), Times.Never);
        }

        [TestMethod]
        public void Delete_GivenItemPresenter_ViewModelIsDisabled_GivenItemPresenterGetsNotDeletedFromTheViewModel()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.Items.Add(itemPresenter);

            TestObject.IsEnabled = false;

            TestObject.Raised += (sender, e) =>
            {
                ((Confirmation)e.Context).Confirmed = true;
            };

            // Act
            TestObject.Delete(itemPresenter);

            // Assert
            CollectionAssert.Contains(TestObject.ToArray(), itemPresenter);
        }

        [TestMethod]
        public void Delete_GivenItemPresenter_ViewModelIsEnabled_CallIsPassedToTheService()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            TestObject.IsEnabled = true;

            SetupConfirmationResponse(true);

            // Act
            TestObject.Delete(itemPresenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Delete(item));
        }

        [TestMethod]
        public void Delete_GivenItemPresenter_ViewModelIsEnabled_ServicesDoesNotGetDisposed()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);

            TestObject.IsEnabled = true;

            SetupConfirmationResponse(true);

            // Act
            TestObject.Delete(itemPresenter);

            // Assert
            ApplicationServicesMock.Verify(x => x.Dispose());
        }

        [TestMethod]
        public void Delete_GivenItemPresenter_ViewModelIsEnabled_GivenItemPresenterGetsNotDeletedToTheViewModel()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.Items.Add(itemPresenter);

            TestObject.IsEnabled = true;

            SetupConfirmationResponse(true);

            // Act
            TestObject.Delete(itemPresenter);

            // Assert
            CollectionAssert.DoesNotContain(TestObject.ToArray(), itemPresenter);
        }


        [TestMethod]
        public void OnPresenterPropertyChanged_IfDisabled_DoesNotSendUpdateCallToServices()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            var itemPresenterMock = Mock.Get(itemPresenter);
            
            ApplicationServicesMock.Setup(x => x.Items).Returns(new[] { item });
            SetupCreatePresenterFor(item, itemPresenter);

            TestObject.IsEnabled = false;

            // Act
            itemPresenterMock.Raise(x => x.PropertyChanged += null, new PropertyChangedEventArgs("Id"));

            // Assert
            ApplicationServicesMock.Verify(x => x.Update(item), Times.Never);
        }

        [TestMethod]
        public void OnPresenterPropertyChanged_IfDisabled_DoesNotDisposesServices()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            var itemPresenterMock = Mock.Get(itemPresenter);

            ApplicationServicesMock.Setup(x => x.Items).Returns(new[] { item });
            SetupCreatePresenterFor(item, itemPresenter);

            TestObject.IsEnabled = false;

            // Act
            itemPresenterMock.Raise(x => x.PropertyChanged += null, new PropertyChangedEventArgs("Id"));

            // Assert
            ApplicationServicesMock.Verify(x => x.Dispose(), Times.Never);
        }


        [TestMethod]
        public void CanAdd_IfDisabled_ReturnsFalse()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = false;

            ApplicationServicesMock.Setup(x => x.CanAdd(item)).Returns(true);
            
            // Act
            bool canAdd = TestObject.CanAdd(itemPresenter);

            // Assert
            Assert.IsFalse(canAdd);
        }

        [TestMethod]
        public void CanAdd_IfEnabled_ReturnsTrue()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = true;

            ApplicationServicesMock.Setup(x => x.CanAdd(item)).Returns(true);

            // Act
            bool canAdd = TestObject.CanAdd(itemPresenter);

            // Assert
            Assert.IsTrue(canAdd);
        }

        [TestMethod]
        public void CanAdd_IfDisabled_ReturnsFalse1()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = false;

            ApplicationServicesMock.Setup(x => x.CanAdd(item)).Returns(true);

            // Act
            bool canAdd = TestObject.CanAdd(itemPresenter);

            // Assert
            Assert.IsFalse(canAdd);
        }

        [TestMethod]
        public void CanAdd_IfEnabled_ReturnsTrue1()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = true;

            ApplicationServicesMock.Setup(x => x.CanAdd(item)).Returns(true);

            // Act
            bool canAdd = TestObject.CanAdd(itemPresenter);

            // Assert
            Assert.IsTrue(canAdd);
        }


        [TestMethod]
        public void CanDelete_IfDisabled_ReturnsFalse()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = false;

            // Act
            bool canDelete = TestObject.CanDelete(itemPresenter);

            // Assert
            Assert.IsFalse(canDelete);
        }

        [TestMethod]
        public void CanDelete_IfEnabled_ReturnsTrue()
        {
            // Arrange
            var item = Mock.Of<IEntity>();
            var itemPresenter = Mock.Of<IPresenter<IEntity>>(x => x.Object == item);
            TestObject.IsEnabled = true;

            ApplicationServicesMock.Setup(x => x.CanDelete(item)).Returns(true);

            // Act
            bool canDelete = TestObject.CanDelete(itemPresenter);

            // Assert
            Assert.IsTrue(canDelete);
        }
    }
}
