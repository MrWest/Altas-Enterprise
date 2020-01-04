﻿using CompanyName.Atlas.Contracts.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.Atlas.Contracts.Presentation.Common
{
   public interface ICurrenciablePresenter : ICosttable
    {
        ICurrencyPresenter Currency { get; set; }
    }
}