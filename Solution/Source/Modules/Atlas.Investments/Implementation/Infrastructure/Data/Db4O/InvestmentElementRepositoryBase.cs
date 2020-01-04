using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Construction;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;


namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{


    /// <summary>
    ///     Default implementation of the contract <see cref="IInvestmentElementRepository2{T}" />, representing the contract
    ///     of the
    ///     repository of investment elements. Note: that this implementation targets Db4O Database System.
    /// </summary>
    public abstract class InvestmentElementRepositoryBase<T> : Db4ORepositoryBase<T>, IInvestmentElementRepository2<T>
        where T : class, IInvestmentElement
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementRepositoryBase{T}" /> given the database context.
        /// </summary>
        /// <param name="databaseContext">
        ///     The <see cref="IDb4ODatabaseContext" /> representing the Db4O database context used to carry on with the row data
        ///     operations.
        /// </param>
        protected InvestmentElementRepositoryBase(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets the properties of the investment elements to save its values.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.Code),
                    GetName(x => x.Location), GetName(x => x.Constructor), GetName(x => x.Objective), GetName(x => x.Scope)
                }).ToArray();
            }
        }


        /// <summary>
        ///     Adds to the current repository the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement" /> does not have an <see cref="IBudget" />.</exception>
        public override T Add(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.InvestmentElementWithoutBudget, "investmentElement");
            if (investmentElement.Period == null)
                throw new ArgumentException(Resources.InvestmentElementWithoutBudget, "investmentElement");

            T addedInvestmentElement = base.Add(investmentElement);
            investmentElement.Budget = addedInvestmentElement.Budget;
            investmentElement.Period = addedInvestmentElement.Period;

            this.Relate(investmentElement, investmentElement.Period, DatabaseContext);
            this.Relate(investmentElement, investmentElement.Budget, DatabaseContext);


            return addedInvestmentElement;
        }




        /// <summary>
        ///     Updates the changes there were made to the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement" /> does not have an <see cref="IBudget" />.</exception>
        public override void Update(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.InvestmentElementWithoutBudget, "investmentElement");

            base.Update(investmentElement);


            this.Relate(investmentElement, investmentElement.Budget, DatabaseContext);
            this.Relate(investmentElement, investmentElement.Period, DatabaseContext);

        }

        /// <summary>
        ///     Deletes the given investment element from the repository.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement" /> has a null budget.</exception>
        public override void Delete(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentElement");

            this.Unrelate(investmentElement, investmentElement.Budget, DatabaseContext);
            this.Unrelate(investmentElement, investmentElement.Period, DatabaseContext);

            var dbInvestmentElement = DatabaseContext.Find<IInvestmentElement>(investmentElement.Id);
            DatabaseContext.Delete(dbInvestmentElement);
        }


        /// <summary>
        ///     Relates the given investment element with a budget, following the domain specification that every investment
        ///     element must have a
        ///     budget.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to relate to <paramref name="budget" />.</param>
        /// <param name="budget">The <see cref="IBudget" /> to relate to <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="budget" /> is null.
        /// </exception>
        public void Relate(T investmentElement, IBudget budget)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (budget == null)
                throw new ArgumentNullException("budget");

            investmentElement.Budget = budget;
            budget.InvestmentElement = investmentElement;
        }
        /// <summary>
        /// Relates the given investment element with a period, following the domain specification that every investment element must have a
        /// time period.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="period"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Relate(T investmentElement, IPeriod period)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (period == null)
                throw new ArgumentNullException("period");

            investmentElement.Period = period;
            period.Holder = investmentElement;
        }
        /// <summary>
        ///     Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> to break its relation with
        ///     <paramref name="budget" />.
        /// </param>
        /// <param name="budget">The <see cref="IBudget" /> to break its relation with <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="budget" /> is null.
        /// </exception>
        public void Unrelate(T investmentElement, IBudget budget)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (budget == null)
                throw new ArgumentNullException("budget");

            investmentElement.Budget = null;
            budget.InvestmentElement = null;

            Delete(budget);
        }

        /// <summary>
        ///     Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> to break its relation with
        ///     <paramref name="period" />.
        /// </param>
        /// <param name="period">The <see cref="IPeriod" /> to break its relation with <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="period" /> is null.
        /// </exception>
        public void Unrelate(T investmentElement, IPeriod period)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (period == null)
                throw new ArgumentNullException("budget");

            investmentElement.Period = null;
            period.Holder = null;

            Delete(period);
        }
        /// <summary>
        ///     Saves the references of an investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to save its references.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        public void SaveReference(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");

            if (Equals(investmentElement.Id, default(Guid)))
                return;

            DatabaseContext.Save();
            // DatabaseContext.Store(investmentElement.Elements);
        }

        /// <summary>
        ///     Saves the references of a budget.
        /// </summary>
        /// <param name="budget">The <see cref="IBudget" /> to save its references.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budget" /> is null.</exception>
        public void SaveReference(IBudget budget)
        {
            // DatabaseContext.Store(budget);
        }
        /// <summary>
        ///     Saves the references of a budget.
        /// </summary>
        /// <param name="period">The <see cref="IBudget" /> to save its references.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budget" /> is null.</exception>
        public void SaveReference(IPeriod period)
        {
            // DatabaseContext.Store(period);
        }

        /// <summary>
        ///     Makes a clone of the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to make a clone of.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="investmentElement" /> has a null budget when not deleting an investment element.
        /// </exception>
        /// <returns>
        ///     A clone of <paramref name="investmentElement" /> with all the relevant data to this repository copied into the
        ///     clone.
        /// </returns>
        protected override T Clone(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");

            T investmentElementClone = base.Clone(investmentElement);

            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentElement");

            investmentElementClone.Budget = Clone(investmentElement.Budget);
            investmentElementClone.Budget.InvestmentElement = investmentElementClone;


            investmentElementClone.Period = Clone(investmentElement.Period);
            investmentElementClone.Period.Holder = investmentElementClone;

            return investmentElementClone;
        }

        private void Delete(IBudget budget)
        {
            //equipment
            DeleteComponentParts<IPlannedActivity, IPlannedActivityRepository>(budget.EquipmentComponent);
            DeleteComponentParts<IExecutedActivity, IExecutedActivityRepository>(budget.EquipmentComponent);

            //construction
            DeleteComponentParts<IPlannedActivity, IPlannedActivityRepository>(budget.ConstructionComponent);
            DeleteComponentParts<IExecutedActivity, IExecutedActivityRepository>(budget.ConstructionComponent);

            //others
            DeleteComponentParts<IPlannedActivity, IPlannedActivityRepository>(budget.OtherExpensesComponent);
            DeleteComponentParts<IExecutedActivity, IExecutedActivityRepository>(budget.OtherExpensesComponent);

            //work capital
            DeleteComponentParts<IPlannedActivity, IPlannedActivityRepository>(budget.WorkCapitalComponent);
            DeleteComponentParts<IExecutedActivity, IExecutedActivityRepository>(budget.WorkCapitalComponent);

            Delete(budget.EquipmentComponent);
            Delete(budget.ConstructionComponent);
            Delete(budget.OtherExpensesComponent);
            Delete(budget.WorkCapitalComponent);

            DatabaseContext.Delete(budget);
        }
        private void Delete(IPeriod period)
        {
            DatabaseContext.Delete(period);
        }

        private void DeleteComponentParts<T, TRepository>(IBudgetComponent component)
          where T : class, IActivity
          //where TComponent : class, IBudgetComponent
          where TRepository : IActivityRepository<T>
        {
            var repository = ServiceLocator.Current.GetInstance<IPlannedSubSpecialityHolderRepository>();
            repository.BudgetComponent = component;

            foreach (IPlannedSubSpecialityHolder subSpecialityHolder in repository.Entities)
            {
                var activityRepo = ServiceLocator.Current.GetInstance<TRepository>();
                activityRepo.SubSpecialityHolder = subSpecialityHolder;
                activityRepo.DeleteAll();
            }

            repository.DeleteAll();

            var repositoryEx = ServiceLocator.Current.GetInstance<IPlannedSubSpecialityHolderRepository>();
            repositoryEx.BudgetComponent = component;

            foreach (IExecutedSubSpecialityHolder subSpecialityHolder in repositoryEx.Entities)
            {
                var activityRepo = ServiceLocator.Current.GetInstance<TRepository>();
                activityRepo.SubSpecialityHolder = subSpecialityHolder;
                activityRepo.DeleteAll();
            }

            repositoryEx.DeleteAll();

        }
        //private static void DeleteComponentParts<TElem, TComponent, TRepository>(TComponent component)
        //    where TElem : class, IBudgetComponentItem
        //    where TComponent : class, IBudgetComponent
        //    where TRepository : IPlannedActivityRepository<TComponent>
        //{
        //    var repository = ServiceLocator.Current.GetInstance<TRepository>();
        //    repository.Component = component;
        //    repository.DeleteAll();
        //}
        //private void DeleteComponentPartsExecuted<T, TComponent, TRepository>(TComponent component)
        // where T : class, IBudgetComponentItem
        // where TComponent : class, IBudgetComponent
        // where TRepository : IExecutedActivityRepository<TComponent>
        //{
        //    var repository = ServiceLocator.Current.GetInstance<TRepository>();
        //    repository.Component = component;
        //    repository.DeleteAll();
        //}

        private void Delete(IBudgetComponent component)
        {
            // DatabaseContext.Delete((object)component.PlannedResources);
            DatabaseContext.Delete((object)component.PlannedActivities);
            //  DatabaseContext.Delete((object)component.ExecutedResources);
            DatabaseContext.Delete((object)component.ExecutedActivities);
            DatabaseContext.Delete(component);
        }

        private IBudget Clone(IBudget budget)
        {
            var budgetClone = ServiceLocator.Current.GetInstance<IBudget>();

            var equipmentClone = ServiceLocator.Current.GetInstance<IEquipmentComponent>();
            var constructionClone = ServiceLocator.Current.GetInstance<IConstructionComponent>();
            var otherExpensesClone = ServiceLocator.Current.GetInstance<IOtherExpensesComponent>();
            var workCapitalClone = ServiceLocator.Current.GetInstance<IWorkCapitalComponent>();

            equipmentClone.Budget = budgetClone;
            constructionClone.Budget = budgetClone;
            otherExpensesClone.Budget = budgetClone;
            workCapitalClone.Budget = budgetClone;

            budgetClone.EquipmentComponent = equipmentClone;
            budgetClone.ConstructionComponent = constructionClone;
            budgetClone.OtherExpensesComponent = otherExpensesClone;

            var workCapitalCashFlowClone = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlow>();
            workCapitalCashFlowClone.WorkCapital = workCapitalClone;
            workCapitalClone.WorkCapitalCashFlow = workCapitalCashFlowClone;

            var workCapitalCashFlowClone2 = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlow>();
            workCapitalCashFlowClone2.WorkCapital = workCapitalClone;
            workCapitalClone.ExecutedWorkCapitalCashFlow = workCapitalCashFlowClone2;

            budgetClone.WorkCapitalComponent = workCapitalClone;


            budgetClone.Id = budget?.Id ?? DatabaseContext.GenerateKey();

            budgetClone.EquipmentComponent.Id = budget?.EquipmentComponent?.Id ?? DatabaseContext.GenerateKey();
            budgetClone.ConstructionComponent.Id = budget?.ConstructionComponent?.Id ?? DatabaseContext.GenerateKey();
            budgetClone.OtherExpensesComponent.Id = budget?.OtherExpensesComponent?.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.Id = budget.WorkCapitalComponent.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.Id = budget.WorkCapitalComponent.WorkCapitalCashFlow.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Id = budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Id ?? DatabaseContext.GenerateKey();
            //for new domain
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries.Id = budget.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings.Id = budget.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashEntries.Id = budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashEntries.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashOutgoings.Id = budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashOutgoings.Id ?? DatabaseContext.GenerateKey();


            return budgetClone;
        }
        private IPeriod Clone(IPeriod period)
        {
            var periodClone = ServiceLocator.Current.GetInstance<IPeriod>();

            periodClone.Starts = period.OriStart();
            periodClone.Ends = period.OriEnd();
            periodClone.PeriodKind = period.PeriodKind;

            periodClone.Id = period.Id ?? DatabaseContext.GenerateKey();

            return periodClone;
        }
    }


 /// <summary>
    ///     Default implementation of the contract <see cref="IInvestmentElementRepository2{T}" />, representing the contract
    ///     of the
    ///     repository of investment elements. Note: that this implementation targets Db4O Database System.
    /// </summary>
    public abstract class InvestmentElementRepositoryBaseEF<T, TClass> : EntityFrameworkRepositoryBase<T, TClass>, IInvestmentElementRepository2<T>
        where T : class, IInvestmentElement
         where TClass : InvestmentElement
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementRepositoryBase{T}" /> given the database context.
        /// </summary>
        /// <param name="databaseContext">
        ///     The <see cref="IDb4ODatabaseContext" /> representing the Db4O database context used to carry on with the row data
        ///     operations.
        /// </param>
        protected InvestmentElementRepositoryBaseEF(IEntityFrameworkDbContext<TClass> databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        ///     Gets the properties of the investment elements to save its values.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description), GetName(x => x.Code), 
                    GetName(x => x.Location), GetName(x => x.Constructor), GetName(x => x.Objective), GetName(x => x.Scope), GetName(x => x.BudgetId), GetName(x => x.PeriodId),
                    GetName(x => x.LastCalculatedStartDate), GetName(x => x.LastCalculatedFinishDate), GetName(x => x.StartCalculated), GetName(x => x.EndCalculated)
                }).ToArray();
            }
        }


        /// <summary>
        ///     Adds to the current repository the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement" /> does not have an <see cref="IBudget" />.</exception>
        public override T Add(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.InvestmentElementWithoutBudget, "investmentElement");
            if (investmentElement.Period == null)
                throw new ArgumentException(Resources.InvestmentElementWithoutBudget, "investmentElement");


            this.Relate(investmentElement, investmentElement.Period);
            this.Relate(investmentElement, investmentElement.Budget);


           
           

            T addedInvestmentElement = base.Add(investmentElement);

            investmentElement.Budget = addedInvestmentElement.Budget;
            investmentElement.Period = addedInvestmentElement.Period;

            investmentElement.BudgetId = addedInvestmentElement.BudgetId;
            investmentElement.PeriodId = addedInvestmentElement.PeriodId;


          

            return investmentElement;
        }

       
       

        /// <summary>
        ///     Updates the changes there were made to the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement" /> does not have an <see cref="IBudget" />.</exception>
        public override void Update(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.InvestmentElementWithoutBudget, "investmentElement");

            this.Relate(investmentElement, investmentElement.Period);
            this.Relate(investmentElement, investmentElement.Budget);

            base.Update(investmentElement);

           
           
          
        }

        /// <summary>
        ///     Deletes the given investment element from the repository.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement" /> has a null budget.</exception>
        public override void Delete(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentElement");

            this.Unrelate(investmentElement, investmentElement.Budget);
            this.Unrelate(investmentElement, investmentElement.Period);

           // var dbInvestmentElement = DatabaseContext.Find<IInvestmentElement>(investmentElement.Id);

            var invComponentsRepo = ServiceLocator.Current.GetInstance<IInvestmentComponentRepository>();
            invComponentsRepo.InvestmentElement = investmentElement;
            invComponentsRepo.DeleteAll();
            base.Delete(investmentElement);
        }


        /// <summary>
        ///     Relates the given investment element with a budget, following the domain specification that every investment
        ///     element must have a
        ///     budget.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to relate to <paramref name="budget" />.</param>
        /// <param name="budget">The <see cref="IBudget" /> to relate to <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="budget" /> is null.
        /// </exception>
        public void Relate(T investmentElement, IBudget budget)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (budget == null)
                throw new ArgumentNullException("budget");

            investmentElement.Budget = budget;
            investmentElement.BudgetId = budget.Id;
            budget.InvestmentElement = investmentElement;
        }
        /// <summary>
        /// Relates the given investment element with a period, following the domain specification that every investment element must have a
        /// time period.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="period"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Relate(T investmentElement, IPeriod period)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (period == null)
                throw new ArgumentNullException("period");

            investmentElement.Period = period;
            investmentElement.PeriodId = period.Id;
            period.Holder = investmentElement;
        }
        /// <summary>
        ///     Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> to break its relation with
        ///     <paramref name="budget" />.
        /// </param>
        /// <param name="budget">The <see cref="IBudget" /> to break its relation with <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="budget" /> is null.
        /// </exception>
        public void Unrelate(T investmentElement, IBudget budget)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (budget == null)
                throw new ArgumentNullException("budget");

            investmentElement.Budget = null;
            investmentElement.BudgetId = null;
            budget.InvestmentElement = null;

            Delete(budget);
        }

        /// <summary>
        ///     Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">
        ///     The <see cref="IInvestmentElement" /> to break its relation with
        ///     <paramref name="period" />.
        /// </param>
        /// <param name="period">The <see cref="IPeriod" /> to break its relation with <paramref name="investmentElement" />.</param>
        /// <exception cref="ArgumentNullException">
        ///     Either <paramref name="investmentElement" /> or <paramref name="period" /> is null.
        /// </exception>
        public void Unrelate(T investmentElement, IPeriod period)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (period == null)
                throw new ArgumentNullException("budget");

            investmentElement.Period = null;
            investmentElement.PeriodId = null;
            period.Holder = null;

            Delete(period);
        }
        /// <summary>
        ///     Saves the references of an investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to save its references.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        public void SaveReference(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");

            if (Equals(investmentElement.Id, default(Guid)))
                return;

            DatabaseContext.Save();
            DatabaseContext.Save();
        }

        /// <summary>
        ///     Saves the references of a budget.
        /// </summary>
        /// <param name="budget">The <see cref="IBudget" /> to save its references.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budget" /> is null.</exception>
        public void SaveReference(IBudget budget)
        {
           // DatabaseContext.Store(budget);
        }
        /// <summary>
        ///     Saves the references of a budget.
        /// </summary>
        /// <param name="period">The <see cref="IBudget" /> to save its references.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budget" /> is null.</exception>
        public void SaveReference(IPeriod period)
        {
         // DatabaseContext.Store(period);
        }

        /// <summary>
        ///     Makes a clone of the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to make a clone of.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="investmentElement" /> has a null budget when not deleting an investment element.
        /// </exception>
        /// <returns>
        ///     A clone of <paramref name="investmentElement" /> with all the relevant data to this repository copied into the
        ///     clone.
        /// </returns>
        protected override T Clone(T investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");

            T investmentElementClone = base.Clone(investmentElement);

            if (investmentElement.Budget == null && investmentElement.BudgetId !=null)
            {
                var budgetRepo = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Domain.Entities.Budget>>();

                investmentElement.Budget = budgetRepo.Find(investmentElement.BudgetId);

                if (investmentElement.Budget == null)
                    throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentElement");

            }
            if (!isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();
                var realPeriod = perDBC.Find(investmentElement.PeriodId);
                if (realPeriod != null)
                    investmentElement.Period = realPeriod;


            }

            investmentElementClone.Budget = Clone(investmentElement.Budget);
            investmentElementClone.Budget.InvestmentElement = investmentElementClone;
            investmentElementClone.BudgetId = investmentElementClone.Budget.Id;

           

            investmentElementClone.Period = Clone(investmentElement.Period);
            investmentElementClone.Period.Holder = investmentElementClone;
            investmentElementClone.PeriodId = investmentElementClone.Period.Id;

            if (isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();

                perDBC.Add(investmentElementClone.Period);
                perDBC.Save();
                investmentElementClone.Period = Clone(investmentElementClone.Period);
                investmentElementClone.Period.Holder = investmentElementClone;
                investmentElementClone.PeriodId = investmentElementClone.Period.Id;
            }

            return investmentElementClone;
        }

        /// <summary>
        ///     Makes a clone of the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement" /> to make a clone of.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement" /> is null.</exception>
        /// <exception cref="ArgumentException">
        ///     <paramref name="investmentElement" /> has a null budget when not deleting an investment element.
        /// </exception>
        /// <returns>
        ///     A clone of <paramref name="investmentElement" /> with all the relevant data to this repository copied into the
        ///     clone.
        /// </returns>
        protected override TClass Clone(TClass investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");

            if (!isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();
                var realPeriod = perDBC.Find(investmentElement.PeriodId);
                if (realPeriod != null)
                    investmentElement.Period = realPeriod;


            }

            TClass investmentElementClone = base.Clone(investmentElement);

            if (investmentElement.Budget == null && investmentElement.BudgetId != null)
            {
                var budgetRepo = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Domain.Entities.Budget>>();

                investmentElement.Budget = budgetRepo.Find(investmentElement.BudgetId);

               

                if (investmentElement.Budget == null)
                    throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentElement");

                var eq = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<EquipmentComponent>>();
                investmentElement.Budget.EquipmentComponent = eq.Find(investmentElement.Budget.EquipmentComponentId);
                var cons = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<ConstructionComponent>>();
                investmentElement.Budget.ConstructionComponent = cons.Find(investmentElement.Budget.ConstructionComponentId);

                var oth = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<OtherExpensesComponent>>();
                investmentElement.Budget.OtherExpensesComponent = oth.Find(investmentElement.Budget.OtherExpensesComponentId);

                var wrk = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<WorkCapitalComponent>>();
                investmentElement.Budget.WorkCapitalComponent = wrk.Find(investmentElement.Budget.WorkCapitalComponentId);

                var wrkCF = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<WorkCapitalCashFlow>>();
                investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow = wrkCF.Find(investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlowId);
                investmentElement.Budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow = wrkCF.Find(investmentElement.Budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlowId);

                investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries.Id = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow?.CashEntriesId;
                investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings.Id = investmentElement.Budget.WorkCapitalComponent.WorkCapitalCashFlow?.CashOutgoingsId;

                investmentElement.Budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashEntries.Id = investmentElement.Budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow?.CashEntriesId;
                investmentElement.Budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashOutgoings.Id = investmentElement.Budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow?.CashOutgoingsId;
            }


            investmentElementClone.Budget = Clone(investmentElement.Budget);
            investmentElementClone.Budget.InvestmentElement = investmentElementClone;
            investmentElementClone.BudgetId = investmentElementClone.Budget.Id;

            // adjusting dates
            if (investmentElementClone.EndCalculated)
            {
                investmentElementClone.Budget.EndCalculated = investmentElementClone.EndCalculated;
                investmentElementClone.Budget.LastCalculatedFinishDate = investmentElementClone.LastCalculatedFinishDate;
                investmentElementClone.Budget.EquipmentComponent.EndCalculated = investmentElementClone.EndCalculated;
                investmentElementClone.Budget.EquipmentComponent.LastCalculatedFinishDate = investmentElementClone.LastCalculatedFinishDate;
                investmentElementClone.Budget.ConstructionComponent.EndCalculated = investmentElementClone.EndCalculated;
                investmentElementClone.Budget.ConstructionComponent.LastCalculatedFinishDate = investmentElementClone.LastCalculatedFinishDate;
                 investmentElementClone.Budget.OtherExpensesComponent.EndCalculated = investmentElementClone.EndCalculated;
                investmentElementClone.Budget.OtherExpensesComponent.LastCalculatedFinishDate = investmentElementClone.LastCalculatedFinishDate;
                investmentElementClone.Budget.WorkCapitalComponent.EndCalculated = investmentElementClone.EndCalculated;
                investmentElementClone.Budget.WorkCapitalComponent.LastCalculatedFinishDate = investmentElementClone.LastCalculatedFinishDate;
                
            }

            if (investmentElementClone.StartCalculated)
            {
                investmentElementClone.Budget.StartCalculated = investmentElementClone.StartCalculated;
                investmentElementClone.Budget.LastCalculatedStartDate = investmentElementClone.LastCalculatedStartDate;
                investmentElementClone.Budget.EquipmentComponent.StartCalculated = investmentElementClone.EndCalculated;
                investmentElementClone.Budget.EquipmentComponent.LastCalculatedStartDate = investmentElementClone.LastCalculatedStartDate;
                investmentElementClone.Budget.ConstructionComponent.StartCalculated = investmentElementClone.EndCalculated;
                investmentElementClone.Budget.ConstructionComponent.LastCalculatedStartDate = investmentElementClone.LastCalculatedStartDate;
                investmentElementClone.Budget.OtherExpensesComponent.StartCalculated = investmentElementClone.EndCalculated;
                investmentElementClone.Budget.OtherExpensesComponent.LastCalculatedStartDate = investmentElementClone.LastCalculatedStartDate;
                investmentElementClone.Budget.WorkCapitalComponent.StartCalculated = investmentElementClone.EndCalculated;
                investmentElementClone.Budget.WorkCapitalComponent.LastCalculatedStartDate = investmentElementClone.LastCalculatedStartDate;

            }


            investmentElementClone.Period = Clone(investmentElement.Period);
            investmentElementClone.Period.Holder = investmentElementClone;
            investmentElementClone.PeriodId = investmentElementClone.Period.Id;

            if (isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();

                perDBC.Add(investmentElementClone.Period);
                perDBC.Save();

            }


            return investmentElementClone;
        }

        private void Delete(IBudget budget)
        {
            //equipment
            DeleteComponentParts<IPlannedActivity, IPlannedActivityRepository>(budget.EquipmentComponent);
            DeleteComponentParts<IExecutedActivity, IExecutedActivityRepository>(budget.EquipmentComponent);

            //construction
            DeleteComponentParts<IPlannedActivity, IPlannedActivityRepository>(budget.ConstructionComponent);
            DeleteComponentParts<IExecutedActivity, IExecutedActivityRepository>(budget.ConstructionComponent);

            //others
            DeleteComponentParts<IPlannedActivity, IPlannedActivityRepository>(budget.OtherExpensesComponent);
            DeleteComponentParts<IExecutedActivity, IExecutedActivityRepository>(budget.OtherExpensesComponent);

            //work capital
            DeleteComponentParts<IPlannedActivity, IPlannedActivityRepository>(budget.WorkCapitalComponent);
            DeleteComponentParts<IExecutedActivity, IExecutedActivityRepository>(budget.WorkCapitalComponent);

            Delete<EquipmentComponent>(budget.EquipmentComponent);
            Delete<ConstructionComponent>(budget.ConstructionComponent);
            Delete<OtherExpensesComponent>(budget.OtherExpensesComponent);
            Delete<WorkCapitalComponent>(budget.WorkCapitalComponent);

            var budgetRepo = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Domain.Entities.Budget>>();
            budgetRepo.Delete(budget);
            budgetRepo.Save();
        }
        private void Delete(IPeriod period)
        {
            var periodRepo = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            periodRepo.Delete(period);
        }

        private void DeleteComponentParts<T, TRepository>(IBudgetComponent component)
          where T : class, IActivity
          //where TComponent : class, IBudgetComponent
          where TRepository : IActivityRepository<T>
        {
            var repository = ServiceLocator.Current.GetInstance<IPlannedSubSpecialityHolderRepository>();
            repository.BudgetComponent = component;

            foreach (IPlannedSubSpecialityHolder subSpecialityHolder in repository.Entities)
            {
                var activityRepo = ServiceLocator.Current.GetInstance<TRepository>();
                activityRepo.SubSpecialityHolder = subSpecialityHolder;
                activityRepo.DeleteAll();
            }

            repository.DeleteAll();

            var repositoryEx = ServiceLocator.Current.GetInstance<IExecutedSubSpecialityHolderRepository>();
            repositoryEx.BudgetComponent = component;

            foreach (IExecutedSubSpecialityHolder subSpecialityHolder in repositoryEx.Entities)
            {
                var activityRepo = ServiceLocator.Current.GetInstance<TRepository>();
                activityRepo.SubSpecialityHolder = subSpecialityHolder;
                activityRepo.DeleteAll();
            }

            repositoryEx.DeleteAll();

        }
        //private static void DeleteComponentParts<TElem, TComponent, TRepository>(TComponent component)
        //    where TElem : class, IBudgetComponentItem
        //    where TComponent : class, IBudgetComponent
        //    where TRepository : IPlannedActivityRepository<TComponent>
        //{
        //    var repository = ServiceLocator.Current.GetInstance<TRepository>();
        //    repository.Component = component;
        //    repository.DeleteAll();
        //}
        //private void DeleteComponentPartsExecuted<T, TComponent, TRepository>(TComponent component)
        // where T : class, IBudgetComponentItem
        // where TComponent : class, IBudgetComponent
        // where TRepository : IExecutedActivityRepository<TComponent>
        //{
        //    var repository = ServiceLocator.Current.GetInstance<TRepository>();
        //    repository.Component = component;
        //    repository.DeleteAll();
        //}

        private void Delete<TClassBc>(IBudgetComponent component) where TClassBc : BudgetComponentBase
        {
            // DatabaseContext.Delete((object)component.PlannedResources);
            //  DatabaseContext.Delete((object)component.PlannedActivities);
            //  DatabaseContext.Delete((object)component.ExecutedResources);
            //  DatabaseContext.Delete((object)component.ExecutedActivities);
            var budgetCRepo = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<TClassBc>>();
            var checkList = budgetCRepo.Entities.ToList();
            budgetCRepo.Delete(component);
            budgetCRepo.Save();
        }

        private IBudget Clone(IBudget budget)
        {
            var budgetClone = ServiceLocator.Current.GetInstance<IBudget>();

            var equipmentClone = ServiceLocator.Current.GetInstance<IEquipmentComponent>();
            var constructionClone = ServiceLocator.Current.GetInstance<IConstructionComponent>();
            var otherExpensesClone = ServiceLocator.Current.GetInstance<IOtherExpensesComponent>();
            var workCapitalClone = ServiceLocator.Current.GetInstance<IWorkCapitalComponent>();

            equipmentClone.Budget = budgetClone;
            constructionClone.Budget = budgetClone;
            otherExpensesClone.Budget = budgetClone;
            workCapitalClone.Budget = budgetClone;

            budgetClone.EquipmentComponent = equipmentClone;
            budgetClone.ConstructionComponent = constructionClone;
            budgetClone.OtherExpensesComponent = otherExpensesClone;

            var workCapitalCashFlowClone = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlow>();
            workCapitalCashFlowClone.WorkCapital = workCapitalClone;
            workCapitalClone.WorkCapitalCashFlow = workCapitalCashFlowClone;

            var workCapitalCashFlowClone2 = ServiceLocator.Current.GetInstance<IWorkCapitalCashFlow>();
            workCapitalCashFlowClone2.WorkCapital = workCapitalClone;
            workCapitalClone.ExecutedWorkCapitalCashFlow = workCapitalCashFlowClone2;

            budgetClone.WorkCapitalComponent = workCapitalClone;

            
            budgetClone.Id = budget?.Id ?? DatabaseContext.GenerateKey();

            budgetClone.EquipmentComponent.Id = budget?.EquipmentComponent?.Id ?? DatabaseContext.GenerateKey();
            budgetClone.ConstructionComponent.Id = budget?.ConstructionComponent?.Id ?? DatabaseContext.GenerateKey();
            budgetClone.OtherExpensesComponent.Id = budget?.OtherExpensesComponent?.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.Id = budget?.WorkCapitalComponent.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.Id = budget?.WorkCapitalComponent.WorkCapitalCashFlow.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Id = budget?.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Id ?? DatabaseContext.GenerateKey();
            
            //new domain
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.Starts = budget?.WorkCapitalComponent.WorkCapitalCashFlow.Starts ?? DateTime.Today;
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.Ends = budget?.WorkCapitalComponent.WorkCapitalCashFlow.Ends ?? DateTime.Today;
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Starts = budget?.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Starts ?? DateTime.Today;
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Ends = budget?.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Ends ?? DateTime.Today;

            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.DateTimeScale = budget?.WorkCapitalComponent.WorkCapitalCashFlow.DateTimeScale ?? DateTimeScale.Monthly;
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.DateTimeScale = budget?.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.DateTimeScale ?? DateTimeScale.Monthly;


            // to the strings ids
            budgetClone.EquipmentComponentId = budgetClone.EquipmentComponent.Id;
            budgetClone.ConstructionComponentId = budgetClone.ConstructionComponent.Id;
            budgetClone.OtherExpensesComponentId = budgetClone.OtherExpensesComponent.Id; ;
            budgetClone.WorkCapitalComponentId = budgetClone.WorkCapitalComponent.Id; ;
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlowId = budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.Id;
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlowId = budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Id;
            //for new domain
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries.Id = budget.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings.Id = budget.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashEntries.Id = budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashEntries.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashOutgoings.Id = budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashOutgoings.Id ?? DatabaseContext.GenerateKey();

            //new values
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.IsWorkCapitalCalculated = budget.WorkCapitalComponent.WorkCapitalCashFlow.IsWorkCapitalCalculated;
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.CalculatedWorkCapital = budget.WorkCapitalComponent.WorkCapitalCashFlow.CalculatedWorkCapital;
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.IsWorkCapitalCalculated = budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.IsWorkCapitalCalculated;
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CalculatedWorkCapital = budget.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CalculatedWorkCapital;


            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.CashEntriesId = budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.CashEntries.Id;
            budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoingsId = budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.CashOutgoings.Id;
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashEntriesId = budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashEntries.Id ;
            budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashOutgoingsId = budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.CashOutgoings.Id;

            var budgetRepo = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Domain.Entities.Budget>>();

            var repoBudget = budgetRepo.Find(budgetClone.Id);
            if (repoBudget == null)
            {
                var eq = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<EquipmentComponent>>();
                eq.Add(budgetClone.EquipmentComponent);
                eq.Save();
                var cons = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<ConstructionComponent>>();
                cons.Add(budgetClone.ConstructionComponent);
                cons.Save();

                var oth = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<OtherExpensesComponent>>();
                oth.Add(budgetClone.OtherExpensesComponent);
                oth.Save();


               

                var wrkCF = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<WorkCapitalCashFlow>>();
                wrkCF.Add(budgetClone.WorkCapitalComponent.WorkCapitalCashFlow);
                wrkCF.Add(budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow);
                wrkCF.Save();

                budgetClone.WorkCapitalComponent.WorkCapitalCashFlowId =
                    budgetClone.WorkCapitalComponent.WorkCapitalCashFlow.Id;
                budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlowId =
                    budgetClone.WorkCapitalComponent.ExecutedWorkCapitalCashFlow.Id;


                var wrk = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<WorkCapitalComponent>>();
                wrk.Add(budgetClone.WorkCapitalComponent);
                wrk.Save();


                budgetClone.EquipmentComponentId = budgetClone.EquipmentComponent.Id;
                budgetClone.ConstructionComponentId = budgetClone.ConstructionComponent.Id;
                budgetClone.OtherExpensesComponentId = budgetClone.OtherExpensesComponent.Id; ;
                budgetClone.WorkCapitalComponentId = budgetClone.WorkCapitalComponent.Id; ;

                budgetRepo.Add(budgetClone as Domain.Entities.Budget);

                budgetRepo.Save();
            }

            return budgetClone;
        }
        private IPeriod Clone(IPeriod period)
        {
            var periodClone = ServiceLocator.Current.GetInstance<IPeriod>();

            periodClone.Starts = period.OriStart();
            periodClone.Ends = period.OriEnd();
            periodClone.PeriodKind = period.PeriodKind;

            periodClone.Id = period.Id ?? DatabaseContext.GenerateKey();

            return periodClone;
        }
    }


   
}