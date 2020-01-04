using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Application.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the application services handling the coming CRUD-operations from upper layers regarding to the
    ///     planned resources of a work capital budget component.
    /// </summary>
    public class WorkCapitalPlannedResourceManagerApplicationServices :
        BudgetComponentItemManagerApplicationServicesBase<IPlannedResource, IWorkCapitalComponent, IWorkCapitalPlannedResourceRepository, IWorkCapitalPlannedResourceDomainServices>,
        IWorkCapitalPlannedResourceManagerApplicationServices
    {
    }
}