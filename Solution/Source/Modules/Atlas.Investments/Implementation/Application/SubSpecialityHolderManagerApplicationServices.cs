using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public abstract class SubSpecialityHolderManagerApplicationServices<THolder,TRepository,TDomain>: ItemManagerApplicationServicesBase<THolder, TRepository, TDomain>, ISubSpecialityHolderManagerApplicationServices<THolder>
        where THolder:class,ISubSpecialityHolder
        where TDomain:class , ISubSpecialityHolderDomainServices<THolder>
        where TRepository:class ,ISubSpecialityHolderRepository<THolder>

    {
        public IBudgetComponent BudgetComponent { get; set; }

        protected override TRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.BudgetComponent = BudgetComponent;
                return repo;
            }
        }

        protected override TDomain DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.BudgetComponent = BudgetComponent;
                return domain;
            }
        }
    }
}