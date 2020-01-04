using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    class CashMovementCategoryManagerApplicationServices<TItem> : ItemManagerApplicationServicesBase<TItem, ICashMovementCategoryRepository<TItem>, ICashMovementCategoryDomainService<TItem>>, ICashMovementCategoryManagerApplicationServices<TItem>
        where TItem : class,ICashMovementCategory
    {
        private ICashMovementCategory _cashMovementCategory;
        /// <summary>
        /// Superior Category
        /// </summary>
        public ICashMovementCategory SuperiorCategory
        {
            get { return _cashMovementCategory; }
            set { _cashMovementCategory = value; }
        }

        public void SetCashMovement(decimal value, IPeriod period, ICashMovementCategory cashMovementCategory)
        {
            var cashMovementService = ServiceLocator.Current.GetInstance<ICashMovementRepository>();
            cashMovementService.Category = cashMovementCategory;

            var list = cashMovementService.Entities.ToList();
            if (list.Any(x=>x.Date.ToShortDateString()==period.Starts.ToShortDateString()))
            {
                var newCashMovement = list.FirstOrDefault(x => x.Date.ToShortDateString() == period.Starts.ToShortDateString());
                newCashMovement.Date = period.Starts;
                newCashMovement.Amount = value;
                cashMovementService.Update(newCashMovement);
            }
            else
            {
               var newCashMovement =  ServiceLocator.Current.GetInstance<ICashMovement>();
                newCashMovement.Date = period.Starts;
                newCashMovement.Amount = value;
                cashMovementService.Add(newCashMovement);
            }

        }

        /// <summary>
       /// Gets repository to make the data operations over the budget component items.
       /// </summary>
       protected override ICashMovementCategoryRepository<TItem> Repository
       {
           get
           {
               ICashMovementCategoryRepository<TItem> repository = base.Repository;
               repository.SuperiorCategory = SuperiorCategory;

               return repository;
           }
       }


       /// <summary>
       /// Gets domain services to make the data operations over the budget component items.
       /// </summary>
       protected override ICashMovementCategoryDomainService<TItem> DomainServices
       {
           get
           {
               ICashMovementCategoryDomainService<TItem> domainServices = base.DomainServices;
               domainServices.SuperiorCategory = SuperiorCategory;

               return domainServices;
           }
       }
    }
}
