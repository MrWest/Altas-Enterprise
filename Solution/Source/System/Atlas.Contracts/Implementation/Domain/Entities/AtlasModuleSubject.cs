using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Entities
{
    public class AtlasModuleSubject : AtlasModuleGenericSubject, IAtlasModuleSubject
    {
        public IAtlasModuleGenericSubject OwnerSubject { get; set; }
        public string OwnerSubjectId { get; set; }
    }
}
