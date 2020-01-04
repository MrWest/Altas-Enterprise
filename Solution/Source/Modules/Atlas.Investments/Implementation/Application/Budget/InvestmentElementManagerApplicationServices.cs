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
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// Default implementation of <see cref="IInvestmentElementManagerApplicationServices"/> contract, representing the contract 
    /// for the application services allowing to carry on with CRUD calls coming from upper layers (like presentation).
    /// </summary>
    public class InvestmentElementManagerApplicationServices :
        ItemManagerApplicationServicesBase<IInvestmentElement, IInvestmentElementRepository, IInvestmentElementDomainServices>,
        IInvestmentElementManagerApplicationServices
    {
        /// <summary>
        /// Gets or sets the <see cref="IInvestmentElement"/> being the parent of those handled in the current
        /// <see cref="IInvestmentElementManagerApplicationServices"/>.
        /// </summary>
        public IInvestmentElement Parent
        {
            [SkipUnitOfWork]
            get;
            [SkipUnitOfWork]
            set;
        }

      
        /// <summary>
        /// Gets the instance of the repository (of type <see cref="IInvestmentElementRepository"/>) this class uses
        /// to carry on with the data operations implying the investment elements the current
        /// <see cref="InvestmentElementManagerApplicationServices"/> handles.
        /// </summary>
        protected override IInvestmentElementRepository Repository
        {
            [SkipUnitOfWork]
            get
            {
                IInvestmentElementRepository repository = base.Repository;
                repository.Parent = Parent;
               

                return repository;
            }
        }

        /// <summary>
        /// Gets the instance of the domain services (of type <see cref="IInvestmentElementDomainServices"/>) this class uses
        /// to carry on with the data operations implying the investment elements the current
        /// <see cref="InvestmentElementManagerApplicationServices"/> handles.
        /// </summary>
        protected override IInvestmentElementDomainServices DomainServices
        {
            [SkipUnitOfWork]
            get
            {
                IInvestmentElementDomainServices domainServices = base.DomainServices;
                domainServices.Parent = Parent;

                return domainServices;
            }
        }

        /// <summary>
        /// This method allows to obtain the key for the possible cached result of a method.
        /// </summary>
        /// <param name="method">The method being call which result will be cached.</param>
        /// <param name="arguments">All the arguments passed to the method.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="method" /> os <paramref name="arguments" /> is null.
        /// </exception>
        /// <returns>
        /// A string representing the key for the result to cache, which is formed using all the given data.
        /// </returns>
        [SkipUnitOfWork]
        public override string GetKeyFor(MethodBase method, params object[] arguments)
        {
            var baseKey = base.GetKeyFor(method, arguments);

            return "{0}->{1}".EasyFormat(Parent != null ? Parent.Id : string.Empty, baseKey);
        }
    }
}
