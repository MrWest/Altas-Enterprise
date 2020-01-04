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
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class ExecutedSubSpecialityHolderManagerApplicationServices: SubSpecialityHolderManagerApplicationServices<IExecutedSubSpecialityHolder,
        IExecutedSubSpecialityHolderRepository,IExecutedSubSpecialityHolderDomainServices>,IExecutedSubSpecialityHolderManagerApplicationServices
    {
        private IPlannedSubSpecialityHolderRepository _plannedItemRepository;

        protected IPlannedSubSpecialityHolderRepository PlannedItemRepository
        {
            get
            {
                if (Equals(_plannedItemRepository, null))
                {
                    _plannedItemRepository = ServiceLocator.Current.GetInstance<IPlannedSubSpecialityHolderRepository>();
                    _plannedItemRepository.BudgetComponent = BudgetComponent;
                }

                return _plannedItemRepository;

            }
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
        public IEnumerable<IExecutedSubSpecialityHolder> BeExecuted(IPlannedSubSpecialityHolder[] plannedItems)
        {

            if (plannedItems == null)
                throw new ArgumentNullException("plannedItems");

            var dbPlannedItems = GetPlannedDatabaseItems(plannedItems).ToArray();

            IEnumerable<IExecutedSubSpecialityHolder> result = DomainServices.Execute(dbPlannedItems).ToArray();


            var executedActivityService = ServiceLocator.Current.GetInstance<IExecutedActivityItemManagerApplicationServices>();
            var plannedActivityRepo = ServiceLocator.Current.GetInstance<IPlannedActivityRepository>();
            foreach (IExecutedSubSpecialityHolder item in result)
            {
                Repository.Add(item);
                
                
                var planned = plannedItems.FirstOrDefault(x => x.Id.ToString() == item.Plannification.ToString());
                if (planned != null)
                    AdquireProperties(item, planned);

                executedActivityService.SubSpecialityHolder = item;
                plannedActivityRepo.SubSpecialityHolder = planned;
               // executedActivityService.PlannedItemRepository = plannedActivityRepo;
                if (executedActivityService.CanBeExecute(plannedActivityRepo.Entities))
                    executedActivityService.BeExecuted(plannedActivityRepo.Entities);

            }

          
           

            foreach (IPlannedSubSpecialityHolder item in dbPlannedItems)
            {
             
               
                plannedActivityRepo.SubSpecialityHolder = item;



            }



            return result;
        }

      
        private void AdquireProperties(IExecutedSubSpecialityHolder item, IPlannedSubSpecialityHolder planned)
        {
            item.SubSpeciality = planned.SubSpeciality;
            item.Category = planned.Category;
            item.SubExpenseConcept = planned.SubExpenseConcept;
            Repository.Update(item);
        }

        private IEnumerable<TPlanned> GetPlannedDatabaseItems<TPlanned>(IEnumerable<TPlanned> plannedItems) where TPlanned : class, IPlannedSubSpecialityHolder
        {
            
                IEnumerable<object> ids = from plannedItem in plannedItems select plannedItem.Id;
                IEnumerable<TPlanned> dbPlannedItems = PlannedItemRepository.FindById(ids.ToArray()).Cast<TPlanned>();

                return dbPlannedItems;
           
            
           
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
        public bool CanBeExecute(IPlannedSubSpecialityHolder[] plannedItems)
        {
            return true;
        }
    }
}