using System;
using System.Collections.Generic;
using System.ComponentModel.Edition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace Common.Tests.ComponentModel.Edition
{
    [TestClass, ExcludeFromCodeCoverage]
    public class EditionOverCopyStrategyTests
    {
        EditionStrategyBase<Owner> _target;

        private class Owner
        {
            public Owner()
            {
            }

            public Owner(string property1, int property4)
            {
                Property1 = property1;
                Property4 = property4;
            }

            public string Property1 { get; set; }
            public int Property2 { get; set; }
            public bool Property3 { get; set; }
            public int Property4 { get; private set; }
        }


        public class EditableObject
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }


        [TestInitialize]
        public void Initialize()
        {
            _target = new EditionOverCopyStrategy<Owner>();
        }

        [TestMethod]
        public void BeginEdition_Always_CopyTheEditingObjectToMakeEditionOverSuchCopy()
        {
            // Act
            var editableObject = new Owner
            {
                Property1 = "Property1",
                Property2 = 0
            };
            _target.EditingObject = editableObject;
            _target.BeginEdition();

            // Assert
            Owner copy = _target.EditingObject;
            Assert.AreNotEqual(editableObject, copy);
            Assert.AreEqual("Property1", copy.Property1);
            Assert.AreEqual(0, copy.Property2);
        }
        
        [TestMethod]
        public void CancelEdition_Always_RollsBackTheChangesPerformed()
        {
            // Arrange
            var editableObject = new Owner
            {
                Property1 = "Property1",
                Property2 = 0
            };
            _target.EditingObject = editableObject;
            _target.BeginEdition();
            
            // Act
            // Make some changes to it
            _target.EditingObject.Property1 = string.Empty;
            _target.EditingObject.Property2 = 100;
            _target.CancelEdition();

            // Assert
            Assert.AreEqual("Property1", editableObject.Property1);
            Assert.AreEqual(0, editableObject.Property2);
        }
        
        [TestMethod]
        public void EndsEdition()
        {
            // Arrange
            var editableObject = new Owner
            {
                Property1 = "Property1",
                Property2 = 0
            };
            _target.EditingObject = editableObject;
            _target.BeginEdition();
            
            // Act
            // Make some changes to it
            _target.Values["Property3"] = true;
            _target.EditingObject.Property1 = string.Empty;
            _target.EditingObject.Property2 = 100;
            _target.EndEdition();

            // Assert
            Assert.IsTrue(editableObject.Property3);
            Assert.AreEqual(string.Empty, editableObject.Property1);
            Assert.AreEqual(100, editableObject.Property2);
        }

        [TestMethod]
        public void HasChanges_NotInEdition_ReturnsFalse()
        {
            // Arrange
            var obj = new Owner("A", 90);
            // Act
            _target.EditingObject = obj;
            _target.BeginEdition();
            obj.Property1 = "B";
            _target.EndEdition();
            // Assert
            Assert.IsFalse(_target.HasChanges);
        }

        [TestMethod]
        public void HasChanges_WhenObjectInEditionSufferChangesInItsPublicWritableProperties_ReturnsTrue()
        {
            // Arrange
            var obj = new Owner("A", 90);
            // Act
            _target.EditingObject = obj;
            _target.BeginEdition();
            obj.Property1 = "B";
            // Assert
            Assert.IsTrue(_target.HasChanges);
        }

        [TestMethod]
        public void HasChanges_WhenChangesAreInPropertiesNotTakenIntoAccount_ReturnsFalse()
        {
            // Arrange
            var editableObject = new EditableObject { Name = "A", Age = 1 };

            // Prepare the edition strategy, but in this case removing the Name properties from the properties set to 
            // check when the changes are evaluated
            Type editableObjectType = typeof(EditableObject);
            PropertyInfo[] properties = editableObjectType.GetProperties().Where(p => p.Name == "Age").ToArray();
            
            var strategyMock = new Mock<EditionOverCopyStrategy<EditableObject>> { CallBase = true };
            strategyMock.Protected().SetupGet<IEnumerable<PropertyInfo>>("Properties").Returns(properties);
            EditionOverCopyStrategy<EditableObject> strategy = strategyMock.Object;
            strategy.EditingObject = editableObject;

            // Act, begin the edition
            strategy.BeginEdition();

            // Change the name property
            strategy.EditingObject.Name = "B";

            // Assert, since the Name property is ignored, the no changes must be detected
            Assert.IsFalse(strategy.HasChanges);
        }

        [TestMethod]
        public void HasChanges_WhenChangesAreInPropertiesNTakenIntoAccount_ReturnsFalse()
        {
            // Arrange
            var editableObject = new EditableObject { Name = "A", Age = 1 };

            // Prepare the edition strategy, but in this case removing the Name properties from the properties set to 
            // check when the changes are evaluated
            Type editableObjectType = typeof(EditableObject);
            PropertyInfo[] properties = editableObjectType.GetProperties().Where(p => p.Name == "Age").ToArray();

            var strategyMock = new Mock<EditionOverCopyStrategy<EditableObject>> { CallBase = true };
            strategyMock.Protected().SetupGet<IEnumerable<PropertyInfo>>("Properties").Returns(properties);
            EditionOverCopyStrategy<EditableObject> strategy = strategyMock.Object;
            strategy.EditingObject = editableObject;

            // Act, begin the edition
            strategy.BeginEdition();

            // Change the Age property
            strategy.EditingObject.Age = 10;

            // Assert, since the Age property is ignored, the changes must be detected
            Assert.IsTrue(strategy.HasChanges);
        }

        [TestMethod]
        public void HasChanges_WhenObjectInEditionSufferChangesInItsPublicReadOnlyProperties_ReturnsFalse()
        {
            // Arrange
            var obj = new Owner("A", 90);
            // Act
            _target.EditingObject = obj;
            _target.BeginEdition();
            var internals = new PrivateObject(obj);
            internals.SetProperty("Property4", 90);
            // Assert
            Assert.IsFalse(_target.HasChanges);
        }
    }
}