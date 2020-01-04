using System;
using System.Reflection;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Features;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.Construction;
using CompanyName.Atlas.Investments.Application.Budget.Equipment;
using CompanyName.Atlas.Investments.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Construction;
using CompanyName.Atlas.Investments.Domain.Services.Budget.Equipment;
using CompanyName.Atlas.Investments.Domain.Services.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentComponentManagerApplicationServices" /> representing the
    ///     application services provider used to respond to the CRUD requests coming from upper layers regarding to the domain
    ///     entities: investment components.
    /// </summary>
    public class InvestmentComponentManagerApplicationServices :
        InvestmentElementManagerApplicationServices<IInvestmentComponent, IInvestmentComponentRepository, IInvestmentComponentDomainServices>,
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

        [Commit]
        public void Paste()
        {
            if (Equals(CopiedObject, null))
                return;
            //if (Equals(ObjectToPasteOn, null))
            //    return;
            try
            {
                var entity = CopiedObject as IInvestmentComponent;
                if (Equals(entity, null))
                    return;
                // creates an addes a new element
                var newElement = Repository.Add(DomainServices.Create());
                // adquiere local values
                AdquireSome(newElement, entity);
                // adquires related values
                AdquireAll(newElement, entity);

                //SubComponents

                var applicationService =
                    ServiceLocator.Current.GetInstance<IInvestmentComponentManagerApplicationServices>();
                applicationService.InvestmentElement = newElement;
                var componentRepoService =
                    ServiceLocator.Current.GetInstance<IInvestmentComponentRepository>();
                componentRepoService.InvestmentElement = entity;

                foreach (IInvestmentComponent component in componentRepoService.Entities)
                {
                    applicationService.CopiedObject = component;
                    applicationService.Paste();
                }




            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                throw new Exception(e.Message);
            }
           

        }

        private void AdquireSome(IInvestmentComponent adquiring, IInvestmentComponent toaquiquire)
        {
            adquiring.Name = toaquiquire.Name;
            adquiring.Description = toaquiquire.Description;
            adquiring.Code = toaquiquire.Code;
            adquiring.Constructor = toaquiquire.Constructor;
            adquiring.Location = toaquiquire.Location;
            adquiring.Objective = toaquiquire.Objective;
            adquiring.Scope = toaquiquire.Scope;
            adquiring.Period.Starts = toaquiquire.Period.Starts;
            adquiring.Period.Ends = toaquiquire.Period.Ends;

            Repository.Update(adquiring);
           // aquiring.Description = toaquiquire.Description;


           
        }

        private void AdquireAll(IInvestmentComponent adquiring, IInvestmentComponent toadquiquire)
        {
            //period
            var periodRepo = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            periodRepo.Holder = adquiring;
            periodRepo.Update(adquiring.Period);

            //Equipment

            //sets everythigs for the loop


            AdquireBudgetElements(adquiring.Budget.EquipmentComponent, toadquiquire.Budget.EquipmentComponent);

            AdquireBudgetElements(adquiring.Budget.ConstructionComponent, toadquiquire.Budget.ConstructionComponent);

            AdquireBudgetElements(adquiring.Budget.OtherExpensesComponent, toadquiquire.Budget.OtherExpensesComponent);

            AdquireBudgetElements(adquiring.Budget.WorkCapitalComponent, toadquiquire.Budget.WorkCapitalComponent);
            
           

        }

        private static void AdquireBudgetElements(IBudgetComponent adquiring, IBudgetComponent toadquiquire)
        {
            var repository = ServiceLocator.Current.GetInstance<IPlannedSubSpecialityHolderRepository>();
            repository.BudgetComponent = toadquiquire;

            var adquiringRepository = ServiceLocator.Current.GetInstance<IPlannedSubSpecialityHolderRepository>();
            adquiringRepository.BudgetComponent = adquiring;

            var adquiringDomain = ServiceLocator.Current.GetInstance<IPlannedSubSpecialityHolderDomainServices>();

            //  repository.BudgetComponent = component;
            foreach (ISubSpecialityHolder subSpecialityHolder in repository.Entities)
            {
                var newSubSpecialityHolder = adquiringDomain.Create();
                newSubSpecialityHolder.SubSpeciality = subSpecialityHolder.SubSpeciality;
                newSubSpecialityHolder = adquiringRepository.Add(newSubSpecialityHolder);

                var activityRepo = ServiceLocator.Current.GetInstance<IPlannedActivityRepository>();
                activityRepo.SubSpecialityHolder = subSpecialityHolder;

                var adquirindActivityRepo = ServiceLocator.Current.GetInstance<IPlannedActivityRepository>();
                adquirindActivityRepo.SubSpecialityHolder = newSubSpecialityHolder;

                var adquirindActivityService = ServiceLocator.Current.GetInstance<IPlannedActivityManagerApplicationServices>();
                adquirindActivityService.SubSpecialityHolder = newSubSpecialityHolder;

                var adquirindActivityDomain = ServiceLocator.Current.GetInstance<IPlannedActivityDomainServices>();
                adquirindActivityDomain.SubSpecialityHolder = newSubSpecialityHolder;

                foreach (IPlannedActivity plannedActivity in activityRepo.Entities)
                {
                    var newActivity = adquirindActivityDomain.Create();
                    //Changing Quantity for now
                    newActivity.Quantity = plannedActivity.Quantity;
                    var aquiringActivity = adquirindActivityRepo.Add(newActivity);


                    //uses to adquire method
                    adquirindActivityService.AdquireProperties(aquiringActivity, plannedActivity);
                }
            }
        }

       
        public IEntity CopiedObject { get; set; }
       

        //public ICopyPasteable ObjectToPasteOn { get; set; }
    }
}