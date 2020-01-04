using System.Linq;
using CompanyName.Atlas.Contracts.Implementation.Application;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Investments.Application;
using CompanyName.Atlas.Investments.Domain.Entities;
using CompanyName.Atlas.Investments.Domain.Services;
using CompanyName.Atlas.Investments.Infrastructure.Data;

namespace CompanyName.Atlas.Investments.Implementation.Application
{
    /// <summary>
    ///     Implementation of the contract <see cref="IInvestmentTypeManagerApplicationServices" /> being the application
    ///     services used to manage the CRUD operations coming from upper layers regarding to the Investment Type domain
    ///     entities.
    /// </summary>
    public class InvestmentTypeManagerApplicationServices :
        ItemManagerApplicationServicesBase<IInvestmentType, IInvestmentTypeRepository, IInvestmentTypeDomainServices>,
        IInvestmentTypeManagerApplicationServices
    {
        public IInvestmentType Export(IDatabaseContext databaseContext, IInvestmentType exportable)
        {
            if (Equals(exportable, null))
                return null;

            var expConvert = Repository.GetClone(exportable);
            //if in the context is nothing found
            if (!databaseContext.GetAll<IInvestmentType>().Any(x => x.Code == expConvert.Code))
                databaseContext.Add(expConvert);
            else
            {
                expConvert = databaseContext.GetAll<IInvestmentType>().First(x => x.Code == expConvert.Code);
            }

            return expConvert;
        }
    }
}