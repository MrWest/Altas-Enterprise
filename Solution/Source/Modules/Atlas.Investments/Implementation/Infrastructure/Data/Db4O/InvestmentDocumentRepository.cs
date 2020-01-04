using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Domain.Specifications;
using CompanyName.Atlas.Investments.Infrastructure;
using CompanyName.Atlas.Investments.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Specifications;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;
using CompanyName.Atlas.Investments.Implementation.Domain.Entities;

namespace CompanyName.Atlas.Investments.Implementation.Infrastructure.Data.Db4O
{
    public class InvestmentDocumentRepository : DocumentRepository<IInvestmentDocument>, IInvestmentDocumentRepository
   {
        public InvestmentDocumentRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.DocumentType), GetName(x => x.Institution), GetName(x => x.Code),
                    GetName(x => x.Oace), GetName(x => x.Osde),
                    GetName(x => x.RecieveDate), GetName(x => x.DeliverDate)
                   
                }).ToArray();
            }
        }

       
      
        //public IEntity Holder { get; set; }
   }

    public class InvestmentDocumentRepositoryEF : DocumentRepositoryEF<IInvestmentDocument, InvestmentDocument>, IInvestmentDocumentRepository
    {
        public InvestmentDocumentRepositoryEF(IEntityFrameworkDbContext<InvestmentDocument> databaseContext) : base(databaseContext)
        {
        }

        protected override string[] RelevantProperties
        {
            get
            {
                return base.RelevantProperties.Concat(new[]
                {
                    GetName(x => x.DocumentType), GetName(x => x.Institution), GetName(x => x.Code),
                    GetName(x => x.Oace), GetName(x => x.Osde),
                    GetName(x => x.RecieveDate), GetName(x => x.DeliverDate)

                }).ToArray();
            }
        }



        //public IEntity Holder { get; set; }
    }
}
