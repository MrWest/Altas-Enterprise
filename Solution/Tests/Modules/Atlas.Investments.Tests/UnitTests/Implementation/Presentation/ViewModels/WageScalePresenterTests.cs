using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class WageScalePresenterTests : PresenterTestsBase<IWageScale, WageScalePresenter, IWageScaleManagerApplicationServices>
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
        public void Retribution_GivenSameValue_DoesNotNotifyPropertyChanges()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Retribution, () => 0.2m);
        }

        [TestMethod]
        public void Retribution_GivenDifferentValue_NotifiesPropertyChanges()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Retribution, () => 0.6m, () => 5.3m);
        }

        [TestMethod]
        public void Retribution_GivenDifferentValue_GetsSetValue()
        {
            TestGetsSetValue(x => x.Retribution, () => 5m, false);
        }
    }
}