using System.Collections.Generic;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Investments.Domain.Entities;

namespace CompanyName.Atlas.Investments.Domain.Services
{
    public interface IExecutedSubSpecialityHolderDomainServices: ISubSpecialityHolderDomainServices<IExecutedSubSpecialityHolder>
    {
        IEnumerable<IExecutedSubSpecialityHolder> Execute(IEnumerable<IPlannedSubSpecialityHolder> dbPlannedItems);
    }
}