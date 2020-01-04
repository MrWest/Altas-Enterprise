using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// Defines the main interface describing the resourses associeted to another budget component item 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TComponentItem"></typeparam>
    public class BudgetComponentResourceManagerApplicationService<TItem, TComponentItem, TRepository, TDomainServices> :
         ItemManagerApplicationServicesBase<TItem, TRepository, TDomainServices>
        where TItem : class, IBudgetComponentItem
        where TComponentItem :class, IBudgetComponentItem
        where TRepository : IBudgetComponentResourceRepository<TItem, TComponentItem>
        where TDomainServices : IBudgetComponentResourceDomainServices<TItem, TComponentItem>
    {

        private TComponentItem _componentItem;
        /// <summary>
        ///     Gets or sets the budget component to which belong the items which business rules are enforced in the current
        ///     <see cref="IBudgetComponentItemDomainServices{TItem}" />.
        /// </summary>
        TComponentItem BudgetComponentItem
        {
            get
            {
                if (_componentItem == null)
                    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

                return _componentItem;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _componentItem = value;
            }

        }
    }
}
