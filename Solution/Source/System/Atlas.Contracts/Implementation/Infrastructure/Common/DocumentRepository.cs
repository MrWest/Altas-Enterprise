using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Specification;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public abstract class DocumentRepository<TDocument> : CodedNomenclatorRepositoryBase<TDocument>, IRelatedRepository<TDocument, IEntity>,IDocumentRepository<TDocument>
     where TDocument : class, IDocument
    {
        public DocumentRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
        public IEntity Holder
        {
            get; set;
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Author),
                    GetName(x => x.FilePath),
                }).ToArray();
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TDocument> Entities
        {
            get
            {

                ISpecification<TDocument> specification = new DocumentOfSpecification<TDocument>(Holder);

                return Where(specification);
            }
        }
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override TDocument Add(TDocument budgetComponenIOverGroup)
        {
            TDocument addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, Holder, DatabaseContext);

            return addedBudgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(TDocument budgetComponenIOverGroup)
        {
            base.Update(budgetComponenIOverGroup);

            this.Relate(budgetComponenIOverGroup, Holder, DatabaseContext);

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(TDocument budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("TDocument");

            this.Unrelate(budgetComponenIOverGroup, Holder, DatabaseContext);



            base.Delete(budgetComponenIOverGroup);
        }


        public void Relate(TDocument current, IEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("ISubExpenseConcept");
            if (other == null)
                throw new ArgumentNullException("IExpenseConcept");

            current.Holder = other;
        }

        public void Unrelate(TDocument current, IEntity other)
        {

            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.Holder = null;
        }

        public void SaveReference(TDocument current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Store(current);
        }

        public void SaveReference(IEntity other)
        {

            DatabaseContext.Store(other);
        }

    }


    public abstract class DocumentRepositoryEF<TDocument, TClass> : CodedNomenclatorRepositoryBaseEF<TDocument, TClass>, IRelatedRepository<TDocument, IEntity>, IDocumentRepository<TDocument>
     where TDocument : class, IDocument
        where TClass :  Document
    {
        public DocumentRepositoryEF(IEntityFrameworkDbContext<TClass> databaseContext) : base(databaseContext)
        {
        }
        public IEntity Holder
        {
            get; set;
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.Author),
                    GetName(x => x.FilePath),
                     GetName(x => x.HolderId),
                }).ToArray();
            }
        }

        /// <summary>
        /// Gets the all budget component items of the current component.
        /// </summary>
        public override IEnumerable<TDocument> Entities
        {
            get
            {

                var specification = new DocumentOfQueryable<TClass>(Holder, DatabaseContext);

                return Where(specification);
            }
        }
        /// <summary>
        /// Adds the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override TDocument Add(TDocument budgetComponenIOverGroup)
        {

            this.Relate(budgetComponenIOverGroup, Holder);
            TDocument addedBudgetComponenIOverGroup = base.Add(budgetComponenIOverGroup);

           

            return addedBudgetComponenIOverGroup;
        }

        /// <summary>
        /// Updates the given buget component item to the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to update.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Update(TDocument budgetComponenIOverGroup)
        {
            this.Relate(budgetComponenIOverGroup, Holder);
            base.Update(budgetComponenIOverGroup);

           

        }

        /// <summary>
        /// Deletes the given buget component item from the current repository.
        /// </summary>
        /// <param name="budgetComponenIOverGroup">The <see cref="IBudgetComponenIOverGroup"/> to delete.</param>
        /// <exception cref="ArgumentNullException"><paramref name="budgetComponenIOverGroup"/> is null.</exception>
        public override void Delete(TDocument budgetComponenIOverGroup)
        {
            if (budgetComponenIOverGroup == null)
                throw new ArgumentNullException("TDocument");

           // this.Unrelate(budgetComponenIOverGroup, Holder, DatabaseContext);



            base.Delete(budgetComponenIOverGroup);
        }


        public void Relate(TDocument current, IEntity other)
        {
            if (current == null)
                throw new ArgumentNullException("ISubExpenseConcept");
            if (other == null)
                throw new ArgumentNullException("IExpenseConcept");

            current.Holder = other;
            current.HolderId = other.Id;
        }

        public void Unrelate(TDocument current, IEntity other)
        {

            if (current == null)
                throw new ArgumentNullException("budgetComponentItem");
            if (other == null)
                throw new ArgumentNullException("budgetComponent");

            current.Holder = null;
            current.HolderId = null;
        }

        public void SaveReference(TDocument current)
        {
            if (current == null)
                throw new ArgumentNullException("ICashMovementCategory");

            DatabaseContext.Save();
        }

        public void SaveReference(IEntity other)
        {

            DatabaseContext.Save();
        }

    }
}
