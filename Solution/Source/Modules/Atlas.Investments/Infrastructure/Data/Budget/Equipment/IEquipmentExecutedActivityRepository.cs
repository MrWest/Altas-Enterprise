using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget.Equipment
{
    /// <summary>
    /// Contract to be implemented by the repository handling the Data operations for the set of <see cref="IExecutedActivity"/>
    /// of an <see cref="IEquipmentComponent"/>.
    /// </summary>
    public interface IEquipmentExecutedActivityRepository : IExecutedActivityRepository
        //where TItem : class ,IExecutedActivity
        //where TComponent : class ,IBudgetComponent
    {
    }
}
