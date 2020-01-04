using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.Views.Converters;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Presentation.Views.Converters
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExpenseConceptTypeToStringConverterTests : TestBase<ExpenseConceptTypeToStringConverter>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Convert_GivenExpenseConceptType_ConvertsItToString()
        {
            Assert.AreEqual(Resources.Direct, TestObject.Convert(ExpenseConceptType.Direct, null, null, null));
            Assert.AreEqual(Resources.Indirect, TestObject.Convert(ExpenseConceptType.Indirect, null, null, null));
        }

        [TestMethod]
        public void ConvertBack_GivenExpenseConceptTypeString_ConvertsItToExpenseConceptType()
        {
            Assert.AreEqual(ExpenseConceptType.Direct, TestObject.ConvertBack(Resources.Direct, null, null, null));
            Assert.AreEqual(ExpenseConceptType.Indirect, TestObject.ConvertBack(Resources.Indirect, null, null, null));
        }

        [TestMethod]
        public void Convert_ConvertsGivenExpenseConceptTypes_ReturnsStringArrayWithTheirConvertions()
        {
            // Arrange
            string[] expected = { Resources.Direct, Resources.Indirect };
            ExpenseConceptType[] types = { ExpenseConceptType.Direct, ExpenseConceptType.Indirect };

            // Act
            var actual = (IEnumerable<string>)TestObject.Convert(types, null, null, null);

            // Assert
            CollectionAssert.AreEquivalent(expected, actual.ToArray());
        }

        [TestMethod, ExpectedException(typeof(NotSupportedException))]
        public void Convert_ConvertsGivenExpenseConceptTypeStrings_Throws()
        {
            // Arrange
            string[] types = { Resources.Direct, Resources.Indirect };

            // Act
            TestObject.ConvertBack(types, null, null, null);
        }
    }
}