using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    public interface ISubSpecialityHolderDomainServices<THolder>: IDomainServices<THolder>
        where THolder:class ,ISubSpecialityHolder
    {
         IBudgetComponent BudgetComponent { get; set; }
    }
}