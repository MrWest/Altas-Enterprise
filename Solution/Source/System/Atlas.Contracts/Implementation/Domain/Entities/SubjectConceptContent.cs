using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public abstract class SubjectConceptContent: EntityBase, ISubjectConceptContent
    {
        public String Content { get; set; }

        public String Source { get; set; }

        public DateTime LastUpdate { get; set; }

        public String Author { get; set; }

        public String WebSite { get; set; }

        public ISubjectConcept SubjectConcept { get; set; }
        public string SubjectConceptId { get;  set; }
    }
}
