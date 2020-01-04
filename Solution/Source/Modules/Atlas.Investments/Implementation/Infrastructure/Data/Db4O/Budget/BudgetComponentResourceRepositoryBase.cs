using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O.Budget
{
    /// <summary>
    /// Implements a resource repository contract
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public abstract  class BudgetComponentResourceRepositoryBase<TComponent> : BudgetComponentItemRepositoryBase<IPlannedResource>,
        IRelatedRepository<IPlannedResource, IWeight>, IRelatedRepository<IPlannedResource, IVolume>
        where TComponent : class, IBudgetComponentItem
    {
        private TComponent _component;
        public TComponent Component
        {
            get
            {
                if (_component == null)
                    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

                return _component;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _component = value;
            }
        }
        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> given the database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext"/> used to carry on with the raw data operations.</param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>
        public BudgetComponentResourceRepositoryBase(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IPlannedResource> Entities
        {
            get
            {
                if (Component.GetType().Implements(typeof(IVariantLinesHolder)))
                {
                   
                    return base.Entities;
                }

                ISpecification<IPlannedResource> specification = new BudgetComponentResourceOfSpecification<IPlannedResource, TComponent>(Component);

                return Where(specification);
            }
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
                    GetName(x => x.Norm),
                    GetName(x => x.WasteCoefficient),
                    GetName(x => x.WageScale),
                    GetName(x => x.MenNumber),
                    GetName(x => x.ResourceKind),
                    GetName(x => x.Provider),
                    GetName(x => x.Supplier)

                }).ToArray();
            }
        }
        ///// <summary>
        ///// Gets the collection of the planned activities in the budget component in which it will be contained the items managed
        ///// in the current <see cref="PlannedActivityRepositoryBase{TComponent}"/>.
        ///// </summary>
        //protected override Func<TComponent, IList<IPlannedResource>> GetItemCollection
        //{
        //    get { return x => x.PlannedResources; }
        //}
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponentItem">The <see cref="IBudgetComponentItem"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
       
        /// <summary>
        /// Relates a item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to relate to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to relate to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public  void Relate(IPlannedResource budgetComponentItem, TComponent budgetComponent)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

             budgetComponentItem.Component = budgetComponent;
            //var itemCollection = GetItemCollection(budgetComponent);
            //if (itemCollection.All(x => x!=null&&!Equals(x.Id, budgetComponentItem.Id)))
            //    itemCollection.Add(budgetComponentItem);
        }

        /// <summary>
        /// Breaks the relation of the given item with its budget component.
        /// </summary>
        /// <param name="budgetComponentItem">The item to break its relation to <paramref name="budgetComponent"/>.</param>
        /// <param name="budgetComponent">The budget component to break its relation to <paramref name="budgetComponentItem"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="budgetComponentItem"/> or <paramref name="budgetComponent"/> is null.
        /// </exception>
        public void Unrelate(IPlannedResource budgetComponentItem, TComponent budgetComponent)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

             budgetComponentItem.Component = null;
            //IList<IPlannedResource> itemCollection = GetItemCollection(budgetComponent);
            //if (itemCollection.Any(x => Equals(x.Id, budgetComponentItem.Id)))
            //    itemCollection.Remove(budgetComponentItem);
        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void SaveReference(IPlannedResource budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            DatabaseContext.Store(budgetComponentItem);
        }

        /// <summary>
        /// Saves the changes made to the references of the given budget component.
        /// </summary>
        /// <param name="budgetComponent">The budget component which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponent"/> is null.</exception>
        public void SaveReference(TComponent budgetComponent)
        {
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

            //IList<IPlannedResource> itemCollection = GetItemCollection(budgetComponent);
            //DatabaseContext.Store(itemCollection);

            DatabaseContext.Store(budgetComponent);
        }

        protected override IPlannedResource Clone(IPlannedResource plannedResource)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");

            IPlannedResource plannedResourceClone = base.Clone(plannedResource);

            if (plannedResource.Weight == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedResourceWeight");
            if (plannedResource.Volume == null)
                throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedResourceVolume");
            if (plannedResource.Period == null)
            {
                plannedResource.Period = ServiceLocator.Current.GetInstance<IPeriod>();
              //  throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedActivityPeriod");


            }


            plannedResourceClone.Weight = Clone(plannedResource.Weight);
           // plannedResourceClone.Weight.Holder = plannedResourceClone;
            plannedResourceClone.Volume = Clone(plannedResource.Volume);
            // plannedResourceClone.Volume.Holder = plannedResourceClone;




            plannedResourceClone.Period = Clone(plannedResource.Period);
            plannedResourceClone.Period.Holder = plannedResourceClone;

            return plannedResourceClone;
        }


        private IWeight Clone(IWeight weight)
        {
            var weightClone = ServiceLocator.Current.GetInstance<IWeight>();

            weightClone.MeasurementUnit = weight.MeasurementUnit;
            weightClone.Amount = weight.Amount;


            weightClone.Id = weight.Id ?? DatabaseContext.GenerateKey();

            return weightClone;
        }
        private IVolume Clone(IVolume volume)
        {
            var volumeClone = ServiceLocator.Current.GetInstance<IVolume>();

            volumeClone.MeasurementUnit = volume.MeasurementUnit;
            volumeClone.Amount = volume.Amount;


            volumeClone.Id = volume.Id ?? DatabaseContext.GenerateKey();

            return volumeClone;
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

        /// Relates the given investment element with a period, following the domain specification that every investment element must have a
        /// time period.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="period"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Relate(IPlannedResource plannedResource, IWeight weight)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedActivity");
            if (weight == null)
                throw new ArgumentNullException("weight");

            plannedResource.Weight = weight;

            var wieghtRepo = ServiceLocator.Current.GetInstance<IMeasurableUnitRepository<IWeight>>();
            wieghtRepo.Update(weight);
            // weight.Holder = plannedResource;
        }
        public void Relate(IPlannedResource plannedResource, IVolume volume)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedActivity");
            if (volume == null)
                throw new ArgumentNullException("volume");

            plannedResource.Volume = volume;
            var volumeRepo = ServiceLocator.Current.GetInstance<IMeasurableUnitRepository<IVolume>>();
            volumeRepo.Update(volume);
            // volume.Holder = plannedResource;
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
        public void Unrelate(IPlannedResource plannedResource, IWeight weight)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");
            if (weight == null)
                throw new ArgumentNullException("weight");
            var weightRepo = ServiceLocator.Current.GetInstance<IMeasurableUnitRepository<IWeight>>();
           // weightRepo.Holder = plannedResource;
            weightRepo.Delete(weight);
           // Delete(weight);

          //  plannedResource.Weight = null;
           // weight.Holder = null;

        }
        public void Unrelate(IPlannedResource plannedResource, IVolume volume)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");
            if (volume == null)
                throw new ArgumentNullException("volume");

            var volumeRepo = ServiceLocator.Current.GetInstance<IMeasurableUnitRepository<IVolume>>();
           // volumeRepo.Holder = plannedResource;
            volumeRepo.Delete(volume);
           // Delete(volume);

         //   plannedResource.Volume = null;
          //  volume.Holder = null;

        }

        public void SaveReference(IWeight other)
        {
         //   DatabaseContext.Store(other);
        }
        public void SaveReference(IVolume other)
        {
         //   DatabaseContext.Store(other);
        }
        public override IEnumerable<IPlannedResource> FilterByName(string nameSpecification)
        {
            var itemsOfComponent = new BudgetComponentResourceOfSpecification<IPlannedResource, TComponent>(Component);
            var itemsWithName = new NomenclatorHavingInItsNameSpecification<IPlannedResource>(nameSpecification);
            var query = itemsOfComponent & itemsWithName;

            return DatabaseContext.Where(query).ToArray();
        }
    }


    /// <summary>
    /// Implements a resource repository contract
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    public abstract class BudgetComponentResourceRepositoryBaseEF<TComponent, TClass> : BudgetComponentItemRepositoryBaseEF<IPlannedResource, TClass>,
        IRelatedRepository<IPlannedResource, IWeight>, IRelatedRepository<IPlannedResource, IVolume>
        where TComponent : class, IBudgetComponentItem
        where TClass : PlannedResource
        
    {
        private TComponent _component;
        public TComponent Component
        {
            get
            {
                if (_component == null)
                    throw new InvalidOperationException(Resources.InitializeComponentReferenceBeforeUsingIt);

                return _component;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _component = value;
            }
        }
        /// <summary>
        /// Initializes a new instance of <see cref="BudgetComponentItemRepositoryBase{T, TComponent}"/> given the database context.
        /// </summary>
        /// <param name="databaseContext">The <see cref="IDb4ODatabaseContext"/> used to carry on with the raw data operations.</param>
        /// <exception cref="ArgumentNullException"><paramref name="databaseContext"/> is null.</exception>
        public BudgetComponentResourceRepositoryBaseEF(IEntityFrameworkDbContext<TClass> databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<IPlannedResource> Entities
        {
            get
            {
                //if (Component.GetType().Implements(typeof(IVariantLinesHolder)))
                //{

                //    return base.Entities;
                //}

              

                var specification = new BudgetComponentResourceOfQueryable<TClass,TComponent>(Component, DatabaseContext);

                //DBControlSQL dbControl =  new DBControlSQL();
                //dbControl.Load(DatabaseContext.DbConnectionString);
                //var rstl = dbControl.SelectQuerryFixed(specification.SQL).Tables[0].Rows;

                return WhereSQL(specification);
            }
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
                    GetName(x => x.Norm),
                    GetName(x => x.WasteCoefficient),
                    GetName(x => x.WageScale),
                    GetName(x => x.MenNumber),
                    GetName(x => x.ResourceKind),
                    GetName(x => x.Provider),
                    GetName(x => x.Supplier),
                    GetName(x => x.ComponentId),
                    GetName(x => x.WeightId),
                    GetName(x => x.VolumeId)

                }).ToArray();
            }
        }
       
        /// <summary>
        /// Saves the changes made to the references of the given budget component item.
        /// </summary>
        /// <param name="budgetComponentItem">The budget component item which references will be saved.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponentItem"/> is null.</exception>
        public override void SaveReference(IPlannedResource budgetComponentItem)
        {
            if (budgetComponentItem == null)
                throw new ArgumentNullException("budgetComponentItem");

            DatabaseContext.Save();
        }

       
        protected override IPlannedResource Clone(IPlannedResource plannedResource)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");

            IPlannedResource plannedResourceClone = base.Clone(plannedResource);

            if (plannedResource.WeightId == null)
            {
                var weiDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Weight>>();
                if (plannedResource.Weight != null)
                {
                    weiDBC.Add(plannedResource.Weight);
                    weiDBC.Save();
                }
                  
            }
        
            if (plannedResource.Weight == null)
            {
                var weiDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Weight>>();
               
                if (plannedResource.WeightId != null)
                {
                    plannedResource.Weight = weiDBC.Find(plannedResource.WeightId);
                }

            }
            if (plannedResource.VolumeId == null)
            {
                var volDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Volume>>();
                if (plannedResource.Volume != null)
                {
                    volDBC.Add(plannedResource.Volume);
                    volDBC.Save();
                }

            }

            if (plannedResource.Volume == null)
            {
                var volDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Volume>>();
                if (plannedResource.VolumeId != null)
                {
                    plannedResource.Volume = volDBC.Find(plannedResource.VolumeId);
                }

            }

            if (plannedResource.Weight == null)
            {
                plannedResource.Weight = ServiceLocator.Current.GetInstance<IWeight>();
                // throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedResourceWeight");
            }

            if (plannedResource.Volume == null)
            {
                plannedResource.Volume = ServiceLocator.Current.GetInstance<IVolume>();
                // throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedResourceVolume");
            }
            if (plannedResource.Period == null)
            {
                plannedResource.Period = ServiceLocator.Current.GetInstance<IPeriod>();
                //  throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedActivityPeriod");


            }

            if (!isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();
                var realPeriod = perDBC.Find(plannedResource.PeriodId);
                if(realPeriod!=null)
                plannedResource.Period = realPeriod;
              

            }

            plannedResourceClone.Weight = Clone(plannedResource.Weight);
            plannedResourceClone.WeightId = plannedResourceClone.Weight.Id;
            plannedResourceClone.Volume = Clone(plannedResource.Volume);
            plannedResourceClone.VolumeId = plannedResourceClone.Volume.Id;




            plannedResourceClone.Period = Clone(plannedResource.Period);
            plannedResourceClone.Period.Holder = plannedResourceClone;
            plannedResourceClone.Period.HolderId = plannedResourceClone.Id;
            plannedResourceClone.PeriodId = plannedResourceClone.Period.Id;

            if (isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();

                perDBC.Add(plannedResourceClone.Period);
                perDBC.Save();
                plannedResourceClone.Period = Clone(plannedResourceClone.Period);
                plannedResourceClone.Period.Holder = plannedResourceClone;
                plannedResourceClone.Period.HolderId = plannedResourceClone.Id;
                plannedResourceClone.PeriodId = plannedResourceClone.Period.Id;
            }

            plannedResourceClone.Component = Component;

            return plannedResourceClone;
        }

        protected override TClass Clone(TClass plannedResource)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");

            IPlannedResource plannedResourceClone = base.Clone(plannedResource);

            if (plannedResource.WeightId == null)
            {
                var weiDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Weight>>();
                if (plannedResource.Weight != null)
                {
                    weiDBC.Add(plannedResource.Weight);
                    weiDBC.Save();
                }

            }
            if (plannedResource.VolumeId == null)
            {
                var volDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Volume>>();
                if (plannedResource.Volume != null)
                {
                    volDBC.Add(plannedResource.Volume);
                    volDBC.Save();
                    //var list = volDBC.Entities.ToList();
                }

            }

            if (plannedResource.Weight == null)
            {
                var weiDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Weight>>();

                if (plannedResource.WeightId != null)
                {
                    plannedResource.Weight = weiDBC.Find(plannedResource.WeightId);
                }

            }
            if (plannedResource.Volume == null)
            {
                var volDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Volume>>();
                if (plannedResource.VolumeId != null)
                {
                    //var list = volDBC.Entities.ToList();
                    plannedResource.Volume = volDBC.Find(plannedResource.VolumeId);
                }

            }

            if (plannedResource.Weight == null)
            {
                plannedResource.Weight = ServiceLocator.Current.GetInstance<IWeight>();
               // throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedResourceWeight");
            }

            if (plannedResource.Volume == null)
            {
                plannedResource.Volume = ServiceLocator.Current.GetInstance<IVolume>();
               // throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedResourceVolume");
            }
                
            if (plannedResource.Period == null)
            {
                plannedResource.Period = ServiceLocator.Current.GetInstance<IPeriod>();
                //  throw new ArgumentException(Resources.ProvidedInvestmentElementWithBudget, "plannedActivityPeriod");


            }

            if (!isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();
                var realPeriod = perDBC.Find(plannedResource.PeriodId);
                if (realPeriod != null)
                    plannedResource.Period = realPeriod;


            }

            plannedResourceClone.Weight = Clone(plannedResource.Weight);
            plannedResourceClone.WeightId = plannedResourceClone.Weight.Id;
            plannedResourceClone.Volume = Clone(plannedResource.Volume);
            plannedResourceClone.VolumeId = plannedResourceClone.Volume.Id;




            plannedResourceClone.Period = Clone(plannedResource.Period);
            plannedResourceClone.Period.Holder = plannedResourceClone;
            plannedResourceClone.PeriodId = plannedResourceClone.Period.Id;

            if (isAdding)
            {
                var perDBC = ServiceLocator.Current.GetInstance<IEntityFrameworkDbContext<Period>>();

                perDBC.Add(plannedResourceClone.Period);
                perDBC.Save();
                plannedResourceClone.Period = Clone(plannedResource.Period);
                plannedResourceClone.Period.Holder = plannedResourceClone;
                plannedResourceClone.PeriodId = plannedResourceClone.Period.Id;
            }

            plannedResourceClone.Component = Component;
            plannedResourceClone.ComponentId = Component.Id;
            return plannedResourceClone as TClass;
        }
        private IWeight Clone(IWeight weight)
        {
            var weightClone = ServiceLocator.Current.GetInstance<IWeight>();

            weightClone.MeasurementUnit = weight.MeasurementUnit;
            weightClone.Amount = weight.Amount;


            weightClone.Id = weight.Id ?? DatabaseContext.GenerateKey();

            return weightClone;
        }
        private IVolume Clone(IVolume volume)
        {
            var volumeClone = ServiceLocator.Current.GetInstance<IVolume>();

            volumeClone.MeasurementUnit = volume.MeasurementUnit;
            volumeClone.Amount = volume.Amount;


            volumeClone.Id = volume.Id ?? DatabaseContext.GenerateKey();

            return volumeClone;
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

        /// Relates the given investment element with a period, following the domain specification that every investment element must have a
        /// time period.
        /// </summary>
        /// <param name="investmentElement">The <see cref="IInvestmentElement"/> to relate to <paramref name="period"/>.</param>
        /// <param name="period">The <see cref="IPeriod"/> to relate to <paramref name="investmentElement"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="investmentElement"/> or <paramref name="period"/> is null.
        /// </exception>
        public void Relate(IPlannedResource plannedResource, IWeight weight)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedActivity");
            if (weight == null)
                throw new ArgumentNullException("weight");

            plannedResource.Weight = weight;

            //var wieghtRepo = ServiceLocator.Current.GetInstance<IMeasurableUnitRepository<IWeight>>();
            //wieghtRepo.Update(weight);
            // weight.Holder = plannedResource;
        }
        public void Relate(IPlannedResource plannedResource, IVolume volume)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedActivity");
            if (volume == null)
                throw new ArgumentNullException("volume");

            plannedResource.Volume = volume;
            //var volumeRepo = ServiceLocator.Current.GetInstance<IMeasurableUnitRepository<IVolume>>();
            //volumeRepo.Update(volume);
            // volume.Holder = plannedResource;
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
        public void Unrelate(IPlannedResource plannedResource, IWeight weight)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");
            if (weight == null)
                throw new ArgumentNullException("weight");
             var weightRepo = ServiceLocator.Current.GetInstance<IMeasurableUnitRepository<IWeight>>();
            
            weightRepo.Delete(weight);
            // Delete(weight);

            //  plannedResource.Weight = null;
            // weight.Holder = null;

        }
        public void Unrelate(IPlannedResource plannedResource, IVolume volume)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");
            if (volume == null)
                throw new ArgumentNullException("volume");

            var volumeRepo = ServiceLocator.Current.GetInstance<IMeasurableUnitRepository<IVolume>>();
            //// volumeRepo.Holder = plannedResource;
            volumeRepo.Delete(volume);
            // Delete(volume);

            //   plannedResource.Volume = null;
            //  volume.Holder = null;

        }

        public void Unrelate(IPlannedResource plannedResource, IPeriod period)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("plannedResource");
            if (period == null)
                throw new ArgumentNullException("volume");

            var periodRepo = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            //// volumeRepo.Holder = plannedResource;
            periodRepo.Delete(period);
            // Delete(volume);

            //   plannedResource.Volume = null;
            //  volume.Holder = null;

        }
        public void SaveReference(IWeight other)
        {
            //   DatabaseContext.Store(other);
        }
        public void SaveReference(IVolume other)
        {
            //   DatabaseContext.Store(other);
        }
        public override IEnumerable<IPlannedResource> FilterByName(string nameSpecification)
        {
            var itemsOfComponent = new BudgetComponentResourceOfSpecification<IPlannedResource, TComponent>(Component);
            var itemsWithName = new NomenclatorHavingInItsNameSpecification<IPlannedResource>(nameSpecification);
            var query = itemsOfComponent & itemsWithName;

            return DatabaseContext.Where(query).ToArray();
        }
    }
}
