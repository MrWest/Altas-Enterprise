﻿using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application.Budget.OtherExpenses
{
    /// <summary>
    /// Contract of the application services handling the CRUD-operation requests coming from upper layers, regarding
    /// to the Other expenses planned resources of an investment element's budget.
    /// </summary>
    public interface IOtherExpensesPlannedResourceManagerApplicationServices :
        IBudgetComponentItemManagerApplicationServices<IPlannedResource>
    {
    }
}
