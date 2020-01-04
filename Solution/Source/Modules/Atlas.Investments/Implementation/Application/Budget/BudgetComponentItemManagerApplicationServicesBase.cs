using System;
using System.Collections.Generic;
using System.Reflection;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// Implementation of the base contract <see cref="IBudgetComponentItemManagerApplicationServices{TItem, TComponent}"/> representing
    /// the application services to handle the coming CRUD-operation request from upper layers regarding to budget component items.
    /// </summary>
    /// <typeparam name="TItem">The type of budget component item to manage.</typeparam>
    /// <typeparam name="TComponent">The type of the budget component to which belong the items.</typeparam>
    /// <typeparam name="TRepository">The type of the repository required to make the data oeprations.</typeparam>
    /// <typeparam name="TDomainServices">The type of the domain services used to ensure the business rules for the items.</typeparam>
    public class BudgetComponentItemManagerApplicationServicesBase<TItem, TComponent, TRepository, TDomainServices> :
        ItemManagerApplicationServicesBase<TItem, TRepository, TDomainServices>,
        IBudgetComponentItemManagerApplicationServices<TItem>
        where TItem : class, IBudgetComponentItem
        where TComponent : class, IBudgetComponent
        where TRepository : IBudgetComponentItemRepository<TItem>
        where TDomainServices : IBudgetComponentItemDomainServices<TItem>
    {
        


      
        /// <summary>
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override TRepository Repository
        {
            get
            {
                TRepository repository = base.Repository;
                //repository.Component = Component;

                return repository;
            }
        }

        /// <summary>
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        protected override TDomainServices DomainServices
        {
            get
            {
                TDomainServices domainServices = base.DomainServices;
             //   domainServices.Component = Component;

                return domainServices;
            }
        }


        /// <summary>
        /// Gets the key to cache the result for the given method, using it and its arguments.
        /// </summary>
        /// <param name="method">The <see cref="MethodBase"/> to generate a key for.</param>
        /// <param name="arguments">The arguments currently being passed to the method.</param>
        /// <returns>The key for the method's result.</returns>
        public override string GetKeyFor(MethodBase method, params object[] arguments)
        {
            string baseKey = base.GetKeyFor(method, arguments);

            return "{0}".EasyFormat(baseKey);
        }

        /// <summary>
        /// Determines whether there can be narrowed the set of the budget component items by leaving only the ones with the given
        /// specification in their names.
        /// </summary>
        /// <param name="nameSpecification">
        /// A <see cref="string"/> being the criteria that must match the name of the budget component items in order to be
        /// returned.
        /// </param>
        /// <returns>True.</returns>
        [CachesResult]
        public bool CanFilter(string nameSpecification)
        {
            return true;
        }

        /// <summary>
        /// Gets the budget component items which names match the given specification.
        /// </summary>
        /// <param name="nameSpecification">The criteria to be matched by the budget component items in order to be returned.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> matching <paramref name="nameSpecification"/>.</returns>
        [CachesResult]
        public IEnumerable<TItem> Filter(string nameSpecification)
        {
            return Repository.FilterByName(nameSpecification);
        }
    }
}
