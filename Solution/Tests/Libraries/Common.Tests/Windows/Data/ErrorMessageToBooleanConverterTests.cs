using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.Windows.Data
{
    /// <summary>
    /// Collection of tests to check the implementation of
    /// <see cref="System.Windows.Data.ErrorMessageToBooleanConverter"/> type.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ErrorMessageToBooleanConverterTests
    {
        /// <summary>
        /// Must be able to determine whether an Entity has invalid data in its properties.
        /// </summary>
        [TestMethod]
        public void GetWhetherAnEntityHasInvalidData()
        {
            var target = new ErrorMessageToBooleanConverter();

            // True for null objects
            Assert.IsTrue((bool)target.Convert(null, null, null, null));

            // False for non-null objects (specially strings) for none IDataErrorInfo objects
            Assert.IsTrue((bool)target.Convert(new object(), null, null, null));
            Assert.IsFalse((bool)target.Convert(string.Empty, null, null, null));

            // True for IDataErrorInfo objects with null Error property
            Assert.IsFalse((bool)target.Convert("A", null, null, null));
        }

        /// <summary>
        /// Does not supports back convertions.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void DoesNotSupportBackConversion()
        {
            var target = new ErrorMessageToBooleanConverter();
            target.ConvertBack(null, null, null, null);
        }
    }
}