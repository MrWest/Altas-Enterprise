﻿using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;

namespace CompanyName.Atlas.Investments.Implementation.Domain.Services
{
    /// <summary>
    /// Implementation of the contract <see cref="IOtherExpensesPlannedActivityDomainServices"/>, representing a domain
    /// services ensuring that the business rules for the <see cref="IPlannedActivity"/> of a certain
    /// <see cref="IOtherExpensesComponent"/> are respected.
    /// </summary>
    public class OtherExpensesPlannedActivityDomainServices :
        PlannedActivityDomainServicesBase<IOtherExpensesComponent>,
        IOtherExpensesPlannedActivityDomainServices
    {
    }
}