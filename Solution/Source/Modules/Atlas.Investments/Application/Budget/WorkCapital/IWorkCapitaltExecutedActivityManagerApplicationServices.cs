﻿using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application.Budget.WorkCapital
{
    /// <summary>
    /// Contract of the application services handling the CRUD-operation requests coming from upper layers, regarding
    /// to the WorkCapital executed activities of an investment element's budget.
    /// </summary>
    public interface IWorkCapitalExecutedActivityManagerApplicationServices :
        IExecutedActivityItemManagerApplicationServices
    {
    }
}
