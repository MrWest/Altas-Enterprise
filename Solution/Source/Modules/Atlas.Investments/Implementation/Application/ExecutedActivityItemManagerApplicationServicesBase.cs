using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    ///     Implementation of the base contract
    ///     <see cref="ExecutedActivityItemManagerApplicationServicesBase" />
    ///     , representing the application services involved in responding the executed budget component items CRUD-operations
    ///     coming from upper layers.
    /// </summary>
    /// <typeparam name="TItem">The type of the executed budget component items managed here.</typeparam>
    /// <typeparam name="TComponent">The type of budget component to which belong the excuted items managed here.</typeparam>
    /// <typeparam name="TRepository">
    ///     The type of the repository used to carry on with the data operations regarding to the executed items.
    /// </typeparam>
    /// <typeparam name="TDomainServices">
    ///     The type of the domain services used to enforce the business layers for the executed items managed here.
    /// </typeparam>
    public  class ExecutedActivityItemManagerApplicationServicesBase:
        BudgetComponentItemManagerApplicationServicesBase<IExecutedActivity, IExecutedActivityRepository, IExecutedActivityDomainServices>,
        IExecutedActivityItemManagerApplicationServices
        //where TItem : class, IExecutedBudgetComponentItem
        //where TComponent : class, IBudgetComponent
        //where TRepository : IExecutedActivityRepository<TComponent>
        //where TDomainServices : IExecutedActivityDomainServices< TComponent>
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

        protected override IExecutedActivityRepository Repository
        {
            get
            {
                IExecutedActivityRepository repository = base.Repository;
                //repository.Component = Component;
                repository.SubSpecialityHolder = SubSpecialityHolder;
                return repository;
            }
        }

        /// <summary>
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        protected override IExecutedActivityDomainServices DomainServices
        {
            get
            {
                IExecutedActivityDomainServices domainServices = base.DomainServices;
                //domainServices.Component = Component;
                domainServices.SubSpecialityHolder = SubSpecialityHolder;
                return domainServices;
            }
        }
        /// <summary>
        ///     When overridden in a deriver it gets the corresponding planned items repository allowing to access the executed
        ///     items in the current application services being their execution.
        /// </summary>
       // public  IPlannedActivityRepository PlannedItemRepository { get; set; }


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
        public bool CanBeExecute<TPlanned>(IEnumerable<TPlanned> plannedItems) where TPlanned : class, IPlannedActivity
        {
            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");

            var dbPlannedItems = plannedItems;

            return DomainServices.CanExecute(dbPlannedItems);
        }

       

        /// <summary>
        ///     Executes the given planned items.
        /// </summary>
        /// <param name="plannedItems">The <see cref="IEnumerable{T}" /> of planned items to execute.</param>
        /// <returns>
        ///     The count of <see cref="IPlannedBudgetComponentItem" /> in <paramref name="plannedItems" /> that were actually
        ///     executed.
        /// </returns>
        [Validate]
        [ResetsCache]
        [Commit]
        public IEnumerable<IExecutedActivity> BeExecuted<TPlanned>(IEnumerable<TPlanned> plannedItems) where TPlanned : class, IPlannedActivity
        {
            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");

            var dbPlannedItems = plannedItems.ToArray();

            IEnumerable<IExecutedActivity> result = DomainServices.Execute(dbPlannedItems).ToArray();

            foreach (IExecutedActivity item in result)
            {
                Repository.Add(item);
                var planned = plannedItems.FirstOrDefault(x => x.Id.ToString() == item.Planification.ToString());
                if (planned != null)
                    AdquireProperties(item,planned);

            }
               

            //var 
           
            //var resourceService = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IExecutedActivity>>();
           
            
           
            //foreach (IExecutedActivity executedItem in result)
            //{
            //    resourceService.Component = executedItem;
            //    khjhkjk
            //    foreach (IPlannedResource resourceItem in executedItem.PlannedResources)
            //    {
            //        resourceService.Add(resourceItem);
            //        ResoruceRecursiveFill<TPlanned>( resourceItem);

            //    }

            //    // Repository.Add(executedItem);
            //}


            return result;
        }

        protected IExecutedActivity AdquireProperties(IExecutedActivity onAdquiring, IPlannedActivity toAdquire)
        {

            var resourceRepoSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IExecutedActivity>>();
            resourceRepoSevice.Component = onAdquiring;
            resourceRepoSevice.DeleteAll();

            var resourceRepoSeviceOther = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IPlannedActivity>>();

            resourceRepoSeviceOther.Component = toAdquire;
            foreach (IPlannedResource plannedResource in resourceRepoSeviceOther.Entities)
            {
                var recursiveDomainSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceDomainServices<IExecutedActivity>>();
                recursiveDomainSevice.Component = onAdquiring;
                var recursiveApplicationSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IExecutedActivity>>();
                recursiveApplicationSevice.Component = onAdquiring;
                var recursiveRepoSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IExecutedActivity>>();
                recursiveRepoSevice.Component = onAdquiring;
                var newAquiredResource = recursiveApplicationSevice.AdquireProperties(
                   recursiveRepoSevice.Add(recursiveDomainSevice.Create()), plannedResource);

                //   recursiveRepoSevice.Update(newAquiredResource);
            }


            //}

            onAdquiring.Code = toAdquire.Code;
            onAdquiring.Name = toAdquire.Name;
            onAdquiring.Description = toAdquire.Description;
            onAdquiring.MeasurementUnit = toAdquire.MeasurementUnit;
            onAdquiring.Currency = toAdquire.Currency;
            onAdquiring.Quantity = toAdquire.Quantity;
            onAdquiring.SubExpenseConcept = toAdquire.SubExpenseConcept;
            onAdquiring.Category = toAdquire.Category;
            onAdquiring.UnitaryCost = toAdquire.UnitaryCost;
            onAdquiring.SubSpeciality = toAdquire.SubSpeciality;

          //  onAdquiring.Period = ServiceLocator.Current.GetInstance<IPeriod>(); ;
            onAdquiring.Period.Starts = toAdquire.Period.Starts;
            onAdquiring.Period.Ends = toAdquire.Period.Ends;
            //  onAdquiring = SetAdquiring(onAdquiring, toAdquire);
            if (onAdquiring.Id != null)
                Repository.Update(onAdquiring);

            return onAdquiring;
          

        }

        private static void ResoruceRecursiveFill<TPlanned>( IPlannedResource executedItem)
            where TPlanned : class, IPlannedBudgetComponentItem
        {
            var resourceService = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IPlannedResource>>();
            resourceService.Component = executedItem;
            foreach (IPlannedResource resourceItem in executedItem.PlannedResources)
            {
                resourceService.Add(resourceItem);
                ResoruceRecursiveFill<TPlanned>( resourceItem);

            }
                
        }

       
        public ISubSpecialityHolder SubSpecialityHolder { get; set; }

    
        public override IExecutedActivity Export(IDatabaseContext exportDatabaseContext, IExecutedActivity item)
        {
            var newexecutedActivity = Repository.GetClone(item);
            newexecutedActivity.SubSpecialityHolder = SubSpecialityHolder;

            //Export MeasurementUnits and Currency
            newexecutedActivity = ExportRelated(exportDatabaseContext, newexecutedActivity);

            exportDatabaseContext.Add(newexecutedActivity);
            //for period
            //var periodRepository = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            //periodRepository.Holder = plannedActivity;
            //exportDatabaseContext.Add(periodRepository.Entities.First());

            var resourceRepository =
                    ServiceLocator.Current
                        .GetInstance<IBudgetComponentResourceRepository<IExecutedActivity>>();
            resourceRepository.Component = item;

            var serviceResource = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IExecutedActivity>>();
            serviceResource.Component = newexecutedActivity;

            foreach (IPlannedResource plannedResource in resourceRepository.Entities)
            {
                serviceResource.Export(exportDatabaseContext, plannedResource);
            }

            //Execution Log
            var executionRepository =
               ServiceLocator.Current
                   .GetInstance<IExecutionRepository>();
            executionRepository.ExecutedActivity = item;

            foreach (IExecution execution in executionRepository.Entities)
            {
                var newExecution = executionRepository.GetClone(execution);
                newExecution.ExecutedActivity = newexecutedActivity;
                exportDatabaseContext.Add(newExecution);
            }


            return newexecutedActivity;
        }
    }
}