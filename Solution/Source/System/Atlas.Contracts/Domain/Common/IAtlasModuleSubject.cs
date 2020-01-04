using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Domain.Common
{
    public interface IAtlasModuleSubject : IAtlasModuleGenericSubject
    {
        IAtlasModuleGenericSubject OwnerSubject { get; set; }
        string OwnerSubjectId { get; set; }
    }
}
