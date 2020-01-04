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
    ///     Implementation of the contract <see cref="IExpenseConceptManagerApplicationServices" /> being the application services used
    ///     to manage the CRUD operations coming from upper layers regarding to the ExpenseConcept domain entities.
    /// </summary>
    public class SpecialityManagerApplicationServices :
        ItemManagerApplicationServicesBase<ISpeciality, ISpecialityRepository, ISpecialityDomainServices>,
        ISpecialityManagerApplicationServices
    {
        public ISpeciality Export(IDatabaseContext databaseContext, ISpeciality subSpeciality)
        {
            if (Equals(subSpeciality, null))
                return null;
            var expConvert = Repository.GetClone(subSpeciality);
            //if in the context is nothing found
            if (!databaseContext.GetAll<ISpeciality>().Any(x => x.Code == expConvert.Code))
                databaseContext.Add(expConvert);
            else
            {
                expConvert = databaseContext.GetAll<ISpeciality>().First(x => x.Code == expConvert.Code);
            }

            return expConvert;
      
        }
    }
}