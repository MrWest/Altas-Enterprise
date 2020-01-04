using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class ResourceProviderPresenter:CodedNomenclatorPresenterBase<IResourceProvider, IResourceProviderManagerApplicationServices>, IResourceProviderPresenter
    {
        public ResourceProviderPresenter(IResourceProvider nomenclator) : base(nomenclator)
        {
        }
    }
}