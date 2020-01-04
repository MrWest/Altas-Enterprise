using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class UnderGroupActivityManagerApplicationServices : UnderGroupItemManagerApplicationServices< IUnderGroupActivity, IUnderGroupActivityRepository, IUnderGroupActivityDomainServices>, IUnderGroupActivityManagerApplicationServices
    {

        protected override IUnderGroupActivityDomainServices DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.UnderGroup = UnderGroup;
                return domain;
            }
        }

        public override IUnderGroupActivity Export(IDatabaseContext exportDatabaseContext, IUnderGroupActivity item)
        {




            var newpUnderActivity = Repository.GetClone(item);
            newpUnderActivity.UnderGroup = UnderGroup;

            //Export MeasurementUnits and Currency
            newpUnderActivity = ExportRelated(exportDatabaseContext, newpUnderActivity);

            exportDatabaseContext.Add(newpUnderActivity);
            //for period
            //var periodRepository = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            //periodRepository.Holder = plannedActivity;
            //exportDatabaseContext.Add(periodRepository.Entities.First());

            var resourceRepository =
                    ServiceLocator.Current
                        .GetInstance<IBudgetComponentResourceRepository<IUnderGroupActivity>>();
            resourceRepository.Component = item;

            var serviceResource = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IUnderGroupActivity>>();
            serviceResource.Component = newpUnderActivity;

            foreach (IPlannedResource plannedResource in resourceRepository.Entities)
            {
                plannedResource.PriceSystem = newpUnderActivity.PriceSystem;
                serviceResource.Export(exportDatabaseContext, plannedResource);
            }

            return newpUnderActivity;
        }

        /// <summary>
        /// adquiere properties from another IBudgetComponentItem
        /// </summary>
        /// <param name="onAdquiring"></param>
        /// <param name="toAdquire"></param>
        /// <returns></returns>
        public IUnderGroupActivity AdquirePlannedProperties(IUnderGroupActivity onAdquiring, IPlannedActivity toAdquire)
        //where TOther: class,IBudgetComponentItem
        {


            if (Equals(onAdquiring, null) || Equals(toAdquire, null))
                return null;
            // var resourceDomainSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceDomainServices<TItem>>();





            var resourceRepoSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IUnderGroupActivity>>();
            resourceRepoSevice.Component = onAdquiring;
            resourceRepoSevice.DeleteAll();

            var resourceRepoSeviceOther = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IPlannedActivity>>();
            resourceRepoSeviceOther.Component = toAdquire;

            foreach (IPlannedResource plannedResource in resourceRepoSeviceOther.Entities)
            {
                var recursiveDomainSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceDomainServices<IUnderGroupActivity>>();
                recursiveDomainSevice.Component = onAdquiring;
                var recursiveApplicationSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IUnderGroupActivity>>();
                recursiveApplicationSevice.Component = onAdquiring;
                var recursiveRepoSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IUnderGroupActivity>>();
                recursiveRepoSevice.Component = onAdquiring;
                var newAquiredResource = recursiveApplicationSevice.AdquireProperties(
                   recursiveRepoSevice.Add(recursiveDomainSevice.Create()), plannedResource);

                //   recursiveRepoSevice.Update(newAquiredResource);
            }


            //}




            onAdquiring = SetAdquiring(onAdquiring, toAdquire);


            return onAdquiring;



        }

        private IUnderGroupActivity SetAdquiring(IUnderGroupActivity onAdquiring, IPlannedActivity toAdquire)
        {

            onAdquiring.Name = toAdquire.Name;
            onAdquiring.Description = toAdquire.Description;
            onAdquiring.Code = toAdquire.Code;
            onAdquiring.MeasurementUnit = toAdquire.MeasurementUnit;
            onAdquiring.Currency = toAdquire.Currency;
            // onAdquiring.Quantity = toAdquire.Quantity;
            onAdquiring.SubExpenseConcept = toAdquire.SubExpenseConcept;
            onAdquiring.Category = toAdquire.Category;
            onAdquiring.UnitaryCost = toAdquire.UnitaryCost;
            onAdquiring.PriceSystem = toAdquire.PriceSystem;

            return onAdquiring;
        }

        protected override void Check4Spread(IUnderGroupActivity entity, IUnderGroupActivity repoEntity)
        {


            //if (!Equals(entity.Executor, repoEntity.Execution)) // if Execution Changed
            //    return;
            var commonRepo = ServiceLocator.Current.GetInstance<ICommonRepository<IUnderGroupActivity>>();

            if (commonRepo.Where(new Specification<IUnderGroupActivity>(x => x.Code.ToString() == entity.Code.ToString())).Count() == 1)
                return;

            base.Check4Spread(entity, repoEntity);

        }

        public void AddFromScratch(string code, string name, string desc, string mu, string cu, decimal price)
        {
            var activity = DomainServices.Create();
            activity.Name = name;
            activity.Code = code;
            activity.Description = desc;
            activity.MeasurementUnit = mu;
            activity.Currency = cu;
            activity.UnitaryCost = price;

            Repository.Add(activity);
        }

        public void EditFromScratch(object Id, string name, string desc, string mu, string cu, decimal price)
        {
            var activity = Find(Id);
            activity.Name = name;
            activity.Description = desc;
            activity.MeasurementUnit = mu;
            activity.Currency = cu;
            activity.UnitaryCost = price;

            Repository.Update(activity);
        }

       
    }
}
