using CompanyName.Atlas.Contracts.Presentation.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Presentation.ViewModels
{
    public interface IActivityExecutorViewModel:ICrudViewModel<IActivityExecutor, IActivityExecutorPresenter>, IActivityExecutorProvider
    {
        
    }
}