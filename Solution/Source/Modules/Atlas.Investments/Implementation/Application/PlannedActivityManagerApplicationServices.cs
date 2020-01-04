using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Caching;
using CompanyName.Atlas.Contracts.Infrastructure.CrossCutting.Validation;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using CompanyName.Atlas.Investments.Properties;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class PlannedActivityManagerApplicationServices:
        ActivityManagerApplicationServices<IPlannedActivity, IPlannedActivityRepository, IPlannedActivityDomainServices>
        , IPlannedActivityManagerApplicationServices
      
    {

      

        public override IPlannedActivity Export(IDatabaseContext exportDatabaseContext, IPlannedActivity item)
        {
          
                var newplannedActivity = Repository.GetClone(item);
                newplannedActivity.SubSpecialityHolder = SubSpecialityHolder;

                //Export MeasurementUnits and Currency
                newplannedActivity = ExportRelated(exportDatabaseContext, newplannedActivity);

                exportDatabaseContext.Add(newplannedActivity);
            //for period
            //var periodRepository = ServiceLocator.Current.GetInstance<IPeriodRepository>();
            //periodRepository.Holder = plannedActivity;
            //exportDatabaseContext.Add(periodRepository.Entities.First());

            var resourceRepository =
                    ServiceLocator.Current
                        .GetInstance<IBudgetComponentResourceRepository<IPlannedActivity>>();
                resourceRepository.Component = item;

            var serviceResource = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IPlannedActivity>>();
            serviceResource.Component = newplannedActivity;

            foreach (IPlannedResource plannedResource in resourceRepository.Entities)
            {
                serviceResource.Export(exportDatabaseContext, plannedResource);
            }

            return newplannedActivity;
        }

        /// <summary>
        /// adquiere properties from another IBudgetComponentItem
        /// </summary>
        /// <param name="onAdquiring"></param>
        /// <param name="toAdquire"></param>
        /// <returns></returns>
        public  IPlannedActivity AdquireUnderProperties(IPlannedActivity onAdquiring, IUnderGroupActivity toAdquire)
        //where TOther: class,IBudgetComponentItem
        {


            if (Equals(onAdquiring, null) || Equals(toAdquire, null))
                return null;
            // var resourceDomainSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceDomainServices<TItem>>();





            var resourceRepoSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IPlannedActivity>>();
            resourceRepoSevice.Component = onAdquiring;
            resourceRepoSevice.DeleteAll();

            var resourceRepoSeviceOther = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IUnderGroupActivity>>();

            resourceRepoSeviceOther.Component = toAdquire;
            foreach (IPlannedResource plannedResource in resourceRepoSeviceOther.Entities)
            {
                var recursiveDomainSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceDomainServices<IPlannedActivity>>();
                recursiveDomainSevice.Component = onAdquiring;
                var recursiveApplicationSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IPlannedActivity>>();
                recursiveApplicationSevice.Component = onAdquiring;
                var recursiveRepoSevice = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IPlannedActivity>>();
                recursiveRepoSevice.Component = onAdquiring;
                var newAquiredResource = recursiveApplicationSevice.AdquireProperties(
                   recursiveRepoSevice.Add(recursiveDomainSevice.Create()), plannedResource);

                //   recursiveRepoSevice.Update(newAquiredResource);
            }


            //}




            onAdquiring = SetAdquiring(onAdquiring, toAdquire);


            return onAdquiring;



        }

        private  IPlannedActivity SetAdquiring(IPlannedActivity onAdquiring, IUnderGroupActivity toAdquire)
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

        protected override void Check4Spread(IPlannedActivity entity, IPlannedActivity repoEntity)
        {
           
                
                if (!Equals(entity.Executor, repoEntity.Execution)) // if Execution Changed
                    return;
                var commonRepo = ServiceLocator.Current.GetInstance<ICommonRepository<IPlannedActivity>>();


            int number =
          commonRepo.Count(
              from plannedActivity in
              (commonRepo.DbContext as IEntityFrameworkDbContext<PlannedActivity>)?.Entities
              where plannedActivity.Code == entity.Code
              select plannedActivity);

            if (number == 1)
                return;
            //if (commonRepo.Where(new Specification<IPlannedActivity>(x=> x.Code.ToString()== entity.Code.ToString())).Count()==1)
            //    return;

                base.Check4Spread(entity,repoEntity);
           
        }

        public DateTime StartDate(IPlannedActivity plannedActivity)
        {

            var repoAct = Repository.Find(plannedActivity.Id);
            var rsltDate = repoAct.Period.Starts;
           

            var resRepo = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IPlannedActivity>>();
            resRepo.Component = plannedActivity;

            if (resRepo.Entities.Any(x => x.ResourceKind == ResourceKind.Activity))
            {
                var resServ = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IPlannedActivity>>();
                resServ.Component = plannedActivity;

                foreach (IPlannedResource plannedResource in
                    resRepo.Entities.Where(x => x.ResourceKind == ResourceKind.Activity))
                {
                    var auxDate = plannedResource.Period.Starts;
                    if (rsltDate < auxDate)
                        rsltDate = auxDate;

                }
                
            }

           

             return rsltDate;

        }

        public DateTime FinishDate(IPlannedActivity plannedActivity)
        {
                var rsltDate = plannedActivity.Period.Ends;
           

            var resRepo = ServiceLocator.Current.GetInstance<IBudgetComponentResourceRepository<IPlannedActivity>>();
            resRepo.Component = plannedActivity;

            if (resRepo.Entities.Any(x => x.ResourceKind == ResourceKind.Activity))
            {
                var resServ = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IPlannedActivity>>();
                resServ.Component = plannedActivity;

                foreach (IPlannedResource plannedResource in
                    resRepo.Entities.Where(x => x.ResourceKind == ResourceKind.Activity))
                {
                    var auxDate = resServ.FinishDate(plannedResource);
                    if (rsltDate < auxDate)
                        rsltDate = auxDate;

                }

            }

            if (resRepo.Entities.Any(x => x.ResourceKind == ResourceKind.MenLabor))
            {
                var resServ = ServiceLocator.Current.GetInstance<IBudgetComponentResourceManagerApplicationServices<IPlannedActivity>>();
                resServ.Component = plannedActivity;
                var hours = new List<decimal>();
                foreach (IPlannedResource plannedResource in
                    resRepo.Entities.Where(x => x.ResourceKind == ResourceKind.MenLabor))
                {
                    hours.Add(plannedResource.Quantity / plannedResource.MenNumber);

                }


                //if nothing here
                if (hours.Count == 0)
                    return rsltDate;
                //get max hours amount
                var maxhours = hours.Max();

                int days = (int)maxhours / 8;

                if (days>0&&(int)maxhours % 8 > 0)
                    days++;

                var auxDate = StartDate(plannedActivity).AddDays(days); ;
                if (rsltDate < auxDate)
                    rsltDate = auxDate;




            }

            return rsltDate;

        }
    }


}
