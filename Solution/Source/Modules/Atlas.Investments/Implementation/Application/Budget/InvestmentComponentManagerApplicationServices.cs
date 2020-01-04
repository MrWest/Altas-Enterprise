using System;
using System.Reflection;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentComponentManagerApplicationServices" /> representing the
    ///     application services provider used to respond to the CRUD requests coming from upper layers regarding to the domain
    ///     entities: investment components.
    /// </summary>
    public class InvestmentComponentManagerApplicationServices :
        ItemManagerApplicationServicesBase<IInvestmentComponent, IInvestmentComponentRepository, IInvestmentComponentDomainServices>,
        IInvestmentComponentManagerApplicationServices
    {
        /// <summary>
        ///     Gets an instance of the repository of type <see cref="IInvestmentComponentRepository" />.
        /// </summary>
        protected override IInvestmentComponentRepository Repository
        {
            get
            {
                IInvestmentComponentRepository repository = base.Repository;
                repository.InvestmentElement = InvestmentElement;

                return repository;
            }
        }

        /// <summary>
        ///     Gets or sets the parent investment element of those managed in the current application services provider.
        /// </summary>
        public IInvestmentElement InvestmentElement { get; set; }


        /// <summary>
        ///     This method allows to obtain the key for the possible cached result of a method.
        /// </summary>
        /// <param name="method">The method being call which result will be cached.</param>
        /// <param name="arguments">All the arguments passed to the method.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="method" /> os <paramref name="arguments" /> is null.
        /// </exception>
        /// <returns>
        ///     A string representing the key for the result to cache, which is formed using all the given data.
        /// </returns>
        public override string GetKeyFor(MethodBase method, params object[] arguments)
        {
            string baseKey = base.GetKeyFor(method, arguments);

            return "{0}->{1}".EasyFormat(InvestmentElement.Id, baseKey);
        }
    }
}