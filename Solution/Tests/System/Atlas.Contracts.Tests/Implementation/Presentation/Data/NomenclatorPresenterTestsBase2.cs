using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Domain.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Tests.Implementation.Presentation.Data
{
    [TestClass, ExcludeFromCodeCoverage]
    public class NomenclatorPresenterTestsBase :
        PresenterTestsBase<INomenclator, NomenclatorPresenterBase<INomenclator, IItemManagerApplicationServices<INomenclator>>, IItemManagerApplicationServices<INomenclator>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
        }


        [TestMethod]
        public void Name_GivenDifferentValue_SetsValue()
        {
            TestGetsSetValue(x => x.Name, () => "A");
        }

        [TestMethod]
        public void Name_GivenDifferentValue_NotifiesPropertyChanged()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Name, () => "A", () => "B");
        }

        [TestMethod]
        public void Name_GivenSameValue_DoesNotNotifyPropertyChanged()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Name, () => "A");
        }


        [TestMethod]
        public void Description_GivenDifferentValue_SetsValue()
        {
            TestGetsSetValue(x => x.Description, () => "A");
        }

        [TestMethod]
        public void Description_GivenDifferentValue_NotifiesPropertyChanged()
        {
            TestNotifiesPropertyChangesIfSetValueIsDifferent(x => x.Description, () => "A", () => "B");
        }

        [TestMethod]
        public void Description_GivenSameValue_DoesNotNotifyPropertyChanged()
        {
            TestDoesNotNotifPropertyChangesIfSetValueIsSame(x => x.Description, () => "A");
        }
    }
}
