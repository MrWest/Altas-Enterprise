using System;
using System.Linq;
using System.Linq.Expressions;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;


namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    public class NomenclatorByContainsSpecification<T> : Specification<T>
        where T : ICodedNomenclator
    {
        public NomenclatorByContainsSpecification(string text, Expression<Predicate<T>> added)
        {
            Predicate =  x => x.Name != null && text != null && 
            (x.Name.ToLower().Contains(text.ToLower()) || x.Description.ToLower().Contains(text.ToLower()) || x.Code.ToLower().Contains(text.ToLower()))
            
            ;
            if (!Equals(added, null))
            {
                Predicate = Predicate.And(added);
            }
        }

       
    }

    public class NomenclatorByContainsQueryable<TClass> : EntityFrameworkQueryable<TClass>
        where TClass : CodedNomenclatorBase
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public NomenclatorByContainsQueryable(string text, IEntityFrameworkDbContext<TClass> context) : base(context)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            Query = (from e in context.Entities orderby e.Id ascending where e.Name.Contains(text) || e.Code.Contains(text) select e);
        }
    }
}