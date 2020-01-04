using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    public class SubSpecialityHolderOfSpecification<THolder> : Specification<THolder>
        where THolder:class,ISubSpecialityHolder
    {
        public SubSpecialityHolderOfSpecification(IBudgetComponent budgetComponent)
        {
            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");

            Predicate = subs => subs.BudgetComponent != null && Equals(subs.BudgetComponent.Id, budgetComponent.Id);

        }
    }

    public class SubSpecialityHolderOfQueryable<THolder> : EntityFrameworkQueryable<THolder>
       where THolder : SubSpecialityHolder
    {

        public SubSpecialityHolderOfQueryable(IBudgetComponent budgetComponent, IEntityFrameworkDbContext<THolder> context) : base(context)
        {

            if (budgetComponent == null)
                throw new ArgumentNullException("budgetComponent");
            Query = (from e in context.Entities orderby e.Id ascending where e.BudgetComponentId == budgetComponent.Id select e);
        }
    }
}