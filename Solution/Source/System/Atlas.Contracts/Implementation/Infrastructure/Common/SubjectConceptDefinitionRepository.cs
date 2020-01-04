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
    public class SubjectConceptDefinitionRepository: SubjectConceptContentRepository<ISubjectConceptDefinition>, ISubjectConceptDefinitionRepository
    {
        public SubjectConceptDefinitionRepository(IDb4ODatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }

    public class SubjectConceptDefinitionRepositoryEF : SubjectConceptContentRepositoryEF<ISubjectConceptDefinition, SubjectConceptDefinition>, ISubjectConceptDefinitionRepository
    {
        public SubjectConceptDefinitionRepositoryEF(IEntityFrameworkDbContext<SubjectConceptDefinition> databaseContext) : base(databaseContext)
        {
        }
    }
}
