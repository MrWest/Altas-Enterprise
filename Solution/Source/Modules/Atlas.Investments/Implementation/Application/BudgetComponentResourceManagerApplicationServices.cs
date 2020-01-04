using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Contracts.Presentation.Services;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using CompanyName.Atlas.UIControls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// Defines the main interface describing the resourses associeted to another budget component item 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <typeparam name="TComponent"></typeparam>
    public class BudgetComponentResourceManagerApplicationService<TComponent> :
         BudgetComponentItemManagerApplicationServicesBase<IPlannedResource, IBudgetComponentResourceRepository<TComponent>, IBudgetComponentResourceDomainServices<TComponent>>,
        IBudgetComponentResourceManagerApplicationServices<TComponent>
        //where TItem : class, IPlannedResource
        where TComponent :class, IBudgetComponentItem
        //where TRepository : IBudgetComponentResourceRepository<TComponent>
        //where TDomainServices : IBudgetComponentResourceDomainServices<TComponent>
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
        /// Gets repository to make the data operations over the budget component items.
        /// </summary>
        protected override IBudgetComponentResourceRepository<TComponent> Repository
        {
            get
            {
                IBudgetComponentResourceRepository<TComponent> repository = base.Repository;
                repository.Component = Component;

                return repository;
            }
        }


        /// <summary>
        /// Gets domain services to make the data operations over the budget component items.
        /// </summary>
        protected override IBudgetComponentResourceDomainServices<TComponent> DomainServices
        {
            get
            {
                IBudgetComponentResourceDomainServices<TComponent> domainServices = base.DomainServices;
                domainServices.Component = Component;

                return domainServices;
            }
        }

        /// <summary>
        /// Gets the entities from the data source.
        /// </summary>
        //public override IEnumerable<TItem> Items
        //{
        //  //  [CachesResult]
        //    get { return Repository.Entities; }
        //}



      

        protected override IPlannedResource SetAdquiring(IPlannedResource onAdquiring, IPlannedResource toAdquire)
        {
            base.SetAdquiring(onAdquiring, toAdquire);

            if(onAdquiring.Norm==0) // if norm zero adquire norm
             onAdquiring.Norm = toAdquire.Norm;
           onAdquiring.WageScale = toAdquire.WageScale;
           onAdquiring.WasteCoefficient = toAdquire.WasteCoefficient;
           onAdquiring.ResourceKind = toAdquire.ResourceKind;
           onAdquiring.MenNumber = toAdquire.MenNumber;
            if (toAdquire.Volume != null)
            {
                onAdquiring.Volume.Amount = toAdquire.Volume.Amount;
                onAdquiring.Volume.MeasurementUnit = toAdquire.Volume.MeasurementUnit;
            }

            if (toAdquire.Weight != null)
            {
                onAdquiring.Weight.Amount = toAdquire.Weight.Amount;

                onAdquiring.Weight.MeasurementUnit = toAdquire.Weight.MeasurementUnit;
            }
            

            // onAdquiring.Volume.Id = null;
            // onAdquiring.Weight.Id = null;
           Update(onAdquiring);
            
            return onAdquiring;
        }

        public override IPlannedResource Export(IDatabaseContext exportDatabaseContext, IPlannedResource item)
        {
            var newplannedResource = Repository.GetClone(item);
            newplannedResource.Component = Component;
            newplannedResource = ExportRelated(exportDatabaseContext, newplannedResource);

            exportDatabaseContext.Add(newplannedResource);

            var resourceRepository =
              ServiceLocator.Current
                  .GetInstance<IBudgetComponentResourceRepository<IPlannedResource>>();
            resourceRepository.Component = item;

            var service = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IPlannedResource>>();
            service.Component = newplannedResource;// as TComponent;

            foreach (IPlannedResource plannedResource in resourceRepository.Entities)
            {
                plannedResource.PriceSystem = newplannedResource.PriceSystem;
                service.Export(exportDatabaseContext, plannedResource);
            }

            return newplannedResource;
        }

        protected override void Check4Spread(IPlannedResource entity, IPlannedResource repoEntity)
        {
            
                
                  
                    if (!Equals(entity.MenNumber, repoEntity.MenNumber)) // if menNumer Changed
                        return;
                    if (!Equals(entity.Supplier, repoEntity.Supplier)) // if Supplier Changed
                        return;

                     if (!Equals(entity.Provider, repoEntity.Provider)) // if Provider Changed
                        return;

            var commonRepo = ServiceLocator.Current.GetInstance<ICommonRepository<IPlannedResource>>();

            int number =
                commonRepo.Count(
                    from plannedResource in
                    (commonRepo.DbContext as IEntityFrameworkDbContext<PlannedResource>)?.Entities
                    where plannedResource.Code == entity.Code select plannedResource);

            if ( number == 1)
                return;

            bool forActivity = !Equals(entity.Norm, repoEntity.Norm);
                    //    Spread(entity.Component);
              

                AtlasModuleView navigationServices =
                    ServiceLocator.Current.GetInstance<INavigationServices>() as AtlasModuleView;
                if (navigationServices != null)
                    if(forActivity)
                        navigationServices.StatusBar = new StatusBarConfirmationView()
                        {
                            Command = new DelegateCommand<IActivity>(ExecuteMethod, CanExecuteMethod),
                            DataContext = GetActivity(entity)
                        };
                else
                  base.Check4Spread(entity,repoEntity);
           
        }

        private IActivity GetActivity(IPlannedResource plannedResource)
        {
            if (plannedResource == null)
                return null;

            if (plannedResource.Component.GetType().Implements<IActivity>())
                return (IActivity) plannedResource.Component;
            return GetActivity(plannedResource.Component as IPlannedResource);
        }

        public override void Delete(IPlannedResource entity)
        {
            base.Delete(entity);

            AtlasModuleView navigationServices =
                   ServiceLocator.Current.GetInstance<INavigationServices>() as AtlasModuleView;

            if (navigationServices != null)
                navigationServices.StatusBar = new StatusBarConfirmationView()
                {
                    Command = new DelegateCommand<IActivity>(ExecuteMethod, CanExecuteMethod),
                    DataContext = GetActivity(entity)
                };
        }

        public DateTime FinishDate(IPlannedResource plannedResource)
        {
            if (plannedResource == null)
                throw new ArgumentNullException("activity");

            //get db period for the activity
            //var periodRepo = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            //periodRepo.Holder = plannedResource;

            //if (periodRepo.Entities.Any())
            //{
                IPeriod period = plannedResource.Period;
                // get all the menlabor resources
                var plannedResources = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IPlannedResource>>();
                plannedResources.Component = plannedResource;

                var hours = new List<decimal>();

                foreach (IPlannedResource planned in plannedResources.Entities)
                {
                    if (planned.ResourceKind == ResourceKind.MenLabor)
                        hours.Add(planned.Quantity / planned.MenNumber);
                }



                //if nothing here
                if (hours.Count == 0)
                    return period.OriEnd();
                //get max hours amount
                var maxhours = hours.Max();

                int days = (int)maxhours / 8;

                if (days > 0 && (int)maxhours % 8 > 0)
                    days++;

                return period.Starts.AddDays(days);
            //}

            // Repository.Delete(activity);
           // return DateTime.Today;
        }

        void IItemManagerApplicationServices<IPlannedResource>.Add(IPlannedResource item)
        {
            //var periodService = ServiceLocator.Current.GetInstance<IPeriodManagerApplicationServices>();
            //periodService.Holder = item;

            //periodService.Add(item.Period);

            //item.Period = periodService.Items.FirstOrDefault();

            base.Add(item);

          

        }

        public void AddFromScratch(string code, string name, string desc, string mu, string cu, decimal norm, decimal price, int kind, string wmu, decimal wv)
        {
            var resource = DomainServices.Create();
            resource.Name = name;
            resource.Code = code;
            resource.Description = desc;
            resource.MeasurementUnit = mu;
            resource.Currency = cu;
            resource.Norm = norm;
            resource.UnitaryCost = price;
            resource.ResourceKind = GetKind(kind);

            if (wmu != null)
            {
                var weightDs = ServiceLocator.Current.GetInstance<IWeightDomainService>();

               var weight =  weightDs.Create();
                weight.Amount = wv;
                weight.MeasurementUnit = wmu;

               // var weightAs = ServiceLocator.Current.GetInstance<IMeasurableUnitManagerApplicationServices<IWeight>>();
               // weightAs.Add(weight);

                resource.Weight = weight;


            }


            Repository.Add(resource);
        }

        public void EditFromScratch(object id, string name, string desc, string mu, string cu, decimal norm, decimal price, int kind, string wmu, decimal wv)
        {
            var resource = Find(id);
            resource.Name = name;
            resource.Description = desc;
            resource.MeasurementUnit = mu;
            resource.Currency = cu;
            resource.Norm = norm;
            resource.UnitaryCost = price;
            resource.ResourceKind = GetKind(kind);

            if (wmu != null)
            {
               // var weightDs = ServiceLocator.Current.GetInstance<IWeightDomainService>();

                resource.Weight.Amount = wv;
                resource.Weight.MeasurementUnit = wmu;
            }


            Repository.Update(resource);
        }

        private ResourceKind GetKind(int kind)
        {
            if( kind == 1)
                return ResourceKind.MenLabor;
            if (kind == 2)
                return ResourceKind.Equipment;
            if (kind == 3)
                return ResourceKind.Activity;

            return ResourceKind.Supply;

        }

        public bool ExistPlannedResource(string code)
        {

            return Items.Any(x =>
                string.Equals(x.Code.ToString(), code,
                    StringComparison.Ordinal));
        }

      
        public IPlannedResource GetPlannedResource<TComponent>(string code)
        {
            return Items.SingleOrDefault(x =>
               string.Equals(x.Code.ToString(), code,
                 StringComparison.Ordinal));
        }
    }
}
