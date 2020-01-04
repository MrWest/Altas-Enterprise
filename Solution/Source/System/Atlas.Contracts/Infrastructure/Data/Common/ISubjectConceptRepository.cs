﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Infrastructure.Data.Common
{
    public interface ISubjectConceptRepository:IRepository<ISubjectConcept>
    {
        IAtlasModuleGenericSubject ModuleSubject { get; set; }
    }
}