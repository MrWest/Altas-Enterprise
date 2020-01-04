using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
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
    ///     Implementation of the contract <see cref="IInvestmentManagerApplicationServices" /> representing the application
    ///     services provider object responding to the CRUD requests coming from upper layers regarding to the domain entities:
    ///     investments.
    /// </summary>
    public class InvestmentManagerApplicationServices :
        InvestmentElementManagerApplicationServices<IInvestment, IInvestmentRepository, IInvestmentDomainServices>,
        IInvestmentManagerApplicationServices
    {

       public IInvestment Export(IDatabaseContext exportDatabaseContext,IInvestment investment)
        {
            if (exportDatabaseContext == null)
                return null;
         //  var defaultDatabaseContext = ServiceLocator.Current.GetInstance<IDatabaseContext>();

         var expInvestment =   SaveInvestemt( exportDatabaseContext, investment);
          

           exportDatabaseContext.Save();
           return expInvestment;
        }

       private IInvestment SaveInvestemt(IDatabaseContext exportDatabaseContext, IInvestment investmentElement)
       {

           var newInvestment = Repository.GetClone( investmentElement);

           newInvestment = ExportRelated(exportDatabaseContext, newInvestment);

            exportDatabaseContext.Add(newInvestment);

            //related Docs
            ExpRelatedDocuments(exportDatabaseContext, investmentElement, newInvestment);

           FillInvestmentElement(exportDatabaseContext, investmentElement, newInvestment);
            //recursive thread
            var investmentComponentRepository = ServiceLocator.Current.GetInstance<IInvestmentComponentRepository>();
            investmentComponentRepository.InvestmentElement = investmentElement;

            foreach (IInvestmentComponent investmentComponent in investmentComponentRepository.Entities)
            {
                SafeInvestemtElement(exportDatabaseContext, investmentComponent, newInvestment);
            }
            return newInvestment;

        }

        private static void ExpRelatedDocuments(IDatabaseContext exportDatabaseContext, IInvestmentElement investmentElement,
            IInvestmentElement newInvestment)
        {
               //RelatedDocuments

            var relatedDocRepo = ServiceLocator.Current.GetInstance<IInvestmentDocumentRepository>();
            relatedDocRepo.Holder = investmentElement;

            var oaceService =
                      ServiceLocator.Current.GetInstance<IOaceManagerApplicationServices>();
            
            var osdeService =
               ServiceLocator.Current.GetInstance<IOsdeManagerApplicationServices>();
            
            foreach (IInvestmentDocument investmentDocument in relatedDocRepo.Entities)
            {
                var newInvesmentDoc = relatedDocRepo.GetClone(investmentDocument);
                newInvesmentDoc.Holder = newInvestment;

                newInvesmentDoc.Oace = oaceService.Export(exportDatabaseContext,
                oaceService.Find(newInvesmentDoc.Oace))?.Id;
                newInvesmentDoc.Osde = osdeService.Export(exportDatabaseContext,
                    osdeService.Find(newInvesmentDoc.Osde))?.Id;

                exportDatabaseContext.Add(newInvesmentDoc);
            }
        }

        private void SafeInvestemtElement(IDatabaseContext exportDatabaseContext, IInvestmentComponent investmentElement,
            IInvestmentElement aboveElement)
        {
          
            var investmentComponentRepository = ServiceLocator.Current.GetInstance<IInvestmentComponentRepository>();
            investmentComponentRepository.InvestmentElement = investmentElement;
            var newInvestment = investmentComponentRepository.GetClone(investmentElement);
            newInvestment.Parent = aboveElement;

            exportDatabaseContext.Add(newInvestment);

            //related Docs
            ExpRelatedDocuments(exportDatabaseContext, investmentElement, aboveElement);

            FillInvestmentElement(exportDatabaseContext, investmentElement, newInvestment);


            //recursive thread
            //var investmentComponentRepository = ServiceLocator.Current.GetInstance<IInvestmentComponentRepository>();
            //investmentComponentRepository.InvestmentElement = investmentElement;

            foreach (IInvestmentComponent investmentComponent in investmentComponentRepository.Entities)
            {
                SafeInvestemtElement(exportDatabaseContext, investmentComponent, newInvestment);
            }
        }

        private static void FillInvestmentElement(IDatabaseContext exportDatabaseContext, IInvestmentElement investmentElement,
            IInvestmentElement newInvestment)
        {
//Equipment
            //planned


            ExportByBudgetComponents(exportDatabaseContext, investmentElement.Budget.EquipmentComponent, newInvestment.Budget.EquipmentComponent);

            //executed 
            ExportByBudgetComponentsExecuted(exportDatabaseContext, investmentElement.Budget.EquipmentComponent, newInvestment.Budget.EquipmentComponent);


            //Construction
            //planned
            ExportByBudgetComponents(exportDatabaseContext, investmentElement.Budget.ConstructionComponent, newInvestment.Budget.ConstructionComponent);

            //executed 

            ExportByBudgetComponentsExecuted(exportDatabaseContext, investmentElement.Budget.ConstructionComponent, newInvestment.Budget.ConstructionComponent);


            //Others
            //planned
            ExportByBudgetComponents(exportDatabaseContext, investmentElement.Budget.OtherExpensesComponent, newInvestment.Budget.OtherExpensesComponent);


            //executed 
            ExportByBudgetComponentsExecuted(exportDatabaseContext, investmentElement.Budget.OtherExpensesComponent, newInvestment.Budget.OtherExpensesComponent);


            //work capital
            //planned
            ExportByBudgetComponents(exportDatabaseContext, investmentElement.Budget.WorkCapitalComponent, newInvestment.Budget.WorkCapitalComponent);

            //executed 
            ExportByBudgetComponentsExecuted(exportDatabaseContext, investmentElement.Budget.WorkCapitalComponent, newInvestment.Budget.WorkCapitalComponent);


            var cashMovementsCategoryEntriesRepository =
                ServiceLocator.Current.GetInstance<ICashMovementCategoryRepository< ICashEntry>>();

            cashMovementsCategoryEntriesRepository.SuperiorCategory =
                investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries;

            newInvestment.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries =
                cashMovementsCategoryEntriesRepository.GetClone(investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries);
           SaveWorkCapitalCategories(exportDatabaseContext,cashMovementsCategoryEntriesRepository, newInvestment.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries);

            var cashMovementsCategoryOutgoingsRepository =
                ServiceLocator.Current.GetInstance<ICashMovementCategoryRepository<ICashOutgoing>>();

            cashMovementsCategoryOutgoingsRepository.SuperiorCategory =
                investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings;

            newInvestment.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings =
                cashMovementsCategoryOutgoingsRepository.GetClone(investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings);

            SaveWorkCapitalCategories(exportDatabaseContext,cashMovementsCategoryOutgoingsRepository,newInvestment.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings);
        }

        private static void ExportByBudgetComponents(IDatabaseContext exportDatabaseContext,
            IBudgetComponent component, IBudgetComponent newComponent)
        {
            var EquipmentSubSpecialityHolerRepository =
                ServiceLocator.Current
                    .GetInstance<IPlannedSubSpecialityHolderRepository>();
            EquipmentSubSpecialityHolerRepository.BudgetComponent = component;


            foreach (IPlannedSubSpecialityHolder subSpeciality in EquipmentSubSpecialityHolerRepository.Entities)
            {
                var newsubSpeciality = EquipmentSubSpecialityHolerRepository.GetClone(subSpeciality);
                newsubSpeciality.BudgetComponent = newComponent;

                var subspecialityService =
                    ServiceLocator.Current.GetInstance<ISubSpecialityManagerApplicationServices>();
                subspecialityService.Speciality = subspecialityService.Find(newsubSpeciality.SubSpeciality)?.Speciality;

                newsubSpeciality.SubSpeciality = subspecialityService.Export(exportDatabaseContext,
                    subspecialityService.Find(newsubSpeciality.SubSpeciality))?.Id;
                exportDatabaseContext.Add(newsubSpeciality);

                SavePlannedActivities(exportDatabaseContext, subSpeciality, newsubSpeciality);
            }
        }
        private static void ExportByBudgetComponentsExecuted(IDatabaseContext exportDatabaseContext,
           IBudgetComponent component, IBudgetComponent newComponent)
        {
            var EquipmentSubSpecialityHolerRepository =
                ServiceLocator.Current
                    .GetInstance<IExecutedSubSpecialityHolderRepository>();
            EquipmentSubSpecialityHolerRepository.BudgetComponent = component;


            foreach (IExecutedSubSpecialityHolder subSpeciality in EquipmentSubSpecialityHolerRepository.Entities)
            {
                var newsubSpeciality = EquipmentSubSpecialityHolerRepository.GetClone(subSpeciality);
                newsubSpeciality.BudgetComponent = newComponent;

                var subspecialityService =
                    ServiceLocator.Current.GetInstance<ISubSpecialityManagerApplicationServices>();
                subspecialityService.Speciality = subspecialityService.Find(newsubSpeciality.SubSpeciality)?.Speciality;

                newsubSpeciality.SubSpeciality = subspecialityService.Export(exportDatabaseContext,
                    subspecialityService.Find(newsubSpeciality.SubSpeciality))?.Id;
                exportDatabaseContext.Add(newsubSpeciality);

                SaveExecutedActivities(exportDatabaseContext, subSpeciality, newsubSpeciality);
            }
        }

        //private static void SaveWorkCapitalCategories<TMovement>(IDatabaseContext exportDatabaseContext, ICashMovementCategoryRepository<TMovement> cashMovementsCategoryRepository,
        //    IWorkCapitalCashFlow SuperiorCategory)
        //    where TMovement:class ,ICashMovementCategory
        //{
        //    foreach (TMovement cashEntry in cashMovementsCategoryRepository.Entities)
        //    {
        //       var newcashEntry = cashMovementsCategoryRepository.GetClone(cashEntry);
        //        newcashEntry.SuperiorCategory = SuperiorCategory;
        //        exportDatabaseContext.Add(newcashEntry);

        //        var  categoryRepository = ServiceLocator.Current.GetInstance< ICashMovementCategoryRepository<TMovement>>();
        //        categoryRepository.SuperiorCategory = cashEntry;

               
        //         SaveWorkCapitalCategories(exportDatabaseContext,categoryRepository,newcashEntry);

        //        var cashMovementRepositoryRepository =
        //        ServiceLocator.Current.GetInstance<ICashMovementRepository>();
        //        cashMovementRepositoryRepository.Category = cashEntry;


        //        SaveCashMovements(exportDatabaseContext, cashMovementRepositoryRepository, newcashEntry);


        //    }
        //}

        private static void SaveWorkCapitalCategories<TMovement>(IDatabaseContext exportDatabaseContext,
            ICashMovementCategoryRepository<TMovement> cashMovementsCategoryRepository,
             TMovement newSuperiorCategory)
            where TMovement : class, ICashMovementCategory
        {
            foreach (TMovement cashEntry in cashMovementsCategoryRepository.Entities)
            {
                var newcashEntry = cashMovementsCategoryRepository.GetClone(cashEntry);
                newcashEntry.SuperiorCategory = newSuperiorCategory;
                exportDatabaseContext.Add(newcashEntry);

                var categoryRepository = ServiceLocator.Current.GetInstance<ICashMovementCategoryRepository<TMovement>>();
                categoryRepository.SuperiorCategory = cashEntry;
                SaveWorkCapitalCategories(exportDatabaseContext, categoryRepository, newcashEntry);

                var cashMovementRepositoryRepository =
                 ServiceLocator.Current.GetInstance<ICashMovementRepository>();
                cashMovementRepositoryRepository.Category = cashEntry;


                SaveCashMovements(exportDatabaseContext, cashMovementRepositoryRepository, newcashEntry);
            }
        }

        private static void SaveCashMovements<TMovement>(IDatabaseContext exportDatabaseContext, ICashMovementRepository cashMovementsRepository, TMovement category)
         where TMovement : class ,ICashMovementCategory
       {
           foreach (ICashMovement cashEntry in cashMovementsRepository.Entities)
           {
                  var newcashMovement = cashMovementsRepository.GetClone(cashEntry);
                   newcashMovement.CashMovementCategory = category;
                    exportDatabaseContext.Add(newcashMovement);


             
           }
       }

        private static void SavePlannedActivities(IDatabaseContext exportDatabaseContext,
             ISubSpecialityHolder budgetComponent, ISubSpecialityHolder newbudgetComponent)
        //where TComponent:class,IBudgetComponent
        {
            var activityRepo = ServiceLocator.Current.GetInstance<IPlannedActivityRepository>();
            activityRepo.SubSpecialityHolder = budgetComponent;

            var service = ServiceLocator.Current.GetInstance<IPlannedActivityManagerApplicationServices>();
            service.SubSpecialityHolder = newbudgetComponent;

            foreach (IPlannedActivity plannedActivity in activityRepo.Entities)
            {
                service.Export(exportDatabaseContext, plannedActivity);
            }
        }

        private static void SaveExecutedActivities(IDatabaseContext exportDatabaseContext,
             ISubSpecialityHolder budgetComponent, ISubSpecialityHolder newbudgetComponent)
        //where TComponent : class,IBudgetComponent
        {
            var activityRepo = ServiceLocator.Current.GetInstance<IExecutedActivityRepository>();
            activityRepo.SubSpecialityHolder = budgetComponent;

            var service = ServiceLocator.Current.GetInstance<IExecutedActivityItemManagerApplicationServices>();
            service.SubSpecialityHolder = newbudgetComponent;
            foreach (IExecutedActivity executedActivity in activityRepo.Entities)
            {
                service.Export(exportDatabaseContext, executedActivity);

            }
        }
        
        public override IInvestment ExportRelated(IDatabaseContext exportDatabaseContext, IInvestment item)
        {
            var oaceService =
                        ServiceLocator.Current.GetInstance<IOaceManagerApplicationServices>();
            item.Oace = oaceService.Export(exportDatabaseContext,
                oaceService.Find(item.Oace))?.Id;
            var osdeService =
               ServiceLocator.Current.GetInstance<IOsdeManagerApplicationServices>();
            item.Osde = osdeService.Export(exportDatabaseContext,
                osdeService.Find(item.Osde))?.Id;

            var phaseService =
                        ServiceLocator.Current.GetInstance<IPhaseManagerApplicationServices>();
            item.Phase = phaseService.Export(exportDatabaseContext,
                phaseService.Find(item.Phase))?.Id;

            //var investmentTypeService =
            //            ServiceLocator.Current.GetInstance<IInvestmentTypeManagerApplicationServices>();
            //item.InvestmentType = investmentTypeService.Export(exportDatabaseContext,
            //    investmentTypeService.Find(item.InvestmentType))?.Id;


            return item;
        }

        public IEnumerable<IInvestment> FindInvestmentTypeByContains(string text)
        {
            if (text == null)
                return null;
            var repo = ServiceLocator.Current.GetInstance<ICommonRepository<IInvestment>>();
            return repo.Where(new Specification<IInvestment>(x => !string.IsNullOrEmpty(x.InvestmentType) && x.InvestmentType.ToLower().Contains(text.ToLower()))).Take(5);

        }

        public IEnumerable<IInvestment> FindNatureByContains(string text)
        {
            if (text == null)
                return null;
            var repo = ServiceLocator.Current.GetInstance<ICommonRepository<IInvestment>>();
            return repo.Where(new Specification<IInvestment>(x => !string.IsNullOrEmpty(x.Nature) && x.Nature.ToLower().Contains(text.ToLower()))).Take(5);

        }

        public IEnumerable<IInvestment> FindImpactByContains(string text)
        {
            if (text == null)
                return null;
            var repo = ServiceLocator.Current.GetInstance<ICommonRepository<IInvestment>>();
            return repo.Where(new Specification<IInvestment>(x => !string.IsNullOrEmpty(x.Impact) && x.Impact.ToLower().Contains(text.ToLower()))).Take(5);

        }

    
    }
}