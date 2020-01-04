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
    public class AtlasModuleMainSubjectOfSpecifcation : Specification<IAtlasModuleMainSubject>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public AtlasModuleMainSubjectOfSpecifcation(String assemblyName)
        {
            if (assemblyName == null)
                throw new ArgumentNullException("assemblyName");

            Predicate = acces => acces.AssemblyName != null && Equals(acces.AssemblyName, assemblyName);
        }
    }
    public class AtlasModuleMainSubjectOfQueryable : EntityFrameworkQueryable<AtlasModuleMainSubject>
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="InvestmentElementsOfSpecification" /> given an investment element.
        /// </summary>
        /// <param name="parentInvestmentElement">The <see cref="IInvestmentElement" /> to get its children.</param>
        /// <exception cref="ArgumentNullException"><paramref name="parentInvestmentElement" /> is null.</exception>
        public AtlasModuleMainSubjectOfQueryable(String assemblyName, IEntityFrameworkDbContext<AtlasModuleMainSubject> context) : base(context)
        {
            if (assemblyName == null)
                throw new ArgumentNullException("assemblyName");

            Query = (from e in context.Entities orderby e.Id ascending where e.AssemblyName == assemblyName select e);
        }
    }
}
