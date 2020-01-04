using System;
using System.Collections.Generic;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using Microsoft.Practices.ServiceLocation;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public class SubSpecialityRepository : CodedNomenclatorRepositoryBase<ISubSpeciality>, IRelatedRepository<ISubSpeciality, ISpeciality>, ISubSpecialityRepository
    {
        public SubSpecialityRepository(IDb4ODatabaseContext databaseContext)
            : base(databaseContext)
        {
        }

        public ISpeciality Speciality { get; set; }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<ISubSpeciality> Entities
        {
            get
            {

                ISpecification<ISubSpeciality> specification = new SubSpecialityOfSpecification(Speciality);

                return Where(specification);
            }
        }
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override ISubSpeciality Add(ISubSpeciality budgetComponenIOverGroup)
        {
            ISubSpeciality addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, Speciality, DatabaseContext);

            return addedBudgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(ISubSpeciality budgetComponenIOverGroup)
        {
            base.Update(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, Speciality, DatabaseContext);

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(ISubSpeciality budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ISubExpenseConcept");

            this.Unrelate(budgetComponenIOverGroup, Speciality, DatabaseContext);

           

            base.Delete(budgetComponenIOverGroup);
        }


        public void Relate(ISubSpeciality current, ISpeciality other)
        {
            if (current == null)
                throw new ArgumentNullException("ISubExpenseConcept");
            if (other == null)
                throw new ArgumentNullException("IExpenseConcept");

            current.Speciality = other;
        }

        public void Unrelate(ISubSpeciality current, ISpeciality other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.Speciality = null;
        }

        public void SaveReference(ISubSpeciality current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(ISpeciality other)
        {
            
            DatabaseContext.Store(other);
        }
    }

    public class SubSpecialityRepositoryEF : CodedNomenclatorRepositoryBaseEF<ISubSpeciality, SubSpeciality>, IRelatedRepository<ISubSpeciality, ISpeciality>, ISubSpecialityRepository
    {
        public SubSpecialityRepositoryEF(IEntityFrameworkDbContext<SubSpeciality> databaseContext)
            : base(databaseContext)
        {
        }

        /// <summary>
        ///     Gets all the public properties non-readonly properties of the coded nomenclators that are relevant to the current
        ///     repository when making its operations.
        /// </summary>
        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.SpecialityId)
                }).ToArray();
            }
        }

        public ISpeciality Speciality { get; set; }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<ISubSpeciality> Entities
        {
            get
            {

                var specification = new SubSpecialityBaseOfQueryable(Speciality, DatabaseContext);

                return Where(specification);
            }
        }
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override ISubSpeciality Add(ISubSpeciality budgetComponenIOverGroup)
        {
            this.Relate(budgetComponenIOverGroup, Speciality);

            ISubSpeciality addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

           

            return addedBudgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(ISubSpeciality budgetComponenIOverGroup)
        {
            this.Relate(budgetComponenIOverGroup, Speciality);

            base.Update(budgetComponenIOverGroup);

            

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(ISubSpeciality budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ISubExpenseConcept");

            this.Unrelate(budgetComponenIOverGroup, Speciality);



            base.Delete(budgetComponenIOverGroup);
        }


        public void Relate(ISubSpeciality current, ISpeciality other)
        {
            if (current == null)
                throw new ArgumentNullException("ISubExpenseConcept");
            if (other == null)
                throw new ArgumentNullException("IExpenseConcept");

            current.Speciality = other;
            current.SpecialityId = other.Id;
        }

        public void Unrelate(ISubSpeciality current, ISpeciality other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.Speciality = null;
            current.SpecialityId = null;
        }

        public void SaveReference(ISubSpeciality current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Save();
        }

        public void SaveReference(ISpeciality other)
        {

            DatabaseContext.Save();
        }
    }
}
