using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    public interface IUnderGroupResourceDomainService: IBudgetComponentItemDomainServices<IUnderGroupResource>
    {
        IUnderGroup UnderGroup { get; set; }
    }
}