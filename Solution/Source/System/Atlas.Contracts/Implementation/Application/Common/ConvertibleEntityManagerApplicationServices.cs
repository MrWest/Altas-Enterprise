using System.Linq;
using CompanyName.Atlas.Contracts.Application.Common;
using CompanyName.Atlas.Contracts.Domain;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Infrastructure.Data;
using CompanyName.Atlas.Contracts.Infrastructure.Data.Common;
using CompanyName.Atlas.Contracts.Presentation.Common;
using Microsoft.Practices.ServiceLocation;

namespace CompanyName.Atlas.Contracts.Implementation.Application.Common
{

    /// <summary>
    ///     Implementation of the contract <see cref="IConvertibleEntityManagerApplicationServices{TEntity}" /> being the application services used
    ///     to manage the CRUD operations coming from upper layers regarding to the Convertibles domain entities.
    /// </summary>
    public class ConvertibleEntityManagerApplicationServices<TEntity,TDomainService> : ItemManagerApplicationServicesBase<TEntity, IConvertibleEntityRepository<TEntity>,TDomainService>,
        IConvertibleEntityManagerApplicationServices<TEntity>
        where TEntity : class, IConvertibleEntity
        where TDomainService:class , IConvertibleEntityDomainService<TEntity>
    {
       

        public TEntity Export(IDatabaseContext databaseContext, TEntity convertible)
        {
            if (databaseContext == null)
                return null;
           // var defaultDatabaseContext = ServiceLocator.Current.GetInstance<IDatabaseContext>();

           var convertibleExported = SaveConvertible(databaseContext, convertible);


            databaseContext.Save();
            return convertibleExported;
        }

        private TEntity SaveConvertible(IDatabaseContext databaseContext, TEntity convertible)
        {
            if (Equals(convertible, null))
                return null;

           var expConvert = Repository.GetClone(convertible);
            //if in the context is nothing found
            if(!databaseContext.GetAll<TEntity>().Any(x=>  x.Letters==expConvert.Letters))
                databaseContext.Add(expConvert);
            else
            {
                expConvert.Id = databaseContext.GetAll<TEntity>().First(x => x.Letters == expConvert.Letters).Id;
            }

            var convertions = ServiceLocator.Current.GetInstance<IUnitConverterRepository>();
            convertions.ConversionForEntity = convertible;

            foreach (IUnitConverter unitConverter in convertions.Entities)
            {
                var expconvertions = convertions.GetClone(unitConverter);
                expconvertions.ConversionForEntity = expConvert;
                if (unitConverter.ConversionUnit != null)
                    expconvertions.ConversionUnit = this.Export(databaseContext, Find(unitConverter.Id)).Id;

                databaseContext.Add(expconvertions);
            }

            return expConvert;
        }

        public void AddFromScratch(string muname, string s)
        {
            var unit = DomainServices.Create();
            unit.Name = muname;
            unit.Letters = s;

            Repository.Add(unit);
        }
    }
}
