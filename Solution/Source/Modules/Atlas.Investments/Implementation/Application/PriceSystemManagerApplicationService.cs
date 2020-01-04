using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// Implements an application service for  Price system entities
    /// </summary>
    public class PriceSystemManagerApplicationService:ItemManagerApplicationServicesBase<IPriceSystem,IPriceSystemRepository,IPriceSystemDomainService>,IPriceSystemManagerApplicationService
    {
        
        public bool Export(IDatabaseContext databaseContext, IPriceSystem priceSystem)
        {

            if (databaseContext == null)
                return false;
            // var defaultDatabaseContext = ServiceLocator.Current.GetInstance<IDatabaseContext>();

            bool stateSaved = priceSystem.IsActive;

            priceSystem.IsActive = false;

            SavePriceSystem(databaseContext, priceSystem);

            priceSystem.IsActive = stateSaved;

            databaseContext.Save();

            return true;
        }

        private void SavePriceSystem(IDatabaseContext databaseContext, IPriceSystem priceSystem)
        {

            var expriceSystem = Repository.GetClone(priceSystem);
            databaseContext.Add(expriceSystem);
            var overGrupRepo = ServiceLocator.Current.GetInstance<IOverGroupRepository>();
            overGrupRepo.PriceSystem = priceSystem;

            foreach (IOverGroup overGroup in overGrupRepo.Entities)
            {
                var newoverGroup = overGrupRepo.GetClone(overGroup);
                newoverGroup.AbovePriceSystem = expriceSystem;
                newoverGroup.PriceSystem = expriceSystem.Id;
                databaseContext.Add(newoverGroup);

                var regularGrupRepo = ServiceLocator.Current.GetInstance<IRegularGroupRepository>();
                regularGrupRepo.OverGroup = overGroup;

                foreach (IRegularGroup regular in regularGrupRepo.Entities)
                {
                    var newRegular = regularGrupRepo.GetClone(regular);
                    newRegular.OverGroup = newoverGroup;
                    newRegular.PriceSystem = newoverGroup.PriceSystem;
                    databaseContext.Add(newRegular);

                    var underGrupRepo = ServiceLocator.Current.GetInstance<IUnderGroupRepository>();
                    underGrupRepo.RegularGroup = regular;

                    foreach (IUnderGroup under in underGrupRepo.Entities)
                    {
                        var newUnder = underGrupRepo.GetClone(under);
                        newUnder.RegularGroup = newRegular;
                        newUnder.PriceSystem = newRegular.PriceSystem;
                        databaseContext.Add(newUnder);

                        var activityRepo = ServiceLocator.Current.GetInstance<IUnderGroupActivityRepository>();
                        activityRepo.UnderGroup = under;

                        var service = ServiceLocator.Current.GetInstance<IUnderGroupActivityManagerApplicationServices>();
                        service.UnderGroup = newUnder;

                        foreach (IUnderGroupActivity underGroupActivity in activityRepo.Entities)
                        {
                            underGroupActivity.PriceSystem = newUnder.PriceSystem;
                            service.Export(databaseContext, underGroupActivity);

                        }

                    
                    }
                }

            }
        }

        public override void Update(IPriceSystem entity)
        {
            if (entity.IsActive)
            {
              var pActive =  Repository.Entities.SingleOrDefault(x => x.IsActive);
                if (!Equals(null, pActive))
                {
                    pActive.IsActive = false;
                    Repository.Update(pActive);
                }
            }
            base.Update(entity);

        }

        public bool ExistOverGroup(string code, IPriceSystem priceSystem)
        {
            var overGroupService = ServiceLocator.Current.GetInstance<IOverGroupManagerApplicationServices>();
            overGroupService.PriceSystem = priceSystem;
            return overGroupService.Items.Any(x =>
                string.Equals(x.Code.ToString(), code,
                    StringComparison.Ordinal));
        }

        public void AddFromScratch(string code, string name, IPriceSystem priceSystem)
        {
            var overGroupService = ServiceLocator.Current.GetInstance<IOverGroupManagerApplicationServices>();
            overGroupService.PriceSystem = priceSystem;
            var overgroup =  overGroupService.Create();
            overgroup.Code = code;
            overgroup.Name = name;
            overGroupService.Add(overgroup);

        }

        public IOverGroup GetOverGroup(string code, IPriceSystem priceSystem)
        {
            var overGroupService = ServiceLocator.Current.GetInstance<IOverGroupManagerApplicationServices>();
            overGroupService.PriceSystem = priceSystem;
            return overGroupService.Items.SingleOrDefault(x =>
                            string.Equals(x.Code.ToString(), code,
                                StringComparison.Ordinal));
        }
    }
}
