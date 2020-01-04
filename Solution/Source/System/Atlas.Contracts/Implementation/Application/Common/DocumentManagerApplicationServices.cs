using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class DocumentManagerApplicationServices<TDocument, TRepository, TDomain> : ItemManagerApplicationServicesBase<TDocument, TRepository, TDomain>, IDocumentManagerApplicationServices<TDocument>
        where TDocument : class, IDocument
        where TRepository : class, IDocumentRepository<TDocument>
        where TDomain : class, IDocumentDomainService<TDocument>
    {
        public IEntity Holder
        {
            get;

            set;
        }
        protected override TRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.Holder = Holder;
                return repo;
            }
        }

        protected override TDomain DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.Holder = Holder;
                return domain;
            }
        }

       
    }
}
