using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    /// Represents the application services provider letting the upper layers (like Presentation) perform CRUD-operations 
    /// implying investment period elements (<see cref="IInvestmentElementPeriod"/> instances).
    /// </summary>


    public class InvestmentElementPeriodManagerApplicationServices : ItemManagerApplicationServicesBase<IInvestmentElementPeriod, IPeriodRepository<IInvestmentElementPeriod>, IPeriodDomainService<IInvestmentElementPeriod>>,
        IInvestmentElementPeriodManagerApplicationServices<IInvestmentElementPeriod>
    {
        protected override IPeriodRepository<IInvestmentElementPeriod> Repository
        {
            [SkipUnitOfWork]
            get
            {

                IPeriodRepository<IInvestmentElementPeriod> repository = base.Repository;



                return repository;
            }
        }

    
        //public  bool CanUpdate(IInvestmentElementPeriod item)
        //{
        //    return item!=null;
        //}
    }
}
