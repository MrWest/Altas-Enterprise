using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{
    public class SubjectConceptManagerApplicationServices: ItemManagerApplicationServicesBase<ISubjectConcept, ISubjectConceptRepository, ISubjectConceptDomainServices>, ISubjectConceptManagerApplicationServices
    {
        public IAtlasModuleGenericSubject ModuleSubject { get; set; }

        protected override ISubjectConceptRepository Repository
        {
            get
            {
                var repo = base.Repository;
                repo.ModuleSubject = ModuleSubject;
                return repo;
            }
        }

        protected override ISubjectConceptDomainServices DomainServices
        {
            get
            {
                var domain = base.DomainServices;
                domain.ModuleSubject = ModuleSubject;
                return domain;
            }
        }
    }
}
