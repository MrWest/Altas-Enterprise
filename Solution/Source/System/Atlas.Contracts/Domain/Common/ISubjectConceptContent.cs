using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface ISubjectConceptContent: IEntity
    {
        ISubjectConcept SubjectConcept { get; set; }

        String Content { get; set; }

        String Source { get; set; }

        DateTime LastUpdate { get; set; }

        String Author { get; set; }

        String WebSite { get; set; }

        string SubjectConceptId { get; set; }
    }
}
