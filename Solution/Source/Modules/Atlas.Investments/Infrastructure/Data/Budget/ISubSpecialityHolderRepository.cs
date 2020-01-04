using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Infrastructure.Data.Budget
{
    public interface ISubSpecialityHolderRepository<THolder>:IRepository<THolder>
        where THolder:class,ISubSpecialityHolder
    {
        IBudgetComponent BudgetComponent { get; set; }
    }
}