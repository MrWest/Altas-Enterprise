using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Domain.Validation;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Tests.Domain.Validation
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ValidationsTests : TestBase
    {
        private Mock<IRepository<INomenclator>> _nomenclatorRepositoryMock;
        private IRepository<INomenclator> _nomenclatorRepository;


        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();

            _nomenclatorRepositoryMock = new Mock<IRepository<INomenclator>>();
            _nomenclatorRepository = _nomenclatorRepositoryMock.Object;
            ServiceLocatorMock.Setup(x => x.GetInstance<IRepository<INomenclator>>()).Returns(_nomenclatorRepository);
        }


        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void HasUniqueName_GivenNullNomenclator_Throws()
        {
            Validations.HasUniqueName<INomenclator, IRepository<INomenclator>>(null);
        }

        [TestMethod]
        public void HasUniqueName_ThowingInvalidOperationException_ReturnsFalse()
        {
            // Arrange
            _nomenclatorRepositoryMock.Setup(x => x.Find(It.IsAny<ISpecification<INomenclator>>())).Throws<InvalidOperationException>();
            var nomenclator = Mock.Of<INomenclator>(x => x.Name == "N");

            // Act
            bool isValid = nomenclator.HasUniqueName<INomenclator, IRepository<INomenclator>>();

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void HasUniqueName_GivenNewNomenclatorAndNameBelongsToNoOtherNomenclator_ReturnsTrue()
        {
            // Arrange
            var nomenclator = Mock.Of<INomenclator>(x => x.Name == "N");

            _nomenclatorRepositoryMock.Setup(x => x.Find(It.IsAny<ISpecification<INomenclator>>())).Returns((INomenclator)null);

            // Act
            bool isValid = nomenclator.HasUniqueName<INomenclator, IRepository<INomenclator>>();

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void HasUniqueName_GivenNomenclatorWhichNameBelongsToOtherNomenclator_ReturnsFalse()
        {
            // Arrange
            var nomenclator = Mock.Of<INomenclator>(x => x.Name == "N");
            var otherNomenclator = Mock.Of<INomenclator>(x => x.Name == "N");
            _nomenclatorRepositoryMock.Setup(x => x.Find(It.IsAny<ISpecification<INomenclator>>())).Returns(otherNomenclator);

            // Act
            bool isValid = nomenclator.HasUniqueName<INomenclator, IRepository<INomenclator>>();

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void HasUniqueName_GivenNullSpecification_Throws()
        {
            // Arrange
            var nomenclator = Mock.Of<INomenclator>(x => x.Name == "N");

            // Act
            nomenclator.HasUniqueName<INomenclator, IRepository<INomenclator>>(null);
        }

        [TestMethod]
        public void HasUniqueName_GivenAnotherSpecificationToBeAddedToTheDefault_ItGetsAddedInOrderToNarrowTheSearchOfDuplicatedNames()
        {
            // Arrange
            var nomenclator = Mock.Of<INomenclator>(x => x.Name == "N");
            Expression<Predicate<INomenclator>> predicate = x => true;
            var specification = Mock.Of<ISpecification<INomenclator>>(x => x.Predicate == predicate);
            _nomenclatorRepositoryMock.Setup(x => x.Find(It.IsAny<ISpecification<INomenclator>>())).Returns((INomenclator)null);

            // Act
            bool isValid = nomenclator.HasUniqueName<INomenclator, IRepository<INomenclator>>(specification);

            // Assert
            Assert.IsTrue(isValid);
        }
    }
}
