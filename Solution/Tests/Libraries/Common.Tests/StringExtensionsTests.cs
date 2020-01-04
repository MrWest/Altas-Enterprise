using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Common.Tests
{
    /// <summary>
    ///     Collection of tests to check the implementaion of <see cref="System.StringExtensions" /> type.
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class StringExtensionsTests
    {
        /// <summary>
        ///     Checks that when provided a null argument, it string representation in the result is an empty string.
        /// </summary>
        [TestMethod]
        public void ConvertsNullsByEmptyStrings()
        {
            const string template = "{0}A";
            Assert.AreEqual("A", template.EasyFormat(new object[] { null }));
        }

        /// <summary>
        ///     Checks whether the string extensions has one to format using the invariant culture info format provider.
        /// </summary>
        [TestMethod]
        public void FormatsStringsUsingInvariantCultureInfo()
        {
            const string template = "Name: {0}, Last Name: {1}";
            const string name = "Name1";
            const string lastName = "Last Name2";

            string formattedString = template.EasyFormat(name, lastName);

            Assert.AreEqual(string.Format(template, name, lastName), formattedString);
        }

        /// <summary>
        ///     Checks whether the formatting method does not allow null strings.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringFormattingAcceptsNoNulls()
        {
            StringExtensions.EasyFormat(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringFormattingAcceptsNoNulls2()
        {
            "".EasyFormat(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void StringFormattingAcceptsNoNulls3()
        {
            "".EasyFormat(CultureInfo.InvariantCulture, null);
        }

        /// <summary>
        ///     Capitalizes strings.
        /// </summary>
        [TestMethod]
        public void CapitalizesString()
        {
            Assert.AreEqual("Hello", "hello".Capitalize());
            Assert.AreEqual("Hello", "Hello".Capitalize());
            Assert.AreEqual("Hello test", "hello test".Capitalize());
            Assert.AreEqual("Hello test", "Hello test".Capitalize());
        }

        /// <summary>
        ///     Strings not starting with letters cannot be capitalized.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotCapitalizeStringNotStartingWithLetters()
        {
            "1iop".Capitalize();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotCapitalizeStringNotStartingWithLetters2()
        {
            "/iop".Capitalize();
        }

        /// <summary>
        ///     Must throw if commanded to capitalize null or empty string.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotCapitalizeNullOrEmptyString()
        {
            StringExtensions.Capitalize(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotCapitalizeNullOrEmptyString2()
        {
            "".Capitalize();
        }
    }
}