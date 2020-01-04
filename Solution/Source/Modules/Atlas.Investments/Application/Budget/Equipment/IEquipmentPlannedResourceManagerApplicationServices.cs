﻿using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application.Budget.Equipment
{
    /// <summary>
    /// Contract of the application services handling the CRUD-operation requests coming from upper layers, regarding
    /// to the equipment planned resources of an investment element's budget.
    /// </summary>
    public interface IEquipmentPlannedResourceManagerApplicationServices :
        IBudgetComponentItemManagerApplicationServices<IPlannedResource>
    {
    }
}
