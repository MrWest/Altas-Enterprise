using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface ISubjectConcept: ICodedNomenclator
    {
        IAtlasModuleGenericSubject ModuleSubject { get; set; }
        string ModuleSubjectId { get; set; }
        //string Code { get; set; }

        IList<ISubjectConceptDefinition> Definitions { get; set; }

        IList<ISubjectConceptExample> Examples { get; set; }

        IList<ISubjectConcept> RelatedConcepts { get; set; }
    }
}
