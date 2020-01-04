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
    ///     Implementation of the contract <see cref="IPhaseManagerApplicationServices" /> being the application services used
    ///     to manage the CRUD operations coming from upper layers regarding to the Phase domain entities.
    /// </summary>
    public class PhaseManagerApplicationServices :
        ItemManagerApplicationServicesBase<IPhase, IPhaseRepository, IPhaseDomainServices>,
        IPhaseManagerApplicationServices
    {
        public IPhase Export(IDatabaseContext databaseContext, IPhase exportable)
        {
            if (Equals(exportable, null))
                return null;
            var expConvert = Repository.GetClone(exportable);
            //if in the context is nothing found
            if (!databaseContext.GetAll<IPhase>().Any(x => x.Code == expConvert.Code))
                databaseContext.Add(expConvert);
            else
            {
                expConvert = databaseContext.GetAll<IPhase>().First(x => x.Code == expConvert.Code);
            }

            return expConvert;
        }
    }
}