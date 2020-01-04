using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    public class BudgetComponentActivityOfSpecification<TItem> : BudgetComponentItemsOfSpecification<TItem>
        where TItem:class, IActivity
        //where TComponent:class,IBudgetComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemsOfSpecification{TItem}"/> given a budget component.
        /// </summary>
        /// <param name="component">The <see cref="IBudgetComponent"/> to get its items.</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/> is null.</exception>
        public BudgetComponentActivityOfSpecification(ISubSpecialityHolder component)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            Predicate = item => Equals(item.SubSpecialityHolder.Id, component.Id);
        }
    }

    public class BudgetComponentExecutedActivityOfSpecification<TItem> : BudgetComponentItemsOfSpecification<TItem>
        where TItem : class,IExecutedActivity
        //where TComponent : class,IBudgetComponent
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemsOfSpecification{TItem}"/> given a budget component.
        /// </summary>
        /// <param name="component">The <see cref="IBudgetComponent"/> to get its items.</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/> is null.</exception>
        public BudgetComponentExecutedActivityOfSpecification(ISubSpecialityHolder component)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            Predicate = item => Equals(item.SubSpecialityHolder.Id, component.Id);
        }
    }
    public class SubSpecialityActivityOfSpecification<TActivity> : BudgetComponentItemsOfSpecification<TActivity>
        where TActivity : class, IActivity
    {
        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemsOfSpecification{TItem}"/> given a budget component.
        /// </summary>
        /// <param name="component">The <see cref="IBudgetComponent"/> to get its items.</param>
        /// <exception cref="ArgumentNullException"><paramref name="component"/> is null.</exception>
        public SubSpecialityActivityOfSpecification(ISubSpecialityHolder component)
        {
            if (component == null)
                throw new ArgumentNullException("component");

            Predicate = item => Equals(item.SubSpecialityHolder.Id, component.Id);
        }
    }

    public class ActivityBaseOfQueryable<TActivity> : EntityFrameworkQueryable<TActivity>
       where TActivity : Activity
    {

        public ActivityBaseOfQueryable(ISubSpecialityHolder component, IEntityFrameworkDbContext<TActivity> context) : base(context)
        {

            if (component == null)
                throw new ArgumentNullException("component");
            Query = (from e in context.Entities orderby e.Id ascending where e.SubSpecialityHolderId == component.Id select e);
            Parameter = component.Id;
        }
    }
}
