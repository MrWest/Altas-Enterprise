using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Edition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.ComponentModel.Edition
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditionOverCopyStrategyExtensionsTests
    {
        [TestInitialize]
        public void Initialize()
        {
            _comparer = new Comparer();

            _owner = new Owner();
            _owned1 = new Owned
            {
                Id = 1,
                Name = "Owned1",
                Owner = _owner
            };
            _owned2 = new Owned
            {
                Id = 2,
                Name = "Owned2",
                Owner = _owner
            };
            _owner.Fakes.Add(_owned1);
            _owner.Fakes.Add(_owned2);

            _target = new EditionOverCopyStrategy<Owner>();
            _target.EditingObject = _owner;
            _targetInternals = new PrivateObject(_target);
            _target.BeginEdition();
        }

        Owner _owner;
        Owned _owned1;
        Owned _owned2;
        EditionOverCopyStrategy<Owner> _target;
        PrivateObject _targetInternals;
        IEqualityComparer<Owned> _comparer;

        class Base
        {
            public int Id { get; set; }
        }

        class Owner : Base
        {
            public Owner()
            {
                Fakes = new Collection<Owned>();
            }

            public Collection<Owned> Fakes { get; set; }
        }

        class Owned : Base
        {
            public string Name { get; set; }
            public Owner Owner { get; set; }
        }

        class Comparer : EqualityComparer<Base>
        {
            public override bool Equals(Base x, Base y)
            {
                return x != null && y != null && x.Id == y.Id;
            }

            public override int GetHashCode(Base obj)
            {
                if (obj == null)
                    throw new ArgumentNullException("obj");
                return obj.GetHashCode();
            }
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyOwnedCollection_GivenNullCollectionSetterDelegate_ThrowsException()
        {
            _target.CopyOwnedCollection(_owner, _owner, _owner.Fakes, null, new string[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyOwnedCollection_GivenNullCopyObject_ThrowsException()
        {
            _target.CopyOwnedCollection(_owner, null, _owner.Fakes, (o, f) => { }, new string[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyOwnedCollection_GivenNullGetCollectionDelegate_ThrowsException()
        {
            _target.CopyOwnedCollection<Owner, Owned>(_owner, _owner, null, (o, f) => { }, new string[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyOwnedCollection_GivenNullOriginalObject_ThrowsException()
        {
            _target.CopyOwnedCollection<Owner, Owned>(null, null, null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyOwnedCollection_GivenNullPropertiesNamesArray_ThrowsException()
        {
            _target.CopyOwnedCollection(_owner, _owner, _owner.Fakes, (o, f) => { }, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyOwnedCollection_GivenNullStrategy_ThrowsException()
        {
            EditionOverCopyStrategyExtensions.CopyOwnedCollection<Owner, Owned>(null, null, null, null, null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyOwnedCollection_GivenPropertiesNamesArrayWithNullStrings_ThrowsException()
        {
            _target.CopyOwnedCollection(_owner, _owner, _owner.Fakes, (o, f) => { }, new[] { null, "A" });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyOwnedCollection_GivenPropertiesNamesArrayWithIncorrectPropertiesNames_ThrowsException()
        {
            _target.CopyOwnedCollection(_owner, _owner, _owner.Fakes, (o, f) => { }, new[] { "Name", "A" });
        }

        [TestMethod]
        public void CopyOwnedCollection_WhenCalledCorrectly_CopiesOwnedCollection()
        {
            // Act
            _target.CopyOwnedCollection(_owner, _target.EditingObject, _owner.Fakes, (o, c) => o.Fakes = new Collection<Owned>(c.ToList()), new[]
            {
                "Name"
            });

            // Assert
            Assert.AreNotEqual(_owner, _target.EditingObject);
            Assert.AreEqual(2, _owner.Fakes.Count);
            Assert.AreEqual(2, _target.EditingObject.Fakes.Count);
            CollectionAssert.AreNotEquivalent(_owner.Fakes, _target.EditingObject.Fakes);
            Assert.AreEqual(_owner.Fakes[0].Name, _target.EditingObject.Fakes[0].Name);
            Assert.AreEqual(_owner.Fakes[1].Name, _target.EditingObject.Fakes[1].Name);
            Assert.AreNotEqual(_owner.Fakes[0].Id, _target.EditingObject.Fakes[0].Id);
            Assert.AreNotEqual(_owner.Fakes[1].Id, _target.EditingObject.Fakes[1].Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeOwnedCollection_GetsNullOriginalObject_ThrowsException()
        {
            _target.MergeOwnedCollection(null, o => o.Fakes, _comparer, o => true, (o, on) => { }, new string[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeOwnedCollection_GetsNullCollectionGetterDelegate_ThrowsException()
        {
            _target.MergeOwnedCollection(_owner, null, _comparer, o => true, (o, on) => { }, new string[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeOwnedCollection_GetsNullEqualityComparer_ThrowsException()
        {
            _target.MergeOwnedCollection(_owner, o => o.Fakes, null, o => true, (o, on) => { }, new string[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeOwnedCollection_GetsNullExtendedStrategy_ThrowsException()
        {
            EditionOverCopyStrategyExtensions.MergeOwnedCollection(null, _owner, null, _comparer, o => true, (o, on) => { }, new string[0]);
        }

        /// <summary>
        ///     Checks that the edition over copy strategy extensions allows no null new entities checker delegate.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeOwnedCollection_GetsNullIsNewDelegate_ThrowsException()
        {
            _target.MergeOwnedCollection(_owner, o => o.Fakes, _comparer, null, (o, on) => { }, new string[0]);
        }

        /// <summary>
        ///     Checks that the edition over copy strategy extensions allows no null owner reference setter delegate.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeOwnedCollection_GetsNullOwnerSetterDelegate_ThrowsException()
        {
            _target.MergeOwnedCollection(_owner, o => o.Fakes, _comparer, o => true, null, new string[0]);
        }

        /// <summary>
        ///     Checks that the edition over copy strategy extensions allows no null properties names array.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MergeOwnedCollection_GetsNullPropertiesNamesEnumerable_ThrowsException()
        {
            _target.MergeOwnedCollection(_owner, o => o.Fakes, _comparer, o => true, (o, on) => { }, null);
        }

        /// <summary>
        ///     Checks that the edition over copy strategy extensions allows no properties names array with names of
        ///     unexisting properties in the owned entity's type.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MergeOwnedCollection_GetsPropertiesNamesEnumerableWithIncorrectPropertiesNames_ThrowsException()
        {
            _target.MergeOwnedCollection(_owner, o => o.Fakes, _comparer, o => true, (o, on) => { }, new[] { "A", "Name" });
        }

        /// <summary>
        ///     Checks that the edition over copy strategy extensions allows no properties names array with null
        ///     strings.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MergeOwnedCollection_GetsPropertiesNamesEnumerableWithNullStrings_ThrowsException()
        {
            _target.MergeOwnedCollection(_owner, o => o.Fakes, _comparer, o => true, (o, on) => { }, new[] { null, "Name" });
        }

        /// <summary>
        ///     Checks that the edition over copy strategy, in case of editing an object containing a collection of
        ///     entities of another type, when ended the edition of the object, the changes made to the copy of the
        ///     editing object (specially to the collection of the entity) are passed back to the original object.
        /// </summary>
        [TestMethod]
        public void MergeOwnedCollection_MergesCollectionOfOwnedEntities_Correctly()
        {
            #region Arrrange

            /* Arrange, create a strategy, create a copy of it with a faked collection of owned in the owner entities,
             * and put that in the strategy editing object */
            var copy = new Owner
            {
                Fakes = new Collection<Owned>
                {
                    new Owned
                    {
                        Id = 1,
                        Name = "Owned1",
                    },
                    new Owned
                    {
                        Id = 2,
                        Name = "Owned2",
                    }
                }
            };
            _targetInternals.SetProperty("EditingObject", copy);

            #endregion

            #region Act

            // Add a new owned entity
            var owned3 = new Owned
            {
                Name = "Owned3"
            };
            _target.EditingObject.Fakes.Add(owned3);

            // Remove an existing one
            _target.EditingObject.Fakes.RemoveAt(0);

            // Update another existing one
            _target.EditingObject.Fakes[0].Name = "Owned21";

            // End the edition
            _target.MergeOwnedCollection(_owner, o => o.Fakes, _comparer, o => o.Id == 0, (owned, o) => owned.Owner = o, new[] { "Id", "Name" });

            #endregion

            // Assert
            CollectionAssert.AreEquivalent(new[] { 2, 0 }, _owner.Fakes.Select(o => o.Id).ToArray());
            Assert.AreNotEqual(_owned1, _owner.Fakes[0]);
            Assert.AreNotEqual(_owned2, _owner.Fakes[1]);
            Assert.IsTrue(_comparer.Equals(_owned2, _owner.Fakes[0]));
            Assert.IsTrue(_comparer.Equals(owned3, _owner.Fakes[1]));
            Assert.AreEqual(_owned2.Name, _owner.Fakes[0].Name);
            Assert.AreEqual(owned3.Name, _owner.Fakes[1].Name);
        }
    }
}