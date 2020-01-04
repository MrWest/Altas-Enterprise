using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Presentation.Data;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface ISubjectConceptViewModel:ICrudViewModel<ISubjectConcept, ISubjectConceptPresenter>
    {
        IAtlasModuleGenericSubjectPresenter ModuleSubject { get; set; }
    }
}
