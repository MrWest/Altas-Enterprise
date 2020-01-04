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
    public class UnderGroupItemOfSpecification<TItem> : Specification<TItem>
          where TItem : class, IUnderGroupItem
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="UnderGroupItemOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="underGroup">The <see cref="IUnderGroup" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="underGroup" /> is null.</exception>
        public UnderGroupItemOfSpecification(IUnderGroup underGroup)
        {
            if (underGroup == null)
                throw new ArgumentNullException("underGroup");

            Predicate = regular => regular.UnderGroup != null && Equals(regular.UnderGroup.Id, underGroup.Id);
        }
    }

    public class UnderGroupItemOfQueryable<TSettings> : EntityFrameworkQueryable<TSettings>
       where TSettings : BudgetComponentItemBase, IUnderGroupItem
    {

        public UnderGroupItemOfQueryable(IUnderGroup underGroup, IEntityFrameworkDbContext<TSettings> context) : base(context)
        {

            if (underGroup == null)
                throw new ArgumentNullException("underGroup");
            Query = (from e in context.Entities orderby e.Id ascending where e.UnderGroupId == underGroup.Id select e);
            Parameter = underGroup.Id;
        }
    }
}