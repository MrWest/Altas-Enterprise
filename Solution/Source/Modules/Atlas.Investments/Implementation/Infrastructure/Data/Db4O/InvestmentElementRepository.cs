using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
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
    /// Default implementation of the contract <see cref="IInvestmentElementRepository"/>, representing the contract of the
    /// repository of investment elements. Note: that this implementation targets Db4O Database System.
    /// </summary>
    public class InvestmentElementRepository : Db4ORepositoryBase<IInvestmentElement>, IInvestmentElementRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InvestmentElementRepository"/> given the database context.
        /// </summary>
        /// <param name="databaseContext">
        /// The <see cref="IDb4ODatabaseContext"/> representing the Db4O database context used to carry on with the row data
        /// operations.
        /// </param>
        public InvestmentElementRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }


        /// <summary>
        /// Gets or sets the <see cref="IInvestmentElement"/> being the parent of those handled in the current
        /// <see cref="IInvestmentElementRepository"/>.
        /// </summary>
        public IInvestmentElement Parent { get; set; }
        ///// <summary>
        ///// Gets or sets the time period in the current
        ///// <see cref="IInvestmentElementRepository"/>.
        ///// </summary>
        //public IPeriod Period { get; set; }

        /// <summary>
        /// Gets all the independent investment elements (<see cref="IInvestmentElement"/>) if the there is no parent defined, otherwise
        /// returns the investment element being direct childs of the one defined in the Parent property.
        /// </summary>
        public override IEnumerable<IInvestmentElement> Entities
        {
            get
            {
                var specification = Parent != null
                    ? (ISpecification<IInvestmentElement>)new InvestmentElementsOfSpecification(Parent)
                    : new RootInvestmentElementsSpecification();

                return Where(specification);
            }
        }

        /// <summary>
        /// Gets the properties of the investment elements to save its values.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Name), GetName(x => x.Description) }
                ).ToArray();
            }
        }


        /// <summary>
        /// Adds to the current repository the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement"/> does not have an <see cref="IBudget"/>.</exception>
        public override IInvestmentElement Add(IInvestmentElement investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.InvestmentElementWithoutBudget, "investmentElement");

            IInvestmentElement addedInvestmentElement = base.Add(investmentElement);
            investmentElement.Budget = addedInvestmentElement.Budget;
            investmentElement.Period = addedInvestmentElement.Period;

           
            this.Relate(investmentElement, Parent, DatabaseContext);

            this.Relate(investmentElement, investmentElement.Budget, DatabaseContext);
            this.Relate(investmentElement, investmentElement.Period, DatabaseContext);

            return addedInvestmentElement;
        }

        /// <summary>
        /// Updates the changes there were made to the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement"/> does not have an <see cref="IBudget"/>.</exception>
        public override void Update(IInvestmentElement investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.InvestmentElementWithoutBudget, "investmentElement");

        

          
            this.Relate(investmentElement, Parent, DatabaseContext);

            this.Relate(investmentElement, investmentElement.Budget, DatabaseContext);
            this.Relate(investmentElement, investmentElement.Period, DatabaseContext);
        }

        /// <summary>
        /// Deletes the given investment element from the repository.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="investmentElement"/> has a null budget.</exception>
        public override void Delete(IInvestmentElement investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (investmentElement.Budget == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "investmentElement");

            if (Parent != null)
                this.Unrelate(investmentElement, Parent, DatabaseContext);

            this.Unrelate(investmentElement, investmentElement.Budget, DatabaseContext);
            this.Unrelate(investmentElement, investmentElement.Period, DatabaseContext);

            var dbInvestmentElement = DatabaseContext.Find<IInvestmentElement>(investmentElement.Id);
            foreach (IInvestmentElement child in dbInvestmentElement.Elements.ToArray())
                Delete(child);

            DatabaseContext.Delete((object)dbInvestmentElement.Elements);
            DatabaseContext.Delete(dbInvestmentElement);
        }

        /// <summary>
        /// Relates an investment element to its parent, following the rule that an investment element can be part of another one.
        /// </summary>
        /// <param name="investmentElement">
        /// The <see cref="IInvestmentElement"/> to relate with <paramref name="parentInvestmentElement"/> as a child of it.
        /// </param>
        /// <param name="parentInvestmentElement">
        /// The other <see cref="IInvestmentElement"/> to be the parent of <paramref name="investmentElement"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="parentInvestmentElement"/> is null.
        /// </exception>
        public void Relate(IInvestmentElement investmentElement, IInvestmentElement parentInvestmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (parentInvestmentElement == null)
                throw new ArgumentNullException("parentInvestmentElement");

          //  investmentElement.Parent = parentInvestmentElement;
            if (parentInvestmentElement.Elements.All(x => !Equals(x.Id, investmentElement.Id)))
                parentInvestmentElement.Elements.Add(investmentElement);
        }

        /// <summary>
        /// Disrelates the given investment element from its parent.
        /// </summary>
        /// <param name="investmentElement">
        /// The <see cref="IInvestmentElement"/> to relate with its parent which is <paramref name="parentInvestmentElement"/>.
        /// </param>
        /// <param name="parentInvestmentElement">
        /// The <see cref="IInvestmentElement"/> to become parent of <paramref name="investmentElement"/>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="parentInvestmentElement"/> is null.
        /// </exception>
        public void Unrelate(IInvestmentElement investmentElement, IInvestmentElement parentInvestmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (parentInvestmentElement == null)
                throw new ArgumentNullException("parentInvestmentElement");

           // investmentElement.Parent = null;
            if (parentInvestmentElement.Elements.Any(x => Equals(x.Id, investmentElement.Id)))
                parentInvestmentElement.Elements.Remove(investmentElement);
        }

        /// <summary>
        /// Relates the given investment element with a budget, following the domain specification that every investment element must have a
        /// budget.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="budget"/>.</param>
        /// <param name="budget">The <see cref="IBudget"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="budget"/> is null.
        /// </exception>
        public void Relate(IInvestmentElement investmentElement, IBudget budget)
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
        public void Relate(IInvestmentElement investmentElement, IPeriod period)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (period == null)
                throw new ArgumentNullException("period");

            investmentElement.Period = period;
            period.Holder = investmentElement;
        }
        /// <summary>
        /// Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to break its relation with <paramref name="budget"/>.</param>
        /// <param name="budget">The <see cref="IBudget"/> to break its relation with <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="budget"/> is null.
        /// </exception>
        public void Unrelate(IInvestmentElement investmentElement, IBudget budget)
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
        /// Disrelates the given investment element with a budget.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to break its relation with <paramref name="budget"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to break its relation with <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Unrelate(IInvestmentElement investmentElement, IPeriod period)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");
            if (period == null)
                throw new ArgumentNullException("period");

            investmentElement.Budget = null;
            period.Holder = null;

            Delete(period);
        }

        /// <summary>
        /// Saves the references of an investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to save its references.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        public void SaveReference(IInvestmentElement investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");

            if (Equals(investmentElement.Id, default(Guid)))
                return;

            DatabaseContext.Store(investmentElement);
            DatabaseContext.Store(investmentElement.Elements);
        }

        /// <summary>
        /// Saves the references of a budget.
        /// </summary>
        /// <param name="budget">The <see cref="IBudget"/> to save its references.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budget"/> is null.</exception>
        public void SaveReference(IBudget budget)
        {
          //  DatabaseContext.Store(budget);
        }
        /// <summary>
        /// Saves the references of a budget.
        /// </summary>
        /// <param name="period">The <see cref="IPeriod"/> to save its references.</param>
        /// <exception cref="ArgumentNullException"><paramref name="period"/> is null.</exception>
        public void SaveReference(IPeriod period)
        {
           // DatabaseContext.Store(period);
        }
        /// <summary>
        /// Makes a clone of the given investment element.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to make a clone of.</param>
        /// <exception cref="ArgumentNullException"><paramref name="investmentElement"/> is null.</exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="investmentElement"/> has a null budget when not deleting an investment element.
        /// </exception>
        /// <returns>
        /// A clone of <paramref name="investmentElement"/> with all the relevant data to this repository copied into the clone.
        /// </returns>
        protected override IInvestmentElement Clone(IInvestmentElement investmentElement)
        {
            if (investmentElement == null)
                throw new ArgumentNullException("investmentElement");

            IInvestmentElement investmentElementClone = base.Clone(investmentElement);

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
            var perDBC = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            perDBC.Delete(period);
        }
        private void DeleteComponentParts<T, TRepository>(IBudgetComponent component)
            where T : class, IActivity
            //where TComponent : class, IBudgetComponent
            where TRepository : IActivityRepository<T>
        {
            var repository = ServiceLocator.Current.GetInstance<IPlannedSubSpecialityHolderRepository>();
            repository.BudgetComponent = component;

            foreach ( IPlannedSubSpecialityHolder subSpecialityHolder in repository.Entities)
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
        //private void DeleteComponentPartsExecuted<T, TComponent, TRepository>(TComponent component)
        //   where T : class, IBudgetComponentItem
        //   where TComponent : class, IBudgetComponent
        //   where TRepository : IExecutedActivityRepository<TComponent>
        //{
        //    var repository = ServiceLocator.Current.GetInstance<TRepository>();
        //    repository.Component = component;
        //    repository.DeleteAll();
        //}

        private void Delete(IBudgetComponent component)
        {
           // DatabaseContext.Delete((object)component.PlannedResources);
            DatabaseContext.Delete((object)component.PlannedActivities);
           // DatabaseContext.Delete((object)component.ExecutedResources);
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
            budgetClone.WorkCapitalComponent = workCapitalClone;

            budgetClone.Id = budget.Id ?? DatabaseContext.GenerateKey();

            budgetClone.EquipmentComponent.Id = budget.EquipmentComponent.Id ?? DatabaseContext.GenerateKey();
            budgetClone.ConstructionComponent.Id = budget.ConstructionComponent.Id ?? DatabaseContext.GenerateKey();
            budgetClone.OtherExpensesComponent.Id = budget.OtherExpensesComponent.Id ?? DatabaseContext.GenerateKey();
            budgetClone.WorkCapitalComponent.Id = budget.WorkCapitalComponent.Id ?? DatabaseContext.GenerateKey();

            return budgetClone;
        }
        private IPeriod Clone(IPeriod period)
        {
            var periodClone = ServiceLocator.Current.GetInstance<IPeriod>();
            periodClone.Starts = period.Starts;
            periodClone.Ends = period.Ends;
            periodClone.PeriodKind = period.PeriodKind;

            

            periodClone.Id = period.Id ?? DatabaseContext.GenerateKey();

           

            return periodClone;
        }
    }
}
