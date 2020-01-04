using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class BudgetComponentActivityManagerApplicationServices<TItem, TComponent, TRepository, TDomainServices> :
        BudgetComponentItemManagerApplicationServicesBase<TItem, TComponent, TRepository, TDomainServices>
        , IBudgetComponentActivityManagerApplicationServices<TItem,TComponent>
        where TItem : class, IBudgetComponentItem
        where TComponent : class, IBudgetComponent
        where TRepository : IBudgetComponentActivityRepository<TItem, TComponent>
        where TDomainServices : IBudgetComponentActivityDomainServices<TItem,TComponent>
    {

        private TComponent _component;
        /// <summary>
        /// Gets or sets the budget component to which belong the items managed in the current
        /// <see cref="BudgetComponentItemManagerApplicationServicesBase{TItem, TComponent, TRepository, TDomainServices}"/>.
        /// </summary>
        public TComponent Component
        {
            get
            {
                if (_component == null)
                    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

                return _component;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _component = value;
            }
        }

        /// <summary>
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override TRepository Repository
        {
            get
            {
                TRepository repository = base.Repository;
                repository.Component = Component;

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
                domainServices.Component = Component;

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

            return "{0}->{1}".EasyFormat(Component.Id, baseKey);
        }


    }
}
