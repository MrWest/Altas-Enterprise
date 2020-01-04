using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Application.Budget.WorkCapital;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Implementation of the contract <see cref="IWorkCapitalExecutedActivityViewModel" /> representing the crud view
    ///     model used to manage the executed activities of an equipment budget component.
    /// </summary>
    public class WorkCapitalExecutedActivityViewModel :
        ExecutedActivityViewModelBase,
        IWorkCapitalExecutedActivityViewModel
    {
    }
}