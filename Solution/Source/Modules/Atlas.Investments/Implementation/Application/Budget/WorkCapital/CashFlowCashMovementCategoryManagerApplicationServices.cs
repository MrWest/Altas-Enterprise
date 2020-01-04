using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.WorkCapital
{
    public class CashFlowCashMovementCategoryManagerApplicationServices<TItem> : ItemManagerApplicationServicesBase<TItem, ICashFlowCashMovementCategoryRepository<TItem>, ICashFlowCashMovementCategoryDomainService<TItem>>, ICashFlowCashMovementCategoryManagerApplicationServices<TItem>
     where TItem : class ,ICashMovementCategory
    {
        public IWorkCapitalCashFlow WorkCapitalCashFlow { get; set; }

        /// <summary>
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override ICashFlowCashMovementCategoryRepository<TItem> Repository
        {
            get
            {
                ICashFlowCashMovementCategoryRepository<TItem> repository = base.Repository;
                repository.WorkCapitalCashFlow = WorkCapitalCashFlow;

                return repository;
            }
        }


        /// <summary>
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        protected override ICashFlowCashMovementCategoryDomainService<TItem> DomainServices
        {
            get
            {
                ICashFlowCashMovementCategoryDomainService<TItem> domainServices = base.DomainServices;
                domainServices.WorkCapitalCashFlow = WorkCapitalCashFlow;

                return domainServices;
            }
        }
    }
}
