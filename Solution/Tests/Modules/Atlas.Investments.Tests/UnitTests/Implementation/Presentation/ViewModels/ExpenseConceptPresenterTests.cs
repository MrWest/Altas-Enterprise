using System;
using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class ExpenseConceptPresenterTests : PresenterTestsBase<IExpenseConcept, ExpenseConceptPresenter, IExpenseConceptManagerApplicationServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Name_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Name, () => "A");
        }

        [TestMethod]
        public void Name_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Name, () => "A", () => "B");
        }

        [TestMethod]
        public void Name_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Name, () => "A", false);
        }


        [TestMethod]
        public void Code_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Code, () => "A");
        }

        [TestMethod]
        public void Code_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Code, () => "A", () => "B");
        }

        [TestMethod]
        public void Code_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Code, () => "A");
        }


        [TestMethod]
        public void Type_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Type, () => ExpenseConceptType.Direct);
        }

        [TestMethod]
        public void Type_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Type, () => ExpenseConceptType.Direct, () => ExpenseConceptType.Indirect);
        }

        [TestMethod]
        public void Type_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Type, () => ExpenseConceptType.Direct);
        }


        [TestMethod]
        public void Types_ReturnsValuesOfTheExpenseConceptEnum()
        {
            CollectionAssert.AreEquivalent(Enum.GetValues(typeof(ExpenseConceptType)), TestObject.Types);
        }
    }
}