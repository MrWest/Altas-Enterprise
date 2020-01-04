using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public abstract class UnderGroupItemManagerApplicationServices<TItem,TRepository,TDomain>: BudgetComponentItemManagerApplicationServicesBase<TItem,TRepository,TDomain>, IUnderGroupItemManagerApplicationServices<TItem>
        where TItem : class, IUnderGroupItem
        where TRepository : class, IUnderGroupItemRepository<TItem>
        where TDomain : class, IBudgetComponentItemDomainServices<TItem>

    {
        public IUnderGroup UnderGroup { get; set; }

        protected override TRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.UnderGroup = UnderGroup;
                return repo;
            }
        }


    }
}