using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public abstract class ActivityManagerApplicationServices<TActivity, TRepository, TDomainServices> : BudgetComponentItemManagerApplicationServicesBase<TActivity, TRepository, TDomainServices>, IActivityManagerApplicationServices<TActivity>
        where TActivity:class ,IActivity
         //where TComponent : class, IBudgetComponent
        where TRepository : IActivityRepository<TActivity>
        where TDomainServices : IActivityDomainServices<TActivity>
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

        protected override TRepository Repository
        {
            get
            {
                TRepository repository = base.Repository;
                //repository.Component = Component;
                repository.SubSpecialityHolder = SubSpecialityHolder;
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
                //domainServices.Component = Component;
                domainServices.SubSpecialityHolder = SubSpecialityHolder;

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

            return "{0}->{1}".EasyFormat(SubSpecialityHolder.Id, baseKey);
        }

        /// <summary>
        ///     Determines whether there can be executed the given planned items.
        /// </summary>
        /// <typeparam name="TPlanned">The type of the planned items to try executing.</typeparam>
        /// <param name="plannedItems">
        ///     An <see cref="IEnumerable{T}" /> of planned items being the ones to determine whether there can
        ///     be executed.
        /// </param>
        /// <returns>True when there is at least one of <see cref="plannedItems" /> that can be executed; false otherwise.</returns>
        [CachesResult]
        public bool CanExecute(IEnumerable<IPlannedActivity> plannedItems)
        {
            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");



            return DomainServices.CanExecute(plannedItems);
        }

        ///// <summary>
        /////     Executes the given planned items.
        ///// </summary>
        ///// <param name="plannedItems">The <see cref="IEnumerable{T}" /> of planned items to execute.</param>
        ///// <returns>
        /////     The count of <see cref="IPlannedBudgetComponentItem" /> in <paramref name="plannedItems" /> that were actually
        /////     executed.
        ///// </returns>
        [Validate]
        [ResetsCache]
        [Commit]
        public IEnumerable<IExecutedActivity> Execute(IEnumerable<IPlannedActivity> plannedItems)
        {
            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");


            var executionService =
                ServiceLocator.Current.GetInstance<IExecutedActivityItemManagerApplicationServices>();
            executionService.SubSpecialityHolder = SubSpecialityHolder;
            if (executionService.CanBeExecute(plannedItems))
                return executionService.BeExecuted(plannedItems);
            //IEnumerable<IExecutedActivity> result =  DomainServices.Execute(plannedItems).ToArray();

            //var resourceSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IExecutedActivity>>();

            //foreach (IExecutedActivity toexectueItem in result)
            //{
            //    adquire
            //    resourceSevice.Component = toexectueItem;
            //    foreach (IPlannedResource resourceItem in toexectueItem.PlannedResources)
            //        resourceSevice.Add(resourceItem);

            //    Repository.Add(toexectueItem);
            //}


            //return result;
            return new List<IExecutedActivity>();
        }

       

        protected override TActivity SetAdquiring(TActivity onAdquiring, TActivity toAdquire)
        {

            base.SetAdquiring(onAdquiring, toAdquire);
           

            onAdquiring.SubSpeciality = toAdquire.SubSpeciality;

            //Update entity in current 
            Update(onAdquiring);

            return onAdquiring;
        }

        //public DateTime FinishDate(TActivity activity)
        //{
        //    if(activity==null)
        //        throw new ArgumentNullException("activity");

        //    //get db period for the activity
        //    //var periodRepo = ServiceLocator.Current.GetInstance<IPeriodRepository>();
        //    //periodRepo.Holder = activity;

        //    //if (periodRepo.Entities.Any())
        //    //{
        //    IPeriod period = activity.Period;
        //        // get all the menlabor resources
        //        var plannedResources = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<TActivity>>();
        //        plannedResources.Component = activity;

        //        var hours = new List<decimal>();

        //        foreach (IPlannedResource plannedResource in plannedResources.Entities)
        //        {
        //            if (plannedResource.ResourceKind == ResourceKind.MenLabor)
        //                hours.Add(plannedResource.Quantity / plannedResource.MenNumber);
        //        }



        //        //if nothing here
        //        if (hours.Count == 0)
        //            return period.OriEnd();
        //        //get max hours amount
        //        var maxhours = hours.Max();

        //        int days = (int)maxhours / 8;

        //        if ((int)maxhours % 8 > 0)
        //            days++;
        //        return period.Starts.AddDays(days);
        //  //  }
            
        //   // Repository.Delete(activity);
        //  //  return DateTime.Today;
        //}

        public ISubSpecialityHolder SubSpecialityHolder { get; set; }


        void IItemManagerApplicationServices<TActivity>.Add(TActivity item)
        {
            //var periodService = ServiceLocator.Current.GetInstance<IPeriodManagerApplicationServices>();
            //periodService.Holder = item;

            //periodService.Add(item.Period);

            //item.Period = periodService.Items.FirstOrDefault();

            base.Add(item);
        }
    }

}
