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
    public class SubExpenseConceptRepository : CodedNomenclatorRepositoryBase<ISubExpenseConcept>, IRelatedRepository<ISubExpenseConcept, IExpenseConcept>, ISubExpenseConceptRepository
    {
        public SubExpenseConceptRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public IExpenseConcept ExpenseConcept { get; set; }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<ISubExpenseConcept> Entities
        {
            get
            {
                
                ISpecification<ISubExpenseConcept> specification = new SubExpenseConceptOfSpecification(ExpenseConcept);

                return Where(specification);
            }
        }
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override ISubExpenseConcept Add(ISubExpenseConcept budgetComponenIOverGroup)
        {
            ISubExpenseConcept addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, ExpenseConcept, DatabaseContext);

            return addedBudgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(ISubExpenseConcept budgetComponenIOverGroup)
        {
            base.Update(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, ExpenseConcept, DatabaseContext);

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(ISubExpenseConcept budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ISubExpenseConcept");

            this.Unrelate(budgetComponenIOverGroup, ExpenseConcept, DatabaseContext);

           

            base.Delete(budgetComponenIOverGroup);
        }

      
        public void Relate(ISubExpenseConcept current, IExpenseConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("ISubExpenseConcept");
            if (other == null)
                throw new ArgumentNullException("IExpenseConcept");

            current.ExpenseConcept = other;
        }

        public void Unrelate(ISubExpenseConcept current, IExpenseConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.ExpenseConcept = null;
        }

        public void SaveReference(ISubExpenseConcept current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IExpenseConcept other)
        {
            
            DatabaseContext.Store(other);
        }
    }


    public class SubExpenseConceptRepositoryEF : CodedNomenclatorRepositoryBaseEF<ISubExpenseConcept, SubExpenseConcept>, IRelatedRepository<ISubExpenseConcept, IExpenseConcept>, ISubExpenseConceptRepository
    {
        public SubExpenseConceptRepositoryEF(IEntityFrameworkDbContext<SubExpenseConcept> databaseContext) : base(databaseContext)
        {
        }

        public IExpenseConcept ExpenseConcept { get; set; }

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
                    GetName(x => x.ExpenseConceptId)
                }).ToArray();
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<ISubExpenseConcept> Entities
        {
            get
            {

                var specification = new SubExpenseConceptOfQueryable(ExpenseConcept, DatabaseContext);

              //  var list = DatabaseContext.Entities.ToList();

                return Where(specification);
            }
        }
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override ISubExpenseConcept Add(ISubExpenseConcept budgetComponenIOverGroup)
        {
            this.Relate(budgetComponenIOverGroup, ExpenseConcept);

            ISubExpenseConcept addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

         

            return budgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(ISubExpenseConcept budgetComponenIOverGroup)
        {
            this.Relate(budgetComponenIOverGroup, ExpenseConcept);
            base.Update(budgetComponenIOverGroup);

           

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(ISubExpenseConcept budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("ISubExpenseConcept");

            this.Unrelate(budgetComponenIOverGroup, ExpenseConcept, DatabaseContext);



            base.Delete(budgetComponenIOverGroup);
        }


        public void Relate(ISubExpenseConcept current, IExpenseConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("ISubExpenseConcept");
            if (other == null)
                throw new ArgumentNullException("IExpenseConcept");

            current.ExpenseConcept = other;
            current.ExpenseConceptId = other.Id;
        }

        public void Unrelate(ISubExpenseConcept current, IExpenseConcept other)
        {
            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.ExpenseConcept = null;
            current.ExpenseConceptId = null;
        }

        public void SaveReference(ISubExpenseConcept current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Save();
        }

        public void SaveReference(IExpenseConcept other)
        {

            DatabaseContext.Save();
        }
    }
}
