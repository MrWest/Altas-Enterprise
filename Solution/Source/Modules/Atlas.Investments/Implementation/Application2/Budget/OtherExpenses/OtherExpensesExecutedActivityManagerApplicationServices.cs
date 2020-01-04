﻿using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Application.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.OtherExpenses;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.OtherExpenses;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.OtherExpenses
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     planned activities of an OtherExpenses budget component.
    /// </summary>
    public class OtherExpensesExecutedActivityManagerApplicationServices :
        ExecutedBudgetComponentItemManagerApplicationServicesBase<IExecutedActivity, IOtherExpensesComponent, IOtherExpensesExecutedActivityRepository, IOtherExpensesExecutedActivityDomainServices>,
        IOtherExpensesExecutedActivityManagerApplicationServices
    {
        /// <summary>
        ///     Gets the instance of the other expenses planned activity repository.
        /// </summary>
        protected override IRepository PlannedItemRepository
        {
            get { return ServiceLocator.Current.GetInstance<IOtherExpensesPlannedActivityRepository>(); }
        }
    }
}