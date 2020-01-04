﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
    public interface IAtlasModuleSubjectPresenter : IAtlasModuleGenericSubjectPresenter<IAtlasModuleSubject>
    {
        IAtlasModuleGenericSubjectPresenter OwnerSubject { get; set; }
    }
}
