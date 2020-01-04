using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels.Budget.WorkCapital
{
    /// <summary>
    ///     Contract of the crud view model managing the executed activities of an WorkCapital budget component.
    /// </summary>
    public interface IWorkCapitalExecutedActivityViewModel :
        IExecutedActivityViewModel<IWorkCapitalComponent, IWorkCapitalExecutedActivityPresenter>
    {
    }
}