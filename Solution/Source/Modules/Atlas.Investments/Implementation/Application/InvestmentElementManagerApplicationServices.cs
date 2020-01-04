using System;
using System.Linq;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// Default implementation of <see cref="IInvestmentElementManagerApplicationServices"/> contract, representing the contract 
    /// for the application services allowing to carry on with CRUD calls coming from upper layers (like presentation).
    /// </summary>
    public abstract class InvestmentElementManagerApplicationServices<TElement,TRepository,TDomain> :
        ItemManagerApplicationServicesBase<TElement, TRepository, TDomain>,
        IInvestmentElementManagerApplicationServices<TElement>
        where TElement:class,IInvestmentElement
        where TRepository:class ,IRepository<TElement>
        where TDomain:class ,IDomainServices<TElement>
    {
        public virtual TElement ExportRelated(IDatabaseContext exportDatabaseContext, TElement item)
        {
            return item;
        }

        public  int InDeep(IInvestmentElement investmentElement)
        {
            int count = 1;

            var investmentRepo = ServiceLocator.Current.GetInstance<IInvestmentComponentRepository>();
            investmentRepo.InvestmentElement = investmentElement;

            foreach (IInvestmentComponent investmentComponent in investmentRepo.Entities)
            {
                var investmentService = ServiceLocator.Current.GetInstance<IInvestmentComponentManagerApplicationServices>();
                investmentService.InvestmentElement = investmentComponent;
                count += investmentService.InDeep(investmentComponent);
            }
            return count;

        }

    }
}
