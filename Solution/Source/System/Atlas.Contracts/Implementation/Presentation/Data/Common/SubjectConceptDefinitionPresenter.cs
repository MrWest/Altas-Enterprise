using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common
{
    public class SubjectConceptDefinitionPresenter : SubjectConceptContentPresenter<ISubjectConceptDefinition, ISubjectConceptDefinitionManagerApplicationServices>, ISubjectConceptDefinitionPresenter
    {
        public override int Number
        {
            get { return SubjectConcept.ConceptDefinitions.Items.IndexOf(this) + 1; }
        }
    }
}
