using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class ResourceSupplierPresenter:CodedNomenclatorPresenterBase<IResourceSupplier, IResourceSupplierManagerApplicationServices>, IResourceSupplierPresenter
    {
        public ResourceSupplierPresenter(IResourceSupplier nomenclator) : base(nomenclator)
        {
        }
    }
}