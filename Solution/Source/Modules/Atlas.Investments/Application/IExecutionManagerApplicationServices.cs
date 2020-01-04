using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IExecutionManagerApplicationServices:IItemManagerApplicationServices<IExecution>
    {
        IExecutedActivity ExecutedActivity { get; set; }
    }
}
