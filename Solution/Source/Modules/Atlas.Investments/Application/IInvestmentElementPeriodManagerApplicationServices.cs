using CompanyName.Atlas.Contracts.Application;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Implementation.Application;

namespace CompanyName.Atlas.Investments.Application
{
    /// <summary>
    /// Represents the application services provider letting the upper layers (like Presentation) perform CRUD-operations 
    /// implying investment period elements (<see cref="IInvestmentElementPeriod"/> instances).
    /// </summary>
   public interface IInvestmentElementPeriodManagerApplicationServices<TItem> : IItemManagerApplicationServices<TItem>
        where TItem:class, IPeriod
    
    {
       
    }
}
