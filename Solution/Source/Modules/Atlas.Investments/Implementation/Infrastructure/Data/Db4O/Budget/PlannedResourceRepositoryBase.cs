using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget.Equipment;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    /// <summary>
    /// Base class of the repository managing the data operations related to the planned resources of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the planned resources.</typeparam>

    public  class PlannedResourceRepositoryBase<TComponent> :
        BudgetComponentResourceRepositoryBase<TComponent>, IBudgetComponentResourceRepository<TComponent>,
         IRelatedRepository<IPlannedResource, TComponent>
        where TComponent : class, IBudgetComponentItem
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentPlannedResourceRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public PlannedResourceRepositoryBase(IDb4ODatabaseContext databaseContext)
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
                    GetName(x => x.Norm)
                }).ToArray();
            }
        }

        public override IPlannedResource Add(IPlannedResource budgetComponentItem)
        {
            IPlannedResource addedBudgetComponentITem = base.Add(budgetComponentItem);

            budgetComponentItem.Weight = addedBudgetComponentITem.Weight;
            budgetComponentItem.Volume = addedBudgetComponentITem.Volume;
            budgetComponentItem.Period = addedBudgetComponentITem.Period;

            this.Relate(budgetComponentItem, Component, DatabaseContext);
            this.Relate(budgetComponentItem, budgetComponentItem.Period, DatabaseContext);
            this.Relate(budgetComponentItem, budgetComponentItem.Weight, DatabaseContext);
            this.Relate(budgetComponentItem, budgetComponentItem.Volume, DatabaseContext);

            return addedBudgetComponentITem;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IPlannedResource budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            //this.Relate(budgetComponentItem, Component, DatabaseContext);
            this.Relate(budgetComponentItem, budgetComponentItem.Period, DatabaseContext);
            this.Relate(budgetComponentItem, budgetComponentItem.Weight, DatabaseContext);
            this.Relate(budgetComponentItem, budgetComponentItem.Volume, DatabaseContext);
        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(IPlannedResource budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");


            //this.Unrelate(budgetComponentItem, Component, DatabaseContext);
            this.Unrelate(budgetComponentItem, budgetComponentItem.Period, DatabaseContext);
            this.Unrelate(budgetComponentItem, budgetComponentItem.Weight, DatabaseContext);
            this.Unrelate(budgetComponentItem, budgetComponentItem.Volume, DatabaseContext);
            base.Delete(budgetComponentItem);
        }

        /// <summary>
        /// Gets the collection of the planned resources in the budget component in which it will be contained the items managed
        /// in the current <see cref="PlannedResourceRepositoryBase{TComponent}"/>.
        /// </summary>
        ////protected override Func<TComponent, IList<IPlannedResource>> GetItemCollection
        ////{
        ////    get { return x => x.PlannedResources; }
        ////}
    }

    /// <summary>
    /// Base class of the repository managing the data operations related to the planned resources of a certain budget component.
    /// </summary>
    /// <typeparam name="TComponent">The type of the budget component to which belong the planned resources.</typeparam>

    public class PlannedResourceRepositoryBaseEF<TComponent> :
        BudgetComponentResourceRepositoryBaseEF<TComponent,PlannedResource>, IBudgetComponentResourceRepository<TComponent>
        where TComponent : class, IBudgetComponentItem
    {
        /// <summary>
        /// Initializes a new instance of <see cref="EquipmentPlannedResourceRepository"/> with a
        /// <see cref="IDb4ODatabaseContext"/>.
        /// </summary>
        /// <param name="databaseContext">
        /// The instance of <see cref="IDb4ODatabaseContext"/> that carries on the actual raw data operations the
        /// initializing repository performs.
        /// </param>
        public PlannedResourceRepositoryBaseEF(IEntityFrameworkDbContext<PlannedResource> databaseContext)
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
                    GetName(x => x.Norm)
                }).ToArray();
            }
        }

        public override IPlannedResource Add(IPlannedResource budgetComponentItem)
        {
            this.Relate(budgetComponentItem, Component);
            IPlannedResource addedBudgetComponentITem = base.Add(budgetComponentItem);

            budgetComponentItem.Weight = addedBudgetComponentITem.Weight;
            budgetComponentItem.Volume = addedBudgetComponentITem.Volume;
            budgetComponentItem.Period = addedBudgetComponentITem.Period;
            budgetComponentItem.WeightId = addedBudgetComponentITem.WeightId;
            budgetComponentItem.VolumeId = addedBudgetComponentITem.VolumeId;
            budgetComponentItem.PeriodId = addedBudgetComponentITem.PeriodId;

            //  this.Relate(budgetComponentItem, Component);
            //this.Relate(budgetComponentItem, budgetComponentItem.Period);
            //this.Relate(budgetComponentItem, budgetComponentItem.Weight);
            //this.Relate(budgetComponentItem, budgetComponentItem.Volume);

            return addedBudgetComponentITem;
        }

        private void Relate(IPlannedResource budgetComponentItem, TComponent component)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");
            budgetComponentItem.Component = component;
            budgetComponentItem.ComponentId = component.Id;

        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Update(IPlannedResource budgetComponentItem)
        {
            base.Update(budgetComponentItem);

            //this.Relate(budgetComponentItem, Component, DatabaseContext);
            this.Relate(budgetComponentItem, budgetComponentItem.Period);
            this.Relate(budgetComponentItem, budgetComponentItem.Weight);
            this.Relate(budgetComponentItem, budgetComponentItem.Volume);
        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void Delete(IPlannedResource budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            var resourceRepo = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IPlannedResource>>();
            resourceRepo.Component = budgetComponentItem;
            resourceRepo.DeleteAll();

            //this.Unrelate(budgetComponentItem, Component, DatabaseContext);
            this.Unrelate(budgetComponentItem, budgetComponentItem.Period);
            this.Unrelate(budgetComponentItem, budgetComponentItem.Weight);
            this.Unrelate(budgetComponentItem, budgetComponentItem.Volume);
            base.Delete(budgetComponentItem);
        }

        /// <summary>
        /// Gets the collection of the planned resources in the budget component in which it will be contained the items managed
        /// in the current <see cref="PlannedResourceRepositoryBase{TComponent}"/>.
        /// </summary>
        ////protected override Func<TComponent, IList<IPlannedResource>> GetItemCollection
        ////{
        ////    get { return x => x.PlannedResources; }
        ////}
    }

}
