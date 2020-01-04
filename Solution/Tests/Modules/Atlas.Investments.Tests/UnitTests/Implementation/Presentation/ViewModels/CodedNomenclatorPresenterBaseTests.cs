using System.Diagnostics.CodeAnalysis;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CompanyName.Atlas.Investments.Tests.UnitTests.Implementation.Presentation.ViewModels
{
    [TestClass, ExcludeFromCodeCoverage]
    public class CodedNomenclatorPresenterBaseTests :
        PresenterTestsBase<ICodedNomenclator, CodedNomenclatorPresenterBaseTests.CodedNomenclatorBaseStub, IItemManagerApplicationServices<ICodedNomenclator>>
    {
        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
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


        public class CodedNomenclatorBaseStub : CodedNomenclatorPresenterBase<ICodedNomenclator, IItemManagerApplicationServices<ICodedNomenclator>>
        {
            public CodedNomenclatorBaseStub(ICodedNomenclator nomenclator)
                : base(nomenclator)
            {
            }
        }
    }
}