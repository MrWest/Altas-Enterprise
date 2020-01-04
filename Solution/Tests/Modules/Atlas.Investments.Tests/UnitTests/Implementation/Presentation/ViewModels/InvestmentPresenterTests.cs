using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class InvestmentPresenterTests :
        PresenterTestsBase<IInvestment, InvestmentPresenter, IInvestmentManagerApplicationServices>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void RelatedPrograms_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.RelatedPrograms, () => "A");
        }

        [TestMethod]
        public void RelatedPrograms_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.RelatedPrograms, () => "A", () => "B");
        }

        [TestMethod]
        public void RelatedPrograms_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.RelatedPrograms, () => "A");
        }


        [TestMethod]
        public void AuthorOrEmitter_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.AuthorOrEmitter, () => "A");
        }

        [TestMethod]
        public void AuthorOrEmitter_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.AuthorOrEmitter, () => "A", () => "B");
        }

        [TestMethod]
        public void AuthorOrEmitter_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.AuthorOrEmitter, () => "A");
        }


        [TestMethod]
        public void Entity_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Entity, () => "A");
        }

        [TestMethod]
        public void Entity_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Entity, () => "A", () => "B");
        }

        [TestMethod]
        public void Entity_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Entity, () => "A");
        }


        [TestMethod]
        public void Capactity_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Capacity, () => "A");
        }

        [TestMethod]
        public void Capactity_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Capacity, () => "A", () => "B");
        }

        [TestMethod]
        public void Capactity_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Capacity, () => "A");
        }


        [TestMethod]
        public void InducedDoings_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.InducedDoings, () => "A");
        }

        [TestMethod]
        public void InducedDoings_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.InducedDoings, () => "A", () => "B");
        }

        [TestMethod]
        public void InducedDoings_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.InducedDoings, () => "A");
        }


        [TestMethod]
        public void Depth_Returns0()
        {
            Assert.AreEqual(0, TestObject.Depth);
        }


        [TestMethod]
        public void Parent_ReturnsNull()
        {
            Assert.IsNull(TestObject.Parent);
        }
    }
}