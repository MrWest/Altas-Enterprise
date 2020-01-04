using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public class ExecutionRepository : Db4ORepositoryBase<IExecution>, IExecutionRepository
    {
        private IExecutedActivity _activity;

        public ExecutionRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public IExecutedActivity ExecutedActivity { get { return _activity; } set { _activity = value; } }

        /// <summary>
        ///     Gets all the public properties non-readonly properties that are relevant to the current repository when making its
        ///     operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Date), GetName(x => x.Description), GetName(x => x.Amount)
                }).ToArray();
            }
        }

        /// <summary>
        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<IExecution> Entities
        {
            get
            {
                var specification = new ExecutionOfSpecification(ExecutedActivity);

                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IExecution Add(IExecution execution)
        {
            IExecution addedExecution = base.Add(execution);

            this.Relate(execution, ExecutedActivity, DatabaseContext);

            return execution;
        }
        public void Delete(IExecution execution)
        {
            if (execution == null)
                throw new ArgumentNullException("execution");

            this.Unrelate(execution, ExecutedActivity, DatabaseContext);

            var dbExecution = DatabaseContext.Find<IExecution>(execution.Id);

            DatabaseContext.Delete(dbExecution);
        }

      

       
        /// <summary>
        /// Relates a item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public virtual void Relate(IExecution execution, IExecutedActivity executedActivity)
        {
            if (execution == null)
                throw new ArgumentNullException("section");
            if (executedActivity == null)
                throw new ArgumentNullException("priceSystem");

            execution.ExecutedActivity = executedActivity;
           
        }

        /// <summary>
        /// Breaks the relation of the given item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to break its relation to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to break its relation to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public virtual void Unrelate(IExecution execution, IExecutedActivity executedActivity)
        {
            if (execution == null)
                throw new ArgumentNullException("execution");
            if (executedActivity == null)
                throw new ArgumentNullException("executedActivity");

             execution.ExecutedActivity = null;
           
        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public virtual void SaveReference(IExecution execution)
        {
            if (execution == null)
                throw new ArgumentNullException("execution");

            DatabaseContext.Store(execution);
        }

        public void SaveReference(IExecutedActivity other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Store(other);
        }
    }

    public class ExecutionRepositoryEF : EntityFrameworkRepositoryBase<IExecution, Execution>, IExecutionRepository
    {
        private IExecutedActivity _activity;

        public ExecutionRepositoryEF(IEntityFrameworkDbContext<Execution> databaseContext) : base(databaseContext)
        {
        }

        public IExecutedActivity ExecutedActivity { get { return _activity; } set { _activity = value; } }

        /// <summary>
        ///     Gets all the public properties non-readonly properties that are relevant to the current repository when making its
        ///     operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Date), GetName(x => x.Description), GetName(x => x.Amount), GetName(x => x.ExecutedActivityId)
                }).ToArray();
            }
        }

        /// <summary>
        ///     Gets all the independent investment elements (<see cref="IInvestmentComponent" />) if the there is no parent
        ///     defined, otherwise returns the investment element being direct childs of the one defined in the InvestmentElement property.
        /// </summary>
        public override IEnumerable<IExecution> Entities
        {
            get
            {
                var specification = new ExecutionOfQueryable(ExecutedActivity, DatabaseContext);

                return Where(specification);
            }
        }

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IExecution Add(IExecution execution)
        {
            this.Relate(execution, ExecutedActivity);
            IExecution addedExecution = base.Add(execution);

            

            return execution;
        }

        public override void Update(IExecution execution)
        {
            this.Relate(execution, ExecutedActivity);
            base.Update(execution);
        }

        public void Delete(IExecution execution)
        {
            if (execution == null)
                throw new ArgumentNullException("execution");

            this.Unrelate(execution, ExecutedActivity);

            base.Delete(execution);
        }




        /// <summary>
        /// Relates a item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public virtual void Relate(IExecution execution, IExecutedActivity executedActivity)
        {
            if (execution == null)
                throw new ArgumentNullException("section");
            if (executedActivity == null)
                throw new ArgumentNullException("priceSystem");

            execution.ExecutedActivity = executedActivity;
            execution.ExecutedActivityId = executedActivity.Id;

        }

        /// <summary>
        /// Breaks the relation of the given item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to break its relation to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to break its relation to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public virtual void Unrelate(IExecution execution, IExecutedActivity executedActivity)
        {
            if (execution == null)
                throw new ArgumentNullException("execution");
            if (executedActivity == null)
                throw new ArgumentNullException("executedActivity");

            execution.ExecutedActivity = null;
            execution.ExecutedActivityId = null;

        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public virtual void SaveReference(IExecution execution)
        {
            if (execution == null)
                throw new ArgumentNullException("execution");

            DatabaseContext.Save();
        }

        public void SaveReference(IExecutedActivity other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            DatabaseContext.Save();
        }
    }
}
