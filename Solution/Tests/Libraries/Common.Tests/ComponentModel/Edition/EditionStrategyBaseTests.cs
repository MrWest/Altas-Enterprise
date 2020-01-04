using System;
using System.ComponentModel.Edition;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace Common.Tests.ComponentModel.Edition
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditionStrategyBaseTests
    {
        private EditionStrategyBase<object> _target;
        private Mock<EditionStrategyBase<object>> _targetMock;


        [TestInitialize]
        public void Initialize()
        {
            _targetMock = new Mock<EditionStrategyBase<object>> { CallBase = true };
            _target = _targetMock.Object;
        }


        [TestMethod]
        public void BeginEdition_Always_CallsInternalBeginEdition()
        {
            // Act
            _target.BeginEdition();

            // Assert
            _targetMock.Protected().Verify("InternalBeginEdition", Times.Once());
        }

        [TestMethod]
        public void CancelEdition_Always_CallsInternalCancelEdition()
        {
            // Act
            _target.BeginEdition();
            _target.CancelEdition();
            // Assert
            _targetMock.Protected().Verify("InternalCancelEdition", Times.Once());
        }

        [TestMethod]
        public void EndEdition_Always_CallsInternalEndEdition()
        {
            // Act
            _target.BeginEdition();
            _target.EndEdition();

            // Assert
            _targetMock.Protected().Verify("InternalEndEdition", Times.Once());
        }

        [TestMethod]
        public void BeginEdition_Always_NotifiesEditionStarted()
        {
            // Arrange
            bool notified = false;
            EventHandler handler = (sender, e) => notified = true;
            _target.BeganEdition += handler;

            // Act
            _target.BeginEdition();

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void BeginEdition_IfEditing_DoesNothing()
        {
            // Arrange
            _target.BeginEdition();
            _targetMock.ResetCalls();
            // Act
            _target.BeginEdition();
            // Assert
            _targetMock.Protected().Verify("InternalBeginEdition", Times.Never());
        }

        [TestMethod]
        public void CancelEdition_Always_NotifiesEditionCancelled()
        {
            // Arrange
            _target.BeginEdition();
            bool notified = false;
            EventHandler handler = (sender, e) => notified = true;
            _target.CancelledEdition += handler;

            // Act
            _target.CancelEdition();

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void EndEdition_Always_NotifiesEditionEnded()
        {
            // Arrange
            _target.BeginEdition();
            bool notified = false;
            EventHandler handler = (sender, e) => notified = true;
            _target.EndedEdition += handler;

            // Act
            _target.EndEdition();

            // Assert
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void CancelEdition_IfNotEditing_DoesNothing()
        {
            // Act
            _target.CancelEdition();
            // Assert
            _targetMock.Protected().Verify("InternalCancelEdition", Times.Never());
        }

        [TestMethod]
        public void EndEdition_IfNotEditing_ThrowsException()
        {
            // Act
            _target.EndEdition();
            // Assert
            _targetMock.Protected().Verify("InternalEndEdition", Times.Never());
        }
    }
}