using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    /// <summary>
    /// Base class of the repository managing the data operations related to the planned activities of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the planned activities.</typeparam>
    public class PlannedActivityRepositoryBase :
        ActivityRepositoryBase<IPlannedActivity>,
        IPlannedActivityRepository,
         IRelatedRepository<IPlannedActivity, ISubSpecialityHolder>
        //where TComponent : class, IBudgetComponent
    {

        

        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentPlannedActivityRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public PlannedActivityRepositoryBase(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
               {
                    GetName(x=>x.Execution)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }
        /// <summary>
        /// Gets the collection of the planned activities in the budget component in which it will be contained the items managed
        /// in the current <see cref="PlannedActivityRepositoryBase{TComponent}"/>.
        /// </summary>
        //protected override Func<TComponent, IList<IPlannedActivity>> GetItemCollection
        //{
        //    get { return x => x.PlannedActivities; }
        //}

        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override IPlannedActivity Add(IPlannedActivity budgetComponentItem)
        {
            IPlannedActivity addedBudgetComponentITem = base.Add(budgetComponentItem);

            budgetComponentItem.Period = addedBudgetComponentITem.Period;



            this.Relate(budgetComponentItem, budgetComponentItem.Period, DatabaseContext);
            this.Relate(budgetComponentItem, SubSpecialityHolder, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IPlannedActivity budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            this.Relate(budgetComponentItem, SubSpecialityHolder, DatabaseContext);
            this.Relate(budgetComponentItem, budgetComponentItem.Period, DatabaseContext);
        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(IPlannedActivity budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            this.Unrelate(budgetComponentItem, SubSpecialityHolder, DatabaseContext);
            this.Unrelate(budgetComponentItem, budgetComponentItem.Period, DatabaseContext);
            base.Delete(budgetComponentItem);
        }

        /// <summary>
        /// Gets the collection of the planned activities in the budget component in which it will be contained the items managed
        /// in the current <see cref="PlannedActivityRepositoryBase{TComponent}"/>.
        /// </summary>
        //protected override Func<IPlannedSubSpecialityHolder, IList<IPlannedActivity>> GetItemCollection
        //{
        //    get { return x => x.PlannedActivities; }
        //}
    }

    /// <summary>
    /// Base class of the repository managing the data operations related to the planned activities of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the planned activities.</typeparam>
    public class PlannedActivityRepositoryBaseEF :
            ActivityRepositoryBaseEF<IPlannedActivity, PlannedActivity>,
            IPlannedActivityRepository,
            IRelatedRepository<IPlannedActivity, ISubSpecialityHolder>
        //where TComponent : class, IBudgetComponent
    {



        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentPlannedActivityRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public PlannedActivityRepositoryBaseEF(IEntityFrameworkDbContext<PlannedActivity> databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        /// Gets the properties which changes must be saved.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Execution)
                }).ToArray();
                //,GetName(x=>x.MeasurementUnit),GetName(x=>x.Currency),GetName(x=>x.Category),GetName(x=>x.ExpenseConcept)
            }
        }
    }
}
