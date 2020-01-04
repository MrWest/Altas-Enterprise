using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    /// <summary>
    ///     Collection of tests to check the implementation of <see cref="System.TypeExtensions" />.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TypeExtensionsTests
    {
        class Cloneable : ICloneable
        {
            public object Clone()
            {
                return null;
            }
        }


        /// <summary>
        ///     Checks whether it accepts no nulls when calling Implements methods.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AcceptsNoNullsInImplementsMethods()
        {
            TypeExtensions.Implements(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AcceptsNoNullsInImplementsMethods2()
        {
            typeof(int).Implements(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AcceptsNoNullsInImplementsMethods3()
        {
            TypeExtensions.Implements(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AcceptsNoNullsInImplementsMethods4()
        {
            TypeExtensions.Implements<int>(null);
        }

        /// <summary>
        ///     Checks whether it accepts no nulls when calling IsCloneable methods.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AcceptsNoNullsInIsCloneableMethods()
        {
            TypeExtensions.IsCloneable(null);
        }

        /// <summary>
        ///     Must determine whether a type implements an interface.
        /// </summary>
        [TestMethod]
        public void DeterminesWhetherATypeImplementsAnInterface()
        {
            Assert.IsTrue(typeof(List<int>).Implements(typeof(IEnumerable)));
            Assert.IsTrue(typeof(List<>).Implements(typeof(IEnumerable<>)));
            Assert.IsFalse(typeof(List<>).Implements(typeof(IServiceProvider)));
            Assert.IsFalse(typeof(List<int>).Implements(typeof(IServiceProvider)));
            Assert.IsTrue(typeof(List<int>).Implements<IEnumerable>());
            Assert.IsFalse(typeof(List<int>).Implements<IServiceProvider>());
        }

        /// <summary>
        ///     Must determine whether a type is cloneable.
        /// </summary>
        [TestMethod]
        public void DeterminesWhetherATypeIsCloneable()
        {
            Assert.IsTrue(typeof(Cloneable).IsCloneable());
            Assert.IsFalse(typeof(object).IsCloneable());
        }
    }
}