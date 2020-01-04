using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class ReferenceDocumentRepository:DocumentRepository<IReferenceDocument>, IReferenceDocumentRepository
    {
        public ReferenceDocumentRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.KeyWords),
                    GetName(x => x.PublishDate),
                }).ToArray();
            }
        }
    }

    public class ReferenceDocumentRepositoryEF : DocumentRepositoryEF<IReferenceDocument, ReferenceDocument>, IReferenceDocumentRepository
    {
        public ReferenceDocumentRepositoryEF(IEntityFrameworkDbContext<ReferenceDocument> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.KeyWords),
                    GetName(x => x.PublishDate),
                }).ToArray();
            }
        }
    }
}
