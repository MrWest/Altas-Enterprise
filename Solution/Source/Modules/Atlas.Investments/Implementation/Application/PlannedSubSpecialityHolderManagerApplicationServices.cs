using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data.Budget;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    public class PlannedSubSpecialityHolderManagerApplicationServices: SubSpecialityHolderManagerApplicationServices<IPlannedSubSpecialityHolder,
        IPlannedSubSpecialityHolderRepository,IPlannedSubSpecialityHolderDomainServices>,IPlannedSubSpecialityHolderManagerApplicationServices
    {
        
    }
}