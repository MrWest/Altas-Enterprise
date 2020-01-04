using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    public class BudgetComponentItemForNomenclatorSpecification<TItem> : Specification<TItem>
         where TItem : class, IBudgetComponentItem
    {
        public BudgetComponentItemForNomenclatorSpecification(String text)
        {
            var psRepo = ServiceLocator.Current.GetInstance<IPriceSystemRepository>();
            var activePs = psRepo.Entities.SingleOrDefault(x => x.IsActive);
            if(Equals(activePs,null))
            Predicate = x => x.Name != null && text != null &&
           (x.Name.ToLower().Contains(text.ToLower()) || x.Description.ToLower().Contains(text.ToLower()) || x.Code.ToLower().Contains(text.ToLower()));
            else
            {
                Predicate = x => x.Name != null && text != null &&
                 (x.Name.ToLower().Contains(text.ToLower()) || x.Description.ToLower().Contains(text.ToLower()) || x.Code.ToLower().Contains(text.ToLower()))
                 && (Equals(x.PriceSystem, null) || Equals(x.PriceSystem.ToString(), activePs.Id.ToString()));
            }

        }
    }
}