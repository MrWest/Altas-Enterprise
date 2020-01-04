﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application.Budget.WorkCapital
{
    public interface ICashFlowCashMovementCategoryManagerApplicationServices<TItem> : IItemManagerApplicationServices<TItem>
     where TItem : class ,ICashMovementCategory
    {
         IWorkCapitalCashFlow WorkCapitalCashFlow { get; set; }
    }
}
