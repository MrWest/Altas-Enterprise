using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;
using CompanyName.Atlas.Contracts.Presentation.Prism;
using CompanyName.Atlas.Contracts.Properties;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Presentation.Data
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ViewModelBaseTests : BindableTestsBase<ViewModelBaseTests.ViewModelStub, ViewModelBaseTests.ViewModelStub>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Raised_ExecutesRegisteredHandlers()
        {
            // Arrange
            bool notified = false;
            EventHandler<InteractionRequestedEventArgs> handler = (sender, e) => notified = true;

            TestObject.Raised += handler;

            // Act
            TestObject.Raise(Mock.Of<INotification>());

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void Raised_DoesNotExecuteUnregisteredHandlers()
        {
            // Arrange
            bool notified = false;
            EventHandler<InteractionRequestedEventArgs> handler = (sender, e) => notified = true;

            TestObject.Raised += handler;
            TestObject.Raised -= handler;

            // Act
            TestObject.Raise(Mock.Of<INotification>());

            // Assert
            Assert.IsFalse(notified);
        }


        [TestMethod]
        public void SignalStatus_PassesTextToTheStatusBarServices()
        {
            // Act
            TestObject.Signal("A");

            // Assert
            StatusBarServicesMock.Verify(x => x.SignalText("A"));
        }


        [TestMethod]
        public void Confirm_RaisesRaisedEventSendingCorrectContext()
        {
            // Arrange
            bool notified = false;
            TestObject.Raised += (sender, e) => notified = true;

            // Act
            IConfirmation confirmation = TestObject.Confirm("A", "B");

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void Confirm_ReturnsConfirmationWithCorrectContent()
        {
            // Act
            IConfirmation confirmation = TestObject.Confirm("A", "B");

            // Assert
            Assert.AreEqual("A", confirmation.Content);
        }

        [TestMethod]
        public void Confirm_ReturnsConfirmationWithCorrectTitle()
        {
            // Act
            IConfirmation confirmation = TestObject.Confirm("A", "B");

            // Assert
            Assert.AreEqual("B", confirmation.Title);
        }

        [TestMethod]
        public void Confirm_ReturnsConfirmationWithCorrectReason()
        {
            // Act
            IConfirmation confirmation = TestObject.Confirm("A", "B");

            // Assert
            var notificationWithReason = (INotificationWithReason)confirmation;
            Assert.AreEqual(NotificationReason.Question, notificationWithReason.Reason);
        }


        [TestMethod]
        public void Notify_RaisesRaisedEventSendingCorrectContext()
        {
            // Arrange
            bool notified = false;
            TestObject.Raised += (sender, e) => notified = true;

            // Act
            TestObject.Notify("A", "B");

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void Notify_ReturnsNotificationWithCorrectContent()
        {
            // Act
            INotification notification = TestObject.Notify("A", "B");

            // Assert
            Assert.AreEqual("A", notification.Content);
        }

        [TestMethod]
        public void Notify_ReturnsNotificationWithCorrectTitle()
        {
            // Act
            INotification notification = TestObject.Notify("A", "B");

            // Assert
            Assert.AreEqual("B", notification.Title);
        }

        [TestMethod]
        public void Notify_ReturnsNotificationWithCorrectReason()
        {
            // Act
            INotification notification = TestObject.Notify("A", "B");

            // Assert
            var notificationWithReason = (INotificationWithReason)notification;
            Assert.AreEqual(NotificationReason.Info, notificationWithReason.Reason);
        }


        [TestMethod]
        public void NotifyError_RaisesRaisedEventSendingCorrectContext()
        {
            // Arrange
            bool notified = false;
            TestObject.Raised += (sender, e) => notified = true;

            // Act
            INotification notification = TestObject.NotifyError("A", "B");

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void NotifyError_ReturnsNotificationWithCorrectContent()
        {
            // Act
            INotification notification = TestObject.NotifyError("A", "B");

            // Assert
            Assert.AreEqual("A", notification.Content);
        }

        [TestMethod]
        public void NotifyError_ReturnsNotificationWithCorrectTitle()
        {
            // Act
            INotification notification = TestObject.NotifyError("A", "B");

            // Assert
            Assert.AreEqual("B", notification.Title);
        }

        [TestMethod]
        public void NotifyError_ReturnsNotificationWithCorrectReason()
        {
            // Act
            INotification notification = TestObject.NotifyError("A", "B");

            // Assert
            var notificationWithReason = (INotificationWithReason)notification;
            Assert.AreEqual(NotificationReason.Error, notificationWithReason.Reason);
        }


        [TestMethod]
        public void ExecuteSafely_NoException_MethodWithResult_ReturnsResultWithoutProblems()
        {
            // Act
            Type testObjectType = TestObject.GetType();
            MethodInfo method = testObjectType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "ExecuteSafely").First();
            method = method.MakeGenericMethod(typeof(int));

            var result = (int)method.Invoke(TestObject, new object[] { new Func<int>(() => 90) });

            // Assert
            Assert.AreEqual(90, result);
        }

        [TestMethod]
        public void ExecuteSafely_NoException_MethodWithoutResult_NothingHappens()
        {
            // Act
            Type testObjectType = TestObject.GetType();
            MethodInfo method = testObjectType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "ExecuteSafely").Last();

            bool called = false;
            method.Invoke(TestObject, new object[] { new Action(() => { called = true; }) });

            // Assert
            Assert.IsTrue(called);
        }

        [TestMethod]
        public void ExecuteSafely_ThrownException_MethodWithResult_NotifiesTheProblemCorrectly()
        {
            // Arrange
            var mock = new Mock<ViewModelBase>() { CallBase = true };
            ViewModelBase viewModel = mock.Object;

            // Act
            Type testObjectType = viewModel.GetType();
            MethodInfo method = testObjectType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "ExecuteSafely").First();
            method = method.MakeGenericMethod(typeof(int));

            var result = (int)method.Invoke(viewModel,
                new object[] 
                { 
                    new Func<int>(() => { throw new DataMisalignedException("A"); })
                });

            // Assert
            mock.Protected().Verify("NotifyError", Times.Once(), "A", Resources.UnexpectedExceptionTitle);
        }

        [TestMethod]
        public void ExecuteSafely_ThrownException_MethodWithoutResult_NotifiesTheProblemCorrectly()
        {
            // Arrange
            var mock = new Mock<ViewModelBase>() { CallBase = true };
            ViewModelBase viewModel = mock.Object;

            // Act
            Type testObjectType = viewModel.GetType();
            MethodInfo method = testObjectType.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).Where(x => x.Name == "ExecuteSafely").Last();

            method.Invoke(viewModel,
                new object[] 
                { 
                    new Action(() => { throw new DataMisalignedException("A"); })
                });

            // Assert
            mock.Protected().Verify("NotifyError", Times.Once(), "A", Resources.UnexpectedExceptionTitle);
        }


        public class ViewModelStub : ViewModelBase
        {
            public INotification LastRaisedNotification { get; set; }

            public INotification LastRaisedErrorNotification { get; set; }

            public INotification LastRaisedConfirmation { get; set; }


            public new IConfirmation Confirm(string text, string title)
            {
                return (IConfirmation)(LastRaisedConfirmation = base.Confirm(text, title));
            }

            public new INotification Notify(string text, string title)
            {
                return base.Notify(text, title);
            }

            public new virtual INotification NotifyError(string text, string title)
            {
                return (INotification)(LastRaisedErrorNotification = base.NotifyError(text, title));
            }

            public T Raise<T>(T notification) where T : INotification
            {
                return (T)(LastRaisedNotification = Interact(notification));
            }

            public void Signal(string text)
            {
                SignalStatus(text);
            }
        }
    }
}
