using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface ISubjectConceptPresenter:IPresenter<ISubjectConcept>
    {
        IAtlasModuleGenericSubjectPresenter ModuleSubject { get; set; }
        string Code { get; set; }
        String Name { get; set; }
        ISubjectConceptDefinitionViewModel ConceptDefinitions { get; }
        ISubjectConceptExampleViewModel ConceptExamples { get; }
        IRelatedConceptViewModel RelatedConcepts { get; }

    }
}
