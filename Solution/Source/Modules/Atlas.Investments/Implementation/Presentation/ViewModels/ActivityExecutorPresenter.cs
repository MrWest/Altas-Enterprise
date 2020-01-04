using CompanyName.Atlas.Contracts.Implementation.Presentation.Data.Common;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Presentation.ViewModels;

namespace CompanyName.Atlas.Investments.Implementation.Presentation.ViewModels
{
    public class ActivityExecutorPresenter: CodedNomenclatorPresenterBase<IActivityExecutor, IActivityExecutorManagerApplicationServices>, IActivityExecutorPresenter
    {
        public ActivityExecutorPresenter(IActivityExecutor nomenclator) : base(nomenclator)
        {
        }
    }
}