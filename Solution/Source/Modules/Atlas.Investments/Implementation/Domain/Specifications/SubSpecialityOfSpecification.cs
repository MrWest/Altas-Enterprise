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
    public class SubSpecialityOfSpecification : Specification<ISubSpeciality>
    {
         /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public SubSpecialityOfSpecification(ISpeciality expenseConcept)
        {
            if (expenseConcept == null)
                throw new ArgumentNullException("aboveCategory");

            Predicate = sub => sub.Speciality != null && Equals(sub.Speciality.Id, expenseConcept.Id);
        }
    }

    public class SubSpecialityBaseOfQueryable : EntityFrameworkQueryable<SubSpeciality>
     
    {

        public SubSpecialityBaseOfQueryable(ISpeciality speciality, IEntityFrameworkDbContext<SubSpeciality> context) : base(context)
        {

            if (speciality == null)
                throw new ArgumentNullException("speciality");
            Query = (from e in context.Entities orderby e.Id ascending where e.SpecialityId == speciality.Id select e);
        }
    }
}
