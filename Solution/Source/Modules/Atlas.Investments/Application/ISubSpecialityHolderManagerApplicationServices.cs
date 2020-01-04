using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface ISubSpecialityHolderManagerApplicationServices<THolder>:IItemManagerApplicationServices<THolder>
        where THolder:class,ISubSpecialityHolder
    {
        IBudgetComponent BudgetComponent { get; set; }
    }
}