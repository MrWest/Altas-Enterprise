using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    public class CashMovementCategoryOfSpecification<TItem> : Specification<TItem>
        where TItem:class ,ICashMovementCategory
    {
         /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public CashMovementCategoryOfSpecification(ICashMovementCategory aboveCategory)
        {
            if (aboveCategory == null)
                throw new ArgumentNullException("aboveCategory");

            Predicate = section => section.SuperiorCategory != null && Equals(section.SuperiorCategory.Id, aboveCategory.Id);
        }
    }

    public class CashFlowCashMovementCategoryOfSpecification<TItem> : Specification<TItem>
       where TItem : class ,ICashMovementCategory
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public CashFlowCashMovementCategoryOfSpecification(IWorkCapitalCashFlow aboveCategory)
        {
            if (aboveCategory == null)
                throw new ArgumentNullException("aboveCategory");

            Predicate = section => section.SuperiorCategory != null && Equals(section.SuperiorCategory.Id, aboveCategory.Id);
        }
    }

    public class CashMovementCategoryOfQueryable<TItem> : EntityFrameworkQueryable<TItem>
        where TItem : CashMovementCategory
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public CashMovementCategoryOfQueryable(ICashMovementCategory aboveCategory, IEntityFrameworkDbContext<TItem> context ): base(context)
        {
            if (aboveCategory == null)
                throw new ArgumentNullException("aboveCategory");

            Query = from section in context.Entities where section.SuperiorCategoryId == aboveCategory.Id select section;
        }
    }

    public class CashFlowCashMovementCategoryOfQueryable<TItem> : EntityFrameworkQueryable<TItem>
        where TItem : CashMovementCategory
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public CashFlowCashMovementCategoryOfQueryable(IWorkCapitalCashFlow aboveCategory, IEntityFrameworkDbContext<TItem> context) : base(context)
        {
            if (aboveCategory == null)
                throw new ArgumentNullException("aboveCategory");

            Query = from section in context.Entities where section.SuperiorCategoryId == aboveCategory.Id select section;
        }
    }
}
