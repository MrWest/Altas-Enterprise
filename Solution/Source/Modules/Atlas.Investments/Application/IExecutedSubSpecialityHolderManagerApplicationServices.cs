using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Application
{
    public interface IExecutedSubSpecialityHolderManagerApplicationServices: ISubSpecialityHolderManagerApplicationServices<IExecutedSubSpecialityHolder>
    {
        IEnumerable<IExecutedSubSpecialityHolder> BeExecuted(IPlannedSubSpecialityHolder[] plannedItems);
        bool CanBeExecute(IPlannedSubSpecialityHolder[] plannedItems);
    }
}