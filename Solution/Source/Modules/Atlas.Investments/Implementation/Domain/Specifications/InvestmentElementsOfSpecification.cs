using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Specifications
{
    /// <summary>
    ///     This is the specification containing the predicate to obtain all those investment elements being child of a certain
    ///     one.
    /// </summary>
    public class InvestmentElementsOfSpecification : Specification<IInvestmentComponent>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public InvestmentElementsOfSpecification(IInvestmentElement parentInvestmentElement)
        {
            if (parentInvestmentElement == null)
                throw new ArgumentNullException("parentInvestmentElement");

            Predicate = invElem => invElem.Parent != null && Equals(invElem.Parent.Id, parentInvestmentElement.Id);
        }
    }


    public class InvestmentElementsOfQueryable : EntityFrameworkQueryable<InvestmentComponent>
      
    {

        public InvestmentElementsOfQueryable(IInvestmentElement parentInvestmentElement, IEntityFrameworkDbContext<InvestmentComponent> context) : base(context)
        {

            if (parentInvestmentElement == null)
                throw new ArgumentNullException("parentInvestmentElement");
            Query = (from e in context.Entities orderby e.Id ascending where e.ParentId == parentInvestmentElement.Id select e);
            Parameter = parentInvestmentElement.Id;
            
        }
    }
}