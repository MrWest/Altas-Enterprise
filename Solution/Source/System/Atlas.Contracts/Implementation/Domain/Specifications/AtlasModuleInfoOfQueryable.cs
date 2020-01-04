using System;
using System.Linq;
using CompanyName.Atlas.Contracts.Domain.Common;
using CompanyName.Atlas.Contracts.Implementation.Domain.Entities;
using CompanyName.Atlas.Contracts.Implementation.Domain.EntityFramework;
using CompanyName.Atlas.Contracts.Infrastructure.Data.EntityFramework;

namespace CompanyName.Atlas.Contracts.Implementation.Domain.Specifications
{
    public class AtlasModuleInfoOfQueryable : EntityFrameworkQueryable<AtlasModuleInfo>
    {
        //     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public AtlasModuleInfoOfQueryable(IAtlasUser user, IEntityFrameworkDbContext<AtlasModuleInfo> context): base(context)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            Query = (from e in context.Entities orderby e.Id ascending where e.AtlasUserId == user.Id select e);

            // Predicate = convertible => convertible.ConversionForEntity != null && Equals(convertible.ConversionForEntity.Id, convertibleEntity.Id);
        }
    }
}