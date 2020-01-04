using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Db4O;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Infrastructure.Common
{
    public class SubjectConceptExampleRepository: SubjectConceptContentRepository<ISubjectConceptExample>, ISubjectConceptExampleRepository
    {
        public SubjectConceptExampleRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }

    public class SubjectConceptExampleRepositoryEF : SubjectConceptContentRepositoryEF<ISubjectConceptExample, SubjectConceptExample>, ISubjectConceptExampleRepository
    {
        public SubjectConceptExampleRepositoryEF(IEntityFrameworkDbContext<SubjectConceptExample> databaseContext) : base(databaseContext)
        {
        }
    }
}
