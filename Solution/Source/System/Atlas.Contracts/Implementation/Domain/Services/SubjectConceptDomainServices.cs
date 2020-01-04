using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Properties;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Services
{
    public class SubjectConceptDomainServices: CodedNomenclatorDomainServicesBase<ISubjectConcept>, ISubjectConceptDomainServices
    {
        public IAtlasModuleGenericSubject ModuleSubject { get; set; }

        public override ISubjectConcept Create()
        {
            var concept = base.Create();
            concept.Name = Resources.New_SubjectConcept;
            concept.ModuleSubject = ModuleSubject;
            return concept;
        }
    }
}
