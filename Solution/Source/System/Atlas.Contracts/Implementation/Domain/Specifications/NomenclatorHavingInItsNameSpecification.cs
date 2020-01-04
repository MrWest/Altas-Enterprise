using System;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    /// <summary>
    /// This is the specification used to retreive the nomenclators containing a certain string in their names.
    /// </summary>
    /// <typeparam name="TNomenclator">The type of the actual nomenclators to evaluate with this specification.</typeparam>
    public class NomenclatorHavingInItsNameSpecification<TNomenclator> : Specification<TNomenclator>
        where TNomenclator : INomenclator
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="NomenclatorHavingInItsNameSpecification{TNomenclator}"/> given a name
        /// specification.
        /// </summary>
        /// <param name="nameSpecification">The <see cref="string"/> to find in the evaluated nomenclators.</param>
        /// <exception cref="ArgumentNullException"><paramref name="nameSpecification"/> is null.</exception>
        public NomenclatorHavingInItsNameSpecification(string nameSpecification)
        {
            if (nameSpecification == null)
                throw new ArgumentNullException("nameSpecification");
            //search to lower
            Predicate = x => x.Name.ToLower().Contains(nameSpecification.ToLower());
        }
    }
}
