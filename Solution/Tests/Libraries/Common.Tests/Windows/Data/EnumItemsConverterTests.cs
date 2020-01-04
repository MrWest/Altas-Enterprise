using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests.Windows.Data
{
    /// <summary>
    /// Collection of tests to check the implementation of the <see cref="EnumerationValuesConverter"/>
    /// type.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EnumItemsConverterTests
    {
        enum FakeEnum
        {
            A,
            B,
            C,
            D
        }


        /// <summary>
        /// Checks whether the tested class indeed from an enum gets the values of it.
        /// </summary>
        [TestMethod]
        public void GetsEnumValues()
        {
            var target = new EnumerationValuesConverter();

            const FakeEnum enumeration = default(FakeEnum);
            Array expected = Enum.GetValues(typeof(FakeEnum));
            var actualArray = (Array)target.Convert(enumeration, null, null, null);
            CollectionAssert.AreEquivalent(expected, actualArray);
        }

        /// <summary>
        /// Must throw if attempted to convert the selected value back to the source.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void ThrowsWhenConvertingBack()
        {
            var target = new EnumerationValuesConverter();
            target.ConvertBack(null, null, null, null);
        }
    }
}