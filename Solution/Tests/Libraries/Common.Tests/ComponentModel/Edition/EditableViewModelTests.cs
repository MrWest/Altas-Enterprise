using System;
using System.ComponentModel;
using System.ComponentModel.Edition;
using System.Diagnostics.CodeAnalysis;
using Common.Tests.Stubs;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.ComponentModel.Edition
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EditableViewModelTests
    {
        EditableViewModel<EditableObjectStub> _target;
        Mock<IEditionStrategy<EditableObjectStub>> _strategyMock;
        IEditionStrategy<EditableObjectStub> _strategy;
        private EditableObjectStub _object;


        [TestInitialize]
        public void Initialize()
        {
            _object = new EditableObjectStub();

            _strategyMock = new Mock<IEditionStrategy<EditableObjectStub>>();
            _strategy = _strategyMock.Object;

            _target = new EditableViewModel<EditableObjectStub>(_object, _strategy);

            _strategyMock.SetupGet(s => s.EditingObject).Returns(_object);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullObject_ThrowsException()
        {
            new EditableViewModel<EditableObjectStub>(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GivenNullEditionStrategy_ThrowsException()
        {
            new EditableViewModel<EditableObjectStub>(new EditableObjectStub(), null);
        }

        [TestMethod]
        public void BeginEdition_Always_CallsSameMethodInEditionStrategy()
        {
            // Act
            _target.BeginEdit();
            // Assert
            _strategyMock.Verify(s => s.BeginEdition(), Times.Once);
        }

        [TestMethod]
        public void CancelEdition_Always_CallsSameMethodInEditionStrategy()
        {
            // Act
            _target.CancelEdit();
            // Assert
            _strategyMock.Verify(s => s.CancelEdition(), Times.Once);
        }
        
        [TestMethod]
        public void Object_Always_ReturnsWhatEverReturnsEditionStrategy()
        {
            // Act
            var obj = _target.Object;
            // Assert
            _strategyMock.Verify(s => s.EditingObject, Times.Once);
        }

        [TestMethod]
        public void EndEdition_Always_CallsSameMethodInEditionStrategy()
        {
            // Act
            _target.EndEdit();
            // Assert
            _strategyMock.Verify(s => s.EndEdition(), Times.Once);
        }

        [TestMethod]
        public void Constructor_Always_InitializesCorrectlyEditionStrategy()
        {
            // Arrange
            Initialize();
            // Assert
            Assert.AreEqual(_strategy, _target.Strategy);
        }

        [TestMethod]
        public void Constructor_IfNoArgs_CreatesDefaultEditionStrategy()
        {
            // Act
            _target = new EditableViewModel<EditableObjectStub>();
            // Assert
            Assert.IsInstanceOfType(_target.Strategy, typeof(EditionOverCopyStrategy<EditableObjectStub>));
        }

        [TestMethod]
        public void Constructor_IfNoArgs_CreatesDefaultObjectInDefaultEditionStrategy()
        {
            // Act
            _target = new EditableViewModel<EditableObjectStub>();
            // Assert
            Assert.IsNotNull(_target.Strategy.EditingObject);
        }

        [TestMethod]
        public void HasChanges_Always_ReturnsStrategyHasChangesResult()
        {
            // Arrange
            _strategyMock.SetupGet(s => s.HasChanges).Returns(true);
            // Act
            bool hasChanges = _target.HasChanges;
            // Assert
            Assert.IsTrue(hasChanges);
        }
    }
}