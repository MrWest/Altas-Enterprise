using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels.Budget;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IExecutionViewModel : ICrudViewModel<IExecution, IExecutionPresenter>
     //where TComponent : class ,IBudgetComponent
    {
        IExecutedActivityPresenter ExecutedActivity { get; set; }
    }
}
