using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    public class UnitConverterOfSpecification: Specification<IUnitConverter>
    {
        //     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public UnitConverterOfSpecification(IConvertibleEntity convertibleEntity)
        {
            if (convertibleEntity == null)
                throw new ArgumentNullException("aboveCategory");

            Predicate = convertible => convertible.ConversionForEntity != null && Equals(convertible.ConversionForEntity.Id, convertibleEntity.Id);
        }
    }

    public class UnitConverterOfEntityFrameworkQueryable : EntityFrameworkQueryable<UnitConverter>
    {
        //     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public UnitConverterOfEntityFrameworkQueryable(IConvertibleEntity convertibleEntity, IEntityFrameworkDbContext<UnitConverter> context): base(context)
        {
            if (convertibleEntity == null)
                throw new ArgumentNullException("convertibleEntity");

            Query = (from e in context.Entities orderby e.Id ascending where e.ConversionForEntityId == convertibleEntity.Id  select e);

            // Predicate = convertible => convertible.ConversionForEntity != null && Equals(convertible.ConversionForEntity.Id, convertibleEntity.Id);
        }
    }
}
