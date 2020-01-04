using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    ///     Implementation of the base contract of the
    ///     <see cref="IExecutedActivityDomainServices{TComponent}" />, representing
    ///     the base class of the domain services used to ensure the business rules for the executed items of a budget
    ///     component.
    /// </summary>
    /// <typeparam name="TItem">The type of the executed budget component items which domain rules and enforced here.</typeparam>
    /// <typeparam name="TComponent">The executed budget component to which belong the items.</typeparam>
    public abstract class ActivityDomainServicesBase<TActivity> :
        BudgetComponentItemDomainServicesBase<TActivity>,
        IActivityDomainServices<TActivity>
        where TActivity : class, IActivity
        //where TComponent : class, IBudgetComponent
    {
        //private TComponent _component;

        //public TComponent Component
        //{
        //    get
        //    {
        //        if (_component == null)
        //            throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

        //        return _component;
        //    }
        //    set
        //    {
        //        if (value == null)
        //            throw new ArgumentNullException("value");

        //        _component = value;
        //    }
        //}

        /// <summary>
            ///     Determines whether there can be executed any of the given planned items.
            /// </summary>
            /// <typeparam name="TPlanned">The type of the planned items to determine whether there can executed or not.</typeparam>
            /// <param name="plannedItems">
            ///     A <see cref="IEnumerable{T}" /> of planned items to determine whether there can be executed at least one of them.
            /// </param>
            /// <exception cref="ArgumentNullException"><paramref name="plannedItems"/> is null.</exception>
            /// <returns>True if there is at least one planned item unexecuted; false otherwise.</returns>
           public  bool CanExecute(IEnumerable<IPlannedActivity> plannedItems) 
            {
                if (plannedItems == null)
                    throw new ArgumentNullException("plannedItems");

                return plannedItems.Any(x => x.Execution == null);
            }

        /// <summary>
        ///     Executes all the planned items still not executed.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to execute.</typeparam>
        /// <param name="plannedItems">
        ///     The <see cref="IEnumerable{T}" /> of planned items to executed the unexecuted ones among them.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="plannedItems"/> is null.</exception>
        /// <returns>An <see cref="int" /> representing how many planned items have been actually executed.</returns>
        public virtual IEnumerable<IExecutedActivity> Execute(IEnumerable<IPlannedActivity> plannedItems) 
        {
            return new List<IExecutedActivity>();
        }


        protected IExecutedActivity SetComponent(IExecutedActivity addedItem, ISubSpecialityHolder component)
        {
            addedItem.SubSpecialityHolder = component;
            return addedItem;
        }

        public ISubSpecialityHolder SubSpecialityHolder { get; set; }
    }
}