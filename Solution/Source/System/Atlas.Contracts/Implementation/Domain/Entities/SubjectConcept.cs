using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class SubjectConcept: CodedNomenclatorBase, ISubjectConcept
    {
        //public string Code { get; set; }

        public IList<ISubjectConceptDefinition> Definitions { get; set; }

        public IList<ISubjectConceptExample> Examples { get; set; }

        public IList<ISubjectConcept> RelatedConcepts { get; set; }

        public IAtlasModuleGenericSubject ModuleSubject { get; set; }
        public string ModuleSubjectId { get; set; }
    }
}
