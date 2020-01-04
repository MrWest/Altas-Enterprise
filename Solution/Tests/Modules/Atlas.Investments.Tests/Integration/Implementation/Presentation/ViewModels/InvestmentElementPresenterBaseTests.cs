using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Implementation.Domain.Validation.EnterpriseLibrary;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.Integration.Implementation.Presentation.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentElementPresenterBaseTests :
        PresenterTestsBase<IInvestmentElement, UnitTests.Implementation.Presentation.ViewModels.InvestmentElementPresenterBaseTests.InvestmentElementPresenterStub, IItemManagerApplicationServices<IInvestmentElement>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        ///     Creates the actual instance of the validator factory, so validators can be extracted from it.
        /// </summary>
        protected override void CreateValidatorFactory()
        {
            ValidatorFactory = new EntLibValidatorFactory();
        }


        [TestMethod]
        public void Name_GivenNull_Invalid()
        {
            // Arrange
            TestObject.Name = null;

            // Act
            var errors = (IEnumerable<string>)TestObjectInternals.Invoke("Validate");

            // Assert
            CollectionAssert.Contains(errors.ToArray(), Resources.InvestmentElementMustHaveAName);
        }

        [TestMethod]
        public void Name_GivenEmpty_Invalid()
        {
            // Arrange
            TestObject.Name = string.Empty;

            // Act
            var errors = (IEnumerable<string>)TestObjectInternals.Invoke("Validate");

            // Assert
            CollectionAssert.Contains(errors.ToArray(), Resources.InvestmentElementMustHaveAName);
        }

        [TestMethod]
        public void Name_GivenStringWithOneOrMoreCharacters_Valid()
        {
            // Arrange
            TestObject.Name = "Name";

            // Act
            var errors = (IEnumerable<string>)TestObjectInternals.Invoke("Validate");

            // Assert
            Assert.IsFalse(errors.Any());
        }
    }
}