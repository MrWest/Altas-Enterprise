using System;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;

namespace CompanyName.Atlas.Contracts.Domain.Validation
{
    /// <summary>
    /// This class contains some extensions that allow to validate certain type of domain entities.
    /// </summary>
    public static class Validations
    {
        /// <summary>
        /// Determines whether a nomenclator has a unique name, a one that only belongs to it and no to other nomenclator. The rest of the
        /// nomenclators used to search for duplications is narrowed using the given specification.
        /// </summary>
        /// <typeparam name="TNomenclator">The type of the specific nomenclator to validate.</typeparam>
        /// <typeparam name="TRepository">The <see cref="IRepository{T}"/> used to access the rest of the nomenclators.</typeparam>
        /// <param name="nomenclator">The <see cref="INomenclator"/> to validate its name.</param>
        /// <param name="specification">
        /// The <see cref="ISpecification{T}"/> to narrow the scope of nomenclators to be taken into account when finding duplications.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Either <paramref name="nomenclator"/> or <paramref name="specification"/> is null.
        /// </exception>
        /// <returns>True if the given nomenclator has a unique name; false otherwise.</returns>
        public static bool HasUniqueName<TNomenclator, TRepository>(this TNomenclator nomenclator, ISpecification<TNomenclator> specification)
            where TNomenclator : INomenclator
            where TRepository : IRepository<TNomenclator>
        {
            if (nomenclator == null)
                throw new ArgumentNullException("nomenclator");
            if (specification == null)
                throw new ArgumentNullException("specification");

            // Construct the query
            var criteriaA = new Specification<TNomenclator>(x => !Equals(x.Id, nomenclator.Id));
            var criteriaB = new NomenclatorByNameSpecification<TNomenclator>(nomenclator.Name);
            ISpecification<TNomenclator> query = criteriaA & criteriaB & specification;

            try
            {
                // Find out whether there is another nomenclator with the same name as given's
                var repository = ServiceLocator.Current.GetInstance<TRepository>();
                var otherNomenclator = repository.Find(query);

                // Returns false when found another nomenclator with the same name; true otherwise
                return otherNomenclator == null;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether a nomenclator has a unique name, a one that only belongs to it and no to other nomenclator.
        /// </summary>
        /// <typeparam name="TNomenclator">The type of the specific nomenclator to validate.</typeparam>
        /// <typeparam name="TRepository">The <see cref="IRepository{T}"/> used to access the rest of the nomenclators.</typeparam>
        /// <param name="nomenclator">The <see cref="INomenclator"/> to validate its name.</param>
        /// <exception cref="ArgumentNullException"><paramref name="nomenclator"/> is null.</exception>
        /// <returns>True if the given nomenclator has a unique name; false otherwise.</returns>
        public static bool HasUniqueName<TNomenclator, TRepository>(this TNomenclator nomenclator)
            where TNomenclator : INomenclator
            where TRepository : IRepository<TNomenclator>
        {
            if (nomenclator == null)
                throw new ArgumentNullException("nomenclator");

            return nomenclator.HasUniqueName<TNomenclator, TRepository>(new Specification<TNomenclator>(x => true));
        }
    }
}
