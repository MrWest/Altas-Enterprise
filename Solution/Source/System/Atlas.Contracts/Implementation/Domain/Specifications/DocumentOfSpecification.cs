using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    public class DocumentOfSpecification<TDocument> : Specification<TDocument>
        where TDocument:class,IDocument
    {
        public DocumentOfSpecification(IEntity holder)
        {
            if (holder == null)
                throw new ArgumentNullException("holder");

            Predicate = document => document.Holder != null && Equals(document.Holder.Id, holder.Id);
        }
    }

    class DocumentOfQueryable<TDocument> : EntityFrameworkQueryable<TDocument>
        where TDocument : Document
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public DocumentOfQueryable(IEntity holder, IEntityFrameworkDbContext<TDocument> context) : base(context)
        {
            if (holder == null)
                throw new ArgumentNullException("holder");

            Query = (from e in context.Entities orderby e.Id ascending where e.HolderId == holder.Id select e);
        }
    }
  
}
