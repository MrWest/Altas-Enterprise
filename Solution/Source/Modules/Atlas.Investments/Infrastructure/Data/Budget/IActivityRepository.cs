using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget
{
    public interface IActivityRepository<TActivity>: IBudgetComponentItemRepository<TActivity>
         where TActivity : class, IActivity
         //where TComponent : class, IBudgetComponent
    {
        //TComponent Component { get; set; }
        ISubSpecialityHolder SubSpecialityHolder { get; set; }
    }
}